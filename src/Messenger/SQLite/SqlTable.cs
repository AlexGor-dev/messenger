using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data.Common;
using Complex.Serialization;
using Complex.Data;

namespace Complex.Data
{
    public class SqlTable<T> : Table where T : ISerializable
    {
        public SqlTable(ISqlProvider provider, string name)
            :base(name)
        {
            this.provider = provider;
            using (DbCommand cmd = this.provider.SelectTop(this.Name, "", 0))
                this.CreateColumns(cmd);

            this.constructor = Serializable.GetIDataConstructor(typeof(T));
        }

        private ISqlProvider provider;
        private ConstructorInfo constructor;

        private event UpdateTableHandler<T> updated;
        public event UpdateTableHandler<T> Updated
        {
            add
            {
                if(this.updated == null)
                    this.provider.Updated += provider_Updated;
                this.updated += value;
            }
            remove
            {
                this.updated -= value;
                if (this.updated == null)
                    this.provider.Updated -= provider_Updated;
            }
        }

        private void provider_Updated(string table, long rowId, UpdateType type)
        {
            switch (type)
            {
                case UpdateType.Insert:
                case UpdateType.Delete:
                    if (this.updated != null)
                    {
                        if (table == this.Name)
                        {
                            using (DbCommand cmd = this.SelectCommand("where rowid=" + rowId))
                            {
                                TableRowCollection rows = base.Select(cmd);
                                TableRow row = rows[0];
                                T el = this.CreateElement(row);
                                this.updated(el, type);
                            }
                        }
                    }
                    break;
            }
        }

        public T this[string id]
        {
            get
            {
                using (DbCommand cmd = this.SelectCommand("where id='" + id + "'"))
                {
                    T[] els = this.Select(cmd);
                    if (els.Length > 0)
                        return els[0];
                }
                return default(T);
            }
        }

        public T[] Get(string field, string value)
        {
            using (DbCommand cmd = this.SelectCommand("where " + field + "='" + value + "'"))
                return this.Select(cmd);
        }

        private void Execute(string cmd)
        {
            using (DbCommand dbcmd = this.provider.CreateCommand(cmd))
                dbcmd.ExecuteNonQuery();
        }

        public void Insert(TableRow row)
        {
            this.Execute(row.GetInsert());
        }

        public void Insert(T element)
        {
            TableRow row = new TableRow(this);
            element.SaveClassData(row);
            this.Insert(row);
        }

        public void Update(TableRow row)
        {
            string updateCmd = "update " + this.Name + " set ";
            for (int i = 0; i < this.Columns.Count; i++)
            {
                TableColumn col = this.Columns[i];
                updateCmd += col.Name + "='" + row.GetString(col.Index) + "'";
                if (i < this.Columns.Count - 1)
                    updateCmd += ",";
            }
            updateCmd += " where id='" + row["id"] + "'";
            this.Execute(updateCmd);
        }

        public void Delete(string id)
        {
            this.Execute("delete from " + this.Name + " where id='" + id + "'");
        }

        public void DeleteWhere(string where)
        {
            this.Execute("delete from " + this.Name + " " + where);
        }

        public void Update(T element)
        {
            TableRow row = new TableRow(this);
            element.SaveClassData(row);
            this.Update(row);
        }

        public SqlTable<T> CloneStructure(string name)
        {
            string columns = "";
            for (int i = 0; i < this.Columns.Count; i++)
            {
                TableColumn column = this.Columns[i];
                columns += column.Name + " " + column.TypeName;
                if (i < this.Columns.Count - 1)
                    columns += ", ";
            }
            this.Execute("CREATE TABLE " + name + " (" + columns + ");");
            return new SqlTable<T>(this.provider, name);
        }

        private DbCommand SelectCommand(string fields, string where)
        {
            return this.provider.CreateCommand("select " + fields + " from " + this.Name + " " + where);
        }

        private DbCommand SelectCommand(string where)
        {
            return SelectCommand("*", where);
        }

        private T CreateElement(TableRow row)
        {
            T el = (T)this.constructor.Invoke(new object[] { row });
            el.LoadClassData(row);
            return el;
        }

        private new T[] Select(DbCommand cmd)
        {
            TableRowCollection rows = base.Select(cmd);
            T[] items = new T[rows.Count];
            for (int i = 0; i < items.Length; i++)
                items[i] = this.CreateElement(rows[i]);
            return items;
        }

        public T[] Select(string where)
        {
            using (DbCommand cmd = this.SelectCommand(where))
                return this.Select(cmd);
        }

        public T[] Select()
        {
            return this.Select(null as string);
        }
    }
}
