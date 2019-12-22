using System;
using System.Collections.Generic;
using System.Drawing;

namespace Messenger
{
    public class MessageBody
    {
        public MessageBody(Message message, Owner sender, Owner owner)
        {
            this.message = message;
            this.sender = sender;
            this.owner = owner;

            this.items.Add(new MessageItem(this, MessageItemType.Header, this.sender.Name));

            

            string msg = message.Text;
            string[] arr = msg.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (string text in arr)
            {
                string[] lines = this.SplitLine(text);
                foreach(string res in lines)
                    this.items.Add(new MessageItem(this, MessageItemType.Text, res));
            }
            //int count = (int)Math.Ceiling((float)msg.Length / maxLineLen);

            //int index = 0;
            //for (int i = 0; i < count; i++)
            //{
            //    int last = index + maxLineLen;
            //    if(last < msg.Length - 1)
            //        last = msg.LastIndexOf(' ', last - 1);
            //    if (last >= index)
            //    {
            //        string text = msg.Substring(index, Math.Min(last - index, msg.Length - index));
            //        this.items.Add(new MessageItem(this, MessageItemType.Text, text));
            //    }
            //    index = last + 1;
            //}

            this.items.Add(new MessageItem(this, MessageItemType.Footer, null));
            this.items.Add(new MessageItem(this, MessageItemType.None, null));
        }

        private Message message;
        public Message Message => message;

        public DateTime Time => Utils.GetLocalTime(this.message.Time);

        private Owner sender;
        public Owner Sender => sender;

        private Owner owner;
        public Owner Owner => owner;

        public bool IsOwnerSender => owner == sender;

        private List<MessageItem> items = new List<MessageItem>();
        public List<MessageItem> Items => items;

        private int maxLineLen = 50;

        private int minWidth = 150;

        private int meassuredWidth = 0;

        public int Measure(Graphics g, Font font)
        {
            if (this.meassuredWidth == 0)
            {
                this.meassuredWidth = this.minWidth;
                foreach (MessageItem item in this.items)
                    this.meassuredWidth = (int)Math.Max(this.meassuredWidth, g.MeasureString(item.Text, font).Width + (item.Type == MessageItemType.Header ? 50 : 0));
            }
            return this.meassuredWidth;
        }

        private string[] SplitLine(string text)
        {
            int count = (int)Math.Ceiling((float)text.Length / maxLineLen);
            string[] lines = new string[count];
            int index = 0;
            for (int i = 0; i < count; i++)
            {
                int last = index + maxLineLen;
                if (last < text.Length - 1)
                {
                    last = text.LastIndexOf(' ', last - 1);
                    if(last == -1)
                        last = index + maxLineLen;
                }
                if (last >= index)
                    lines[i] = text.Substring(index, Math.Min(last - index, text.Length - index));
                index = last + 1;
            }
            return lines;
        }
    }
}
