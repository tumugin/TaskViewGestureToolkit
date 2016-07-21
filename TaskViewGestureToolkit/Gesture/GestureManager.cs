using PluginBaseLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsDesktop;
using WindowsInput;
using WindowsInput.Native;

namespace TaskViewGestureToolkit.Gesture
{
    public class GestureManager
    {
        public static bool useVirtualDesktopAPI = false;
        public PluginBase plugin;
        private bool isMoving = false;
        private int[] startPoint = { 0, 0 };
        IInputSimulator inputSimulator = new InputSimulator();

        public void onGestureAsync(int x, int y, PluginBase.EventType etype)
        {
            new Thread(() =>
            {
                onGesture(x, y, etype);
            }).Start();
        }

        public void onGesture(int x, int y, PluginBase.EventType etype)
        {
            if (etype == PluginBase.EventType.OnGestureStart && !isMoving)
            {
                //start event
                isMoving = true;
                startPoint = new int[] { x, y };
                Trace.WriteLine($"[{plugin.pluginName} START]");
            }
            else if (etype == PluginBase.EventType.OnGestureStart && isMoving)
            {
                //something is wrong
                isMoving = false;
            }
            else if (etype == PluginBase.EventType.OnGestureEnd && isMoving)
            {
                //end event
                isMoving = false;
                int xDiff = x - startPoint[0];
                int yDiff = y - startPoint[1];
                Trace.WriteLine($"[{plugin.pluginName} END] ({xDiff},{yDiff})");
                if (Math.Abs(xDiff) > Math.Abs(yDiff))
                {
                    if (xDiff < 0)
                    {
                        //left
                        Trace.WriteLine($"[{plugin.pluginName} EVENT] LEFT");
                        if (useVirtualDesktopAPI)
                        {
                            VirtualDesktop.Current?.GetRight()?.Switch();
                        }
                        else
                        {
                            inputSimulator.Keyboard.ModifiedKeyStroke(new VirtualKeyCode[] { VirtualKeyCode.LWIN, VirtualKeyCode.CONTROL }, VirtualKeyCode.RIGHT);
                        }
                    }
                    else if (xDiff > 0)
                    {
                        //right
                        Trace.WriteLine($"[{plugin.pluginName} EVENT] RIGHT");
                        if (useVirtualDesktopAPI)
                        {
                            VirtualDesktop.Current?.GetLeft()?.Switch();
                        }
                        else
                        {
                            inputSimulator.Keyboard.ModifiedKeyStroke(new VirtualKeyCode[] { VirtualKeyCode.LWIN, VirtualKeyCode.CONTROL }, VirtualKeyCode.LEFT);
                        }
                    }
                }
                else
                {
                    if (yDiff < 0)
                    {
                        //down
                        Trace.WriteLine($"[{plugin.pluginName} EVENT] DOWN");
                        inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.TAB);
                    }
                    else if (yDiff > 0)
                    {
                        //up
                        Trace.WriteLine($"[{plugin.pluginName} EVENT] UP");
                        inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.TAB);
                    }
                }
            }
        }
    }
}
