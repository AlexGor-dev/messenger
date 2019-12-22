using System;
using System.Windows.Forms;

namespace Messenger
{
    public class MessagePage : TabPage
    {
        public MessagePage(Member member)
        {
            this.BorderStyle = BorderStyle.None;

            this.member = member;
            this.Text = this.member.Name;
            this.messageView = new MessageListView();
            this.messageView.Dock = DockStyle.Fill;
            this.Controls.Add(this.messageView);

            ContextMenu menu = new ContextMenu();
            MenuItem item = new MenuItem("Close");
            item.Click += delegate (object s, EventArgs e)
            {
                this.TabControl.TabPages.Remove(this);
            };
            menu.MenuItems.Add(item);

            this.ContextMenu = menu;
        }

        private Member member;
        public Member Member => member;

        private MessageListView messageView;
        public MessageListView MessageView => messageView;

        private TabControl TabControl => this.Parent as TabControl;

        
    }
}
