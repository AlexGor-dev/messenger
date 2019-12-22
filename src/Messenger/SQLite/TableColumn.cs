using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Complex.Data
{
    public partial class TableColumn : IEnumerable
    {
        internal TableColumn(Table table, string name, Type type, string typeName, int index)
        {
            this.table = table;
            this.name = name;
            this.type = type;
            this.typeName = typeName;
            this.index = index;
        }

        private Table table;
        public Table Table
        {
            get { return this.table; }
        }

        private Type type;
        public Type Type
        {
            get { return this.type; }
        }

        private string typeName;
        public string TypeName => typeName;

        public object this[int index]
        {
            get { return this.table.Rows[index][this.index]; }
        }

        private string name;
        public string Name
        {
            get { return this.name; }
        }

        private int index;
        public int Index
        {
            get { return this.index; }
        }

        public IEnumerator GetEnumerator()
        {
            foreach (TableRow row in this.table)
            {
                yield return row[this.index];
            }
        }

        public override string ToString()
        {
            return "Name = " + this.name + "; Index = " + this.index + ";";
        }
    }
}
