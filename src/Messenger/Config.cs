using System;
using Complex.Serialization;

namespace Messenger
{
    public class Config : Serializable
    {
        protected Config(IData data)
        {

        }

        protected override void LoadClassData(IData data)
        {
            this.id = (int)data["id"];
            this.currentUser = data["currentUser"] as string;
        }

        protected override void SaveClassData(IData data)
        {
            data["currentUser"] = this.currentUser;
            data["id"] = this.id;
        }

        public Config()
        {

        }

        private int id;

        private string currentUser;
        public string CurrentUser
        {
            get => currentUser;
            set => currentUser = value;
        }

        public override string ToString()
        {
            return currentUser;
        }
    }
}
