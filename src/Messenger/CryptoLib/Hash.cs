using System;
using System.Collections.Generic;

namespace CryptoLib
{
    public static class Hash
    {
        public static Dictionary<HashAlgorithm, Func<System.Security.Cryptography.HashAlgorithm>> SupportedHashes = new Dictionary<HashAlgorithm, Func<System.Security.Cryptography.HashAlgorithm>>()
        {
            {HashAlgorithm.MD5, System.Security.Cryptography.MD5.Create},
            {HashAlgorithm.SHA1, System.Security.Cryptography.SHA1.Create},
            {HashAlgorithm.SHA256, System.Security.Cryptography.SHA256.Create},
            {HashAlgorithm.SHA384, System.Security.Cryptography.SHA384.Create},
            {HashAlgorithm.SHA512, System.Security.Cryptography.SHA512.Create},
        };
    }

    public enum HashAlgorithm
    {
        MD5,
        SHA1,
        SHA224,
        SHA256,
        SHA384,
        SHA512
    }


}
