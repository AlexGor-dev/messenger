using System;
namespace Messenger
{
    public static class Constants
    {
        public static bool LinuxMode = true;

        public const string LibDirectory = "lib/";
        public const string ContractsDirectory = "Contracts/";
        public const string BocDirectory = "Boc/";
        public const string CommonDirectory = "Common/";
        public const string TempDirectory = "Temp/";

        public const string SimpleDirectory = "Simple/";
        public const string ManagerDirectory = "Manager/";
        public const string ChatDirectory = "Chat/";
        public const string MessengerDirectory = "Messenger/";

        public static string GetDir(ContractType type)
        {
            switch (type)
            {
                case ContractType.Simple:
                    return SimpleDirectory;
                case ContractType.Messenger:
                    return MessengerDirectory;
                case ContractType.Chat:
                    return ChatDirectory;
                case ContractType.Manager:
                    return ManagerDirectory;
            }
            return null;
        }
    }
}
