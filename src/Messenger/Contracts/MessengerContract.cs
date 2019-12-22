using System;
using System.IO;
using System.Windows.Forms;
using System.Text;
using CryptoLib;

namespace Messenger
{
    public class MessengerContract : Contract
    {
        public MessengerContract(StreamReader reader)
            : base(reader)
        {
        }

        public MessengerContract(string name, MessagerType ownerType) 
            : base(name, ContractType.Messenger, (int)ownerType)
        {
        }

        public MessagerType MessagerType => (MessagerType)base.OwnerType;

        private System.Threading.Timer timer;

        private UDict<Member> members = new UDict<Member>();
        public UDict<Member> Members => members;

        private UDict<Message> messages = new UDict<Message>();
        public UDict<Message> Messages => messages;

        private long maxMessageID = -1;
        public long MaxMessageID
        {
            get => maxMessageID;
            set => maxMessageID = Math.Max(maxMessageID, value);
        }

        
        public TabControl TabControl { get; set; }

        public void LoadMembers()
        {
            this.members.Clear();
        }

        public void Subscribe()
        {
            if (this.timer == null)
                this.timer = new System.Threading.Timer(this.TimerChange);
            this.timer.Change(0, 5000);
        }

        public void Unsubscribe()
        {
            if(this.timer != null)
                this.timer.Change(-1, -1);
        }

        public void AddMember(Member m)
        {
            if (!Constants.LinuxMode)
                return;

            this.members.Add(m);

            ThreadStack.Run(delegate (object[] param)
            {
                MessengerContract contract = param[0] as MessengerContract;
                Member member = param[1] as Member;
                string fileName = Constants.GetDir(contract.Type) + "add_member.fif";
                string bocfile = Constants.BocDirectory + "add_member-query";
                ClientExecutor.Instance.FiftScriptSend(fileName,
                    Utils.UtcNowMilliseconds,
                    contract.PrivKey, 
                    contract.Address, 
                    member.PubKey, 
                    member.Address, 
                    bocfile);
            }, this, m);
        }


        public void RemoveMember(Member m)
        {
            if (!Constants.LinuxMode)
                return;

            ThreadStack.Run(delegate (object[] param)
            {
                MessengerContract contract = param[0] as MessengerContract;
                Member member = param[1] as Member;
                string fileName = Constants.GetDir(contract.Type) + "remove_member.fif";
                string bocfile = Constants.BocDirectory + "remove_member-query";
                ClientExecutor.Instance.FiftScriptSend(fileName,
                    Utils.UtcNowMilliseconds, 
                    contract.PrivKey, 
                    contract.Address, 
                    member.PubKey, 
                    bocfile);
                contract.WaitGramsChangedLoop();
                contract.members.Remove(member);
            }, this, m);
        }

        public void AddToBlackList(Member m)
        {
            if (!Constants.LinuxMode)
                return;

            this.members.Add(m);

            ThreadStack.Run(delegate (object[] param)
            {
                MessengerContract contract = param[0] as MessengerContract;
                Member member = param[1] as Member;
                string fileName = Constants.GetDir(contract.Type) + "add_to_blacklist.fif";
                string bocfile = Constants.BocDirectory + "add_to_blacklist-query";
                ClientExecutor.Instance.FiftScriptSend(fileName,
                    Utils.UtcNowMilliseconds,
                    contract.PrivKey, 
                    contract.Address, 
                    member.PubKey, 
                    bocfile);
                contract.WaitGramsChanged();
            }, this, m);
        }

        public void RemoveFromBlackList(Member m)
        {
            if (!Constants.LinuxMode)
                return;
            this.members.Remove(m);

            ThreadStack.Run(delegate (object[] param)
            {
                MessengerContract contract = param[0] as MessengerContract;
                Member member = param[1] as Member;
                string fileName = Constants.GetDir(contract.Type) + "remove_from_blacklist.fif";
                string bocfile = Constants.BocDirectory + "remove_from_blacklist-query";
                ClientExecutor.Instance.FiftScriptSend(fileName,
                    Utils.UtcNowMilliseconds,
                    contract.PrivKey, 
                    contract.Address, 
                    member.PubKey, 
                    bocfile);
                contract.WaitGramsChanged();
            }, this, m);
        }

        public void DeleteMessages(int id, DeleteMessageMode mode)
        {
            if (!Constants.LinuxMode)
                return;
            ThreadStack.Run(delegate (object[] param)
            {
                MessengerContract contract = param[0] as MessengerContract;
                string fileName = Constants.GetDir(contract.Type) + "remove_message.fif";
                string bocfile = Constants.BocDirectory + "remove_message-query";
                ClientExecutor.Instance.FiftScriptSend(fileName,
                    Utils.UtcNowMilliseconds,
                    contract.PrivKey, 
                    contract.Address, 
                    (long)param[1], 
                    (int)param[2], 
                    bocfile);
                contract.WaitGramsChanged();
            }, this, (long)id, (int)mode);

        }

        public static string GetSliceText(string[] arr, int index, int count, string privKey)
        {
            int fullSize = 0;
            for (int i = index + count - 1; i >= index; i--)
                fullSize += (arr[i].Length - 4) / 2;

            byte[] data = new byte[fullSize];
            int pos = 0;
            for (int i = index + count - 1; i >= index; i--)
            {
                byte[] buff = Utils.HexToByteArray(arr[i]);
                Array.Copy(buff, 2, data, pos, buff.Length - 2);
                pos += buff.Length - 2;
            }
            return Encoding.UTF8.GetString(Decode(data, privKey));
        }


        private void TimerChange(object state)
        {
            if (!Constants.LinuxMode)
                return;

            ThreadStack.Run(delegate (object[] param)
            {
                MessengerContract contract = param[0] as MessengerContract;
                ClientExecutor.Instance.Last();

                System.Numerics.BigInteger index = - 1;
                while (true)
                {
                    string[] res = ClientExecutor.Instance.Runmethod(contract.Address, "getMember", index.ToString(), ParseType.Int, ParseType.Slice);
                    if (res == null)
                    {
                        ClientExecutor.Instance.LastWait(1000);
                        continue;
                    }
                    index = System.Numerics.BigInteger.Parse(res[0]);
                    if (index == -1)
                        break;
                    string pubKey = index.ToString();
                    string address = Utils.ParseAddress(res[1]);

                    Owner owner = ClientExecutor.Instance.GetOwner(address);
                    if (owner != null)
                    {
                        Member member = contract.members[owner.ID];
                        if (member == null)
                        {
                            member = new Member(owner);
                            contract.members.Add(member);
                        }
                        else
                        {
                            member.Update(owner);
                        }
                    }
                }

                long id = contract.MaxMessageID;
                while (true)
                {
                    string[] res = ClientExecutor.Instance.Runmethod(contract.Address, "getMessages", id.ToString(), ParseType.Int, ParseType.Int, ParseType.Int, ParseType.Slice, ParseType.Slice, ParseType.Slice, ParseType.Slice, ParseType.Slice, ParseType.Slice, ParseType.Slice);
                    if (res == null)
                    {
                        ClientExecutor.Instance.LastWait(1000);
                        continue;
                    }
                    id = long.Parse(res[0]);
                    if (id == -1)
                        break;
                    if (id > contract.maxMessageID)
                    {
                        int time = int.Parse(res[1]);
                        string sender = res[2];
                        string text = GetSliceText(res, 3, 7, contract.PrivKey);
                        Message message = new Message(id, time, sender, contract.ID,  text);
                        contract.MaxMessageID = id;
                        contract.messages.Add(message);
                    }
                }


            }, this);
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
