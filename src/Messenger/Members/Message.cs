using System;
using Complex.Serialization;

namespace Messenger
{
    public class Message : Serializable, IUnique
    {
        protected Message(IData data)
        {

        }

        protected override void LoadClassData(IData data)
        {
            this.id = (long)data["id"];
            this.time = (int)data["time"];
            this.sender = data["sender"] as string;
            this.destination = data["destination"] as string;
            this.text = data["text"] as string;
        }

        protected override void SaveClassData(IData data)
        {
            data["id"] = this.id;
            data["time"] = this.time;
            data["sender"] = this.sender;
            data["destination"] = this.destination;
            data["text"] = this.text;
        }

        public Message(long id, int time, string sender, string destination, string text)
        {
            this.id = id;
            this.time = time;
            this.sender = sender;
            this.destination = destination;
            this.text = text;
        }

        private long id;
        public long ID => id;

        string IUnique.ID => id.ToString();

        private int time;
        public int Time => time;

        private string sender;
        public string Sender => sender;

        private string destination;
        public string Destination => destination;

        private string text;
        public string Text => text;

        public override string ToString()
        {
            return this.time + " " + this.sender + " {" + this.text + "}";
        }
    }
}
