using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complex.Data;

namespace Messenger
{
    public class MessageBase : SQLBase
    {
        public MessageBase()
            :base("data", "Messenger.db")
        {
            this.messages = new SqlTable<Message>(this.Provider, "messages");
            this.configTable = new SqlTable<Config>(this.Provider, "config");
        }

        private SqlTable<Message> messages;
        public SqlTable<Message> Messages
        {
            get { return this.messages; }
        }

        private SqlTable<Config> configTable;
        public SqlTable<Config> ConfigTable => configTable;

    }
}
