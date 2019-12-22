using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace Complex.Serialization
{
    public abstract class Serializable : ISerializable
    {

        public const BindingFlags Bindings = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;


        protected virtual void LoadClassData(IData data)
        {

        }

        protected virtual void SaveClassData(IData data)
        {

        }

        void ISerializable.LoadClassData(IData data)
        {
            this.clasdataLoaded = false;
            this.LoadClassData(data);
            this.OnClassDataLoaded();
            this.clasdataLoaded = true;
        }
        
        void ISerializable.SaveClassData(IData data)
        {
            this.SaveClassData(data);
        }

        private bool clasdataLoaded = true;
        [Browsable(false)]
        public bool IsClassDataLoaded
        {
            get { return this.clasdataLoaded; }
        }

        private event EventHandler classDataLoaded;
        public event EventHandler ClassDataLoaded
        {
            add
            {
                if (this.clasdataLoaded)
                    value(this, EventArgs.Empty);
                this.classDataLoaded += value;
            }
            remove { this.classDataLoaded -= value; }
        }

        protected virtual void OnClassDataLoaded()
        {
            if (this.classDataLoaded != null) this.classDataLoaded(this, EventArgs.Empty);
        }

        public static ConstructorInfo GetConstructor(System.Type t, params Type[] types)
        {
            return t.GetConstructor(Bindings, null, types, null);
        }

        public static ConstructorInfo GetIDataConstructor(System.Type t)
        {
            return GetConstructor(t, typeof(IData));
        }

        public static T Create<T>(params object[] param)
        {
            return (T)Create(typeof(T), param);
        }

        public static object Create(Type type, params object[] param)
        {
            Type[] types = Type.GetTypeArray(param);
            ConstructorInfo info = type.GetConstructor(Bindings, null, types, null);
            if (info != null)
                return info.Invoke(param);
            return null;
        }
    }
}
