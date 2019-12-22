using System;
using System.Windows.Forms;

namespace Messenger
{
    public class OwnerItem : ListViewItem, INameSource
    {
        public OwnerItem(Owner owner)
        {
            this.owner = owner;
            this.Text = this.owner.Name;
            this.Name = this.owner.Name;
            this.timer = new Timer();
            this.timer.Interval = 50;
            this.timer.Tick += Timer_Tick;
            this.owner.Changed += Owner_Changed;
        }



        public override void Remove()
        {
            this.timer.Dispose();
            this.owner.Changed -= Owner_Changed;
            base.Remove();
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            this.waitPosition++;
            this.Redraw();
        }

        private void Owner_Changed(object sender, EventArgs e)
        {
            this.StopWait();
            this.Redraw();
        }

        private Timer timer;

        private int numstart = 0;
        private Owner owner;
        public Owner Owner => owner;

        private int waitPosition;
        public int WaitPosition => waitPosition;

        public bool IsWaitState => this.timer.Enabled;

        private void Invoke(EmptyHandler handler)
        {
            ListView view = this.ListView;
            if (view != null)
                Utils.Invoke(view, handler);
        }

        public void Redraw()
        {
            Invoke(delegate
            {
                this.ListView.RedrawItems(this.Index, this.Index, false);
            });
        }

        public void StartWait()
        {
            Invoke(delegate
            {
                this.numstart++;
                this.timer.Enabled = this.numstart > 0;
            });
        }

        public void StopWait()
        {
            Invoke(delegate
            {
                if(this.numstart > 0)
                    this.numstart--;
                this.timer.Enabled = this.numstart > 0;
            });
        }

    }
}
