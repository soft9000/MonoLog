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
            // STEP: Creat & Load a Configuration TOKEN
            {
                File.Delete(tlfA);
                MemoryStream ms = new MemoryStream();
                StreamWriter userLines = new StreamWriter(ms);
                userLines.WriteLine(tname);
                userLines.WriteLine(tlfA);
                userLines.WriteLine("Nope");
                userLines.Flush();

                ms.Seek(0L, SeekOrigin.Begin);

                StreamWriter wResult = new StreamWriter(new MemoryStream());
                LogConfig cfgA = LogConfigDlg.Create(
                    wResult,
                    new StreamReader(ms)
                    );
                if (LogConfig.Save(cfgA) == false)
                {
                    throw new Exception("Error: Regression 1001");
                }
                LogConfig cfg = LogConfig.LoadConfig(tname);
                if (cfg == null)
                {
                    throw new Exception("Error: Regression 1011");
                }
                if (cfgA.CompareTo(cfg) != 0)
                {
                    throw new Exception("Error: Regression 1021");
                }                
                if (File.Exists(cfg.GetConfigFile()) == false)
                {
                    throw new Exception("Error: Regression 1031");
                }

                if (MonoLog.Log(cfg, "Simply elegant") == false)
                {
                    throw new Exception("Error: Regression 1041");
                }
                if (File.Exists(cfg.GetConfigFile()) == false)
                {
                    throw new Exception("Error: Regression 1041");
                }
                if (File.Exists(cfg.GetLogFile()) == false)
                {
                    throw new Exception("Error: Regression 1051");
                }
                if (File.Exists(tlfA) == false)
                {
                    throw new Exception("Error: Regression 1061");
                }
            }

            // STEP: Update a configuration BY TOKEN.
            String tlfB = "c:/foo/Test2.log";
            File.Delete(tlfB);
            {
                LogConfig cfg = LogConfig.LoadConfig(tname);
                if (cfg == null)
                {
                    throw new Exception("Error: Regression 2001");
                } 
                
                MemoryStream ms = new MemoryStream();
                StreamWriter userLines = new StreamWriter(ms);
                userLines.WriteLine("yup");
                userLines.WriteLine(tlfB);
                userLines.WriteLine("Nope");
                userLines.Flush();

                ms.Seek(0L, SeekOrigin.Begin);

                StreamWriter wResult = new StreamWriter(new MemoryStream());
                TROOL trool = LogConfigDlg.DisplayOrUpdate(cfg,
                    wResult,
                    new StreamReader(ms)
                    );
                if (trool == TROOL.ERROR)
                {
                    throw new Exception("Error: Regression 2011");
                }
                if (LogConfig.Save(cfg) == false)
                {
                    throw new Exception("Error: Regression 2021");
                }
                if (MonoLog.Log(cfg, "Simply eleganterer") == false)
                {
                    throw new Exception("Error: Regression 2031");
                }
                if (File.Exists(cfg.GetConfigFile()) == false)
                {
                    throw new Exception("Error: Regression 2041");
                }
                if (File.Exists(cfg.GetLogFile()) == false)
                {
                    throw new Exception("Error: Regression 2051");
                }
                if (File.Exists(tlfB) == false)
                {
                    throw new Exception("Error: Regression 2061");
                }
            }

            File.Delete(tlfA);
            File.Delete(tlfB);
            TUI.Message("Testing Success.", Console.Out);

        }
    }
}
