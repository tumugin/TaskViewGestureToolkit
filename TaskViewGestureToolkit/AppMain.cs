using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskViewGestureToolkit.Config;
using TaskViewGestureToolkit.Gesture;
using TaskViewGestureToolkit.Log;
using TaskViewGestureToolkit.Plugin;
using TaskViewGestureToolkit.UI;
using TaskViewGestureToolkit.Updater;

namespace TaskViewGestureToolkit
{
    public class AppMain
    {
        public static ConfigClass config;
        public static UIManager uiman;

        [STAThread]
        public static void Main(string[] args)
        {
            //init UI
            uiman = new UIManager();
            //load config
            config = ConfigSerialize.loadConfig();
            Application.ApplicationExit += onApplicationExit;
            //init logger
            Logger.initLogger();
            //init plugin
            PluginManager.loadFromPluginDir();
            PluginManager.activateAllPlugins();
            //start update checker
            UpdateChecker.checkUpdate();
            //start app
            Application.Run();
        }

        private static void onApplicationExit(object sender, EventArgs e)
        {
            ConfigSerialize.saveConfig(config);
            uiman.ttComponent.notifyIcon.Dispose();
        }
    }
}
