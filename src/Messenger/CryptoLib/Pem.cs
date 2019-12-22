using System;
using System.IO;
using System.Text;

namespace CryptoLib
{
    public static class Pem
    {
        public static byte[] Unwrap(byte[] data)
        {
            var str = Encoding.UTF8.GetString(data);
            var reader = new StringReader(str);
            var pemReader = new PemReader(reader);
            try
            {
                var obj = pemReader.ReadPemObject();
                return obj.Content;
            }
            catch (Exception) // try simple base64 instead
            {
                try
                {
                    var res = Base64.Decode(data);
                    return res;
                }
                catch (Exception) // Ok, it's not base64. Maybe Pem?
                {
                    return data;
                }
            }
        }
    }
}
