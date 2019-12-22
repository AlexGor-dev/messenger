﻿using System;

namespace CryptoLib
{
    public class OidTokenizer
    {
        private string oid;
        private int index;

        public OidTokenizer(
            string oid)
        {
            this.oid = oid;
        }

        public bool HasMoreTokens
        {
            get { return index != -1; }
        }

        public string NextToken()
        {
            if (index == -1)
            {
                return null;
            }

            int end = oid.IndexOf('.', index);
            if (end == -1)
            {
                string lastToken = oid.Substring(index);
                index = -1;
                return lastToken;
            }

            string nextToken = oid.Substring(index, end - index);
            index = end + 1;
            return nextToken;
        }
    }
}
