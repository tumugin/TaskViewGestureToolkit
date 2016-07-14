using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TaskViewGestureToolkit.Plugin;

namespace TaskViewGestureToolkit
{
    public class AppMain
    {
        [STAThread]
        public static void Main(string[] args)
        {
            PluginManager.loadPlugin(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\plugin\\SynapticsGesturePlugin.dll");
            PluginManager.activateAllPlugins();
            Application app = new Application();
            app.Run();
        }
    }
}
