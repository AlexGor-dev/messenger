using System;

namespace CryptoLib
{
    public class Iso18033KdfParameters
        : IDerivationParameters
    {
        byte[] seed;

        public Iso18033KdfParameters(
            byte[] seed)
        {
            this.seed = seed;
        }

        public byte[] GetSeed()
        {
            return seed;
        }
    }
}
