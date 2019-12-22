using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Messenger
{
    public partial class AddMemberForm : Form
    {
        public AddMemberForm()
        {
            InitializeComponent();
        }

        private Member member;
        public Member Member => member;

        private void findButton_Click(object sender, EventArgs e)
        {
            if (Constants.LinuxMode)
            {
                this.findButton.Enabled = false;
                ThreadStack.Run(delegate (object[] param)
                {
                    this.member = ClientExecutor.Instance.GetMember(param[0] as string);
                    Utils.Invoke(this, this.UpdateMember);
                }, this.textBox1.Text);
            }
        }

        private void UpdateMember()
        {
            if (this.member != null)
            {
                this.nameLabel.Text = this.member.Name;
                this.typeLabel.Text = this.member.MessagerType.ToString();
            }
            else
            {
                this.nameLabel.Text = "error";
                this.typeLabel.Text = "invalid messager-contract";
            }
            this.addButton.Enabled = this.member != null;
            this.findButton.Enabled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.findButton.Enabled = !string.IsNullOrEmpty(this.textBox1.Text);
        }

        private void punKeyLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
