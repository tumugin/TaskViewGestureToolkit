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
            PluginManager.loadFromPluginDir();
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
            ConfigSerialize.saveConfig(config);
        }

        private static void useAPIStripMenuItemOnClick(object sender, EventArgs e)
        {
            GestureManager.useVirtualDesktopAPI = ((ToolStripMenuItem)sender).Checked;
        }

        private static void reloadPluginMenuItemOnClick(object sender, EventArgs e)
        {
            PluginManager.deactivateAllPlugins();
            PluginManager.clearAllPlugins();
            PluginManager.loadFromPluginDir();
            PluginManager.activateAllPlugins();
        }

        private static void exitApplicationMenuItemOnClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
