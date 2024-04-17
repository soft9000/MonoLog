using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzLog
{
    /// <summary>
    /// MonoLog's home / installed / assembly location. 
    /// </summary>
    public class LogHome
    {
        private static string ExeName = System.Reflection.Assembly.GetExecutingAssembly().Location;

        /// <summary>
        /// Place a node in the .exe file's location.
        /// </summary>
        /// <param name="sfile"></param>
        /// <returns></returns>
        public static string Home(string sfile)
        {
            int pos = ExeName.LastIndexOf("\\");
            if (pos == -1)
            {
                pos = ExeName.LastIndexOf("/");
            }
            if (pos == -1)
            {
                return ExeName + "/" + sfile;
            }
            string subs = ExeName.Substring(0,pos + 1);
            return subs + sfile;
        }

        /// <summary>
        /// Load a configuration file from MonoLog's default, home, location.
        /// </summary>
        /// <param name="configName">The short name, as defined during creation..</param>
        /// <returns>Null if no configuration file was found.</returns>
        public static LogConfig LoadConfig(string configName)
        {
            configName = configName.ToLower();
            string root = Home("");
            string[] files = Directory.GetFiles(root, "*" + LogConfig.TYPE_CONFIG, SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            {
                string afile = file.ToLower();
                if (afile.EndsWith(configName + LogConfig.TYPE_CONFIG))
                {
                    return LogConfig.LoadConfigFile(file);
                }
            }
            return null;
        }
    }
}
