using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzLog
{
    public class LogConfig : AbsLogConfig, IComparable
    {
        public static string DEFAULT_CONFIG = LogHome.Home("mono.config");
        public static string DEFAULT_LOG = "./mono.log";
        private string sConfigName = "mono";

        private string sFqFilePath;
        string sDateFormat = "yyyy/MM/dd HH:mm:ss [zz]";


        public LogConfig()
        {
            this.sFqFilePath = DEFAULT_LOG;
        }

        public override string ToString()
        {
            TagLines lines = new TagLines();
            lines.Add(new TagLine("NAME", this.ConfigName));
            lines.Add(new TagLine("LOGFILE", this.FilePath));
            lines.Add(new TagLine("DATEFORMAT", this.DateFormat));
            return lines.ToString();
        }

        override
        public string GetConfigName()
        {
            return sConfigName;
        }

        override
        public string GetConfigFile()
        {
            return LogHome.Home(GetConfigName() + ".config");
        }

        override
        public string GetLogFile()
        {
            return sFqFilePath;
        }

        override
        public string GetTime()
        {
            DateTime pw = DateTime.Now;
            return pw.ToString(sDateFormat);
        }


        public static bool Save(LogConfig config)
        {
            try
            {
                StreamWriter ofi = new StreamWriter(config.GetConfigFile(), false);
                ofi.Write(config.ToString());
                ofi.Flush();
                ofi.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public static LogConfig Load()
        {
            LogConfig result = new LogConfig();
            return result;
        }

        public static LogConfig LoadConfig(string configName)
        {
            return LogHome.LoadConfig(configName);
        }

        internal static LogConfig LoadConfigFile(string sFile)
        {
            if (sFile == null || sFile.Length == 0)
            {
                return null;
            }
            TagLines lines = TagLines.FileRead(sFile);
            if (lines == null || lines.IsNull())
            {
                sFile = LogHome.Home(sFile);
                lines = TagLines.FileRead(sFile);
                if (lines == null || lines.IsNull())
                {
                    return null;
                }
            }
            TagLine line = lines.Get("LOGFILE");
            if (line == null || line.IsNull())
            {
                return null; // required
            }
            LogConfig result = Load();
            result.sFqFilePath = line.Value;
            line = lines.Get("DATEFORMAT");
            if (line != null)
            {
                result.sDateFormat = line.Value; // optional
            }
            line = lines.Get("NAME");
            if (line == null || line.IsNull())
            {
                return null; // required
            }
            result.sConfigName = line.Value;
            return result;

        }

        public static string GetConfigNameError(string zname)
        {
            if (zname == null || zname.Length == 0)
            {
                return "unvalue name...";
            }
            foreach (char ch in zname)
            {
                if (Char.IsWhiteSpace(ch))
                {
                    return "unvalue whitespace...";

                }
                if (Char.IsPunctuation(ch))
                {
                    return "unvalue punctuation...";

                }
            }
            return null;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            return this.ToString().CompareTo(obj.ToString());

        }

        public string ConfigName
        {
            get { return sConfigName; }
            set { sConfigName = value; }
        }

        public string FilePath
        {
            get { return sFqFilePath; }
            set { sFqFilePath = value; }
        }

        public string DateFormat
        {
            get { return sDateFormat; }
            set { sDateFormat = value; }
        }
    }
}
