using System;

namespace CryptoLib
{
    public class EdPrivateKey : BaseKey, IPrivateKey
    {
        public EdPrivateKey(byte[] value, byte[] recieverId) : base(value, recieverId)
        {
        }
    }
}
