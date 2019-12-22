using System;
using System.IO;
using System.Security.Cryptography;


namespace CryptoLib
{
    public class AesUtils
    {
        public const int GCM_TAG_SIZE = 16;

        public static byte[] DecryptKeyWithPassword(string password, byte[] encryptedKey, byte[] kdfIV, byte[] keyIV, int iterations)
        {
            var keyEncryptionKey = PBKdf2.GetHash(password, kdfIV, iterations, 32);

            var decryptedKey = DecryptWithAesCBC(encryptedKey, keyEncryptionKey, keyIV);
            return decryptedKey;
        }

        public static byte[] DecryptWithAesCBC(byte[] ciphertext, byte[] key, byte[] IV)
        {
            using (var aes = new AesCryptoServiceProvider())
            {
                using (var decyptor = aes.CreateDecryptor(key, IV))
                {
                    using (var msi = new MemoryStream(ciphertext))
                    {
                        using (var cs = new CryptoStream(msi, decyptor, CryptoStreamMode.Read))
                        {
                            using (var mso = new MemoryStream())
                            {
                                int read;
                                byte[] buffer = new byte[Ed25519.PrivateKeySizeInBytes];
                                while ((read = cs.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    mso.Write(buffer, 0, read);
                                }
                                return mso.ToArray();
                            }
                        }
                    }
                }
            }
        }

        public static byte[] EncryptKeyWithPassword(string password, byte[] plainKey, out byte[] keyIV, out byte[] kdfIV,
            out int iterations)
        {
            var rnd = new SecureRandom();
            kdfIV = new byte[16];
            iterations = rnd.Next(3072, 8192);
            rnd.NextBytes(kdfIV);

            var keyEncryptionKey = PBKdf2.GetHash(password, kdfIV, iterations, 32);
            var encryptedKey = EncryptWithAesCbc(plainKey, keyEncryptionKey, out keyIV);

            return encryptedKey;

        }

        public static byte[] EncryptWithAesCbc(byte[] plaintext, byte[] key, out byte[] IV)
        {
            var rnd = new SecureRandom();
            IV = new byte[16];
            rnd.NextBytes(IV);

            using (var aes = new AesCryptoServiceProvider())
            using (var encryptor = aes.CreateEncryptor(key, IV))
            using (var ms = new MemoryStream())
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            {
                cs.Write(plaintext, 0, plaintext.Length);
                cs.FlushFinalBlock();
                return ms.ToArray();
            }
        }

        public static void EncryptWithAesGcm(byte[] data, int inOff, int inLen, byte[] outBuf, int outOff, int outLen, byte[] randomKey, byte[] nonce, byte[] ad = null, IAeadBlockCipher cipher = null)
        {
            IAeadBlockCipher gcmCipher;
            if (cipher != null)
            {
                gcmCipher = cipher;
            }
            else
            {
                var engine = new AesFastEngine();
                gcmCipher = new GcmBlockCipher(engine);
            }

            var keyParam = new KeyParameter(randomKey);
            var cipherParams = new AeadParameters(keyParam, 128, nonce, ad);

            gcmCipher.Init(true, cipherParams);
            if (outBuf == null || outLen < gcmCipher.GetOutputSize(inLen))
            {
                throw new Exception("Output buffer must be the size of input buffer + GCM tag size");
            }
            var resultLength = gcmCipher.ProcessBytes(data, inOff, inLen, outBuf, outOff);
            resultLength += gcmCipher.DoFinal(outBuf, resultLength + outOff);
            if (resultLength != outLen)
            {
                throw new Exception("AES GCM buffers size mismatch");
            }
        }

        public static void DecryptAesGcm(byte[] data, int inOff, int inLen, byte[] outBuff, int outOff, int outLen, byte[] key, byte[] nonce, byte[] ad = null, IAeadBlockCipher cipher = null)
        {
            IAeadBlockCipher gcmCipher;
            if (cipher != null)
            {
                gcmCipher = cipher;
            }
            else
            {
                var engine = new AesFastEngine();
                gcmCipher = new GcmBlockCipher(engine);
            }

            var keyParam = new KeyParameter(key);
            var cipherParams = new AeadParameters(keyParam, 128, nonce, ad);

            gcmCipher.Init(false, cipherParams);
            if (outBuff == null || outLen < gcmCipher.GetOutputSize(outLen))
            {
                throw new Exception("Output buffer must be the size of input buffer - GCM tag size");
            }
            var length = gcmCipher.ProcessBytes(data, inOff, inLen, outBuff, outOff);
            length += gcmCipher.DoFinal(outBuff, length + outOff);

            if (length != outLen)
            {
                throw new Exception("AES GCM buffers size mismatch");
            }

        }
    }
}
