using System;
using System.Linq;
using System.Security.Cryptography;

namespace CryptoLib
{
    public static class ECIES
    {

        public static PublicKeyRecipient EncryptSymmetricKey(byte[] id, byte[] publicKey, byte[] symmetricKey)
        {
            if (publicKey == null)
                throw new ArgumentNullException("publicKey");
            if (symmetricKey == null)
                throw new ArgumentNullException("symmetricKey");
            if (publicKey.Length != Ed25519.PublicKeySizeInBytes)
                throw new ArgumentException("publicKey");
            var random = new SecureRandom();
            var seed = random.GenerateSeed(Ed25519.PrivateKeySeedSizeInBytes);
            byte[] ephPub, ephPriv;
            Ed25519.KeyPairFromSeed(out ephPub, out ephPriv, seed);

            var shared = Ed25519.KeyExchange(publicKey, ephPriv);

            var kdf = new Kdf2BytesGenerator(new Sha384Digest());
            kdf.Init(new KdfParameters(shared, null));
            var derivedKeys = new byte[80];
            kdf.GenerateBytes(derivedKeys, 0, 80); // 32 bytes - AES key + 48 bytes HMAC key

            var keyEncryptionKey = derivedKeys.Take(32).ToArray();
            byte[] keyIv;

            var encryptedKey = AesUtils.EncryptWithAesCbc(symmetricKey, keyEncryptionKey, out keyIv);

            byte[] tag;
            using (var macFunc = new HMACSHA384(derivedKeys.Skip(32).ToArray()))
            {
                macFunc.Initialize();
                tag = macFunc.ComputeHash(encryptedKey);
            }

            return new PublicKeyRecipient(id, ephPub, tag, keyIv, encryptedKey);
        }

        public static byte[] DecryptSymmetricKey(PublicKeyRecipient model, byte[] privateKey)
        {
            //if (privateKey?.Length !=Ed25519.Ed25519.ExpandedPrivateKeySizeInBytes)
            //    throw new ArgumentException("privateKey");
            if (model == null)
                throw new ArgumentNullException("model");
            //if (model.EphemeralPublicKey?.Length != Ed25519.PublicKeySizeInBytes)
            //    throw new ArgumentException("EphemeralPublicKey");
            if (model.EncryptedSymmetricKey == null || model.EncryptedSymmetricKey.Length == 0)
                throw new ArgumentException("EncryptedSymmetricKey");
            if (model.IV?.Length != 16)
                throw new ArgumentException("IV");
            if (model.Tag.Length != 48)
                throw new ArgumentException("Tag");

            var shared = Ed25519.KeyExchange(model.EphemeralPublicKey, privateKey);

            var kdf = new Kdf2BytesGenerator(new Sha384Digest());
            kdf.Init(new KdfParameters(shared, null));
            var derivedKeys = new byte[80];
            kdf.GenerateBytes(derivedKeys, 0, 80); // 32 bytes - AES key + 48 bytes HMAC key

            byte[] tag;
            using (var macFunc = new HMACSHA384(derivedKeys.Skip(32).ToArray()))
            {
                macFunc.Initialize();
                tag = macFunc.ComputeHash(model.EncryptedSymmetricKey);
            }
            //if (!Arrays.AreEqual(tag, model.Tag))
            //{
            //    throw new ArgumentException("Tag");
            //}

            return AesUtils.DecryptWithAesCBC(model.EncryptedSymmetricKey, derivedKeys.Take(32).ToArray(), model.IV);
        }
    }
}
