using java.io;
using TechTalk.SpecFlow;
using java.awt;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using WindowsInput;
using WindowsInput.Native;
using Xamarin.Forms;
using Console = System.Console;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using javax.swing.plaf;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Mail;
using System.Net;
using TestStack.BDDfy.Reporters.Html;
using System.Text.RegularExpressions;
using File = System.IO.File;

namespace AppiumWinApp.StepDefinitions
{
    [Binding]
    public sealed class Hooks1
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        public static appconfigsettings config;
        static string configsettingpath = System.IO.Directory.GetParent(@"../../../").FullName
        + Path.DirectorySeparatorChar + "appconfig.json";
        private static ExtentReports extent;
        private static ExtentHtmlReporter htmlReporter;
        private static ExtentTest test;
        public static String textDir = Directory.GetCurrentDirectory();

        [BeforeFeature]
        [Obsolete]
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
        [Obsolete]
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
    }

}