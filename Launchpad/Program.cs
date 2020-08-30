using System;
using System.Windows.Forms;
using Launchpad.Forms;

namespace Launchpad
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Initial());
        }
    }
}