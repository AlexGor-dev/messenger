using System;
using Complex.Data;

namespace Messenger
{
    public class Member : Owner
    {
        public Member(string name, string pubKey, string address, ContractState state, ContractType type, MessagerType messagerType)
            :base(name, pubKey, address, state, type, (int)messagerType)
        {

        }

        public Member(string name, string pubKey, ContractState state, MessagerType messagerType)
            : this(name, pubKey, null, state, ContractType.Messenger, messagerType)
        {

        }

        public Member(Owner owner)
            : base(owner.Name, owner.PubKey, owner.Address, owner.State, owner.Type, owner.OwnerType)
        {

        }

        private SqlTable<Message> messages;
        public SqlTable<Message> Messages
        {
            get => messages;
            set => messages = value;
        }

        public string TableName => "_" + this.PubKey;


        private MessagePage messagePage;
        public MessagePage MessagePage
        {
            get => messagePage;
            set => messagePage = value;
        }

        public MessageListView MessageView
        {
            get
            {
                if (this.messagePage != null)
                    return this.messagePage.MessageView;
                return null;
            }
        }

        public MessagerType MessagerType => (MessagerType)base.OwnerType;
    }
}
