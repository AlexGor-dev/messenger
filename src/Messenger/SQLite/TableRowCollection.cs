using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Complex.Data
{
    public class TableRowCollection : IEnumerable
    {
        public TableRowCollection(Table table)
        {
            this.table = table;
            this.items = new List<TableRow>();
        }

        public event TableRowEventHandler Added;
        public event TableRowEventHandler Changed;
        public event TableRowEventHandler Removed;

        private void OnAdded(TableRow row)
        {
            if (this.Added != null) this.Added(this, row);
        }

        protected internal void OnChanged(TableRow row)
        {
            if (this.Changed != null) this.Changed(this, row);
        }

        protected internal void OnRemoved(TableRow row)
        {
            if (this.Removed != null) this.Removed(this, row);
        }

        private List<TableRow> items;

        private Table table;
        public Table Table
        {
            get { return this.table; }
        }

        public int Count
        {
            get { return this.items.Count; }
        }

        public TableRow this[int index]
        {
            get { return this.items[index]; }
        }

        public TableRow Add(object[] values)
        {
            TableRow r = new TableRow(this.table, values, this.items.Count);
            this.items.Add(r);
            this.OnAdded(r);
            return r;
        }

        public TableRow Add()
        {
            object[] values = new object[this.table.Columns.Count];
            return this.Add(values);
        }

        public void Clear()
        {
            this.items.Clear();
        }

        public IEnumerator GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        public override string ToString()
        {
            return "Count = " + this.Count;
        }
    }
}
