using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Messenger
{
    public class OwnerListView : ListView
    {
        public OwnerListView()
        {
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, 50);
            this.SmallImageList = imgList;
            this.OwnerDraw = true;

            this.View = View.Details;
            this.MultiSelect = false;
        }

        private SolidBrush headerBrush = new SolidBrush(Color.FromArgb(239, 239, 239));

        public event EventHandler FocusedItemChanged;

        public int RowHeight
        {
            get => this.SmallImageList.ImageSize.Height;
            set => this.SmallImageList.ImageSize = new Size(1, value);
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public OwnerItem SelectedItem
        {
            get
            {
                OwnerItem item = this.FocusedItem as OwnerItem;
                if (item != null && item.Selected)
                    return item;
                return null;
            }
            set
            {
                if (this.SelectedItem == value) return;
                if (value != null)
                {
                    value.Selected = true;
                    value.Focused = true;
                    this.EnsureVisible(value.Index);

                }
                this.FocusedItem = value;
                this.OnFocusedItemChanged();
            }
        }

        public Owner SelectedOwner
        {
            get
            {
                OwnerItem item = this.SelectedItem;
                if (item != null)
                    return item.Owner;
                return null;
            }
        }

        private void SelectItem(int y)
        {
            this.SelectedItem = this.GetItemAt(5, y) as OwnerItem;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.SelectItem(e.Y);
            base.OnMouseUp(e);
        }

        protected virtual void OnFocusedItemChanged()
        {
            if (this.FocusedItemChanged != null) this.FocusedItemChanged(this, EventArgs.Empty);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if(this.Columns.Count > 0)
                this.Columns[0].Width = this.ClientSize.Width - 2;
            base.OnSizeChanged(e);
        }

        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            Rectangle rect = e.Bounds;
            rect.Inflate(0, 1);
            e.Graphics.FillRectangle(headerBrush, rect);
        }

        protected static int LOWORD(IntPtr value)
        {
            return (short)((int)value & 0xffff);
        }

        protected static int HIWORD(IntPtr value)
        {
            return ((int)value >> 16) & 0xffff;
        }

        //private bool doubleDown = false;
        //protected override void WndProc(ref System.Windows.Forms.Message m)
        //{
        //    switch (m.Msg)
        //    {
        //        case 0x203:
        //            this.doubleDown = true;
        //            break;
        //        case 0x202:
        //            if (this.doubleDown)
        //            {
        //                this.doubleDown = false;
        //                int y = HIWORD(m.LParam);
        //                this.SelectItem(y);
        //                this.OnMouseDoubleClick(new MouseEventArgs(MouseButtons.Left, 2, LOWORD(m.LParam), y, 0));
        //            }
        //            //this.OnMouseUp(new MouseEventArgs(MouseButtons.Left, 1, LOWORD(m.LParam), HIWORD(m.LParam), 0));
        //            break;
        //    }
        //    base.WndProc(ref m);
        //}
    }
}
