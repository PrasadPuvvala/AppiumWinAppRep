using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;


namespace AppiumWinApp
{
    [TestFixture]
    public class SampleTest
    {
        private static IWebDriver driver;
        private static ExtentReports extent;
        private static ExtentHtmlReporter htmlReporter;
        private static ExtentTest test;

        [OneTimeSetUp]
        public void SetupReporting()
        {
            htmlReporter = new ExtentHtmlReporter("F:\\Winium\\AppiumWinApp\\report.html");
            htmlReporter.LoadConfig("F:\\Winium\\AppiumWinApp\\AppiumWinApp\\ExtentConfig.xml");
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }

        [SetUp]
        public void InitBrowser()
        {

        }

        [Test]
        public void PassingTest()
        {
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
            test = extent.CreateTest("Passing test");

            // driver.Navigate().GoToUrl("http://www.google.com");

            try
            {
                Assert.IsTrue(true);
                stepName.Log(Status.Pass, "Test executed");
                test.Pass("Assertion passed");
            }
            catch (AssertionException)
            {
                test.Fail("Assertion failed");
                throw;
            }
        }

        [Test]
        public void FailingTest()
        {
            test = extent.CreateTest("Failing test");

            //  driver.Navigate().GoToUrl("http://www.yahoo.com");

            try
            {
                Assert.IsTrue(false);
                test.Pass("Assertion passed");
            }
            catch (AssertionException)
            {
                test.Fail("Assertion failed");
                throw;
            }
        }

        [TearDown]
        public void CloseBrowser()
        {
            //  driver.Quit();
        }

        [OneTimeTearDown]
        public void GenerateReport()
        {
            extent.Flush();
        }
    }
}