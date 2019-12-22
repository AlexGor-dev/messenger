using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLib
{
    public class CryptoTest
    {
        public static void Test()
        {

            //var text = Encoding.UTF8.GetBytes("При этом другие эксперты считают, что различные виды насилия уже прописаны в Уголовном кодексе, поэтому описывать их ещё раз не имеет смысла");
            ////byte[] sign = c.Sign(text, privateKey);
            ////bool res = c.Verify(text, sign, publicKey);

            //var random = new SecureRandom();
            //var seed = random.GenerateSeed(Ed25519.PrivateKeySeedSizeInBytes);
            //byte[] ephPub, ephPriv;
            //Ed25519.KeyPairFromSeed(out ephPub, out ephPriv, seed);

            //var encoded = new PublicKey(ephPub).GetDerEncoded();
            //var fp = ManagedCrypto.CalculateFingerprint(encoded).GetValue();
            //EdPublicKey pk = new EdPublicKey(ephPub, fp);
            //EdPrivateKey privateKey = new EdPrivateKey(ephPriv, fp);

            //byte[] ct = ManagedCrypto.Encrypt(text, pk);
            //var dec = ManagedCrypto.Decrypt(ct, privateKey);
            //string res = Encoding.UTF8.GetString(dec);


        }
    }
}
