using AventStack.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Api.BDDTest.Common
{
    [Binding]
    public class Hooks
    {
        public static ExtentReports extent;
        public static ExtentTest test;

        [BeforeFeature()]
        public static void BasicSetUp()
        {
            extent = new ExtentReports();
        }

        [BeforeScenario()]
        public static void BeforeScenarioSetUp()
        {
            string testName = "API";
            test = extent.CreateTest(testName);
        }
        [AfterFeature()]
        public static void EndReport()
        {
            extent.Flush();

        }
    }
}
