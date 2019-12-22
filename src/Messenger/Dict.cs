using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Messenger
{
    [DebuggerDisplay("Count = {Count}"), DebuggerTypeProxy(typeof(CollectionDebugView<,>))]
    public class Dict<K, V> : IEnumerable
    {
        private List<V> items = new List<V>();
        private Dictionary<K, V> hash = new Dictionary<K, V>();

        public int Count => items.Count;
        public Dictionary<K, V>.ValueCollection Values => hash.Values;

        public event EventHandler<V> Added;
        public event EventHandler<V> Removed;

        public V this[int index]
        {
            get => this.items[index];
            set => this.items[index] = value;
        }

        public V Get(K key)
        {
            V value;
            if (this.hash.TryGetValue(key, out value))
                return value;
            return default(V);
        }

        public bool Add(K key, V element)
        {
            if (!this.hash.ContainsKey(key))
            {
                this.hash.Add(key, element);
                this.items.Add(element);
                if (this.Added != null) this.Added(this, element);
                return true;
            }
            return false;
        }

        public void AddCore(K key, V element)
        {
            this.hash.Add(key, element);
            this.items.Add(element);
        }

        public void Remove(K key)
        {
            V element = this.hash[key];
            if (!element.Equals(default(V)))
            {
                this.items.Remove(element);
                this.hash.Remove(key);
                if (this.Removed != null) this.Removed(this, element);
            }
        }

        public bool Contains(K key)
        {
            if (key != null)
                return this.hash.ContainsKey(key);
            return false;
        }

        public V[] ToArray()
        {
            return this.items.ToArray();
        }

        public void Clear()
        {
            this.hash.Clear();
            this.items.Clear();
        }

        public void Sort(IComparer<V> comparer)
        {
            this.items.Sort(comparer);
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)items).GetEnumerator();
        }
    }

    public class UDict<T> : Dict<string, T> where T : IUnique
    {
        public T this[string key] => Get(key);

        public void Add(T element)
        {
            base.Add(element.ID, element);
        }

        public void Remove(T element)
        {
            base.Remove(element.ID);
        }

        public bool Contains(T element)
        {
            return base.Contains(element.ID);
        }

    }

    public class CollectionDebugView<K, V>
    {
        public CollectionDebugView(Dict<K, V> collection)
        {
            this.collection = collection;
        }

        private Dict<K, V> collection;

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public V[] Items
        {
            get { return this.collection.ToArray(); }
        }

    }
}
