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

namespace TaskViewGestureToolkit.UI
{
    public class UIManager
    {
        public UI.TaskTrayComponent ttComponent;

        public UIManager()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            ttComponent = new UI.TaskTrayComponent();
            ttComponent.notifyIcon.Icon = UI.StockIcon.getStackIcon();
            ttComponent.exitApplicationMenuItem.Click += exitApplicationMenuItemOnClick;
            ttComponent.reloadPluginMenuItem.Click += reloadPluginMenuItemOnClick;
            ttComponent.useAPIStripMenuItem.Click += useAPIStripMenuItemOnClick;
            ttComponent.writeLogMenuItem.Click += writeLogMenuItemOnClick;
        }

        private void writeLogMenuItemOnClick(object sender, EventArgs e)
        {
            Logger.writeToLogfile();
        }

        private void useAPIStripMenuItemOnClick(object sender, EventArgs e)
        {
            GestureManager.useVirtualDesktopAPI = ((ToolStripMenuItem)sender).Checked;
        }

        private void reloadPluginMenuItemOnClick(object sender, EventArgs e)
        {
            PluginManager.deactivateAllPlugins();
            PluginManager.clearAllPlugins();
            PluginManager.loadFromPluginDir();
            PluginManager.activateAllPlugins();
        }

        private void exitApplicationMenuItemOnClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
