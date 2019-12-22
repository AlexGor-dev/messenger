using System;
using System.Globalization;
using System.Collections;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Messenger
{
    public static class Utils
    {

        private static readonly DateTime startTime = new DateTime(1970, 1, 1, 0, 0, 0,DateTimeKind.Utc);

        public static int UtcNow => GetSeconds(DateTime.UtcNow);
        public static long UtcNowMilliseconds => GetMilliseconds(DateTime.UtcNow);

        public static DateTime GetLocalTime(int seconds)
        {
            return startTime.AddSeconds(seconds).ToLocalTime();
        }

        public static int GetSeconds(DateTime time)
        {
            return (int)(time - startTime).TotalSeconds;
        }

        public static long GetMilliseconds(DateTime time)
        {
            return (long)(time - startTime).TotalMilliseconds;
        }

        public static byte[] HexToByteArray(string hexString)
        {
            byte[] data = new byte[hexString.Length / 2];
            for (int i = 0; i < data.Length; i++)
            {
                string byteValue = hexString.Substring(i * 2, 2);
                data[i] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }
            return data;
        }

        public static string ByteArrayToHex(byte[] data)
        {
            string res = "";
            for (int i = 0; i < data.Length; i++)
                res += data[i].ToString("X2");
            return res;        
        }

        public static bool IsDigit(string sub)
        {
            if (string.IsNullOrEmpty(sub)) return false;
            foreach (char ch in sub)
                if (!Char.IsDigit(ch)) return false;
            return true;
        }

        public static string GetNext(string name, IEnumerable items)
        {
            string rname = null;
            if (name != null)
            {
                int i = name.Length - 1;
                while (i >= 0 && Char.IsDigit(name[i]))
                    i--;
                name = name.Substring(0, i + 1);
                rname = name;
            }
            int maxId = 0;
            int maxName = 0;
            if (items != null)
            {
                foreach (object el in items)
                {
                    if (name != null)
                    {
                        INameSource ns = el as INameSource;
                        if (ns != null && ns.Name.StartsWith(name))
                        {
                            string sub = ns.Name.Substring(name.Length).Trim();
                            if (IsDigit(sub))
                            {
                                int num = Convert.ToInt32(sub);
                                maxName = Math.Max(maxName, num);
                            }
                        }
                    }
                }
            }
            maxId++;
            maxName++;

            rname += maxName;
            return rname;
        }

        public static string[] ParseFiftResult(string result)
        {
            int index = result.IndexOf("result:[");
            if(index != -1)
            {
                index += 8;
                int endIndex = result.IndexOf(']', index);
                string data = result.Substring(index, endIndex - index);
                string[] arr = data.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < arr.Length; i++)
                    arr[i] = arr[i].Trim();
                return arr;
            }
            return null;
        }

        public static string ParseAddress(string data)
        {
            int addrLen = 33;
            int fullLen = addrLen * 2;
            string address = data.Substring(data.Length - fullLen);
            int wc = byte.Parse(address.Substring(0, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            address = wc + ":" + address.Substring(2);
            return address;
        }

        public static void Invoke(Control control, EmptyHandler handler)
        {
            if (control.IsHandleCreated)
            {
                if (control.InvokeRequired)
                    control.Invoke(handler);
                else
                    handler();
            }
        }

        public static GraphicsPath RoundRect(RectangleF r, float r1, float r2, float r3, float r4)
        {
            float x = r.X, y = r.Y, w = r.Width, h = r.Height;
            GraphicsPath rr = new GraphicsPath();
            rr.AddBezier(x, y + r1, x, y, x + r1, y, x + r1, y);
            rr.AddLine(x + r1, y, x + w - r2, y);
            rr.AddBezier(x + w - r2, y, x + w, y, x + w, y + r2, x + w, y + r2);
            rr.AddLine(x + w, y + r2, x + w, y + h - r3);
            rr.AddBezier(x + w, y + h - r3, x + w, y + h, x + w - r3, y + h, x + w - r3, y + h);
            rr.AddLine(x + w - r3, y + h, x + r4, y + h);
            rr.AddBezier(x + r4, y + h, x, y + h, x, y + h - r4, x, y + h - r4);
            rr.AddLine(x, y + h - r4, x, y + r1);
            return rr;
        }

    }
}
