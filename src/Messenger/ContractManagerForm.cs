using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace Messenger
{
    public partial class ContractManagerForm : Form
    {
        public ContractManagerForm()
        {
            InitializeComponent();

            this.waitControl.Start();
        }

        public event EventHandler ManagersLoaded;

        private List<ManagerContract> managers = new List<ManagerContract>();
        public List<ManagerContract> Managers => managers;

        private ManagerContract SelectedManager => this.managersListView.SelectedContract as ManagerContract;

        public ContractItem FindContractItem(Contract contract)
        {
            if (contract != null)
            {
                foreach (ContractItem item in this.contractListView.Items)
                    if (item.Contract == contract)
                        return item;
            }
            return null;
        }

        public MessengerContract FindContract(string pubKey)
        {
            foreach (ManagerContract manager in this.managers)
            {
                foreach (Contract contract in manager.Contracts)
                    if (contract.PubKey == pubKey)
                        return contract as MessengerContract;
            }
            return null;
        }

        public void LoadManagers(Form form)
        {
            if (this.managers.Count == 0)
            {
                ThreadStack.Run(delegate (object[] param)
                {
                    if (Constants.LinuxMode)
                        ClientExecutor.Instance.Start();
                    foreach (string file in Directory.GetFiles(Constants.ContractsDirectory, "*.ct"))
                    {
                        using (StreamReader reader = new StreamReader(file))
                        {
                            ManagerContract manager = new ManagerContract(reader);
                            this.managers.Add(manager);
                            if (Constants.LinuxMode)
                            {
                                Owner owner = ClientExecutor.Instance.GetOwner(manager.Address);
                                if (owner != null)
                                {
                                    manager.Update(owner);
                                    manager.LoadContracts();
                                }
                            }
                            else
                            {
                                MessengerContract contract = new MessengerContract("Александр Гордиенко", MessagerType.Private);
                                contract.Update("0", null, null, ContractState.Online);
                                contract.Members.Add(new Member("Вася", "1", ContractState.Online, MessagerType.Private));
                                contract.Members.Add(new Member("Петя", "2", ContractState.Offline, MessagerType.Private));
                                manager.Contracts.Add(contract);

                                contract = new MessengerContract("Вася", MessagerType.Public);
                                contract.Update("1", null, null, ContractState.Online);
                                contract.Members.Add(new Member("Андрей", "3", ContractState.Online, MessagerType.Public));
                                contract.Members.Add(new Member("Сергей", "4", ContractState.Offline, MessagerType.Private));
                                manager.Contracts.Add(contract);

                                contract = new MessengerContract("Андрей", MessagerType.Public);
                                contract.Update("3", null, null, ContractState.Online);
                                contract.Members.Add(new Member("Алексей", "5", ContractState.Offline, MessagerType.Public));
                                contract.Members.Add(new Member("Михаил", "6", ContractState.Online, MessagerType.Public));
                                manager.Contracts.Add(contract);
                            }
                        }
                    }

                    Utils.Invoke(form, this.StartLoaded);
                });
            }
        }

        private void StartLoaded()
        {
            foreach (ManagerContract manager in this.managers)
            {
                this.Add(manager);
                foreach (Contract contract in manager.Contracts)
                    contract.GetGrams();
            }
            this.waitControl.Stop();
            this.OnManagersLoaded();
        }

        private void OnManagersLoaded()
        {
            if (this.ManagersLoaded != null) this.ManagersLoaded(this, EventArgs.Empty);
        }

        private void createManagerButton_Click(object sender, EventArgs e)
        {
            using (CreateManagerForm form = new CreateManagerForm(Utils.GetNext("Manager", managers)))
            {
                if(form.ShowDialog(this) == DialogResult.OK)
                {
                    ManagerContract manager = new ManagerContract(form.ContractName, 0);
                    this.Add(manager);
                    if (Constants.LinuxMode)
                    {
                        ThreadStack.Run(delegate (object[] param)
                        {
                            ClientExecutor.Instance.CreateContract(param[0] as Contract, param[1] as string);
                        }, manager, form.WorkchainID);
                    }
                }
            }
        }

        private void createContractButton_Click(object sender, EventArgs e)
        {
            ContractItem manageritem = this.managersListView.SelectedItem;
            if (manageritem != null)
            {
                ManagerContract manager = manageritem.Contract as ManagerContract;

                using (CreateContractForm form = new CreateContractForm(Utils.GetNext("Messenger", this.contractListView.Items)))
                {
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        Contract contract = Contract.Create(form.ContractName, form.ContractType, form.OwnerType);
                        ContractItem item = new ContractItem(contract);
                        this.contractListView.Items.Add(item);
                        this.contractListView.SelectedItem = item;
                        item.StartWait();
                        manageritem.StartWait();
                        if (Constants.LinuxMode)
                        {
                            ThreadStack.Run(delegate (object[] param)
                            {
                                ManagerContract mgr = param[0] as ManagerContract;
                                Contract ct = param[1] as Contract;
                                ClientExecutor.Instance.CreateContract(ct, param[2] as string);
                                if(!item.IsWaitState)
                                    item.StartWait();
                                mgr.Contracts.Add(contract);
                                ClientExecutor.Instance.InitContract(mgr, ct, (double)param[3]);
                                manageritem.StartWait();
                                mgr.AddContract(ct);
                                Utils.Invoke(this, this.OnManagersLoaded);
                            }, manager, contract, form.WorkchainID, (double)form.Grams);
                        }
                    }
                }
            }
        }

        private void removeContractMenuItem_Click(object sender, EventArgs e)
        {
            ContractItem manageritem = this.managersListView.SelectedItem;
            if (manageritem != null)
            {
                ManagerContract manager = manageritem.Contract as ManagerContract;
                ContractItem item = this.contractListView.SelectedItem;
                if (item != null)
                {
                    if (Constants.LinuxMode)
                    {
                        item.StartWait();
                        manageritem.StartWait();
                        ThreadStack.Run(delegate (object[] param)
                        {
                            ManagerContract mc = param[0] as ManagerContract;
                            ContractItem ctitem = param[1] as ContractItem;
                            if (mc.RemoveContact(ctitem.Contract))
                            {
                                mc.Contracts.Remove(ctitem.Contract);
                                Utils.Invoke(this, delegate
                                {
                                    this.contractListView.Items.Remove(ctitem);
                                });
                                mc.WaitGramsChangedLoop();
                            }

                        }, manager, item);
                    }
                }
            }
        }

        private void Add(ManagerContract manager)
        {
            ContractItem item = new ContractItem(manager);
            this.managersListView.Items.Add(item);
        }

        private void sendCreateManagerMenuItem_Click(object sender, EventArgs e)
        {
            if (Constants.LinuxMode)
            {
                ContractItem manageritem = this.managersListView.SelectedItem;
                if (manageritem != null)
                {
                    manageritem.StartWait();
                    manageritem.Contract.SendCreate();
                    manageritem.Contract.WaitGramsChanged();
                }
            }
        }

        private void ChangeCode(ContractItem item)
        {
            if (Constants.LinuxMode)
            {
                if (item != null)
                {
                    item.StartWait();
                    item.Contract.ChangeCode();
                }
            }
        }

        private void changeCodeManagerMenuItem_Click(object sender, EventArgs e)
        {
            this.ChangeCode(this.managersListView.SelectedItem);
        }


        public static void SendGrams(Form parent, ContractItem senderItem, OwnerItem destItem)
        {
            if (senderItem != null && destItem !=null)
            {
                if (destItem != null)
                {
                    using (SendGramForm form = new SendGramForm(senderItem.Contract, destItem.Owner))
                    {
                        if (form.ShowDialog(parent) == DialogResult.OK)
                        {
                            if (Constants.LinuxMode && form.Grams > 0)
                            {
                                senderItem.StartWait();
                                destItem.StartWait();
                                ThreadStack.Run(delegate (object[] param)
                                {
                                    Contract src = param[0] as Contract;
                                    Owner dst = param[1] as Owner;
                                    src.SendGram(dst.Address, (double)(decimal)param[2]);
                                    if(dst is Contract)
                                        (dst as Contract).WaitGramsChanged();
                                }, senderItem.Contract, destItem.Owner, form.Grams);
                            }
                        }
                    }
                }
            }

        }

        private void sendGrammsContractMenuItem_Click(object sender, EventArgs e)
        {
            SendGrams(this, this.managersListView.SelectedItem, this.contractListView.SelectedItem);
        }

        private void changeCodeContractMenuItem_Click(object sender, EventArgs e)
        {
            this.ChangeCode(this.contractListView.SelectedItem);
        }

        private void copyAddresManagerMenuItem_Click(object sender, EventArgs e)
        {
            ManagerContract manager = this.SelectedManager;
            if (manager != null)
                Clipboard.SetText(manager.Address);
        }

        private void copyAddressContractMenuItem_Click(object sender, EventArgs e)
        {
            Contract contract = this.contractListView.SelectedContract;
            if (contract != null)
                Clipboard.SetText(contract.Address);
        }


        private void Rename(ContractItem contractItem)
        {
            if (contractItem != null)
            {
                Contract contract = contractItem.Contract;
                using (RenameForm form = new RenameForm(contract.Name))
                {
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        if (contract.Name != form.NewName)
                        {
                            contract.Name = form.NewName;
                            if (Constants.LinuxMode)
                            {
                                contractItem.StartWait();
                                contract.ChangeOwner(true);
                                if (contract is ManagerContract)
                                    contract.Save();
                            }
                        }
                    }
                }
            }
        }
        private void renameManagerMenuItem_Click(object sender, EventArgs e)
        {
            this.Rename(this.managersListView.SelectedItem);
        }

        private void renameContractMenuItem_Click(object sender, EventArgs e)
        {
            this.Rename(this.contractListView.SelectedItem);
        }

        private void UpdateSelectedItem()
        {
            ContractItem item = this.managersListView.SelectedItem as ContractItem;
            bool enabled = item != null && item.Selected && item.Contract.Type == ContractType.Manager;

            bool created = item != null && item.Contract.IsCreated;

            this.createContractButton.Enabled = enabled;
            this.sendCreateManagerMenuItem.Enabled = enabled;
            this.changeCodeManagerMenuItem.Enabled = enabled && created;
            this.copyAddresManagerMenuItem.Enabled = enabled && item != null && item.Contract.IsLoaded;
            this.renameManagerMenuItem.Enabled = enabled;
            this.sendGramsToMenuItem.Enabled = enabled && created && false;

            if (enabled)
            {
                this.managerGroupBox.Text = item.Contract.Name;
                this.addressTextBox.Text = item.Contract.Address;
            }
            else
            {
                this.managerGroupBox.Text = "";
                this.addressTextBox.Text = "";
            }

            this.contractListView.Items.Clear();
            if (enabled)
            {
                foreach (Contract contract in SelectedManager.Contracts)
                    this.contractListView.Items.Add(new ContractItem(contract));
            }
        }

        private void managersListView_MouseUp(object sender, MouseEventArgs e)
        {
            this.UpdateSelectedItem();
        }

        private void contractListView_MouseUp(object sender, MouseEventArgs e)
        {
            ContractItem item = this.contractListView.FocusedItem as ContractItem;
            bool enabled = item != null && item.Selected;
            bool created = item != null && item.Contract.IsCreated;

            this.sendGrammsContractMenuItem.Enabled = enabled && created;
            this.changeCodeContractMenuItem.Enabled = enabled && created;
            this.copyAddressContractMenuItem.Enabled = enabled && item != null && item.Contract.IsLoaded;
            this.removeContractMenuItem.Enabled = enabled && created;
            this.renameContractMenuItem.Enabled = enabled && created;
        }

        private void managersListView_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void sendGramsToMenuItem_Click(object sender, EventArgs e)
        {
            ContractItem manageritem = this.managersListView.SelectedItem;
            if (manageritem != null)
            {
                using (SendGramsToForm form = new SendGramsToForm(manageritem.Contract))
                {
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        string address = form.Address;
                        if (!string.IsNullOrEmpty(address) && form.Grams > 0)
                        {
                            manageritem.StartWait();
                            manageritem.Contract.SendGram(address, form.Grams);
                            manageritem.Contract.WaitGramsChanged();
                        }
                    }
                }
            }
        }
    }
}
