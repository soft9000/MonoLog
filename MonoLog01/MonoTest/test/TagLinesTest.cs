using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using EzLog;

namespace EzLogTesting
{
    public class TagLinesTest
    {
        static void Main(string[] args)
        {
            MainTest();
        }

        // Testing The Critical Path, Only.
        public static void MainTest()
        {
            string ZFILE = "testA"+ LogConfig.TYPE_CONFIG;

            // STEP:
            TagLines testA = new TagLines();
            testA.Add(new TagLine("MoNO", "Thi si a longis one."));
            if (TagLines.FileWrite(ZFILE, testA) == false)
            {
                TestMain.Regression(typeof(ConMainTest).Name, 1001);
            }
            TagLines testB = TagLines.FileRead(ZFILE);
            if (testB == null || testB.CompareTo(testA) != 0)
            {
                TestMain.Regression(typeof(ConMainTest).Name, 1011);
            }

            // STEP:
            testA.Add(new TagLine("Zippo", "The Z is the Zip."));
            if (TagLines.FileWrite(ZFILE, testA) == false)
            {
                TestMain.Regression(typeof(ConMainTest).Name, 1021);
            }
            testB = TagLines.FileRead(ZFILE);
            if (testB == null || testB.CompareTo(testA) != 0)
            {
                TestMain.Regression(typeof(ConMainTest).Name, 1031);
            }

            // STEP:
            testA.Add(new TagLine("Baston", "Ware Ware III."));
            if (testB.CompareTo(testA) == 0)
            {
                TestMain.Regression(typeof(ConMainTest).Name, 1041);
            }

            // DONE:
            System.IO.File.Delete(ZFILE);

        }
    }
}
