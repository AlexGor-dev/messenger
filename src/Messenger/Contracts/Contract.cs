using System;
using System.IO;
using System.Threading;
using CryptoLib;

namespace Messenger
{
    public abstract class Contract : Owner
    {
        public Contract(StreamReader reader)
            : base(reader)
        {
            this.privKey = reader.ReadLine().Trim();
        }


        public override void Save(StreamWriter writer)
        {
            base.Save(writer);
            writer.WriteLine(this.privKey);
        }

        public void Save()
        {
            using (StreamWriter writer = new StreamWriter(Constants.ContractsDirectory + this.Name + ".ct"))
                this.Save(writer);
        }

        public Contract(string name, ContractType type, int ownerType)
            : base(name, type, ownerType)
        {
        }

        private Timer waitTimer;
        private int waitCount;
        private int waitRunCount = 0;

        private string privKey;
        public string PrivKey => privKey;

        private int isCreated = -1;
        public bool IsCreated
        {
            get
            {
                if (isCreated == -1)
                {
                    isCreated = 0;
                    GetGrams(this);
                }
                return isCreated == 1;
            }
        }

        private Gram grams;
        public Gram Grams => grams;

        public void Update(string pubKey, string privKey, string address, ContractState state)
        {
            this.privKey = privKey;
            this.Update(pubKey, address, state);
        }

        public void SendCreate()
        {
            ClientExecutor.Instance.SendFile(Constants.BocDirectory + this.Name.Replace(" ", "_") + "-query.boc");
        }

        public void SendGram(string address, double grams)
        {
            ClientExecutor.Instance.SendGram(this, address, grams);
        }

        private static void GetGrams(Contract contract)
        {
            if (Constants.LinuxMode)
            {
                ThreadStack.Run(delegate (object[] param)
                {
                    (param[0] as Contract).GetGrams();
                }, contract);
            }
        }

        public Gram GetGrams()
        {
            if (Constants.LinuxMode)
            {
                Gram g = ClientExecutor.Instance.GetGrams(this.Address);
                if (this.grams != g)
                {
                    this.grams = g;
                    this.isCreated = 1;
                    this.OnChanged();
                }
                return g;
            }
            return 0;
        }

        public void WaitGramsChangedLoop()
        {
            int num = 0;
            Gram mgrams = this.Grams;
            while (true)
            {
                num++;
                ClientExecutor.Instance.LastWait(2000);
                Gram g = GetGrams();
                if (g != mgrams || num > 20)
                {
                    this.grams = g;
                    this.OnChanged();
                    break;
                }
            }
        }

        private void WaitGramsChanged(params object[] param)
        {
            Interlocked.Increment(ref this.waitCount);
            Gram mgrams = this.Grams;
            ClientExecutor.Instance.Last();
            Gram g = GetGrams();
            if (g != mgrams || this.waitCount > 20)
            {
                lock (this.waitTimer)
                {
                    if (this.waitRunCount > 0)
                    {
                        Interlocked.Decrement(ref this.waitRunCount);
                        if (this.waitRunCount == 0)
                            this.waitTimer.Change(-1, -1);
                        else
                            this.waitTimer.Change(2000, -1);
                    }
                }
                this.OnChanged();
            }
            else
            {
                lock (this.waitTimer)
                    this.waitTimer.Change(2000, -1);
            }
        }

        private void WaitTimerChanged(object state)
        {
            ThreadStack.Run(this.WaitGramsChanged);
        }

        public void WaitGramsChanged()
        {
            if (this.waitTimer == null)
                this.waitTimer = new Timer(this.WaitTimerChanged);
            lock (this.waitTimer)
            {
                this.waitCount = 0;
                if (this.waitRunCount == 0)
                    this.waitTimer.Change(2000, -1);
                Interlocked.Increment(ref this.waitRunCount);
            }
        }

        public void ChangeCode()
        {
            if (Constants.LinuxMode)
            {
                ThreadStack.Run(delegate (object[] param)
                {
                    ClientExecutor.Instance.ChangeCode(param[0] as Contract);
                }, this);
            }
        }

        public void ChangeOwner(bool wait)
        {
            if (Constants.LinuxMode)
            {
                ThreadStack.Run(delegate (object[] param)
                {
                    ClientExecutor.Instance.ChangeOwner(param[0] as Contract, (bool)param[1]);

                }, this, wait);
            }
        }

        public void SendMessage(Owner dest, Message message)
        {
            if (Constants.LinuxMode)
            {
                ThreadStack.Run(delegate (object[] param)
                {
                    ClientExecutor.Instance.SendMessage(param[0] as Contract, param[1] as Owner, param[2] as Message);
                }, this, dest, message);
            }
        }

        public static Contract Create(string name, ContractType type, int ownerType)
        {
            switch (type)
            {
                case ContractType.Manager:
                    return new ManagerContract(name, ownerType);
                case ContractType.Messenger:
                    return new MessengerContract(name, (MessagerType)ownerType);
            }
            return null;
        }

        public static byte[] Encode(byte[] data, string pubKey)
        {
            return ManagedCrypto.Encrypt(data, new BigInteger(pubKey).ToByteArrayUnsigned());
        }

        public static byte[] Decode(byte[] data, string privKey)
        {
            return ManagedCrypto.Decrypt(data, new BigInteger(privKey).ToByteArrayUnsigned());
        }
    }
}
