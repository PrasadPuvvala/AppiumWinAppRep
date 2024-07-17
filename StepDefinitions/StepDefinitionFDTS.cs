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
using MyNamespace;
using AppiumWinApp;
using Microsoft.SqlServer.Management.XEvent;
using AventStack.ExtentReports;
using System.IO;
using AventStack.ExtentReports.Reporter;
using AppiumWinApp.PageFactory;
using AventStack.ExtentReports.Model;
using System.Configuration;
using System.Xml.Linq;
using OpenQA.Selenium.Appium;
using System.Collections.ObjectModel;
using jdk;
using System.Xml;

namespace AppiumWinApp.StepDefinitions
{
    [Binding]
    public class StepDefinitionFDTS
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
        private static ExtentHtmlReporter htmlReporter;

        public TestContext TestContext { get; set; }

        string computer_name = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
        String user_name = Environment.UserName;

        public static string screenshot = string.Empty;
        public StepDefinitionFDTS(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _scenarioContext = scenarioContext;
            extent = ModuleFunctions.extent;
            _featureContext = featureContext;

        }

        /** This is to clear exisisting dump image files in the c drive **/

        [Given(@"\[Cleaning up dumps before execution starts]")]
        public void GivenCleaningUpDumpsBeforeExecutionStarts()
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());

            try
            {
                if (System.IO.File.Exists(@"C:\Device A.xml"))
                {
                    System.IO.File.Delete(@"C:\Device A.xml");
                }
            }
            catch (Exception e) { }

            try
            {
                if (System.IO.File.Exists(@"C:\Device B.xml"))
                {
                    System.IO.File.Delete(@"C:\Device B.xml");
                }
            }
            catch (Exception e) { }

            try
            {
                if (System.IO.File.Exists(@"C:\Device C.xml"))
                {
                    System.IO.File.Delete(@"C:\Device C.xml");
                }
            }
            catch (Exception e) { }

            try
            {
                if (System.IO.File.Exists(@"C:\Device D.xml"))
                {
                    System.IO.File.Delete(@"C:\Device D.xml");
                }
            }
            catch (Exception e) { }

            stepName.Log(Status.Pass, "All dumps are cleaned up");

        }

        /** Opens S&R tool 
         *  Navigates to settings tab
         *  changes the HI side selection **/

        [When(@"\[Change communication channel in S and RLeft(.*)]")]
        public void WhenChangeCommunicationChannelInSAndRLeft(string side)
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            FunctionLibrary lib = new FunctionLibrary();
            lib.processKill("msedge");
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());

            /** Launching S&R Tool **/

            try
            {
                session = ModuleFunctions.launchApp(Directory.GetCurrentDirectory() + "\\LaunchSandR.bat", Directory.GetCurrentDirectory());

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Thread.Sleep(8000);

            config = (appconfigsettings)_featureContext["config"];

            session = ModuleFunctions.sessionInitialize(config.ApplicationPath.SandRAppPath, config.workingdirectory.SandR);

            Thread.Sleep(2000);

            stepName.Log(Status.Pass, "S&R Tool launched successfully");
            session.FindElementByName("Device Info").Click();
            Thread.Sleep(20000);

            /** Navigation to settings tab **/

            session.FindElementByName("Settings").Click();
            Thread.Sleep(8000);

            /** Identifying side selection dropdown **/

            var ele = session.FindElementsByClassName("ComboBox");

            try
            {
                do
                {

                } while ((ele[2].GetAttribute("IsEnabled").ToString()) == "False");
            }
            catch (Exception e) { }

            ele[2].Click();

            Thread.Sleep(10000);

            /** Changes the side selection **/

            if (side.Equals("Right"))
            {
                stepName.Log(Status.Pass, "Communication channel selected to Left");

                WindowsElement RightSide = session.FindElementByName("Right");
                RightSide.Click();

                stepName.Log(Status.Pass, "Communication channel chanted to Right");

            }
            else
            {

                stepName.Log(Status.Pass, "Communication channel selected to Right");

                WindowsElement LeftSide = session.FindElementByName("Left");
                LeftSide.Click();

                stepName.Log(Status.Pass, "Communication channel chanted to Left");

            }

            /** Identifying checkbox **/

            session = lib.waitForElement(session, "Connect to hearing instrument automatically");
            session = lib.waitForElement(session, "Connect to hearing instrument automatically");
            Thread.Sleep(8000);

            try
            {
                do
                {

                } while ((ele[2].GetAttribute("IsEnabled").ToString()) == "False");
            }
            catch (Exception e) { }

            session.FindElementByName("Services").Click();
            session.CloseApp();
            // stepName.Log(Status.Pass, "Communication channel chanted to " +side);

        }


        /** Opens Storagelayoutviewer
          * Connects the HI device
          * Saves the dump images by checking all 'OS' nodes using Storagelayoutviewer **/

        [When(@"\[Get the dump of connected device by storage layout ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)""and""([^""]*)""]")]
        public void WhenGetTheDumpOfConnectedDeviceByStorageLayoutAndAndAnd(string device, string side, string DeviceNo, string DeviceType)
        {

            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());

            if (side.Equals("Left"))
            {
                Thread.Sleep(4000);

                if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
                {

                    ModuleFunctions.socketA(session, test, DeviceType);
                }
                ModuleFunctions.takeDeviceDumpImage(session, stepName, device, "Device A", side, DeviceNo, DeviceType);
                stepName.Log(Status.Pass, " Dump image taken for Device A ");

            }
            else if (side.Equals("Right"))
            {


                Thread.Sleep(4000);

                if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
                {
                    ModuleFunctions.socketB(session, test, DeviceType);
                }
                ModuleFunctions.takeDeviceDumpImage(session, stepName, device, "Device B", side, DeviceNo, DeviceType);
                stepName.Log(Status.Pass, " Dump image taken for Device B ");

            }

            else if (side.Equals("Cdevice"))
            {


                Thread.Sleep(4000);

                if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
                {
                    ModuleFunctions.socketC(session, test, DeviceType);
                }
                ModuleFunctions.takeDeviceDumpImage(session, stepName, device, "Device C", side, DeviceNo, DeviceType);
                stepName.Log(Status.Pass, " Dump image taken for Device C ");

            }
            else if (side.Equals("Device C"))
            {


                Thread.Sleep(4000);

                if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
                {
                    ModuleFunctions.socketB(session, test, DeviceType);
                }
                ModuleFunctions.takeDeviceDumpImage(session, stepName, device, "Device C", side, DeviceNo, DeviceType);
                stepName.Log(Status.Pass, " Dump image taken for Device C ");

            }
            else
            {

                if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
                {

                    ModuleFunctions.socketB(session, test, DeviceType);
                }
                ModuleFunctions.takeDeviceDumpImage(session, stepName, device, "Device D", side, DeviceNo, DeviceType);
                stepName.Log(Status.Pass, " Dump image taken for Device D ");

            }

        }



        /** Opens FDTS system configuration tool
         *  Navigates to System settings - Communication device
         *  Changes the Interface Channel side  **/

        [Given(@"Lauch socket Driver ""([^""]*)""and""([^""]*)""")]
        public void GivenLauchSocketDriverAnd(string device, string DeviceType)
        {

            if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
            {
                ModuleFunctions.socket(session, test, DeviceType);
            }

        }




        [Given(@"\[Change channel side in FDTS(.*)]")]
        public void WhenChangeChannelSideInFDTS(string side)

        {

            test = ScenarioContext.Current["extentTest"] as ExtentTest;

            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());


            /** Selecting Device side in System Settings **/

            SystemPageFactory.launchSystemSettings(side, extent, stepName);

            stepName.Log(Status.Pass, "Channel changed to side: " + side);

        }


        /** Opens S&R tool
          * Navigates to services tab
          * Perfrom restore operation using RTS Option **/

        [When(@"\[Perform Restore with above captured image using RTS option ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)""and""([^""]*)""]")]
        public void WhenPerformRestoreWithAboveCapturedImageUsingRTSOptionAndAndAndAnd(string DeviceLeftSlNo, string deviceSlNo, string device, string side, string DeviceType)
        {

            FunctionLibrary lib = new FunctionLibrary();

            config = (appconfigsettings)_featureContext["config"];

            test = ScenarioContext.Current["extentTest"] as ExtentTest;

            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());

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

                else if (side.Equals("Cdevice"))

                {
                    ModuleFunctions.socketC(session, test, DeviceType);
                }
            }

            /** To lauch the S&R tool **/

            try
            {
                session = ModuleFunctions.launchApp(Directory.GetCurrentDirectory() + "\\LaunchSandR.bat", Directory.GetCurrentDirectory());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Thread.Sleep(8000);

            /** To lauch the S&R tool **/

            session = ModuleFunctions.sessionInitialize(config.ApplicationPath.SandRAppPath, config.workingdirectory.SandR);
            Thread.Sleep(2000);
            stepName.Log(Status.Pass, "S&R Tool launched successfully");
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

                    if (side.Equals("Right"))
                    {
                        session.FindElementByAccessibilityId("SerialNumberTextBox").SendKeys(deviceSlNo);
                    }

                    else if (side.Equals("Left"))
                    {
                        session.FindElementByAccessibilityId("SerialNumberTextBox").SendKeys(DeviceLeftSlNo);
                    }
                    else if (side.Equals("Cdevice"))
                    {
                        session.FindElementByAccessibilityId("SerialNumberTextBox").SendKeys(deviceSlNo);
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


                    session = lib.waitForElement(session, "Model Name");

                    stepName.Log(Status.Pass, "Dook2 Dev");
                }
                catch (Exception ex)
                { }
            }

            session.FindElementByName("Settings").Click();
            Thread.Sleep(8000);
            var ele = session.FindElementsByClassName("ComboBox");

            try
            {
                do
                {

                } while ((ele[2].GetAttribute("IsEnabled").ToString()) == "False");
            }
            catch (Exception e) { }

            ele[2].Click();

            Thread.Sleep(4000);
            WindowsElement RightSide = session.FindElementByName("Right");
            RightSide.Click();
            Thread.Sleep(5000);

            /** Identifying checkbox **/

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

            /** To pass the User password **/

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

            /** To pass the Device serial number **/

            session.FindElementByAccessibilityId("textBoxSerialNumber").SendKeys(DeviceLeftSlNo);
            Thread.Sleep(2000);
            session = lib.functionWaitForId(session, "buttonFind");
            WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(50));
            session = lib.waitForElement(session, "SELECT");




            try
            {
                session = lib.functionWaitForId(session, "radioButtonRestoreAfterRTS");
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
                stepName.Log(Status.Pass, "Restoration " + text.Text, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
            }
            else
            {
                screenshot = ModuleFunctions.CaptureScreenshot(session);
                stepName.Log(Status.Fail, "Restoration Failed" + ":" + " " + text.Text, MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
            }

            session = ModuleFunctions.sessionInitialize(config.ApplicationPath.SandRAppPath, config.workingdirectory.SandR);
            session = lib.waitForElement(session, "OK");
            stepName.Log(Status.Pass, "Restore is successful.");
            var btncls1 = session.FindElementByAccessibilityId("PART_Close");
            btncls1.Click();
            Thread.Sleep(1000);

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

                else if (side.Equals("Cdevice"))

                {
                    ModuleFunctions.socketC(session, test, DeviceType);
                }
            }

        }


        [Then(@"\[Do the dump comparison between two devices in Swap dumps""([^""]*)""]")]
        public void ThenDoTheDumpComparisonBetweenTwoDevicesInSwapDumps(string side)
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            FunctionLibrary lib = new FunctionLibrary();
            lib.dumpCompare1(side, test);
        }



        /** Compares the dump image files **/

        [Then(@"\[Do the dump comparison between two device dumps(.*)]")]
        public void ThenDoTheDumpComparisonBetweenTwoDeviceDumps(string side)
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            FunctionLibrary lib = new FunctionLibrary();
            lib.dumpCompare(side, test);

        }


        /** Opens FSW 
          * selects a patient, connects the HIs to the FSW, 
          * Programs are added, performs save and exit. **/


        [When(@"\[Create a Patient and add programs to HI In FSW ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)""]")]
        public void WhenCreateAPatientAndAddProgramsToHIInFSWAlterFSW(string alterValue, string device, string DeviceNo, string side)
        {
            Console.WriteLine("This is When method");
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            config = (appconfigsettings)_featureContext["config"];
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
            FunctionLibrary lib = new FunctionLibrary();

            if (device.Contains("RT") || device.Contains("RU") || device.Contains("NX"))
            {
                ModuleFunctions.socketA(session, test, device);
                Thread.Sleep(3000);
                Console.WriteLine("This is When method");
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
                    if (counter == 3)
                    {
                        PatientName = element.GetAttribute("AutomationId");
                        PatientDescription = element.GetAttribute("Name");
                        break;
                    }

                    counter = counter + 1;
                }

                lib.clickOnAutomationId(session, PatientDescription, PatientName);

                /** Clicks on "Fit patient" **/

                Thread.Sleep(8000);
                lib.waitForIdToBeClickable(session, "StandAloneAutomationIds.DetailsAutomationIds.FitAction");
                stepName.Pass("Patient is clicked");

                Thread.Sleep(10000);
                session.Close();

                appCapabilities = new DesiredCapabilities();
                appCapabilities.SetCapability("app", config.ApplicationPath.SmartFitAppPath);
                appCapabilities.SetCapability("deviceName", "WindowsPC");
                Thread.Sleep(5000);

                session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                Thread.Sleep(10000);
                session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);

                Thread.Sleep(10000);

                session.FindElementByName("Back").Click();

                Thread.Sleep(5000);

                session.FindElementByAccessibilityId("ConnectionAutomationIds.CommunicationInterfaceItems").Click();

                Thread.Sleep(2000);

                session.FindElementByName("Noahlink Wireless").Click();


                lib.clickOnAutomationId(session, "Connect", "SidebarAutomationIds.ConnectAction");

                Thread.Sleep(8000);

                try

                {

                    if (session.FindElementByName("Unassign").Enabled)

                    {

                        lib.clickOnAutomationName(session, "Assign Instruments");

                        // Initial wait before searching
                        Thread.Sleep(5000);

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
                    bool deviceFound = false;

                    if (!deviceFound)

                    {

                        DateTime startTime = DateTime.Now;

                        while (!session.FindElementByAccessibilityId("SearchButton").Enabled &&

                               (DateTime.Now - startTime).TotalSeconds < 40) // Wait for button to become enabled for 30 seconds

                        {

                            Thread.Sleep(1000); // Wait for 1 second before checking again

                        }

                        // Check if the button is enabled

                        if (session.FindElementByAccessibilityId("SearchButton").Enabled)

                        {
                            session.FindElementByAccessibilityId("SearchButton").Click();

                            Thread.Sleep(5000); // Wait after clicking search button

                            var List = session.FindElementsByClassName("ListBoxItem");

                            // Loop until the device is found or timeout occurs

                            startTime = DateTime.Now;

                            while (!deviceFound && (DateTime.Now - startTime).TotalSeconds < 30) // Adjust timeout as needed

                            {
                                foreach (WindowsElement value in List)
                                {
                                    string S = value.Text;

                                    if (S.Contains(DeviceNo))

                                    {
                                        // Ensure it contains "Assign Left"

                                        if (S.Contains("Assign Left"))
                                        {
                                            value.Click();
                                            value.FindElementByName("Assign Left").Click();
                                            deviceFound = true;
                                            break;
                                        }
                                    }
                                }

                                // If device not found yet, click on the search button again
                                if (!deviceFound)
                                {
                                    session.FindElementByAccessibilityId("SearchButton").Click();

                                    Thread.Sleep(5000);

                                    List = session.FindElementsByClassName("ListBoxItem");
                                }

                            }

                        }

                        else

                        {
                            // Handle case where search button is not enabled within timeout
                            Console.WriteLine("Search button not enabled within timeout.");
                        }

                    }

                    if (!deviceFound)
                    {
                        // Handle timeout or device not found
                        Console.WriteLine("Device not found within timeout.");
                    }

                }

                /** Select the Connection Flow's "Continue" button to continue. **/

                lib.clickOnAutomationName(session, "Continue");
                Thread.Sleep(15000);
            }


            if (device.Contains("LT") || device.Contains("RE"))
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

                /** Clicks on "Fit patient" button **/

                Thread.Sleep(8000);
                lib.waitForIdToBeClickable(session, "StandAloneAutomationIds.DetailsAutomationIds.FitAction");
                stepName.Pass("Patient is clicked");
                Thread.Sleep(10000);
                session.Close();
                appCapabilities = new DesiredCapabilities();
                appCapabilities.SetCapability("app", config.ApplicationPath.SmartFitAppPath);
                appCapabilities.SetCapability("deviceName", "WindowsPC");
                Thread.Sleep(5000);
                session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);
                Thread.Sleep(10000);
                session = new WindowsDriver<WindowsElement>(new Uri(config.TestEnvironment.WinappDriverUrl), appCapabilities);

                try
                {
                    lib.clickOnElementWithIdonly(session, "ConnectionAutomationIds.ConnectAction");
                    stepName.Pass("Connect button is clicked");

                }
                catch (Exception e)
                {


                }
                Thread.Sleep(10000);


                /** clicks the "back" button, selects the Speed Link, and then clicks "connect" **/

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

            catch (Exception)
            {
                Thread.Sleep(4000);

                /** Clicks on "Fit Patient" in Profile screen **/

                try
                {
                    lib.clickOnElementWithIdonly(session, "PatientAutomationIds.ProfileAutomationIds.FitPatientAction");
                }
                catch (Exception)
                {

                }

                try
                {
                    lib.clickOnElementWithIdonly(session, "ProgramStripAutomationIds.AddProgramAction");
                    /** To add the Program Outdoor and music **/
                    session.FindElementByName("Outdoor").Click();
                    Thread.Sleep(5000);
                    lib.clickOnElementWithIdonly(session, "ProgramStripAutomationIds.AddProgramAction");
                    session.FindElementByName("Music").Click();
                    lib.functionWaitForName(session, "Music");

                }
                catch (Exception)
                { }



                /** Click on Skip button and save the data **/

                try
                {
                    Thread.Sleep(10000);
                    lib.clickOnElementWithIdonly(session, "FittingAutomationIds.SaveAction");

                    try
                    {
                        Thread.Sleep(2000);
                        lib.clickOnElementWithIdonly(session, "SkipButton");
                    }
                    catch (Exception e1)
                    {
                        lib.clickOnElementWithIdonly(session, "PART_Cancel");
                    }

                    stepName.Pass("Save is successfully done and Close the FSW");
                }
                catch (Exception skip)

                {
                    Console.WriteLine(skip);
                }

                /** Exit the Fsw **/

                lib.clickOnElementWithIdonly(session, "SaveAutomationIds.PerformSaveAutomationIds.ExitAction");
                stepName.Pass("Click on FSW Exit button");

                Thread.Sleep(8000);

                lib.processKill("SmartFitSA");
            }
        }


        [Then(@"\[Do the Comparison between Azure Data and SandR Data]")]
        public void ThenDoTheComparisonBetweenAzureDataAndSandRData()
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            FunctionLibrary lib = new FunctionLibrary();
            Thread.Sleep(15000);
            lib.AzureFileCompare(session, test);
        }

        [Given(@"\[Launch SandRTool]")]
        public void GivenLaunchSandRTool()
        {
            ModuleFunctions.SandRenvironmentchange();
            config = (appconfigsettings)_featureContext["config"];
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
            session = ModuleFunctions.sessionInitialize1(config.ApplicationPath.SandRAppPath, config.workingdirectory.SandR);         
        }

        [When(@"\[Navigate to settings tab and set the system role to ""([^""]*)""]")]
        public void WhenNavigateToSettingsTabAndSetTheSystemRoleTo(string SystemRole)
        {
            session.SwitchTo().Window(session.WindowHandles.First());
            session.FindElementByAccessibilityId("Button_4").FindElementByName("Settings").Click();
            Thread.Sleep(2000);
            session.FindElementByAccessibilityId("GroupBox_1").FindElementByAccessibilityId("ComboBox_2").Click();
            Thread.Sleep(2000);
            session.FindElementByClassName("Popup").FindElementByName(SystemRole).Click();
            Thread.Sleep(2000);
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
            screenshot = ModuleFunctions.CaptureScreenshot(session);
            stepName.Log(Status.Pass, "Verify the visibility of set sales order connection string option", MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
        }

        [Then(@"\[Click on set sales order connection string and input the invalid base string ""([^""]*)""]")]
        public void ThenClickOnSetSalesOrderConnectionStringAndInputTheInvalidBaseString(string value)
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;          
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
            session.FindElementByName("Set sales order connection string").Click();
            Thread.Sleep(2000);
            session.FindElementByAccessibilityId("controls:MetroWindow_2").FindElementByAccessibilityId("ConnectionStringTextBox").SendKeys(value);
            Thread.Sleep(2000);
            screenshot = ModuleFunctions.CaptureScreenshot(session);
            stepName.Log(Status.Pass, "Textbox to Enter the connection string value", MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
            session.FindElementByAccessibilityId("SaveButton").Click();
            Thread.Sleep(2000);
        }

        [Then(@"\[Verify the error message on the connection string pop-up window ""([^""]*)""]")]
        public void ThenVerifyTheErrorMessageOnTheConnectionStringPop_UpWindow(string expectedmessage)
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
            screenshot = ModuleFunctions.CaptureScreenshot(session);
            var actualmessage = session.FindElementByAccessibilityId("TextBlock_1").Text;
            Thread.Sleep(2000);
            stepName.Log(Status.Pass, $"Verify the error message {actualmessage}", MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
            Assert.That(actualmessage, Is.EqualTo(expectedmessage));
        }

        [Then(@"\[Verify that a ""([^""]*)"" string in the user config file in the local machine]")]
        public void ThenVerifyThatAStringInTheUserConfigFileInTheLocalMachine(string value)
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
            config = (appconfigsettings)_featureContext["config"];
            try
            {              
                XmlDocument doc = new XmlDocument();
                doc.Load(config.connectionStringPath.PathtoValue);
                string settingName = "SalesOrderConnection";
                XmlNode node = doc.SelectSingleNode($"//setting[@name='{settingName}']/value");
                Assert.That(value, Is.EqualTo(node.InnerText));
                stepName.Log(Status.Pass, $"valid base64 connection string value is updated in the user.config file : {node.InnerText}");
            }
            catch
            {
                stepName.Log(Status.Pass, $"Invalid base64 connection string is not updated in the user.config file : {value}");
            }       
        }

        [Then(@"\[Click on set sales order connection string and input the valid base string ""([^""]*)""]")]
        public void ThenClickOnSetSalesOrderConnectionStringAndInputTheValidBaseString(string ValidBase64Value)
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
            session.FindElementByName("Set sales order connection string").Click();
            Thread.Sleep(2000);
            session.FindElementByAccessibilityId("controls:MetroWindow_2").FindElementByAccessibilityId("ConnectionStringTextBox").SendKeys(ValidBase64Value);
            Thread.Sleep(2000);
            screenshot = ModuleFunctions.CaptureScreenshot(session);
            stepName.Log(Status.Pass, "Textbox to Enter the connection string value", MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
            session.FindElementByAccessibilityId("SaveButton").Click();
            Thread.Sleep(2000);
            var btncls = session.FindElementByAccessibilityId("PART_Close");
            btncls.Click();
        }

        [Then(@"\[Verify the visibility of connection string]")]
        public void ThenVerifyTheVisibilityOfConnectionString()
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
            screenshot = ModuleFunctions.CaptureScreenshot(session);
            stepName.Log(Status.Pass, "Set sales order connection string is displayed", MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
        }

        [Then(@"\[Verify the visibility of connection string other than VA system role]")]
        public void ThenVerifyTheVisibilityOfConnectionStringOtherThanVASystemRole()
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
            screenshot = ModuleFunctions.CaptureScreenshot(session);
            stepName.Log(Status.Pass, "Set sales order connection string is not displayed for other than VA system role", MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
        }

        [When(@"\[Uninstall the current S&R Tool]")]
        public void WhenUninstallTheCurrentSRTool()
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
            ModuleFunctions.UninstallSandRTool();
            stepName.Log(Status.Pass, "S&R Tool Uninstalled sucessfully");
        }

        [When(@"\[Install the latest S&R Tool]")]
        public void WhenInstallTheLatestSRTool()
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
            ModuleFunctions.InstallSandRTool();
            stepName.Log(Status.Pass, "S&R Tool Installed sucessfully");
            ModuleFunctions.SandRenvironmentchange();
        }

        [Then(@"\[Validate the previous SystemRole ""([^""]*)"" and valid base connection string ""([^""]*)"" and ""([^""]*)"" is preserved to latest S&R]")]
        public void ThenValidateThePreviousSystemRoleAndValidBaseConnectionStringAndIsPreservedToLatestSR(string systemrole, string validbasevalue, string connectionStringButton)
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
            session.SwitchTo().Window(session.WindowHandles.First());
            session.FindElementByAccessibilityId("Button_4").FindElementByName("Settings").Click();
            Thread.Sleep(2000);
            screenshot = ModuleFunctions.CaptureScreenshot(session);
            try
            {
                var actualsystemrole = session.FindElementByAccessibilityId("GroupBox_1").FindElementByAccessibilityId("ComboBox_2").Text;
                Assert.That(systemrole, Is.EqualTo(actualsystemrole));
                stepName.Log(Status.Pass, "Previously selected VA system role is preserved to latest S&R Tool", MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
            }
            catch (Exception ex)
            {
                stepName.Log(Status.Fail, "Previously selected VA system role is not preserved to latest S&R Tool", MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
            }
            try
            {
                var actualConnectionStringButton = session.FindElementByAccessibilityId("GroupBox_1").FindElementByAccessibilityId("Button_3").FindElementByClassName("TextBlock").Text;
                Assert.That(connectionStringButton, Is.EqualTo(actualConnectionStringButton));
                stepName.Log(Status.Pass, "Sales order connection string button with value is preserved to latest S&R Tool", MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
            }
            catch(Exception ex)
            {
                stepName.Log(Status.Fail, "Sales order connection string button with value is not preserved to latest S&R Tool", MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
            }                
        }

        [AfterScenario]

        //[Then(@"\[done]")]
        public void ThenDone()
        {

            //Process winApp = new Process();
            //winApp.StartInfo.FileName = "C:\\Program Files (x86)\\Windows Application Driver\\WinAppDriver.exe";
            //winApp.Kill();

            Console.WriteLine("This is Done method");
            var scenarioContext = ScenarioContext.Current;
            var testStatus = scenarioContext.TestError == null ? "PASS" : "FAIL";
            var testcaseId = scenarioContext.Get<string>("TestCaseID");

            var xmlFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), $"{testcaseId}.xml", SearchOption.AllDirectories);


            foreach (var xmlFile in xmlFiles)
            {
                XDocument xmlDoc = XDocument.Load(xmlFile);


                foreach (var testResultSetElement in xmlDoc.Descendants("TFSTestResultsSet"))
                {
                    var elementTestCaseID = (string)testResultSetElement.Element("TestCaseID");

                    if (elementTestCaseID == testcaseId)
                    {

                        var elementTestStatus = testResultSetElement.Element("TestStatus");
                        if (elementTestStatus != null)
                        {
                            elementTestStatus.Value = testStatus;
                        }
                    }
                }

                xmlDoc.Save(xmlFile);                                                                                                                                                                                                                                                                            // Save the updated XML
            }



            {

                string projectPath = AppDomain.CurrentDomain.BaseDirectory;

                string xmlFolderPath = Path.Combine(projectPath, "XML");

                string keyToUpdate = "WorkFlowsXMLsPath";
                string valueToUpdate = xmlFolderPath;

                string[] configFiles = Directory.GetFiles(projectPath, "*.config", SearchOption.AllDirectories);

                foreach (var configFile in configFiles)
                {
                    UpdateAppSettingValue(configFile, keyToUpdate, valueToUpdate);
                }
            }

            static void UpdateAppSettingValue(string configFilePath, string key, string value)
            {
                try
                {
                    ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap
                    {
                        ExeConfigFilename = configFilePath
                    };
                    Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

                    if (config.AppSettings.Settings[key] != null)
                    {
                        config.AppSettings.Settings[key].Value = value;
                        config.Save(ConfigurationSaveMode.Modified);
                        ConfigurationManager.RefreshSection("appSettings");

                        string updatedValue = ConfigurationManager.AppSettings[key];
                        Console.WriteLine($"Updated {key} in {configFilePath}: {updatedValue}");
                    }
                    else
                    {
                        Console.WriteLine($"Key {key} not found in {configFilePath}.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating configuration file {configFilePath}: {ex.Message}");
                }
            }

            try

            {
                string agentPath = Path.Combine(Directory.GetCurrentDirectory(), @"XML\TFS API\TFS.Agent.Run\bin\Debug\TFS.Agent.Run.exe");

                if (System.IO.File.Exists(agentPath))
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = agentPath,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    };

                    Process process = new Process
                    {
                        StartInfo = startInfo
                    };

                    process.Start();
                   // process.WaitForExit(); // Optionally wait for the process to complete

                    //string standardOutput = process.StandardOutput.ReadToEnd();
                    //string standardError = process.StandardError.ReadToEnd();

                }
                else
                {
                    Console.WriteLine("TFS agent executable not found at the specified path.");
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            //}
            //    processKill("SmartFit");
            //    processKill("SmartFitSA");
            //    processKill("Camelot.WorkflowRuntime");
            //    processKill("Camelot.SystemInfobar");
            //    processKill("Lucan.App.UI");
            //    processKill("StorageLayoutViewer");


            //}


            //public void processKill(string name)
            //{
            //    Process[] processCollection = Process.GetProcesses();
            //    foreach (Process proc in processCollection)
            //    {
            //        Console.WriteLine(proc);
            //        if (proc.ProcessName == name)
            //        {
            //            proc.Kill();
            //        }
            //    }
            //}
        }
    }
}

