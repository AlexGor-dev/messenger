using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Messenger
{
    public class RunmethodParser
    {
        public RunmethodParser()
        {
        }

        public static string GetSliceText(string slice)
        {
            byte[] arr = Utils.HexToByteArray(slice);
            return Encoding.UTF8.GetString(arr, 2, arr.Length - 2);
        }

        public static byte[] GetSliceData(string slice)
        {
            byte[] arr = Utils.HexToByteArray(slice);
            byte[] buff = new byte[arr.Length - 2];
            Array.Copy(arr, 2, buff, 0, buff.Length);
            return buff;
        }

        private static string ParseInt(string data, ref int rindex)
        {
            int index = data.IndexOf(" ", rindex);
            if (index != -1)
            {
                string res = data.Substring(rindex, index - rindex);
                rindex = index + 1;
                return res;
            }
            else
            {
                string res = data.Substring(rindex, data.Length - rindex);
                rindex = data.Length;
                return res;
            }
        }

        private static string ParseSlice(string data, ref int rindex)
        {
            int index = data.IndexOf("Cell{", rindex);
            if (index != -1)
            {
                rindex = index + 5;
                index = data.IndexOf("}", rindex);
                if (index != -1)
                {
                    string res = data.Substring(rindex, index - rindex);
                    rindex = data.IndexOf("}", index + 1) + 2;
                    return res;
                }
            }
            return null;

        }

        public static string[] Parse(string data, params ParseType[] types)
        {
            int index = data.IndexOf("result: ");
            if (index != -1)
            {
                index = data.IndexOf("[", index);
                int endIndex = data.IndexOf("]", index);
                string res = data.Substring(index + 1, endIndex - index - 1).Trim();

                List<string> list = new List<string>();
                int rindex = 0;
                foreach(ParseType type in types)
                {
                    switch(type)
                    {
                        case ParseType.Int:
                            list.Add(ParseInt(res, ref rindex));
                            break;
                        case ParseType.Slice:
                            list.Add(ParseSlice(res, ref rindex));
                            break;
                    }
                }
                return list.ToArray();
            }
            return null;
        }
    }
}
