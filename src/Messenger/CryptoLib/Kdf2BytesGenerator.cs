using System;

namespace CryptoLib
{
    public class Kdf2BytesGenerator
        : BaseKdfBytesGenerator
    {
        /**
		* Construct a KDF2 bytes generator. Generates key material
		* according to IEEE P1363 or ISO 18033 depending on the initialisation.
		*
		* @param digest the digest to be used as the source of derived keys.
		*/
        public Kdf2BytesGenerator(IDigest digest)
            : base(1, digest)
        {
        }
    }
}
