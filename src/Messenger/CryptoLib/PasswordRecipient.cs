using System;

namespace CryptoLib
{
    public class PasswordRecipient : Asn1Encodable
    {
        public byte[] KdfIV { get; private set; }
        public byte[] KeyIV { get; private set; }
        public int Iterations { get; private set; }
        public byte[] EncryptedKey { get; private set; }

        public PasswordRecipient(byte[] kdfIv, int iterations, byte[] keyIv, byte[] encryptedKey)
        {
            KdfIV = kdfIv;
            Iterations = iterations;
            KeyIV = keyIv;
            EncryptedKey = encryptedKey;
        }

        public PasswordRecipient(Asn1TaggedObject tag)
        {
            var recepient = PasswordRecipientInfo.GetInstance(tag, true);
            if (recepient.Version.Value.IntValue != 0)
                throw new Asn1Exception("Unsupported recipient version");
            if (recepient.KeyEncryptionAlgorithm.Algorithm.Id != PkcsObjectIdentifiers.IdPbeS2.Id)
                throw new Asn1Exception("Unsupported algorithm");

            var paramz = PbeS2Parameters.GetInstance(recepient.KeyEncryptionAlgorithm.Parameters);
            if (paramz.EncryptionScheme.Algorithm.Id != NistObjectIdentifiers.IdAes256Cbc.Id)
                throw new Asn1Exception("Unsupported algorithm");
            if (paramz.KeyDerivationFunc.Algorithm.Id != PkcsObjectIdentifiers.IdPbkdf2.Id)
                throw new Asn1Exception("Unsupported algorithm");

            var kdfParams = (Pbkdf2Params)paramz.KeyDerivationFunc.Parameters;
            if (kdfParams.Prf.Algorithm.Id != PkcsObjectIdentifiers.IdHmacWithSha384.Id)
                throw new Asn1Exception("Unsupported algorithm");


            KdfIV = kdfParams.GetSalt();
            Iterations = kdfParams.IterationCount.IntValue;
            KeyIV = ((Asn1OctetString)paramz.EncryptionScheme.Parameters).GetOctets();
            EncryptedKey = recepient.EncryptedKey.GetOctets();
        }

        public override Asn1Object ToAsn1Object()
        {
            var keyDerevationParameters = new Pbkdf2Params(KdfIV, Iterations,
                new AlgorithmIdentifier(PkcsObjectIdentifiers.IdHmacWithSha384));
            var func = new KeyDerivationFunc(PkcsObjectIdentifiers.IdPbkdf2, keyDerevationParameters);
            var scheme = new EncryptionScheme(NistObjectIdentifiers.IdAes256Cbc, new DerOctetString(KeyIV));
            var keyEncryptionParameters = new PbeS2Parameters(func, scheme);
            var keyEncryptionAlgorithm = new AlgorithmIdentifier(PkcsObjectIdentifiers.IdPbeS2, keyEncryptionParameters);
            var info = new PasswordRecipientInfo(keyEncryptionAlgorithm, new DerOctetString(EncryptedKey));
            return new DerTaggedObject(true, 3, info);
        }

        public static PasswordRecipient GetInstance(object obj)
        {
            if (obj == null)
                return (PasswordRecipient)null;
            return obj as PasswordRecipient ?? new PasswordRecipient(Asn1TaggedObject.GetInstance(obj));
        }
    }
}
