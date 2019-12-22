using System;

namespace CryptoLib
{
    public class BaseKey
    {
        public BaseKey(byte[] value, byte[] recieverId)
        {
            Value = value;
            RecieverId = recieverId;
        }
        internal byte[] Value { get; }
        internal byte[] RecieverId { get; }

        public override bool Equals(object obj)
        {
            var k = (obj as EdPublicKey);
            if (k != null)
            {
                return CryptoBytes.ConstantTimeEquals(Value, k.Value) &&
                       CryptoBytes.ConstantTimeEquals(RecieverId, k.RecieverId);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode() ^ RecieverId.GetHashCode();
        }
    }
}
