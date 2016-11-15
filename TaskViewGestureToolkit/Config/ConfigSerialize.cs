using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskViewGestureToolkit.Config
{
    class ConfigSerialize
    {
        public const string CONFIG_FILE = "config.xml";
        public static ConfigClass loadConfig()
        {
            if (!System.IO.File.Exists($"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\{CONFIG_FILE}")) return new ConfigClass();
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(ConfigClass));
            var sr = new System.IO.StreamReader($"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\{CONFIG_FILE}", new System.Text.UTF8Encoding(false));
            ConfigClass cfg = (ConfigClass)serializer.Deserialize(sr);
            sr.Close();
            sr.Dispose();
            return cfg;
        }
        public static void saveConfig(ConfigClass config)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(ConfigClass));
            var sw = new System.IO.StreamWriter($"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\{CONFIG_FILE}", false, new System.Text.UTF8Encoding(false));
            serializer.Serialize(sw, config);
            sw.Close();
        }
    }
}
