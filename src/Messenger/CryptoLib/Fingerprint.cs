using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLib
{
    public class Fingerprint
    {
        private readonly byte[] fingerprint;

        public Fingerprint(byte[] fingerprint)
        {
            this.fingerprint = fingerprint;
        }
        public byte[] GetValue()
        {
            return this.fingerprint;
        }
    }

}
