using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using Complex.Serialization;
using Messenger;

namespace Complex.Data
{
    public abstract class SqlDatabase : Disposable, IUnique
    {
        public SqlDatabase(string id, string dataSource)
        {
            this.id = id;
            this.dataSource = dataSource;
            this.provider = this.CreateProvider(this.dataSource);
            this.provider.Open();
        }

        protected override void OnDisposed()
        {
            this.provider.Close();
            base.OnDisposed();
        }

        private string dataSource;
        public string DataSource
        {
            get { return this.dataSource; }
        }

        private string id;
        public string ID
        {
            get { return this.id; }
        }

        private DbTransaction transaction;

        private ISqlProvider provider;
        public ISqlProvider Provider
        {
            get { return this.provider; }
        }

        protected abstract ISqlProvider CreateProvider(string dataSource);

        public void BeginUpdate()
        {
            if(this.transaction == null)
                this.transaction = this.provider.BeginTransaction();
        }

        public void EndUpdate()
        {
            if (this.transaction != null)
                this.transaction.Commit();
            this.transaction = null;
        }

        public bool Exist(string name)
        {
            DbCommand cmd = this.provider.CreateCommand("select * from " + name);
            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {

            }
            return false;
        }

        public static T[] Select<T>(ISqlProvider provider, string name, string where) where T : ISerializable
        {
            using (SqlTable<T> table = new SqlTable<T>(provider, name))
                return table.Select(where);

        }


        public static T SelectSingle<T>(ISqlProvider provider, string name, string where) where T : ISerializable
        {
            using (SqlTable<T> table = new SqlTable<T>(provider, name))
            {
                T[] res = table.Select(where);
                if (res.Length > 0)
                    return res[0];
            }
            return default(T);
        }

    }
}
