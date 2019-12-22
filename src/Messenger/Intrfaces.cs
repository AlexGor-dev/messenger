using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger
{
    public delegate void MessageHandler(object s, Message message);
    public delegate void EmptyHandler();

    public interface INameSource
    {
        string Name { get; }
    }

    public interface IUnique
    {
        string ID { get; }
    }

}
