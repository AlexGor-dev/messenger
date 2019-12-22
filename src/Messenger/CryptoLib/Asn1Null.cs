using System;

namespace CryptoLib
{
    public abstract class Asn1Null
        : Asn1Object
    {
        internal Asn1Null()
        {
        }

        public override string ToString()
        {
            return "NULL";
        }
    }
}
