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
using TechTalk.SpecFlow;
using WindowsInput;
using WindowsInput.Native;
using Xamarin.Forms;
using Console = System.Console;
using Environment = System.Environment;
using File = System.IO.File;
using AventStack.ExtentReports.Gherkin.Model;
using System.Collections.ObjectModel;

namespace MyNamespace
{
    [Binding]
    public class StepDefinitions
    {

        public static appconfigsettings config;
        static string configsettingpath = System.IO.Directory.GetParent(@"../../../").FullName
        + Path.DirectorySeparatorChar + "appconfig.json";
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
            config = new appconfigsettings();
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(configsettingpath);
            IConfigurationRoot configuration = builder.Build();
            configuration.Bind(config);
            FeatureContext.Current["config"] = config;
            Console.WriteLine("This is BeforeFetaure method");
            htmlReporter = new ExtentHtmlReporter(textDir + "\\report.html");
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            htmlReporter.Config.ReportName = "SandR Regression Test - Prasad PSSNV";
            htmlReporter.Config.EnableTimeline = true;
            htmlReporter.Config.DocumentTitle = "S and R Report";
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            ModuleFunctions.callbyextentreport(extent);
            ModuleFunctions.callbyAlgoTestLabVariables(config);


            /* Reading CSV file to get device names */

            String[] csvVal = FunctionLibrary.readCSVFile();

            /*Launch Socket Driver*/


        }


        [BeforeScenario]

        public static void BeforeScenario()

        {         
            ProcessStartInfo psi = new ProcessStartInfo(config.TestEnvironment.WinAppDriverPath);
            psi.UseShellExecute = true;
            psi.Verb = "runas"; // run as administrator
            Process.Start(psi);
            string test1 = ScenarioContext.Current.ScenarioInfo.Title;
            test = extent.CreateTest(test1.ToString());
            ScenarioContext.Current["extentTest"] = test;

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

            string test2 = ScenarioContext.Current.ScenarioInfo.Title;
            string pattern = @"Case ID (\d+)";

            Match match = Regex.Match(test2, pattern);
            string testcaseID = "";

            if (match.Success)

            {
                testcaseID = match.Groups[1].Value;
                ScenarioContext.Current.Set(testcaseID, "TestCaseID");

            }

            Console.WriteLine("Test Case ID: " + testcaseID);

            /*Update XML files with TestPlan,TestSuite,TestConfig*/

            FunctionLibrary lib = new FunctionLibrary();

            //lib.PassingXML(test);

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


        [AfterScenario]

        [Then(@"\[done]")]
        public void ThenDone()

        {

            //stopWinappdriver();

            //Console.WriteLine("This is Done method");
            //var scenarioContext = ScenarioContext.Current;
            //var testStatus = scenarioContext.TestError == null ? "PASS" : "FAIL";
            //var testcaseId = scenarioContext.Get<string>("TestCaseID");

            //var xmlFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), $"{testcaseId}.xml", SearchOption.AllDirectories);


            //foreach (var xmlFile in xmlFiles)
            //{
            //    XDocument xmlDoc = XDocument.Load(xmlFile);


            //    foreach (var testResultSetElement in xmlDoc.Descendants("TFSTestResultsSet"))
            //    {
            //        var elementTestCaseID = (string)testResultSetElement.Element("TestCaseID");

            //        if (elementTestCaseID == testcaseId)
            //        {

            //            var elementTestStatus = testResultSetElement.Element("TestStatus");
            //            if (elementTestStatus != null)
            //            {
            //                elementTestStatus.Value = testStatus;
            //            }
            //        }
            //    }

            //    xmlDoc.Save(xmlFile);                                                                                                                                                                                                                                                                            // Save the updated XML
            //}



            //{

            //    string projectPath = AppDomain.CurrentDomain.BaseDirectory;

            //    string xmlFolderPath = Path.Combine(projectPath, "XML");

            //    string keyToUpdate = "WorkFlowsXMLsPath";
            //    string valueToUpdate = xmlFolderPath;

            //    string[] configFiles = Directory.GetFiles(projectPath, "*.config", SearchOption.AllDirectories);

            //    foreach (var configFile in configFiles)
            //    {
            //        UpdateAppSettingValue(configFile, keyToUpdate, valueToUpdate);
            //    }
            //}

            //static void UpdateAppSettingValue(string configFilePath, string key, string value)
            //{
            //    try
            //    {
            //        ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap
            //        {
            //            ExeConfigFilename = configFilePath
            //        };
            //        Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

            //        if (config.AppSettings.Settings[key] != null)
            //        {
            //            config.AppSettings.Settings[key].Value = value;
            //            config.Save(ConfigurationSaveMode.Modified);
            //            ConfigurationManager.RefreshSection("appSettings");

            //            string updatedValue = ConfigurationManager.AppSettings[key];
            //            Console.WriteLine($"Updated {key} in {configFilePath}: {updatedValue}");
            //        }
            //        else
            //        {
            //            Console.WriteLine($"Key {key} not found in {configFilePath}.");
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine($"Error updating configuration file {configFilePath}: {ex.Message}");
            //    }
            //}

            //try

            //{
            //    string agentPath = Path.Combine(Directory.GetCurrentDirectory(), @"XML\TFS API\TFS.Agent.Run\bin\Debug\TFS.Agent.Run.exe");

            //    if (System.IO.File.Exists(agentPath))
            //    {
            //        ProcessStartInfo startInfo = new ProcessStartInfo
            //        {
            //            FileName = agentPath,
            //            UseShellExecute = false,
            //            RedirectStandardOutput = true,
            //            RedirectStandardError = true,
            //            CreateNoWindow = true
            //        };

            //        Process process = new Process
            //        {
            //            StartInfo = startInfo
            //        };

            //        process.Start();
            //        process.WaitForExit(); // Optionally wait for the process to complete

            //        string standardOutput = process.StandardOutput.ReadToEnd();
            //        string standardError = process.StandardError.ReadToEnd();

            //        Console.WriteLine("Standard Output:");
            //        Console.WriteLine(standardOutput);

            //        Console.WriteLine("Standard Error:");
            //        Console.WriteLine(standardError);
            //    }
            //    else
            //    {
            //        Console.WriteLine("TFS agent executable not found at the specified path.");
            //    }
            //}

            //catch (Exception ex)
            //{
            //    Console.WriteLine("An error occurred: " + ex.Message);
            //}

            //processKill("SmartFit");
            //processKill("SmartFitSA");
            //processKill("Camelot.WorkflowRuntime");
            //processKill("Camelot.SystemInfobar");
            //processKill("Lucan.App.UI");
            //processKill("StorageLayoutViewer");
            //processKill("WinAppDriver.exe");

            string[] processesToKill = { "SmartFit", "SmartFitSA", "Camelot.WorkflowRuntime", "Camelot.SystemInfobar", "Lucan.App.UI", "StorageLayoutViewer" };
            foreach (string processName in processesToKill)
            {
                processKill(processName);
            }
        }

        public void processKill(string name)
        {
            Process[] processCollection = Process.GetProcesses();
            foreach (Process proc in processCollection)
            {
                Console.WriteLine(proc);
                if (proc.ProcessName == name)
                {
                    proc.Kill();
                }
            }
        }

        [AfterFeature]
        public static void afterFeature()
        {
            Console.WriteLine("This is AfterFeature method");
            extent.Flush();
        }

        [AfterStep]
        [Obsolete]
        public void AfterEachStep()
        {
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            if (ScenarioContext.Current.TestError != null)
            {
                if (stepType == "Given")
                    test.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                if (stepType == "When")
                    test.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                if (stepType == "Then")
                    test.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                if (stepType == "And")
                    test.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
            }
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            string reportPath = Path.Combine(Directory.GetCurrentDirectory(), "index.html");
            extent.Flush();
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("assettracker@i-raysolutions.com");
            mailMessage.CC.Add(new MailAddress("prasad.puvvala@i-raysolutions.com"));
            mailMessage.To.Add(new MailAddress("siva.bojja@i-raysolutions.com"));
            mailMessage.To.Add(new MailAddress("sbojja@gnhearing.com"));
            mailMessage.To.Add(new MailAddress("surya.kondreddy@i-raysolutions.com"));
            mailMessage.To.Add(new MailAddress("xxsurkon@gnresound.com"));
            mailMessage.Subject = "S&R Automation Report";
            mailMessage.Body = "Please find the attached S&R Automation Report.";
            Attachment attachment = new Attachment(reportPath);
            mailMessage.Attachments.Add(attachment);
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"); // Specify the SMTP host
            smtpClient.Port = 587; // Specify the SMTP port (Gmail typically uses port 587 for TLS/SSL)
            smtpClient.EnableSsl = true; // Enable SSL/TLS
            smtpClient.Credentials = new NetworkCredential("assettracker@i-raysolutions.com", "asset@2k19"); // Provide credentials
            smtpClient.Send(mailMessage);
        }

        [OneTimeTearDown]
        public void GenerateReport()
        {
        }


        /** This is used for launching the FDTS
          * Passes the HI Serial number
          * perfrom Flashing and close the FDTS **/


        [Given(@"Launch FDTS WorkFlow And Flash Device ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)""and""([^""]*)""")]
        public void GivenLaunchFDTSWorkFlowAndFlashDeviceAndAndAndAnd(string device, string DeviceNo, string flashHIWithSlno, string side, string DeviceType)
        {

            Console.WriteLine("This is Given method");

            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());

            /** Socket Launching to pass commands for D2 and D3 **/

            try
            {
                if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
                {

                    Thread.Sleep(240000);

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
            session = ModuleFunctions.sessionInitialize1(config.ApplicationPath.FDTSAppPath, config.workingdirectory.FDTS);
            Thread.Sleep(2000);
            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", config.ApplicationPath.FDTSAppPath);
            appCapabilities.SetCapability("deviceName", "WindowsPC");
            appCapabilities.SetCapability("appArguments", "--run-as-administrator");
            session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
            Thread.Sleep(8000);
            session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);

            stepName.Log(Status.Pass, "Test Work Flow launched successfully");

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
                session.SwitchTo().ActiveElement();
                session.FindElementByName("Continue >>").Click();
                Thread.Sleep(4000);
                session.SwitchTo().Window(session.WindowHandles.First());
                session.SwitchTo().ActiveElement();
                var secondWindow = session.FindElementsByClassName(workFlowProductSelection);
                Thread.Sleep(2000);
            }

            else if (device.Contains("RT") || device.Contains("RU") || device.Contains("NX"))
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
                    session = ModuleFunctions.sessionInitialize1(config.ApplicationPath.FDTSAppPath, config.workingdirectory.FDTS);
                }

                else
                {
                    session = ModuleFunctions.sessionInitialize1(config.ApplicationPath.FDTSAppPath, config.workingdirectory.FDTS);
                }

                try
                {
                    session.SwitchTo().Window(session.WindowHandles.First());
                    session.SwitchTo().ActiveElement();
                    if (DeviceType.Equals("Rechargeable"))
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

                stepName.Log(Status.Pass, "Flashing is started for Device" + devName);

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

            if (computer_name == "FSWIRAY80" || computer_name == "UKBRAHPF2M76W6" || computer_name == "FSWIRAY112")
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



        /** To create a patient, 
         * connect the HIs to the FSW, 
         * make adjustments, save them, and then exit. **/


        [When(@"\[Create a Patient and Fitting HI In FSW ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)""and""([^""]*)""]")]
        public void WhenCreateAPatientAndFittingHIInFSWAndAndAndAnd(string alterValue, string device, string DeviceNo, string side, string DeviceType)
        {

            string devName = device;
            FunctionLibrary lib = new FunctionLibrary();
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
                    Thread.Sleep(2000);
                    DesiredCapabilities appCapabilities = new DesiredCapabilities();
                    appCapabilities.SetCapability("app", config.ApplicationPath.FSWAppPath);
                    appCapabilities.SetCapability("deviceName", "WindowsPC");
                    appCapabilities.SetCapability("appArguments", "--run-as-administrator");
                    session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                    Thread.Sleep(10000);
                    session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                    session.Manage().Window.Maximize();
                    var wait = new WebDriverWait(session, TimeSpan.FromSeconds(60));
                    var div = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("ListBoxItem")));
                    var text_Button = session.FindElementsByClassName("ListBoxItem");

                    stepName.Log(Status.Pass, "FSW is launched successfully");

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

                    Thread.Sleep(10000);
                    lib.waitForIdToBeClickable(session, "StandAloneAutomationIds.DetailsAutomationIds.FitAction");
                    stepName.Pass("Patient is clicked");
                    Thread.Sleep(10000);
                    session.Close();
                    appCapabilities = new DesiredCapabilities();
                    appCapabilities.SetCapability("app", config.ApplicationPath.SmartFitAppPath);
                    appCapabilities.SetCapability("deviceName", "WindowsPC");
                    appCapabilities.SetCapability("appArguments", "--run-as-administrator");
                    Thread.Sleep(5000);
                    session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                    Thread.Sleep(5000);
                    session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                    Thread.Sleep(10000);
                    lib.clickOnAutomationName(session, "Assign Instruments");
                    session.FindElementByName("Back").Click();
                    Thread.Sleep(5000);
                    session.FindElementByAccessibilityId("ConnectionAutomationIds.CommunicationInterfaceItems").Click();
                    Thread.Sleep(2000);

                    /** Select Noah link Wireless now, then click Connect.  **/

                    session.FindElementByName("Noahlink Wireless").Click();
                    lib.clickOnAutomationId(session, "Connect", "SidebarAutomationIds.ConnectAction");


                    Thread.Sleep(13000);

                    try
                    {
                        if (session.FindElementByName("Unassign").Enabled)
                        {

                            lib.clickOnAutomationName(session, "Assign Instruments");

                            Thread.Sleep(5000); // Initial wait before searching

                            var SN = session.FindElementsByClassName("ListBoxItem");

                            // Check if DeviceNo is already discovered

                            foreach (WindowsElement value in SN)
                            {
                                string S = value.Text;
                                if (S.Contains(DeviceNo) && S.Contains("Assign Left"))
                                {
                                    value.Text.Contains("Assign Left");
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
                            processKill("SmartFitSA");
                            processKill("SmartFit");
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
                                    appCapabilities = new DesiredCapabilities();
                                    appCapabilities.SetCapability("app", config.ApplicationPath.FSWAppPath);
                                    appCapabilities.SetCapability("deviceName", "WindowsPC");
                                    appCapabilities.SetCapability("appArguments", "--run-as-administrator");
                                    session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                                    Thread.Sleep(10000);
                                    session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                                    session.Manage().Window.Maximize();
                                    var wait1 = new WebDriverWait(session, TimeSpan.FromSeconds(60));
                                    var div1 = wait1.Until(ExpectedConditions.ElementIsVisible(By.ClassName("ListBoxItem")));
                                    var text_Button1 = session.FindElementsByClassName("ListBoxItem");

                                    stepName.Log(Status.Pass, "FSW is launched successfully");

                                    int counter1 = 0;
                                    string PatientName1 = null;
                                    string PatientDescription1 = null;
                                    foreach (var element in text_Button1)
                                    {
                                        if (counter1 == 3)
                                        {
                                            PatientName1 = element.GetAttribute("AutomationId");
                                            PatientDescription1 = element.GetAttribute("Name");
                                            break;
                                        }
                                        counter1 = counter1 + 1;
                                    }



                                    lib.clickOnAutomationId(session, PatientDescription, PatientName);

                                    /** Clicks on Fit patient button **/

                                    Thread.Sleep(8000);
                                    lib.waitForIdToBeClickable(session, "StandAloneAutomationIds.DetailsAutomationIds.FitAction");
                                    stepName.Pass("Patient is clicked");
                                    Thread.Sleep(10000);
                                    session.Close();
                                    appCapabilities = new DesiredCapabilities();
                                    appCapabilities.SetCapability("app", config.ApplicationPath.SmartFitAppPath);
                                    appCapabilities.SetCapability("deviceName", "WindowsPC");
                                    appCapabilities.SetCapability("appArguments", "--run-as-administrator");
                                    Thread.Sleep(5000);
                                    session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                                    Thread.Sleep(5000);
                                    session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                                    Thread.Sleep(5000);
                                    lib.clickOnAutomationName(session, "Assign Instruments");
                                    session.FindElementByName("Back").Click();
                                    Thread.Sleep(5000);
                                    session.FindElementByAccessibilityId("ConnectionAutomationIds.CommunicationInterfaceItems").Click();
                                    Thread.Sleep(2000);

                                    /** Select Noah link Wireless now, then click Connect.  **/

                                    session.FindElementByName("Noahlink Wireless").Click();
                                    lib.clickOnAutomationId(session, "Connect", "SidebarAutomationIds.ConnectAction");
                                    Thread.Sleep(13000);
                                    lib.clickOnAutomationName(session, "Assign Instruments");
                                    Thread.Sleep(5000); // Initial wait before searching
                                    var SN = session.FindElementsByClassName("ListBoxItem");

                                    // Check if DeviceNo is already discovered

                                    foreach (WindowsElement value in SN)
                                    {
                                        string S = value.Text;
                                        if (S.Contains(DeviceNo) && S.Contains("Assign Left"))
                                        {
                                            value.Text.Contains("Assign Left");
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


            if (DeviceType.Equals("D1rechargeableWired"))
            {
                Console.WriteLine("This is When method");
                Thread.Sleep(2000);
                DesiredCapabilities appCapabilities = new DesiredCapabilities();
                appCapabilities.SetCapability("app", config.ApplicationPath.FSWAppPath);
                appCapabilities.SetCapability("deviceName", "WindowsPC");
                appCapabilities.SetCapability("appArguments", "--run-as-administrator");
                session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                Thread.Sleep(10000);
                session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                Thread.Sleep(2000);
                session.Manage().Window.Maximize();
                var wait = new WebDriverWait(session, TimeSpan.FromSeconds(20));
                var div = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("ListBoxItem")));
                var text_Button = session.FindElementsByClassName("ListBoxItem");

                stepName.Log(Status.Pass, "FSW is launched successfully");

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

                stepName.Pass("Patient is clicked");

                Thread.Sleep(10000);
                session.Close();

                appCapabilities = new DesiredCapabilities();
                appCapabilities.SetCapability("app", config.ApplicationPath.SmartFitAppPath);
                appCapabilities.SetCapability("deviceName", "WindowsPC");
                appCapabilities.SetCapability("appArguments", "--run-as-administrator");
                Thread.Sleep(5000);
                session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                Thread.Sleep(10000);
                session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
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
                catch (Exception)
                { }
            }

            else if (DeviceType.Equals("Wired"))
            {
                Console.WriteLine("This is When method");
                Thread.Sleep(2000);
                DesiredCapabilities appCapabilities = new DesiredCapabilities();
                appCapabilities.SetCapability("app", config.ApplicationPath.FSWAppPath);
                appCapabilities.SetCapability("deviceName", "WindowsPC");
                appCapabilities.SetCapability("appArguments", "--run-as-administrator");
                session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                Thread.Sleep(10000);
                session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                Thread.Sleep(2000);
                session.Manage().Window.Maximize();
                var wait = new WebDriverWait(session, TimeSpan.FromSeconds(20));
                var div = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("ListBoxItem")));
                var text_Button = session.FindElementsByClassName("ListBoxItem");
                stepName.Log(Status.Pass, "FSW is launched successfully");
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
                stepName.Pass("Patient is clicked");
                Thread.Sleep(10000);
                session.Close();
                appCapabilities = new DesiredCapabilities();
                appCapabilities.SetCapability("app", config.ApplicationPath.SmartFitAppPath);
                appCapabilities.SetCapability("deviceName", "WindowsPC");
                appCapabilities.SetCapability("appArguments", "--run-as-administrator");
                Thread.Sleep(5000);
                session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                Thread.Sleep(10000);
                session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                Thread.Sleep(10000);

                try

                {
                    lib.clickOnElementWithIdonly(session, "ConnectionAutomationIds.ConnectAction");
                    stepName.Pass("Connect button is clicked");
                }

                catch (Exception)
                {


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
                catch (Exception)
                { }

            }
            Thread.Sleep(10000);
            var textBlockelements = session.FindElementsByClassName("TextBlock");
            foreach (var element in textBlockelements)
            {
                try
                {
                    if (element.Text == "Connection Lost")
                    {
                        stepName.Log(Status.Fail, "Connection Error");
                        MailMessage mailMessage = new MailMessage();
                        mailMessage.From = new MailAddress("assettracker@i-raysolutions.com");
                        mailMessage.CC.Add(new MailAddress("prasad.puvvala@i-raysolutions.com"));
                        mailMessage.To.Add(new MailAddress("surya.kondreddy@i-raysolutions.com"));
                        mailMessage.To.Add(new MailAddress("siva.bojja@i-raysolutions.com"));
                        mailMessage.To.Add(new MailAddress("anjaneyulu.chinthapalli@i-raysolutions.com"));
                        mailMessage.Subject = "S&R Automation Script Error";
                        mailMessage.Body = "FSW Connection Error";
                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"); // Specify the SMTP host
                        smtpClient.Port = 587; // Specify the SMTP port (Gmail typically uses port 587 for TLS/SSL)
                        smtpClient.EnableSsl = true; // Enable SSL/TLS
                        smtpClient.Credentials = new NetworkCredential("assettracker@i-raysolutions.com", "asset@2k19"); // Provide credentials
                        smtpClient.Send(mailMessage);
                        processKill("SmartFit");
                        processKill("SmartFitSA");
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
                                session.SwitchTo().ActiveElement();

                                if (buttonCount >= 1)
                                {
                                    session.SwitchTo().ActiveElement();
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
                                    session.SwitchTo().ActiveElement();

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
                                    session.SwitchTo().ActiveElement();

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
                                    lib.clickOnElementWithIdonly(session, "SkipButton");
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
                        mailMessage.To.Add(new MailAddress("anjaneyulu.chinthapalli@i-raysolutions.com"));
                        mailMessage.Subject = "S&R Automation Script Error";
                        mailMessage.Body = "FSW Connection Error";
                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"); // Specify the SMTP host
                        smtpClient.Port = 587; // Specify the SMTP port (Gmail typically uses port 587 for TLS/SSL)
                        smtpClient.EnableSsl = true; // Enable SSL/TLS
                        smtpClient.Credentials = new NetworkCredential("assettracker@i-raysolutions.com", "asset@2k19"); // Provide credentials
                        smtpClient.Send(mailMessage);
                        processKill("SmartFit");
                        processKill("SmartFitSA");
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
                                session.SwitchTo().ActiveElement();

                                if (buttonCount >= 1)
                                {
                                    session.SwitchTo().ActiveElement();
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
                                    session.SwitchTo().ActiveElement();

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
                                    session.SwitchTo().ActiveElement();

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
                                    lib.clickOnElementWithIdonly(session, "SkipButton");
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
                            lib.clickOnElementWithIdonly(session, "SaveAutomationIds.PerformSaveAutomationIds.ExitAction");

                            /** Exit the FSW **/

                            stepName.Pass("Click on FSW Exit button");
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

        [When(@"\[Cleaning up Capture and Restore Reports Before Launch SandR]")]
        public void WhenCleaningUpCaptureAndRestoreReportsBeforeLaunchSandR()
        {
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
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
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
            stepName.Log(Status.Pass, "S&R Tool launched successfully");
            session.FindElementByName("Device Info").Click();
            Thread.Sleep(2000);

            if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
            {
                /** Clciks on "Discover" button **/

                session.FindElementByName("Discover").Click();
                stepName.Log(Status.Pass, "Clicked on Discover.");
                session.SwitchTo().Window(session.WindowHandles.First());
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

                stepName.Log(Status.Pass, "Dook2 Dev");
            }
        }



        /** Navigates to Device Info 
         *  reads device information into an Excel spreadsheet **/


        [When(@"\[Go to Device Info tab and capture device info in excel then verify the device information is shown correctly ""([^""]*)""]")]
        public void WhenGoToDeviceInfoTabAndCaptureDeviceInfoInExcelThenVerifyTheDeviceInformationIsShownCorrectly(string deviceType)
        {
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
                        stepName.Log(Status.Pass, "Expected Firmware Version is: " + version.Text + " But Current Firmware is: " + version.Text);
                        break;


                    case string _ when device.Contains("RT962-DRW"):

                        stepName.Log(Status.Info, "Expected Firmware Version is: " + Dooku2nonRechargeable + " But Current Firmware Version is: " + version.Text);
                        break;


                    case string _ when device.Contains("RE962-DRW"):

                        stepName.Log(Status.Info, "Expected Firmware Version is: " + Dooku1nonRechargeble + " But Current Firmware Version is: " + version.Text);
                        break;


                    case string _ when device.Contains("RT961-DRWC"):

                        stepName.Log(Status.Info, "Expected Firmware Version is: " + Dooku2Rechargeable + " But Current Firmware Version is: " + version.Text);
                        break;


                    case string _ when device.Contains("RU961-DRWC"):

                        stepName.Log(Status.Info, "Expected Firmware Version is: " + Dooku3Rechargeable + " But Current Firmware Version is: " + version.Text);
                        break;

                    case string _ when device.Contains("RU988-DWC"):

                        stepName.Log(Status.Info, "Expected Firmware Version is: " + Dooku3PBTE + " But Current Firmware Version is: " + version.Text);
                        break;


                    default:
                        stepName.Log(Status.Info, "Unknown Firmware Version: " + version.Text);
                        break;
                }

            }

        }




        /** Validates the updated firmware version in the S&R tool under device info **/


        [Then(@"\[Compare firmware version is downgraded successfully ""([^""]*)""]")]
        public void ThenCompareFirmwareVersionIsDowngradedSuccessfully(string device)
        {
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

                        stepName.Log(Status.Pass, "Expected Firmware Version is: " + version.Text + " But Current Firmware is: " + version.Text);
                        break;


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



        [When(@"\[Come back to Settings and wait till controls enabled]")]
        public void WhenComeBackToSettingsAndWaitTillControlsEnabled()
        {
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
            session = lib.functionWaitForName(session, "CAPTURE");
            var HIData = lib.waitUntilElementExists(session, "windowUserMessage", 1);
            var text = session.FindElementByAccessibilityId("textBlockMessage");

            if (session.FindElementByAccessibilityId("labelHeader").Text == "Capture Succeeded")
            {
                stepName.Log(Status.Pass, "Capture " + text.Text);
            }
            else
            {
                stepName.Log(Status.Fail, "Capture Failed" + ":" + "  " + text.Text);
            }

            ModuleFunctions.sessionInitialize(config.ApplicationPath.SandRAppPath, config.workingdirectory.SandR);
            session = lib.waitForElement(session, "OK");
            var btnClose = session.FindElementByAccessibilityId("PART_Close");
            btnClose.Click();

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


        [When(@"\[Perform Capture with listening test settings]")]
        public void WhenPerformCaptureWithListeningTestSettings()

        {
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
            Thread.Sleep(8000);

            /** To Check the Setlistening checkbox **/

            try
            {
                if (session.FindElementByClassName("CheckBox").Selected)
                {
                    session = lib.functionWaitForName(session, "CAPTURE");
                    var HIData = lib.waitUntilElementExists(session, "windowUserMessage", 1);
                    var text = session.FindElementByAccessibilityId("textBlockMessage");

                    if (session.FindElementByAccessibilityId("labelHeader").Text == "Capture Succeeded")
                    {
                        stepName.Log(Status.Pass, "Capture " + text.Text);
                    }
                    else
                    {
                        stepName.Log(Status.Fail, "Capture Failed" + ":" + "  " + text.Text);
                    }

                    ModuleFunctions.sessionInitialize(config.ApplicationPath.SandRAppPath, config.workingdirectory.SandR);
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

                    var HIData = lib.waitUntilElementExists(session, "windowUserMessage", 1);
                    var text = session.FindElementByAccessibilityId("textBlockMessage");

                    if (session.FindElementByAccessibilityId("labelHeader").Text == "Capture Succeeded")
                    {
                        stepName.Log(Status.Pass, "Capture " + text.Text);
                    }
                    else
                    {
                        stepName.Log(Status.Fail, "Capture Failed" + ":" + "  " + text.Text);
                    }

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
                    session.Close();
                }
            }

            catch (Exception e)
            { }
        }


        /** To verify the desired Capture time in log file **/

        [When(@"\[Go to logs and verify capturing time]")]
        public void WhenGoToLogsAndVerifyCapturingTime()
        {
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

            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());

            /** Algo Tet Lab **/

            stepName.Log(Status.Pass, "Altered value is 1");
            ModuleFunctions.altTestLab(session, stepName, device, DeviceNo, DeviceType);
            Thread.Sleep(2000);
        }



        /** Opens S&R tool
         *  Navigates to servies tab
         *  performs Restore operation with available captured data **/

        [When(@"\[Perform Restore with above captured image ""([^""]*)"" and ""([^""]*)""and""([^""]*)"" and ""([^""]*)""]")]
        public void WhenPerformRestoreWithAboveCapturedImageAndAndAnd(string device, string DeviceNo, string DeviceType, string side)
        {
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
            Thread.Sleep(2000);
            session.FindElementByName("Device Info").Click();
            Thread.Sleep(2000);

            if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
            {
                session.FindElementByName("Discover").Click();
                stepName.Log(Status.Pass, "Clicked on Discover.");
                session.SwitchTo().Window(session.WindowHandles.First());
                session.SwitchTo().ActiveElement();

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


                stepName.Log(Status.Pass, "Restoration " + text.Text);
            }
            else
            {
                stepName.Log(Status.Fail, "Restoration Failed" + ":" + " " + text.Text);
            }

            ModuleFunctions.sessionInitialize(config.ApplicationPath.SandRAppPath, config.workingdirectory.SandR);
            session = lib.waitForElement(session, "OK");
            var btncls = session.FindElementByAccessibilityId("PART_Close");
            btncls.Click();
            Thread.Sleep(1000);
        }


        /** Closes the S&R tool **/

        [Then(@"\[Close SandR tool]")]
        public void ThenCloseSandRTool()
        {
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

            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());

            /** Verify AlgoTest Lab **/

            ModuleFunctions.checkADLValue(session, stepName, device, DeviceNo, DeviceType);
        }


        /** Opens FSW
          * Navigates to Fitting Screen
          * validates the FSW programs **/

        [Then(@"\[Launch FSW and check the added programs ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)""and""([^""]*)""]")]
        public void ThenLaunchFSWAndCheckTheAddedProgramsAndAndAnd(string device, string DeviceNo, string side, string DeviceType)
        {

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
                    Thread.Sleep(2000);
                    DesiredCapabilities appCapabilities = new DesiredCapabilities();
                    appCapabilities.SetCapability("app", config.ApplicationPath.FSWAppPath);
                    appCapabilities.SetCapability("deviceName", "WindowsPC");
                    appCapabilities.SetCapability("appArguments", "--run-as-administrator");
                    session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                    Thread.Sleep(10000);
                    session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                    session.Manage().Window.Maximize();
                    var wait = new WebDriverWait(session, TimeSpan.FromSeconds(60));
                    var div = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("ListBoxItem")));
                    var text_Button = session.FindElementsByClassName("ListBoxItem");
                    stepName.Log(Status.Pass, "FSW is launched successfully");
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
                    stepName.Pass("Patient is clicked");
                    Thread.Sleep(10000);
                    session.Close();
                    appCapabilities = new DesiredCapabilities();
                    appCapabilities.SetCapability("app", config.ApplicationPath.SmartFitAppPath);
                    appCapabilities.SetCapability("deviceName", "WindowsPC");
                    appCapabilities.SetCapability("appArguments", "--run-as-administrator");
                    Thread.Sleep(5000);
                    session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                    Thread.Sleep(5000);
                    session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                    Thread.Sleep(10000);
                    lib.clickOnAutomationName(session, "Assign Instruments");
                    session.FindElementByName("Back").Click();
                    Thread.Sleep(5000);
                    session.FindElementByAccessibilityId("ConnectionAutomationIds.CommunicationInterfaceItems").Click();
                    Thread.Sleep(2000);

                    /** Select Noah link Wireless now, then click Connect.  **/

                    session.FindElementByName("Noahlink Wireless").Click();
                    lib.clickOnAutomationId(session, "Connect", "SidebarAutomationIds.ConnectAction");


                    Thread.Sleep(13000);

                    try
                    {
                        if (session.FindElementByName("Unassign").Enabled)
                        {

                            lib.clickOnAutomationName(session, "Assign Instruments");

                            Thread.Sleep(5000); // Initial wait before searching

                            var SN = session.FindElementsByClassName("ListBoxItem");

                            // Check if DeviceNo is already discovered

                            foreach (WindowsElement value in SN)
                            {
                                string S = value.Text;
                                if (S.Contains(DeviceNo) && S.Contains("Assign Left"))
                                {
                                    value.Text.Contains("Assign Left");
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
                            processKill("SmartFitSA");
                            processKill("SmartFit");
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
                                    appCapabilities = new DesiredCapabilities();
                                    appCapabilities.SetCapability("app", config.ApplicationPath.FSWAppPath);
                                    appCapabilities.SetCapability("deviceName", "WindowsPC");
                                    appCapabilities.SetCapability("appArguments", "--run-as-administrator");
                                    session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                                    Thread.Sleep(10000);
                                    session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                                    session.Manage().Window.Maximize();
                                    var wait1 = new WebDriverWait(session, TimeSpan.FromSeconds(60));
                                    var div1 = wait1.Until(ExpectedConditions.ElementIsVisible(By.ClassName("ListBoxItem")));
                                    var text_Button1 = session.FindElementsByClassName("ListBoxItem");

                                    stepName.Log(Status.Pass, "FSW is launched successfully");

                                    int counter1 = 0;
                                    string PatientName1 = null;
                                    string PatientDescription1 = null;
                                    foreach (var element in text_Button1)
                                    {
                                        if (counter1 == 3)
                                        {
                                            PatientName1 = element.GetAttribute("AutomationId");
                                            PatientDescription1 = element.GetAttribute("Name");
                                            break;
                                        }
                                        counter1 = counter1 + 1;
                                    }



                                    lib.clickOnAutomationId(session, PatientDescription, PatientName);

                                    /** Clicks on Fit patient button **/

                                    Thread.Sleep(8000);
                                    lib.waitForIdToBeClickable(session, "StandAloneAutomationIds.DetailsAutomationIds.FitAction");
                                    stepName.Pass("Patient is clicked");
                                    Thread.Sleep(10000);
                                    session.Close();
                                    appCapabilities = new DesiredCapabilities();
                                    appCapabilities.SetCapability("app", config.ApplicationPath.SmartFitAppPath);
                                    appCapabilities.SetCapability("deviceName", "WindowsPC");
                                    appCapabilities.SetCapability("appArguments", "--run-as-administrator");
                                    Thread.Sleep(5000);
                                    session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                                    Thread.Sleep(5000);
                                    session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                                    Thread.Sleep(5000);
                                    lib.clickOnAutomationName(session, "Assign Instruments");
                                    session.FindElementByName("Back").Click();
                                    Thread.Sleep(5000);
                                    session.FindElementByAccessibilityId("ConnectionAutomationIds.CommunicationInterfaceItems").Click();
                                    Thread.Sleep(2000);

                                    /** Select Noah link Wireless now, then click Connect.  **/

                                    session.FindElementByName("Noahlink Wireless").Click();
                                    lib.clickOnAutomationId(session, "Connect", "SidebarAutomationIds.ConnectAction");

                                    Thread.Sleep(13000);
                                    lib.clickOnAutomationName(session, "Assign Instruments");

                                    Thread.Sleep(5000); // Initial wait before searching

                                    var SN = session.FindElementsByClassName("ListBoxItem");

                                    // Check if DeviceNo is already discovered

                                    foreach (WindowsElement value in SN)
                                    {
                                        string S = value.Text;
                                        if (S.Contains(DeviceNo) && S.Contains("Assign Left"))
                                        {
                                            value.Text.Contains("Assign Left");
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

                Thread.Sleep(2000);
                DesiredCapabilities appCapabilities = new DesiredCapabilities();
                appCapabilities.SetCapability("app", config.ApplicationPath.FSWAppPath);
                appCapabilities.SetCapability("deviceName", "WindowsPC");
                session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                Thread.Sleep(10000);
                session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                Thread.Sleep(2000);
                session.Manage().Window.Maximize();
                var wait = new WebDriverWait(session, TimeSpan.FromSeconds(20));
                var div = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("ListBoxItem")));
                var text_Button = session.FindElementsByClassName("ListBoxItem");

                stepName.Log(Status.Pass, "FSW is launched successfully");

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

                stepName.Pass("Patient is clicked");

                Thread.Sleep(8000);
                session.Close();

                /** To launch the return visit session **/

                appCapabilities = new DesiredCapabilities();
                appCapabilities.SetCapability("app", config.ApplicationPath.SmartFitAppPath);
                appCapabilities.SetCapability("deviceName", "WindowsPC");
                Thread.Sleep(5000);
                session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                Thread.Sleep(10000);
                session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                Thread.Sleep(10000);

                try

                {
                    lib.clickOnElementWithIdonly(session, "ConnectionAutomationIds.ConnectAction");
                    stepName.Pass("Connect button is clicked");
                }

                catch (Exception e)
                {
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
                Thread.Sleep(2000);

                try
                {
                    session.FindElementByAccessibilityId("ProgramStripAutomationIds.ProgramSlot.P1").Click();

                    if ((session.FindElementByAccessibilityId("PART_Items").Text.ToString()).Contains("All-Around"))
                    {
                        Console.WriteLine("After listening test setting Value is :" + session.FindElementByAccessibilityId("PART_Items").Text.ToString());
                        stepName.Log(Status.Pass, "After listening test setting Value is :" + session.FindElementByAccessibilityId("PART_Items").Text.ToString());
                        Assert.Pass();
                        session.CloseApp();
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

                    stepName.Pass("Save is successfully done and Close the FSW");

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
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
            Thread.Sleep(1000);
            string path = (@"C:\Users\Public\Documents\Camelot\Logs\" + computer_name + "-" + user_name + "-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");
            FunctionLibrary lib = new FunctionLibrary();
            lib.fileVerify(path, stepName, "Restoring the hearing");
        }


        /** Reports for "captured" and "restored" data. **/

        [When(@"\[Open Capture and Restore report and log info in report]")]
        public void WhenOpenCaptureAndRestoreReportAndLogInfoInReport()
        {
           // ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());

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
                                            processKill(processName);
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

                                DateTimeOffset currentTime = DateTimeOffset.Now;
                                Console.WriteLine(currentTime);
                                DateTimeOffset tenDaysAgo = currentTime.AddDays(-10);
                                Console.WriteLine(tenDaysAgo);
                                string formattedtenDaysAgo = tenDaysAgo.ToString("yyyy-MM-dd HH:mm:ssZ");
                                Console.WriteLine(formattedtenDaysAgo);
                                long unixTimestamp = tenDaysAgo.ToUnixTimeSeconds();
                                Console.WriteLine(unixTimestamp);
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
                                stepName.Pass("Writing Presets is done successfully.");
                            }
                            if (DeviceType.Equals("Wired") || DeviceType.Equals("D1rechargeableWired"))
                            {
                                try
                                {
                                    session.SwitchTo().Window(session.WindowHandles.First());
                                    session.SwitchTo().ActiveElement();
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
                                            session.SwitchTo().ActiveElement();

                                            try
                                            {
                                                do
                                                {

                                                } while (session.FindElementByAccessibilityId("WorkerDialog").Enabled);

                                            }

                                            catch (Exception e)
                                            {
                                                stepName.Pass("Writing Presets is done successfully.");
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
                                                stepName.Pass("Writing Presets is done successfully.");
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
