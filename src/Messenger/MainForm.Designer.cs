namespace Messenger
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.managerButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gramsLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.stateComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.addMemberButton = new System.Windows.Forms.Button();
            this.contractsComboBox = new System.Windows.Forms.ComboBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.messagePanel = new System.Windows.Forms.Panel();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panel5 = new System.Windows.Forms.Panel();
            this.sendTextBox = new System.Windows.Forms.TextBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.charsLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.recipLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sendButton = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sendGrammsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.copyAddressMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeMemberMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAllMessagesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.addToBlackListMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeFromBlackListMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.label6 = new System.Windows.Forms.Label();
            this.memberListView = new Messenger.MemberListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.waitPanel = new Messenger.WaitControl();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.messagePanel.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(103, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Current user:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // managerButton
            // 
            this.managerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.managerButton.Enabled = false;
            this.managerButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.managerButton.Location = new System.Drawing.Point(572, 9);
            this.managerButton.Name = "managerButton";
            this.managerButton.Size = new System.Drawing.Size(116, 23);
            this.managerButton.TabIndex = 6;
            this.managerButton.Text = "Contracts manager";
            this.managerButton.UseVisualStyleBackColor = true;
            this.managerButton.Click += new System.EventHandler(this.managerButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.waitPanel);
            this.panel1.Controls.Add(this.gramsLabel);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.stateComboBox);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.addMemberButton);
            this.panel1.Controls.Add(this.contractsComboBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.managerButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(740, 41);
            this.panel1.TabIndex = 7;
            // 
            // gramsLabel
            // 
            this.gramsLabel.AutoSize = true;
            this.gramsLabel.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.gramsLabel.Location = new System.Drawing.Point(474, 14);
            this.gramsLabel.Name = "gramsLabel";
            this.gramsLabel.Size = new System.Drawing.Size(40, 13);
            this.gramsLabel.TabIndex = 12;
            this.gramsLabel.Text = "-----------";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(436, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Grams:";
            // 
            // stateComboBox
            // 
            this.stateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stateComboBox.Enabled = false;
            this.stateComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.stateComboBox.FormattingEnabled = true;
            this.stateComboBox.Location = new System.Drawing.Point(363, 11);
            this.stateComboBox.Name = "stateComboBox";
            this.stateComboBox.Size = new System.Drawing.Size(63, 21);
            this.stateComboBox.TabIndex = 10;
            this.stateComboBox.SelectedIndexChanged += new System.EventHandler(this.stateComboBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(328, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "State:";
            // 
            // addMemberButton
            // 
            this.addMemberButton.Enabled = false;
            this.addMemberButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.addMemberButton.Location = new System.Drawing.Point(12, 9);
            this.addMemberButton.Name = "addMemberButton";
            this.addMemberButton.Size = new System.Drawing.Size(85, 23);
            this.addMemberButton.TabIndex = 8;
            this.addMemberButton.Text = "Add member";
            this.addMemberButton.UseVisualStyleBackColor = true;
            this.addMemberButton.Click += new System.EventHandler(this.addMemberButton_Click);
            // 
            // contractsComboBox
            // 
            this.contractsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.contractsComboBox.Enabled = false;
            this.contractsComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.contractsComboBox.FormattingEnabled = true;
            this.contractsComboBox.Location = new System.Drawing.Point(171, 9);
            this.contractsComboBox.Name = "contractsComboBox";
            this.contractsComboBox.Size = new System.Drawing.Size(146, 21);
            this.contractsComboBox.TabIndex = 7;
            this.contractsComboBox.SelectedIndexChanged += new System.EventHandler(this.contractsComboBox_SelectedIndexChanged);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(200, 41);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(10, 448);
            this.splitter1.TabIndex = 9;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.messagePanel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(210, 41);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(530, 448);
            this.panel2.TabIndex = 10;
            // 
            // messagePanel
            // 
            this.messagePanel.Controls.Add(this.splitter2);
            this.messagePanel.Controls.Add(this.panel5);
            this.messagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messagePanel.Location = new System.Drawing.Point(0, 0);
            this.messagePanel.Name = "messagePanel";
            this.messagePanel.Size = new System.Drawing.Size(530, 448);
            this.messagePanel.TabIndex = 3;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter2.Location = new System.Drawing.Point(0, 319);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(530, 10);
            this.splitter2.TabIndex = 4;
            this.splitter2.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.sendTextBox);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 329);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(530, 119);
            this.panel5.TabIndex = 3;
            // 
            // sendTextBox
            // 
            this.sendTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sendTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sendTextBox.Enabled = false;
            this.sendTextBox.Location = new System.Drawing.Point(0, 0);
            this.sendTextBox.Multiline = true;
            this.sendTextBox.Name = "sendTextBox";
            this.sendTextBox.Size = new System.Drawing.Size(530, 88);
            this.sendTextBox.TabIndex = 0;
            this.sendTextBox.TextChanged += new System.EventHandler(this.sendTextBox_TextChanged);
            this.sendTextBox.KeyDown += SendTextBox_KeyDown;
            // 
            // panel6

            // 
            this.panel6.Controls.Add(this.label6);
            this.panel6.Controls.Add(this.charsLabel);
            this.panel6.Controls.Add(this.label3);
            this.panel6.Controls.Add(this.recipLabel);
            this.panel6.Controls.Add(this.label2);
            this.panel6.Controls.Add(this.sendButton);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 88);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(530, 31);
            this.panel6.TabIndex = 0;
            // 
            // charsLabel
            // 
            this.charsLabel.AutoSize = true;
            this.charsLabel.Location = new System.Drawing.Point(131, 10);
            this.charsLabel.Name = "charsLabel";
            this.charsLabel.Size = new System.Drawing.Size(49, 13);
            this.charsLabel.TabIndex = 5;
            this.charsLabel.Text = "--------------";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(88, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Chars:";
            // 
            // recipLabel
            // 
            this.recipLabel.AutoSize = true;
            this.recipLabel.Location = new System.Drawing.Point(304, 9);
            this.recipLabel.Name = "recipLabel";
            this.recipLabel.Size = new System.Drawing.Size(49, 13);
            this.recipLabel.TabIndex = 3;
            this.recipLabel.Text = "--------------";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(202, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Message recipient:";
            // 
            // sendButton
            // 
            this.sendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sendButton.Enabled = false;
            this.sendButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.sendButton.Location = new System.Drawing.Point(443, 5);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(75, 23);
            this.sendButton.TabIndex = 1;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sendGrammsMenuItem,
            this.toolStripSeparator1,
            this.copyAddressMenuItem,
            this.removeMemberMenuItem,
            this.toolStripSeparator2,
            this.addToBlackListMenuItem,
            this.removeFromBlackListMenuItem,
            this.toolStripSeparator3,
            this.deleteAllMessagesMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(267, 154);
            // 
            // sendGrammsMenuItem
            // 
            this.sendGrammsMenuItem.Name = "sendGrammsMenuItem";
            this.sendGrammsMenuItem.Size = new System.Drawing.Size(266, 22);
            this.sendGrammsMenuItem.Text = "Send grams";
            this.sendGrammsMenuItem.Click += new System.EventHandler(this.sendGrammsMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(263, 6);
            // 
            // copyAddressMenuItem
            // 
            this.copyAddressMenuItem.Name = "copyAddressMenuItem";
            this.copyAddressMenuItem.Size = new System.Drawing.Size(266, 22);
            this.copyAddressMenuItem.Text = "Copy address";
            this.copyAddressMenuItem.Click += new System.EventHandler(this.copyAddressMenuItem_Click);
            // 
            // removeMemberMenuItem
            // 
            this.removeMemberMenuItem.Name = "removeMemberMenuItem";
            this.removeMemberMenuItem.Size = new System.Drawing.Size(266, 22);
            this.removeMemberMenuItem.Text = "Remove meber";
            this.removeMemberMenuItem.Click += new System.EventHandler(this.removeMemberMenuItem_Click);
            // 
            // deleteAllMessagesMenuItem
            // 
            this.deleteAllMessagesMenuItem.Name = "deleteAllMessagesMenuItem";
            this.deleteAllMessagesMenuItem.Size = new System.Drawing.Size(266, 22);
            this.deleteAllMessagesMenuItem.Text = "Delete all messages from blockchain";
            this.deleteAllMessagesMenuItem.Click += new System.EventHandler(this.deleteAllMessagesMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(263, 6);
            // 
            // addToBlackListMenuItem
            // 
            this.addToBlackListMenuItem.Name = "addToBlackListMenuItem";
            this.addToBlackListMenuItem.Size = new System.Drawing.Size(266, 22);
            this.addToBlackListMenuItem.Text = "Add to black list";
            this.addToBlackListMenuItem.Click += new System.EventHandler(this.addToBlackListMenuItem_Click);
            // 
            // removeFromBlackListMenuItem
            // 
            this.removeFromBlackListMenuItem.Name = "removeFromBlackListMenuItem";
            this.removeFromBlackListMenuItem.Size = new System.Drawing.Size(266, 22);
            this.removeFromBlackListMenuItem.Text = "Remove from black list";
            this.removeFromBlackListMenuItem.Click += new System.EventHandler(this.removeFromBlackListMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(263, 6);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Max chars: 256";
            // 
            // memberListView
            // 
            this.memberListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.memberListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.memberListView.ContextMenuStrip = this.contextMenuStrip1;
            this.memberListView.Dock = System.Windows.Forms.DockStyle.Left;
            this.memberListView.HideSelection = false;
            this.memberListView.Location = new System.Drawing.Point(0, 41);
            this.memberListView.MultiSelect = false;
            this.memberListView.Name = "memberListView";
            this.memberListView.OwnerDraw = true;
            this.memberListView.RowHeight = 50;
            this.memberListView.SelectedItem = null;
            this.memberListView.Size = new System.Drawing.Size(200, 448);
            this.memberListView.TabIndex = 8;
            this.memberListView.UseCompatibleStateImageBehavior = false;
            this.memberListView.View = System.Windows.Forms.View.Details;
            this.memberListView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.memberListView_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 198;
            // 
            // waitPanel
            // 
            this.waitPanel.Location = new System.Drawing.Point(702, 9);
            this.waitPanel.Name = "waitPanel";
            this.waitPanel.Size = new System.Drawing.Size(29, 23);
            this.waitPanel.TabIndex = 13;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 489);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.memberListView);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Messenger";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.messagePanel.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }



        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button managerButton;
        private System.Windows.Forms.Panel panel1;
        private MemberListView memberListView;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.TextBox sendTextBox;
        private System.Windows.Forms.Panel messagePanel;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ComboBox contractsComboBox;
        private System.Windows.Forms.Button addMemberButton;
        private System.Windows.Forms.Label recipLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label charsLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox stateComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label gramsLabel;
        private System.Windows.Forms.Label label5;
        private WaitControl waitPanel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem sendGrammsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem copyAddressMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeMemberMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteAllMessagesMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem addToBlackListMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeFromBlackListMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Label label6;
    }
}

