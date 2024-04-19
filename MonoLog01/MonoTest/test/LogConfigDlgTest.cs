using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EzLog;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace EzLogTesting
{
    public class LogConfigDlgTest
    {
        static void Main(string[] args)
        {
            MainTest();
        }

        // Testing The Working, Only.
        public static void MainTest()
        {
            String tname = "testA";
            String tlfA = "c:/foo/TestA.log";
            File.Delete(tlfA);
            // STEP: Create & Load a Configuration TOKEN
            {
                File.Delete(tlfA);
                MemoryStream ms = new MemoryStream();
                StreamWriter userLines = new StreamWriter(ms);
                userLines.WriteLine(tname);
                userLines.WriteLine(tlfA);
                userLines.WriteLine("Nope");
                userLines.Flush();
                ms.Seek(0L, SeekOrigin.Begin);

                LogOptionParams p = new LogOptionParams(0, "--config", null);
                StreamWriter osw = new StreamWriter(new MemoryStream());
                p.SetOutput(osw);
                StreamReader isw = new StreamReader(ms);
                p.SetInput(isw);
                LogConfig cfgA = LogConfigDlg.Create(p);
                if (LogConfig.Save(cfgA) == false)
                {
                    TestMain.Regression(typeof(ConMainTest).Name, 1000);
                }
                LogConfig cfg = LogConfig.LoadConfig(tname);
                if (cfg == null)
                {
                    TestMain.Regression(typeof(ConMainTest).Name, 1001);
                }
                TROOL trool = LogConfigDlg.DisplayOrUpdate(cfg, p);
                if (cfgA.CompareTo(cfg) != 0)
                {
                    TestMain.Regression(typeof(ConMainTest).Name, 1011);
                }
                if (File.Exists(cfg.GetConfigFile()) == false)
                {
                    TestMain.Regression(typeof(ConMainTest).Name, 1021);
                }

                if (MonoLog.Log(cfg, "Simply elegant") == false)
                {
                    TestMain.Regression(typeof(ConMainTest).Name, 1031);
                }
                if (File.Exists(cfg.GetConfigFile()) == false)
                {
                    TestMain.Regression(typeof(ConMainTest).Name, 1041);
                }
                if (File.Exists(cfg.GetLogFile()) == false)
                {
                    TestMain.Regression(typeof(ConMainTest).Name, 1051);
                }
                if (File.Exists(tlfA) == false)
                {
                    TestMain.Regression(typeof(ConMainTest).Name, 1061);
                }
            }

            // STEP: Update a configuration BY TOKEN.
            String tlfB = "c:/foo/Test2.log";
            File.Delete(tlfB);
            {
                LogConfig cfg = LogConfig.LoadConfig(tname);
                if (cfg == null)
                {
                    TestMain.Regression(typeof(ConMainTest).Name, 2001);
                }

                MemoryStream ms = new MemoryStream();
                StreamWriter userLines = new StreamWriter(ms);
                userLines.WriteLine("yup");
                userLines.WriteLine(tlfB);
                userLines.WriteLine("Nope");
                userLines.Flush();
                ms.Seek(0L, SeekOrigin.Begin);

                LogOptionParams p = new LogOptionParams(0, "--config", null);
                p.SetOutput(new StreamWriter(new MemoryStream()));
                p.SetInput(new StreamReader(ms));
                TROOL trool = LogConfigDlg.DisplayOrUpdate(cfg, p);
                if (trool == TROOL.ERROR)
                {
                    TestMain.Regression(typeof(ConMainTest).Name, 2011);
                }
                if (LogConfig.Save(cfg) == false)
                {
                    TestMain.Regression(typeof(ConMainTest).Name, 2021);
                }
                if (MonoLog.Log(cfg, "Simply eleganterer") == false)
                {
                    TestMain.Regression(typeof(ConMainTest).Name, 2031);
                }
                if (File.Exists(cfg.GetConfigFile()) == false)
                {
                    TestMain.Regression("ELogConfigDlgTest", 2041);
                }
                if (File.Exists(cfg.GetLogFile()) == false)
                {
                    TestMain.Regression(typeof(ConMainTest).Name, 2051);
                }
                if (File.Exists(tlfB) == false)
                {
                    TestMain.Regression(typeof(ConMainTest).Name, 2061);
                }
            }

            File.Delete(tlfA);
            File.Delete(tlfB);

        }
    }
}
