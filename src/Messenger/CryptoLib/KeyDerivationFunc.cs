using System;

namespace CryptoLib
{
    public class KeyDerivationFunc
        : AlgorithmIdentifier
    {
        internal KeyDerivationFunc(Asn1Sequence seq)
            : base(seq)
        {
        }

        public KeyDerivationFunc(
            DerObjectIdentifier id,
            Asn1Encodable parameters)
            : base(id, parameters)
        {
        }
    }
}
