using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using EzLog;
using System.IO;

namespace EzLogTesting
{

    public class ConfigManagementTest
    {
        public static void MainTest()
        {
            // STEP: Create Configurations
            string[] configs = {
                        "~a", "~b", "~c"
                               };
            {
                List<LogConfig> cfgs = new List<LogConfig>();
                foreach (string n in configs)
                {
                    LogConfig cfg = new LogConfig();
                    cfg.ConfigName = n;
                    cfg.FilePath = "./" + n + ".nolog";
                    if (File.Exists(cfg.FilePath))
                    {
                        File.Delete(cfg.FilePath);
                    }
                    if (LogConfig.Save(cfg) == false)
                    {
                        TestMain.Regression(typeof(ConMainTest).Name, 1001);
                    }
                    cfgs.Add(cfg);
                }
            }
            {
                // STEP: List Configurations
                LogOptionParams p = new LogOptionParams(0, null, null);
                MemoryStream ms = new MemoryStream(1024);
                StreamWriter swriter = new StreamWriter(ms);
                if (p.SetOutput(swriter) == false)
                {
                    TestMain.Regression(typeof(ConMainTest).Name, 1011);
                }
                EzLog.ConMain.config_list(ref p);
                swriter.Flush();
                ms.Seek(0L, SeekOrigin.Begin);
                StreamReader reader = new StreamReader(ms);
                string result = reader.ReadToEnd();
                /*
                User-Defined Configurations:
                1.) \~a.config
                2.) \~b.config
                3.) \~c.config
                */
                if (result.IndexOf("~a.config") == -1)
                {
                    TestMain.Regression(typeof(ConMainTest).Name, 1021);
                }
                if (result.IndexOf("~b.config") == -1)
                {
                    TestMain.Regression(typeof(ConMainTest).Name, 1031);
                }
                if (result.IndexOf("~c.config") == -1)
                {
                    TestMain.Regression(typeof(ConMainTest).Name, 1041);
                }
            }
            {
                // STEP: Remove Configurations
                foreach (string n in configs)
                {
                    LogConfig cfg = LogConfig.LoadConfig(n);
                    if (cfg == null)
                    {
                        TestMain.Regression(typeof(ConMainTest).Name, 2001);
                    }
                    File.Delete(cfg.GetConfigFile());
                }
                LogOptionParams p = new LogOptionParams(0, null, null);
                MemoryStream ms = new MemoryStream(1024);
                StreamWriter swriter = new StreamWriter(ms);
                if (p.SetOutput(swriter) == false)
                {
                    TestMain.Regression(typeof(ConMainTest).Name, 2011);
                }
                EzLog.ConMain.config_list(ref p);
                swriter.Flush();
                ms.Seek(0L, SeekOrigin.Begin);
                StreamReader reader = new StreamReader(ms);
                string result = reader.ReadToEnd();
                /*
                User-Defined Configurations:
                1.) \~a.config
                2.) \~b.config
                3.) \~c.config
                */
                if (result.IndexOf("~a.config") != -1)
                {
                    TestMain.Regression(typeof(ConMainTest).Name, 2021);
                }
                if (result.IndexOf("~b.config") != -1)
                {
                    TestMain.Regression(typeof(ConMainTest).Name, 2031);
                }
                if (result.IndexOf("~c.config") != -1)
                {
                    TestMain.Regression(typeof(ConMainTest).Name, 3041);
                }
            }

        }
    }
}
