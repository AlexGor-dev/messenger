using System;
using System.Windows.Forms;

namespace Messenger
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.Run(new MainForm());
        }
    }
}
