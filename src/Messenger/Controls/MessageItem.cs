using System;
using System.Windows.Forms;

namespace Messenger
{
    public class MessageItem : ListViewItem
    {
        public MessageItem(MessageBody body, MessageItemType type, string text)
        {
            this.body = body;
            this.type = type;
            this.Text = text;
        }

        private MessageBody body;
        public MessageBody Body => body;

        private MessageItemType type;
        public MessageItemType Type => type;

    }

    public enum MessageItemType
    {
        None,
        Header,
        Text,
        Footer,
    }
}
