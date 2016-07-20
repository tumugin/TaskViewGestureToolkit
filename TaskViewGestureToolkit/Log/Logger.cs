using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskViewGestureToolkit.Log
{
    public class Logger
    {
        static LogListener listener = new LogListener();
        
        public static void initLogger()
        {
            if(!Trace.Listeners.Contains(listener)) Trace.Listeners.Add(listener);
        }

        public static void writeToLogfile()
        {
            var stream = new System.IO.FileStream($"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\log.txt", System.IO.FileMode.Create);
            byte[] writebyte = Encoding.UTF8.GetBytes(listener.logTextBuilder.ToString());
            stream.Write(writebyte, 0, writebyte.Count());
            stream.Close();
        }
    }
}
