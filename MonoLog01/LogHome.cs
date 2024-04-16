using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzLog
{
    public class LogHome
    {
        private static string ExeName = System.Reflection.Assembly.GetExecutingAssembly().Location;

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

        public static LogConfig LoadConfig(string configName)
        {
            configName = configName.ToLower();
            string root = Home("");
            string[] files = Directory.GetFiles(root, "*.config", SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            {
                string afile = file.ToLower();
                if (afile.EndsWith(configName + ".config"))
                {
                    return LogConfig.LoadConfigFile(file);
                }
            }
            return null;
        }
    }
}
