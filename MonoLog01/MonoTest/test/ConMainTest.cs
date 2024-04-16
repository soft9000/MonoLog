using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace EzLog
{
    [TestClass]
    class ConMainTest
    {
        // Testing The Working, Only.
        [TestMethod]
        static void Main(string[] args)
        {
            // STEP:
            string[] test = 
                    {
                    "This", "isatest."
                    };
            if (ConMain.DoMain(test) != 0)
            {
                throw new Exception("Error: Regression 1000");
            }
            LogConfig cfg = new LogConfig();

            // STEP:
            string ATEST = "testa";
            if (LogConfig.GetConfigNameError(ATEST) != null)
            {
                throw new Exception("Error: Regression 1001");
            }
            cfg.ConfigName = ATEST;
            if (LogConfig.Save(cfg) == false)
            {
                throw new Exception("Error: Regression 1011");

            }
            if (LogConfig.LoadConfig(cfg.GetConfigName()) == null)
            {
                throw new Exception("Error: Regression 1021");
            }
            test = new string[] {
                    "--config",
                    ATEST,
                    "This is a test to config."
                    };
            if (ConMain.DoMain(test) != 0)
            {
                throw new Exception("Error: Regression 1031");
            }

            Console.WriteLine("Testing Success!");

        }
    }
}
