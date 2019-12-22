using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using CryptoLib;

namespace Messenger
{
    public class ManagerContract : Contract
    {
        public ManagerContract(StreamReader reader)
            :base(reader)
        {

        }

        public ManagerContract(string name, int ownerType)
            :base(name, ContractType.Manager, ownerType)
        {

        }

        private UDict<Contract> contracts = new UDict<Contract>();
        public UDict<Contract> Contracts => contracts;

        public void AddContract(Contract contract)
        {
            byte[] keyData = new BigInteger(this.PrivKey).ToByteArrayUnsigned();
            byte[] valueData = System.Numerics.BigInteger.Parse(contract.PrivKey).ToByteArray();
            byte[] encrypt = Crypto.Transform(keyData, valueData, true);

            string mdir = Constants.ManagerDirectory;
            string fileName = Constants.TempDirectory + contract.Name + "_contract.data";
            File.WriteAllBytes(fileName, encrypt);
            string bocfile = Constants.BocDirectory + contract.Name + "_add_contract-query";
            ClientExecutor.Instance.FiftScriptSend(mdir + "add_contract.fif", 
                Utils.UtcNowMilliseconds, 
                this.PrivKey, 
                this.Address, 
                fileName, 
                contract.PubKey, 
                contract.Address, 
                bocfile);
            this.WaitGramsChangedLoop();
        }

        public bool RemoveContact(Contract contract)
        {
            string bocfile = Constants.BocDirectory + contract.Name + "_remove_contract-query";
            return ClientExecutor.Instance.FiftScriptSend(Constants.ManagerDirectory + "remove_contract.fif",
                Utils.UtcNowMilliseconds, 
                this.PrivKey, 
                this.Address, 
                contract.PubKey, 
                bocfile);
        }

        public void LoadContracts()
        {
            ClientExecutor.Instance.Last();
            this.contracts.Clear();
            System.Numerics.BigInteger index = - 1;
            while (true)
            {
                string[] res = ClientExecutor.Instance.Runmethod(this.Address, "getContract", index.ToString(), ParseType.Int, ParseType.Slice, ParseType.Slice);
                if(res == null)
                {
                    ClientExecutor.Instance.LastWait(1000);
                    continue;
                }
                index = System.Numerics.BigInteger.Parse(res[0]);
                if (index == -1)
                    break;
                string pubKey = index.ToString();
                string address = Utils.ParseAddress(res[1]);
                string data = res[2].Substring(4);

                byte[] keyData = new BigInteger(this.PrivKey).ToByteArrayUnsigned();
                byte[] valueData = Utils.HexToByteArray(data);
                byte[] deccrypt = Crypto.Transform(keyData, valueData, false);
                string privKey = new System.Numerics.BigInteger(deccrypt).ToString();
                Owner owner = ClientExecutor.Instance.GetOwner(address);
                string name = "invalid";
                int type = 0;
                ContractState state = ContractState.Offline;
                if(owner != null)
                {
                    name = owner.Name;
                    type = owner.OwnerType;
                    state = owner.State;
                }
                MessengerContract messenger = new MessengerContract(name, (MessagerType)type);
                messenger.Update(pubKey, privKey, address, state);
                this.contracts.Add(messenger);
            }
            this.GetGrams();

        }
    }
}
