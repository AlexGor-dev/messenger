using System;
using System.Collections;
using System.Text;
using System.Globalization;
using Complex.Serialization;

namespace Complex.Data
{
    public partial class TableRow : IEnumerable, IData
    {
        static TableRow()
        {
            numberInfo = new NumberFormatInfo();
            numberInfo.CurrencyDecimalSeparator = ".";

        }

        private static NumberFormatInfo numberInfo;
        //public static NumberFormatInfo NumberInfo
        //{
        //    get { return numberInfo; }
        //}

        public TableRow(Table table)
            :this(table, new object[table.Columns.Count], -1)
        {

        }
        internal TableRow(Table table, object[] values, int index)
        {
            this.table = table;
            this.values = values;
            this.index = index;
        }

        public event EventHandler Changed;
        protected virtual void OnChanged()
        {
            if (this.Changed != null) this.Changed(this, EventArgs.Empty);
        }

        private object[] values;
        public object[] Values
        {
            get { return this.values; }
            set
            {
                this.values = value;
                this.OnChanged();
                if (this.table != null)
                    this.table.Rows.OnChanged(this);
            }
        }

        private Table table;
        public Table Table
        {
            get { return this.table; }
        }

        private int index;
        public int Index
        {
            get { return this.index; }
        }

        public object this[string name]
        {
            get 
            { 
                TableColumn c = this.table.Columns[name];
                if(c != null)
                    return this.values[c.Index];
                return null;
            }
            set
            {
                TableColumn c = this.table.Columns[name];
                if (c != null)
                    this.values[c.Index] = value;
            }
        }

        public object this[string name, object defaultValue]
        {
            get 
            {
                object value = this[name];
                if (value == null)
                    return defaultValue;
                return value;
            }
        }

        public object this[int index]
        {
            get { return this.values[index]; }
        }

        public IEnumerator GetEnumerator()
        {
            return this.values.GetEnumerator();
        }

        public string GetString(string name)
        {
            return Convert.ToString(this[name], numberInfo);
        }

        public string GetString(int index)
        {
            return Convert.ToString(this[index], numberInfo);
        }

        public string ToLine()
        {
            string line = "";
            for (int i = 0; i < this.values.Length; i++)
                line += this.GetString(i) + ";";
            return line;
        }

        public string GetInsert()
        {
            string textCmd = "insert into " + this.table.Name + " (";
            string values = "values (";
            for (int i = 0; i < this.table.Columns.Count; i++)
            {
                TableColumn col = this.table.Columns[i];
                textCmd += col.Name;
                string text = this.GetString(col.Index);
                text = text.Replace("'", "''").Replace("\"", "\"\"");
                values += "\"" + text + "\"";
                if (i < this.table.Columns.Count - 1)
                {
                    textCmd += ",";
                    values += ",";
                }
            }
            textCmd = textCmd + ") " + values + ")";
            return textCmd;
        }

        public override string ToString()
        {
            string text = "";
            foreach (TableColumn c in this.table.Columns)
            {
                text  += c.Name + " = " + Convert.ToString(this[c.Index]) + "; ";
            }
            return text;
        }

        System.Collections.Generic.IEnumerator<IEntry> System.Collections.Generic.IEnumerable<IEntry>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

    }
}
