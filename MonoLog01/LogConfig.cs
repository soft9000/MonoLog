using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzLog
{
    /// <summary>
    /// The configuration file enables customizable, user-defined, logging options.
    /// </summary>
    public class LogConfig : AbsLogConfig, IComparable
    {
        public static string TYPE_CONFIG = ".config"; // unique project file type
        public static string TYPE_LOG    = ".log";    // unique project file type
        public static string DEFAULT_CONFIG = MonoHome.Home("mono" + TYPE_CONFIG);
        public static string DEFAULT_LOG = "./mono" + TYPE_LOG;
        private string sConfigName = "mono";

        private string sFqFilePath;
        string sDateFormat = "yyyy/MM/dd HH:mm:ss [zz]";

        /// <summary>
        /// The default log is in the present working directory (`pwd`)
        /// </summary>
        public LogConfig()
        {
            this.sFqFilePath = DEFAULT_LOG;
        }

        /// <summary>
        /// The actual file content.
        /// </summary>
        /// <returns>Representable, parsable, file content.</returns>
        public override string ToString()
        {
            TagLines lines = new TagLines();
            lines.Add(new TagLine("NAME", this.ConfigName));
            lines.Add(new TagLine("LOGFILE", this.FilePath));
            lines.Add(new TagLine("DATEFORMAT", this.DateFormat));
            return lines.ToString();
        }

        /// <summary>
        /// A unique alphanumeric name / token for the confihuration. 
        /// </summary>
        /// <returns>The unique configuration name</returns>
        override
        public string GetConfigName()
        {
            return sConfigName;
        }

        /// <summary>
        /// The fully qualified path and name for the configuration file.
        /// </summary>
        /// <returns>The configuration-file name.</returns>
        override
        public string GetConfigFile()
        {
            return MonoHome.Home(GetConfigName() + TYPE_CONFIG);
        }

        /// <summary>
        /// The fully qualified path and name for the log file.
        /// </summary>
        /// <returns>The log-file name.</returns>
        override
        public string GetLogFile()
        {
            return sFqFilePath;
        }

        /// <summary>
        /// Called every time a date and time string is required.
        /// </summary>
        /// <returns>Present time, formatted as desired.</returns>
        override
        public string GetTime()
        {
            DateTime pw = DateTime.Now;
            return pw.ToString(sDateFormat);
        }

        /// <summary>
        /// Save the configuration to the location specified.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Load the default, in-memory, configuration file.
        /// </summary>
        /// <returns></returns>
        public static LogConfig Load()
        {
            LogConfig result = new LogConfig();
            return result;
        }

        /// <summary>
        /// Load the short-name configuration file from `MonoHome`
        /// </summary>
        /// <param name="configName">User defined configuration name / alphanumeric token.</param>
        /// <returns>Can be null.</returns>
        public static LogConfig LoadConfig(string configName)
        {
            return MonoHome.LoadConfig(configName);
        }

        /// <summary>
        /// Load a configuration file from anywhere.
        /// </summary>
        /// <param name="sFile">Fully qualified path name.</param>
        /// <returns>Can be null</returns>
        internal static LogConfig LoadConfigFile(string sFile)
        {
            if (sFile == null || sFile.Length == 0)
            {
                return null;
            }
            TagLines lines = TagLines.FileRead(sFile);
            if (lines == null || lines.IsNull())
            {
                sFile = MonoHome.Home(sFile);
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

        /// <summary>
        /// Verify a user's configuration name entry.
        /// </summary>
        /// <param name="zname"></param>
        /// <returns></returns>
        public static string GetConfigNameError(string zname)
        {
            if (zname.ToLower().Equals("mono"))
            {
                return "unable to use default name...";
            }
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

        /// <summary>
        /// String compare two objects.
        /// </summary>
        /// <param name="obj">Any</param>
        /// <returns>Classic -1|0|1</returns>
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            return this.ToString().CompareTo(obj.ToString());

        }

        /// <summary>
        /// Set / Get this configuration's user-defined name (UDN.) 
        /// Note that the UDN is / assume to have been validated by 
        /// the LogConfigDlg.
        /// </summary>
        public string ConfigName
        {
            get { return sConfigName; }
            set { sConfigName = value; }
        }

        /// <summary>
        /// Set / Get path to the log file.
        /// </summary>
        public string FilePath
        {
            get { return sFqFilePath; }
            set { sFqFilePath = value; }
        }

        /// <summary>
        /// Set / Get The DateFormat for log entries.
        /// </summary>
        public string DateFormat
        {
            get { return sDateFormat; }
            set { sDateFormat = value; }
        }
    }
}
