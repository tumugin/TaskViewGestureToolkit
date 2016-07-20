using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskViewGestureToolkit.Log
{
    public class LogListener : TraceListener
    {
        public StringBuilder logTextBuilder = new StringBuilder();

        public override void Write(string message)
        {
            logTextBuilder.Append(message);
        }

        public override void WriteLine(string message)
        {
            logTextBuilder.AppendLine(message);
        }
    }
}
