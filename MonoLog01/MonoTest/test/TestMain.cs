using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EzLog;

namespace EzLogTesting
{
    [TestClass]
    public class TestMain
    {
        static void Main(string[] args)
        {
            new TestMain().MegaTestSeq();
        }

        [TestMethod]
        public void MegaTestSeq()
        {
            TagLinesTest.MainTest();
            ConfigManagementTest.MainTest();
            LogConfigDlgTest.MainTest();
            ConMainTest.MainTest();
            MonoLog.Log("Testing Success");
        }
    }
}
