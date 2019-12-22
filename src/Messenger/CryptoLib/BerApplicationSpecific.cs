using System;

namespace CryptoLib
{
    public class BerApplicationSpecific
        : DerApplicationSpecific
    {
        public BerApplicationSpecific(
            int tagNo,
            Asn1EncodableVector vec)
            : base(tagNo, vec)
        {
        }
    }
}
