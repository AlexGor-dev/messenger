using System;
using System.Collections.Generic;
using System.Text;

namespace Complex.Data
{
    public class SQLBase : SqlDatabase
    {
        public SQLBase(string id, string dataSource)
            : base(id, dataSource)
        {
        }

        protected override ISqlProvider CreateProvider(string dataSource)
        {
            return new SQLiteProfider(dataSource);
        }
    }
}
