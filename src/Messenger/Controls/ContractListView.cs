using System;
using System.Drawing;
using System.Windows.Forms;

namespace Messenger
{
    public class ContractListView : OwnerListView
    {
        public ContractListView()
        {

            this.stringFormat.LineAlignment = StringAlignment.Center;
            this.stringFormat.Alignment = StringAlignment.Near;
        }

        private Pen borderPen = new Pen(Color.FromArgb(169, 177, 199));
        private Pen waitPen = new Pen(Color.FromArgb(129, 137, 179));

        private Brush nameBrush = new SolidBrush(Color.FromArgb(69, 77, 99));
        private Brush typeBrush = new SolidBrush(Color.FromArgb(129, 137, 159));
        private Brush selBrush = new SolidBrush(Color.FromArgb(219, 227, 249));
        private Brush createdBrush = new SolidBrush(Color.FromArgb(149, 157, 179));
        private Brush gramBrush = new SolidBrush(Color.FromArgb(99, 197, 99));

        private Font nameFont = new Font("Arial", 12, FontStyle.Bold);
        private Font typeFont = new Font("Arial", 9, FontStyle.Bold);
        private Font createdFont = new Font("Arial", 9, FontStyle.Bold);
        private Font gramFont = new Font("Arial", 9);

        private StringFormat stringFormat = new StringFormat();


        private Brush noneBrush = new SolidBrush(Color.FromArgb(109, 117, 139));
        private Brush onlineBrush = new SolidBrush(Color.LimeGreen);

        public new ContractItem SelectedItem
        {
            get => base.SelectedItem as ContractItem;
            set => base.SelectedItem = value;
        }

        public Contract SelectedContract => base.SelectedOwner as Contract;

        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            ContractItem item = e.Item as ContractItem;
            Contract contract = item.Contract;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Rectangle rect = e.Bounds;
            rect.Inflate(-2, -2);
            if (e.Item.Selected)
                e.Graphics.FillRectangle(selBrush, rect);

            e.Graphics.DrawRectangle(borderPen, rect);

            Rectangle stateRect = new Rectangle(rect.X + 6, rect.Y + 6, 10, 10);

            if (item.IsWaitState)
                WaitControl.DrawWait(e.Graphics, stateRect, waitPen, item.WaitPosition);

            if (contract.State == ContractState.Offline)
                e.Graphics.FillEllipse(noneBrush, stateRect);
            else
                e.Graphics.FillEllipse(onlineBrush, stateRect);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

            rect.X += 10;
            rect.Width -= 10;
            Rectangle nameRect = rect;
            nameRect.Y += 4;
            nameRect.Height += 4;
            e.Graphics.DrawString(contract.Name, nameFont, nameBrush, nameRect, stringFormat);

            Rectangle typeRect = rect;
            typeRect.Y += 2;
            typeRect.Height = 16;
            typeRect.Inflate(-16, 0);
            e.Graphics.DrawString(contract.Type.ToString(), typeFont, typeBrush, typeRect, stringFormat);

            MessengerContract messenger = contract as MessengerContract;
            if (messenger != null)
            {
                typeRect.X += (int)e.Graphics.MeasureString(contract.Type.ToString(), typeFont).Width + 10;
                e.Graphics.DrawString(messenger.MessagerType.ToString(), typeFont, typeBrush, typeRect, stringFormat);
            }

            Rectangle createRect = typeRect;
            createRect.Width = 90;
            createRect.X = rect.Right - createRect.Width;
            string text = "not created?";
            if (contract.IsCreated)
                text = "created!";
            e.Graphics.DrawString(text, createdFont, createdBrush, createRect, stringFormat);

            Rectangle gramRect = nameRect;
            gramRect.Width = 90;
            gramRect.X = rect.Right - gramRect.Width;
            e.Graphics.DrawString("grams: " + contract.Grams, gramFont, gramBrush, gramRect, stringFormat);


            base.OnDrawItem(e);
        }
    }
}
