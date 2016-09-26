using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskViewGestureToolkit.Tools
{
    public class STAThreadTool
    {
        public static void RunSTAThread<T>(Func<T> func)
        {
            var th = new Thread(() => func());
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        public static void RunSTAThread(Action ac)
        {
            RunSTAThread<bool>(() =>
            {
                ac();
                return true;
            });
        }
    }

}
