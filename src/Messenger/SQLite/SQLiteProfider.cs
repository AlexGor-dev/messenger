using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using Mono.Data.Sqlite;

namespace Complex.Data
{
    public class SQLiteProfider : ISqlProvider
    {
        public SQLiteProfider(string fileName)
        {
            this.fileName = fileName;
            this.connection = new SqliteConnection("Data Source=" + fileName);
        }

        private string fileName;

        private event UpdateHandler updated;
        public event UpdateHandler Updated
        {
            add
            {
                if(this.updated == null)
                    this.connection.Update += connection_Update;
                this.updated += value;
            }
            remove
            {
                this.updated -= value;
                if (this.updated == null)
                    this.connection.Update -= connection_Update;
            }
        }

        private static UpdateType GetUpdateType(UpdateEventType type)
        {
            switch (type)
            {
                case UpdateEventType.Delete:
                    return UpdateType.Delete;
                case UpdateEventType.Insert:
                    return UpdateType.Insert;
                case UpdateEventType.Update:
                    return UpdateType.Update;
            }
            return UpdateType.None;
        }

        private void connection_Update(object s, UpdateEventArgs e)
        {
            this.updated(e.Table, e.RowId, GetUpdateType(e.Event));
        }

        private SqliteConnection connection;
        public DbConnection Connection
        {
            get { return this.connection; }
        }

        public void Open()
        {
            this.connection.Open();
        }

        public void Close()
        {
            this.connection.Close();
        }

        public DbCommand CreateCommand(string command)
        {
            return new SqliteCommand(command, this.connection);
        }

        public DbTransaction BeginTransaction()
        {
            return this.connection.BeginTransaction();
        }

        public DbCommand SelectTop(string table, string where, int count)
        {
            return  new SqliteCommand("select * from " + table + " " + where + " limit " + count, this.connection);
        }
    }
}
