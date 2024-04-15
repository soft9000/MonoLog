using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzLog
{
    public class LogConfig : AbsLogConfig
    {
        string sFqFilePath = "./mono.log";

        override
        public string getFilePath()
        {
            return sFqFilePath;
        }

        override
        public string getDefaultTime()
        {
            DateTime pw = DateTime.Now;
            return pw.ToUniversalTime().ToString();
        }

        private LogConfig() { 
        }

        private LogConfig(string sFile)
        {
            sFqFilePath = sFile;
        }

        public static LogConfig Load()
        {
            LogConfig result = new LogConfig();
            return result;
        }

        /*
         * TODO - Load from an INI file, maybe ...
        public LogConfig Load(string sFile)
        {
            if (sFile == null || sFile.Length == 0)
            {
                return Load();
            }
            return Load(sFile);
        }
         */
    }
}
