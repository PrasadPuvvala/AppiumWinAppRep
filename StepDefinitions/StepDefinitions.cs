using AppiumWinApp;
using AppiumWinApp.PageFactory;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using com.sun.org.apache.xml.@internal.resolver.helpers;
using com.sun.org.glassfish.external.statistics.annotations;
using com.sun.xml.@internal.bind.v2.model.core;
using com.sun.xml.@internal.ws.api.server;
using IronPdf;
using java.awt;
using java.awt;
using java.io;
using java.util;
using javax.print.attribute.standard;
using javax.swing;
using jdk.nashorn.@internal.ir.annotations;
using Microsoft.Build.Tasks;
using Microsoft.Extensions.DependencyModel;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
//using java.io;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using NUnit.Framework;
using NUnit.Framework;
using OfficeOpenXml.Drawing.Slicer.Style;
using OpenQA.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.UI;
using org.w3c.dom.css;
using RazorEngine.Compilation.ImpromptuInterface.Optimization;
using sun.management.counter;
using sun.tools.java;
using System;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
//using Console = System.Console;
using System.IO;
using System.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading;
using System.Windows;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow;
using TestStack.BDDfy.Reporters.Html;
using WindowsInput;
using WindowsInput;
using WindowsInput.Native;
using WindowsInput.Native;
using Xamarin.Forms;
using Xamarin.Forms;
using Console = System.Console;
using Environment = System.Environment;
using File = System.IO.File;
using System.Threading.Tasks;
using OfficeOpenXml.ConditionalFormatting;
using Newtonsoft.Json.Linq;
using Azure;

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
        private static ExtentReports extent;
        private static ExtentHtmlReporter htmlReporter;
        private static ExtentTest test;


        public TestContext TestContext { get; set; }

        /*   declaration and initialization of a string variable */

        public static string workFlowProductSelection = "treeView";
        public static string storageLayOutDate = "WindowsForms10.Window.8.app.0.2804c64_r9_ad1";
        public static string algoTestProp = "";
        public static String textDir = Directory.GetCurrentDirectory();
        string computer_name = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
        String user_name = Environment.UserName;

        public StepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }


        [BeforeFeature]

        public static void beforeFeature()
        {

            
            Console.WriteLine("This is BeforeFetaure method");
            htmlReporter = new ExtentHtmlReporter(textDir + "\\report.html");
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            htmlReporter.Config.ReportName = "SandR Regression Test - Prasad PSSNV";
            htmlReporter.Config.EnableTimeline = true;
            htmlReporter.Config.DocumentTitle = "S and R Report";
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            ModuleFunctions.callbyextentreport(extent);

            /*Launch WinApp Driver*/

            Process winApp = new Process();
            winApp.StartInfo.FileName = "C:\\Program Files (x86)\\Windows Application Driver\\WinAppDriver.exe";
            winApp.Start();

            /* Reading CSV file to get device names */

            String[] csvVal = FunctionLibrary.readCSVFile();

            /*Launch Socket Driver*/


        }


        //    public static void BeforeFeature()
        //    {
        //        Console.WriteLine("Initializing BeforeFeature...");

        //        // Create an ExtentHtmlReporter with a specified file path
        //        var htmlReporter = new ExtentHtmlReporter(Path.Combine(textDir, "report.html"));

        //        // Configure the appearance of the HTML report
        //        htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
        //        htmlReporter.Config.ReportName = "SandR Regression Test - Prasad PSSNV";
        //        htmlReporter.Config.EnableTimeline = true;
        //        htmlReporter.Config.DocumentTitle = "S and R Report";

        //        // Customize the HTML and CSS settings

        //        CustomizeHtmlReporter(htmlReporter);

        //        // Create an ExtentReports instance and attach the ExtentHtmlReporter
        //        extent = new ExtentReports();
        //        extent.AttachReporter(htmlReporter);

        //        // Call a method to handle additional configuration if needed
        //        ModuleFunctions.callbyextentreport(extent);

        //        // Launch WinApp Driver
        //        StartWinAppDriver();

        //        // Read device names from a CSV file
        //        string[] csvValues = FunctionLibrary.readCSVFile();

        //        Console.WriteLine("BeforeFeature initialization completed.");
        //    }

        //    private static void CustomizeHtmlReporter(ExtentHtmlReporter htmlReporter)
        //    {
        //        // Customize the background color and font style using CSS
        //        htmlReporter.Config.CSS = @"
        //    .extent-wrapper {
        //        background-color: #1A1A1A;
        //        color: #fff;
        //        font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif;
        //        font-size: 14px;
        //        line-height: 1.6;
        //    }

        //    .extent-header {
        //        background-color: #333;
        //        color: #fff;
        //        padding: 15px;
        //        font-size: 18px;
        //    }

        //    .test-status.fail {
        //        background-color: #D32F2F;
        //        color: #fff;
        //    }

        //    .test-status.pass {
        //        background-color: #388E3C;
        //        color: #fff;
        //    }

        //    .test-step.log {
        //        color: #4FC3F7;
        //        font-weight: bold;
        //    }

        //    .extent-container {
        //        border: 1px solid #333;
        //        border-radius: 5px;
        //        overflow: hidden;
        //        margin: 10px; /* Add margin for better visibility */
        //    }

        //    .extent-test {
        //        border-bottom: 1px solid #555;
        //        padding: 10px;
        //    }

        //    .highlight {
        //        background-color: #FFD700;
        //        color: #333;
        //        padding: 2px;
        //        border-radius: 3px;
        //        font-weight: bold;
        //    }
        //";
        //    }

        //    //private static void CustomizeHtmlReporter(ExtentHtmlReporter htmlReporter)
        //    //{
        //    //    // Customize the background color and font style using CSS
        //    //    htmlReporter.Config.CSS = ".extent-wrapper { background-color: #333; color: #fff; font-family: 'Arial', sans-serif; }";

        //    //    // Add additional CSS styles as needed
        //    //    // htmlReporter.Config.CSS += ".your-class { property: value; }";
        //    //}

        //    private static void StartWinAppDriver()
        //    {
        //        var winApp = new Process();
        //        winApp.StartInfo.FileName = "C:\\Program Files (x86)\\Windows Application Driver\\WinAppDriver.exe";
        //        winApp.Start();
        //    }

        //    private static string[] ReadCSVFile()
        //    {
        //        // Add your logic to read and process the CSV file here
        //        return new string[0];
        //    }


        [BeforeScenario]



        public static void BeforeScenario()

        {


            // Read the scenario title to run from the configuration file //
            List<string> scenariosToRun = ReadScenariosToRunFromConfig();

            string currentScenarioTitle = ScenarioContext.Current.ScenarioInfo.Title;

            Console.WriteLine($"Starting scenario: {currentScenarioTitle}");

            if (scenariosToRun == null || scenariosToRun.Contains(currentScenarioTitle, StringComparer.OrdinalIgnoreCase))
            {
                // Continue with the scenario execution.
            }
            else
            {
                Console.WriteLine($"Scenario '{currentScenarioTitle}' not found or not configured to run. Skipping...");
                ScenarioContext.Current.Pending();
            }



            /*Extract the TestcseId from the Scenario Context*/

            Console.WriteLine("Starting " + ScenarioContext.Current.ScenarioInfo.Title);

            string test1 = ScenarioContext.Current.ScenarioInfo.Title;
            string pattern = @"Case ID (\d+)";

            Match match = Regex.Match(test1, pattern);
            string testcaseID = "";

            if (match.Success)

            {
                testcaseID = match.Groups[1].Value;
                ScenarioContext.Current.Set(testcaseID, "TestCaseID");

            }

            Console.WriteLine("Test Case ID: " + testcaseID);

            /*Update XML files with TestPlan,TestSuite,TestConfig*/


            FunctionLibrary lib = new FunctionLibrary();

            lib.PassingXML(test);

        }

        private static List<string> ReadScenariosToRunFromConfig()

        {
            string configFilePath = Path.Combine(Directory.GetCurrentDirectory(), "ScenarioConfig.json");

            if (!File.Exists(configFilePath))
            {
                Console.WriteLine($"Config file not found at {configFilePath}. No scenarios will be skipped.");
                return null;
            }

            try
            {
                string jsonContent = File.ReadAllText(configFilePath);
                JObject config = JObject.Parse(jsonContent);

                JArray scenarios = config["Scenarios"] as JArray;

                if (scenarios != null && scenarios.Any())

                {
                    //return scenarios.Select(s => s.ToString()).ToList();
                    return scenarios.Select(s => s.ToString()).ToList();

                }

                Console.WriteLine("No scenarios found in the config file.");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading config file: {ex.Message}. No scenarios will be skipped.");
                return null;
            }
        }



        [AfterFeature]
        public static void afterFeature()
        {
            Console.WriteLine("This is AfterFeature method");
            extent.Flush();
        }


        [OneTimeTearDown]
        public void GenerateReport()
        {
        }


            /** This is used for launching the FDTS
              * Passes the HI Serial number
              * perfrom Flashing and close the FDTS **/


        [Given(@"Launch FDTS WorkFlow And Flash Device ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)""")]
        public void GivenLaunchFDTSWorkFlowAndFlashDeviceAndAnd(string device, string DeviceNo, string flashHIWithSlno, string side)
        {
            Console.WriteLine("This is Given method");


            /** Socket Launching to pass commands for D2 and D3 **/

            try
            {
                if (device.Contains("RT") || device.Contains("RU"))
                {
                    if (side.Equals("Left"))
                    {
                        ModuleFunctions.socketA(session, test, device);
                    }
                    else if (side.Equals("Right"))
                    {
                        ModuleFunctions.socketB(session, test, device);
                    }
                }
            }
            catch (Exception ex) { }


            /** FDTS Launching **/

            try
            {
                session = ModuleFunctions.launchApp(textDir + "\\LaunchFDTS.bat", textDir);

            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }

            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());

            FunctionLibrary lib = new FunctionLibrary();
            ApplicationPath = "C:\\Program Files (x86)\\GN Hearing\\Camelot\\WorkflowRuntime\\Camelot.WorkflowRuntime.exe";
            session = ModuleFunctions.sessionInitialize1("C:\\Program Files (x86)\\GN Hearing\\Camelot\\WorkflowRuntime\\Camelot.WorkflowRuntime.exe", "C:\\Program Files (x86)\\GN Hearing\\Camelot\\WorkflowRuntime");
            Thread.Sleep(2000);
            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", ApplicationPath);
            appCapabilities.SetCapability("deviceName", "WindowsPC");
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            Thread.Sleep(8000);
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);

            test.Log(Status.Pass, "Test Work Flow launched successfully");

            try
            {
                System.IO.Directory.Delete(@"C:\Users\Public\Documents\Camelot\Logs", true);
            }
            catch (Exception e)
            {

            }

            Thread.Sleep(2000);         
            session.FindElement(WorkFlowPageFactory.filterBox).Clear();
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

                //session.FindElementByName(devName + " [1] (Final)").Click();              

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

                session.FindElementByName("Continue >>").Click();
                Thread.Sleep(2000);
                session.SwitchTo().Window(session.WindowHandles.First());
                session.SwitchTo().ActiveElement();
                session.FindElementByName("Continue >>").Click();
                Thread.Sleep(2000);
                session.SwitchTo().Window(session.WindowHandles.First());
                session.SwitchTo().ActiveElement();
                session.FindElementByName("Continue >>").Click();
                Thread.Sleep(4000);
                session.SwitchTo().Window(session.WindowHandles.First());
                session.SwitchTo().ActiveElement();
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
                }
                else
                {
                    session.FindElementByName(devName + " (Final)").Click();
                }                        
             
                session.FindElementByName("Continue >>").Click();
                Thread.Sleep(2000);
                Thread.Sleep(2000);
                session.SwitchTo().Window(session.WindowHandles.First());
                session.SwitchTo().ActiveElement();
                session.FindElementByName("Continue >>").Click();
                Thread.Sleep(4000);
                session.SwitchTo().Window(session.WindowHandles.First());
                session.SwitchTo().ActiveElement();
                var secondWindow = session.FindElementsByClassName(workFlowProductSelection);              
                Thread.Sleep(2000);
            }

            else if (device.Contains("RT") || device.Contains("RU"))
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

                    //session.FindElementByName(devName + " [9] (Final)").Click();
                }
                else
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
                    //session.FindElementByName(devName + " [9] (Final)").Click();
                }    
                
                session.FindElementByName("Continue >>").Click();
                Thread.Sleep(2000);
                session.SwitchTo().Window(session.WindowHandles.First());
                session.SwitchTo().ActiveElement();
                
                /** Passing Serial Number of HI device **/

                session.FindElementByAccessibilityId("textBoxSN").SendKeys(DeviceNo);
                session.FindElementByName("Continue >>").Click();
                Thread.Sleep(30000);
                session.SwitchTo().Window(session.WindowHandles[0]);

                try
                {
                    if (session.FindElementByAccessibilityId("MessageForm").Displayed)

                    {
                        ModuleFunctions.discoveryFailed(session, test, textDir, device, side, DeviceNo);
                    }
                }
                catch (Exception) 
                
                {
                        //ModuleFunctions.discoveryFailed(session, test, textDir, device, side, DeviceNo);
                }

                Thread.Sleep(8000);

                try
                {

                    session.SwitchTo().Window(session.WindowHandles.First());
                    session.SwitchTo().ActiveElement();

                }
                catch { }

                var allWindowHandles = session.WindowHandles;

                try
                {
                    do
                    {
                        allWindowHandles = session.WindowHandles;
                        session.SwitchTo().ActiveElement();


                    } while ((session.FindElementByClassName("WindowsForms10.STATIC.app.0.27a2811_r21_ad1").Enabled));
                }
                catch (Exception)
                {              
                }

                if (computer_name.Equals("FSWIRAY80") && computer_name.Equals("FSWIRAY112"))
                {
                    session = ModuleFunctions.sessionInitialize1("C:\\Program Files (x86)\\GN Hearing\\Camelot\\WorkflowRuntime\\Camelot.WorkflowRuntime.exe", "C:\\Program Files (x86)\\GN Hearing\\Camelot\\WorkflowRuntime");
                }

                else
                {
                    session = ModuleFunctions.sessionInitialize1("C:\\Program Files (x86)\\GN Hearing\\Camelot\\WorkflowRuntime\\Camelot.WorkflowRuntime.exe", "C:\\Program Files (x86)\\GN Hearing\\Camelot\\WorkflowRuntime");
                }

                try
                {
                    session.SwitchTo().Window(session.WindowHandles.First());
                    session.SwitchTo().ActiveElement();
                    if (device.Contains("RT") && device.Contains("C"))
                    {
                        session.SwitchTo().ActiveElement();
                        lib.waitUntilElementExists(session, "testParameter-Multiple-BatteryType", 1);
                        session.FindElementByName("Continue >>").Click();
                    }
                }
                catch (Exception) { }

                Thread.Sleep(5000);
          
                try
                {
                        session.SwitchTo().Window(session.WindowHandles.First());
                        session.SwitchTo().ActiveElement();

                    if (session.FindElementByName("Optimized").Displayed)
                    {
                            session.FindElementByName("Optimized").Click();
                            Thread.Sleep(2000);
                            session.SwitchTo().Window(session.WindowHandles.First());
                            session.SwitchTo().ActiveElement();
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
                        session.SwitchTo().ActiveElement();
                   
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
                session.SwitchTo().ActiveElement();

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

                test.Log(Status.Pass, "Flashing is started for Device" + devName);

                /** To handle Optimizeed window and Click on Continue **/

                try
                {
                    session.SwitchTo().Window(session.WindowHandles.First());
                    session.SwitchTo().ActiveElement(); 

                    if (session.FindElementByName("Optimized").Displayed)
                    {
                        session.FindElementByName("Optimized").Click();
                        Thread.Sleep(2000);
                        session.SwitchTo().Window(session.WindowHandles.First());
                        session.SwitchTo().ActiveElement();
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

               if (computer_name == "FSWIRAY80" ||computer_name == "UKBRAHPF2M76W6" || computer_name == "FSWIRAY112")
               {                  

                    Thread.Sleep(5000);
                    var allWindowHandles = session.WindowHandles;

                    try
                    {
                        do
                        {
                            allWindowHandles = session.WindowHandles;
                            session.SwitchTo().ActiveElement();


                        } while ((session.FindElementByClassName("Button").Enabled).ToString() == "False");
                    }

                    catch (Exception e)
                    {
                        session = FunctionLibrary.clickOnCloseTestFlow(session, "Test Execution");
                        session = lib.waitForElementToBeClickable(session, "Close");
                        test.Pass("Succesfully Flashed");
                        test.Info("Device: " + devName + " flashed");
                    }                 
               }

            try
            {
                if (computer_name.Equals("UKBRAHPF2M76W6 || FSWIRAY112"))

                {
                    session = ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\GN Hearing\\Camelot\\TestRuntimePC\\Camelot.TestRuntimePC.exe", "C:\\Program Files (x86)\\GN Hearing\\Camelot\\TestRuntimePC");
                    session = lib.waitForElementToBeClickable(session, "Close");
                }

                Thread.Sleep(6000);

                if (computer_name.Equals("FSWIRAY80 || FSWIRAY112 || UKBRAHPF2M76W6"))
                {
                    session = ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\GN Hearing\\Camelot\\WorkflowRuntime\\Camelot.WorkflowRuntime.exe", "C:\\Program Files (x86)\\GN Hearing\\Camelot\\WorkflowRuntime");
                }

                else
                {
                    session = ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\GN Hearing\\Camelot\\WorkflowRuntime\\Camelot.WorkflowRuntime.exe", "C:\\Program Files (x86)\\GN Hearing\\Camelot\\WorkflowRuntime");
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
                    //test.Log(Status.Fail, line);
                    break;
                }
                else
                {
                    test.Log(Status.Pass, "No Errors found in the log file while flashing");
                }
                counter++;
            }

            Console.WriteLine("Line number: {0}", counter);
            file.Close();
            Thread.Sleep(4000);        
        }



        /** To create a patient, 
         * connect the HIs to the FSW, 
         * make adjustments, save them, and then exit. **/


        [When(@"\[Create a Patient and Fitting HI In FSW ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)""]")]
        public void WhenCreateAPatientAndFittingHIInFSW(string alterValue, string device, string DeviceNo, string side)
        {
            string devName = device;
            FunctionLibrary lib = new FunctionLibrary();

            if (devName.Contains("RT") || devName.Contains("RU"))
            {
                Console.WriteLine("This is When method");
                test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());

                try
                {
                    if (side.Equals("Left"))
                    {
                        ModuleFunctions.socketA(session, test, device);
                        Thread.Sleep(2000);
                    }

                    else if (side.Equals("Right"))
                    {
                        ModuleFunctions.socketB(session, test, device);
                        Thread.Sleep(2000);
                    }
                }
                catch (Exception ex) { }

                try
                {


                    ApplicationPath = "C:\\Program Files (x86)\\ReSound\\SmartFit\\SmartFitSA.exe";
                    Thread.Sleep(2000);
                    DesiredCapabilities appCapabilities = new DesiredCapabilities();
                    appCapabilities.SetCapability("app", ApplicationPath);
                    appCapabilities.SetCapability("deviceName", "WindowsPC");
                    session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                    Thread.Sleep(10000);
                    session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                    Thread.Sleep(10000);
                    session.Manage().Window.Maximize();
                    var wait = new WebDriverWait(session, TimeSpan.FromSeconds(60));
                    var div = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("ListBoxItem")));
                    var text_Button = session.FindElementsByClassName("ListBoxItem");

                    test.Log(Status.Pass, "FSW is launched successfully");

                    int counter = 0;
                    string PatientName = null;
                    string PatientDescription = null;
                    foreach (var element in text_Button)
                    {
                        if (counter == 3)
                        {
                            PatientName = element.GetAttribute("AutomationId");
                            PatientDescription = element.GetAttribute("Name");
                            break;
                        }
                        counter = counter + 1;
                    }



                    lib.clickOnAutomationId(session, PatientDescription, PatientName);

                    /** Clicks on Fit patient button **/

                    Thread.Sleep(8000);
                    lib.waitForIdToBeClickable(session, "StandAloneAutomationIds.DetailsAutomationIds.FitAction");
                    test.Pass("Patient is clicked");
                    Thread.Sleep(10000);
                    session.Close();
                    ApplicationPath = "C:\\Program Files (x86)\\ReSound\\SmartFit\\SmartFit.exe";
                    appCapabilities = new DesiredCapabilities();
                    appCapabilities.SetCapability("app", ApplicationPath);
                    appCapabilities.SetCapability("deviceName", "WindowsPC");
                    Thread.Sleep(5000);
                    session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                    Thread.Sleep(10000);
                    session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                    Thread.Sleep(10000);

                }
                catch (Exception ex) 
                
                {

                    lib.processKill("SmartFitSA");

                    ApplicationPath = "C:\\Program Files (x86)\\ReSound\\SmartFit\\SmartFitSA.exe";
                    Thread.Sleep(2000);
                    DesiredCapabilities appCapabilities = new DesiredCapabilities();
                    appCapabilities.SetCapability("app", ApplicationPath);
                    appCapabilities.SetCapability("deviceName", "WindowsPC");
                    session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                    Thread.Sleep(10000);
                    session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                    Thread.Sleep(10000);
                    session.Manage().Window.Maximize();
                    var wait = new WebDriverWait(session, TimeSpan.FromSeconds(60));
                    var div = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("ListBoxItem")));
                    var text_Button = session.FindElementsByClassName("ListBoxItem");

                    test.Log(Status.Pass, "FSW is launched successfully");

                    int counter = 0;
                    string PatientName = null;
                    string PatientDescription = null;
                    foreach (var element in text_Button)
                    {
                        if (counter == 3)
                        {
                            PatientName = element.GetAttribute("AutomationId");
                            PatientDescription = element.GetAttribute("Name");
                            break;
                        }
                        counter = counter + 1;
                    }



                    lib.clickOnAutomationId(session, PatientDescription, PatientName);

                    /** Clicks on Fit patient button **/

                    Thread.Sleep(8000);
                    lib.waitForIdToBeClickable(session, "StandAloneAutomationIds.DetailsAutomationIds.FitAction");
                    test.Pass("Patient is clicked");
                    Thread.Sleep(10000);
                    session.Close();
                    ApplicationPath = "C:\\Program Files (x86)\\ReSound\\SmartFit\\SmartFit.exe";
                    appCapabilities = new DesiredCapabilities();
                    appCapabilities.SetCapability("app", ApplicationPath);
                    appCapabilities.SetCapability("deviceName", "WindowsPC");
                    Thread.Sleep(5000);
                    session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                    Thread.Sleep(10000);
                    session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                    Thread.Sleep(10000);

                }

                finally {  }


                /** Clicks on back button **/

                session.FindElementByName("Back").Click();
                Thread.Sleep(5000);
                session.FindElementByAccessibilityId("ConnectionAutomationIds.CommunicationInterfaceItems").Click();
                Thread.Sleep(2000);

                /** Select Noah link Wireless now, then click Connect.  **/

                session.FindElementByName("Noahlink Wireless").Click();
                lib.clickOnAutomationId(session, "Connect", "SidebarAutomationIds.ConnectAction");
                Thread.Sleep(8000);

                try
                {
                    if(session.FindElementByName("Unassign").Enabled)
                    {
                        lib.clickOnAutomationName(session, "Assign Instruments");
                        Thread.Sleep(5000);
                        var SN = session.FindElementsByClassName("ListBoxItem");
                        Thread.Sleep(10000);

                        /** The left side is assigned **/

                        foreach (WindowsElement value in SN)

                        {
                            string S = value.Text;
                            if (S.Contains(DeviceNo))
                            {
                                value.Text.Contains("Assign Left");
                                value.Click();
                            }
                        }
                    }
                }
                catch (Exception e) 
                {
                    lib.clickOnAutomationName(session, "Assign Instruments");
                    Thread.Sleep(5000);
                    var SN = session.FindElementsByClassName("ListBoxItem");
                    Thread.Sleep(10000);

                    /** The left side is assigned **/

                    foreach (WindowsElement value in SN)
                    {
                        string S = value.Text;
                        if (S.Contains(DeviceNo))
                        {
                            value.Text.Contains("Assign Left");
                            value.Click();
                        }
                    }
                    Thread.Sleep(10000);
                }

                /** Clicks on Continue buttion **/

                lib.clickOnAutomationName(session, "Continue");
                Thread.Sleep(4000);


                try
                {
                    lib.clickOnAutomationName(session, "Continue");
                }
                catch { }

            }


            if (devName.Contains("RE961-DRWC"))
            {
                Console.WriteLine("This is When method");
                test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());
                ApplicationPath = "C:\\Program Files (x86)\\ReSound\\SmartFit\\SmartFitSA.exe";
                Thread.Sleep(2000);
                DesiredCapabilities appCapabilities = new DesiredCapabilities();
                appCapabilities.SetCapability("app", ApplicationPath);
                appCapabilities.SetCapability("deviceName", "WindowsPC");
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                Thread.Sleep(10000);
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                Thread.Sleep(2000);
                session.Manage().Window.Maximize();
                var wait = new WebDriverWait(session, TimeSpan.FromSeconds(20));
                var div = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("ListBoxItem")));
                var text_Button = session.FindElementsByClassName("ListBoxItem");

                test.Log(Status.Pass, "FSW is launched successfully");

                int counter = 0;
                string PatientName = null;
                string PatientDescription = null;
                foreach (var element in text_Button)
                {
                    if (counter == 2)
                    {
                        PatientName = element.GetAttribute("AutomationId");
                        PatientDescription = element.GetAttribute("Name");
                        break;
                    }

                    counter = counter + 1;
                }

                lib.clickOnAutomationId(session, PatientDescription, PatientName);

                /** Clicks on Fit patient button **/

                Thread.Sleep(8000);
                lib.waitForIdToBeClickable(session, "StandAloneAutomationIds.DetailsAutomationIds.FitAction");

                test.Pass("Patient is clicked");

                Thread.Sleep(10000);
                session.Close();

                ApplicationPath = "C:\\Program Files (x86)\\ReSound\\SmartFit\\SmartFit.exe";
                appCapabilities = new DesiredCapabilities();
                appCapabilities.SetCapability("app", ApplicationPath);
                appCapabilities.SetCapability("deviceName", "WindowsPC");
                Thread.Sleep(5000);
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                Thread.Sleep(10000);
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                Thread.Sleep(10000);

                /**   clicks the back button, selects the Speed Link and then clicks "connect" **/

                try
                {
                    session.FindElementByName("Back").Click();
                    Thread.Sleep(5000);
                    session.FindElementByAccessibilityId("ConnectionAutomationIds.CommunicationInterfaceItems").Click();
                    Thread.Sleep(2000);
                    session.FindElementByName("Speedlink").Click();
                    lib.clickOnAutomationId(session, "Connect", "SidebarAutomationIds.ConnectAction");
                }
                catch(Exception)
                { }
            }


            else if (devName.Contains("LT") || devName.Contains("RE"))
            {
                Console.WriteLine("This is When method");
                
                test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());

                ApplicationPath = "C:\\Program Files (x86)\\ReSound\\SmartFit\\SmartFitSA.exe";
                Thread.Sleep(2000);
                DesiredCapabilities appCapabilities = new DesiredCapabilities();
                appCapabilities.SetCapability("app", ApplicationPath);
                appCapabilities.SetCapability("deviceName", "WindowsPC");
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                Thread.Sleep(10000);
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                Thread.Sleep(2000);
                session.Manage().Window.Maximize();
                var wait = new WebDriverWait(session, TimeSpan.FromSeconds(20));
                var div = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("ListBoxItem")));
                var text_Button = session.FindElementsByClassName("ListBoxItem");
                
                test.Log(Status.Pass, "FSW is launched successfully");

                int counter = 0;
                string PatientName = null;
                string PatientDescription = null;
                foreach (var element in text_Button)
                {
                    if (counter == 2)
                    {
                        PatientName = element.GetAttribute("AutomationId");
                        PatientDescription = element.GetAttribute("Name");
                        break;
                    }

                    counter = counter + 1;
                }

                lib.clickOnAutomationId(session, PatientDescription, PatientName);

                /** Clicks on Fit patient button **/

                Thread.Sleep(8000);

                lib.waitForIdToBeClickable(session, "StandAloneAutomationIds.DetailsAutomationIds.FitAction");
                test.Pass("Patient is clicked");
                Thread.Sleep(10000);
                session.Close();
                ApplicationPath = "C:\\Program Files (x86)\\ReSound\\SmartFit\\SmartFit.exe";
                appCapabilities = new DesiredCapabilities();
                appCapabilities.SetCapability("app", ApplicationPath);
                appCapabilities.SetCapability("deviceName", "WindowsPC");
                Thread.Sleep(5000);
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                Thread.Sleep(10000);
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                Thread.Sleep(10000);

                try

                {
                    lib.clickOnElementWithIdonly(session, "ConnectionAutomationIds.ConnectAction");
                    test.Pass("Connect button is clicked");
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    test.Fail(e.Message);
                }

                Thread.Sleep(10000);

                /**   clicks the back button, selects the Speed Link and then clicks "connect" **/

                try
                {

                    session.FindElementByName("Back").Click();
                    Thread.Sleep(10000);
                    session.FindElementByAccessibilityId("ConnectionAutomationIds.CommunicationInterfaceItems").Click();
                    Thread.Sleep(2000);
                    session.FindElementByName("Speedlink").Click();
                    lib.clickOnAutomationId(session, "Connect", "SidebarAutomationIds.ConnectAction");
                }
                catch(Exception)
                { }

            }

            Thread.Sleep(10000);
        
            int buttonCount = 0;
            try
            {
                WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(60));
                waitForMe.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("Button")));

                do
                {
                    session.SwitchTo().ActiveElement();

                    if (buttonCount >= 1)
                    {
                        session.SwitchTo().ActiveElement();
                        session = ModuleFunctions.getControlsOfParentWindow(session, "ScrollViewer", test);
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
                Thread.Sleep(4000);

                try
                {
                    lib.clickOnElementWithIdonly(session, "PatientAutomationIds.ProfileAutomationIds.FitPatientAction");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
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
                        session.SwitchTo().ActiveElement();

                        try
                        {
                            Thread.Sleep(2000);
                            lib.clickOnAutomationName(session, "Continue");
                        }
                        catch (Exception e1)
                        {
                        }

                        test.Pass("Reset is successfully done");
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
                        session.SwitchTo().ActiveElement();

                        try
                        {
                            Thread.Sleep(2000);
                            lib.clickOnAutomationName(session, "Continue");
                        }
                        catch (Exception e1)
                        {
                        }

                        test.Pass("Reset is successfully done");

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
                        test.Pass("Increment Gain is clicked for :" + increment + " times");
                        Thread.Sleep(2000);
                    } while (increment <= stepIncrement);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                try
                {
                    Thread.Sleep(10000);
                    lib.clickOnElementWithIdonly(session, "FittingAutomationIds.SaveAction");

                    /** Clicks on "Skip" Button **/

                    try
                    {
                        Thread.Sleep(2000);
                        lib.clickOnElementWithIdonly(session, "SkipButton");
                    }
                    catch (Exception e1)
                    {
                        lib.clickOnElementWithIdonly(session, "PART_Cancel");
                    }

                    test.Pass("Save is successfully done and Close the FSW");

                }
                catch (Exception skip)

                {
                    Console.WriteLine(skip);
                }

                lib.clickOnElementWithIdonly(session, "SaveAutomationIds.PerformSaveAutomationIds.ExitAction");

                /** Exit the FSW **/

                test.Pass("Click on FSW Exit button");

                Thread.Sleep(8000);

                lib.processKill("SmartFitSA");

            }
        }

        /** Used to clear exisisting 'capture' and 'restore' reports in specified path **/

        [When(@"\[Cleaning up Capture and Restore Reports Before Launch SandR]")]
        public void WhenCleaningUpCaptureAndRestoreReportsBeforeLaunchSandR()
        {

            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());

            try
            {
                System.IO.Directory.Delete(@"C:\CaptureBase\Reports", true);
                test.Pass("All files are deleted");
            }
            catch (Exception e)
            {
                test.Fail("No files found to be deleted");
            }

            try
            {
                System.IO.Directory.Delete(@"C:\Users\Public\Documents\Camelot\Logs", true);
                test.Pass("All Camelot log files are deleted");
            }
            catch (Exception e)
            {
                test.Fail("No files found to be deleted");
            }
        }

        /** Opens S&R tool 
          * Connects the HI device **/

        [When(@"\[Launch SandR ""([^""]*)"" and ""([^""]*)""]")]
        public void WhenLaunchSandR(string device, string DeviceNo)
        
        {
            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());

            FunctionLibrary lib = new FunctionLibrary();

            try
            {
                if (device.Contains("RT") || device.Contains("RU"))
                {                 
                    ModuleFunctions.socketA(session, test, device);
                                   
                }
            }
            catch { }

            Thread.Sleep(10000);

            try
            {
                session = ModuleFunctions.launchApp(Directory.GetCurrentDirectory() + "\\LaunchSandR.bat", Directory.GetCurrentDirectory());

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
           
            Thread.Sleep(5000);

            session = ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\GN Hearing\\Lucan\\App\\Lucan.App.UI.exe", "C:\\Program Files (x86)\\GN Hearing\\Lucan\\App");
            //Thread.Sleep(10000);
            test.Log(Status.Pass, "S&R Tool launched successfully");
            session.FindElementByName("Device Info").Click();
            Thread.Sleep(2000);

            if (device.Contains("RT") || device.Contains("RU"))
            {
                /** Clciks on "Discover" button **/

                session.FindElementByName("Discover").Click();
                test.Log(Status.Pass, "Clicked on Discover.");
                session.SwitchTo().Window(session.WindowHandles.First());
                session.SwitchTo().ActiveElement();

                /** Type the HI serial number in the search field. **/

                try
                {
                        session.FindElementByAccessibilityId("SerialNumberTextBox").SendKeys(DeviceNo);
                        lib.functionWaitForName(session, "Search");
                }

                catch(Exception ex)
                {

                }

                /** Finding the HI till Disconnect button on the display **/

                do
                {
                    try
                    {
                        session.SwitchTo().Window(session.WindowHandles.First());

                        if (session.FindElementByName("Discover").Text == "Discover")
                        {
                            session.SwitchTo().Window(session.WindowHandles.First());
                            session.FindElementByName("Search").Click();
                        }
                      
                    }
                    catch (Exception)
                    {

                    }

                } while (!session.FindElementByName("Disconnect").Displayed);



                test.Log(Status.Pass, "Clicked on Search");

                session = lib.waitForElement(session, "Model Name");

                test.Log(Status.Pass, "Dook2 Dev");
            }

            
        }



        /** Navigates to Device Info 
         *  reads device information into an Excel spreadsheet **/


        

        [When(@"\[Go to Device Info tab and capture device info in excel then verify the device information is shown correctly]")]
        public void WhenGoToDeviceInfoTabAndCaptureDeviceInfoInExcelThenVerifyTheDeviceInformationIsShownCorrectly()
        {
            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());

            /** Getting Device Info from Info tab **/

            FunctionLibrary lib = new FunctionLibrary();
            lib = new FunctionLibrary();
            lib.getDeviceInfo(session);

            test.Log(Status.Info, "Device information is captured in excel file");
        }




        /** Validates the updated firmware version in the S&R tool under device info **/




        [Then(@"\[Compare firmware version is upgraded successfully ""([^""]*)""]")]
        public void ThenCompareFirmwareVersionIsUpgradedSuccessfully(string device)
        {
            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());
            FunctionLibrary lib = new FunctionLibrary();
            Thread.Sleep(2000);
        
            string Dooku1nonRechargeble = "[1].18.1.1 (Dooku1)";
            string Dooku2nonRechargeable = "[9].71.1.1 (Dooku2)";
            string Dooku2Rechargeable = "[9].60.1.1 (Dooku2)";
            string Dooku3PBTE = "[7].42.1.1 (Dooku3)";
            string Dooku3Rechargeable = "[7].42.1.1 (Dooku3)";

            if (device.Contains("RT962-DRW") || device.Contains("RT961-DRWC") || device.Contains("RU961-DRWC") || device.Contains("RE962-DRW") || device.Contains("RU988-DWC"))
            {
                session.FindElementByName("Device Info").Click();
                Thread.Sleep(2000);

                session = lib.waitForElement(session, "Firmware Version");

                var version = session.FindElementByAccessibilityId("TextBox_6");

                Thread.Sleep(3000);

                switch (version.Text)

                {

                    case string _ when version.Text.Equals(Dooku2nonRechargeable) || version.Text.Equals(Dooku2Rechargeable) || version.Text.Equals(Dooku3PBTE) || version.Text.Equals(Dooku1nonRechargeble):
                        test.Log(Status.Pass, "Expected Firmware Version is: " + version.Text + " But Current Firmware is: " + version.Text);
                        break;


                    case string _ when device.Contains("RT962-DRW"):

                        test.Log(Status.Fail, "Expected Firmware Version is: " + Dooku2nonRechargeable + " But Current Firmware Version is: " + version.Text);
                        break;


                    case string _ when device.Contains("RE962-DRW"):

                        test.Log(Status.Fail, "Expected Firmware Version is: " + Dooku1nonRechargeble + " But Current Firmware Version is: " + version.Text);
                        break;


                    case string _ when device.Contains("RT961-DRWC"):

                        test.Log(Status.Fail, "Expected Firmware Version is: " + Dooku2Rechargeable + " But Current Firmware Version is: " + version.Text);
                        break;


                    case string _ when device.Contains("RU961-DRWC"):

                        test.Log(Status.Fail, "Expected Firmware Version is: " + Dooku3Rechargeable + " But Current Firmware Version is: " + version.Text);
                        break;

                    case string _ when  device.Contains("RU988-DWC"):

                        test.Log(Status.Fail, "Expected Firmware Version is: " + Dooku3PBTE + " But Current Firmware Version is: " + version.Text);
                        break;


                    default:
                        test.Log(Status.Fail, "Unknown Firmware Version: " + version.Text);
                        break;
                
                
                }

            }

        }




        /** Validates the updated firmware version in the S&R tool under device info **/





        [Then(@"\[Compare firmware version is downgraded successfully ""([^""]*)""]")]
        public void ThenCompareFirmwareVersionIsDowngradedSuccessfully(string device)
        {
            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());
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

                        test.Log(Status.Pass, "Expected Firmware Version is: " + version.Text + " But Current Firmware is: " + version.Text);
                        break;


                    case string _ when device.Contains("RE962-DRW"):

                        test.Log(Status.Fail, "Expected Firmware Version is: " + Dooku1nonRechargeble + " But Current Firmware Version is: " + version.Text);
                        break;


                    case string _ when device.Contains("RT961-DRWC"):

                        test.Log(Status.Fail, "Expected Firmware Version is: " + Dooku3nonRechargeable + " But Current Firmware Version is: " + version.Text);
                        break;


                    case string _ when device.Contains("RU988-DWC"):

                        test.Log(Status.Fail, "Expected Firmware Version is: " + Dooku3Rechargeable + " But Current Firmware Version is: " + version.Text);
                        break;


                    default:
                        test.Log(Status.Fail, "Unknown Firmware Version: " + version.Text);
                        break;

                }

            }

        }


        
        /** Navigate to services tab in S&R tool **/



        [When(@"\[Come back to Settings and wait till controls enabled]")]
        public void WhenComeBackToSettingsAndWaitTillControlsEnabled()
         {
            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());
            FunctionLibrary lib = new FunctionLibrary();
            Thread.Sleep(2000);
            session.FindElementByName("Services").Click();
            test.Log(Status.Pass, "Clicked on Services.");
        }



        /** Click on Disconnect button 
         *  Validates device information **/



        [Given(@"\[Download and verify azure storage files ""([^""]*)"" and ""([^""]*)""]")]
        public async Task GivenDownloadAndVerifyAzureStorageFilesAsync(string scenarioTitle, string DeviceNo)
        {
            //var Sandclose = session.FindElementByAccessibilityId("PART_Close");
           // Sandclose.Click();

            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());
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
                if (containerName == "lucan-hiservicerecords")

                {
                // Parse the date from the blob name (assuming the date format is "yyyy-MM-dd")
                var currentDate = DateTime.UtcNow.Date;
                var formattedCurrentDate = currentDate.ToString("yyyy-MM-dd");

                return Path.GetDirectoryName(blob.Name) == formattedCurrentDate;
                
                }

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

                    test.Log(Status.Pass, "Downloaded the file for the current system date to: {destinationFilePath}");

                if (containerName == "lucan-hiservicerecords")
                {
                    test.Log(Status.Pass, "Cloud service record is uploaded under today’s date");
                }
            }
            else
            {
                test.Log(Status.Fail, "No matching files found for the specified serial number or date in the " + scenarioTitle + " container.");
            }
        
        }
        
    


        



        [When(@"\[Clicks on disconnect and verify device information is cleared]")]
        public void ClicksOnDisconnectAndVerifyDeviceInformationIsCleared()
        {
            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());
            FunctionLibrary lib = new FunctionLibrary();
            lib = new FunctionLibrary();
            session = lib.waitForElement(session, "Connect to hearing instrument automatically");
            Thread.Sleep(2000);
            session.FindElementByName("Disconnect").Click();
            lib.getDeviceInfo(session);

            test.Log(Status.Pass, "Clicked on Disconnect.");
        }




        /** Opens S&R tool 
         *  Navigates to settings tab
         *  Perform the Capture operation **/

        
        [When(@"\[Perform Capture""([^""]*)""]")]
        public void WhenPerformCapture(string device)
        {
            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());

            FunctionLibrary lib = new FunctionLibrary();

            session = lib.functionWaitForName(session, "Capture");

            test.Log(Status.Pass, "Clicked on Capture.");

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
            ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\GN Hearing\\Lucan\\App\\Lucan.App.UI.exe", "C:\\Program Files (x86)\\GN Hearing\\Lucan\\App");
            session = lib.waitForElement(session, "OK");
            var btnClose = session.FindElementByAccessibilityId("PART_Close");
            btnClose.Click();

            try
            {
                if (device.Contains("RT") || device.Contains("RU"))
                {
                    ModuleFunctions.socketA(session, test, device);
                }
            }
            catch { }

        }

        

        /** Clicks on Capture operation
          * Checks the checkbox for "listening test settings"
          * Performs capture scenario **/



        [When(@"\[Perform Capture with listening test settings]")]
        public void WhenPerformCaptureWithListeningTestSettings()

        {
            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());

            FunctionLibrary lib = new FunctionLibrary();

            session = lib.functionWaitForName(session, "Capture");

            test.Log(Status.Pass, "Clicked on Capture.");

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

            Thread.Sleep(8000);

            /** To Check the Setlistening checkbox **/

            try
            {
                if (session.FindElementByClassName("CheckBox").Selected)
                {
                    session = lib.functionWaitForName(session, "CAPTURE");
                    ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\GN Hearing\\Lucan\\App\\Lucan.App.UI.exe", "C:\\Program Files (x86)\\GN Hearing\\Lucan\\App");
                    session = lib.waitForElement(session, "OK");
                    session.Close();
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
                    session = lib.functionWaitForName(session, "CAPTURE");
                    ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\GN Hearing\\Lucan\\App\\Lucan.App.UI.exe", "C:\\Program Files (x86)\\GN Hearing\\Lucan\\App");
                    session = lib.waitForElement(session, "OK");
                    Thread.Sleep(4000);

                    session.FindElementByName("Services").Click();
                    ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\GN Hearing\\Lucan\\App\\Lucan.App.UI.exe", "C:\\Program Files (x86)\\GN Hearing\\Lucan\\App");
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
                    session= lib.waitUntilElementExists(session, "checkBoxSetInListeningTest", 1);
                    ext = session.FindElements(MobileBy.AccessibilityId("checkBoxSetInListeningTest"));
                    action = new Actions(session);
                    action.MoveToElement(ext[0]).Build().Perform();
                    Thread.Sleep(2000);
                    action.MoveToElement(ext[0]).Click().Build().Perform();
                    Thread.Sleep(2000);
                    session.Close();
                }
            }

            catch(Exception e)
            { }
        }


        /** To verify the desired Capture time in log file **/

        [When(@"\[Go to logs and verify capturing time]")]
        public void WhenGoToLogsAndVerifyCapturingTime()
        {
            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());
            FunctionLibrary lib = new FunctionLibrary();
            Thread.Sleep(4000);
            string path = (@"C:\Users\Public\Documents\Camelot\Logs\" + computer_name + "-" + user_name + "-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");
            lib.fileVerify(path, test, "Capturing the hearing");
            Thread.Sleep(2000);
            Thread.Sleep(2000);
            lib.processKill("msedge");
        }



        /** Opens AlgoLabTest 
         *  connects the Hi device
         *  Naviagtes to ADL window
         *  Alter the ADL values and stores the information to device **/



        [When(@"\[Launch algo and alter ADL value ""([^""]*)"" and ""([^""]*)""]")]
        public void WhenLaunchAlgoAndAlterADLValue(string device, string DeviceNo)
        {
            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());

            /** Algo Tet Lab **/

            test = extent.CreateTest("Test ALT WorkFlow");

            test.Log(Status.Pass, "Altered value is 1");

            ModuleFunctions.altTestLab(session, test, device, DeviceNo);
            Thread.Sleep(2000);
        }



        /** Opens S&R tool
         *  Navigates to servies tab
         *  performs Restore operation with available captured data **/



        [When(@"\[Perform Restore with above captured image ""([^""]*)"" and ""([^""]*)""]")]
        public void WhenPerformRestoreWithAboveCapturedImage(string device, string DeviceNo)

        {
            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());

            if(device.Contains("RT") || device.Contains("RU"))
            {
                ModuleFunctions.socketA(session, test, device);
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

            session = ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\GN Hearing\\Lucan\\App\\Lucan.App.UI.exe", "C:\\Program Files (x86)\\GN Hearing\\Lucan\\App");
            
            test.Log(Status.Pass, "S&R Tool launched successfully");
            
            Thread.Sleep(2000);
            session.FindElementByName("Device Info").Click();
            Thread.Sleep(2000);

            if (device.Contains("RT") || device.Contains("RU"))
            {
                session.FindElementByName("Discover").Click();

                test.Log(Status.Pass, "Clicked on Discover.");

                session.SwitchTo().Window(session.WindowHandles.First());
                session.SwitchTo().ActiveElement();
                
                try
                {
                    session.FindElementByAccessibilityId("SerialNumberTextBox").SendKeys(DeviceNo);
                    lib.functionWaitForName(session, "Search");

                    test.Log(Status.Pass, "Clicked on Search");


                    /** Finding the HI till Disconnect button on the display **/

                    do
                    {
                        try
                        {
                            session.SwitchTo().Window(session.WindowHandles.First());

                            if (session.FindElementByName("Discover").Text == "Discover")
                            {
                                session.SwitchTo().Window(session.WindowHandles.First());
                                session.FindElementByName("Search").Click();
                            }
                        }
                        catch (Exception)
                        {

                        }

                    } while (!session.FindElementByName("Disconnect").Displayed);

                    session = lib.waitForElement(session, "Model Name");

                    test.Log(Status.Pass, "Dook2 Dev");
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
            ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\GN Hearing\\Lucan\\App\\Lucan.App.UI.exe", "C:\\Program Files (x86)\\GN Hearing\\Lucan\\App");
            session = lib.waitForElement(session, "OK");
            test.Log(Status.Pass, "Restore is successful.");
            var btncls = session.FindElementByAccessibilityId("PART_Close");
            btncls.Click();
            Thread.Sleep(1000);
        }


        /** Closes the S&R tool **/

        [Then(@"\[Close SandR tool]")]
        public void ThenCloseSandRTool()
        {
            ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\GN Hearing\\Lucan\\App\\Lucan.App.UI.exe", "C:\\Program Files (x86)\\GN Hearing\\Lucan\\App");
            var btncls = session.FindElementByAccessibilityId("PART_Close");
            btncls.Click();
            Thread.Sleep(1000);
        }


        /** Opens AlgoLabTest 
         *  Navigates to ADL window
         *  validates the ADL saved value **/


        [When(@"\[Launch algo lab and check the ADL value ""([^""]*)"" and ""([^""]*)""]")]
        public void WhenLaunchAlgoLabAndCheckTheADLValue(string device, string DeviceNo)
        {
            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());

            /** Verify AlgoTest Lab **/

            ModuleFunctions.checkADLValue(session, test, device, DeviceNo);
        }


        /** Opens FSW
          * Navigates to Fitting Screen
          * validates the FSW programs **/

        [Then(@"\[Launch FSW and check the added programs ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)""]")]
        public void ThenLaunchFSWAndCheckTheAddedPrograms(string device, string DeviceNo, string side)
        {
            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());
            Console.WriteLine("This is When method");

            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());

            FunctionLibrary lib = new FunctionLibrary();

            if(device.Contains("RT") || device.Contains("RU"))

            {
                ModuleFunctions.socketA(session, test, device);

                Console.WriteLine("This is When method");

                test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());


                ApplicationPath = "C:\\Program Files (x86)\\ReSound\\SmartFit\\SmartFitSA.exe";
                Thread.Sleep(2000);
                DesiredCapabilities appCapabilities = new DesiredCapabilities();
                appCapabilities.SetCapability("app", ApplicationPath);
                appCapabilities.SetCapability("deviceName", "WindowsPC");
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                Thread.Sleep(10000);
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                Thread.Sleep(2000);
                session.Manage().Window.Maximize();
                var wait = new WebDriverWait(session, TimeSpan.FromSeconds(20));
                var div = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("ListBoxItem")));
                var text_Button = session.FindElementsByClassName("ListBoxItem");

                test.Log(Status.Pass, "FSW is launched successfully");

                int counter = 0;
                string PatientName = null;
                string PatientDescription = null;
                foreach (var element in text_Button)
                {
                    if (counter == 3)
                    {
                        PatientName = element.GetAttribute("AutomationId");
                        PatientDescription = element.GetAttribute("Name");
                        break;
                    }

                    counter = counter + 1;
                }

                lib.clickOnAutomationId(session, PatientDescription, PatientName);

                /** Clicks on "Fit patient" button **/

                Thread.Sleep(8000);
                lib.waitForIdToBeClickable(session, "StandAloneAutomationIds.DetailsAutomationIds.FitAction");

                test.Pass("Patient is clicked");

                Thread.Sleep(10000);
                session.Close();

                /** To launch the return visit session **/

                ApplicationPath = "C:\\Program Files (x86)\\ReSound\\SmartFit\\SmartFit.exe";
                appCapabilities = new DesiredCapabilities();
                appCapabilities.SetCapability("app", ApplicationPath);
                appCapabilities.SetCapability("deviceName", "WindowsPC");
                Thread.Sleep(5000);
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                Thread.Sleep(10000);
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                Thread.Sleep(10000);

                /** Clicks on "Back" button **/

                session.FindElementByName("Back").Click();
                Thread.Sleep(5000);
                session.FindElementByAccessibilityId("ConnectionAutomationIds.CommunicationInterfaceItems").Click();
                Thread.Sleep(2000);

                /** Select Noah link, then click "Connect" **/

                session.FindElementByName("Noahlink Wireless").Click();
                lib.clickOnAutomationId(session, "Connect", "SidebarAutomationIds.ConnectAction");
                Thread.Sleep(10000);

                try
                {
                    if (session.FindElementByName("Unassign").Enabled)
                    {

                    }
                }
                catch (Exception e)
                {
                    lib.clickOnAutomationName(session, "Assign Instruments");
                    Thread.Sleep(5000);
                    var SN = session.FindElementsByClassName("ListBoxItem");
                    Thread.Sleep(10000);

                    /** The left side is assigned **/

                    foreach (WindowsElement value in SN)
                    {
                        string S = value.Text;
                        if (S.Contains(DeviceNo))
                        {
                            value.Text.Contains("Assign Left");
                            value.Click();
                        }
                    }
                }

                /** Choose the Keep Going "Continue"button in the Connection Flow  **/

                lib.clickOnAutomationName(session, "Continue");
                Thread.Sleep(15000);
            }


            if (device.Contains("LT") || device.Contains("RE"))
            {
                ApplicationPath = "C:\\Program Files (x86)\\ReSound\\SmartFit\\SmartFitSA.exe";
                Thread.Sleep(2000);
                DesiredCapabilities appCapabilities = new DesiredCapabilities();
                appCapabilities.SetCapability("app", ApplicationPath);
                appCapabilities.SetCapability("deviceName", "WindowsPC");
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                Thread.Sleep(10000);
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                Thread.Sleep(2000);
                session.Manage().Window.Maximize();
                var wait = new WebDriverWait(session, TimeSpan.FromSeconds(20));
                var div = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("ListBoxItem")));
                var text_Button = session.FindElementsByClassName("ListBoxItem");

                test.Log(Status.Pass, "FSW is launched successfully");

                int counter = 0;
                string PatientName = null;
                string PatientDescription = null;
                foreach (var element in text_Button)
                {
                    if (counter == 2)
                    {
                        PatientName = element.GetAttribute("AutomationId");
                        PatientDescription = element.GetAttribute("Name");
                        break;
                    }

                    counter = counter + 1;
                }

                lib.clickOnAutomationId(session, PatientDescription, PatientName);

                /** Clicks on Fit patient **/

                Thread.Sleep(4000);
                lib.clickOnAutomationId(session, "Fit Patient", "StandAloneAutomationIds.DetailsAutomationIds.FitAction");

                test.Pass("Patient is clicked");

                Thread.Sleep(8000);
                session.Close();

                /** To launch the return visit session **/

                ApplicationPath = "C:\\Program Files (x86)\\ReSound\\SmartFit\\SmartFit.exe";
                appCapabilities = new DesiredCapabilities();
                appCapabilities.SetCapability("app", ApplicationPath);
                appCapabilities.SetCapability("deviceName", "WindowsPC");
                Thread.Sleep(5000);
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                Thread.Sleep(10000);
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                Thread.Sleep(10000);

                try

                {
                    lib.clickOnElementWithIdonly(session, "ConnectionAutomationIds.ConnectAction");
                    test.Pass("Connect button is clicked");
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    test.Fail(e.Message);
                }

                Thread.Sleep(10000);


                try
                {  
                    /**  Clicks on "Back" button **/

                    session.FindElementByName("Back").Click();
                    Thread.Sleep(10000);
                    session.FindElementByAccessibilityId("ConnectionAutomationIds.CommunicationInterfaceItems").Click();
                    Thread.Sleep(2000);

                    /** Choose the Speed link and Click on Connect **/

                    session.FindElementByName("Speedlink").Click();
                    lib.clickOnAutomationId(session, "Connect", "SidebarAutomationIds.ConnectAction");
                }
                catch (Exception)
                { }
            }

            
                int buttonCount = 0;

                try
                {
                    WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(60));
                    waitForMe.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("Button")));

                    do
                    {
                        session.SwitchTo().ActiveElement();

                        if (buttonCount >= 1)
                        {
                            session.SwitchTo().ActiveElement();
                            session = ModuleFunctions.getControlsOfParentWindow(session, "ScrollViewer", test);
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
                    Thread.Sleep(4000);

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

                session.FindElementByName("Instrument").Click();
                Thread.Sleep(2000);
                session.FindElementByName("Read Instrument").Click();
                Thread.Sleep(2000);
            
                try
                {
                    session.FindElementByAccessibilityId("ProgramStripAutomationIds.ProgramSlot.P1").Click();

                    if ((session.FindElementByAccessibilityId("PART_Items").Text.ToString()).Contains("All-Around"))
                    {
                        Console.WriteLine("After listening test setting Value is :" + session.FindElementByAccessibilityId("PART_Items").Text.ToString());
                        test.Log(Status.Pass, "After listening test setting Value is :" + session.FindElementByAccessibilityId("PART_Items").Text.ToString());
                        Assert.Pass();
                        session.CloseApp();
                    }
                    else
                    {
                        test.Log(Status.Fail, "After listening test setting Value is :" + session.FindElementByAccessibilityId("PART_Items").Text.ToString());

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
                        test.Log(Status.Pass, "After listening test setting Value is :" + session.FindElementByAccessibilityId("PART_Items").Text.ToString());
                        Assert.Pass();
                    }

                    else
                    {
                        test.Log(Status.Fail, "After listening test setting Value is :" + session.FindElementByAccessibilityId("PART_Items").Text.ToString());
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
                        test.Log(Status.Pass, "After listening test setting Value is :" + session.FindElementByAccessibilityId("PART_Items").Text.ToString());
                        Assert.Pass();
                    }

                    else
                    {
                        test.Log(Status.Fail, "After listening test setting Value is :" + session.FindElementByAccessibilityId("PART_Items").Text.ToString());
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
                        test.Log(Status.Pass, "After listening test setting Value is :" + session.FindElementByAccessibilityId("PART_Items").Text.ToString());
                        Assert.Pass();
                    }

                    else
                    {
                        test.Log(Status.Fail, "After listening test setting Value is :" + session.FindElementByAccessibilityId("PART_Items").Text.ToString());
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
                    session.SwitchTo().ActiveElement();

                    /** Exit FSW with Out Saving **/

                    try
                    {
                        Thread.Sleep(2000);
                        lib.clickOnAutomationName(session, "Exit Without Saving");
                    }
                    catch (Exception e1)
                    {
                    }

                    test.Pass("Save is successfully done and Close the FSW");

                }
                catch (Exception ex)
                { }
               
                Thread.Sleep(8000);
                lib.processKill("SmartFitSA");
            }
        }



        /** To Verify the desired restoration time in log file **/

        [When(@"\[Go to log file for verifying Restore time]")]
        public void WhenGoToLogFileForVerifyingRestoreTime()
        {

            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());

            Thread.Sleep(1000);
            string path = (@"C:\Users\Public\Documents\Camelot\Logs\" + computer_name + "-" + user_name + "-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");
            FunctionLibrary lib = new FunctionLibrary();
            lib.fileVerify(path, test, "Restoring the hearing");
        }


        /** Reports for "captured" and "restored" data. **/

        [When(@"\[Open Capture and Restore report and log info in report]")]
        public void WhenOpenCaptureAndRestoreReportAndLogInfoInReport()
        {
            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());


            /** This is to check if Capture and Restore files are existing **/

            if (computer_name.Equals("FSWIRAY80"))
            {
                ModuleFunctions.verifyIfReportsExisted(test);

                /** Kill Acrobat reader **/

                Process[] processCollection = Process.GetProcesses();

                foreach (Process proc in processCollection)
                {
                    if (computer_name.Equals("FSWIRAY80"))
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


        [When(@"\[Verify StorageLayout Scenario By Changing Date and Confirm Cloud Icon ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)""]")]
        public void WhenVerifyStorageLayoutScenarioByChangingDateAndConfirmCloudIcon(string device, string side, string DeviceNo)

        {
            if (device.Contains("RT") || device.Contains("RU"))

            {
                try
                {
                    ModuleFunctions.socketA(session, test, device);
                }
                catch (Exception ex) { }

                Thread.Sleep(2000);

                string computer_name = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
                test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());


                session = ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\ReSound\\Dooku2.9.78.1\\StorageLayoutViewer.exe", "C:\\Program Files (x86)\\ReSound\\Dooku2.9.78.1");

                FunctionLibrary lib = new FunctionLibrary();

                Actions actions = new Actions(session);
                Thread.Sleep(5000);
                session.FindElementByAccessibilityId("FINDICON").Click();
                Thread.Sleep(15000);
                session.FindElementByAccessibilityId("FINDICON").Click();
                Thread.Sleep(15000);


                /** To select the Hi serial number **/

                var SN = session.FindElementsByClassName("TextBlock");

                foreach (WindowsElement value in SN)

                {
                    string S = value.Text;

                    if (S == DeviceNo)

                    {
                        value.Click();
                    }

                }



                lib.functionWaitForName(session, "Connect");
                lib.waitUntilElementExists(session, "File", 0);
                Thread.Sleep(4000);
                var ext = session.FindElements(WorkFlowPageFactory.fileMenu);
                ext[0].Click();
                Thread.Sleep(2000);
                ext = session.FindElements(WorkFlowPageFactory.readHI);
                actions = new Actions(session);
                actions.MoveToElement(ext[0]).Build().Perform();
                Thread.Sleep(2000);
                session.Keyboard.PressKey(Keys.Enter);
                Thread.Sleep(5000);

                /** Click on Uncheck button **/

                session.FindElementByName("Uncheck All").Click();
                Thread.Sleep(3000);
                session.FindElementByAccessibilityId("1001").Click();
                Thread.Sleep(2000);

                /** Choose the All option in drop down **/

                var rd = session.FindElementByName("All");
                actions.MoveToElement(rd).Click().Perform();
                Thread.Sleep(2000);

                /** To Click the Apply selection button **/

                session.FindElementByName("Apply selection").Click();
                Thread.Sleep(5000);
                var txt = session.FindElementByName("0f8e00:0004a ProductionTestData");
                txt.Click();              
                Thread.Sleep(4000);
                var data = session.FindElementByName("DataGridView");
                data.Click();
                var row = session.FindElementByName("Value  -   from FittingDongle:0/Left Row 0");
                row.Click();

                /**In order to passing the Date and Time in the Product Testdata **/

                row.SendKeys("2022-08-01 12:45:54Z");
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

                row.SendKeys("1652118942");
                Thread.Sleep(2000);
                min = session.FindElementByName("_Write to");
                min.Click();
                Thread.Sleep(3000);
                test.Pass("Writing Presets is done successfully.");
            }


            if (device.Contains("LT") || device.Contains("RE"))

            {
                if (device.Contains("LT"))

                {
                    test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());
                    FunctionLibrary lib = new FunctionLibrary();
                    InputSimulator sim = new InputSimulator();
                    session = ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\ReSound\\Palpatine6.7.4.21-RP-S\\StorageLayoutViewer.exe", "C:\\Program Files (x86)\\ReSound\\Palpatine6.7.4.21-RP-S");                 
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

                    test.Log(Status.Pass, side + ": is selected");

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
                        test.Log(Status.Pass, "3e900:004c ProductionTestData " + "is selected");

                        foreach (var item in txt)
                        {
                            Console.WriteLine(item.GetAttribute("Name"));
                            item.Click();
                            test.Log(Status.Pass, "3e900:004c ProductionTestData " + "is selected");
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
                        childTable[0].SendKeys("2022-08-01 12:45:54Z");
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
                            test.Pass("2a000:0026 MiniIdentification is selected");
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
                            test.Log(Status.Pass, "Saved value for date +" + item.Text);
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
                        childTable[0].SendKeys("1652118942");
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
                        session.SwitchTo().ActiveElement();

                        try
                        {
                            do
                            {

                            } while (session.FindElementByAccessibilityId("WorkerDialog").Enabled);

                        }

                        catch (Exception e)
                        {
                            test.Pass("Writing Presets is done successfully.");
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

                        test.Log(Status.Pass, "3e900:004c ProductionTestData " + "is selected");

                        foreach (var item in txt)
                        {
                            Console.WriteLine(item.GetAttribute("Name"));
                            item.Click();
                            test.Log(Status.Pass, "3e900:004c ProductionTestData " + "is selected");
                        }

                        Thread.Sleep(2000);


                        var data = session.FindElementByName("DataGridView");
                        data.Click();
                        var row = session.FindElementByName("Value Row 0");
                        row.Click();
                        row.SendKeys("2022-08-01 12:45:54Z");
                        var min = session.FindElementByName("2a000:0026 MiniIdentification");
                        min.Click();
                        Thread.Sleep(2000);
                        data = session.FindElementByName("DataGridView");
                        data.Click();
                        row = session.FindElementByName("Row 6");
                        row.Click();
                        row.SendKeys("1652118942");
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
                            test.Pass("Writing Presets is done successfully.");
                        }
                    }
                }



                else if (device.Contains("RE"))

                {

                    if (side.Equals("Left"))
                    {
                        test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());
                        session= ModuleFunctions.storagelayoutD1(session, test, device, side);
                    }


                    else
                    {
                        test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());
                        session= ModuleFunctions.storagelayoutD1(session, test, device, side);
                    }
                }
            }



          session.CloseApp();

        }   

    }
}
            

                

            



               






    
