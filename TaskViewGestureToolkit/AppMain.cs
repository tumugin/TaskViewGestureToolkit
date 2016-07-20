using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskViewGestureToolkit.Config;
using TaskViewGestureToolkit.Gesture;
using TaskViewGestureToolkit.Plugin;

namespace TaskViewGestureToolkit
{
    public class AppMain
    {
        static ConfigClass config;
        [STAThread]
        public static void Main(string[] args)
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            foreach (var path in System.IO.Directory.GetFiles(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\plugin"))
            {
                PluginManager.loadPlugin(path);
            }
            PluginManager.activateAllPlugins();
            UI.TaskTrayComponent ttComponent = new UI.TaskTrayComponent();
            ttComponent.notifyIcon.Icon = UI.StockIcon.getStackIcon();
            ttComponent.exitApplicationMenuItem.Click += exitApplicationMenuItemOnClick;
            ttComponent.reloadPluginMenuItem.Click += reloadPluginMenuItemOnClick;
            ttComponent.useAPIStripMenuItem.Click += useAPIStripMenuItemOnClick;
            //load config
            config = ConfigSerialize.loadConfig();
            Application.ApplicationExit += onApplicationExit;
            Application.Run();
        }

        private static void onApplicationExit(object sender, EventArgs e)
        {
            PluginManager.deactivateAllPlugins();
            PluginManager.clearAllPlugins();
            ConfigSerialize.saveConfig(config);
        }

        private static void useAPIStripMenuItemOnClick(object sender, EventArgs e)
        {
            GestureManager.useVirtualDesktopAPI = ((ToolStripMenuItem)sender).Checked;
        }

        private static void reloadPluginMenuItemOnClick(object sender, EventArgs e)
        {
            PluginManager.deactivateAllPlugins();
            PluginManager.activateAllPlugins();
        }

        private static void exitApplicationMenuItemOnClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
