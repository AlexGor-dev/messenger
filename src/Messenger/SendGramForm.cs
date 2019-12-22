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
    public partial class SendGramForm : Form
    {
        public SendGramForm(Contract src, Owner dst)
        {
            InitializeComponent();
            this.srcLabel.Text = src.Name;
            this.dstLabel.Text = dst.Name;
        }

        public decimal Grams => this.numericUpDown1.Value;
    }
}
