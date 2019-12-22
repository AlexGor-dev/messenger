using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using Complex.Serialization;

namespace Complex.Data
{
    public class Table : Disposable, IEnumerable
    {
        private static object async = new object();

        public static Table FromCommand(DbCommand command)
        {
            lock (async)
            {
                DbDataReader reader = null;
                try
                {
                    reader = command.ExecuteReader();
                    Table table = new Table();
                    table.CreateColumns(reader);
                    table.CreateRows(reader);
                    return table;
                }
                catch(Exception e)
                {

                }
                finally
                {
                    if (reader != null)
                    {
                        reader.Close();
                        reader.Dispose();
                    }
                }
                return null;
            }
        }

        public Table()
            :this("")
        {

        }

        public Table(string name)
        {
            this.name = name;
            this.columns = new TableColumnCollection(this);
            this.rows = new TableRowCollection(this);
        }

        private string name;
        public string Name
        {
            get { return this.name; }
        }

        private TableColumnCollection columns;
        public TableColumnCollection Columns
        {
            get { return this.columns; }
        }

        private TableRowCollection rows;
        public TableRowCollection Rows
        {
            get { return this.rows; }
        }

        public IEnumerator GetEnumerator()
        {
            return this.rows.GetEnumerator();
        }

        public void CreateColumns(DbCommand command)
        {
            using (DbDataReader reader = command.ExecuteReader())
                this.CreateColumns(reader);
        }

        public void CreateColumns(DbDataReader reader)
        {
            this.columns.Clear();
            for (int i = 0; i < reader.FieldCount; i++)
                this.columns.Add(reader.GetName(i), reader.GetFieldType(i), reader.GetDataTypeName(i));
        }

        private void CreateRows(DbDataReader reader)
        {
            this.rows.Clear();
            while (reader.Read())
            {
                object[] values = new object[reader.FieldCount];
                reader.GetValues(values);
                this.rows.Add(values);
            }
        }

        public TableRowCollection Select(DbDataReader reader)
        {
            TableRowCollection rows = new TableRowCollection(this);
            while (reader.Read())
            {
                object[] values = new object[reader.FieldCount];
                reader.GetValues(values);
                rows.Add(values);
            }
            return rows;
        }

        public TableRowCollection Select(DbCommand command)
        {
            using (DbDataReader reader = command.ExecuteReader())
                return this.Select(reader);
        }

        public override string ToString()
        {
            return "Columns = " + this.columns.Count + "; Rows = " + this.rows.Count;
        }
    }
}
