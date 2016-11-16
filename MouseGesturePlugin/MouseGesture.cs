using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace MouseGesturePlugin
{
    public class MouseGesture : PluginBaseLib.PluginBase
    {
        private Thread thread;
        public override string pluginName
        {
            get
            {
                return "MouseGesturePlugin";
            }
        }

        public override void deletePlugin()
        {
            thread?.Abort();
        }

        public override void initializePlugin()
        {
            thread = new Thread(mouseMoveChecker);
            thread.Start();
        }

        private void mouseMoveChecker()
        {
            bool flag = false;
            while (true)
            {
                System.Drawing.Point point = Cursor.Position;
                foreach (var screen in Screen.AllScreens)
                {
                    if(screen.Bounds.X <= point.X && screen.Bounds.Y <= point.Y && (screen.Bounds.X + screen.Bounds.Width) >= point.X && (screen.Bounds.Y + screen.Bounds.Height) >= point.Y)
                    {
                        //cursor is in this screen
                        System.Drawing.Point screenCursorPos = new System.Drawing.Point(point.X - screen.Bounds.X,point.Y - screen.Bounds.Y);
                        if(!flag && screenCursorPos.X >= screen.Bounds.Width - 5 && screenCursorPos.Y <= 5)
                        {
                            flag = true;
                            callOnGesture(0, 0, EventType.OnGestureStart);
                            callOnGesture(0, 10, EventType.OnGestureEnd);
                        }else if (!(screenCursorPos.X >= screen.Bounds.Width - 5 && screenCursorPos.Y <= 5))
                        {
                            flag = false;
                        }
                    }
                }
                Thread.Sleep(300);
            }
        }
    }
}
