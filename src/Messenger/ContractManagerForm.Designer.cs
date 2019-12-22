namespace Messenger
{
    partial class ContractManagerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.createManagerButton = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.contractMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sendGrammsContractMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.changeCodeContractMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameContractMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.copyAddressContractMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeContractMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createContractButton = new System.Windows.Forms.Button();
            this.managerMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sendCreateManagerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.changeCodeManagerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameManagerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.copyAddresManagerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addressTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.managerGroupBox = new System.Windows.Forms.GroupBox();
            this.sendGramsToMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contractListView = new Messenger.ContractListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.managersListView = new Messenger.ContractListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.waitControl = new Messenger.WaitControl();
            this.panel1.SuspendLayout();
            this.contractMenu.SuspendLayout();
            this.managerMenu.SuspendLayout();
            this.managerGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.waitControl);
            this.panel1.Controls.Add(this.createManagerButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(684, 36);
            this.panel1.TabIndex = 0;
            // 
            // createManagerButton
            // 
            this.createManagerButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.createManagerButton.Location = new System.Drawing.Point(8, 7);
            this.createManagerButton.Name = "createManagerButton";
            this.createManagerButton.Size = new System.Drawing.Size(108, 23);
            this.createManagerButton.TabIndex = 0;
            this.createManagerButton.Text = "Create manager";
            this.createManagerButton.UseVisualStyleBackColor = true;
            this.createManagerButton.Click += new System.EventHandler(this.createManagerButton_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(220, 36);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(10, 426);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // contractMenu
            // 
            this.contractMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sendGrammsContractMenuItem,
            this.toolStripSeparator3,
            this.changeCodeContractMenuItem,
            this.renameContractMenuItem,
            this.toolStripSeparator4,
            this.copyAddressContractMenuItem,
            this.removeContractMenuItem});
            this.contractMenu.Name = "contextMenuStrip2";
            this.contractMenu.Size = new System.Drawing.Size(165, 126);
            // 
            // sendGrammsContractMenuItem
            // 
            this.sendGrammsContractMenuItem.Name = "sendGrammsContractMenuItem";
            this.sendGrammsContractMenuItem.Size = new System.Drawing.Size(164, 22);
            this.sendGrammsContractMenuItem.Text = "Send grams";
            this.sendGrammsContractMenuItem.Click += new System.EventHandler(this.sendGrammsContractMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(161, 6);
            // 
            // changeCodeContractMenuItem
            // 
            this.changeCodeContractMenuItem.Name = "changeCodeContractMenuItem";
            this.changeCodeContractMenuItem.Size = new System.Drawing.Size(164, 22);
            this.changeCodeContractMenuItem.Text = "Change code";
            this.changeCodeContractMenuItem.Click += new System.EventHandler(this.changeCodeContractMenuItem_Click);
            // 
            // renameContractMenuItem
            // 
            this.renameContractMenuItem.Name = "renameContractMenuItem";
            this.renameContractMenuItem.Size = new System.Drawing.Size(164, 22);
            this.renameContractMenuItem.Text = "Rename";
            this.renameContractMenuItem.Click += new System.EventHandler(this.renameContractMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(161, 6);
            // 
            // copyAddressContractMenuItem
            // 
            this.copyAddressContractMenuItem.Name = "copyAddressContractMenuItem";
            this.copyAddressContractMenuItem.Size = new System.Drawing.Size(164, 22);
            this.copyAddressContractMenuItem.Text = "Copy address";
            this.copyAddressContractMenuItem.Click += new System.EventHandler(this.copyAddressContractMenuItem_Click);
            // 
            // removeContractMenuItem
            // 
            this.removeContractMenuItem.Name = "removeContractMenuItem";
            this.removeContractMenuItem.Size = new System.Drawing.Size(164, 22);
            this.removeContractMenuItem.Text = "Remove contract";
            this.removeContractMenuItem.Click += new System.EventHandler(this.removeContractMenuItem_Click);
            // 
            // createContractButton
            // 
            this.createContractButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.createContractButton.Enabled = false;
            this.createContractButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.createContractButton.Location = new System.Drawing.Point(332, 38);
            this.createContractButton.Name = "createContractButton";
            this.createContractButton.Size = new System.Drawing.Size(110, 23);
            this.createContractButton.TabIndex = 0;
            this.createContractButton.Text = "Create contract";
            this.createContractButton.UseVisualStyleBackColor = true;
            this.createContractButton.Click += new System.EventHandler(this.createContractButton_Click);
            // 
            // managerMenu
            // 
            this.managerMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sendCreateManagerMenuItem,
            this.toolStripSeparator1,
            this.sendGramsToMenuItem,
            this.changeCodeManagerMenuItem,
            this.renameManagerMenuItem,
            this.toolStripSeparator2,
            this.copyAddresManagerMenuItem});
            this.managerMenu.Name = "contextMenuStrip1";
            this.managerMenu.Size = new System.Drawing.Size(181, 148);
            // 
            // sendCreateManagerMenuItem
            // 
            this.sendCreateManagerMenuItem.Name = "sendCreateManagerMenuItem";
            this.sendCreateManagerMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sendCreateManagerMenuItem.Text = "Send create query";
            this.sendCreateManagerMenuItem.Click += new System.EventHandler(this.sendCreateManagerMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // changeCodeManagerMenuItem
            // 
            this.changeCodeManagerMenuItem.Name = "changeCodeManagerMenuItem";
            this.changeCodeManagerMenuItem.Size = new System.Drawing.Size(180, 22);
            this.changeCodeManagerMenuItem.Text = "Change code";
            this.changeCodeManagerMenuItem.Click += new System.EventHandler(this.changeCodeManagerMenuItem_Click);
            // 
            // renameManagerMenuItem
            // 
            this.renameManagerMenuItem.Name = "renameManagerMenuItem";
            this.renameManagerMenuItem.Size = new System.Drawing.Size(180, 22);
            this.renameManagerMenuItem.Text = "Rename";
            this.renameManagerMenuItem.Click += new System.EventHandler(this.renameManagerMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // copyAddresManagerMenuItem
            // 
            this.copyAddresManagerMenuItem.Name = "copyAddresManagerMenuItem";
            this.copyAddresManagerMenuItem.Size = new System.Drawing.Size(180, 22);
            this.copyAddresManagerMenuItem.Text = "Copy address";
            this.copyAddresManagerMenuItem.Click += new System.EventHandler(this.copyAddresManagerMenuItem_Click);
            // 
            // addressTextBox
            // 
            this.addressTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addressTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.addressTextBox.Location = new System.Drawing.Point(63, 19);
            this.addressTextBox.Name = "addressTextBox";
            this.addressTextBox.ReadOnly = true;
            this.addressTextBox.Size = new System.Drawing.Size(379, 13);
            this.addressTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Address:";
            // 
            // managerGroupBox
            // 
            this.managerGroupBox.Controls.Add(this.addressTextBox);
            this.managerGroupBox.Controls.Add(this.createContractButton);
            this.managerGroupBox.Controls.Add(this.label1);
            this.managerGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.managerGroupBox.Location = new System.Drawing.Point(230, 36);
            this.managerGroupBox.Name = "managerGroupBox";
            this.managerGroupBox.Size = new System.Drawing.Size(454, 72);
            this.managerGroupBox.TabIndex = 5;
            this.managerGroupBox.TabStop = false;
            this.managerGroupBox.Text = "None";
            // 
            // sendGramsToMenuItem
            // 
            this.sendGramsToMenuItem.Name = "sendGramsToMenuItem";
            this.sendGramsToMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sendGramsToMenuItem.Text = "Send grams to";
            this.sendGramsToMenuItem.Click += new System.EventHandler(this.sendGramsToMenuItem_Click);
            // 
            // contractListView
            // 
            this.contractListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.contractListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.contractListView.ContextMenuStrip = this.contractMenu;
            this.contractListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contractListView.HideSelection = false;
            this.contractListView.Location = new System.Drawing.Point(230, 108);
            this.contractListView.MinimumSize = new System.Drawing.Size(413, 320);
            this.contractListView.MultiSelect = false;
            this.contractListView.Name = "contractListView";
            this.contractListView.OwnerDraw = true;
            this.contractListView.RowHeight = 50;
            this.contractListView.SelectedItem = null;
            this.contractListView.Size = new System.Drawing.Size(454, 354);
            this.contractListView.TabIndex = 3;
            this.contractListView.UseCompatibleStateImageBehavior = false;
            this.contractListView.View = System.Windows.Forms.View.Details;
            this.contractListView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.contractListView_MouseUp);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Contact";
            this.columnHeader2.Width = 452;
            // 
            // managersListView
            // 
            this.managersListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.managersListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.managersListView.ContextMenuStrip = this.managerMenu;
            this.managersListView.Dock = System.Windows.Forms.DockStyle.Left;
            this.managersListView.HideSelection = false;
            this.managersListView.Location = new System.Drawing.Point(0, 36);
            this.managersListView.MultiSelect = false;
            this.managersListView.Name = "managersListView";
            this.managersListView.OwnerDraw = true;
            this.managersListView.RowHeight = 50;
            this.managersListView.SelectedItem = null;
            this.managersListView.Size = new System.Drawing.Size(220, 426);
            this.managersListView.TabIndex = 1;
            this.managersListView.UseCompatibleStateImageBehavior = false;
            this.managersListView.View = System.Windows.Forms.View.Details;
            this.managersListView.SelectedIndexChanged += new System.EventHandler(this.managersListView_SelectedIndexChanged);
            this.managersListView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.managersListView_MouseUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Contact";
            this.columnHeader1.Width = 218;
            // 
            // waitControl
            // 
            this.waitControl.Location = new System.Drawing.Point(648, 7);
            this.waitControl.Name = "waitControl";
            this.waitControl.Size = new System.Drawing.Size(24, 23);
            this.waitControl.TabIndex = 1;
            // 
            // ContractManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 462);
            this.Controls.Add(this.contractListView);
            this.Controls.Add(this.managerGroupBox);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.managersListView);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "ContractManagerForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Contract manager";
            this.panel1.ResumeLayout(false);
            this.contractMenu.ResumeLayout(false);
            this.managerMenu.ResumeLayout(false);
            this.managerGroupBox.ResumeLayout(false);
            this.managerGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private ContractListView managersListView;
        private System.Windows.Forms.Splitter splitter1;
        private ContractListView contractListView;
        private System.Windows.Forms.Button createManagerButton;
        private System.Windows.Forms.Button createContractButton;
        private System.Windows.Forms.ContextMenuStrip managerMenu;
        private System.Windows.Forms.ToolStripMenuItem sendCreateManagerMenuItem;
        private System.Windows.Forms.ContextMenuStrip contractMenu;
        private System.Windows.Forms.ToolStripMenuItem sendGrammsContractMenuItem;
        private System.Windows.Forms.TextBox addressTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox managerGroupBox;
        private System.Windows.Forms.ToolStripMenuItem changeCodeContractMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeCodeManagerMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ToolStripMenuItem copyAddressContractMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyAddresManagerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeContractMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameManagerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameContractMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private WaitControl waitControl;
        private System.Windows.Forms.ToolStripMenuItem sendGramsToMenuItem;
    }
}