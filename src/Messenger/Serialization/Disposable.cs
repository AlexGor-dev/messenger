using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Complex.Serialization
{
    public abstract class Disposable : Serializable, IDisposable
    {
        static Disposable()
        {

        }

        public static bool Dispose(object value)
        {
            try
            {
                if (value is IDisposable)
                {
                    (value as IDisposable).Dispose();
                    return true;
                }
                else if (value is Disposable)
                {
                    (value as Disposable).Dispose();
                    return true;
                }
            }
            catch (Exception e)
            {

            }
            return false;
        }

        public static void Dispose(IEnumerable values)
        {
            if (values != null)
            {
                foreach (object value in values)
                    Dispose(value);
            }
        }

        public Disposable()
        {
            //Add(this);
        }

        ~Disposable()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            if (!this.disposed)
            {
                this.disposed = true;
                //Remove(this);
                this.OnDisposed();
                GC.SuppressFinalize(this);
            }
        }

        [field:NonSerialized]
        public event EventHandler Disposed;

        protected virtual void OnDisposed()
        {
            if (this.Disposed != null) this.Disposed(this, EventArgs.Empty);
        }

        [NonSerialized]
        private bool disposed = false;
        [Browsable(false)]
        public bool IsDisposed
        {
            get { return this.disposed; }
        }
    }
}
