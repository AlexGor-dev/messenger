using System;

namespace CryptoLib
{
    public class EdPublicKey : BaseKey, IPublicKey
    {
        public EdPublicKey(byte[] value, byte[] recieverId) : base(value, recieverId)
        {
        }
    }
}
