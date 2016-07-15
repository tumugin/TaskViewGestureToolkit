using PluginBaseLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

namespace TaskViewGestureToolkit.Gesture
{
    public class GestureManager
    {
        public PluginBase plugin;
        private bool isMoving = false;
        private int[] startPoint = { 0, 0 };
        IInputSimulator inputSimulator = new InputSimulator();

        public void onGesture(int x, int y, PluginBase.EventType etype)
        {
            if (etype == PluginBase.EventType.OnGestureStart && !isMoving)
            {
                //start event
                isMoving = true;
                startPoint = new int[] { x, y };
                Debug.WriteLine($"[{plugin.pluginName} START]");
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
                Debug.WriteLine($"[{plugin.pluginName} END] ({xDiff},{yDiff})");
                if (Math.Abs(xDiff) > Math.Abs(yDiff))
                {
                    if (xDiff < 0)
                    {
                        //left
                        Debug.WriteLine($"[{plugin.pluginName} EVENT] LEFT");
                        inputSimulator.Keyboard.ModifiedKeyStroke(new VirtualKeyCode[] { VirtualKeyCode.LWIN, VirtualKeyCode.CONTROL }, new VirtualKeyCode[] { VirtualKeyCode.RIGHT });
                    }
                    else if (xDiff > 0)
                    {
                        //right
                        Debug.WriteLine($"[{plugin.pluginName} EVENT] RIGHT");
                        inputSimulator.Keyboard.ModifiedKeyStroke(new VirtualKeyCode[] { VirtualKeyCode.LWIN, VirtualKeyCode.CONTROL }, new VirtualKeyCode[] { VirtualKeyCode.LEFT });
                    }
                }
                else
                {
                    if (yDiff < 0)
                    {
                        //down
                        Debug.WriteLine($"[{plugin.pluginName} EVENT] DOWN");
                        inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.TAB);
                    }
                    else if (yDiff > 0)
                    {
                        //up
                        Debug.WriteLine($"[{plugin.pluginName} EVENT] UP");
                        inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.TAB);
                    }
                }
            }
        }
    }
}
