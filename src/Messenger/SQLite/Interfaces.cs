using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace Complex.Data
{
    public interface ISqlProvider
    {
        DbCommand SelectTop(string table, string where, int count);
        DbConnection Connection { get; }
        void Open();
        void Close();
        DbCommand CreateCommand(string command);
        DbTransaction BeginTransaction();
        event UpdateHandler Updated;
    }

    public enum UpdateType
    {
        None = 0,
        Delete = 1,
        Insert = 2,
        Update = 4,
    }

    public delegate void UpdateHandler(string table, long rowId, UpdateType type);
    public delegate void UpdateTableHandler<T>(T element, UpdateType type);
}
