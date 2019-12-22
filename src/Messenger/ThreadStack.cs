using System;
using System.Collections.Generic;
using System.Threading;

namespace Messenger
{
    public delegate void ParamHandler(object[] param);

    public class ThreadStack
    {
        public static readonly ThreadStack Instance = new ThreadStack();

        private int runCount;

        private List<Handler> handlers = new List<Handler>();

        public bool Contains(ParamHandler handler)
        {
            lock(this.handlers)
            {
                for (int i = 0; i < this.handlers.Count; i++)
                    if (this.handlers[i].handler == handler)
                        return true;
            }
            return false;
        }

        private bool RunHandler(Handler handler)
        {
            if (this.runCount == 0)
            {
                Interlocked.Increment(ref this.runCount);
                Thread thread = new Thread(Run);
                thread.Priority = ThreadPriority.Highest;
                thread.Start(handler);
                return true;
            }
            return false;
        }

        private void RunHandlers()
        {
            lock(this.handlers)
            {
                if(this.handlers.Count > 0)
                {
                    Handler h = this.handlers[0];
                    if(this.RunHandler(h))
                        this.handlers.RemoveAt(0);
                }
            }
        }

        private void Run(object handler)
        {
            Handler h = handler as Handler;
            try
            {
                h.handler(h.param);
            }
            catch (Exception e)
            {

            }
            Interlocked.Decrement(ref this.runCount);
            this.RunHandlers();
        }

        public void Add(ParamHandler handler, params object[] param)
        {
            Handler hd = new Handler(handler, param);
            if (!this.RunHandler(hd))
                lock (this.handlers)
                    this.handlers.Add(hd);
        }

        public void Wait()
        {
            while (this.runCount > 0 || handlers.Count > 0)
                Thread.Sleep(1);
        }

        public static void Run(ParamHandler handler, params object[] param)
        {
            Instance.Add(handler, param);
        }

        private class Handler
        {
            public Handler(ParamHandler handler, object[] param)
            {
                this.handler = handler;
                this.param = param;
            }

            public readonly ParamHandler handler;
            public readonly object[] param;
        }

    }
}
