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
    public partial class CreateManagerForm : Form
    {
        public CreateManagerForm(string name)
        {
            InitializeComponent();
            this.comboBox1.SelectedIndex = 0;
            this.textBox1.Text = name;
        }

        public string ContractName => this.textBox1.Text;
        public string WorkchainID => this.comboBox1.Text;
    }
}
