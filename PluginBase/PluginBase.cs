using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginBase
{
    public abstract class PluginBase
    {
        public enum EventType
        {
            OnGestureStart,
            OnGestureEnd
        }
        public delegate void gestureEventHandler(int x, int y, EventType etype);
        public event gestureEventHandler onGesture;
        protected virtual void callOnGesture(int x, int y, EventType etype)
        {
            onGesture(x, y, etype);
        }
        public abstract String pluginName { get; }

        public abstract void initializePlugin();
        public abstract void deletePlugin();
    }
}
