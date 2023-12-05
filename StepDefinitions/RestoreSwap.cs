using Microsoft.SqlServer.Management.XEvent;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
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
using AventStack.ExtentReports.Model;

//using AppiumWinApp.PageFactory;

namespace AppiumWinApp.StepDefinitions
{
    [Binding]
    public class RestoreSwap
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

        public RestoreSwap(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            extent = ModuleFunctions.extent;
        }


        /** Opens Storagelayoutviewer
          * Connects the HI device
          * Saves the dump images by checking all 'OS' nodes using Storagelayoutviewer **/

        [When(@"\[Get the dump of connected device left of DumpB by storage layout ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)""]")]
        public void WhenGetTheDumpOfConnectedDeviceLeftOfDumpBByStorageLayoutAnd(string device, string side, string DeviceNo)
        {
            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());

            if (side.Equals("Left"))
            {
                if (device.Contains("RT") || device.Contains("C"))
                {
                    ModuleFunctions.socketA(session, test, device);
                }

                ModuleFunctions.takeDeviceDumpImage(session, test, device, "Device B", side, DeviceNo);
                test.Log(Status.Pass, " Dump image taken for Device B");

            }
        }


        /** Opens S&R tool
          * Navigates to services tab
          * Perfrom restore operation using SWAP Option **/


        [When(@"\[Perform Restore with above captured image using SWAP option ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)""]")]
        public void WhenPerformRestoreWithAboveCapturedImageUsingSWAPOption(string deviceSlNo, string DeviceLeftSlNo, string device, string side)
        
        {
            FunctionLibrary lib = new FunctionLibrary();


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

            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());



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
            session.FindElementByName("Device Info").Click();
            Thread.Sleep(2000);

            test.Log(Status.Pass, "S&R Tool launched successfully");


            if (device.Contains("RT") || device.Contains("RU"))
            {
                session.FindElementByName("Discover").Click();

                test.Log(Status.Pass, "Clicked on Discover.");

                session.SwitchTo().Window(session.WindowHandles.First());
                session.SwitchTo().ActiveElement();

                try
                {
                    session.FindElementByAccessibilityId("SerialNumberTextBox").SendKeys(DeviceLeftSlNo);
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
            lib.waitUntilElementExists(session, "textBoxSerialNumber",1);
            session.FindElementByAccessibilityId("textBoxSerialNumber").SendKeys(DeviceLeftSlNo);
            session = lib.functionWaitForId(session, "buttonFind");
            WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(50));
            ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\GN Hearing\\Lucan\\App\\Lucan.App.UI.exe", "C:\\Program Files (x86)\\GN Hearing\\Lucan\\App");
            session = lib.waitForElement(session, "SELECT");
            test.Log(Status.Pass, "Restore is successful.");
       

            try
            {
                session = lib.functionWaitForId(session, "radioButtonRestoreAfterRepairOrSwap");
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
        }


        /** Opens S&R tool
         * Navigates to services tab
         * Perfrom restore operation using SWAP Option **/


        [When(@"\[Perform Restore with above captured image using SWAP with left ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)""]")]
        public void WhenPerformRestoreWithAboveCapturedImageUsingSWAPWithLeft(string deviceSlNo, string DeviceLeftSlNo, string device, string side)
        
        {
            FunctionLibrary lib = new FunctionLibrary();

            if (device.Contains("RT") || device.Contains("RU"))
            {
                if (side.Equals("Left"))

                {
                    ModuleFunctions.socketA(session, test, device);
                }

                else if(side.Equals("Right"))
                {
                    ModuleFunctions.socketB(session, test, device);
                }
            }


            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());



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
            session.FindElementByName("Device Info").Click();
            Thread.Sleep(2000);

            test.Log(Status.Pass, "S&R Tool launched successfully");

            if (device.Contains("RT") || device.Contains("RU"))
            {
                session.FindElementByName("Discover").Click();

                test.Log(Status.Pass, "Clicked on Discover.");

                session.SwitchTo().Window(session.WindowHandles.First());
                session.SwitchTo().ActiveElement();

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
            ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\GN Hearing\\Lucan\\App\\Lucan.App.UI.exe", "C:\\Program Files (x86)\\GN Hearing\\Lucan\\App");
            session = lib.waitForElement(session, "SELECT");
            test.Log(Status.Pass, "Restore is successful.");


            try
            {               
                session = lib.functionWaitForId(session, "radioButtonRestoreAfterRepairOrSwap");
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
        }


        /** Opens Storagelayoutviewer
          * Connects the HI device
          * Saves the dump images by checking all 'OS' nodes using Storagelayoutviewer **/


        [When(@"\[Get the dump of connected device of left DumpC by storage layout ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)""]")]
        public void WhenGetTheDumpOfConnectedDeviceOfLeftDumpCByStorageLayout(string device, string side, string DeviceNo)
        {
            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());

            if (side.Equals("Left"))
            {
                if (device.Contains("RT") || device.Contains("C"))
                {
                    ModuleFunctions.socketA(session, test, device);
                }

                ModuleFunctions.takeDeviceDumpImage(session, test, device, "Device C", side, DeviceNo);
                test.Log(Status.Pass, " Dump image taken for Device C");

            }
            else if (side.Equals("Right"))
            {

                if (device.Contains("RT") || device.Contains("C"))
                {
                    ModuleFunctions.socketB(session, test, device);
                }

                ModuleFunctions.takeDeviceDumpImage(session, test, device, "Device B", side, DeviceNo);
                test.Log(Status.Pass, " Dump image taken for Device B ");
            }

        }


        /** Opens Storagelayoutviewer
          * Connects the HI device
          * Saves the dump images by checking all 'OS' nodes using Storagelayoutviewer **/

        [When(@"\[Get the dump of connected device of DumpD by storage layout ""([^""]*)"" and ""([^""]*)"" and ""([^""]*)""]")]
        public void WhenGetTheDumpOfConnectedDeviceOfDumpDByStorageLayout(string device, string side, string DeviceNo)
        {
            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());

            if (side.Equals("Right"))
            {
                if (device.Contains("RT") || device.Contains("C"))
                {
                    ModuleFunctions.socketB(session, test, device);
                }

                ModuleFunctions.takeDeviceDumpImage(session, test, device, "Device D", side, DeviceNo);
                test.Log(Status.Pass, " Dump image taken for Device D ");

            }

        }


        
        [Then(@"\[Do the dump comparison between two device DeviceC and DeviceD dumps(.*)]")]
        public void ThenDoTheDumpComparisonBetweenTwoDeviceDeviceCAndDeviceDDumps(string side)
        {
            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());
            FunctionLibrary lib = new FunctionLibrary();
            lib.dumpCompare1(side, test);
        }



    }// Class RestoreSwap

}//Name Space

