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
    public partial class RenameForm : Form
    {
        public RenameForm(string name)
        {
            InitializeComponent();
            this.textBox1.Text = name;
        }


        public string NewName => this.textBox1.Text;
    }
}
