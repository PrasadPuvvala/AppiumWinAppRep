using Microsoft.SqlServer.Management.XEvent;
using System;
using System.Collections.Generic;
using System.Text;
using java.io;
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
using MyNamespace;
using AppiumWinApp;
using Microsoft.SqlServer.Management.XEvent;
using AventStack.ExtentReports;
using System.IO;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Model;
using Microsoft.SqlServer.Management.Smo;
using Reqnroll;


namespace AppiumWinApp.StepDefinitions
{
    [Binding]
    public class RestoreSwap
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        private static appconfigsettings config;
        private static ExtentTest test;
        public static String textDir = Directory.GetCurrentDirectory();
        private const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        public static WindowsDriver<WindowsElement> session;
        private string ApplicationPath = null;
        protected static IOSDriver<IOSElement> AlarmClockSession;   // Temporary placeholder until Windows namespace exists
        protected static IOSDriver<IOSElement> DesktopSession;
        private static ExtentReports extent;
        private static ExtentSparkReporter htmlReporter;

        public TestContext TestContext { get; set; }

        string computer_name = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
        String user_name = Environment.UserName;
        public static string screenshot = string.Empty;

        public RestoreSwap(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _scenarioContext = scenarioContext;
            extent = ModuleFunctions.extent;
            _featureContext = featureContext;
        }


        /** Opens Storagelayoutviewer
          * Connects the HI device
          * Saves the dump images by checking all 'OS' nodes using Storagelayoutviewer **/

        [When(@"\[Get the dump of connected device left of DumpB by storage layout ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)""and""([^""]*)""]")]
        [Obsolete]
        public void WhenGetTheDumpOfConnectedDeviceLeftOfDumpBByStorageLayoutAndAndAnd(string device, string side, string DeviceNo, string DeviceType)
        {

            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());

            if (side.Equals("Left"))
            {
                if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
                {
                    ModuleFunctions.socketA(session, test, DeviceType);
                }

                ModuleFunctions.takeDeviceDumpImage(session, stepName, device, "Device B", side, DeviceNo, DeviceType);
                stepName.Log(Status.Pass, " Dump image taken for Device B");
            }
        }


        /** Opens S&R tool
          * Navigates to services tab
          * Perfrom restore operation using SWAP Option **/



        [When(@"\[Perform Restore with above captured image using SWAP option ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)""and""([^""]*)""]")]
        [Obsolete]
        public void WhenPerformRestoreWithAboveCapturedImageUsingSWAPOptionAndAndAndAnd(string deviceSlNo, string DeviceLeftSlNo, string device, string side, string DeviceType)
        {

            config = (appconfigsettings)_featureContext["config"];
            FunctionLibrary lib = new FunctionLibrary();
            test = ScenarioContext.Current["extentTest"] as ExtentTest;

            var scenarioContext = ScenarioContext.Current;
            var testcaseId = scenarioContext.Get<string>("TestCaseID");

            if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
            {
                if (side.Equals("Left"))

                {
                    ModuleFunctions.socketA(session, test, DeviceType);
                }

                else if (side.Equals("Right"))

                {
                    ModuleFunctions.socketB(session, test, DeviceType);
                }
            }

            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());

            Thread.Sleep(8000);
            session = ModuleFunctions.sessionInitialize(config.ApplicationPath.SandRAppPath, config.workingdirectory.SandR);
            Thread.Sleep(2000);
            session.FindElementByName("Device Info").Click();
            Thread.Sleep(2000);

            stepName.Log(Status.Pass, "S&R Tool launched successfully");


            if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
            {
                session.FindElementByName("Discover").Click();
                stepName.Log(Status.Pass, "Clicked on Discover.");
                session.SwitchTo().Window(session.WindowHandles.First());
                //session.SwitchTo().ActiveElement();

                try
                {
                    if (testcaseId == "1105675" || testcaseId == "1413300")
                    {
                        session.FindElementByAccessibilityId("SerialNumberTextBox").SendKeys(deviceSlNo);
                    }
                    else
                    {
                        session.FindElementByAccessibilityId("SerialNumberTextBox").SendKeys(DeviceLeftSlNo);
                    }
                    lib.functionWaitForName(session, "Search");
                }

                catch (Exception ex)

                {

                }

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
                                Thread.Sleep(1000);
                                ModuleFunctions.Recovery(session, stepName, DeviceType, DeviceLeftSlNo, side);
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


            session.FindElementByName("Settings").Click();
            Thread.Sleep(10000);
            var ele = session.FindElementsByClassName("ComboBox");

            try
            {
                do
                {

                } while ((ele[2].GetAttribute("IsEnabled").ToString()) == "False");
            }
            catch (Exception e) { }

            ele[2].Click();

            Thread.Sleep(2000);
            WindowsElement RightSide = session.FindElementByName("Left");
            RightSide.Click();
            Thread.Sleep(5000);

            /* Identifying checkbox */
            if (DeviceType.Equals("Wired") || DeviceType.Equals("D1rechargeableWired"))
            {
                session.FindElementByName("Connect to hearing instrument automatically").Click();
                Thread.Sleep(2000);
                session.FindElementByName("Connect to hearing instrument automatically").Click();
                Thread.Sleep(8000);
            }

            session.FindElementByName("Services").Click();
            Thread.Sleep(10000);
            var res = session.FindElementsByClassName("Button");
            res[14].Click();
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
            Thread.Sleep(4000);
            lib.waitUntilElementExists(session, "textBoxSerialNumber", 1);



            session.FindElementByAccessibilityId("textBoxSerialNumber").SendKeys(DeviceLeftSlNo);
            session = lib.functionWaitForId(session, "buttonFind");
            WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(50));
            ModuleFunctions.sessionInitialize(config.ApplicationPath.SandRAppPath, config.workingdirectory.SandR);
            session = lib.waitForElement(session, "SELECT");

            try
            {
                Thread.Sleep(6000);
                session = lib.functionWaitForId(session, "radioButtonRestoreAfterRepairOrSwap");
                session = lib.functionWaitForName(session, "RESTORE");
            }
            catch (Exception e)
            {

            }

            var HIData = lib.waitUntilElementExists(session, "windowUserMessage", 1);
            var text = session.FindElementByAccessibilityId("textBlockMessage");

            if (session.FindElementByAccessibilityId("labelHeader").Text == "Restoration Succeeded")
            {
                screenshot = ModuleFunctions.CaptureScreenshot(session);
                stepName.Pass("Restoration " + text.Text, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
            }
            else
            {
                screenshot = ModuleFunctions.CaptureScreenshot(session);
                stepName.Pass("Restoration Failed" + ":" + " " + text.Text, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
            }
            session = ModuleFunctions.sessionInitialize(config.ApplicationPath.SandRAppPath, config.workingdirectory.SandR);
            session = lib.waitForElement(session, "OK");
            var btncls1 = session.FindElementByAccessibilityId("PART_Close");
            btncls1.Click();
            Thread.Sleep(1000);
        }


        /** Opens S&R tool
         * Navigates to services tab
         * Perfrom restore operation using SWAP Option **/


        //[When(@"\[Perform Restore with above captured image using SWAP with left ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)""]")]
        [When(@"\[Perform Restore with above captured image using SWAP with left ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)""and""([^""]*)""]")]
        [Obsolete]
        public void WhenPerformRestoreWithAboveCapturedImageUsingSWAPWithLeftAndAndAndAnd(string deviceSlNo, string DeviceLeftSlNo, string device, string side, string DeviceType)
        {


            FunctionLibrary lib = new FunctionLibrary();

            config = (appconfigsettings)_featureContext["config"];

            if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
            {
                if (side.Equals("Left"))

                {
                    ModuleFunctions.socketA(session, test, DeviceType);
                }

                else if (side.Equals("Right"))
                {
                    ModuleFunctions.socketB(session, test, DeviceType);
                }
            }

            test = ScenarioContext.Current["extentTest"] as ExtentTest;

            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());


            try
            {
                session = ModuleFunctions.launchApp(Directory.GetCurrentDirectory() + "\\LaunchSandR.bat", Directory.GetCurrentDirectory());

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            Thread.Sleep(8000);
            session = ModuleFunctions.sessionInitialize(config.ApplicationPath.SandRAppPath, config.workingdirectory.SandR);
            Thread.Sleep(2000);
            session.FindElementByName("Device Info").Click();
            Thread.Sleep(2000);

            stepName.Log(Status.Pass, "S&R Tool launched successfully");

            if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
            {
                session.FindElementByName("Discover").Click();

                stepName.Log(Status.Pass, "Clicked on Discover.");

                session.SwitchTo().Window(session.WindowHandles.First());
                //session.SwitchTo().ActiveElement();

                try
                {
                    session.FindElementByAccessibilityId("SerialNumberTextBox").SendKeys(deviceSlNo);
                    lib.functionWaitForName(session, "Search");
                }

                catch (Exception ex)

                {
                }

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
                                Thread.Sleep(1000);
                                ModuleFunctions.Recovery(session, stepName, DeviceType, deviceSlNo, side);
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

            session.FindElementByName("Device Info").Click();
            Thread.Sleep(2000);
            session.FindElementByName("Settings").Click();
            Thread.Sleep(4000);
            var ele = session.FindElementsByClassName("ComboBox");

            try
            {
                do
                {

                } while ((ele[2].GetAttribute("IsEnabled").ToString()) == "False");
            }
            catch (Exception e) { }

            ele[2].Click();
            WindowsElement RightSide = session.FindElementByName("Right");
            RightSide.Click();
            Thread.Sleep(5000);

            /* Identifying checkbox */

            if (DeviceType.Equals("Wired") || DeviceType.Equals("D1rechargeableWired"))
            {
                session.FindElementByName("Connect to hearing instrument automatically").Click();
                Thread.Sleep(2000);
                session.FindElementByName("Connect to hearing instrument automatically").Click();
                Thread.Sleep(8000);
            }

            session.FindElementByName("Services").Click();
            Thread.Sleep(20000);
            var res = session.FindElementsByClassName("Button");
            res[14].Click();
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
            Thread.Sleep(4000);
            session.FindElementByAccessibilityId("textBoxSerialNumber").SendKeys(DeviceLeftSlNo);
            session = lib.functionWaitForId(session, "buttonFind");
            WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(50));
            ModuleFunctions.sessionInitialize(config.ApplicationPath.SandRAppPath, config.workingdirectory.SandR);
            session = lib.waitForElement(session, "SELECT");

            try
            {
                session = lib.functionWaitForId(session, "radioButtonRestoreAfterRepairOrSwap");
                session = lib.functionWaitForName(session, "RESTORE");
            }
            catch (Exception e)
            {
            }

            var HIData = lib.waitUntilElementExists(session, "windowUserMessage", 1);
            var text = session.FindElementByAccessibilityId("textBlockMessage");

            if (session.FindElementByAccessibilityId("labelHeader").Text == "Restoration Succeeded")
            {

                screenshot = ModuleFunctions.CaptureScreenshot(session);

                stepName.Pass("Restoration " + text.Text, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
              
            }
            else
            {
                screenshot = ModuleFunctions.CaptureScreenshot(session);

                stepName.Log(Status.Fail, "Restoration Failed" + ":" + " " + text.Text, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
            }
            session = ModuleFunctions.sessionInitialize(config.ApplicationPath.SandRAppPath, config.workingdirectory.SandR);
            session = lib.waitForElement(session, "OK");
            var btncls1 = session.FindElementByAccessibilityId("PART_Close");
            btncls1.Click();
        }


        /** Opens Storagelayoutviewer
          * Connects the HI device
          * Saves the dump images by checking all 'OS' nodes using Storagelayoutviewer **/


        [When(@"\[Get the dump of connected device of left DumpC by storage layout ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)""and""([^""]*)""]")]
        public void WhenGetTheDumpOfConnectedDeviceOfLeftDumpCByStorageLayoutAndAndAnd(string device, string side, string DeviceNo, string DeviceType)
        {
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());

            if (side.Equals("Left"))
            {
                if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
                {
                    ModuleFunctions.socketA(session, test, DeviceType);
                }

                ModuleFunctions.takeDeviceDumpImage(session, stepName, device, "Device C", side, DeviceNo, DeviceType);
                stepName.Log(Status.Pass, " Dump image taken for Device C");

            }
            else if (side.Equals("Right"))
            {

                if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
                {
                    ModuleFunctions.socketB(session, test, DeviceType);
                }

                ModuleFunctions.takeDeviceDumpImage(session, test, device, "Device B", side, DeviceNo, DeviceType);
                stepName.Log(Status.Pass, " Dump image taken for Device B ");
            }

        }


        /** Opens Storagelayoutviewer
          * Connects the HI device
          * Saves the dump images by checking all 'OS' nodes using Storagelayoutviewer **/

        [When(@"\[Get the dump of connected device of DumpD by storage layout ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)""and""([^""]*)""]")]
        public void WhenGetTheDumpOfConnectedDeviceOfDumpDByStorageLayoutAndAndAnd(string device, string side, string DeviceNo, string DeviceType)
        {

            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());

            if (side.Equals("Right"))
            {
                if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
                {
                    ModuleFunctions.socketB(session, test, DeviceType);
                }

                ModuleFunctions.takeDeviceDumpImage(session, stepName, device, "Device D", side, DeviceNo, DeviceType);
                stepName.Log(Status.Pass, " Dump image taken for Device D ");

            }

        }



        [Then(@"\[Do the dump comparison between two device DeviceC and DeviceD dumps(.*)]")]
        public void ThenDoTheDumpComparisonBetweenTwoDeviceDeviceCAndDeviceDDumps(string side)
        {

            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            FunctionLibrary lib = new FunctionLibrary();
            lib.dumpCompare1(side, test);
        }



    }// Class RestoreSwap

}//Name Space

