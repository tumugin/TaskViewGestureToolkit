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
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            System.Drawing.Point point;
            bool flag = false;
            Trace.WriteLine($"SCREEN={screenHeight}x{screenWidth}");
            while (true)
            {
                point = Cursor.Position;
                //Trace.WriteLine($"POS={point.X}x{point.Y} FLAG={flag.ToString()}");
                if (flag == false && point.X >= screenWidth - 5 && point.Y <= 5)
                {
                    flag = true;
                    callOnGesture(0, 0, EventType.OnGestureStart);
                    callOnGesture(0, 10, EventType.OnGestureEnd);
                }else
                {
                    if(!(point.X >= screenWidth - 5 && point.Y <= 5)) flag = false;
                }
                Thread.Sleep(100);
            }
        }
    }
}
