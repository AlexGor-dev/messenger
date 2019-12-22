using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Messenger
{
    public class MessageListView : ListView
    {
        public MessageListView()
        {
            this.header = new ColumnHeader();
            this.header.Width = 500;
            this.Columns.Add(this.header);

            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, 16);
            this.SmallImageList = imgList;

            this.OwnerDraw = true;
            this.BorderStyle = BorderStyle.None;
            this.View = View.Details;

            this.textFormat.LineAlignment = StringAlignment.Center;
            this.textFormat.Alignment = StringAlignment.Near;

            this.DoubleBuffered = true;
        }

        private float radius = 10;

        private ColumnHeader header;

        private Pen borderPen = new Pen(Color.FromArgb(209, 217, 239));

        private Font textFont = new Font("Arial", 9);
        private Font timeFont = new Font("Arial", 8);
        private Font ownerFont = new Font("Arial", 9, FontStyle.Bold);

        private SolidBrush backBrush = new SolidBrush(Color.FromArgb(219, 227, 249));
        private SolidBrush senderBackBrush = new SolidBrush(Color.FromArgb(219, 237, 249));

        private SolidBrush textBrush = new SolidBrush(Color.FromArgb(69, 77, 99));
        private SolidBrush ownerBrush = new SolidBrush(Color.FromArgb(99, 117, 129));
        private SolidBrush timeBrush = new SolidBrush(Color.FromArgb(149, 157, 179));

        private SolidBrush headerBrush = new SolidBrush(Color.FromArgb(239, 239, 239));

        private StringFormat textFormat = new StringFormat();

        private void DrawHeaderItem(DrawListViewItemEventArgs e, MessageItem item, Rectangle rect, SolidBrush brush)
        {
            Rectangle bounds = e.Bounds;

            int height = (int)(rect.Height * 1.0f);
            rect.Y = rect.Bottom - height;
            rect.Height = height + 1;


            using (GraphicsPath path = Utils.RoundRect(rect, radius, radius, 0, 0))
                e.Graphics.FillPath(brush, path);

            Owner sender = item.Body.Sender;
            Rectangle orect = bounds;

            int width = (int)e.Graphics.MeasureString(sender.Name, ownerFont).Width + 10;
            orect.X += 40;
            orect.Width = width;

            textFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(sender.Name, ownerFont, ownerBrush, orect, textFormat);
        }

        private void DrawTextItem(DrawListViewItemEventArgs e, MessageItem item, Rectangle rect, SolidBrush brush)
        {
            rect.Width += 1;
            e.Graphics.FillRectangle(brush, rect);
            rect.Inflate(-8, 0);
            textFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(item.Text, textFont, textBrush, rect, textFormat);
        }

        private void DrawFooterItem(DrawListViewItemEventArgs e, MessageItem item, Rectangle rect, SolidBrush brush)
        {
            rect.Height = (int)(rect.Height * 1.0f);
            using (GraphicsPath path = Utils.RoundRect(rect, 0, 0, radius, radius))
                e.Graphics.FillPath(brush, path);

            rect.Width -= 16;
            textFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(item.Body.Time.ToShortTimeString(), timeFont, timeBrush, rect, textFormat);
        }

        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            MessageItem item = e.Item as MessageItem;

            SolidBrush brush = item.Body.IsOwnerSender ? senderBackBrush : backBrush;

            Rectangle rect = e.Bounds;
            rect.Inflate(-20, 0);
            int textWidth = item.Body.Measure(e.Graphics, textFont);
            rect.Width = textWidth + 20;

            switch (item.Type)
            {
                case MessageItemType.Header:
                    this.DrawHeaderItem(e, item, rect, brush);
                    break;
                case MessageItemType.Text:
                    this.DrawTextItem(e, item, rect, brush);
                    break;
                case MessageItemType.Footer:
                    this.DrawFooterItem(e, item, rect, brush);
                    break;
            }
            e.Graphics.SmoothingMode = SmoothingMode.Default;
        }

        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            Rectangle rect = e.Bounds;
            rect.Inflate(0, 1);
            e.Graphics.FillRectangle(headerBrush, rect);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            this.header.Width = this.ClientSize.Width - 20;
            base.OnSizeChanged(e);
        }

    }
}
