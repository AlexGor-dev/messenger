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
    public partial class CreateContractForm : Form
    {
        public CreateContractForm(string name)
        {
            InitializeComponent();

            this.ownerTypeComboBox.Items.Add(MessagerType.Public);
            this.ownerTypeComboBox.Items.Add(MessagerType.Private);
            this.comboBox1.SelectedIndex = 0;
            this.comboBox2.SelectedIndex = 0;
            this.ownerTypeComboBox.SelectedIndex = 0;
            this.textBox1.Text = name;
        }

        public string ContractName => this.textBox1.Text;
        public string WorkchainID => this.comboBox2.Text;
        public ContractType ContractType => (ContractType)Enum.Parse(typeof(ContractType), this.comboBox1.Text);
        public int OwnerType => (int)this.ownerTypeComboBox.SelectedItem;
        public decimal Grams => this.numericUpDown1.Value;
    }
}
