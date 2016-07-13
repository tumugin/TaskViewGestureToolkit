using PluginBaseLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SynapticsTester
{
    class Program
    {
        static void Main(string[] args)
        {
            PManager pman = new PManager();
            Task.Run(() =>{
                pman.init();
                pman.plugin.onGesture += Plugin_onGesture;
            });

            Console.WriteLine("Press Ctrl-C to terminate...");
            using (ManualResetEvent manualResetEvent = new ManualResetEvent(false))
            {
                manualResetEvent.WaitOne();
            }
        }

        private static void Plugin_onGesture(int x, int y, PluginBase.EventType etype)
        {
            Console.WriteLine($"[{etype.ToString()}]x={x},y={y}");
        }

        class PManager
        {
            public PluginBase plugin;
            public void init()
            {
                plugin = new SynapticsGesturePlugin.SynapticsGesture();
                plugin.initializePlugin();
            }
        }
    }
}
