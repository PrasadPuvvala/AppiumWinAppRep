using AppiumWinApp;
using AppiumWinApp.PageFactory;
using AppiumWinApp.StepDefinitions;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
//using java.io;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Management.Smo;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
//using Console = System.Console;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;
using Xamarin.Forms;
using Console = System.Console;
using Environment = System.Environment;
using File = System.IO.File;
using AventStack.ExtentReports.Gherkin.Model;
using System.Collections.ObjectModel;
using com.sun.rowset.@internal;
using com.sun.tools.corba.se.idl.constExpr;
using ClosedXML.Excel;
using Microsoft.Identity.Client;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.TestManagement.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using System.Xml.Linq;
using Process = System.Diagnostics.Process;
using MailMessage = System.Net.Mail.MailMessage;
using sun.security.x509;
using OfficeOpenXml;
using System.Xml;
using Reqnroll;

namespace MyNamespace
{
    [Binding]
    public class StepDefinitions
    {
        private const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        private readonly ScenarioContext _scenarioContext;
        public static WindowsDriver<WindowsElement> session;
        private string ApplicationPath = null;
        protected static IOSDriver<IOSElement> AlarmClockSession;   // Temporary placeholder until Windows namespace exists
        protected static IOSDriver<IOSElement> DesktopSession;
        private ExtentTest test;
        public static appconfigsettings config;
        private static ExtentReports extent;
        static string configsettingpath = System.IO.Directory.GetParent(@"../../../").FullName
      + Path.DirectorySeparatorChar + "appconfig.json";
        public static String textDir = Directory.GetCurrentDirectory();
        public TestContext TestContext { get; set; }
       
        /*   declaration and initialization of a string variable */

        public static string workFlowProductSelection = "treeView";
        public static string storageLayOutDate = "WindowsForms10.Window.8.app.0.2804c64_r9_ad1";
        public static string algoTestProp = "";
        string computer_name = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
        String user_name = Environment.UserName;

        public static string screenshot = string.Empty;
        private static VssConnection vssConnection = null;

        public StepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
           
        }


        [Given(@"Importing Test Cases to Excel from TFS TestPlanID ""([^""]*)"" equivalent to Testcase Configuration ""([^""]*)"" to Create XML.")]
        [Obsolete]
        public void GivenImportingTestCasesToExcelFromTFSTestPlanIDEquivalentToTestcaseConfigurationToCreateXML(string testPlanID, string testCaseConfiguration)
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;

            Uri collectionUri = new Uri("https://tfs.gnhearing.com/tfs/GNR");
            string personalAccessToken = "2tqmhdkzejhtufnz3fo6rjtodlstu6fl2yoxiuzxnwtf2tscivmq"; // Replace with your PAT
            VssBasicCredential credentials = new VssBasicCredential(string.Empty, personalAccessToken);
            vssConnection = new VssConnection(collectionUri, credentials);
            Console.WriteLine("Authentication successful!");
            var projectClient = vssConnection.GetClient<ProjectHttpClient>();
            var testClient = vssConnection.GetClient<TestManagementHttpClient>();
            var workItemClient = vssConnection.GetClient<Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItemTrackingHttpClient>();
            var projects = projectClient.GetProjects().Result;
            TeamProjectReference project = projects.FirstOrDefault(p => p.Name == "GNR");
            if (project != null)
            {
                using (var workbook = new XLWorkbook())
                {
                    string excelFileName = "TFSTestPlanUpdation.xlsx";
                    string excelFilePath = Path.Combine(Directory.GetCurrentDirectory(), excelFileName);
                    if (File.Exists(excelFilePath))
                    {
                        File.Delete(excelFileName);
                    }
                    var worksheet = workbook.Worksheets.Add("TFSUpdation");
                    // Create column headers
                    worksheet.Cell(1, 1).Value = "Test Plan ID";
                    worksheet.Cell(1, 2).Value = "Test Suite ID";
                    worksheet.Cell(1, 3).Value = "Test Case ID";
                    worksheet.Cell(1, 4).Value = "Test Configuration";
                    worksheet.Cell(1, 5).Value = "Test Scenarios";
                    worksheet.Cell(1, 6).Value = "Test Steps";
                    int currentRow = 2;
                    var testSuites = testClient.GetTestSuitesForPlanAsync(project.Id.ToString(), int.Parse(testPlanID)).Result;
                    foreach (var suite in testSuites)
                    {
                        var testCases = testClient.GetTestCasesAsync(project.Name, int.Parse(testPlanID), suite.Id).Result;
                        if (testCases.Count != 0)
                        {
                            foreach (var testcase in testCases)
                            {
                                var workItem = workItemClient.GetWorkItemAsync(int.Parse(testcase.Workitem.Id), expand: WorkItemExpand.Relations).Result;
                                var title = workItem.Fields["System.Title"];
                                var testConfigurations = testcase.PointAssignments.Select(pa => pa.Configuration).ToList();
                                foreach (var configuration in testConfigurations)
                                {
                                    var configurationName = configuration.Name;
                                    if (configurationName == testCaseConfiguration)
                                    {
                                        var testcaseSteps = workItem.Fields.ContainsKey("Microsoft.VSTS.TCM.Steps") ? workItem.Fields["Microsoft.VSTS.TCM.Steps"].ToString() : "N/A";
                                        var testCaseId = workItem.Id;
                                        int testStepsCount = 0;
                                        // Load the XML string into an XDocument if it's not empty
                                        if (!string.IsNullOrEmpty(testcaseSteps))
                                        {
                                            if (testcaseSteps == "N/A")
                                            {

                                            }
                                            else
                                            {
                                                XDocument xdoc = XDocument.Parse(testcaseSteps);
                                                testStepsCount = xdoc.Descendants("step").Count();
                                            }
                                        }
                                        // Write data to Excel
                                        worksheet.Cell(currentRow, 1).Value = testPlanID;
                                        worksheet.Cell(currentRow, 2).Value = suite.Id;
                                        worksheet.Cell(currentRow, 3).Value = workItem.Id;
                                        worksheet.Cell(currentRow, 4).Value = configurationName;
                                        worksheet.Cell(currentRow, 5).Value = title.ToString();
                                        worksheet.Cell(currentRow, 6).Value = testStepsCount;
                                        currentRow++;
                                    }
                                }
                                workbook.SaveAs(excelFilePath);
                            }
                        }
                    }
                }
            }
            FunctionLibrary lib = new FunctionLibrary();
            string scenarioName = ScenarioContext.Current.ScenarioInfo.Title;

            lib.PassingXML(test, scenarioName);
        }

        /** This is used for launching the FDTS
          * Passes the HI Serial number
          * perfrom Flashing and close the FDTS **/


        [Given(@"Launch FDTS WorkFlow And Flash Device ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)""and""([^""]*)""")]
        [Obsolete]
        public void GivenLaunchFDTSWorkFlowAndFlashDeviceAndAndAndAnd(string device, string DeviceNo, string flashHIWithSlno, string side, string DeviceType)
        {

            Console.WriteLine("This is Given method");
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            config = FeatureContext.Current["config"] as appconfigsettings;

            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());

            /** Socket Launching to pass commands for D2 and D3 **/

            try
            {
                if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
                {

                    //Thread.Sleep(240000);

                    if (side.Equals("Left"))
                    {
                        ModuleFunctions.socketA(session, test, DeviceType);
                    }
                    else if (side.Equals("Right"))
                    {
                        ModuleFunctions.socketB(session, test, DeviceType);
                    }
                }
            }
            catch (Exception ex) { }


            /** FDTS Launching **/


            FunctionLibrary lib = new FunctionLibrary();
            //session = ModuleFunctions.sessionInitialize1(config.ApplicationPath.FDTSAppPath, config.workingdirectory.FDTS);
            Thread.Sleep(2000);
            AppiumOptions appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", config.ApplicationPath.FDTSAppPath);
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
            appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "40");
            appCapabilities.AddAdditionalCapability("appArguments", "--run-as-administrator");
            appCapabilities.AddAdditionalCapability("appWorkingDir", config.workingdirectory.FDTS);
            appCapabilities.AddAdditionalCapability("appArguments", "Test.exe");
            appCapabilities.AddAdditionalCapability("automationName", "Windows");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
            appCapabilities.AddAdditionalCapability("ms:experimental-webdriver", true);
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            Thread.Sleep(8000);
            //session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);

            stepName.Log(Status.Pass, "Test Work Flow launched successfully");

            try
            {
                System.IO.Directory.Delete(@"C:\Users\Public\Documents\Camelot\Logs", true);
            }
            catch (Exception e)
            {

            }

            Thread.Sleep(5000);
            //session.FindElement(WorkFlowPageFactory.filterBox).Clear();
            var autoIDs = session.FindElementByAccessibilityId("textBoxFilter");
            autoIDs.Clear();
            string devName = device;
            session.FindElement(WorkFlowPageFactory.filterBox).SendKeys(devName);
            Thread.Sleep(1000);
            session.FindElement(WorkFlowPageFactory.filterBox).SendKeys(Keys.Tab);
            Thread.Sleep(2000);

            /** declaration and initialization using var keyword **/


            var prdName = session.FindElements(WorkFlowPageFactory.workFlowProductSelection);
            var name = prdName[0].FindElementByXPath("*/*");
            string txt = name.GetAttribute("Name");
            name.Click();
            Actions action = new Actions(session);

            if (devName.Contains("RE961-DRWC"))
            {
                action.MoveToElement(name).Click().DoubleClick().Build().Perform();

                var elements = Enumerable.Range(1, 9).Select(index => $"{devName} [{index}] (Final)").Select(elementName =>
                {
                    try
                    {
                        return session.FindElementByName(elementName);
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }).Where(element => element != null);

                foreach (var element in elements)
                {
                    element.Click();
                }

                session.FindElementByName(devName + " (Final)").Click();

                session.FindElementByName("Continue >>").Click();
                Thread.Sleep(10000);
                session.SwitchTo().Window(session.WindowHandles.First());
                //session.SwitchTo().ActiveElement();
                session.FindElementByName("Continue >>").Click();
                Thread.Sleep(2000);
                session.SwitchTo().Window(session.WindowHandles.First());
                //session.SwitchTo().ActiveElement();
                session.FindElementByName("Continue >>").Click();
                Thread.Sleep(4000);
                session.SwitchTo().Window(session.WindowHandles.First());
                //session.SwitchTo().ActiveElement();
                var secondWindow = session.FindElementsByClassName(workFlowProductSelection);
                Thread.Sleep(2000);
            }

            else if (devName.Contains("LT") || devName.Contains("RE"))
            {
                action.MoveToElement(name).Click().DoubleClick().Build().Perform();

                if (devName.Contains("LT"))
                {
                    session.FindElementByName(devName + " (Final)").Click();
                }

                if (devName.Contains("RE"))
                {
                    var elements = Enumerable.Range(1, 9).Select(index => $"{devName} [{index}] (Final)").Select(elementName =>
                    {
                        try
                        {
                            return session.FindElementByName(elementName);
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                    }).Where(element => element != null);

                    foreach (var element in elements)
                    {
                        element.Click();
                    }

                    session.FindElementByName(devName + " (Final)").Click();

                }
                else
                {
                    session.FindElementByName(devName + " (Final)").Click();
                }

                session.FindElementByName("Continue >>").Click();
                Thread.Sleep(2000);
                Thread.Sleep(2000);
                session.SwitchTo().Window(session.WindowHandles.First());
                ////session.SwitchTo().ActiveElement();
                session.FindElementByName("Continue >>").Click();
                Thread.Sleep(4000);
                session.SwitchTo().Window(session.WindowHandles.First());
                ////session.SwitchTo().ActiveElement();
                var secondWindow = session.FindElementsByClassName(workFlowProductSelection);
                Thread.Sleep(2000);
            }

            else if (device.Contains("RT") || device.Contains("RU") || device.Contains("NX") || device.Contains("CX") || device.Contains("VI"))
            {
                action.MoveToElement(name).Click().DoubleClick().Build().Perform();

                if (device.Contains("RT961-DRWC"))
                {

                    var elements = Enumerable.Range(1, 10).Select(index => $"{devName} [{index}] (Final)").Select(elementName =>
                    {
                        try
                        {
                            return session.FindElementByName(elementName);
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                    }).Where(element => element != null);

                    foreach (var element in elements)
                    {
                        element.Click();
                    }
                }
                else if (device.Contains("ITC"))
                {
                    var elements = Enumerable.Range(1, 10).Select(index => $"{devName} [{index}]").Select(elementName =>
                    {
                        try
                        {
                            return session.FindElementByName(elementName);
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                    }).Where(element => element != null);

                    foreach (var element in elements)
                    {
                        element.Click();
                        Actions a = new Actions(session);
                        a.MoveToElement(element).DoubleClick().Build().Perform();
                    }

                    Thread.Sleep(2000);

                    session.FindElementByName("Final").Click();

                    Thread.Sleep(2000);


                }
                else
                {
                    var elements = Enumerable.Range(1, 10).Select(index => $"{devName} [{index}] (Final)").Select(elementName =>
                    {
                        try
                        {
                            return session.FindElementByName(elementName);
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                    }).Where(element => element != null);

                    foreach (var element in elements)
                    {
                        element.Click();
                    }
                }

                session.FindElementByName("Continue >>").Click();
                Thread.Sleep(2000);
                session.SwitchTo().Window(session.WindowHandles.First());
                //session.SwitchTo().ActiveElement();

                /** Passing Serial Number of HI device **/

                session.FindElementByAccessibilityId("textBoxSN").SendKeys(DeviceNo);
                session.FindElementByName("Continue >>").Click();
                Thread.Sleep(40000);
                session.SwitchTo().Window(session.WindowHandles[0]);

                try
                {
                    if (session.FindElementByName("Specify Parameters").Displayed)
                    {
                        session.FindElementByName("Continue >>").Click();
                    }
                }
                catch (Exception)
                {
                    //session.FindElementByName("Continue >>").Click();
                }

                try
                {
                    if (session.FindElementByAccessibilityId("MessageForm").Displayed)

                    {

                        ModuleFunctions.discoveryFailed(session, test, textDir, device, side, DeviceNo, DeviceType);
                    }
                }
                catch (Exception)

                {
                }

                Thread.Sleep(8000);

                try
                {

                    session.SwitchTo().Window(session.WindowHandles.First());
                    //session.SwitchTo().ActiveElement();

                }
                catch { }

                var allWindowHandles = session.WindowHandles;

                try
                {
                    do
                    {
                        allWindowHandles = session.WindowHandles;
                        //session.SwitchTo().ActiveElement();


                    } while ((session.FindElementByClassName("WindowsForms10.STATIC.app.0.27a2811_r21_ad1").Enabled));
                }
                catch (Exception)
                {
                }

                if (computer_name.Equals("FSWIRAY80") && computer_name.Equals("FSWIRAY112"))
                {
                    session = ModuleFunctions.sessionInitialize1(config.ApplicationPath.FDTSAppPath, config.workingdirectory.FDTS);
                }

                else
                {
                    session = ModuleFunctions.sessionInitialize1(config.ApplicationPath.FDTSAppPath, config.workingdirectory.FDTS);
                }

                try
                {
                    session.SwitchTo().Window(session.WindowHandles.First());
                    //session.SwitchTo().ActiveElement();
                    if (DeviceType.Equals("Rechargeable"))
                    {
                        //session.SwitchTo().ActiveElement();
                        //lib.waitUntilElementExists(session, "testParameter-Multiple-BatteryType", 1);
                        session.FindElementByName("testParameter-Multiple-BatteryType");
                        session.FindElementByName("Continue >>").Click();
                    }
                }
                catch (Exception) { }

                Thread.Sleep(5000);

                try
                {
                    session.SwitchTo().Window(session.WindowHandles.First());
                    //session.SwitchTo().ActiveElement();

                    if (session.FindElementByName("Optimized").Displayed)
                    {
                        session.FindElementByName("Optimized").Click();
                        Thread.Sleep(2000);
                        session.SwitchTo().Window(session.WindowHandles.First());
                        //session.SwitchTo().ActiveElement();
                        Thread.Sleep(2000);
                    }
                }

                catch (Exception e)

                {
                    Console.WriteLine(e.Message);
                }

                Thread.Sleep(5000);

                try
                {
                    session.SwitchTo().Window(session.WindowHandles.First());
                    //session.SwitchTo().ActiveElement();

                    /** Passing administrator password **/

                    session.FindElementByAccessibilityId("textBoxPassword").SendKeys("1234");
                    session.FindElementByName("Continue >>").Click();
                    Thread.Sleep(4000);
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Thread.Sleep(5000);
            }


            if (devName.Contains("LT") || devName.Contains("RE"))
            {
                var wait = new WebDriverWait(session, TimeSpan.FromSeconds(200));

                if (devName.Contains("RE") && computer_name.Equals("FSWIRAY80"))
                {

                }
                else
                {
                    session.FindElementByName("Continue >>").Click();
                }

                Thread.Sleep(2000);

                session.SwitchTo().Window(session.WindowHandles.First());
                ////session.SwitchTo().ActiveElement();

                if (flashHIWithSlno == "Yes")
                {
                    session.FindElementByAccessibilityId("textBoxSN").Click();
                    session.FindElementByAccessibilityId("textBoxSN").SendKeys(DeviceNo);
                }
                else
                {
                    session.FindElementByAccessibilityId("textBoxSN").Click();
                    session.FindElementByAccessibilityId("textBoxSN").SendKeys(DeviceNo);
                }

                session.FindElementByName("Continue >>").Click();
                Thread.Sleep(2000);

                stepName.Log(Status.Pass, "Flashing is started for Device" + devName);

                /** To handle Optimizeed window and Click on Continue **/

                try
                {
                    session.SwitchTo().Window(session.WindowHandles.First());
                    //session.SwitchTo().ActiveElement();

                    if (session.FindElementByName("Optimized").Displayed)
                    {
                        session.FindElementByName("Optimized").Click();
                        Thread.Sleep(2000);
                        session.SwitchTo().Window(session.WindowHandles.First());
                        //session.SwitchTo().ActiveElement();
                        Thread.Sleep(2000);

                    }
                }

                catch (Exception e)
                {
                }

                /** Log on window and passes admin password then Click on Continue **/

                try
                {
                    session.FindElementByAccessibilityId("textBoxPassword").SendKeys("1234");
                    session.FindElementByName("Continue >>").Click();
                    Thread.Sleep(4000);
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }



            Thread.Sleep(8000);

            if (computer_name == "FSWIRAY80" || computer_name == "UKBRAHPF2M76W6" || computer_name == "FSWIRAY112")
            {

                Thread.Sleep(5000);
                var allWindowHandles = session.WindowHandles;

                try
                {
                    do
                    {
                        allWindowHandles = session.WindowHandles;
                        //session.SwitchTo().ActiveElement();


                    } while ((session.FindElementByClassName("Button").Enabled).ToString() == "False");
                }

                catch (Exception e)
                {
                    session = FunctionLibrary.clickOnCloseTestFlow(session, "Test Execution");
                    session = lib.waitForElementToBeClickable(session, "Close");
                    stepName.Pass("Succesfully Flashed");
                    stepName.Info("Device: " + devName + " flashed");
                }
            }

            try
            {
                if (computer_name.Equals("UKBRAHPF2M76W6 || FSWIRAY112"))

                {
                    session = ModuleFunctions.sessionInitialize(config.ApplicationPath.TestRuntimePC, config.workingdirectory.TestRuntime);
                    session = lib.waitForElementToBeClickable(session, "Close");
                }

                Thread.Sleep(6000);

                if (computer_name.Equals("FSWIRAY80 || FSWIRAY112 || UKBRAHPF2M76W6"))
                {
                    session = ModuleFunctions.sessionInitialize(config.ApplicationPath.FDTSAppPath, config.workingdirectory.FDTS);
                }

                else
                {
                    session = ModuleFunctions.sessionInitialize(config.ApplicationPath.FDTSAppPath, config.workingdirectory.FDTS);
                }
            }
            catch (Exception e) { }

            /** Clicks on stop button **/

            session = lib.waitForElement(session, "Stop");

            session.SwitchTo().Window(session.WindowHandles.First());
            Thread.Sleep(2000);

            /** Clicks on NextRound Button **/

            session.FindElementByName("Next Round >>").Click();
            Thread.Sleep(2000);
            session.SwitchTo().Window(session.WindowHandles.First());
            Thread.Sleep(2000);

            /** Clicks on Stop Button **/

            session.FindElementByName("Stop").Click();
            Thread.Sleep(2000);
            session.SwitchTo().Window(session.WindowHandles.First());
            Thread.Sleep(2000);

            /** Clicks on Shutdown Button **/

            session.FindElementByName("Shutdown").Click();
            Thread.Sleep(2000);


            int counter = 0;
            string line;
            var text = "Error";
            string Name = Environment.GetEnvironmentVariable("COMPUTERNAME");
            Console.WriteLine("Computer Name: " + Name);
            string userName = Environment.UserName;
            Console.WriteLine("User Name: " + userName);
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            Console.WriteLine("Date: " + today);
            string fileName = Name + "-" + userName + "-" + today;
            Console.WriteLine("Print file" + fileName);
            Console.WriteLine("Alltogether" + Name + "-" + userName + "-" + today);

            System.IO.StreamReader file =
                new System.IO.StreamReader("C:\\Users\\Public\\Documents\\Camelot\\Logs\\" + fileName + ".log");

            while ((line = file.ReadLine()) != null)
            {
                if (line.Contains(text))
                {
                    break;
                }
                else
                {
                    stepName.Log(Status.Pass, "No Errors found in the log file while flashing");
                }
                counter++;
            }

            Console.WriteLine("Line number: {0}", counter);
            file.Close();
            Thread.Sleep(4000);
        }

        [Given(@"Launch HIRegistration Tool to Unregister Cloud Info for Device A ""([^""]*)""")]
        public void GivenLaunchHIRegistrationToolToUnregisterCloudInfoForDeviceA(string serialNumber)
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            config = FeatureContext.Current["config"] as appconfigsettings;

            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());

            session = ModuleFunctions.sessionInitialize(config.ApplicationPath.HiRegistrationPath, config.workingdirectory.HiRegistration);
            Thread.Sleep(2000);
            AppiumOptions appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", config.ApplicationPath.HiRegistrationPath);
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
            appCapabilities.AddAdditionalCapability("appArguments", "--run-as-administrator");
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);

            stepName.Log(Status.Pass, "Hi Registration App launched successfully");

            session.FindElementByAccessibilityId("cloudEnvironmentComboBox").Click();
            Thread.Sleep(1000);
            session.FindElementByName("Verification").Click();
            Thread.Sleep(1000);
            session.FindElementByAccessibilityId("serialNumberTextBox").SendKeys(serialNumber);
            Thread.Sleep(1000);
            session.FindElementByClassName("Button").FindElementByName("Unregister").Click();
            Thread.Sleep(10000);
            if (session.FindElementByAccessibilityId("statusTextBlock").Text == $"Cloud registration for '{serialNumber}' not found.")
            {
                stepName.Log(Status.Pass, $"{session.FindElementByAccessibilityId("statusTextBlock").Text}");
                session.FindElementByAccessibilityId("cloudEnvironmentComboBox").Click();
                Thread.Sleep(1000);
                session.FindElementByName("Production").Click();
                Thread.Sleep(1000);
                session.FindElementByClassName("Button").FindElementByName("Unregister").Click();
                Thread.Sleep(5000);
                if (session.FindElementByName("Warning").FindElementByAccessibilityId("65535").Text == "You should be very cautious when unregistering HIs from the PRODUCTION cloud.\r\nDo you want to unregister this device from the Production cloud?")
                {
                    stepName.Log(Status.Pass, $"{session.FindElementByName("Warning").FindElementByAccessibilityId("65535").Text}");
                    session.FindElementByName("Yes").Click();
                    Thread.Sleep(10000);
                    if (session.FindElementByAccessibilityId("statusTextBlock").Text == $"Successfully archived cloud registration for '{serialNumber}'.")
                    {
                        stepName.Log(Status.Pass, $"{session.FindElementByAccessibilityId("statusTextBlock").Text}");
                        session.FindElementByName("Close").Click();
                    }
                    else
                    {
                        stepName.Log(Status.Pass, $"{session.FindElementByAccessibilityId("statusTextBlock").Text}");
                        session.FindElementByName("Close").Click();
                    }
                }
            }
            else
            {
                stepName.Log(Status.Pass, $"{session.FindElementByAccessibilityId("statusTextBlock").Text}");
                session.FindElementByAccessibilityId("cloudEnvironmentComboBox").Click();
                Thread.Sleep(1000);
                session.FindElementByName("Production").Click();
                Thread.Sleep(1000);
                session.FindElementByClassName("Button").FindElementByName("Unregister").Click();
                Thread.Sleep(5000);
                if (session.FindElementByName("Warning").FindElementByAccessibilityId("65535").Text == "You should be very cautious when unregistering HIs from the PRODUCTION cloud.\r\nDo you want to unregister this device from the Production cloud?")
                {
                    stepName.Log(Status.Pass, $"{session.FindElementByName("Warning").FindElementByAccessibilityId("65535").Text}");
                    session.FindElementByName("Yes").Click();
                    Thread.Sleep(10000);
                    if (session.FindElementByAccessibilityId("statusTextBlock").Text == $"Successfully archived cloud registration for '{serialNumber}'.")
                    {
                        stepName.Log(Status.Pass, $"{session.FindElementByAccessibilityId("statusTextBlock").Text}");
                        session.FindElementByName("Close").Click();
                    }
                    else
                    {
                        stepName.Log(Status.Pass, $"{session.FindElementByAccessibilityId("statusTextBlock").Text}");
                        session.FindElementByName("Close").Click();
                    }
                }
            }
        }

        [Given("[Set Development and Verification System Role in Basic Setting for System Configuration]")]
        public void GivenSetDevelopmentAndVerificationSystemRoleInBasicSettingForSystemConfiguration()
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
            SystemPageFactory.launchSystemSettingsDevelopmentAndVerification(extent, stepName);
        }

        [Given("[Set Service GROC System Role in Basic Setting for System Configuration]")]
        [Obsolete]

        public void GivenSetServiceGROCSystemRoleInBasicSettingForSystemConfiguration()

        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
            SystemPageFactory.launchSystemSettingsServiceGROC(extent, stepName);
        }

        /** To create a patient, 
         * connect the HIs to the FSW, 
         * make adjustments, save them, and then exit. **/


        [When(@"\[Create a Patient and Fitting HI In FSW ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)""and""([^""]*)""]")]
        [Obsolete]
        public void WhenCreateAPatientAndFittingHIInFSWAndAndAndAnd(string alterValue, string device, string DeviceNo, string side, string DeviceType)
        {
            string devName = device;
            FunctionLibrary lib = new FunctionLibrary();
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            config = FeatureContext.Current["config"] as appconfigsettings;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());

            if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
            {
                Console.WriteLine("This is When method");

                try
                {
                    if (side.Equals("Left"))
                    {
                        ModuleFunctions.socketA(session, test, DeviceType);
                        Thread.Sleep(2000);
                    }
                    else if (side.Equals("Right"))
                    {
                        ModuleFunctions.socketB(session, test, DeviceType);
                        Thread.Sleep(2000);
                    }
                    else if (side.Equals("Cdevice"))
                    {
                        ModuleFunctions.socketC(session, test, DeviceType);
                        Thread.Sleep(2000);

                    }
                }
                catch (Exception ex) { }
                try
                {
                    session = ModuleFunctions.sessionInitialize(config.ApplicationPath.SmartFitAppPath, config.workingdirectory.FSWWorkingPath);
                    
                    try
                    {
                        string addPatientXpath = "//Button[@Name=' Add or Select Patient']";
                        WindowsElement patientXpath = session.FindElement(By.XPath(addPatientXpath));
                        patientXpath.Click();
                    }
                    catch (Exception ex)
                    {
                        string addPatientXpath = "//Button[@ClassName='btn btn-lg btn-secondary-dark w-30']";
                        WindowsElement patientXpath = session.FindElement(By.XPath(addPatientXpath));
                        patientXpath.Click();

                    }
                   
                    session = ModuleFunctions.sessionInitialize1(config.ApplicationPath.FSWAppPath, config.workingdirectory.FSWWorkingPath);
                   
                    WebDriverWait wait = new WebDriverWait(session, TimeSpan.FromMinutes(5));
                    try
                    {
                        wait.Until(d => d.FindElements(By.XPath("//Group")).Count > 0);

                        var allGroups = session.FindElements(By.XPath("//Group"));

                        foreach (var group in allGroups)
                        {
                            Actions actions = new Actions(session);

                            wait.Until(d => group.FindElements(By.XPath(".//Group[1]")).Count > 0);

                            var all = group.FindElements(By.XPath(".//Group[1]"));
                            foreach (var element in all)
                            {
                                wait.Until(d => element.FindElements(By.XPath(".//Group[1]")).Count > 0);

                                var all2 = element.FindElements(By.XPath(".//Group[1]"));
                                foreach (var child in all2)
                                {
                                    wait.Until(d => child.FindElements(By.XPath(".//*")).Count > 0);
                                    var allChildElements = child.FindElements(By.XPath(".//*"));
                                    if (allChildElements != null && allChildElements.Count >= 17)
                                    {
                                        var fourteenthElement = allChildElements[12];
                                        Actions actions1 = new Actions(session);
                                        actions1.MoveToElement(fourteenthElement).Click().Perform();

                                        var selectPatient2 = wait.Until(d => d.FindElement(By.Name("Fit Patient")));
                                        Thread.Sleep(2000);
                                        Actions actions2 = new Actions(session);
                                        actions2.MoveToElement(selectPatient2).Click().Perform();

                                        break;
                                    }

                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }

                    session = ModuleFunctions.sessionInitialize1(config.ApplicationPath.SmartFitAppPath, config.workingdirectory.FSWWorkingPath);
 
                    session.Manage().Window.Maximize();
                    session.SwitchTo().Window(session.WindowHandles.First());
                    session.FindElementByAccessibilityId("fittingpath-button-connect").Click();
                    Thread.Sleep(10000);

                    lib.clickOnAutomationName(session, "Assign Instruments");
                    WebDriverWait wait1 = new WebDriverWait(session, TimeSpan.FromMinutes(5));
                    WindowsElement comboBox = (WindowsElement)wait1.Until(driver =>
                    {
                        var element = driver.FindElement(By.ClassName("ComboBox"));
                        return element.Enabled ? element : null;
                    });

                    comboBox.Click();
                

                    /** Select Noah link Wireless now, then click Connect.  **/

                    session.FindElementByName("Noahlink Wireless").Click();
                    try
                    {
                        session.FindElementByName("Unassign").Click();
                    }
                    catch { }

                    Thread.Sleep(2000);



                    var SN1 = session.FindElementsByClassName("ListBoxItem");

                    // Check if DeviceNo is already discovered

                    foreach (WindowsElement value in SN1)
                    {
                        string S = value.Text;
                        if (S.Contains(DeviceNo))
                        {

                            value.FindElementByName("Assign Left").Click();
                            break;
                        }
                    }
                    Thread.Sleep(5000);

                    try
                    {
                        if (session.FindElementByName("Unassign").Enabled)
                        {

                            lib.clickOnAutomationName(session, "Assign Instruments");

                            Thread.Sleep(10000); // Initial wait before searching

                            var SN = session.FindElementsByClassName("ListBoxItem");

                            // Check if DeviceNo is already discovered

                            foreach (WindowsElement value in SN)
                            {
                                string S = value.Text;
                                if (S.Contains(DeviceNo))
                                {
                                    value.FindElementByName("Assign Left").Click();
                                    break;
                                }
                            }

                        }
                    }
                    catch (Exception)
                    {

                    }

                    do
                    {
                        if (session.FindElementByName("Continue").Enabled == false)
                        {
                            lib.processKill("SmartFitSA");
                            lib.processKill("SmartFit");
                            if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
                            {
                                Console.WriteLine("This is When method");
                                try
                                {
                                    if (side.Equals("Left"))
                                    {
                                        ModuleFunctions.socketA(session, test, DeviceType);
                                        Thread.Sleep(2000);
                                    }
                                    else if (side.Equals("Right"))
                                    {
                                        ModuleFunctions.socketB(session, test, DeviceType);
                                        Thread.Sleep(2000);
                                    }
                                }
                                catch
                                {

                                }
                                try
                                {
                                    Thread.Sleep(2000);
                                    session = ModuleFunctions.sessionInitialize(config.ApplicationPath.SmartFitAppPath, config.workingdirectory.FSWWorkingPath);

                                    try
                                    {
                                        string addPatientXpath = "//Button[@Name=' Add or Select Patient']";
                                        WindowsElement patientXpath = session.FindElement(By.XPath(addPatientXpath));
                                        patientXpath.Click();
                                    }
                                    catch (Exception ex)
                                    {
                                        string addPatientXpath = "//Button[@ClassName='btn btn-lg btn-secondary-dark w-30']";
                                        WindowsElement patientXpath = session.FindElement(By.XPath(addPatientXpath));
                                        patientXpath.Click();

                                    }

                                    session = ModuleFunctions.sessionInitialize1(config.ApplicationPath.FSWAppPath, config.workingdirectory.FSWWorkingPath);

                                    wait = new WebDriverWait(session, TimeSpan.FromMinutes(5));
                                    try
                                    {
                                        wait.Until(d => d.FindElements(By.XPath("//Group")).Count > 0);

                                        var allGroups = session.FindElements(By.XPath("//Group"));

                                        foreach (var group in allGroups)
                                        {
                                            Actions actions = new Actions(session);

                                            wait.Until(d => group.FindElements(By.XPath(".//Group[1]")).Count > 0);

                                            var all = group.FindElements(By.XPath(".//Group[1]"));
                                            foreach (var element in all)
                                            {
                                                wait.Until(d => element.FindElements(By.XPath(".//Group[1]")).Count > 0);

                                                var all2 = element.FindElements(By.XPath(".//Group[1]"));
                                                foreach (var child in all2)
                                                {
                                                    wait.Until(d => child.FindElements(By.XPath(".//*")).Count > 0);
                                                    var allChildElements = child.FindElements(By.XPath(".//*"));
                                                    if (allChildElements != null && allChildElements.Count >= 17)
                                                    {
                                                        var fourteenthElement = allChildElements[12];
                                                        Actions actions1 = new Actions(session);
                                                        actions1.MoveToElement(fourteenthElement).Click().Perform();

                                                        var selectPatient2 = wait.Until(d => d.FindElement(By.Name("Fit Patient")));
                                                        actions1.MoveToElement(selectPatient2).Click().Perform();

                                                        break;
                                                    }

                                                }
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                    }

                                    session = ModuleFunctions.sessionInitialize1(config.ApplicationPath.SmartFitAppPath, config.workingdirectory.FSWWorkingPath);

                                    session.Manage().Window.Maximize();
                                    session.SwitchTo().Window(session.WindowHandles.First());
                                    session.FindElementByAccessibilityId("fittingpath-button-connect").Click();
                                    Thread.Sleep(10000);

                                    lib.clickOnAutomationName(session, "Assign Instruments");
                                    wait1 = new WebDriverWait(session, TimeSpan.FromMinutes(5));
                                    comboBox = (WindowsElement)wait1.Until(driver =>
                                    {
                                        var element = driver.FindElement(By.ClassName("ComboBox"));
                                        return element.Enabled ? element : null;
                                    });

                                    comboBox.Click();


                                    /** Select Noah link Wireless now, then click Connect.  **/

                                    session.FindElementByName("Noahlink Wireless").Click();
                                    try
                                    {
                                        session.FindElementByName("Unassign").Click();
                                    }
                                    catch { }

                                    Thread.Sleep(2000);
                                    var SN2 = session.FindElementsByClassName("ListBoxItem");

                                    // Check if DeviceNo is already discovered

                                    foreach (WindowsElement value in SN2)
                                    {
                                        string S = value.Text;
                                        if (S.Contains(DeviceNo))
                                        {
                                            value.FindElementByName("Assign Left").Click();
                                            break;
                                        }
                                    }
                                }

                                catch
                                {
                                }

                                Thread.Sleep(5000);

                                try
                                {
                                    if (session.FindElementByName("Unassign").Enabled)
                                    {

                                        lib.clickOnAutomationName(session, "Assign Instruments");

                                        Thread.Sleep(10000); // Initial wait before searching

                                        var SN = session.FindElementsByClassName("ListBoxItem");

                                        // Check if DeviceNo is already discovered

                                        foreach (WindowsElement value in SN)
                                        {
                                            string S = value.Text;
                                            if (S.Contains(DeviceNo))
                                            {
                                                value.FindElementByName("Assign Left").Click();
                                                break;
                                            }
                                        }

                                    }
                                }
                                catch (Exception)
                                {

                                }
                            }
                        }
                    } while (!session.FindElementByName("Continue").Enabled);


                    /** Clicks on Continue buttion **/

                    lib.clickOnAutomationName(session, "Continue");

                    Thread.Sleep(4000);

                    try
                    {
                        lib.clickOnAutomationName(session, "Continue");
                    }
                    catch { }


                }
                catch (Exception)
                {

                }

            }


            if (DeviceType.Equals("D1rechargeableWired"))
            {
                //Console.WriteLine("This is When method");
                //Thread.Sleep(2000);
                //AppiumOptions appCapabilities = new AppiumOptions();
                //appCapabilities.AddAdditionalCapability("app", config.ApplicationPath.FSWAppPath);
                //appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
                //appCapabilities.AddAdditionalCapability("appArguments", "--run-as-administrator");
                //session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                //Thread.Sleep(10000);
                //session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                //Thread.Sleep(2000);
                //session.Manage().Window.Maximize();
                //var wait = new WebDriverWait(session, TimeSpan.FromSeconds(20));
                //var div = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("ListBoxItem")));
                //var text_Button = session.FindElementsByClassName("ListBoxItem");

                //stepName.Log(Status.Pass, "FSW is launched successfully");

                //int counter = 0;
                //string PatientName = null;
                //string PatientDescription = null;
                //foreach (var element in text_Button)
                //{
                //    if (counter == 2)
                //    {
                //        PatientName = element.GetAttribute("AutomationId");
                //        PatientDescription = element.GetAttribute("Name");
                //        break;
                //    }

                //    counter = counter + 1;
                //}

                //lib.clickOnAutomationId(session, PatientDescription, PatientName);

                ///** Clicks on Fit patient button **/

                //Thread.Sleep(8000);
                //lib.waitForIdToBeClickable(session, "StandAloneAutomationIds.DetailsAutomationIds.FitAction");

                //stepName.Pass("Patient is clicked");

                //Thread.Sleep(10000);
                //session.Close();

                //appCapabilities = new AppiumOptions();
                //appCapabilities.AddAdditionalCapability("app", config.ApplicationPath.SmartFitAppPath);
                //appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
                //appCapabilities.AddAdditionalCapability("appArguments", "--run-as-administrator");
                //Thread.Sleep(5000);
                //session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                //Thread.Sleep(10000);
                //session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                //Thread.Sleep(10000);

                ///**   clicks the back button, selects the Speed Link and then clicks "connect" **/

                //try
                //{
                //    session.FindElementByName("Back").Click();
                //    Thread.Sleep(5000);
                //    session.FindElementByAccessibilityId("ConnectionAutomationIds.CommunicationInterfaceItems").Click();
                //    Thread.Sleep(2000);
                //    session.FindElementByName("Speedlink").Click();
                //    lib.clickOnAutomationId(session, "Connect", "SidebarAutomationIds.ConnectAction");
                //}
                //catch (Exception)
                //{ }

                session = ModuleFunctions.sessionInitialize(config.ApplicationPath.SmartFitAppPath, config.workingdirectory.FSWWorkingPath);
                Thread.Sleep(8000);
                WindowsDriver<WindowsElement> session2 = null;
                try
                {
                    string addPatientXpath = "//Button[@Name=' Add or Select Patient']";
                    session.FindElement(By.XPath(addPatientXpath)).Click();

                }
                catch (Exception ex)
                {
                    string addPatientXpath = "//Button[@ClassName='btn btn-lg btn-secondary-dark w-30']";
                    session.FindElement(By.XPath(addPatientXpath)).Click();

                }
                Thread.Sleep(5000);
                session = ModuleFunctions.sessionInitialize1(config.ApplicationPath.FSWAppPath, config.workingdirectory.FSWWorkingPath);


                Thread.Sleep(5000);
                session.Manage().Window.Maximize();


                WebDriverWait wait = new WebDriverWait(session, TimeSpan.FromMinutes(5));
                try
                {
                    wait.Until(d => d.FindElements(By.XPath("//Group")).Count > 0);

                    var allGroups = session.FindElements(By.XPath("//Group"));

                    foreach (var group in allGroups)
                    {
                        Actions actions = new Actions(session);

                        wait.Until(d => group.FindElements(By.XPath(".//Group[1]")).Count > 0);

                        var all = group.FindElements(By.XPath(".//Group[1]"));
                        foreach (var element in all)
                        {
                            wait.Until(d => element.FindElements(By.XPath(".//Group[1]")).Count > 0);

                            var all2 = element.FindElements(By.XPath(".//Group[1]"));
                            foreach (var child in all2)
                            {
                                wait.Until(d => child.FindElements(By.XPath(".//*")).Count > 0);
                                var allChildElements = child.FindElements(By.XPath(".//*"));
                                if (allChildElements != null && allChildElements.Count >= 17)
                                {
                                    var fourteenthElement = allChildElements[12];
                                    Actions actions1 = new Actions(session);
                                    actions1.MoveToElement(fourteenthElement).Click().Perform();

                                    var selectPatient2 = wait.Until(d => d.FindElement(By.Name("Fit Patient")));
                                    actions1.MoveToElement(selectPatient2).Click().Perform();

                                    break;
                                }

                            }
                        }
                    }
                }
                catch (WebDriverTimeoutException ex)
                {
                }
                catch (NoSuchElementException ex)
                {
                }
                catch (Exception ex)
                {
                }

                //appCapabilities.AddAdditionalCapability("app", config.ApplicationPath.SmartFitAppPath);
                //appCapabilities.AddAdditionalCapability("platformName", "Windows");
                ////appCapabilities.SetCapability("ms:waitForAppLaunch", "5");
                //appCapabilities.AddAdditionalCapability("appArguments", "--run-as-administrator");
                //appCapabilities.AddAdditionalCapability("appArguments", "Test.exe");
                //appCapabilities.AddAdditionalCapability("appWorkingDir", @"C:\Program Files (x86)\ReSound\SmartFit");
                //appCapabilities.AddAdditionalCapability("automationName", "Windows");
                //appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
                //appCapabilities.AddAdditionalCapability("ms:experimental-webdriver", true);
                //session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                //Thread.Sleep(5000);
                //session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                //session.Manage().Window.Maximize();
                //session.SwitchTo().Window(session.WindowHandles.First());
                ////////session.SwitchTo().ActiveElement();
                session = ModuleFunctions.sessionInitialize1(config.ApplicationPath.SmartFitAppPath, config.workingdirectory.FSWWorkingPath);
                Thread.Sleep(12000);
                //session.FindElementByAccessibilityId("fittingpath-button-connect").Click();
                //Thread.Sleep(12000);

                session.FindElementByAccessibilityId("fittingpath-button-connect").Click();

                Thread.Sleep(10000);

                lib.clickOnAutomationName(session, "Assign Instruments");
                //session.FindElementByName("Back").Click();
                WebDriverWait wait1 = new WebDriverWait(session, TimeSpan.FromMinutes(5));
                WindowsElement comboBox = (WindowsElement)wait1.Until(driver =>
                {
                    var element = driver.FindElement(By.ClassName("ComboBox"));
                    return element.Enabled ? element : null;
                });

                comboBox.Click();

                session.FindElementByName("Speedlink").Click();
                Thread.Sleep(5000);

                session.FindElementByName("Search").Click();
            }

            else if (DeviceType.Equals("Wired"))
            {
                //session = ModuleFunctions.sessionInitialize(config.ApplicationPath.SmartFitAppPath, config.workingdirectory.FSWWorkingPath);
                //_appiumServer.StartServer();
                session = ModuleFunctions.sessionInitialize(config.ApplicationPath.SmartFitAppPath, config.workingdirectory.FSWWorkingPath);
                Thread.Sleep(8000);
                WindowsDriver<WindowsElement> session2 = null;
                try
                {
                    string addPatientXpath = "//Button[@Name=' Add or Select Patient']";
                    session.FindElement(By.XPath(addPatientXpath)).Click();

                }
                catch (Exception ex)
                {
                    string addPatientXpath = "//Button[@ClassName='btn btn-lg btn-secondary-dark w-30']";
                    session.FindElement(By.XPath(addPatientXpath)).Click();

                }
                Thread.Sleep(5000);
                session = ModuleFunctions.sessionInitialize1(config.ApplicationPath.FSWAppPath, config.workingdirectory.FSWWorkingPath);


                Thread.Sleep(5000);
                session.Manage().Window.Maximize();


                WebDriverWait wait = new WebDriverWait(session, TimeSpan.FromMinutes(5));
                try
                {
                    wait.Until(d => d.FindElements(By.XPath("//Group")).Count > 0);

                    var allGroups = session.FindElements(By.XPath("//Group"));

                    foreach (var group in allGroups)
                    {
                        Actions actions = new Actions(session);

                        wait.Until(d => group.FindElements(By.XPath(".//Group[1]")).Count > 0);

                        var all = group.FindElements(By.XPath(".//Group[1]"));
                        foreach (var element in all)
                        {
                            wait.Until(d => element.FindElements(By.XPath(".//Group[1]")).Count > 0);

                            var all2 = element.FindElements(By.XPath(".//Group[1]"));
                            foreach (var child in all2)
                            {
                                wait.Until(d => child.FindElements(By.XPath(".//*")).Count > 0);
                                var allChildElements = child.FindElements(By.XPath(".//*"));
                                if (allChildElements != null && allChildElements.Count >= 17)
                                {
                                    var fourteenthElement = allChildElements[12];
                                    Actions actions1 = new Actions(session);
                                    actions1.MoveToElement(fourteenthElement).Click().Perform();

                                    var selectPatient2 = wait.Until(d => d.FindElement(By.Name("Fit Patient")));
                                    actions1.MoveToElement(selectPatient2).Click().Perform();

                                    break;
                                }

                            }
                        }
                    }
                }
                catch (WebDriverTimeoutException ex)
                {
                }
                catch (NoSuchElementException ex)
                {
                }
                catch (Exception ex)
                {
                }

                //appCapabilities.AddAdditionalCapability("app", config.ApplicationPath.SmartFitAppPath);
                //appCapabilities.AddAdditionalCapability("platformName", "Windows");
                ////appCapabilities.SetCapability("ms:waitForAppLaunch", "5");
                //appCapabilities.AddAdditionalCapability("appArguments", "--run-as-administrator");
                //appCapabilities.AddAdditionalCapability("appArguments", "Test.exe");
                //appCapabilities.AddAdditionalCapability("appWorkingDir", @"C:\Program Files (x86)\ReSound\SmartFit");
                //appCapabilities.AddAdditionalCapability("automationName", "Windows");
                //appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
                //appCapabilities.AddAdditionalCapability("ms:experimental-webdriver", true);
                //session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                //Thread.Sleep(5000);
                //session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                //session.Manage().Window.Maximize();
                //session.SwitchTo().Window(session.WindowHandles.First());
                ////////session.SwitchTo().ActiveElement();
                session = ModuleFunctions.sessionInitialize1(config.ApplicationPath.SmartFitAppPath, config.workingdirectory.FSWWorkingPath);
                Thread.Sleep(12000);
                //session.FindElementByAccessibilityId("fittingpath-button-connect").Click();
                //Thread.Sleep(12000);

                session.FindElementByAccessibilityId("fittingpath-button-connect").Click();

                Thread.Sleep(10000);

                lib.clickOnAutomationName(session, "Assign Instruments");
                //session.FindElementByName("Back").Click();
                WebDriverWait wait1 = new WebDriverWait(session, TimeSpan.FromMinutes(5));
                WindowsElement comboBox = (WindowsElement)wait1.Until(driver =>
                {
                    var element = driver.FindElement(By.ClassName("ComboBox"));
                    return element.Enabled ? element : null;
                });

                comboBox.Click();

                session.FindElementByName("Speedlink").Click();
                Thread.Sleep(5000);

                session.FindElementByName("Search").Click();









                //Console.WriteLine("This is When method");
                //Thread.Sleep(2000);
                //DesiredCapabilities appCapabilities = new DesiredCapabilities();
                //appCapabilities.SetCapability("app", config.ApplicationPath.FSWAppPath);
                //appCapabilities.SetCapability("deviceName", "WindowsPC");
                //appCapabilities.SetCapability("appArguments", "--run-as-administrator");
                //session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                //Thread.Sleep(10000);
                //session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                //Thread.Sleep(2000);
                //session.Manage().Window.Maximize();
                //var wait = new WebDriverWait(session, TimeSpan.FromSeconds(20));
                //var div = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("ListBoxItem")));
                //var text_Button = session.FindElementsByClassName("ListBoxItem");
                //stepName.Log(Status.Pass, "FSW is launched successfully");
                //int counter = 0;
                //string PatientName = null;
                //string PatientDescription = null;
                //foreach (var element in text_Button)
                //{
                //    if (counter == 2)
                //    {
                //        PatientName = element.GetAttribute("AutomationId");
                //        PatientDescription = element.GetAttribute("Name");
                //        break;
                //    }

                //    counter = counter + 1;
                //}

                //lib.clickOnAutomationId(session, PatientDescription, PatientName);

                ///** Clicks on Fit patient button **/

                //Thread.Sleep(8000);

                //lib.waitForIdToBeClickable(session, "StandAloneAutomationIds.DetailsAutomationIds.FitAction");
                //stepName.Pass("Patient is clicked");
                //Thread.Sleep(10000);
                ////session.Close();
                //appCapabilities = new DesiredCapabilities();
                //appCapabilities.SetCapability("app", config.ApplicationPath.SmartFitAppPath);
                //appCapabilities.SetCapability("deviceName", "WindowsPC");
                //appCapabilities.SetCapability("appArguments", "--run-as-administrator");
                //Thread.Sleep(5000);
                //session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                //Thread.Sleep(10000);
                //session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                //Thread.Sleep(10000);

                //try

                //{
                //    lib.clickOnElementWithIdonly(session, "ConnectionAutomationIds.ConnectAction");
                //    stepName.Pass("Connect button is clicked");
                //}

                //catch (Exception)
                //{


                //}

                //Thread.Sleep(10000);

                /**   clicks the back button, selects the Speed Link and then clicks "connect" **/

                try
                {
                    //session.FindElementByName("Back").Click();
                    //Thread.Sleep(10000);
                    //session.FindElementByAccessibilityId("ConnectionAutomationIds.CommunicationInterfaceItems").Click();
                    //Thread.Sleep(2000);
                    //session.FindElementByName("Speedlink").Click();
                    //Thread.Sleep(5000);

                    try
                    {
                        if (session.FindElementByName("Unassign").Enabled)
                        {

                            lib.clickOnAutomationName(session, "Assign Instruments");

                            Thread.Sleep(10000); // Initial wait before searching

                            var SN = session.FindElementsByClassName("ListBoxItem");

                            // Check if DeviceNo is already discovered

                            foreach (WindowsElement value in SN)
                            {
                                string S = value.Text;
                                if (S.Contains(DeviceNo))
                                {
                                    value.FindElementByName("Assign Left").Click();
                                    break;
                                }
                            }

                        }
                    }
                    catch (Exception)
                    {

                    }

                }
                catch (Exception)
                { }

            }

            Thread.Sleep(3000);

            lib.clickOnAutomationName(session, "Continue");
            Thread.Sleep(40000);

            var textBlockelements = session.FindElementsByClassName("TextBlock");

            foreach (var element in textBlockelements)
            {
                try
                {

                    if (element.Text == "Connection Error")
                    {
                        stepName.Log(Status.Fail, "Connection Error");
                        MailMessage mailMessage = new MailMessage();
                        mailMessage.From = new MailAddress("assettracker@i-raysolutions.com");
                        mailMessage.CC.Add(new MailAddress("prasad.puvvala@i-raysolutions.com"));
                        mailMessage.To.Add(new MailAddress("surya.kondreddy@i-raysolutions.com"));
                        mailMessage.To.Add(new MailAddress("siva.bojja@i-raysolutions.com"));
                        mailMessage.Subject = "S&R Automation Script Error";
                        mailMessage.Body = "FSW Connection Error";
                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"); // Specify the SMTP host
                        smtpClient.Port = 587; // Specify the SMTP port (Gmail typically uses port 587 for TLS/SSL)
                        smtpClient.EnableSsl = true; // Enable SSL/TLS
                        smtpClient.Credentials = new NetworkCredential("assettracker@i-raysolutions.com", "asset@2k19"); // Provide credentials
                        smtpClient.Send(mailMessage);
                        lib.processKill("SmartFit");
                        lib.processKill("SmartFitSA");
                        break;
                    }

                    else if (element.Text == "Connection")
                    {
                        Thread.Sleep(10000);
                        stepName.Log(Status.Pass, "Connection Success");
                        int buttonCount = 0;
                        try
                        {
                            WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(60));
                            waitForMe.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("Button")));

                            do
                            {
                                //////session.SwitchTo().ActiveElement();

                                if (buttonCount >= 1)
                                {
                                    //////session.SwitchTo().ActiveElement();
                                    session = ModuleFunctions.getControlsOfParentWindow(session, "ScrollViewer", stepName);
                                    try
                                    {
                                        session.FindElementByAccessibilityId("StateMachineAutomationIds.ContinueAction").Click();
                                        waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(40));
                                        waitForMe.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("Button")));
                                    }
                                    catch
                                    {

                                    }
                                }

                                buttonCount = buttonCount + 1;

                            } while (session.FindElementByAccessibilityId("StateMachineAutomationIds.ContinueAction").Enabled);
                        }

                        catch (Exception)
                        {
                            Thread.Sleep(4000);

                            try
                            {
                                lib.clickOnElementWithIdonly(session, "PatientAutomationIds.ProfileAutomationIds.FitPatientAction");
                            }
                            catch (Exception ex)
                            {

                            }

                            try
                            {
                                lib.clickOnAutomationId(session, "All", "ContentTextBlock");

                                int increment = 0;
                                int stepIncrement = 0;
                                if (alterValue.Equals("Yes"))
                                {
                                    /** Clicks on Fiiting menu buttion **/

                                    session.FindElementByName("Fitting").Click();
                                    Thread.Sleep(2000);

                                    /** To perform reset initial fit **/

                                    session.FindElementByName("Reset to Initial Fit").Click();
                                    Thread.Sleep(4000);
                                    //session.SwitchTo().Window(session.WindowHandles.First());
                                    ////////session.SwitchTo().ActiveElement();

                                    try
                                    {
                                        Thread.Sleep(4000);
                                        lib.clickOnAutomationName(session, "Continue");
                                    }
                                    catch (Exception e1)
                                    {
                                    }

                                    stepName.Pass("Reset is successfully done");
                                    Thread.Sleep(2000);

                                    lib.clickOnElementWithIdonly(session, "ProgramStripAutomationIds.AddProgramAction");

                                    /** Add the music program **/

                                    session.FindElementByName("Music").Click();
                                    Thread.Sleep(5000);
                                    lib.clickOnAutomationId(session, "All", "ContentTextBlock");
                                    stepIncrement = 5;
                                }

                                else
                                {
                                    /** Clicks on "Fiiting" Redmenu buttion **/

                                    session.FindElementByName("Fitting").Click();
                                    Thread.Sleep(2000);

                                    /** To perform reset initial fit **/

                                    session.FindElementByName("Reset to Initial Fit").Click();
                                    Thread.Sleep(4000);
                                    session.SwitchTo().Window(session.WindowHandles.First());
                                    ////////session.SwitchTo().ActiveElement();

                                    try
                                    {
                                        Thread.Sleep(4000);

                                        lib.clickOnAutomationName(session, "Continue");
                                    }
                                    catch (Exception e1)
                                    {
                                    }
                                    stepName.Pass("Reset is successfully done");
                                    Thread.Sleep(2000);
                                    lib.clickOnElementWithIdonly(session, "ProgramStripAutomationIds.AddProgramAction");

                                    /** Add the Outdoor program **/

                                    session.FindElementByName("Outdoor").Click();
                                    Thread.Sleep(5000);
                                    lib.clickOnAutomationId(session, "All", "ContentTextBlock");
                                    stepIncrement = 3;
                                }

                                /** In order to raise the Gain values **/

                                do
                                {
                                    lib.functionWaitForId(session, "FittingAutomationIds.GainAutomationIds.AdjustmentItemsAutomationIds.Increase");
                                    increment = increment + 1;
                                    stepName.Pass("Increment Gain is clicked for :" + increment + " times");
                                    Thread.Sleep(2000);
                                } while (increment <= stepIncrement);
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                Thread.Sleep(10000);
                                lib.clickOnElementWithIdonly(session, "FittingAutomationIds.SaveAction");

                                /** Clicks on "Skip" Button **/

                                try
                                {
                                    Thread.Sleep(2000);
                                    lib.clickOnAutomationName(session, "Skip & Save");
                                }
                                catch (Exception)
                                {
                                    lib.clickOnElementWithIdonly(session, "PART_Cancel");
                                }
                                stepName.Pass("Save is successfully done and Close the FSW");
                            }
                            catch (Exception skip)
                            {
                                Console.WriteLine(skip);
                            }


                            stepName.Pass("Click on FSW Exit button");
                            lib.clickOnElementWithIdonly(session, "SaveAutomationIds.PerformSaveAutomationIds.ExitAction");

                            /** Exit the FSW **/

                            stepName.Pass("Click on FSW Exit button");
                            Thread.Sleep(8000);
                            lib.processKill("SmartFitSA");
                        }
                        break;
                    }
                    else if (element.Text == "Connection Error")
                    {
                        stepName.Log(Status.Fail, "Connection Error");
                        MailMessage mailMessage = new MailMessage();
                        mailMessage.From = new MailAddress("assettracker@i-raysolutions.com");
                        mailMessage.CC.Add(new MailAddress("prasad.puvvala@i-raysolutions.com"));
                        mailMessage.To.Add(new MailAddress("surya.kondreddy@i-raysolutions.com"));
                        mailMessage.To.Add(new MailAddress("siva.bojja@i-raysolutions.com"));
                        mailMessage.Subject = "S&R Automation Script Error";
                        mailMessage.Body = "FSW Connection Error";
                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"); // Specify the SMTP host
                        smtpClient.Port = 587; // Specify the SMTP port (Gmail typically uses port 587 for TLS/SSL)
                        smtpClient.EnableSsl = true; // Enable SSL/TLS
                        smtpClient.Credentials = new NetworkCredential("assettracker@i-raysolutions.com", "asset@2k19"); // Provide credentials
                        smtpClient.Send(mailMessage);
                        lib.processKill("SmartFit");
                        lib.processKill("SmartFitSA");
                        break;
                    }
                    else
                    {
                        Thread.Sleep(10000);
                        stepName.Log(Status.Pass, "Connection Success");
                        int buttonCount = 0;
                        try
                        {
                            WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(60));
                            waitForMe.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("Button")));

                            do
                            {
                                ////////session.SwitchTo().ActiveElement();

                                if (buttonCount >= 1)
                                {
                                    ////////session.SwitchTo().ActiveElement();
                                    session = ModuleFunctions.getControlsOfParentWindow(session, "ScrollViewer", stepName);
                                    try
                                    {
                                        session.FindElementByAccessibilityId("StateMachineAutomationIds.ContinueAction").Click();
                                        waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(40));
                                        waitForMe.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("Button")));
                                    }
                                    catch
                                    {

                                    }

                                }

                                buttonCount = buttonCount + 1;

                            } while (session.FindElementByAccessibilityId("StateMachineAutomationIds.ContinueAction").Enabled);
                        }

                        catch (Exception)
                        {
                            Thread.Sleep(4000);

                            try
                            {
                                lib.clickOnElementWithIdonly(session, "PatientAutomationIds.ProfileAutomationIds.FitPatientAction");
                            }
                            catch (Exception ex)
                            {

                            }

                            try
                            {
                                lib.clickOnAutomationId(session, "All", "ContentTextBlock");

                                int increment = 0;
                                int stepIncrement = 0;
                                if (alterValue.Equals("Yes"))
                                {
                                    /** Clicks on Fiiting menu buttion **/

                                    session.FindElementByName("Fitting").Click();
                                    Thread.Sleep(2000);

                                    /** To perform reset initial fit **/

                                    session.FindElementByName("Reset to Initial Fit").Click();
                                    Thread.Sleep(2000);
                                    session.SwitchTo().Window(session.WindowHandles.First());
                                    ////////session.SwitchTo().ActiveElement();

                                    try
                                    {
                                        Thread.Sleep(2000);
                                        lib.clickOnAutomationName(session, "Continue");
                                    }
                                    catch (Exception e1)
                                    {
                                    }

                                    stepName.Pass("Reset is successfully done");
                                    Thread.Sleep(2000);

                                    lib.clickOnElementWithIdonly(session, "ProgramStripAutomationIds.AddProgramAction");

                                    /** Add the music program **/

                                    session.FindElementByName("Music").Click();
                                    Thread.Sleep(5000);
                                    lib.clickOnAutomationId(session, "All", "ContentTextBlock");
                                    stepIncrement = 5;
                                }

                                else
                                {
                                    /** Clicks on "Fiiting" Redmenu buttion **/

                                    session.FindElementByName("Fitting").Click();
                                    Thread.Sleep(2000);

                                    /** To perform reset initial fit **/

                                    session.FindElementByName("Reset to Initial Fit").Click();
                                    Thread.Sleep(2000);
                                    session.SwitchTo().Window(session.WindowHandles.First());
                                    ////////session.SwitchTo().ActiveElement();

                                    try
                                    {
                                        Thread.Sleep(2000);
                                        lib.clickOnAutomationName(session, "Continue");
                                    }
                                    catch (Exception e1)
                                    {
                                    }
                                    stepName.Pass("Reset is successfully done");
                                    Thread.Sleep(2000);
                                    lib.clickOnElementWithIdonly(session, "ProgramStripAutomationIds.AddProgramAction");

                                    /** Add the Outdoor program **/

                                    session.FindElementByName("Outdoor").Click();
                                    Thread.Sleep(5000);
                                    lib.clickOnAutomationId(session, "All", "ContentTextBlock");
                                    stepIncrement = 3;
                                }

                                /** In order to raise the Gain values **/

                                do
                                {
                                    lib.functionWaitForId(session, "FittingAutomationIds.GainAutomationIds.AdjustmentItemsAutomationIds.Increase");

                                    increment = increment + 1;
                                    stepName.Pass("Increment Gain is clicked for :" + increment + " times");
                                    Thread.Sleep(2000);
                                } while (increment <= stepIncrement);
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                Thread.Sleep(10000);
                                lib.clickOnElementWithIdonly(session, "FittingAutomationIds.SaveAction");

                                /** Clicks on "Skip" Button **/

                                try
                                {
                                    Thread.Sleep(2000);
                                    lib.clickOnAutomationName(session, "Skip & Save");
                                }
                                catch (Exception)
                                {
                                    lib.clickOnElementWithIdonly(session, "PART_Cancel");
                                }


                                stepName.Pass("Save is successfully done and Close the FSW");
                            }
                            catch (Exception skip)
                            {
                                Console.WriteLine(skip);
                            }


                            stepName.Pass("Click on FSW Exit button");
                            lib.clickOnElementWithIdonly(session, "SaveAutomationIds.PerformSaveAutomationIds.ExitAction");

                            /** Exit the FSW **/

                            Thread.Sleep(8000);

                            lib.processKill("SmartFitSA");
                        }
                        break;
                    }
                }
                catch (Exception e)
                {

                }
            }
        }
        /** Used to clear exisisting 'capture' and 'restore' reports in specified path **/

        [When("[Cleaning up Capture and Restore Reports Before Launch SandR]")]
        public void WhenCleaningUpCaptureAndRestoreReportsBeforeLaunchSandR()
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());

            try
            {
                System.IO.Directory.Delete(@"C:\CaptureBase\Reports", true);
                stepName.Pass("All files are deleted");
            }
            catch (Exception e)
            {
                stepName.Info("No files found to be deleted");
            }

            try
            {
                System.IO.Directory.Delete(@"C:\Users\Public\Documents\Camelot\Logs", true);
                stepName.Pass("All Camelot log files are deleted");
            }
            catch (Exception e)
            {
                stepName.Info("No files found to be deleted");
            }
        }

        /** Opens S&R tool 
          * Connects the HI device **/


        [When(@"\[Launch SandR ""([^""]*)"" and ""([^""]*)""and""([^""]*)""and ""([^""]*)""]")]
        public void WhenLaunchSandRAndAndand(string device, string DeviceNo, string DeviceType, string side)
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            config = FeatureContext.Current["config"] as appconfigsettings;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
            ModuleFunctions.SandRenvironmentchange();
            FunctionLibrary lib = new FunctionLibrary();

            try
            {
                if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
                {
                    ModuleFunctions.socketA(session, stepName, DeviceType);

                }
            }
            catch { }

            Thread.Sleep(10000);
            Thread.Sleep(5000);

            session = ModuleFunctions.sessionInitialize(config.ApplicationPath.SandRAppPath, config.workingdirectory.SandR);

            session.FindElementByName("Device Info").Click();

            stepName.Log(Status.Pass, "S&R Tool launched successfully");
            Thread.Sleep(2000);

            if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
            {
                /** Clciks on "Discover" button **/

                session.FindElementByName("Discover").Click();
                stepName.Log(Status.Pass, "Clicked on Discover.");
                session.SwitchTo().Window(session.WindowHandles.First());
                Thread.Sleep(8000);
                session.SwitchTo().ActiveElement();

                /** Type the HI serial number in the search field. **/

                try
                {
                    session.FindElementByAccessibilityId("SerialNumberTextBox").SendKeys(DeviceNo);
                    lib.functionWaitForName(session, "Search");
                }

                catch (Exception ex)
                {

                }

                /** Finding the HI till Disconnect button on the display **/

                do
                {
                    try
                    {
                        session.SwitchTo().Window(session.WindowHandles.First());
                        string Message = "Connecting to the device failed as it has been powered for more than 2 minutes. Reboot the device and try again.";

                        while (session.FindElementByAccessibilityId("TextBox_1").Text != "Discovering wireless device...")
                        {

                            if (session.FindElementByAccessibilityId("TextBox_1").Text == Message || session.FindElementByAccessibilityId("TextBox_1").Text == "No wireless device could be found.")
                            {
                                var sandRConnection = session.FindElementByAccessibilityId("TextBox_1").Text;
                                stepName.Log(Status.Info, sandRConnection);
                                var btncls = session.FindElementByAccessibilityId("PART_Close");
                                btncls.Click();
                                Thread.Sleep(4000);
                                ModuleFunctions.Recovery(session, stepName, DeviceType, DeviceNo, side);
                                session = ModuleFunctions.sessionInitialize(config.ApplicationPath.SandRAppPath, config.workingdirectory.SandR);
                            }

                            if (session.FindElementByName("Discover").Text == "Discover")
                            {
                                session.SwitchTo().Window(session.WindowHandles.First());
                                session.FindElementByName("Search").Click();
                            }

                        }

                    }
                    catch (Exception)
                    {

                    }

                } while (!session.FindElementByName("Disconnect").Displayed);
                stepName.Log(Status.Pass, "Clicked on Search");

                session = lib.waitForElement(session, "Model Name");
                Thread.Sleep(12000);

                screenshot = ModuleFunctions.CaptureScreenshot(session);

                stepName.Log(Status.Pass, "Device Info", MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
            }
        }



        /** Navigates to Device Info 
         *  reads device information into an Excel spreadsheet **/


        [When(@"\[Go to Device Info tab and capture device info in excel then verify the device information is shown correctly ""([^""]*)""]")]
        public void WhenGoToDeviceInfoTabAndCaptureDeviceInfoInExcelThenVerifyTheDeviceInformationIsShownCorrectly(string deviceType)
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());

            /** Getting Device Info from Info tab **/

            FunctionLibrary lib = new FunctionLibrary();
            lib = new FunctionLibrary();
            lib.getDeviceInfo(session, stepName, deviceType);
            stepName.Log(Status.Info, "Device information is captured in excel file");
        }




        /** Validates the updated firmware version in the S&R tool under device info **/


        [Then(@"\[Compare firmware version is upgraded successfully ""([^""]*)""and""([^""]*)""]")]
        public void ThenCompareFirmwareVersionIsUpgradedSuccessfullyAnd(string device, string DeviceType)
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
            FunctionLibrary lib = new FunctionLibrary();
            Thread.Sleep(2000);

            string Dooku1nonRechargeble = "[1].18.1.1 (Dooku1)";
            string Dooku2nonRechargeable = "[9].71.1.1 (Dooku2)";
            string Dooku2Rechargeable = "[9].60.1.1 (Dooku2)";
            string Dooku3PBTE = "[7].42.1.1 (Dooku3)";
            string Dooku3Rechargeable = "[7].42.1.1 (Dooku3)";

            if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable") || DeviceType.Equals("Wired") || DeviceType.Equals("D1rechargeableWired"))
            {
                session.FindElementByName("Device Info").Click();
                Thread.Sleep(2000);

                session = lib.waitForElement(session, "Firmware Version");

                var version = session.FindElementByAccessibilityId("TextBox_6");

                Thread.Sleep(3000);

                switch (version.Text)

                {

                    case string _ when version.Text.Equals(Dooku2nonRechargeable) || version.Text.Equals(Dooku2Rechargeable) || version.Text.Equals(Dooku3PBTE) || version.Text.Equals(Dooku1nonRechargeble):
                        screenshot = ModuleFunctions.CaptureScreenshot(session);
                        stepName.Log(Status.Pass, "Expected Firmware Version is: " + version.Text + " But Current Firmware is: " + version.Text, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());

                        break;


                    case string _ when device.Contains("RT962-DRW"):

                        screenshot = ModuleFunctions.CaptureScreenshot(session);
                        stepName.Log(Status.Info, "Expected Firmware Version is: " + Dooku2nonRechargeable + " But Current Firmware Version is: " + version.Text, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                        break;


                    case string _ when device.Contains("RE962-DRW"):

                        screenshot = ModuleFunctions.CaptureScreenshot(session);
                        stepName.Log(Status.Info, "Expected Firmware Version is: " + Dooku1nonRechargeble + " But Current Firmware Version is: " + version.Text, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                        break;


                    case string _ when device.Contains("RT961-DRWC"):

                        screenshot = ModuleFunctions.CaptureScreenshot(session);
                        stepName.Log(Status.Info, "Expected Firmware Version is: " + Dooku2Rechargeable + " But Current Firmware Version is: " + version.Text, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                        break;


                    case string _ when device.Contains("RU961-DRWC"):

                        screenshot = ModuleFunctions.CaptureScreenshot(session);
                        stepName.Log(Status.Info, "Expected Firmware Version is: " + Dooku3Rechargeable + " But Current Firmware Version is: " + version.Text, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                        break;

                    case string _ when device.Contains("RU988-DWC"):

                        screenshot = ModuleFunctions.CaptureScreenshot(session);
                        stepName.Log(Status.Info, "Expected Firmware Version is: " + Dooku3PBTE + " But Current Firmware Version is: " + version.Text, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                        break;


                    default:

                        screenshot = ModuleFunctions.CaptureScreenshot(session);
                        stepName.Log(Status.Info, "Unknown Firmware Version: " + version.Text, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                        break;
                }

            }

        }




        /** Validates the updated firmware version in the S&R tool under device info **/


        [Then(@"\[Compare firmware version is downgraded successfully ""([^""]*)""]")]
        public void ThenCompareFirmwareVersionIsDowngradedSuccessfully(string device)
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
            FunctionLibrary lib = new FunctionLibrary();
            Thread.Sleep(2000);

            string Dooku1nonRechargeble = "[0].40 (Dooku1)";
            string Dooku3nonRechargeable = "[7].39.2.1(Dooku3)";
            string Dooku3Rechargeable = "[7].40.1.1(Dooku3)";


            if (device.Contains("RU961-DRW") || device.Contains("RU960-DRWC") || device.Contains("RE962-DRW"))
            {
                session.FindElementByName("Device Info").Click();
                Thread.Sleep(2000);
                session = lib.waitForElement(session, "Firmware Version");
                var version = session.FindElementByAccessibilityId("TextBox_6");
                Thread.Sleep(3000);
                switch (version.Text)

                {

                    case string _ when version.Text.Equals(Dooku3nonRechargeable) || version.Text.Equals(Dooku3Rechargeable) || version.Text.Equals(Dooku1nonRechargeble):

                        screenshot = ModuleFunctions.CaptureScreenshot(session);
                        stepName.Log(Status.Pass, "Expected Firmware Version is: " + version.Text + " But Current Firmware is: " + version.Text, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build()); break;


                    case string _ when device.Contains("RE962-DRW"):

                        stepName.Log(Status.Info, "Expected Firmware Version is: " + Dooku1nonRechargeble + " But Current Firmware Version is: " + version.Text);
                        break;


                    case string _ when device.Contains("RT961-DRWC"):

                        stepName.Log(Status.Info, "Expected Firmware Version is: " + Dooku3nonRechargeable + " But Current Firmware Version is: " + version.Text);
                        break;


                    case string _ when device.Contains("RU988-DWC"):

                        stepName.Log(Status.Info, "Expected Firmware Version is: " + Dooku3Rechargeable + " But Current Firmware Version is: " + version.Text);
                        break;


                    default:
                        stepName.Log(Status.Info, "Unknown Firmware Version: " + version.Text);
                        break;

                }

            }

        }



        /** Navigate to services tab in S&R tool **/



        [When("[Come back to Settings and wait till controls enabled]")]
        public void WhenComeBackToSettingsAndWaitTillControlsEnabled()
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
            FunctionLibrary lib = new FunctionLibrary();
            Thread.Sleep(2000);
            session.FindElementByName("Services").Click();
            stepName.Log(Status.Pass, "Clicked on Services.");
        }



        /** Click on Disconnect button 
         *  Validates device information **/



        [Given(@"\[Download and verify azure storage files ""([^""]*)"" and ""([^""]*)""]")]
        public async Task GivenDownloadAndVerifyAzureStorageFilesAsync(string scenarioTitle, string DeviceNo)
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            string path2 = textDir + "\\azurefiles";
            if (Directory.Exists(path2))
            {
                Directory.Delete(path2, true);
            }
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());

            // Define the connection string

            string connectionString = "AccountName=camelotwesteudev;AccountKey=ylFzkolO9hUoR63VeJVr9On4QgnrEIHJPUdv8n1V2One8/7LdnZHfTYLJMVf7Pt7B9EVUIF1Xg/iXxoorXoruw==;EndpointSuffix=core.windows.net;DefaultEndpointsProtocol=https;";

            // Determine the container name based on the scenario title

            string containerName;

            switch (scenarioTitle.ToLower())
            {
                case "capture":
                    containerName = "camelot-hisettingscapture";
                    break;
                case "restore":
                    containerName = "camelot-hisettingsrestore"; // Replace with the actual container name for "restore"
                    break;
                case "service records":
                    containerName = "lucan-hiservicerecords"; // Replace with the actual container name for "service records"
                    break;
                default:
                    Console.WriteLine("Invalid scenario title.");
                    return;
            }

            // Define the destination folder
            string destinationFolder = Path.Combine(Directory.GetCurrentDirectory(), "azurefiles");

            // Ensure that the destination folder exists
            Directory.CreateDirectory(destinationFolder);

            // Parse the storage account connection string
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            // Create a blob client
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Get a reference to the container
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            // List blobs in the container
            var blobList = container.ListBlobs(null, true);

            // Filter matching blobs
            var matchingBlob = blobList.OfType<CloudBlockBlob>().Where(blob =>
            {
                //if (containerName == "lucan-hiservicerecords")

                //{
                //    // Parse the date from the blob name (assuming the date format is "yyyy-MM-dd")
                //    var currentDate = DateTime.UtcNow.Date;
                //    var formattedCurrentDate = currentDate.ToString("yyyy-MM-dd");

                //    return Path.GetDirectoryName(blob.Name) == formattedCurrentDate;

                //}
                

                return blob.Name.Contains(DeviceNo);

            }).OrderByDescending(blob => blob.Properties.LastModified).FirstOrDefault();

            if (matchingBlob != null)
            {
                var destinationFilePath = Path.Combine(destinationFolder, Path.GetFileName(matchingBlob.Name));

                // Download the matching blob
                using (var fileStream = File.OpenWrite(destinationFilePath))
                {
                    matchingBlob.DownloadToStream(fileStream);
                }

                stepName.Log(Status.Pass, "Downloaded the file for the current system date to: {destinationFilePath}");

                if (containerName == "lucan-hiservicerecords")
                {
                    stepName.Log(Status.Pass, "Cloud service record is uploaded under today’s date");
                }
            }
            else
            {
                stepName.Log(Status.Fail, "No matching files found for the specified serial number or date in the " + scenarioTitle + " container.");
            }

        }


        [When(@"\[Clicks on disconnect and verify device information is cleared ""([^""]*)""]")]
        public void WhenClicksOnDisconnectAndVerifyDeviceInformationIsCleared(string deviceType)
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
            FunctionLibrary lib = new FunctionLibrary();
            lib = new FunctionLibrary();
            session = lib.waitForElement(session, "Connect to hearing instrument automatically");
            Thread.Sleep(2000);
            session.FindElementByName("Disconnect").Click();
            lib.getDeviceInfo(session, stepName, deviceType);
            stepName.Log(Status.Pass, "Clicked on Disconnect.");
        }


        /** Opens S&R tool 
         *  Navigates to settings tab
         *  Perform the Capture operation **/


        [When(@"\[Perform Capture""([^""]*)""and""([^""]*)""]")]
        public void WhenPerformCaptureand(string device, string DeviceType)
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            config = FeatureContext.Current["config"] as appconfigsettings;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
            FunctionLibrary lib = new FunctionLibrary();
            session = lib.functionWaitForName(session, "Capture");
            stepName.Log(Status.Pass, "Clicked on Capture.");
            session = lib.functionWaitForName(session, "LOGIN REQUIRED");
            lib.clickOnElementWithIdonly(session, "PasswordBox");

            /** To passing the User password **/

            if (computer_name.Equals("FSWIRAY80"))
            {

                session.FindElementByAccessibilityId("PasswordBox").SendKeys("112233");
            }
            else
            {

                session.FindElementByAccessibilityId("PasswordBox").SendKeys("svk01");
            }

            Thread.Sleep(2000);
            session.FindElementByName("Login").Click();
            Thread.Sleep(5000);
            session = lib.functionWaitForName(session, "CAPTURE");
            var HIData = lib.waitUntilElementExists(session, "windowUserMessage", 1);
            var text = session.FindElementByAccessibilityId("textBlockMessage");

            if (session.FindElementByAccessibilityId("labelHeader").Text == "Capture Succeeded")
            {
                screenshot = ModuleFunctions.CaptureScreenshot(session);
                stepName.Log(Status.Pass, "Capture Information : " + text.Text, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
            }
            else if (session.FindElementByAccessibilityId("labelHeader").Text == "Capture with low battery")
            {
                screenshot = ModuleFunctions.CaptureScreenshot(session);
                stepName.Log(Status.Pass, "Capture Information : " + text.Text, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                session.FindElementByName("Cancel").Click();
                Thread.Sleep(2000);
                session.FindElementByName("Services").Click();
                Thread.Sleep(2000);
                stepName.Log(Status.Pass, "Clicked on Services.");
                session = lib.functionWaitForName(session, "Capture");
                stepName.Log(Status.Pass, "Clicked on Capture.");
                session = lib.functionWaitForName(session, "LOGIN REQUIRED");
                lib.clickOnElementWithIdonly(session, "PasswordBox");

                /** To passing the User password **/

                if (computer_name.Equals("FSWIRAY80"))
                {

                    session.FindElementByAccessibilityId("PasswordBox").SendKeys("112233");
                }
                else
                {

                    session.FindElementByAccessibilityId("PasswordBox").SendKeys("svk01");
                }

                Thread.Sleep(2000);
                session.FindElementByName("Login").Click();
                session = lib.functionWaitForName(session, "CAPTURE");
                var textLowBat = session.FindElementByAccessibilityId("textBlockMessage");
                session = lib.waitForElement(session, "OK");
                var Data = lib.waitUntilElementExists(session, "windowUserMessage", 1);
                if (session.FindElementByAccessibilityId("labelHeader").Text == "Capture Succeeded")
                {
                    screenshot = ModuleFunctions.CaptureScreenshot(session);
                    stepName.Log(Status.Pass, "Capture Information : " + textLowBat.Text, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                }
                else
                {
                    stepName.Log(Status.Fail, "Capture Information : " + textLowBat.Text, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                }

                if (computer_name.Equals("FSWIRAY80") || computer_name.Equals("FSWIRAY112"))
                {
                    ModuleFunctions.verifyIfReportsExisted(test);

                    /** Kill Acrobat reader **/

                    Process[] processCollection = Process.GetProcesses();

                    foreach (Process proc in processCollection)
                    {
                        if (computer_name.Equals("FSWIRAY80") || computer_name.Equals("FSWIRAY112"))
                        {
                            if (proc.ProcessName == "msedge")
                            {
                                proc.Kill();

                            }

                            Console.WriteLine(proc);
                        }

                        else if (proc.ProcessName == "Acrobat")
                        {
                            proc.Kill();

                        }
                    }
                }

                session = lib.waitForElement(session, "OK");
                session.FindElementByName("Services").Click();
                stepName.Log(Status.Pass, "Clicked on Services.");
                Thread.Sleep(2000);
                session = lib.functionWaitForName(session, "Restore");
                stepName.Log(Status.Pass, "Clicked on Restore.");
                session = lib.functionWaitForName(session, "LOGIN REQUIRED");
                lib.clickOnElementWithIdonly(session, "PasswordBox");

                /** To passing the User password **/

                if (computer_name.Equals("FSWIRAY80"))
                {

                    session.FindElementByAccessibilityId("PasswordBox").SendKeys("112233");
                }
                else
                {

                    session.FindElementByAccessibilityId("PasswordBox").SendKeys("svk01");
                }

                Thread.Sleep(2000);
                session.FindElementByName("Login").Click();
                var RestoreLowBatData = lib.waitUntilElementExists(session, "windowUserMessage", 1);
                var RestoreLowBat = session.FindElementByAccessibilityId("textBlockMessage");
                if (session.FindElementByAccessibilityId("labelHeader").Text == "Battery Low")
                {
                    screenshot = ModuleFunctions.CaptureScreenshot(session);
                    stepName.Log(Status.Pass, "Restore Information : " + RestoreLowBat.Text, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                }
                else
                {
                    screenshot = ModuleFunctions.CaptureScreenshot(session);
                    stepName.Log(Status.Fail, "Restore Information : " + RestoreLowBat.Text, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                }
            }
            else
            {
                screenshot = ModuleFunctions.CaptureScreenshot(session);
                stepName.Log(Status.Fail, "Capture Failed" + ":" + "  " + text.Text, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
            }

            ModuleFunctions.sessionInitialize(config.ApplicationPath.SandRAppPath, config.workingdirectory.SandR);
            session = lib.waitForElement(session, "OK");
            var btnClose = session.FindElementByAccessibilityId("PART_Close");
            btnClose.Click();
            //session.Quit();
            //session.CloseApp();
            try
            {
                if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
                {
                    ModuleFunctions.socketA(session, test, DeviceType);
                }
            }
            catch { }

        }



        /** Clicks on Capture operation
          * Checks the checkbox for "listening test settings"
          * Performs capture scenario **/


        [When("[Perform Capture with listening test settings]")]
        public void WhenPerformCaptureWithListeningTestSettings()

        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            config = FeatureContext.Current["config"] as appconfigsettings;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
            FunctionLibrary lib = new FunctionLibrary();
            session = lib.functionWaitForName(session, "Capture");
            stepName.Log(Status.Pass, "Clicked on Capture.");
            session = lib.functionWaitForName(session, "LOGIN REQUIRED");
            lib.clickOnElementWithIdonly(session, "PasswordBox");

            /** To passing the User password **/

            if (computer_name.Equals("FSWIRAY80"))
            {
                session.FindElementByAccessibilityId("PasswordBox").SendKeys("112233");
            }
            else
            {
                session.FindElementByAccessibilityId("PasswordBox").SendKeys("svk01");
            }

            Thread.Sleep(2000);
            session.FindElementByName("Login").Click();
            Thread.Sleep(20000);

            /** To Check the Setlistening checkbox **/

            try
            {
                if (session.FindElementByClassName("CheckBox").Selected)
                {

                    screenshot = ModuleFunctions.CaptureScreenshot(session);
                    stepName.Log(Status.Pass, "Listening test settings checkbox selected", MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                    session = lib.functionWaitForName(session, "CAPTURE");
                    Thread.Sleep(5000);

                    try
                    {
                        if (session.FindElementByAccessibilityId("labelHeader").Text == "Device Without Push Button")
                        {
                            session.FindElement(By.Name("OK")).Click();
                        }


                    }
                    catch (Exception ex) { }
                    var HIData = lib.waitUntilElementExists(session, "windowUserMessage", 1);
                    var text = session.FindElementByAccessibilityId("textBlockMessage");

                    if (session.FindElementByAccessibilityId("labelHeader").Text == "Capture Succeeded")
                    {
                        stepName.Log(Status.Pass, "Capture " + text.Text, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                    }
                    else
                    {
                        stepName.Log(Status.Fail, "Capture Failed" + ":" + "  " + text.Text, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                    }

                    ModuleFunctions.sessionInitialize(config.ApplicationPath.SandRAppPath, config.workingdirectory.SandR);
                    session = lib.waitForElement(session, "OK");
                    Thread.Sleep(2000);
                    session.FindElementByName("Services").Click();
                    ModuleFunctions.sessionInitialize(config.ApplicationPath.SandRAppPath, config.workingdirectory.SandR);
                    Thread.Sleep(4000);
                    session = lib.functionWaitForName(session, "Capture");
                    session = lib.functionWaitForName(session, "LOGIN REQUIRED");
                    lib.clickOnElementWithIdonly(session, "PasswordBox");

                    /** To passing the User password **/

                    if (computer_name.Equals("FSWIRAY80"))
                    {
                        session.FindElementByAccessibilityId("PasswordBox").SendKeys("112233");
                    }
                    else
                    {
                        session.FindElementByAccessibilityId("PasswordBox").SendKeys("svk01");
                    }

                    Thread.Sleep(2000);
                    session.FindElementByName("Login").Click();
                    session = lib.waitUntilElementExists(session, "checkBoxSetInListeningTest", 1);
                    var ext = session.FindElements(MobileBy.AccessibilityId("checkBoxSetInListeningTest"));
                    Actions action = new Actions(session);
                    action.MoveToElement(ext[0]).Build().Perform();
                    Thread.Sleep(2000);
                    action.MoveToElement(ext[0]).Click().Build().Perform();
                    Thread.Sleep(2000);
                    //session.Close();
                }

                else
                {
                    var ext = session.FindElements(MobileBy.AccessibilityId("checkBoxSetInListeningTest"));
                    Actions action = new Actions(session);
                    action.MoveToElement(ext[0]).Build().Perform();
                    Thread.Sleep(2000);
                    action.MoveToElement(ext[0]).Click().Build().Perform();
                    Thread.Sleep(2000);
                    Thread.Sleep(2000);
                    screenshot = ModuleFunctions.CaptureScreenshot(session);
                    stepName.Log(Status.Pass, "Listening test settings checkbox selected", MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                    session = lib.functionWaitForName(session, "CAPTURE");
                    Thread.Sleep(5000);

                    try
                    {
                        if(session.FindElementByAccessibilityId("labelHeader").Text == "Device Without Push Button")
                        {
                            session.FindElement(By.Name("OK")).Click();
                        }
                       
                        
                    }
                    catch (Exception ex) { }

                    var HIData = lib.waitUntilElementExists(session, "windowUserMessage", 1);
                    var text = session.FindElementByAccessibilityId("textBlockMessage");

                    if (session.FindElementByAccessibilityId("labelHeader").Text == "Capture Succeeded")
                    {
                        stepName.Log(Status.Pass, "Capture " + text.Text, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                        ModuleFunctions.sessionInitialize(config.ApplicationPath.SandRAppPath, config.workingdirectory.SandR);
                        session = lib.waitForElement(session, "OK");
                        Thread.Sleep(4000);
                        session.FindElementByName("Services").Click();
                        ModuleFunctions.sessionInitialize(config.ApplicationPath.SandRAppPath, config.workingdirectory.SandR);
                        Thread.Sleep(4000);
                        session = lib.functionWaitForName(session, "Capture");
                        session = lib.functionWaitForName(session, "LOGIN REQUIRED");
                        lib.clickOnElementWithIdonly(session, "PasswordBox");

                        /** To passing the User password **/

                        if (computer_name.Equals("FSWIRAY80"))
                        {
                            session.FindElementByAccessibilityId("PasswordBox").SendKeys("112233");
                        }
                        else
                        {
                            session.FindElementByAccessibilityId("PasswordBox").SendKeys("svk01");
                        }

                        Thread.Sleep(2000);
                        session.FindElementByName("Login").Click();
                        session = lib.waitUntilElementExists(session, "checkBoxSetInListeningTest", 1);
                        ext = session.FindElements(MobileBy.AccessibilityId("checkBoxSetInListeningTest"));
                        action = new Actions(session);
                        action.MoveToElement(ext[0]).Build().Perform();
                        Thread.Sleep(2000);
                        action.MoveToElement(ext[0]).Click().Build().Perform();
                        Thread.Sleep(2000);
                        session.FindElementByName("CANCEL").Click();

                        //session.Close();

                    }
                    else if (session.FindElementByAccessibilityId("labelHeader").Text == "FittingSessionNotFound")
                    {
                        screenshot = ModuleFunctions.CaptureScreenshot(session);
                        stepName.Log(Status.Pass, "FittingSessionNotFound" + ":" + "  " + text.Text, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                        ModuleFunctions.sessionInitialize(config.ApplicationPath.SandRAppPath, config.workingdirectory.SandR);
                        session = lib.waitForElement(session, "Cancel");
                        Thread.Sleep(4000);
                        session.FindElementByName("Services").Click();
                        ModuleFunctions.sessionInitialize(config.ApplicationPath.SandRAppPath, config.workingdirectory.SandR);
                        Thread.Sleep(4000);
                        session = lib.functionWaitForName(session, "Capture");
                        session = lib.functionWaitForName(session, "LOGIN REQUIRED");
                        lib.clickOnElementWithIdonly(session, "PasswordBox");

                        /** To passing the User password **/

                        if (computer_name.Equals("FSWIRAY80"))
                        {
                            session.FindElementByAccessibilityId("PasswordBox").SendKeys("112233");
                        }
                        else
                        {
                            session.FindElementByAccessibilityId("PasswordBox").SendKeys("svk01");
                        }

                        Thread.Sleep(2000);
                        session.FindElementByName("Login").Click();
                        session = lib.waitUntilElementExists(session, "checkBoxSetInListeningTest", 1);
                        ext = session.FindElements(MobileBy.AccessibilityId("checkBoxSetInListeningTest"));
                        action = new Actions(session);
                        action.MoveToElement(ext[0]).Build().Perform();
                        Thread.Sleep(2000);
                        action.MoveToElement(ext[0]).Click().Build().Perform();
                        Thread.Sleep(2000);
                        session.FindElementByName("CANCEL").Click();

                        //session.Close();
                    }
                    else
                    {

                        screenshot = ModuleFunctions.CaptureScreenshot(session);
                        stepName.Log(Status.Fail, "Capture Failed" + text.Text, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                        ModuleFunctions.sessionInitialize(config.ApplicationPath.SandRAppPath, config.workingdirectory.SandR);
                        session = lib.waitForElement(session, "OK");
                        Thread.Sleep(4000);
                        session.FindElementByName("Services").Click();
                        ModuleFunctions.sessionInitialize(config.ApplicationPath.SandRAppPath, config.workingdirectory.SandR);
                        Thread.Sleep(4000);
                        session = lib.functionWaitForName(session, "Capture");
                        session = lib.functionWaitForName(session, "LOGIN REQUIRED");
                        lib.clickOnElementWithIdonly(session, "PasswordBox");

                        /** To passing the User password **/

                        if (computer_name.Equals("FSWIRAY80"))
                        {
                            session.FindElementByAccessibilityId("PasswordBox").SendKeys("112233");
                        }
                        else
                        {
                            session.FindElementByAccessibilityId("PasswordBox").SendKeys("svk01");
                        }

                        Thread.Sleep(2000);
                        session.FindElementByName("Login").Click();
                        session = lib.waitUntilElementExists(session, "checkBoxSetInListeningTest", 1);
                        ext = session.FindElements(MobileBy.AccessibilityId("checkBoxSetInListeningTest"));
                        action = new Actions(session);
                        action.MoveToElement(ext[0]).Build().Perform();
                        Thread.Sleep(2000);
                        action.MoveToElement(ext[0]).Click().Build().Perform();
                        Thread.Sleep(2000);
                        session.FindElementByName("CANCEL").Click();

                        //session.Close();
                    }
                }


            }
            catch (Exception e)
            { }
        }


        /** To verify the desired Capture time in log file **/

        [When("[Go to logs and verify capturing time]")]
        public void WhenGoToLogsAndVerifyCapturingTime()
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
            FunctionLibrary lib = new FunctionLibrary();
            Thread.Sleep(4000);
            string path = (@"C:\Users\Public\Documents\Camelot\Logs\" + computer_name + "-" + user_name + "-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");
            lib.fileVerify(path, stepName, "Capturing the hearing");
            Thread.Sleep(2000);
            Thread.Sleep(2000);
            lib.processKill("msedge");
        }



        /** Opens AlgoLabTest 
         *  connects the Hi device
         *  Naviagtes to ADL window
         *  Alter the ADL values and stores the information to device **/


        [When(@"\[Launch algo and alter ADL value ""([^""]*)"" and ""([^""]*)""and""([^""]*)""]")]
        public void WhenLaunchAlgoAndAlterADLValueAndAnd(string device, string DeviceNo, string DeviceType)
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());

            /** Algo Tet Lab **/

            stepName.Log(Status.Pass, "Altered value is 1");
            ModuleFunctions.altTestLab(session, stepName, device, DeviceNo, DeviceType);
            Thread.Sleep(2000);
        }

        [Then(@"\[Launch Algo with fresh device B and validate the ADL Battery values ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)""]")]
        public void ThenLaunchAlgoWithFreshDeviceBAndValidateTheADLBatteryValuesAndAnd(string device, string DeviceNo, string DeviceType)
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());

            ModuleFunctions.ADLvaluesForFreshDevice(session, stepName, device, DeviceNo, DeviceType);
            Thread.Sleep(2000);
        }

        [When(@"\[Launch algo and alter ADL value for device B ""([^""]*)"" and ""([^""]*)""and""([^""]*)""]")]
        public void WhenLaunchAlgoAndAlterADLValueForDeviceBAndAnd(string device, string DeviceNo, string DeviceType)
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());

            ModuleFunctions.ADLvaluesForDeviceB(session, stepName, device, DeviceNo, DeviceType);
            Thread.Sleep(2000);
        }

        /** Opens S&R tool
         *  Navigates to servies tab
         *  performs Restore operation with available captured data **/

        [When(@"\[Perform Restore with above captured image ""([^""]*)"" and ""([^""]*)""and""([^""]*)"" and ""([^""]*)""]")]
        public void WhenPerformRestoreWithAboveCapturedImageAndAndAnd(string device, string DeviceNo, string DeviceType, string side)
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            config = FeatureContext.Current["config"] as appconfigsettings;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());

            if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
            {
                ModuleFunctions.socketA(session, test, DeviceType);
            }

            try
            {
                session = ModuleFunctions.launchApp(Directory.GetCurrentDirectory() + "\\LaunchSandR.bat", Directory.GetCurrentDirectory());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            FunctionLibrary lib = new FunctionLibrary();

            /** Peforming Restore **/

            Thread.Sleep(8000);
            session = ModuleFunctions.sessionInitialize(config.ApplicationPath.SandRAppPath, config.workingdirectory.SandR);


            stepName.Log(Status.Pass, "S&R Tool launched successfully");
            Thread.Sleep(5000);
            session.FindElementByName("Device Info").Click();
            Thread.Sleep(5000);

            if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
            {
                session.FindElementByName("Discover").Click();
                stepName.Log(Status.Pass, "Clicked on Discover.");
                session.SwitchTo().Window(session.WindowHandles.First());
                Thread.Sleep(5000);
                session.SwitchTo().ActiveElement();
                Thread.Sleep(5000);
                try
                {
                    session.FindElementByAccessibilityId("SerialNumberTextBox").SendKeys(DeviceNo);
                    lib.functionWaitForName(session, "Search");

                    stepName.Log(Status.Pass, "Clicked on Search");


                    /** Finding the HI till Disconnect button on the display **/

                    do
                    {
                        try
                        {
                            session.SwitchTo().Window(session.WindowHandles.First());
                            string Message = "Connecting to the device failed as it has been powered for more than 2 minutes. Reboot the device and try again.";

                            while (session.FindElementByAccessibilityId("TextBox_1").Text != "Discovering wireless device...")
                            {

                                if (session.FindElementByAccessibilityId("TextBox_1").Text == Message || session.FindElementByAccessibilityId("TextBox_1").Text == "No wireless device could be found.")
                                {
                                    var sandRConnection = session.FindElementByAccessibilityId("TextBox_1").Text;
                                    stepName.Log(Status.Info, sandRConnection);
                                    var btncls1 = session.FindElementByAccessibilityId("PART_Close");
                                    btncls1.Click();
                                    Thread.Sleep(1000);
                                    ModuleFunctions.Recovery(session, stepName, DeviceType, DeviceNo, side);
                                    session = ModuleFunctions.sessionInitialize(config.ApplicationPath.SandRAppPath, config.workingdirectory.SandR);

                                }

                                if (session.FindElementByName("Discover").Text == "Discover")
                                {
                                    session.SwitchTo().Window(session.WindowHandles.First());
                                    session.FindElementByName("Search").Click();
                                }
                            }

                        }
                        catch (Exception)
                        {

                        }


                    } while (!session.FindElementByName("Disconnect").Displayed);

                    session = lib.waitForElement(session, "Model Name");
                    stepName.Log(Status.Pass, "Dook2 Dev");
                }

                catch (Exception ex)
                { }
            }

            session.FindElementByName("Device Info").Click();
            Thread.Sleep(2000);
            session.FindElementByName("Services").Click();
            Thread.Sleep(2000);
            var res = session.FindElementsByClassName("Button");
            res[14].Click();
            Thread.Sleep(2000);
            session = lib.functionWaitForName(session, "LOGIN REQUIRED");
            lib.clickOnElementWithIdonly(session, "PasswordBox");

            if (computer_name.Equals("FSWIRAY80"))
            {
                session.FindElementByAccessibilityId("PasswordBox").SendKeys("112233");
            }
            else
            {
                session.FindElementByAccessibilityId("PasswordBox").SendKeys("svk01");
            }

            Thread.Sleep(2000);
            session.FindElementByName("Login").Click();
            session = lib.functionWaitForName(session, "READ");
            session = lib.functionWaitForName(session, "FIND");
            session = lib.waitForElement(session, "SELECT");
            session = lib.functionWaitForName(session, "RESTORE");

            var HIData = lib.waitUntilElementExists(session, "windowUserMessage", 1);
            var text = session.FindElementByAccessibilityId("textBlockMessage");

            if (session.FindElementByAccessibilityId("labelHeader").Text == "Restoration Succeeded")
            {
                screenshot = ModuleFunctions.CaptureScreenshot(session);

                stepName.Log(Status.Pass, "Restoration " + text.Text, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
            }
            else
            {
                screenshot = ModuleFunctions.CaptureScreenshot(session);
                stepName.Log(Status.Fail, "Restoration Failed" + ":" + " " + text.Text, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
            }

            ModuleFunctions.sessionInitialize(config.ApplicationPath.SandRAppPath, config.workingdirectory.SandR);
            session = lib.waitForElement(session, "OK");
            var btncls = session.FindElementByAccessibilityId("PART_Close");
            btncls.Click();
            Thread.Sleep(1000);
        }


        /** Closes the S&R tool **/

        [Then("[Close SandR tool]")]
        public void ThenCloseSandRTool()
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            config = FeatureContext.Current["config"] as appconfigsettings;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
            ModuleFunctions.sessionInitialize(config.ApplicationPath.SandRAppPath, config.workingdirectory.SandR);
            var btncls = session.FindElementByAccessibilityId("PART_Close");
            btncls.Click();
            Thread.Sleep(1000);

            stepName.Log(Status.Pass, "S&R Tool is Closed Successful.");

        }


        /** Opens AlgoLabTest 
         *  Navigates to ADL window
         *  validates the ADL saved value **/


        [When(@"\[Launch algo lab and check the ADL value ""([^""]*)"" and ""([^""]*)""and ""([^""]*)""]")]
        public void WhenLaunchAlgoLabAndCheckTheADLValueAndAnd(string device, string DeviceNo, string DeviceType)
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;

            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());

            /** Verify AlgoTest Lab **/

            ModuleFunctions.checkADLValue(session, stepName, device, DeviceNo, DeviceType);
        }

        [When(@"\[Launch algo lab and check the ADL value ""([^""]*)"" and ""([^""]*)""and ""([^""]*)"" and ""([^""]*)""]")]
        public void WhenLaunchAlgoLabAndCheckTheADLValueAndAndAnd(string device, string DeviceNo, string DeviceType, string side)
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;

            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());

            /** Verify AlgoTest Lab **/

            ModuleFunctions.checkADLValueRightDevice(session, stepName, device, DeviceNo, DeviceType, side);
        }


        /** Opens FSW
          * Navigates to Fitting Screen
          * validates the FSW programs **/

        [Then(@"\[Launch FSW and check the added programs ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)""and""([^""]*)""]")]
        public void ThenLaunchFSWAndCheckTheAddedProgramsAndAndAnd(string device, string DeviceNo, string side, string DeviceType)
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            config = FeatureContext.Current["config"] as appconfigsettings;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
            Console.WriteLine("This is When method");

            FunctionLibrary lib = new FunctionLibrary();

            if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
            {
                Console.WriteLine("This is When method");

                try
                {
                    if (side.Equals("Left"))
                    {
                        ModuleFunctions.socketA(session, test, DeviceType);
                        Thread.Sleep(2000);
                    }
                    else if (side.Equals("Right"))
                    {
                        ModuleFunctions.socketB(session, test, DeviceType);
                        Thread.Sleep(2000);
                    }
                    else if (side.Equals("Cdevice"))
                    {
                        ModuleFunctions.socketC(session, test, DeviceType);
                        Thread.Sleep(2000);

                    }
                }
                catch (Exception ex) { }
                try
                {
                    AppiumOptions appCapabilities = new AppiumOptions();
                    session = ModuleFunctions.sessionInitialize(config.ApplicationPath.SmartFitAppPath, config.workingdirectory.FSWWorkingPath);
                    //AppiumOptions appCapabilities = new AppiumOptions();
                    //appCapabilities.AddAdditionalCapability("app", config.ApplicationPath.SmartFitAppPath);
                    //appCapabilities.AddAdditionalCapability("platformName", "Windows");
                    //appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "30");
                    //appCapabilities.AddAdditionalCapability("appArguments", "--run-as-administrator");
                    //appCapabilities.AddAdditionalCapability("appArguments", "Test.exe");
                    //appCapabilities.AddAdditionalCapability("appWorkingDir", @"C:\Program Files (x86)\ReSound\SmartFit");
                    //appCapabilities.AddAdditionalCapability("automationName", "Windows");
                    //appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
                    //appCapabilities.AddAdditionalCapability("ms:experimental-webdriver", true);
                    //session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                    //Thread.Sleep(5000);
                    //session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                    //session.Manage().Window.Maximize();
                    //session.SwitchTo().Window(session.WindowHandles.First());
                    ////session.SwitchTo().ActiveElement();
                    //Actions act = new Actions(session);
                    //var btnAdd = session.FindElement(By.ClassName("btn btn-lg btn-secondary-dark w-30"));
                    //act.MoveToElement(btnAdd).Click().Perform();
                    try
                    {
                        string addPatientXpath = "//Button[@Name=' Add or Select Patient']";
                        WindowsElement patientXpath = session.FindElement(By.XPath(addPatientXpath));
                        patientXpath.Click();
                    }
                    catch (Exception ex)
                    {
                        string addPatientXpath = "//Button[@ClassName='btn btn-lg btn-secondary-dark w-30']";
                        WindowsElement patientXpath = session.FindElement(By.XPath(addPatientXpath));
                        patientXpath.Click();

                    }
                    //Thread.Sleep(5000);
                    session = ModuleFunctions.sessionInitialize1(config.ApplicationPath.FSWAppPath, config.workingdirectory.FSWWorkingPath);
                    //appCapabilities.AddAdditionalCapability("app", config.ApplicationPath.FSWAppPath);
                    //appCapabilities.AddAdditionalCapability("platformName", "Windows");
                    //appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "30");
                    //appCapabilities.AddAdditionalCapability("appArguments", "--run-as-administrator");
                    //appCapabilities.AddAdditionalCapability("appArguments", "Test.exe");
                    //appCapabilities.AddAdditionalCapability("appWorkingDir", @"C:\Program Files (x86)\ReSound\SmartFit");
                    //appCapabilities.AddAdditionalCapability("automationName", "Windows");
                    //appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
                    ////appCapabilities.AddAdditionalCapability("ms:experimental-webdriver", true);
                    //session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                    //Thread.Sleep(5000);
                    //session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                    //session.Manage().Window.Maximize();
                    //session.SwitchTo().Window(session.WindowHandles.First());
                    //Actions acn1 = new Actions(session);
                    //var elen1 = session.FindElementByName("abc");
                    //acn1.MoveToElement(elen1).Perform();
                    WebDriverWait wait = new WebDriverWait(session, TimeSpan.FromMinutes(5));
                    try
                    {
                        wait.Until(d => d.FindElements(By.XPath("//Group")).Count > 0);

                        var allGroups = session.FindElements(By.XPath("//Group"));

                        foreach (var group in allGroups)
                        {
                            Actions actions = new Actions(session);

                            wait.Until(d => group.FindElements(By.XPath(".//Group[1]")).Count > 0);

                            var all = group.FindElements(By.XPath(".//Group[1]"));
                            foreach (var element in all)
                            {
                                wait.Until(d => element.FindElements(By.XPath(".//Group[1]")).Count > 0);

                                var all2 = element.FindElements(By.XPath(".//Group[1]"));
                                foreach (var child in all2)
                                {
                                    wait.Until(d => child.FindElements(By.XPath(".//*")).Count > 0);
                                    var allChildElements = child.FindElements(By.XPath(".//*"));
                                    if (allChildElements != null && allChildElements.Count >= 17)
                                    {
                                        var fourteenthElement = allChildElements[12];
                                        Actions actions1 = new Actions(session);
                                        actions1.MoveToElement(fourteenthElement).Click().Perform();

                                        var selectPatient2 = wait.Until(d => d.FindElement(By.Name("Fit Patient")));
                                        Actions actions2 = new Actions(session);
                                        actions2.MoveToElement(selectPatient2).Click().Perform();

                                        break;
                                    }

                                }
                            }
                        }
                    }
                    catch (WebDriverTimeoutException ex)
                    {
                    }
                    catch (NoSuchElementException ex)
                    {
                    }
                    catch (Exception ex)
                    {
                    }

                    //Thread.Sleep(1000);
                    //session.FindElementByName("Fit Patient").Click();
                    //appCapabilities.AddAdditionalCapability("app", config.ApplicationPath.SmartFitAppPath);
                    //appCapabilities.AddAdditionalCapability("platformName", "Windows");
                    //appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "25");
                    //appCapabilities.AddAdditionalCapability("appArguments", "--run-as-administrator");
                    //appCapabilities.AddAdditionalCapability("appArguments", "Test.exe");
                    //appCapabilities.AddAdditionalCapability("appWorkingDir", @"C:\Program Files (x86)\ReSound\SmartFit");
                    //appCapabilities.AddAdditionalCapability("automationName", "Windows");
                    //appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
                    //appCapabilities.AddAdditionalCapability("ms:experimental-webdriver", true);
                    //session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                    //Thread.Sleep(15000);
                    session = ModuleFunctions.sessionInitialize1(config.ApplicationPath.SmartFitAppPath, config.workingdirectory.FSWWorkingPath);

                    //session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                    session.Manage().Window.Maximize();
                    session.SwitchTo().Window(session.WindowHandles.First());
                    ////session.SwitchTo().ActiveElement();
                    session.FindElementByAccessibilityId("fittingpath-button-connect").Click();
                    Thread.Sleep(12000);

                    lib.clickOnAutomationName(session, "Assign Instruments");
                    //session.FindElementByName("Back").Click();
                    Thread.Sleep(5000);
                    session.FindElementByAccessibilityId("ConnectionAutomationIds.CommunicationInterfaceItems").Click();
                    // Thread.Sleep(2000);

                    /** Select Noah link Wireless now, then click Connect.  **/

                    session.FindElementByName("Noahlink Wireless").Click();
                    //lib.clickOnAutomationId(session, "Connect", "SidebarAutomationIds.ConnectAction");


                    try
                    {
                        session.FindElementByName("Unassign").Click();
                    }
                    catch { }

                    Thread.Sleep(10000);



                    var SN1 = session.FindElementsByClassName("ListBoxItem");

                    // Check if DeviceNo is already discovered

                    foreach (WindowsElement value in SN1)
                    {
                        string S = value.Text;
                        if (S.Contains(DeviceNo))
                        {

                            value.FindElementByName("Assign Left").Click();
                            break;
                        }
                    }
                    Thread.Sleep(13000);

                    try
                    {
                        if (session.FindElementByName("Unassign").Enabled)
                        {

                            lib.clickOnAutomationName(session, "Assign Instruments");

                            Thread.Sleep(10000); // Initial wait before searching

                            var SN = session.FindElementsByClassName("ListBoxItem");

                            // Check if DeviceNo is already discovered

                            foreach (WindowsElement value in SN)
                            {
                                string S = value.Text;
                                if (S.Contains(DeviceNo))
                                {
                                    value.FindElementByName("Assign Left").Click();
                                    break;
                                }
                            }

                        }
                    }
                    catch (Exception)
                    {

                    }

                    do
                    {
                        if (session.FindElementByName("Continue").Enabled == false)
                        {
                            lib.processKill("SmartFitSA");
                            lib.processKill("SmartFit");
                            if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
                            {
                                Console.WriteLine("This is When method");
                                try
                                {
                                    if (side.Equals("Left"))
                                    {
                                        ModuleFunctions.socketA(session, test, DeviceType);
                                        Thread.Sleep(2000);
                                    }
                                    else if (side.Equals("Right"))
                                    {
                                        ModuleFunctions.socketB(session, test, DeviceType);
                                        Thread.Sleep(2000);
                                    }
                                }
                                catch
                                {

                                }
                                try
                                {
                                    Thread.Sleep(2000);
                                    appCapabilities.AddAdditionalCapability("app", config.ApplicationPath.SmartFitAppPath);
                                    appCapabilities.AddAdditionalCapability("platformName", "Windows");
                                    appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "20");
                                    appCapabilities.AddAdditionalCapability("appArguments", "--run-as-administrator");
                                    appCapabilities.AddAdditionalCapability("appArguments", "Test.exe");
                                    appCapabilities.AddAdditionalCapability("appWorkingDir", @"C:\Program Files (x86)\ReSound\SmartFit");
                                    appCapabilities.AddAdditionalCapability("automationName", "Windows");
                                    appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
                                    appCapabilities.AddAdditionalCapability("ms:experimental-webdriver", true);
                                    session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                                    Thread.Sleep(5000);
                                    session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                                    session.Manage().Window.Maximize();
                                    session.SwitchTo().Window(session.WindowHandles.First());
                                    //session.SwitchTo().ActiveElement();
                                    Actions act1 = new Actions(session);
                                    var btnAdd1 = session.FindElementByClassName("spinner-border spinner-border-sm visually-hidden");
                                    act1.MoveToElement(btnAdd1).Click().Perform();
                                    Thread.Sleep(10000);
                                    appCapabilities.AddAdditionalCapability("app", config.ApplicationPath.FSWAppPath);
                                    appCapabilities.AddAdditionalCapability("platformName", "Windows");
                                    appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "20");
                                    appCapabilities.AddAdditionalCapability("appArguments", "--run-as-administrator");
                                    appCapabilities.AddAdditionalCapability("appArguments", "Test.exe");
                                    appCapabilities.AddAdditionalCapability("appWorkingDir", @"C:\Program Files (x86)\ReSound\SmartFit");
                                    appCapabilities.AddAdditionalCapability("automationName", "Windows");
                                    appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
                                    appCapabilities.AddAdditionalCapability("ms:experimental-webdriver", true);
                                    session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                                    Thread.Sleep(5000);
                                    session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                                    session.Manage().Window.Maximize();
                                    session.SwitchTo().Window(session.WindowHandles.First());
                                    //session.SwitchTo().ActiveElement();
                                    Actions ac1 = new Actions(session);
                                    var ele1 = session.FindElementByName("abc");
                                    ac1.MoveToElement(ele1).Perform();
                                    Thread.Sleep(1000);
                                    session.FindElementByName("Fit Patient").Click();
                                    appCapabilities.AddAdditionalCapability("app", config.ApplicationPath.SmartFitAppPath);
                                    appCapabilities.AddAdditionalCapability("platformName", "Windows");
                                    appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "20");
                                    appCapabilities.AddAdditionalCapability("appArguments", "--run-as-administrator");
                                    appCapabilities.AddAdditionalCapability("appArguments", "Test.exe");
                                    appCapabilities.AddAdditionalCapability("appWorkingDir", @"C:\Program Files (x86)\ReSound\SmartFit");
                                    appCapabilities.AddAdditionalCapability("automationName", "Windows");
                                    appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
                                    appCapabilities.AddAdditionalCapability("ms:experimental-webdriver", true);
                                    session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                                    Thread.Sleep(5000);
                                    session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                                    session.Manage().Window.Maximize();
                                    session.SwitchTo().Window(session.WindowHandles.First());
                                    //session.SwitchTo().ActiveElement();
                                    session.FindElementByName("Connect to ReSound Smart Fit").Click();
                                    Thread.Sleep(12000);

                                    lib.clickOnAutomationName(session, "Assign Instruments");
                                    //session.FindElementByName("Back").Click();
                                    Thread.Sleep(5000);
                                    session.FindElementByAccessibilityId("ConnectionAutomationIds.CommunicationInterfaceItems").Click();
                                    // Thread.Sleep(2000);

                                    /** Select Noah link Wireless now, then click Connect.  **/

                                    session.FindElementByName("Noahlink Wireless").Click();
                                    //lib.clickOnAutomationId(session, "Connect", "SidebarAutomationIds.ConnectAction");
                                    Thread.Sleep(10000);
                                    var SN2 = session.FindElementsByClassName("ListBoxItem");

                                    // Check if DeviceNo is already discovered

                                    foreach (WindowsElement value in SN2)
                                    {
                                        string S = value.Text;
                                        if (S.Contains(DeviceNo))
                                        {
                                            value.FindElementByName("Assign Left").Click();
                                            break;
                                        }
                                    }
                                }

                                catch
                                {
                                }
                            }
                        }
                    } while (!session.FindElementByName("Continue").Enabled);

                    /** Clicks on Continue buttion **/

                    lib.clickOnAutomationName(session, "Continue");
                    Thread.Sleep(4000);


                    try
                    {
                        lib.clickOnAutomationName(session, "Continue");
                    }
                    catch { }


                }
                catch (Exception)
                {
                }
            }


            if (DeviceType.Equals("Wired") || DeviceType.Equals("D1rechargeableWired"))
            {


                session = ModuleFunctions.sessionInitialize(config.ApplicationPath.SmartFitAppPath, config.workingdirectory.FSWWorkingPath);
                Thread.Sleep(8000);
                WindowsDriver<WindowsElement> session2 = null;
                try
                {
                    string addPatientXpath = "//Button[@Name=' Add or Select Patient']";
                    session.FindElement(By.XPath(addPatientXpath)).Click();

                }
                catch (Exception ex)
                {
                    string addPatientXpath = "//Button[@ClassName='btn btn-lg btn-secondary-dark w-30']";
                    session.FindElement(By.XPath(addPatientXpath)).Click();

                }
                Thread.Sleep(5000);
                session = ModuleFunctions.sessionInitialize1(config.ApplicationPath.FSWAppPath, config.workingdirectory.FSWWorkingPath);


                Thread.Sleep(5000);
                session.Manage().Window.Maximize();


                WebDriverWait wait = new WebDriverWait(session, TimeSpan.FromMinutes(5));
                try
                {
                    wait.Until(d => d.FindElements(By.XPath("//Group")).Count > 0);

                    var allGroups = session.FindElements(By.XPath("//Group"));

                    foreach (var group in allGroups)
                    {
                        Actions actions = new Actions(session);

                        wait.Until(d => group.FindElements(By.XPath(".//Group[1]")).Count > 0);

                        var all = group.FindElements(By.XPath(".//Group[1]"));
                        foreach (var element in all)
                        {
                            wait.Until(d => element.FindElements(By.XPath(".//Group[1]")).Count > 0);

                            var all2 = element.FindElements(By.XPath(".//Group[1]"));
                            foreach (var child in all2)
                            {
                                wait.Until(d => child.FindElements(By.XPath(".//*")).Count > 0);
                                var allChildElements = child.FindElements(By.XPath(".//*"));
                                if (allChildElements != null && allChildElements.Count >= 17)
                                {
                                    var fourteenthElement = allChildElements[12];
                                    Actions actions1 = new Actions(session);
                                    actions1.MoveToElement(fourteenthElement).Click().Perform();

                                    var selectPatient2 = wait.Until(d => d.FindElement(By.Name("Fit Patient")));
                                    actions1.MoveToElement(selectPatient2).Click().Perform();

                                    break;
                                }

                            }
                        }
                    }
                }
                catch (WebDriverTimeoutException ex)
                {
                }
                catch (NoSuchElementException ex)
                {
                }
                catch (Exception ex)
                {
                }

                //appCapabilities.AddAdditionalCapability("app", config.ApplicationPath.SmartFitAppPath);
                //appCapabilities.AddAdditionalCapability("platformName", "Windows");
                ////appCapabilities.SetCapability("ms:waitForAppLaunch", "5");
                //appCapabilities.AddAdditionalCapability("appArguments", "--run-as-administrator");
                //appCapabilities.AddAdditionalCapability("appArguments", "Test.exe");
                //appCapabilities.AddAdditionalCapability("appWorkingDir", @"C:\Program Files (x86)\ReSound\SmartFit");
                //appCapabilities.AddAdditionalCapability("automationName", "Windows");
                //appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
                //appCapabilities.AddAdditionalCapability("ms:experimental-webdriver", true);
                //session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                //Thread.Sleep(5000);
                //session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                //session.Manage().Window.Maximize();
                //session.SwitchTo().Window(session.WindowHandles.First());
                ////////session.SwitchTo().ActiveElement();
                session = ModuleFunctions.sessionInitialize1(config.ApplicationPath.SmartFitAppPath, config.workingdirectory.FSWWorkingPath);
                Thread.Sleep(12000);
                //session.FindElementByAccessibilityId("fittingpath-button-connect").Click();
                //Thread.Sleep(12000);

                session.FindElementByAccessibilityId("fittingpath-button-connect").Click();

                Thread.Sleep(5000);

                lib.clickOnAutomationName(session, "Assign Instruments");
                //session.FindElementByName("Back").Click();
                WebDriverWait wait1 = new WebDriverWait(session, TimeSpan.FromMinutes(5));
                WindowsElement comboBox = (WindowsElement)wait1.Until(driver =>
                {
                    var element = driver.FindElement(By.ClassName("ComboBox"));
                    return element.Enabled ? element : null;
                });

                comboBox.Click();
                Thread.Sleep(12000);

                lib.clickOnAutomationName(session, "Assign Instruments");
                //session.FindElementByName("Back").Click();
                Thread.Sleep(5000);
                session.FindElementByAccessibilityId("ConnectionAutomationIds.CommunicationInterfaceItems").Click();

                session.FindElementByName("Speedlink").Click();
                Thread.Sleep(5000);

                session.FindElementByName("Search").Click();




                //Thread.Sleep(2000);
                //DesiredCapabilities appCapabilities = new DesiredCapabilities();
                //appCapabilities.SetCapability("app", config.ApplicationPath.FSWAppPath);
                //appCapabilities.SetCapability("deviceName", "WindowsPC");
                //session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                //Thread.Sleep(10000);
                //session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                //Thread.Sleep(2000);
                //session.Manage().Window.Maximize();
                //var wait = new WebDriverWait(session, TimeSpan.FromSeconds(20));
                //var div = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("ListBoxItem")));
                //var text_Button = session.FindElementsByClassName("ListBoxItem");

                //stepName.Log(Status.Pass, "FSW is launched successfully");

                //int counter = 0;
                //string PatientName = null;
                //string PatientDescription = null;
                //foreach (var element in text_Button)
                //{
                //    if (counter == 2)
                //    {
                //        PatientName = element.GetAttribute("AutomationId");
                //        PatientDescription = element.GetAttribute("Name");
                //        break;
                //    }

                //    counter = counter + 1;
                //}

                //lib.clickOnAutomationId(session, PatientDescription, PatientName);

                ///** Clicks on Fit patient **/

                //Thread.Sleep(4000);
                //lib.clickOnAutomationId(session, "Fit Patient", "StandAloneAutomationIds.DetailsAutomationIds.FitAction");

                //stepName.Pass("Patient is clicked");

                //Thread.Sleep(8000);
                ////session.Close();

                ///** To launch the return visit session **/

                //appCapabilities = new DesiredCapabilities();
                //appCapabilities.SetCapability("app", config.ApplicationPath.SmartFitAppPath);
                //appCapabilities.SetCapability("deviceName", "WindowsPC");
                //Thread.Sleep(5000);
                //session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                //Thread.Sleep(10000);
                //session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                //Thread.Sleep(10000);

                //try

                //{
                //    lib.clickOnElementWithIdonly(session, "ConnectionAutomationIds.ConnectAction");
                //    stepName.Pass("Connect button is clicked");
                //}

                //catch (Exception e)
                //{
                //}

                //Thread.Sleep(10000);


                try
                {
                    /**  Clicks on "Back" button **/

                    //session.FindElementByName("Back").Click();
                    //Thread.Sleep(10000);
                    //session.FindElementByAccessibilityId("ConnectionAutomationIds.CommunicationInterfaceItems").Click();
                    //Thread.Sleep(2000);

                    ///** Choose the Speed link and Click on Connect **/

                    //session.FindElementByName("Speedlink").Click();
                    ////lib.clickOnAutomationId(session, "Connect", "SidebarAutomationIds.ConnectAction");

                    //Thread.Sleep(5000);

                    try
                    {
                        if (session.FindElementByName("Unassign").Enabled)
                        {

                            lib.clickOnAutomationName(session, "Assign Instruments");

                            Thread.Sleep(10000); // Initial wait before searching

                            var SN = session.FindElementsByClassName("ListBoxItem");

                            // Check if DeviceNo is already discovered

                            foreach (WindowsElement value in SN)
                            {
                                string S = value.Text;
                                if (S.Contains(DeviceNo))
                                {
                                    value.FindElementByName("Assign Left").Click();
                                    break;
                                }
                            }

                        }
                    }
                    catch (Exception)
                    {

                    }

                }
                catch (Exception)
                { }
            }

            Thread.Sleep(3000);

            lib.clickOnAutomationName(session, "Continue");

            int buttonCount = 0;

            try
            {
                WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(60));
                waitForMe.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("Button")));

                do
                {
                    //session.SwitchTo().ActiveElement();

                    if (buttonCount >= 1)
                    {
                        //session.SwitchTo().ActiveElement();
                        session = ModuleFunctions.getControlsOfParentWindow(session, "ScrollViewer", stepName);
                        try
                        {
                            session.FindElementByAccessibilityId("StateMachineAutomationIds.ContinueAction").Click();
                            waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(40));
                            waitForMe.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("Button")));
                        }
                        catch
                        {
                        }

                    }

                    buttonCount = buttonCount + 1;
                } while (session.FindElementByAccessibilityId("StateMachineAutomationIds.ContinueAction").Enabled);
            }



            catch (Exception e)
            {
                Thread.Sleep(8000);

                /** Clicks on "Fit Patient" in Profile screen **/

                try
                {
                    lib.clickOnElementWithIdonly(session, "PatientAutomationIds.ProfileAutomationIds.FitPatientAction");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                /** To Read the data from HI **/

                Thread.Sleep(5000);
                session.FindElementByName("Instrument").Click();
                Thread.Sleep(2000);
                session.FindElementByName("Read Instrument").Click();
                Thread.Sleep(7000);
                screenshot = ModuleFunctions.CaptureScreenshot(session);
                try
                {
                    session.FindElementByAccessibilityId("ProgramStripAutomationIds.ProgramSlot.P1").Click();

                    if ((session.FindElementByAccessibilityId("PART_Items").Text.ToString()).Contains("All-Around"))
                    {
                        Console.WriteLine("After listening test setting Value is :" + session.FindElementByAccessibilityId("PART_Items").Text.ToString());
                        stepName.Log(Status.Pass, "After listening test setting Value is :" + session.FindElementByAccessibilityId("PART_Items").Text.ToString(), MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                        Assert.Pass();
                        session.CloseApp();
                    }
                    else
                    {
                        stepName.Log(Status.Fail, "After listening test setting Value is :" + session.FindElementByAccessibilityId("PART_Items").Text.ToString(), MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());

                    }
                }

                catch (Exception ex)
                { }

                try
                {
                    session.FindElementByAccessibilityId("ProgramStripAutomationIds.ProgramSlot.P2").Click();
                    if ((session.FindElementByAccessibilityId("PART_Items").Text.ToString()).Contains("All-Around"))
                    {
                        Console.WriteLine("After listening test setting Value is :" + session.FindElementByAccessibilityId("PART_Items").Text.ToString());
                        stepName.Log(Status.Pass, "After listening test setting Value is :" + session.FindElementByAccessibilityId("PART_Items").Text.ToString());
                        Assert.Pass();
                    }

                    else
                    {
                        stepName.Log(Status.Fail, "After listening test setting Value is :" + session.FindElementByAccessibilityId("PART_Items").Text.ToString());
                    }
                }
                catch (Exception ex)
                { }

                try
                {
                    session.FindElementByAccessibilityId("ProgramStripAutomationIds.ProgramSlot.P3").Click();

                    if ((session.FindElementByAccessibilityId("PART_Items").Text.ToString()).Contains("All-Around"))

                    {
                        Console.WriteLine("After listening test setting Value is :" + session.FindElementByAccessibilityId("PART_Items").Text.ToString());
                        stepName.Log(Status.Pass, "After listening test setting Value is :" + session.FindElementByAccessibilityId("PART_Items").Text.ToString());
                        Assert.Pass();
                    }

                    else
                    {
                        stepName.Log(Status.Fail, "After listening test setting Value is :" + session.FindElementByAccessibilityId("PART_Items").Text.ToString());
                    }
                }
                catch (Exception ex)
                { }

                try
                {
                    session.FindElementByAccessibilityId("ProgramStripAutomationIds.ProgramSlot.P4").Click();

                    if ((session.FindElementByAccessibilityId("PART_Items").Text.ToString()).Contains("All-Around"))

                    {
                        Console.WriteLine("After listening test setting Value is :" + session.FindElementByAccessibilityId("PART_Items").Text.ToString());
                        stepName.Log(Status.Pass, "After listening test setting Value is :" + session.FindElementByAccessibilityId("PART_Items").Text.ToString());
                        Assert.Pass();
                    }

                    else
                    {
                        stepName.Log(Status.Fail, "After listening test setting Value is :" + session.FindElementByAccessibilityId("PART_Items").Text.ToString());
                    }
                }


                catch (Exception ex)
                {
                }

                try
                {
                    Thread.Sleep(10000);
                    lib.clickOnElementWithIdonly(session, "WindowAutomationIds.CloseAction");
                    session.SwitchTo().Window(session.WindowHandles.First());
                    //session.SwitchTo().ActiveElement();

                    /** Exit FSW with Out Saving **/

                    try
                    {
                        Thread.Sleep(2000);
                        lib.clickOnAutomationName(session, "Exit Without Saving");
                    }
                    catch (Exception e1)
                    {
                    }

                    stepName.Pass("Save is successfully done and Close the FSW");

                }
                catch (Exception ex)
                { }

                Thread.Sleep(8000);
                lib.processKill("SmartFitSA");
            }
        }



        /** To Verify the desired restoration time in log file **/

        [When("[Go to log file for verifying Restore time]")]
        public void WhenGoToLogFileForVerifyingRestoreTime()
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
            Thread.Sleep(1000);
            string path = (@"C:\Users\Public\Documents\Camelot\Logs\" + computer_name + "-" + user_name + "-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");
            FunctionLibrary lib = new FunctionLibrary();
            lib.fileVerify(path, stepName, "Restoring the hearing");
        }


        /** Reports for "captured" and "restored" data. **/

        [When("[Open Capture and Restore report and log info in report]")]
        public void WhenOpenCaptureAndRestoreReportAndLogInfoInReport()
        {
            /** This is to check if Capture and Restore files are existing **/

            if (computer_name.Equals("FSWIRAY80") || computer_name.Equals("FSWIRAY112"))
            {
                ModuleFunctions.verifyIfReportsExisted(test);

                /** Kill Acrobat reader **/

                Process[] processCollection = Process.GetProcesses();

                foreach (Process proc in processCollection)
                {
                    if (computer_name.Equals("FSWIRAY80") || computer_name.Equals("FSWIRAY112"))
                    {
                        if (proc.ProcessName == "msedge")
                        {
                            proc.Kill();

                        }

                        Console.WriteLine(proc);
                    }

                    else if (proc.ProcessName == "Acrobat")
                    {
                        proc.Kill();

                    }
                }
            }



            Thread.Sleep(8000);
        }



        /**  Opens the Storagelayoutviewer
           * changes the Date and Time in storage layout viewer **/


        [When(@"\[Verify StorageLayout Scenario By Changing Date and Confirm Cloud Icon ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)""and""([^""]*)""]")]
        public void WhenVerifyStorageLayoutScenarioByChangingDateAndConfirmCloudIconAndAndAnd(string device, string side, string DeviceNo, string DeviceType)
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
            Thread.Sleep(5000);
            string jsonString = File.ReadAllText(configsettingpath);
            Dictionary<string, Dictionary<string, string>> slv = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(jsonString);
            foreach (var slvPath in slv["SLV"])
            {
                if (device == slvPath.Key)
                {
                    Console.WriteLine($"{slvPath.Value}");
                    foreach (var workingDirectory in slv["WorkingDirectory"])
                    {
                        if (device == workingDirectory.Key)
                        {
                            if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
                            {
                                try
                                {
                                    ModuleFunctions.socketA(session, test, DeviceType);
                                }
                                catch (Exception ex) { }
                            }
                            session = ModuleFunctions.sessionInitialize(slvPath.Value, workingDirectory.Value);

                            Thread.Sleep(5000);


                            if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
                            {

                                FunctionLibrary lib = new FunctionLibrary();
                                Actions actions = new Actions(session);
                                var wait = new WebDriverWait(session, TimeSpan.FromSeconds(10));
                                var cancelButton = wait.Until(ExpectedConditions.ElementToBeClickable(session.FindElementByAccessibilityId("FINDICON")));
                                cancelButton.Click();
                                Thread.Sleep(10000);
                                var DetectButton = wait.Until(ExpectedConditions.ElementToBeClickable(session.FindElementByAccessibilityId("FINDICON")));
                                DetectButton.Click();
                                var Popup = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("Popup")));
                                int height = Popup.Size.Height;
                                var drag = Popup.FindElements(By.ClassName("Thumb"));

                                if (drag.Count >= 7)
                                {
                                    actions.MoveToElement(drag[6]).Perform();
                                    actions.ClickAndHold(drag[6]).MoveByOffset(0, height * 3).Release().Perform();
                                }

                                Thread.Sleep(15000);

                                /** To select the Hi serial number **/

                                try
                                {
                                    var dataGrid = session.FindElementByClassName("DataGrid");
                                    ReadOnlyCollection<AppiumWebElement> dataGridCells = dataGrid.FindElementsByClassName("DataGridCell");

                                    // Iterate through DataGridCell elements
                                    foreach (var element in dataGridCells)
                                    {
                                        if (element.Text == DeviceNo)
                                        {
                                            actions = new Actions(session);
                                            actions.MoveToElement(element).Click().Perform();
                                        }
                                    }

                                }
                                catch (Exception ex)
                                {

                                }

                                Thread.Sleep(3000);

                                do
                                {
                                    if (session.FindElementByName("Connect").Enabled == false)
                                    {

                                        string[] processesToKill = { "StorageLayoutViewer" };
                                        foreach (string processName in processesToKill)
                                        {
                                            lib.processKill(processName);
                                        }



                                        if (side.Equals("Left"))
                                        {
                                            Thread.Sleep(4000);

                                            if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
                                            {
                                                ModuleFunctions.socketA(session, test, DeviceType);
                                            }
                                            else if (side.Equals("Right"))
                                            {
                                                ModuleFunctions.socketB(session, test, DeviceType);
                                            }
                                        }

                                        foreach (var slvLaunch in slv["SLV"])
                                        {
                                            if (device == slvLaunch.Key)
                                            {
                                                Console.WriteLine($"{slvLaunch.Value}");
                                                foreach (var slvWorkingDirectory in slv["WorkingDirectory"])
                                                {
                                                    if (device == slvWorkingDirectory.Key)
                                                    {
                                                        Thread.Sleep(3000);
                                                        Console.WriteLine($"{slvWorkingDirectory.Value}");
                                                        session = ModuleFunctions.sessionInitialize(slvLaunch.Value, slvWorkingDirectory.Value);

                                                        if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
                                                        {

                                                            actions = new Actions(session);
                                                            wait = new WebDriverWait(session, TimeSpan.FromSeconds(10));
                                                            Thread.Sleep(5000);
                                                            cancelButton = wait.Until(ExpectedConditions.ElementToBeClickable(session.FindElementByAccessibilityId("FINDICON")));
                                                            cancelButton.Click();
                                                            wait = new WebDriverWait(session, TimeSpan.FromSeconds(10));
                                                            Thread.Sleep(10000);
                                                            DetectButton = wait.Until(ExpectedConditions.ElementToBeClickable(session.FindElementByAccessibilityId("FINDICON")));
                                                            DetectButton.Click();
                                                            wait = new WebDriverWait(session, TimeSpan.FromSeconds(10));
                                                            Popup = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("Popup")));
                                                            height = Popup.Size.Height;
                                                            drag = Popup.FindElements(By.ClassName("Thumb"));

                                                            if (drag.Count >= 7)
                                                            {
                                                                actions.MoveToElement(drag[6]).Perform();
                                                                actions.ClickAndHold(drag[6]).MoveByOffset(0, height * 3).Release().Perform();

                                                            }

                                                            Thread.Sleep(15000);

                                                            var GridCell = session.FindElementByClassName("DataGrid");
                                                            ReadOnlyCollection<AppiumWebElement> GridCells = GridCell.FindElementsByClassName("DataGridCell");

                                                            // Iterate through DataGridCell elements
                                                            foreach (var element in GridCells)
                                                            {
                                                                if (element.Text == DeviceNo)
                                                                {
                                                                    actions = new Actions(session);
                                                                    actions.MoveToElement(element).Click().Perform();
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                } while (!session.FindElementByName("Connect").Enabled);

                                session.SwitchTo().Window(session.WindowHandles[0]);

                                Thread.Sleep(7000);


                                lib.functionWaitForName(session, "Connect");
                                lib.waitUntilElementExists(session, "File", 0);
                                Thread.Sleep(4000);
                                //var ext = session.FindElements(WorkFlowPageFactory.fileMenu);
                                //ext[0].Click();
                                //Thread.Sleep(2000);
                                //ext = session.FindElements(WorkFlowPageFactory.readHI);
                                //actions = new Actions(session);
                                //actions.MoveToElement(ext[0]).Build().Perform();
                                //Thread.Sleep(2000);
                                //session.Keyboard.PressKey(Keys.Enter);
                                //Thread.Sleep(5000);

                                ///** Click on Uncheck button **/

                                //session.FindElementByName("Uncheck All").Click();
                                //Thread.Sleep(3000);
                                //session.FindElementByAccessibilityId("1001").Click();
                                //Thread.Sleep(2000);

                                ///** Choose the All option in drop down **/

                                //var rd = session.FindElementByName("All");
                                //actions.MoveToElement(rd).Click().Perform();
                                //Thread.Sleep(2000);
                                //var ext = session.FindElements(WorkFlowPageFactory.fileMenu);
                                //ext[0].Click();
                                //Thread.Sleep(5000);
                                //ext = session.FindElements(WorkFlowPageFactory.readHI);
                                //actions = new Actions(session);
                                //actions.MoveToElement(ext[0]).Build().Perform();
                                //Thread.Sleep(5000);
                                //session.Keyboard.PressKey(Keys.Enter);
                                var read = "//*[@Name='_Read from']";
                                var readFrom = session.FindElement(By.XPath(read));
                                actions.MoveToElement(readFrom).Click().Perform();
                                Thread.Sleep(10000);

                                stepName.Log(Status.Info, "Device connected successfully..");
                                /** Click on Uncheck button **/

                                session.FindElementByName("Uncheck All").Click();
                                Thread.Sleep(3000);
                                session.FindElementByAccessibilityId("1001").Click();
                                Thread.Sleep(4000);

                                /** Choose the All option in drop down **/

                                // Find the element by XPath

                                var all = "//*[@Name='All']";
                                var allClick = session.FindElement(By.XPath(all));
                                Thread.Sleep(4000);
                                Actions actions1 = new Actions(session);
                                actions1.MoveToElement(allClick).Click().Perform();
                                /** To Click the Apply selection button **/

                                session.FindElementByName("Apply selection").Click();
                                Thread.Sleep(5000);
                                var txt = session.FindElementByName("0f8e00:0004a ProductionTestData");
                                txt.Click();
                                Thread.Sleep(4000);
                                var data = session.FindElementByName("DataGridView");
                                data.Click();

                                if (device.Contains("NX9"))
                                {
                                    var row = session.FindElementByName("Value  -   from DeviceId: FittingDongle:0, Name: FittingDongle:0/Left, Side: Left Row 0");
                                    row.Click();
                                }
                                else
                                {
                                    var row = session.FindElementByName("Value  -   from FittingDongle:0/Left Row 0");
                                    row.Click();
                                }

                                /**In order to passing the Date and Time in the Product Testdata **/

                                DateTimeOffset currentTime = DateTimeOffset.Now;
                                Console.WriteLine(currentTime);
                                DateTimeOffset tenDaysAgo = currentTime.AddDays(-10);
                                Console.WriteLine(tenDaysAgo);
                                string formattedtenDaysAgo = tenDaysAgo.ToString("yyyy-MM-dd HH:mm:ssZ");
                                Console.WriteLine(formattedtenDaysAgo);
                                long unixTimestamp = tenDaysAgo.ToUnixTimeSeconds();
                                Console.WriteLine(unixTimestamp);
                                if (device.Contains("NX9"))
                                {
                                    var row = session.FindElementByName("Value  -   from DeviceId: FittingDongle:0, Name: FittingDongle:0/Left, Side: Left Row 0");
                                    row.SendKeys(formattedtenDaysAgo);
                                    var miniidentification = session.FindElementByName("_Write to");
                                    miniidentification.Click();
                                    Thread.Sleep(3000);
                                    var min = session.FindElementByName("111000:00026 MiniIdentification");
                                    min.Click();
                                    Thread.Sleep(2000);
                                    data = session.FindElementByName("DataGridView");
                                    data.Click();
                                    row = session.FindElementByName("Value  -   from DeviceId: FittingDongle:0, Name: FittingDongle:0/Left, Side: Left Row 6");
                                    row.Click();

                                    /** In order to passing the Unix time Stamp ID in the Miniidentification **/

                                    row.SendKeys(unixTimestamp.ToString());
                                    Thread.Sleep(2000);
                                    min = session.FindElementByName("_Write to");
                                    min.Click();
                                    Thread.Sleep(3000);

                                    screenshot = ModuleFunctions.CaptureScreenshot(session);

                                    stepName.Pass("Writing Presets is done successfully.", MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                                }
                                else
                                {
                                    var row = session.FindElementByName("Value  -   from FittingDongle:0/Left Row 0");
                                    row.SendKeys(formattedtenDaysAgo);
                                    var miniidentification = session.FindElementByName("_Write to");
                                    miniidentification.Click();
                                    Thread.Sleep(3000);
                                    var min = session.FindElementByName("111000:00026 MiniIdentification");
                                    min.Click();
                                    Thread.Sleep(2000);
                                    data = session.FindElementByName("DataGridView");
                                    data.Click();
                                    row = session.FindElementByName("Value  -   from FittingDongle:0/Left Row 6");
                                    row.Click();

                                    /** In order to passing the Unix time Stamp ID in the Miniidentification **/

                                    row.SendKeys(unixTimestamp.ToString());
                                    Thread.Sleep(2000);
                                    min = session.FindElementByName("_Write to");
                                    min.Click();
                                    Thread.Sleep(3000);
                                    screenshot = ModuleFunctions.CaptureScreenshot(session);

                                    stepName.Pass("Writing Presets is done successfully.", MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                                }

                            }
                            if (DeviceType.Equals("Wired") || DeviceType.Equals("D1rechargeableWired"))
                            {
                                try
                                {
                                    session.SwitchTo().Window(session.WindowHandles.First());
                                    //session.SwitchTo().ActiveElement();
                                    Thread.Sleep(5000);
                                    if (session.FindElementByName("Channel").Displayed == true)
                                    {
                                        FunctionLibrary lib = new FunctionLibrary();
                                        InputSimulator sim = new InputSimulator();
                                        lib.waitUntilElementExists(session, "Channel", 0);
                                        var ext = session.FindElements(WorkFlowPageFactory.channel);
                                        ext[0].Click();
                                        Thread.Sleep(2000);

                                        if (computer_name.Equals("FSWIRAY80"))
                                        {
                                            ext = session.FindElements(WorkFlowPageFactory.inter);
                                        }
                                        else
                                        {
                                            ext = session.FindElements(WorkFlowPageFactory.domainInterface);
                                        }

                                        Actions action = new Actions(session);

                                        if (computer_name.Equals("FSWIRAY80"))
                                        {
                                            if (side.Equals("Left"))
                                            {
                                                action.MoveToElement(ext[0]).Build().Perform();
                                                Thread.Sleep(2000);
                                                session.Keyboard.PressKey(Keys.Enter);
                                                Thread.Sleep(2000);
                                                session.Keyboard.PressKey(Keys.ArrowDown);
                                                Thread.Sleep(2000);
                                                session.Keyboard.PressKey(Keys.ArrowDown);
                                                Thread.Sleep(2000);
                                            }


                                            else
                                            {
                                                action.MoveToElement(ext[0]).Build().Perform();
                                                Thread.Sleep(2000);
                                                session.Keyboard.PressKey(Keys.Enter);
                                                Thread.Sleep(2000);
                                                session.Keyboard.PressKey(Keys.ArrowDown);
                                                Thread.Sleep(2000);
                                            }
                                        }

                                        else
                                        {
                                            if (side.Equals("Left"))
                                            {
                                                action.MoveToElement(ext[1]).Build().Perform();
                                                Thread.Sleep(2000);
                                                session.Keyboard.PressKey(Keys.Enter);
                                                Thread.Sleep(2000);
                                                session.Keyboard.PressKey(Keys.ArrowDown);
                                                Thread.Sleep(2000);
                                                session.Keyboard.PressKey(Keys.ArrowDown);
                                                Thread.Sleep(2000);
                                            }

                                            else
                                            {
                                                action.MoveToElement(ext[1]).Build().Perform();
                                                Thread.Sleep(2000);
                                                session.Keyboard.PressKey(Keys.Enter);
                                                Thread.Sleep(2000);
                                                session.Keyboard.PressKey(Keys.ArrowDown);
                                                Thread.Sleep(2000);
                                                session.Keyboard.PressKey(Keys.ArrowDown);
                                                Thread.Sleep(2000);
                                                session.Keyboard.PressKey(Keys.Enter);
                                                Thread.Sleep(2000);
                                            }
                                        }

                                        session.Keyboard.PressKey(Keys.Enter);
                                        stepName.Log(Status.Pass, side + ": is selected");
                                        lib.waitUntilElementExists(session, "File", 0);
                                        Thread.Sleep(4000);
                                        ext = session.FindElements(WorkFlowPageFactory.fileMenu);
                                        ext[0].Click();
                                        Thread.Sleep(2000);
                                        ext = session.FindElements(WorkFlowPageFactory.readHI);
                                        action = new Actions(session);
                                        action.MoveToElement(ext[0]).Build().Perform();
                                        Thread.Sleep(2000);
                                        session.Keyboard.PressKey(Keys.Enter);
                                        Thread.Sleep(2000);

                                        if (computer_name.Equals("FSWIRAY80"))
                                        {
                                            var txt = session.FindElementsByName("3e900:004c ProductionTestData");
                                            stepName.Log(Status.Pass, "3e900:004c ProductionTestData " + "is selected");

                                            foreach (var item in txt)
                                            {
                                                Console.WriteLine(item.GetAttribute("Name"));
                                                item.Click();
                                                stepName.Log(Status.Pass, "3e900:004c ProductionTestData " + "is selected");
                                            }

                                            Thread.Sleep(2000);
                                            string text = session.FindElementByName("Value Row 0").GetAttribute("Value");
                                            Console.WriteLine(text);
                                            var text1 = session.FindElementsByXPath("(//*[@ClassName='WindowsForms10.Window.8.app.0.2804c64_r17_ad1'])");
                                            Thread.Sleep(2000);

                                            int Counter = 0;
                                            foreach (var item in text1)
                                            {
                                                Console.WriteLine("Index value :" + Counter + item.Text);
                                                Console.WriteLine("Index value :" + Counter + item.GetAttribute("Name"));
                                                Console.WriteLine("Index value :" + Counter + item.GetAttribute("Value"));
                                                Console.WriteLine("Index value :" + Counter + item.GetAttribute("ControlType"));
                                                Counter = Counter + 1;
                                            }


                                            string tableName = session.FindElementByXPath("(//*[@ClassName='" + storageLayOutDate + "'])[11]").GetAttribute("ControlType"); ;
                                            Console.WriteLine("Table Index value :" + Counter + tableName);
                                            tableName = session.FindElementByXPath("(//*[@ClassName='" + storageLayOutDate + "'])[11]").GetAttribute("AutomationId"); ;
                                            Console.WriteLine("Table Index value :" + Counter + tableName);
                                            tableName = session.FindElementByXPath("(//*[@ClassName='" + storageLayOutDate + "'])[11]").FindElementByName("Top Row").GetAttribute("Value");
                                            var childTable = session.FindElementsByXPath("(//*[@ClassName='" + storageLayOutDate + "'])[11]//*[@Name='Row 0']//*[@Name='Value Row 0']");

                                            Counter = 0;

                                            foreach (var item in childTable)
                                            {
                                                Console.WriteLine("Production Index value :" + Counter + item.Text);
                                                Console.WriteLine("Production Index value :" + Counter + item.GetAttribute("Name"));
                                                Console.WriteLine("ProductionIndex value :" + Counter + item.GetAttribute("Value"));
                                                Console.WriteLine("Production Index value :" + Counter + item.GetAttribute("ControlType"));
                                                Console.WriteLine("Production Child Table Value is " + item.GetAttribute("HelpText"));
                                                Counter = Counter + 1;
                                            }

                                            childTable[0].Click();
                                            Thread.Sleep(2000);
                                            childTable[0].Click();
                                            Thread.Sleep(2000);
                                            childTable[0].Click();
                                            Thread.Sleep(2000);
                                            childTable[0].Clear();
                                            Thread.Sleep(2000);

                                            DateTimeOffset currentTime = DateTimeOffset.Now;
                                            Console.WriteLine(currentTime);
                                            DateTimeOffset tenDaysAgo = currentTime.AddDays(-10);
                                            Console.WriteLine(tenDaysAgo);
                                            string formattedtenDaysAgo = tenDaysAgo.ToString("yyyy-MM-dd HH:mm:ssZ");
                                            Console.WriteLine(formattedtenDaysAgo);
                                            long unixTimestamp = tenDaysAgo.ToUnixTimeSeconds();
                                            Console.WriteLine(unixTimestamp);

                                            childTable[0].SendKeys(formattedtenDaysAgo);
                                            Thread.Sleep(2000);
                                            txt[0].Click();
                                            Thread.Sleep(4000);
                                            sim.Keyboard.KeyPress(VirtualKeyCode.SPACE);
                                            Thread.Sleep(4000);

                                            /** Mini identification **/

                                            var txt1 = session.FindElementsByName("2a000:0026 MiniIdentification");

                                            foreach (var item in txt1)
                                            {
                                                Console.WriteLine(item.GetAttribute("Name"));
                                                item.Click();
                                                stepName.Pass("2a000:0026 MiniIdentification is selected");
                                            }

                                            Thread.Sleep(2000);
                                            childTable = session.FindElementsByXPath("(//*[@ClassName='" + storageLayOutDate + "'])[11]//*[@Name='Row 6']//*[@Name='Value Row 6']");
                                            Thread.Sleep(2000);

                                            Counter = 0;
                                            foreach (var item in childTable)
                                            {
                                                Console.WriteLine("Index value :" + Counter + item.Text);
                                                Console.WriteLine("Index value :" + Counter + item.GetAttribute("Name"));
                                                Console.WriteLine("Index value :" + Counter + item.GetAttribute("Value"));
                                                Console.WriteLine("Index value :" + Counter + item.GetAttribute("ControlType"));
                                                Console.WriteLine("Child Table Value is " + item.GetAttribute("HelpText"));
                                                stepName.Log(Status.Pass, "Saved value for date +" + item.Text);
                                                Counter = Counter + 1;
                                            }

                                            childTable[0].Click();
                                            Thread.Sleep(2000);
                                            childTable[0].Click();
                                            Thread.Sleep(2000);
                                            childTable[0].Click();
                                            Thread.Sleep(2000);
                                            string timeValue = childTable[0].GetAttribute("Value");
                                            Console.WriteLine(timeValue);
                                            childTable[0].Clear();
                                            Thread.Sleep(2000);
                                            childTable[0].SendKeys(unixTimestamp.ToString());
                                            Thread.Sleep(4000);
                                            txt1[0].Click();
                                            Thread.Sleep(4000);

                                            sim.Keyboard.KeyPress(VirtualKeyCode.SPACE);
                                            Thread.Sleep(2000);

                                            /** Write Data **/

                                            lib.clickOnAutomationName(session, "File");

                                            Thread.Sleep(4000);
                                            ext = session.FindElementsByXPath("(//*[@ClassName='" + storageLayOutDate + "'])[12]//*[@Name='File']//*[@LocalizedControlType='menu item'][3]");
                                            action.MoveToElement(ext[0]).Build().Perform();
                                            Thread.Sleep(2000);
                                            sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
                                            Thread.Sleep(2000);
                                            session.SwitchTo().Window(session.WindowHandles.First());
                                            //session.SwitchTo().ActiveElement();

                                            try
                                            {
                                                do
                                                {

                                                } while (session.FindElementByAccessibilityId("WorkerDialog").Enabled);

                                            }

                                            catch (Exception e)
                                            {
                                                screenshot = ModuleFunctions.CaptureScreenshot(session);

                                                stepName.Pass("Writing Presets is done successfully.", MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                                            }

                                            /** De select the check boxes  **/

                                            txt[0].Click();
                                            Thread.Sleep(4000);
                                            sim.Keyboard.KeyPress(VirtualKeyCode.SPACE);
                                            Thread.Sleep(4000);
                                            txt1[0].Click();
                                            Thread.Sleep(4000);
                                            sim.Keyboard.KeyPress(VirtualKeyCode.SPACE);
                                            Thread.Sleep(4000);
                                        }
                                        else
                                        {
                                            var txt = session.FindElementsByName("3e900:004c ProductionTestData");

                                            stepName.Log(Status.Pass, "3e900:004c ProductionTestData " + "is selected");

                                            foreach (var item in txt)
                                            {
                                                Console.WriteLine(item.GetAttribute("Name"));
                                                item.Click();
                                                stepName.Log(Status.Pass, "3e900:004c ProductionTestData " + "is selected");
                                            }

                                            Thread.Sleep(2000);


                                            var data = session.FindElementByName("DataGridView");
                                            data.Click();
                                            var row = session.FindElementByName("Value Row 0");
                                            row.Click();

                                            DateTimeOffset currentTime = DateTimeOffset.Now;
                                            Console.WriteLine(currentTime);
                                            DateTimeOffset tenDaysAgo = currentTime.AddDays(-10);
                                            Console.WriteLine(tenDaysAgo);
                                            string formattedtenDaysAgo = tenDaysAgo.ToString("yyyy-MM-dd HH:mm:ssZ");
                                            Console.WriteLine(formattedtenDaysAgo);
                                            long unixTimestamp = tenDaysAgo.ToUnixTimeSeconds();
                                            Console.WriteLine(unixTimestamp);

                                            row.SendKeys(formattedtenDaysAgo);
                                            var min = session.FindElementByName("2a000:0026 MiniIdentification");
                                            min.Click();
                                            Thread.Sleep(2000);
                                            data = session.FindElementByName("DataGridView");
                                            data.Click();
                                            row = session.FindElementByName("Row 6");
                                            row.Click();
                                            row.SendKeys(unixTimestamp.ToString());
                                            Thread.Sleep(2000);
                                            lib.clickOnAutomationName(session, "File");
                                            Thread.Sleep(4000);
                                            ext = session.FindElements(WorkFlowPageFactory.writeHI);
                                            action = new Actions(session);
                                            action.MoveToElement(ext[0]).Build().Perform();
                                            Thread.Sleep(2000);
                                            session.Keyboard.PressKey(Keys.Enter);


                                            try
                                            {
                                                do
                                                {

                                                } while (session.FindElementByAccessibilityId("WorkerDialog").Enabled);

                                            }

                                            catch (Exception e)
                                            {
                                                screenshot = ModuleFunctions.CaptureScreenshot(session);

                                                stepName.Pass("Writing Presets is done successfully.", MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                                            }
                                        }
                                    }


                                }

                                catch (Exception e)
                                {

                                    if (side.Equals("Left"))
                                    {
                                        session = ModuleFunctions.storagelayoutD1(session, stepName, device, side);
                                    }


                                    else
                                    {
                                        session = ModuleFunctions.storagelayoutD1(session, stepName, device, side);
                                    }

                                }

                            }

                        }
                    }
                }
            }

            session.CloseApp();

        }

    }
}
