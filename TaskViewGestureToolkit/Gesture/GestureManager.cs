using PluginBaseLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskViewGestureToolkit.Gesture
{
    public class GestureManager
    {
        public PluginBase plugin;
        private bool isMoving = false;
        private int[] startPoint = {0,0};

        public void onGesture(int x, int y, PluginBase.EventType etype)
        {
            if(etype == PluginBase.EventType.OnGestureStart && !isMoving)
            {
                //start event
                isMoving = true;
                startPoint = new int[] { x, y };
                Debug.WriteLine($"[{plugin.pluginName} START]");
            }
            else if(etype == PluginBase.EventType.OnGestureStart && isMoving)
            {
                //something is wrong
                isMoving = false;
            }else if(etype == PluginBase.EventType.OnGestureEnd && isMoving){
                //end event
                isMoving = false;
                int xDiff = x - startPoint[0];
                int yDiff = y - startPoint[1];
                Debug.WriteLine($"[{plugin.pluginName} END] ({xDiff},{yDiff})");
            }
        }
    }
}
