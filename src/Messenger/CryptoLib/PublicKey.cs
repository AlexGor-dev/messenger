using System;

namespace CryptoLib
{
    public class PublicKey : Asn1Encodable
    {
        public byte[] Key { get; private set; }

        public PublicKey(byte[] key)
        {
            Key = key;
        }

        public PublicKey(Asn1Encodable asn1)
        {
            var k = OriginatorPublicKey.GetInstance(asn1);
            if (k.Algorithm.Algorithm.Id != VirgilIdentifier.Ed25519.Id)
                throw new Asn1Exception("Unsupported algorithm");

            Key = k.PublicKey.GetBytes();
        }

        public override Asn1Object ToAsn1Object()
        {
            return new OriginatorPublicKey(new AlgorithmIdentifier(VirgilIdentifier.Ed25519), Key).ToAsn1Object();
        }

        public static PublicKey GetInstance(object obj)
        {
            if (obj == null)
                return (PublicKey)null;

            if (obj is byte[])
            {
                return new PublicKey(Asn1Object.FromByteArray(Pem.Unwrap((byte[])obj)));
            }
            return obj as PublicKey ?? new PublicKey(Asn1Sequence.GetInstance(obj));
        }
    }
}
