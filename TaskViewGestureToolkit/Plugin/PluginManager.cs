using PluginBaseLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaskViewGestureToolkit.Gesture;

namespace TaskViewGestureToolkit.Plugin
{
    public static class PluginManager
    {
        public static List<GestureManager> pluginList = new List<GestureManager>();

        public static void loadPlugin(String path)
        {
            var asm = Assembly.LoadFrom(path);
            PluginBase inst = null;
            foreach (var t in asm.GetTypes())
            {
                if (t.IsInterface) continue;
                if (t.IsAbstract) continue;
                if (t.BaseType.Name != "PluginBase") continue;
                inst = Activator.CreateInstance(t) as PluginBase;
                if (inst != null) break;
            }
            if(inst != null)
            {
                GestureManager gman = new GestureManager();
                gman.plugin = inst;
                pluginList.Add(gman);
                Debug.WriteLine($"[PLUGIN]{inst.pluginName} loaded.");
            }
        }

        public static void activateAllPlugins()
        {
            foreach(var p in pluginList)
            {
                p.plugin.onGesture -= p.onGesture;
                p.plugin.onGesture += p.onGesture;
                p.plugin.initializePlugin();
            }
        }

        public static void deactivateAllPlugins()
        {
            foreach (var p in pluginList)
            {
                p.plugin.onGesture -= p.onGesture;
                p.plugin.deletePlugin();
            }
        }

        public static void clearAllPlugins()
        {
            pluginList.Clear();
        }
    }
}
