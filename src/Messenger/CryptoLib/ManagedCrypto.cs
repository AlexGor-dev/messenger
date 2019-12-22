using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CryptoLib
{
    public static class ManagedCrypto
    {
        private const string SIGN = "VIRGIL-DATA-SIGNATURE";
        private const string CHUNK_SIZE = "chunkSize";

        private static EdPrivateKey CheckPrivateKey(IPrivateKey privateKey)
        {
            if (privateKey == null)
            {
                throw new ArgumentNullException(nameof(privateKey));
            }
            if (!(privateKey is EdPrivateKey))
            {
                throw new ArgumentException(nameof(privateKey));
            }
            return (EdPrivateKey)privateKey;
        }

        private static EdPublicKey CheckPublicKey(IPublicKey publicKey)
        {
            if (publicKey == null)
            {
                throw new ArgumentNullException(nameof(publicKey));
            }
            if (!(publicKey is EdPublicKey))
            {
                throw new ArgumentException(nameof(publicKey));
            }
            return (EdPublicKey)publicKey;
        }

        public static byte[] ComputeHash(byte[] data, HashAlgorithm algorithm)
        {
            if (!Hash.SupportedHashes.ContainsKey(algorithm))
            {
                throw new NotImplementedException();
            }

            using (var hash = Hash.SupportedHashes[algorithm]())
            {
                return hash.ComputeHash(data);
            }

        }

        public static byte[] CalculateFingerprint(byte[] data)
        {
            return ComputeHash(data, HashAlgorithm.SHA256);

        }

        public static byte[] Encrypt(byte[] data, byte[] publicKey)
        {
            byte[] encoded = new PublicKey(publicKey).GetDerEncoded();
            byte[] fp = ManagedCrypto.CalculateFingerprint(encoded);
            return Encrypt(data, new EdPublicKey(publicKey, fp));
        }

        public static byte[] GetPublicKey(byte[] privateKey)
        {
            return Ed25519.PublicKeyFromSeed(privateKey);
        }

        public static byte[] Decrypt(byte[] data, byte[] privateKey)
        {
            var publicKey = Ed25519.PublicKeyFromSeed(privateKey);
            var encoded = new PublicKey(publicKey).GetDerEncoded();
            byte[] fp = ManagedCrypto.CalculateFingerprint(encoded);
            return Decrypt(data, new EdPrivateKey(privateKey, fp));
        }

        private static byte[] Encrypt(byte[] data, IPublicKey publicKey)
        {
            var customParams = new Dictionary<string, object>();
            byte[] randomKey;
            byte[] nonce;
            var envelope = MakeEnvelope(out randomKey, new IPublicKey[] { publicKey }, customParams, out nonce);
            var ciphertext = new byte[data.Length + AesUtils.GCM_TAG_SIZE];

            AesUtils.EncryptWithAesGcm(data, 0, data.Length, ciphertext, 0, ciphertext.Length, randomKey, nonce);

            var message = new byte[envelope.Length + ciphertext.Length];
            Buffer.BlockCopy(envelope, 0, message, 0, envelope.Length);
            Buffer.BlockCopy(ciphertext, 0, message, envelope.Length, ciphertext.Length);
            return message;
        }

        private static byte[] MakeEnvelope(out byte[] randomKey, IPublicKey[] recipients, Dictionary<string, object> customParam, out byte[] nonce)
        {
            var rnd = new SecureRandom();
            randomKey = new byte[32];
            nonce = new byte[12];

            rnd.NextBytes(randomKey);
            rnd.NextBytes(nonce);

            var key = randomKey;
            var recs = recipients.Select(r =>
            {
                var pub = CheckPublicKey(r);

                var rec = ECIES.EncryptSymmetricKey(pub.RecieverId, pub.Value, key);
                var model = new PublicKeyRecipient(rec.Id, rec.EphemeralPublicKey, rec.Tag, rec.IV,
                    rec.EncryptedSymmetricKey);
                return model;
            });
            var nonceModel = new Nonce(nonce);
            var envelope = new Envelope(recs, nonceModel, customParam).GetDerEncoded();
            return envelope;
        }

        private static Envelope ExtractEnvelope(Stream inStream)
        {
            var obj = Asn1Object.FromStream(inStream);
            var envelope = Envelope.GetInstance(obj);
            return envelope;
        }

        private static byte[] DecryptSymmetricKey(Envelope envelope, IPrivateKey privateKey)
        {
            var pk = CheckPrivateKey(privateKey);
            foreach (var recipient in envelope.Recipients)
            {
                var rec = recipient as PublicKeyRecipient;
                if (rec != null)
                {
                    var pkRec = rec;
                    if (CryptoBytes.ConstantTimeEquals(pk.RecieverId, pkRec.Id))
                    {
                        var decryptedSymmetricKey = ECIES.DecryptSymmetricKey(pkRec, pk.Value);
                        return decryptedSymmetricKey;
                    }
                }
            }
            throw new ArgumentException("key");
        }

        public static byte[] Decrypt(byte[] data, IPrivateKey privateKey)
        {

            var stream = new MemoryStream(data);
            var envelope = ExtractEnvelope(stream);

            var decryptedSymmetricKey = DecryptSymmetricKey(envelope, privateKey);

            var ciphertext = new byte[stream.Length - stream.Position];
            stream.Read(ciphertext, 0, ciphertext.Length);
            var plaintext = new byte[ciphertext.Length - AesUtils.GCM_TAG_SIZE];
            AesUtils.DecryptAesGcm(ciphertext, 0, ciphertext.Length, plaintext, 0, plaintext.Length, decryptedSymmetricKey, envelope.Nonce.Content);

            return plaintext;

        }

    }
}
