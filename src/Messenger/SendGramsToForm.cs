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
    public partial class SendGramsToForm : Form
    {
        public SendGramsToForm(Contract sender)
        {
            InitializeComponent();
            this.senderName.Text = sender.Name;
        }

        public string Address => this.addressTextBox.Text;

        public double Grams => (double)this.gramsUpDown.Value;
    }
}
