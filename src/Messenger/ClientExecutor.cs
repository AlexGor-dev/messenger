using System;
using System.IO;
using System.Threading;
using System.Text;

namespace Messenger
{
    public class ClientExecutor
    {
        public static readonly ClientExecutor Instance = new ClientExecutor("");

        public ClientExecutor(string directory)
        {
            this.directory = directory;
            this.liteClient = new ProcessExecutor(directory + "lite-client");
            this.func = new ProcessExecutor(directory + "func");
            this.fift = new ProcessExecutor(directory + "./fift");
        }

        private string directory;
        public string Directory => directory;

        private ProcessExecutor liteClient;
        private ProcessExecutor func;
        private ProcessExecutor fift;

        public void Start()
        {
            liteClient.Start("-C " + directory + "ton-client.config", 300);
            liteClient.Wait();
        }

        public string SendClient(string cmd, int timeOut)
        {
            lock (liteClient)
            {
                liteClient.SendCmd(cmd, timeOut);
                return liteClient.Result;
            }
        }

        public string Func(string cmd)
        {
            lock (func)
            {
                func.Start(cmd);
                func.Wait();
                string res = func.Result;
                func.Exit();
                return res;
            }
        }

        public string Fift(string cmd)
        {
            lock (fift)
            {
                fift.Start(cmd);
                fift.Wait();
                string res = fift.Result;
                fift.Exit();
                return res;
            }
        }

        public string SendFile(string fileName)
        {
            return SendClient("sendfile " + fileName, 100);
        }

        public string Last()
        {
            return SendClient("last", 200);
        }

        public string LastWait(int millisec)
        {
            lock(this.liteClient)
            {
                System.Threading.Thread.Sleep(millisec);
                return this.Last();
            }
        }

        public AccountState GetAccountState(string address)
        {
            string res = SendClient("getaccount " + address, 200);
            if (res.IndexOf("state:account_uninit") != -1)
                return AccountState.Empty;
            if(res.IndexOf("state:(account_active") != -1)
                return AccountState.Active;
            return AccountState.None;
        }

        public Gram GetGrams(string address)
        {
            string[] arr = Runmethod(address, "getGtams", null, ParseType.Int);
            if(arr != null && arr.Length > 0)
                return long.Parse(arr[0]);
            return 0;
        }

        public string[] Runmethod(string address, string funcName, string arguments, params ParseType[] types)
        {
            string data = SendClient("runmethod " + address + " " + funcName + " " + arguments, 200);
            return RunmethodParser.Parse(data, types);
        }

        public string FuncCompile(string name)
        {
            return Func("-P -o " + name + "_code.asm " + Constants.LibDirectory + "stdlib.fc " + name + "_code.fc");
        }

        public string[] FiftScript(string scriptName, params object[] param)
        {
            string cmd = "";
            foreach (object p in param)
                cmd += " " + p;
            string res = Fift("-I " + Constants.LibDirectory + " -s " + scriptName + cmd);
            return Utils.ParseFiftResult(res);
        }

        public bool FiftScriptSend(string scriptName, params object[] param)
        {
            string[] res = FiftScript(scriptName, param);
            if (res == null)
                return false;
            string err = ClientExecutor.Instance.SendFile(res[0]);
            return true;
        }

        private string[] GetContractData(string name, string workchainID, ContractType type, int ownerType)
        {
            string bocDir = Constants.BocDirectory;
            string mdir = type + "/";
            string cname = type.ToString().ToLower();
            string comp = FuncCompile(mdir + cname);
            return FiftScript(mdir + "new_" + cname + ".fif", workchainID, "\"" + name + "\"", (int)type, ownerType, bocDir + name.Replace(" ", "_"));
        }

        public void CreateContract(Contract contract, string workchainID)
        {
            string[] arr = GetContractData(contract.Name, workchainID, contract.Type, contract.OwnerType);
            contract.Update(arr[0], arr[1], arr[2], ContractState.Offline);
            switch (contract.Type)
            {
                case ContractType.Manager:
                    contract.Save();
                    break;
            }
        }

        public void InitContract(Contract sender, Contract contract, double grams)
        {
            sender.SendGram(contract.Address, grams);
            sender.WaitGramsChangedLoop();
            AccountState state = AccountState.None;
            while(state != AccountState.Empty)
            {
                LastWait(1000);
                state = GetAccountState(contract.Address);
            }
            contract.SendCreate();
            while (state != AccountState.Active)
            {
                LastWait(1000);
                state = GetAccountState(contract.Address);
            }
            contract.GetGrams();
        }

        public Owner GetOwner(string address)
        {
            try
            {
                string[] arr = Runmethod(address, "getOwner", null, ParseType.Int, ParseType.Int, ParseType.Int, ParseType.Int, ParseType.Slice);
                if (arr != null && arr.Length >= 5)
                {
                    string pubKey = arr[0];
                    ContractState state = (ContractState)int.Parse(arr[1]);
                    int ownerType = int.Parse(arr[2]);
                    ContractType type = (ContractType)int.Parse(arr[3]);

                    string name = RunmethodParser.GetSliceText(arr[4]);
                    return new Owner(name, pubKey, address, state, type, ownerType);
                }
            }
            catch(Exception e)
            {

            }
            return null;
        }

        public Member GetMember(string address)
        {
            Owner owner = GetOwner(address);
            if(owner != null)
                return new Member(owner);
            return null;
        }

        public Contract Load(string address, string privKey)
        {
            Owner owner = GetOwner(address);
            if (owner != null)
            {
                Contract contract = null;
                switch (owner.Type)
                {
                    case ContractType.Manager:
                        contract = new ManagerContract(owner.Name, owner.OwnerType);
                        break;
                    case ContractType.Messenger:
                        contract = new MessengerContract(owner.Name, (MessagerType)owner.OwnerType);
                        break;

                }
                if (contract != null)
                    contract.Update(owner.PubKey, privKey, address, owner.State);
                return contract;
            }
            return null;
        }

        public int GetSeqno(string address)
        {
            Last();
            string[] res = Runmethod(address, "getSeqno", null, ParseType.Int);
            if (res != null && res.Length == 1)
                return int.Parse(res[0]);
            return -1;
        }

        public void SendGram(Contract src, string address, double grams, string bodyBosFile, bool wait)
        {
            string fileName = Constants.CommonDirectory + "send_message.fif";
            string bocfile = Constants.BocDirectory + "send_message-query";
            ClientExecutor.Instance.FiftScriptSend(fileName, 
                Utils.UtcNowMilliseconds, 
                src.PrivKey, 
                src.Address,
                address, 
                grams.ToString().Replace(',', '.'), 
                bocfile, 
                bodyBosFile);
            if(wait)
                src.WaitGramsChanged();
        }

        public void SendGram(Contract src, string address, double grams)
        {
            this.SendGram(src, address, grams, null, true);
        }

        public void ChangeCode(Contract contract)
        {
            string dir = Constants.GetDir(contract.Type);
            string typeFile = dir + contract.Type.ToString().ToLower();
            string err = ClientExecutor.Instance.FuncCompile(typeFile);
            string fileName = Constants.CommonDirectory + "change_code.fif";
            string bocfile = Constants.BocDirectory + "change_code-query";
            ClientExecutor.Instance.FiftScriptSend(fileName, 
                Utils.UtcNowMilliseconds, 
                typeFile, 
                contract.PrivKey, 
                contract.Address, 
                bocfile);
            contract.WaitGramsChanged();
        }

        public void ChangeOwner(Contract contract, bool wait)
        {
            string fileName = Constants.CommonDirectory + "change_owner.fif";
            string bocfile = Constants.BocDirectory + "change_owner-query";
            ClientExecutor.Instance.FiftScriptSend(fileName,
                Utils.UtcNowMilliseconds,
                "\"" + contract.Name + "\"",
                (int)contract.State,
                (int)contract.Type,
                contract.OwnerType,
                contract.PubKey,
                contract.PrivKey,
                contract.Address,
                 bocfile);
            if(wait)
                contract.WaitGramsChanged();
        }

        public bool SendMessage(Contract sender, Owner dest, Message message)
        {
            string bocFile = Constants.BocDirectory + "send_text-query";
            string dataFile = Constants.TempDirectory + "text.data";
            byte[] data = Encoding.UTF8.GetBytes(message.Text);
            data = Contract.Encode(data, dest.PubKey);
            File.WriteAllBytes(dataFile, data);
            string[] arr = FiftScript(Constants.MessengerDirectory + "send_text.fif", message.Time, dataFile, message.Sender, bocFile);
            if (arr != null && arr.Length == 1)
            {
                bocFile = arr[0];
                this.SendGram(sender, dest.Address, 0.1, bocFile, false);
                sender.WaitGramsChangedLoop();
                return true;
            }
            return false;
        }
    }
}
