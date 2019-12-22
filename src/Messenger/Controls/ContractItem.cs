using System;
using System.Windows.Forms;

namespace Messenger
{
    public class ContractItem : OwnerItem
    {
        public ContractItem(Contract contract)
            :base(contract)
        {
        }

        public Contract Contract => base.Owner as Contract;
    }
}
