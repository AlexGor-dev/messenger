using System;
using System.IO;

namespace Messenger
{
    public class Owner : INameSource, IUnique
    {
        public Owner(StreamReader reader)
        {
            this.name = reader.ReadLine().Trim();
            this.pubKey = reader.ReadLine().Trim();
            this.address = reader.ReadLine().Trim();
            this.type = (ContractType)Enum.Parse(typeof(ContractType), reader.ReadLine());
            this.ownerType = int.Parse(reader.ReadLine());
        }


        public virtual void Save(StreamWriter writer)
        {
            writer.WriteLine(this.name);
            writer.WriteLine(this.pubKey);
            writer.WriteLine(this.address);
            writer.WriteLine(this.type.ToString());
            writer.WriteLine(this.ownerType.ToString());
        }

        public Owner(string name, string pubKey, string address, ContractState state, ContractType type, int ownerType)
        {
            this.name = name;
            this.pubKey = pubKey;
            this.address = address;
            this.state = state;
            this.type = type;
            this.ownerType = ownerType;
        }

        public Owner(string name, ContractType type, int ownerType)
            : this(name, null, null, ContractState.Offline, type, ownerType)
        {

        }

        public event EventHandler Changed;


        private string name;
        public string Name
        {
            get => this.name;
            set
            {
                if (this.name == value) return;
                this.name = value;
                this.OnChanged();
            }
        }

        private string pubKey;
        public string PubKey => pubKey;

        public string ID => pubKey;

        private string address;
        public string Address => address;

        private ContractState state;
        public ContractState State
        {
            get => this.state;
            set
            {
                if (this.state == value) return;
                this.state = value;
                this.OnChanged();
            }
        }

        private ContractType type;
        public ContractType Type => type;

        private int ownerType;
        public int OwnerType => ownerType;

        public bool IsLoaded => this.address != null;

        protected void Update(string pubKey, string address, ContractState state)
        {
            if (this.pubKey != pubKey || this.address != address || this.state != state)
            {
                this.pubKey = pubKey;
                this.address = address;
                this.state = state;
                this.OnChanged();
            }
        }

        public void Update(Owner owner)
        {
            if (owner != null)
            {
                if (this.name != owner.name || this.state != owner.state || this.ownerType != owner.ownerType || this.pubKey != owner.pubKey)
                {
                    this.name = owner.name;
                    this.state = owner.state;
                    this.ownerType = owner.ownerType;
                    this.pubKey = owner.pubKey;
                    this.OnChanged();
                }
            }
        }

        protected virtual void OnChanged()
        {
            if (this.Changed != null) this.Changed(this, EventArgs.Empty);
        }



        public override string ToString()
        {
            return this.type + " (" + name + " " + state + ")";
        }
    }
}
