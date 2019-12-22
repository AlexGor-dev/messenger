using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using CryptoLib;

namespace Messenger
{
    public static class Crypto
    {
        private static byte[] iv = Convert.FromBase64String("vm//Kt3HbB1WfPcVcq33Pg==");

        private static UnicodeEncoding encoder = new UnicodeEncoding();

        public static void Generate(out string publicKey, out string privateKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                privateKey = rsa.ToXmlString(true);
                publicKey = rsa.ToXmlString(false);
            }
        }

        public static string DecryptRsa(string privateKey, byte[] dataByte)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privateKey);
                var decryptedByte = rsa.Decrypt(dataByte, false);
                return encoder.GetString(decryptedByte);
            }
        }

        public static byte[] EncryptRsa(string publicKey, string data)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicKey);
                byte[] dataToEncrypt = encoder.GetBytes(data);
                return rsa.Encrypt(dataToEncrypt, false);
            }
        }


        public static byte[] Transform(byte[] keyData, byte[] valueData, bool encrypt)
        {
            using (RijndaelManaged cipher = new RijndaelManaged())
            {
                if (encrypt)
                    return cipher.CreateEncryptor(keyData, iv).TransformFinalBlock(valueData, 0, valueData.Length);
                return cipher.CreateDecryptor(keyData, iv).TransformFinalBlock(valueData, 0, valueData.Length);
            }
        }

        public static string EncryptRijndael(string key, string data)
        {
            using (RijndaelManaged cipher = new RijndaelManaged())
            {
                BigInteger integer = new BigInteger(key);
                byte[] keyData = integer.ToByteArray();
                cipher.KeySize = keyData.Length * 8;
                cipher.BlockSize = keyData.Length * 8;
                cipher.Padding = PaddingMode.ISO10126;
                cipher.Mode = CipherMode.CBC;
                cipher.Key = keyData;

                var encryptor = cipher.CreateEncryptor(cipher.Key, cipher.IV);
                var msEncrypt = new MemoryStream();
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (var swEncrypt = new StreamWriter(csEncrypt, Encoding.UTF8))
                    swEncrypt.Write(data);
                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }

        public static string DecryptRijndael(string key, string data)
        {
            using (RijndaelManaged cipher = new RijndaelManaged())
            {
                BigInteger integer = new BigInteger(key);
                byte[] keyData = integer.ToByteArray();
                cipher.KeySize = keyData.Length * 8;
                cipher.BlockSize = keyData.Length * 8;
                cipher.Padding = PaddingMode.ISO10126;
                cipher.Mode = CipherMode.CBC;
                cipher.Key = keyData;

                var decryptor = cipher.CreateDecryptor(cipher.Key, cipher.IV);

                byte[] dcipher = Convert.FromBase64String(data);

                using (var msDecrypt = new MemoryStream(dcipher))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt, Encoding.UTF8))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
