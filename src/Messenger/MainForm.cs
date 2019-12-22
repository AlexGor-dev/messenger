using System;
using System.Windows.Forms;
using System.Text;
using Complex.Data;

namespace Messenger
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.messageBase = new MessageBase();
            this.config = this.messageBase.ConfigTable.Select()[0];

            this.contractManager = new ContractManagerForm();
            this.contractManager.ManagersLoaded += ContractManager_ManagersLoaded;

            this.stateComboBox.Items.Add(ContractState.Offline);
            this.stateComboBox.Items.Add(ContractState.Online);

            this.waitPanel.Start();
        }

        private void ContractManager_ManagersLoaded(object sender, EventArgs e)
        {
            this.contractsComboBox.Items.Clear();
            foreach (ManagerContract manager in this.contractManager.Managers)
            {
                foreach (Contract contract in manager.Contracts)
                {
                    this.contractsComboBox.Items.Add(contract);
                }
            }
            this.CurrentUser = this.contractManager.FindContract(this.config.CurrentUser);
            this.waitPanel.Stop();
            this.contractsComboBox.Enabled = true;
            this.stateComboBox.Enabled = true;
            this.managerButton.Enabled = true;
        }

        protected override void OnShown(EventArgs e)
        {
            this.contractManager.LoadManagers(this);
            base.OnShown(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            if (Constants.LinuxMode && this.currentUser != null)
            {
                this.currentUser.State = ContractState.Offline;
                ClientExecutor.Instance.ChangeOwner(this.currentUser, false);
            }
            this.CurrentUser = null;
            base.OnClosed(e);
        }

        private ContractManagerForm contractManager;

        private MessageBase messageBase;
        private Config config;

        private Member CurrentMember
        {
            get
            {
                if (this.currentUser != null)
                {
                    TabControl tab = this.currentUser.TabControl;
                    if (tab != null)
                    {
                        MessagePage page = tab.SelectedTab as MessagePage;
                        if (page != null)
                            return page.Member;
                    }
                }
                return null;
            }
        }

        private MessengerContract currentUser;
        public MessengerContract CurrentUser
        {
            get
            {
                if (this.currentUser == null)
                {
                    this.CurrentUser = this.contractManager.FindContract(this.config.CurrentUser);
                }
                return this.currentUser;
            }
            set
            {
                if (this.currentUser == value) return;
                this.waitPanel.Start();
                if (this.currentUser != null)
                {
                    this.currentUser.Changed -= CurrentUser_Changed;
                    this.currentUser.Unsubscribe();
                    this.currentUser.Messages.Added -= Messages_Added;
                    this.currentUser.Members.Added -= Members_Added;
                    this.currentUser.Members.Removed -= Members_Removed;
                    this.currentUser.State = ContractState.Offline;
                    this.currentUser.ChangeOwner(false);
                }
                this.currentUser = value;
                this.memberListView.Items.Clear();
                if (this.currentUser != null)
                {
                    this.config.CurrentUser = this.currentUser.PubKey;
                    this.messageBase.ConfigTable.Update(this.config);
                    foreach (Member member in this.currentUser.Members)
                        this.AddMember(member);
                    this.currentUser.Changed += CurrentUser_Changed;
                    this.contractsComboBox.SelectedItem = this.currentUser;
                    this.currentUser.Messages.Added += Messages_Added;
                    this.currentUser.Members.Added += Members_Added;
                    this.currentUser.Members.Removed += Members_Removed;
                    this.currentUser.Subscribe();

                    this.currentUser.State = ContractState.Online;
                    this.currentUser.ChangeOwner(false);
                    this.stateComboBox.SelectedItem = this.currentUser.State;

                    if (this.currentUser.TabControl == null)
                    {
                        TabControl tab = new TabControl();
                        tab.Dock = DockStyle.Fill;
                        tab.SelectedIndexChanged += Tab_SelectedIndexChanged;
                        this.messagePanel.Controls.Add(tab);
                        this.currentUser.TabControl = tab;
                    }
                    this.currentUser.TabControl.BringToFront();
                    Tab_SelectedIndexChanged(this.currentUser.TabControl, null);
                }
                this.addMemberButton.Enabled = this.currentUser != null;

                this.waitPanel.Stop();
            }

        }


        private void Tab_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tab = sender as TabControl;
            MessagePage page = tab.SelectedTab as MessagePage;
            if(page != null)
            {
                this.recipLabel.Text = page.Text;
                MemberItem item = this.FindMemberItem(page.Member);
                if(item != null)
                {
                    this.memberListView.SelectedItem = item;
                }
            }
            else
            {
                this.recipLabel.Text = "";
            }
        }

        private void CurrentUser_Changed(object sender, EventArgs e)
        {
            Utils.Invoke(this, delegate
            {
                this.gramsLabel.Text = this.currentUser.Grams.ToString();
                this.stateComboBox.SelectedItem = this.currentUser.State;
            });
        }

        private void Messages_Added(object sender, Message message)
        {
            Utils.Invoke(this, delegate
            {
                Member member = this.currentUser.Members[message.Sender];
                if (member != null)
                {
                    member.Messages.Insert(message);
                    if (member.MessagePage != null)
                    {
                        this.AddMessage(member.MessageView, member, message);
                        member.MessageView.EnsureVisible(member.MessageView.Items.Count - 1);
                    }
                }
            });
        }

        private void AddMember(Member member)
        {
            if (this.messageBase.Exist(member.TableName))
                member.Messages = new SqlTable<Message>(this.messageBase.Provider, member.TableName);
            else
                member.Messages = this.messageBase.Messages.CloneStructure(member.TableName);
            this.memberListView.Items.Add(new MemberItem(member));
            foreach (Message message in member.Messages.Select())
                if(IsMainMessage(this.currentUser, member, message))
                    this.currentUser.MaxMessageID = message.ID;
        }

        private void Members_Added(object sender, Member e)
        {
            Utils.Invoke(this, delegate
            {
                this.AddMember(e);
            });
        }


        private void Members_Removed(object sender, Member e)
        {
            Utils.Invoke(this, delegate
            {
                MemberItem item = this.FindMemberItem(e);
                if (item != null)
                    this.memberListView.Items.Remove(item);
            });
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void managerButton_Click(object sender, EventArgs e)
        {
            this.contractManager.ShowDialog();
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void memberListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MemberItem item = this.memberListView.SelectedItem;
            if (item != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    Member member = item.Member;
                    if (member.MessagePage == null)
                    {
                        member.MessagePage = new MessagePage(member);
                        this.currentUser.TabControl.TabPages.Add(member.MessagePage);
                        Message[] msgs = member.Messages.Select();
                        foreach (Message message in msgs)
                            this.AddMessage(member.MessageView, member, message);
                        if (member.MessageView.Items.Count > 0)
                            member.MessageView.EnsureVisible(member.MessageView.Items.Count - 1);
                    }
                    if (!this.currentUser.TabControl.TabPages.Contains(member.MessagePage))
                        this.currentUser.TabControl.TabPages.Add(member.MessagePage);
                    this.currentUser.TabControl.SelectedTab = member.MessagePage;
                    this.sendTextBox.Enabled = true;
                    this.recipLabel.Text = item.Member.Name;
                }
            }
            bool enabled = item != null;

            this.sendGrammsMenuItem.Enabled = enabled;
            this.copyAddressMenuItem.Enabled = enabled;
            this.removeMemberMenuItem.Enabled = enabled;
            this.deleteAllMessagesMenuItem.Enabled = !enabled;
            this.addToBlackListMenuItem.Enabled = enabled;
            this.removeFromBlackListMenuItem.Enabled = enabled;
        }

        private void memberListView_DoubleClick(object sender, EventArgs e)
        {

        }

        private void addMemberButton_Click(object sender, EventArgs e)
        {
            using (AddMemberForm form = new AddMemberForm())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    Member member = form.Member;
                    if (!this.currentUser.Members.Contains(member.ID))
                    {
                        this.currentUser.AddMember(member);
                    }
                }
            }
        }

        private static bool IsMainMessage(Owner owner1, Owner owner2, Message message)
        {
            if(owner1 != null && owner2 != null)
            {
                if(message.Sender == owner1.ID || message.Sender == owner2.ID)
                {
                    return message.Destination == owner1.ID || message.Destination == owner2.ID;
                }
            }
            return false;
        }

        private void AddMessage(MessageListView listView, Member member, Message message)
        {
            Owner sender = this.currentUser;
            if (sender.ID != message.Sender)
                sender = this.currentUser.Members[message.Sender];
            if (IsMainMessage(this.currentUser, member, message))
            {
                MessageBody body = new MessageBody(message, sender, this.currentUser);
                foreach (MessageItem mitem in body.Items)
                    listView.Items.Add(mitem);
            }
        }

        private void SendText()
        {
            Member member = this.CurrentMember;
            if (member != null)
            {
                string text = this.sendTextBox.Text.Trim();

                Message message = new Message(-1, Utils.UtcNow, this.currentUser.ID, member.ID, text);
                member.Messages.Insert(message);

                this.AddMessage(member.MessageView, member, message);

                member.MessageView.EnsureVisible(member.MessageView.Items.Count - 1);


                if (Constants.LinuxMode)
                {
                    this.currentUser.SendMessage(member, message);
                }
                this.sendTextBox.Text = "";
                this.sendTextBox.Select();
            }
        }

        private void SendTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Enter:
                    this.SendText();
                    break;
            }
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            this.SendText();
        }

        private void sendTextBox_TextChanged(object sender, EventArgs e)
        {
            string text = this.sendTextBox.Text.Trim();
            this.sendButton.Enabled = !string.IsNullOrEmpty(text);
            if (this.sendTextBox.Enabled)
            {
                if (text.Length > 256)
                    this.sendTextBox.Text = text.Substring(0, 256);
                else
                    this.charsLabel.Text = text.Length.ToString();
            }
        }

        private void contractsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CurrentUser = this.contractsComboBox.SelectedItem as MessengerContract;
        }

        private MemberItem FindMemberItem(Member member)
        {
            foreach (MemberItem item in this.memberListView.Items)
                if (item.Member == member)
                    return item;
            return null;
        }

        private void stateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ContractState state = (ContractState)this.stateComboBox.SelectedItem;
            if (this.currentUser.State != state)
            {
                this.currentUser.State = state;
                this.currentUser.ChangeOwner(true);
            }
        }

        private void sendGrammsMenuItem_Click(object sender, EventArgs e)
        {
            ContractManagerForm.SendGrams(this, this.contractManager.FindContractItem(this.currentUser), this.memberListView.SelectedItem);
        }

        private void copyAddressMenuItem_Click(object sender, EventArgs e)
        {
            MemberItem item = this.memberListView.SelectedItem;
            if (item != null && !string.IsNullOrEmpty(item.Member.Address))
                Clipboard.SetText(item.Member.Address);

        }

        private void removeMemberMenuItem_Click(object sender, EventArgs e)
        {
            MemberItem item = this.memberListView.SelectedItem;
            if (item != null)
            {
                this.currentUser.RemoveMember(item.Member);
                this.memberListView.Items.Remove(item);
            }

        }

        private void deleteAllMessagesMenuItem_Click(object sender, EventArgs e)
        {
            this.currentUser.DeleteMessages(0, DeleteMessageMode.All);
        }

        private void addToBlackListMenuItem_Click(object sender, EventArgs e)
        {
            MemberItem item = this.memberListView.SelectedItem;
            if (item != null)
                this.currentUser.AddToBlackList(item.Member);

        }

        private void removeFromBlackListMenuItem_Click(object sender, EventArgs e)
        {
            MemberItem item = this.memberListView.SelectedItem;
            if (item != null)
                this.currentUser.RemoveFromBlackList(item.Member);

        }
    }
}
