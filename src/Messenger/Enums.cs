using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger
{
    public enum ContractType
    {
        Simple = 0,
        Messenger = 1,
        Chat = 2,
        Manager = 3,
    }

    public enum ContractState
    {
        Offline = 0,
        Online = 1,
    }

    public enum ParseType
    {
        Int,
        Slice
    }

    public enum AccountState
    {
        None,
        Empty,
        Active,
    }

    public enum MessagerType
    {
        Public = 0,
        Private = 1,
    }

    public enum DeleteMessageMode
    {
        LessOrEqualI = 0,
        EqualID = 1,
        All = 2,
    }
}
