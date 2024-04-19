using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using EzLog;

namespace EzLogTesting
{

    public class ConMainTest
    {
        // Testing The Working, Only.
        public static void MainTest()
        {
            // STEP:
            string[] test = 
                    {
                    "This", "isatest."
                    };
            if (ConMain.DoMain(test) != 0)
            {
                TestMain.Regression(typeof(ConMainTest).Name, 1001);
            }
            LogConfig cfg = new LogConfig();

            // STEP:
            string ATEST = "testa";
            if (LogConfig.GetConfigNameError(ATEST) != null)
            {
                TestMain.Regression(typeof(ConMainTest).Name,1011);
            }
            cfg.ConfigName = ATEST;
            if (LogConfig.Save(cfg) == false)
            {
                TestMain.Regression(typeof(ConMainTest).Name, 1021);

            }
            if (LogConfig.LoadConfig(cfg.GetConfigName()) == null)
            {
                TestMain.Regression(typeof(ConMainTest).Name, 1031);
            }
            test = new string[] {
                    "--config",
                    ATEST,
                    "This is a test to config."
                    };
            if (ConMain.DoMain(test) != 0)
            {
                TestMain.Regression(typeof(ConMainTest).Name, 1041);
            }

        }
    }
}
