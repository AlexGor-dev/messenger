using System;

namespace CryptoLib
{
    public class Nonce : Asn1Encodable
    {
        public byte[] Content { get; set; }

        public Nonce(byte[] content)
        {
            Content = content;
        }

        public Nonce(Asn1Sequence asn1Sequence)
        {
            var info = EncryptedContentInfo.GetInstance(asn1Sequence);
            if (PkcsObjectIdentifiers.Data.Id != info.ContentType.Id)
                throw new Asn1Exception("Unsupported algorithm");

            if (info.ContentEncryptionAlgorithm.Algorithm.Id != NistObjectIdentifiers.IdAes256Gcm.Id)
                throw new Asn1Exception("Unsupported algorithm");
            Content = ((DerOctetString)info.ContentEncryptionAlgorithm.Parameters).GetOctets();
        }

        public override Asn1Object ToAsn1Object()
        {
            var algo = new AlgorithmIdentifier(NistObjectIdentifiers.IdAes256Gcm,
                new DerOctetString(Content));
            return new EncryptedContentInfo(PkcsObjectIdentifiers.Data, algo, null).ToAsn1Object();
        }

        public static Nonce GetInstance(object obj)
        {
            if (obj == null)
                return null;
            return obj as Nonce ?? new Nonce(Asn1Sequence.GetInstance(obj));
        }
    }
}
