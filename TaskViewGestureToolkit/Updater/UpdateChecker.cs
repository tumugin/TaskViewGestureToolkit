using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskViewGestureToolkit.Updater
{
    public class UpdateChecker
    {
        private const string UPDATE_JSON = "https://raw.githubusercontent.com/kazukioishi/TaskViewGestureToolkit/master/TaskViewGestureToolkit/Updater/updateinfo.json";

        public async static void checkUpdate()
        {
            try
            {
                JObject updjson = null;
                await Task.Run(()=>{
                    updjson = JObject.Parse(new WebClient().DownloadString(UPDATE_JSON));
                });
                var newVer = new Version(updjson["version"].Value<string>());
                if(newVer > Assembly.GetEntryAssembly().GetName().Version)
                {
                    //update available
                    AppMain.uiman.ttComponent.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
                    AppMain.uiman.ttComponent.notifyIcon.BalloonTipTitle = "TaskViewGestureToolkit";
                    AppMain.uiman.ttComponent.notifyIcon.BalloonTipText = updjson["message"].Value<string>();
                    AppMain.uiman.ttComponent.notifyIcon.ShowBalloonTip(5000);
                    var menuitem = new ToolStripMenuItem($"Update to version {newVer.ToString()}");
                    menuitem.Click += (sender, e) =>
                    {
                        Process.Start(updjson["url"].Value<string>());
                    };
                    AppMain.uiman.ttComponent.notifyIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
                    AppMain.uiman.ttComponent.notifyIcon.ContextMenuStrip.Items.Add(menuitem);
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
