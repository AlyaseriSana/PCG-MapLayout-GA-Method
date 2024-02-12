using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MapProject
{
    static class Program
    {
         private static IntPtr formHandle;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  Application.Run(new Form1());

            IntPtr[] handleArray = new IntPtr[] { formHandle }; // A handle to a synchronization object     
            int timeout = 1000; // The time-out interval in milliseconds
            DoWait(handleArray, timeout);
        }

        [DllImport("ole32.dll")]
        private static extern int CoWaitForMultipleHandles(int dwFlags, int dwTimeout, int cHandles, IntPtr[] pHandles, out int lpdwindex);

        // Call this method to wait for multiple handles and dispatch any pending messages.
        private static void DoWait(IntPtr[] handles, int timeout)
        {
            int index;
            int result = CoWaitForMultipleHandles(0, timeout, handles.Length, handles, out index);

            if (result == 0x80)
            {
                // The call was cancelled. Handle the cancellation here.
            }
            else if (result == 0)
            {
                // The wait timed out. Handle the timeout here.
            }
            else if (result == 0x81)
            {
                // The wait failed. Handle the failure here.
            }
            else
            {
                // The wait succeeded. Dispatch any pending messages here.
                Application.DoEvents();
            }
        }
    }
}
