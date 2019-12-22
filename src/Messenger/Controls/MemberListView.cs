using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Messenger
{
    public class MemberListView : OwnerListView
    {
        public MemberListView()
        {
            this.stringFormat.LineAlignment = StringAlignment.Center;
            this.stringFormat.Alignment = StringAlignment.Near;
        }


        private int radius = 5;

        private Pen borderPen = new Pen(Color.FromArgb(199, 207, 229));
        private Pen waitPen = new Pen(Color.FromArgb(129, 137, 179));

        private Brush nameBrush = new SolidBrush(Color.FromArgb(89, 97, 119));
        private Brush typeBrush = new SolidBrush(Color.FromArgb(129, 137, 159));
        private Brush selBrush = new SolidBrush(Color.FromArgb(229, 237, 249));
        private Brush createdBrush = new SolidBrush(Color.FromArgb(149, 157, 179));
        private Brush gramBrush = new SolidBrush(Color.FromArgb(99, 197, 99));

        private Font nameFont = new Font("Arial", 12, FontStyle.Bold);
        private Font typeFont = new Font("Arial", 9, FontStyle.Bold);
        private Font createdFont = new Font("Arial", 9, FontStyle.Bold);
        private Font gramFont = new Font("Arial", 9);

        private StringFormat stringFormat = new StringFormat();


        private Brush noneBrush = new SolidBrush(Color.FromArgb(109, 117, 139));
        private Brush onlineBrush = new SolidBrush(Color.LimeGreen);

        public Member SelectedParticipant => base.SelectedOwner as Member;

        public new MemberItem SelectedItem
        {
            get => base.SelectedItem as MemberItem;
            set => base.SelectedItem = value;
        }

        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            MemberItem item = e.Item as MemberItem;
            Member member = item.Member;
            Rectangle rect = e.Bounds;
            rect.Inflate(-1, -1);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            GraphicsPath path = Utils.RoundRect(rect, radius, radius, radius, radius);
            if (e.Item.Selected)
                e.Graphics.FillPath(selBrush, path);

            e.Graphics.DrawPath(borderPen, path);

            Rectangle stateRect = new Rectangle(rect.X + 6, rect.Y + 6, 10, 10);

            if (item.IsWaitState)
            {
                int pos = item.WaitPosition * 5;
                Rectangle waitrect = stateRect;
                waitrect.Inflate(3, 3);
                e.Graphics.DrawArc(waitPen, waitrect, pos, 180);
            }

            if (member.State == ContractState.Offline)
                e.Graphics.FillEllipse(noneBrush, stateRect);
            else
                e.Graphics.FillEllipse(onlineBrush, stateRect);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

            rect.X += 10;
            rect.Width -= 10;
            Rectangle nameRect = rect;
            nameRect.Y += 4;
            nameRect.Height += 4;
            e.Graphics.DrawString(member.Name, nameFont, nameBrush, nameRect, stringFormat);

            Rectangle typeRect = rect;
            typeRect.Y += 2;
            typeRect.Height = 16;
            typeRect.Inflate(-16, 0);
            e.Graphics.DrawString(member.MessagerType.ToString(), typeFont, typeBrush, typeRect, stringFormat);
        }
    }
}
