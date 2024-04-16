using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EzLog
{
    [TestClass]
    class TagLinesTest
    {
        // Testing The Critical Path, Only.
        [TestMethod]
        public static void Main(string[] args)
        {
            string ZFILE = "testA.config";

            // STEP:
            TagLines testA = new TagLines();
            testA.Add(new TagLine("MoNO", "Thi si a longis one."));
            if (TagLines.FileWrite(ZFILE, testA) == false)
            {
                throw new Exception("Error 1001: TestA Regression.");
            }
            TagLines testB = TagLines.FileRead(ZFILE);
            if (testB == null || testB.CompareTo(testA) != 0)
            {
                throw new Exception("Error 1011: TestA Regression.");
            }

            // STEP:
            testA.Add(new TagLine("Zippo", "The Z is the Zip."));
            if (TagLines.FileWrite(ZFILE, testA) == false)
            {
                throw new Exception("Error 1021: TestA Regression.");
            }
            testB = TagLines.FileRead(ZFILE);
            if (testB == null || testB.CompareTo(testA) != 0)
            {
                throw new Exception("Error 1031: TestA Regression.");
            }

            // STEP:
            testA.Add(new TagLine("Baston", "Ware Ware III."));
            if (testB.CompareTo(testA) == 0)
            {
                throw new Exception("Error 1041: TestA Regression.");
            }

            // DONE:
            System.IO.File.Delete(ZFILE);

            Console.WriteLine("Testing Success.");

        }
    }
}
