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

namespace AppiumWinApp.StepDefinitions
{
    [Binding]
    public class StepDefinitionFDTS
    {
        private readonly ScenarioContext _scenarioContext;
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

        public StepDefinitionFDTS(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            extent = ModuleFunctions.extent;

        }

        /** This is to clear exisisting dump image files in the c drive **/
        //
        [Given(@"\[Cleaning up dumps before execution starts]")]
        public void GivenCleaningUpDumpsBeforeExecutionStarts()
        {
            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());

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

            test.Log(Status.Pass, "All dumps are cleaned up");

        }

        /** Opens S&R tool 
         *  Navigates to settings tab
         *  changes the HI side selection **/

        [When(@"\[Change communication channel in S and RLeft(.*)]")]
        public void WhenChangeCommunicationChannelInSAndRLeft(string side)
        {
            FunctionLibrary lib = new FunctionLibrary();
            lib.processKill("msedge");
            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());

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

            session = ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\GN Hearing\\Lucan\\App\\Lucan.App.UI.exe", "C:\\Program Files (x86)\\GN Hearing\\Lucan\\App");

            Thread.Sleep(2000);

            test.Log(Status.Pass, "S&R Tool launched successfully");
            session.FindElementByName("Device Info").Click();
            Thread.Sleep(2000);

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
                WindowsElement RightSide = session.FindElementByName("Right");
                RightSide.Click();
            }
            else
            {
                WindowsElement LeftSide = session.FindElementByName("Left");
                LeftSide.Click();
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
            test.Log(Status.Pass, "Communication channel chanted to " + side);

        }


        /** Opens Storagelayoutviewer
          * Connects the HI device
          * Saves the dump images by checking all 'OS' nodes using Storagelayoutviewer **/

        [When(@"\[Get the dump of connected device by storage layout ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)""]")]
        public void WhenGetTheDumpOfConnectedDeviceByStorageLayout(string device, string side, string DeviceNo)
        {

            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());

            if (side.Equals("Left"))
            {
                Thread.Sleep(4000);

                if (device.Contains("RT") || device.Contains("NX") || device.Contains("RU") || device.Contains("C"))
                {

                    ModuleFunctions.socketA(session, test, device);
                }
                ModuleFunctions.takeDeviceDumpImage(session, test, device, "Device A", side, DeviceNo);
                test.Log(Status.Pass, " Dump image taken for Device A ");

            }
            else if (side.Equals("Right"))
            {


                Thread.Sleep(4000);

                if (device.Contains("RT") || device.Contains("NX") || device.Contains("RU") || device.Contains("C"))
                {
                    ModuleFunctions.socketB(session, test, device);
                }
                ModuleFunctions.takeDeviceDumpImage(session, test, device, "Device B", side, DeviceNo);
                test.Log(Status.Pass, " Dump image taken for Device B ");

            }

            else if (side.Equals("Cdevice"))
            {


                Thread.Sleep(4000);

                if (device.Contains("RT") || device.Contains("NX") || device.Contains("RU") || device.Contains("C"))
                {
                    ModuleFunctions.socketC(session, test, device);
                }
                ModuleFunctions.takeDeviceDumpImage(session, test, device, "Device C", side, DeviceNo);
                test.Log(Status.Pass, " Dump image taken for Device C ");

            }
            else
            {
                try
                {
                    if (System.IO.File.Exists(@"C:\Device C.xml"))
                    {
                        System.IO.File.Delete(@"C:\Device C.xml");
                    }
                }
                catch (Exception e) { }

                if (device.Contains("RT") || device.Contains("NX") || device.Contains("RU") || device.Contains("C"))
                {

                    ModuleFunctions.socketB(session, test, device);
                }
                ModuleFunctions.takeDeviceDumpImage(session, test, device, "Device C", side, DeviceNo);
                test.Log(Status.Pass, " Dump image taken for Device C ");

            }

        }



        /** Opens FDTS system configuration tool
         *  Navigates to System settings - Communication device
         *  Changes the Interface Channel side **/

        [Given(@"Lauch socket Driver ""([^""]*)""")]
        public void GivenLauchSocketDriver(string device)
        {
            if (device.Contains("RT") || device.Contains("RU") || device.Contains("NX"))
            {
                ModuleFunctions.socket(session, test, device);
            }

        }




        [Given(@"\[Change channel side in FDTS(.*)]")]
        public void WhenChangeChannelSideInFDTS(string side)
        {

            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());

            /** Selecting Device side in System Settings **/

            SystemPageFactory.launchSystemSettings(side, extent);


            test.Log(Status.Pass, "Channel changed to side: " + side);

        }


        /** Opens S&R tool
          * Navigates to services tab
          * Perfrom restore operation using RTS Option **/

        [When(@"\[Perform Restore with above captured image using RTS option ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)""]")]
        public void WhenPerformRestoreWithAboveCapturedImageUsingRTSOption(string DeviceLeftSlNo, string deviceSlNo, string device, string side)
        {
            FunctionLibrary lib = new FunctionLibrary();

            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());


            if (device.Contains("RT") || device.Contains("RU") || device.Contains("NX"))
            {
                if (side.Equals("Left"))
                {
                    ModuleFunctions.socketA(session, test, device);
                }

                else if (side.Equals("Right"))

                {
                    ModuleFunctions.socketB(session, test, device);
                }

                else if (side.Equals("Cdevice"))

                {
                    ModuleFunctions.socketC(session, test, device);
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

            session = ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\GN Hearing\\Lucan\\App\\Lucan.App.UI.exe", "C:\\Program Files (x86)\\GN Hearing\\Lucan\\App");
            Thread.Sleep(2000);
            test.Log(Status.Pass, "S&R Tool launched successfully");
            session.FindElementByName("Device Info").Click();
            Thread.Sleep(2000);

            if (device.Contains("RT") || device.Contains("RU") || device.Contains("NX"))
            {
                session.FindElementByName("Discover").Click();
                test.Log(Status.Pass, "Clicked on Discover.");
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


                    //lib.functionWaitForName(session, "Search"); 

                    //test.Log(Status.Pass, "Clicked on Search"); 

                    session = lib.waitForElement(session, "Model Name");

                    test.Log(Status.Pass, "Dook2 Dev");
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

            if (device.Contains("LT") || device.Contains("RE"))
            {

                session.FindElementByName("Connect to hearing instrument automatically").Click();
                Thread.Sleep(2000);
                session.FindElementByName("Connect to hearing instrument automatically").Click();
                Thread.Sleep(8000);
            }

            session.FindElementByName("Services").Click();
            Thread.Sleep(2000);
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
            ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\GN Hearing\\Lucan\\App\\Lucan.App.UI.exe", "C:\\Program Files (x86)\\GN Hearing\\Lucan\\App");
            Thread.Sleep(2000);
            session = lib.functionWaitForId(session, "buttonFind");

            WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(50));

            ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\GN Hearing\\Lucan\\App\\Lucan.App.UI.exe", "C:\\Program Files (x86)\\GN Hearing\\Lucan\\App");
            session = lib.waitForElement(session, "SELECT");
            test.Log(Status.Pass, "Restore is successful.");




            try
            {
                session = lib.functionWaitForId(session, "radioButtonRestoreAfterRTS");
                session = lib.functionWaitForName(session, "RESTORE");
            }
            catch (Exception e)
            {

            }

            session = ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\GN Hearing\\Lucan\\App\\Lucan.App.UI.exe", "C:\\Program Files (x86)\\GN Hearing\\Lucan\\App");
            session = lib.waitForElement(session, "OK");
            test.Log(Status.Pass, "Restore is successful.");
            var btncls1 = session.FindElementByAccessibilityId("PART_Close");
            btncls1.Click();
            Thread.Sleep(1000);

            if (device.Contains("RT") || device.Contains("RU") || device.Contains("NX"))
            {
                if (side.Equals("Left"))
                {
                    ModuleFunctions.socketA(session, test, device);
                }

                else if (side.Equals("Right"))

                {
                    ModuleFunctions.socketB(session, test, device);
                }

                else if (side.Equals("Cdevice"))

                {
                    ModuleFunctions.socketC(session, test, device);
                }
            }


        }

        /** Compares the dump image files **/

        [Then(@"\[Do the dump comparison between two device dumps(.*)]")]
        public void ThenDoTheDumpComparisonBetweenTwoDeviceDumps(string side)
        {
            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());
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
            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());

            FunctionLibrary lib = new FunctionLibrary();

            if (device.Contains("RT") || device.Contains("RU") || device.Contains("NX"))
            {
                ModuleFunctions.socketA(session, test, device);
                Thread.Sleep(3000);
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

                /** Clicks on "Fit patient" **/

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

                session.FindElementByName("Back").Click();

                Thread.Sleep(5000);

                session.FindElementByAccessibilityId("ConnectionAutomationIds.CommunicationInterfaceItems").Click();

                Thread.Sleep(2000);

                session.FindElementByName("Noahlink Wireless").Click();


                lib.clickOnAutomationId(session, "Connect", "SidebarAutomationIds.ConnectAction");

                Thread.Sleep(8000);

                Thread.Sleep(10000);

                try
                {
                    if (session.FindElementByName("Unassign").Enabled)
                    {
                        lib.clickOnAutomationName(session, "Assign Instruments");
                        Thread.Sleep(5000);

                        var SN = session.FindElementsByClassName("ListBoxItem");

                        Thread.Sleep(10000);

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

                /** Select the Connection Flow's "Continue" button to continue. **/

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

                /** Clicks on "Fit patient" button **/

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
                catch (Exception ex)
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

                    test.Pass("Save is successfully done and Close the FSW");

                }
                catch (Exception skip)

                {
                    Console.WriteLine(skip);
                }

                /** Exit the Fsw **/

                lib.clickOnElementWithIdonly(session, "SaveAutomationIds.PerformSaveAutomationIds.ExitAction");
                test.Pass("Click on FSW Exit button");

                Thread.Sleep(8000);

                lib.processKill("SmartFitSA");


            }

        }


        [Then(@"\[Do the Comparison between Azure Data and SandR Data]")]
        public void ThenDoTheComparisonBetweenAzureDataAndSandRData()
        {
            //throw new PendingStepException();

            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());


            FunctionLibrary lib = new FunctionLibrary();

            Thread.Sleep(15000);
            //session = lib.waitForElement(session, "Model Name");

            //Thread.Sleep(2000);
            //session.SwitchTo().Window(session.WindowHandles.First());
            lib.AzureFileCompare(session, test);

        }


        [Given(@"\[Launch FDTS]")]
        public void GivenLaunchFDTS()
        {
            Console.WriteLine("Given FDTS method");
            Thread.Sleep(5000);
        }

        [When(@"\[Select Device]")]
        public void WhenSelectDevice()
        {
            Console.WriteLine("When FDTS method");
        }

        [Then(@"\[Do Flashing]")]
        public void ThenDoFlashing()
        {
            Console.WriteLine("Do FDTS method");
        }

        /* S&R */

        [Given(@"\[Launch SandRTool]")]
        public void GivenLaunchSandRTool()
        {
            Console.WriteLine("Given FDTS method");
            Thread.Sleep(5000);
        }

        [When(@"\[Moving to Settings]")]
        public void WhenMovingtoSettings()
        {
            Console.WriteLine("When FDTS method");
        }

        [Then(@"\[Finish Capture]")]
        public void ThenFinishCapture()
        {
            Console.WriteLine("Do FDTS method");
        }

        [Given(@"\[HCP Launches WorkFlow And Flash Device]")]
        public void GivenHCPLaunchesWorkFlowAndFlashDevice()
        {
            Console.WriteLine("Second Scenario");
        }


        [AfterScenario]

        [Then(@"\[done]")]
        public void ThenDone()
        {

            //Process winApp = new Process();
            //winApp.StartInfo.FileName = "C:\\Program Files (x86)\\Windows Application Driver\\WinAppDriver.exe";
            //winApp.Kill();

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

            //}
            processKill("SmartFit");
            processKill("SmartFitSA");
            processKill("Camelot.WorkflowRuntime");
            processKill("Camelot.SystemInfobar");
            processKill("Lucan.App.UI");
            processKill("StorageLayoutViewer");


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
    }
}

