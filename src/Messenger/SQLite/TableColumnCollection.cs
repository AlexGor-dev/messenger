using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Complex.Data
{
    public class TableColumnCollection : IEnumerable
    {
        public TableColumnCollection(Table table)
        {
            this.table = table;
            this.hash = new Hashtable();
            this.items = new List<TableColumn>();
        }

        private List<TableColumn> items;
        private Hashtable hash;

        private Table table;
        public Table Table
        {
            get { return this.table; }
        }

        public int Count
        {
            get { return this.items.Count; }
        }

        public TableColumn this[string name]
        {
            get { return this.hash[name.ToUpper()] as TableColumn; }
        }

        public TableColumn this[int index]
        {
            get { return this.items[index]; }
        }

        public IEnumerator GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        public TableColumn Add(string name, Type type, string typeName)
        {
            TableColumn c = new TableColumn(this.table, name, type, typeName, this.items.Count);
            this.items.Add(c);
            this.hash.Add(c.Name.ToUpper(), c);
            return c;
        }

        public void Clear()
        {
            this.items.Clear();
            this.hash.Clear();
        }

        public override string ToString()
        {
            return "Count = " + this.Count;
        }
    }
}
