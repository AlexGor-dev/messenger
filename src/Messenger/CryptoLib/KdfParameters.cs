using System;

namespace CryptoLib
{
    public class KdfParameters : IDerivationParameters
    {
        byte[] iv;
        byte[] shared;

        public KdfParameters(
            byte[] shared,
            byte[] iv)
        {
            this.shared = shared;
            this.iv = iv;
        }

        public byte[] GetSharedSecret()
        {
            return shared;
        }

        public byte[] GetIV()
        {
            return iv;
        }
    }
}
