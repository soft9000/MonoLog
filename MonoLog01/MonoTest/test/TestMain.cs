using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EzLog;
using System.Runtime.CompilerServices;

namespace EzLogTesting
{
    [TestClass]
    public class TestMain
    {
        static void Main(string[] args)
        {
            new TestMain().MegaTestSeq();
        }

        public static void Regression(string message, int code, [CallerLineNumber] int line = 0)
        {
            string info = String.Format("Regression: {0} {1} @{2}", message, code, line);
            throw new Exception(info);
        }

        [TestMethod]
        public void MegaTestSeq()
        {
            TagLinesTest.MainTest();
            ConfigManagementTest.MainTest();
            LogConfigDlgTest.MainTest();
            ConMainTest.MainTest();

            MonoLog.Log("\n\nTesting Success - Press [enter] to continue: ");
            Console.In.ReadLine();
        }
    }
}
