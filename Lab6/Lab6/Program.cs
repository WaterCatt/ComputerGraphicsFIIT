using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace Lab6
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Application.Run(new Form1());
        }
    }
}