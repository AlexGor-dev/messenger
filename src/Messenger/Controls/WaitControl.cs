using System;
using System.Windows.Forms;
using System.Drawing;

namespace Messenger
{
    public class WaitControl : UserControl
    {
        public WaitControl()
        {
            this.timer = new Timer();
            this.timer.Interval = 50;
            this.timer.Tick += Timer_Tick;
            this.DoubleBuffered = true;
        }

        protected override void Dispose(bool disposing)
        {
            this.timer.Dispose();
            base.Dispose(disposing);
        }

        private Timer timer;

        private Pen waitPen = new Pen(Color.FromArgb(129, 137, 179), 2);

        private int runCount;
        private int waitPosition;

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.waitPosition++;
            this.Invalidate();
        }

        public void Start()
        {
            this.runCount++;
            this.timer.Enabled = this.runCount > 0;
        }

        public void Stop()
        {
            this.runCount--;
            this.timer.Enabled = this.runCount > 0;
            if (!this.timer.Enabled)
                this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.timer.Enabled)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                Rectangle rect = this.ClientRectangle;
                int size = Math.Min(rect.Width, rect.Height) / 2;
                rect.X += (rect.Width - size) / 2;
                rect.Y += (rect.Height - size) / 2;
                rect.Width = size;
                rect.Height = size;

                DrawWait(e.Graphics, rect, waitPen, waitPosition);

                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            }
        }

        public static void DrawWait(Graphics g, Rectangle rect, Pen pen, int waitPosition)
        {
            int pos = waitPosition * 5;
            rect.Inflate(3, 3);
            g.DrawArc(pen, rect, pos, 45);
            g.DrawArc(pen, rect, pos + 90, 45);
            g.DrawArc(pen, rect, pos + 180, 45);
            g.DrawArc(pen, rect, pos + 270, 45);
        }

    }
}
