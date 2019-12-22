using System;

namespace CryptoLib
{
    public abstract class X509NameEntryConverter
    {
        /**
         * Convert an inline encoded hex string rendition of an ASN.1
         * object back into its corresponding ASN.1 object.
         *
         * @param str the hex encoded object
         * @param off the index at which the encoding starts
         * @return the decoded object
         */
        protected Asn1Object ConvertHexEncoded(
            string hexString,
            int offset)
        {
            string str = hexString.Substring(offset);

            return Asn1Object.FromByteArray(Hex.Decode(str));
        }

        /**
         * return true if the passed in string can be represented without
         * loss as a PrintableString, false otherwise.
         */
        protected bool CanBePrintable(
            string str)
        {
            return DerPrintableString.IsPrintableString(str);
        }

        /**
         * Convert the passed in string value into the appropriate ASN.1
         * encoded object.
         *
         * @param oid the oid associated with the value in the DN.
         * @param value the value of the particular DN component.
         * @return the ASN.1 equivalent for the value.
         */
        public abstract Asn1Object GetConvertedValue(DerObjectIdentifier oid, string value);
    }
}
