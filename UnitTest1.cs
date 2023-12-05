using java.awt;
//using java.io;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
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
using System.IO;
using System.Text.RegularExpressions;
using com.sun.xml.@internal.bind.v2.model.core;
using com.sun.org.apache.xml.@internal.resolver.helpers;
using System.Windows;
using org.w3c.dom.css;
using com.sun.org.glassfish.external.statistics.annotations;
using java.util;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Linq.Expressions;
using javax.swing;
using sun.management.counter;
using com.sun.xml.@internal.ws.api.server;
using IronPdf;
using System.Drawing;
using Microsoft.Extensions.DependencyModel;
using sun.tools.java;
using Environment = System.Environment;
using AventStack.ExtentReports.Reporter;
using TestStack.BDDfy.Reporters.Html;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter.Configuration;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;
using Chilkat;

using System;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using sun.security.util;
using Castle.Core.Resource;
using static javax.jws.soap.SOAPBinding;
using System.Data;
using Microsoft.SqlServer.Management.HadrModel;
using AppiumWinApp.PageFactory;
using System.Xml.Linq;
using System.Xml;
using sun.tools.tree;

namespace AppiumWinApp
{
    public class SandRWorkFlow
    {
        private const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";

        //  private const string ApplicationPath = "C:\\Program Files (x86)\\GN Hearing\\Lucan\\App\\Lucan.App.UI.exe";

        //   private const string ApplicationPath = "C:\\Program Files (x86)\\GN Hearing\\Camelot\\WorkflowRuntime\\Camelot.WorkflowRuntime.exe";

        // private const string ApplicationPath = "C:\\Program Files (x86)\\GN Hearing\\Camelot\\System Configuration\\Camelot.SystemConfiguration.exe";
        //    private string ApplicationPath = "C:\\Program Files (x86)\\ReSound\\SmartFit\\SmartFitSA.exe";
        //   private const string ApplicationPath = "C:\\Program Files (x86)\\ReSound\\SmartFit\\SmartFit.exe";

      //     private const string ApplicationPath = "F:\\S&R\\4.1.0 (Beta 3)\\S&R Setup.exe";

        public static WindowsDriver<WindowsElement> session;
        private string ApplicationPath = null;
        protected static IOSDriver<IOSElement> AlarmClockSession;   // Temporary placeholder until Windows namespace exists
        protected static IOSDriver<IOSElement> DesktopSession;

        private static ExtentReports extent;
        private static ExtentHtmlReporter htmlReporter;
        private static ExtentTest test;

        /* Variable Declaration */

        public static string workFlowProductSelection= "WindowsForms10.SysTreeView32.app.0.27a2811_r17_ad1";
        public static string storageLayOutDate = "WindowsForms10.Window.8.app.0.2804c64_r17_ad1";
        public static string algoTestProp = "";
       



        [OneTimeSetUp]
        public void Setup()
        {
            /* Thread.Sleep(2000);

             DesiredCapabilities appCapabilities = new DesiredCapabilities();
             appCapabilities.SetCapability("app", ApplicationPath);
             appCapabilities.SetCapability("deviceName", "WindowsPC");
             session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
             Thread.Sleep(10000);
             session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);*/

            //  session.FindElementByName("Diagnostics").Click();
            //  session.Close();

            String textDir = Directory.GetCurrentDirectory();


          //  htmlReporter = new ExtentHtmlReporter("F:\\Winium\\AppiumWinApp\\AppiumWinApp\\report.html");
            htmlReporter = new ExtentHtmlReporter(textDir+"\\report.html");

         //   htmlReporter.LoadConfig("F:\\Winium\\AppiumWinApp\\AppiumWinApp\\ExtentConfig.xml");

            htmlReporter.LoadConfig(textDir+"\\ExtentConfig.xml");

            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }


        /* This is to launch FDTS tool to perform actions***/
        [Test, Order(1)]

        public void TestWorkFlowTest()
        {

            test = extent.CreateTest("Testing FDTS WorkFlow");

            FunctionLibrary lib = new FunctionLibrary();


            ApplicationPath = "C:\\Program Files (x86)\\GN Hearing\\Camelot\\WorkflowRuntime\\Camelot.WorkflowRuntime.exe";
            Thread.Sleep(2000);

            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", ApplicationPath);
            appCapabilities.SetCapability("deviceName", "WindowsPC");
         //   appCapabilities.SetCapability("appWorkingDir", "C:\\Program Files (x86)\\GN Hearing\\Camelot\\WorkflowRuntime");

            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            Thread.Sleep(10000);
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);

            test.Log(Status.Pass, "Test Work Flow launched successfully");

            try
            {
                System.IO.Directory.Delete(@"C:\Users\Public\Documents\Camelot\Logs", true);

            }
            catch(Exception e)
            {

            }

            Thread.Sleep(2000);

            /*Test workflow*/
            Thread.Sleep(4000);

            //   session.SwitchTo().ActiveElement();


            session.FindElementByAccessibilityId("textBoxFilter").Clear();

            string devName = "LT962-DRW-UP";
            session.FindElementByAccessibilityId("textBoxFilter").SendKeys(devName);
            Thread.Sleep(1000);
            session.FindElementByAccessibilityId("textBoxFilter").SendKeys(Keys.Tab);
            Thread.Sleep(2000);

            //  var prdName = session.FindElementsByClassName("WindowsForms10.SysTreeView32.app.0.27a2811_r7_ad1");

            var prdName = session.FindElementsByClassName(workFlowProductSelection);
            var name = prdName[0].FindElementByXPath("*/*"); ;

            string txt = name.GetAttribute("Name");

            name.Click();

            Actions action = new Actions(session);
            action.MoveToElement(name).Click().DoubleClick().Build().Perform();
            session.FindElementByName(devName+" (Final)").Click();
    
          /*  InputSimulator sim = new InputSimulator();
            sim.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
            sim.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
            sim.Keyboard.KeyPress(VirtualKeyCode.DOWN);*/

            session.FindElementByName("Continue >>").Click();

            // session.FindElementByName("Stop").Click();
            Thread.Sleep(2000);

            //  session.SwitchTo().Window("Serial Number");
            Thread.Sleep(4000);

            session.SwitchTo().Window(session.WindowHandles.First());


            session.SwitchTo().ActiveElement();
            //  session.SwitchTo().Frame("1");
            //   WindowsElement text = (WindowsElement)session.SwitchTo().Alert(session.CurrentWindowHandle);

            var secondWindow = session.FindElementsByClassName(workFlowProductSelection);
            /*
                        sim.Mouse.MoveMouseTo(645, 574);
                        sim.Mouse.LeftButtonClick();*/

            //  sim.Keyboard.KeyPress(VirtualKeyCode.TAB);

            //IKeyboardSimulator keyboardSimulator = sim.Keyboard.KeyPress(VirtualKeyCode.SHIFT+Tab);


            Thread.Sleep(2000);
            //   session.FindElementByName("Stop").Click();

            // sim.Keyboard.KeyPress(VirtualKeyCode.TAB);
            // sim.Mouse.LeftButtonClick(); 



            if (devName.Contains("LT"))
            {
                var wait = new WebDriverWait(session, TimeSpan.FromSeconds(200));

                // var sts = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Name("Continue")));

                session.FindElementByName("Continue >>").Click();
                Thread.Sleep(2000);

                session.SwitchTo().Window(session.WindowHandles.First());
                session.SwitchTo().ActiveElement();
                // session.FindElementByAccessibilityId("buttonReadFromDevice").Click();
                session.FindElementByName("Read").Click();

                session.FindElementByName("Continue >>").Click();
                Thread.Sleep(2000);

                test.Log(Status.Pass, "Flashing is started for Device" + devName);


                // session.FindElementByName("Full").Click();
                //    session.FindElementByName("Optimized").Click();

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
                        session.FindElementByAccessibilityId("textBoxPassword").SendKeys("1234");
                        session.FindElementByName("Continue >>").Click();
                        Thread.Sleep(4000);

                    }
                }

                catch (Exception e)
                {
                    Console.WriteLine(e);

                    Thread.Sleep(2000);
                    session.FindElementByAccessibilityId("textBoxPassword").SendKeys("1234");
                    session.FindElementByName("Continue >>").Click();


                    //  Thread.Sleep(200000);
                    /*
                                        session.FindElementByAccessibilityId("textBoxPassword").SendKeys("1234");
                                        session.FindElementByName("Continue >>").Click();*/

                }
                Thread.Sleep(2000);


                var currentWindowHandle = session.CurrentWindowHandle;
                currentWindowHandle.First();

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
                    /* Get Active window Test Execution with flashing steps*/
                    session = FunctionLibrary.clickOnCloseTestFlow(session, "Test Execution");
                    session = lib.waitForElementToBeClickable(session, "Close");

                    test.Pass("Succesfully Flashed");
                    test.Info("Device: " + devName + " flashed");




                }


            }
            else
            {
                session.FindElementByAccessibilityId("textBoxSN").Click();
                session.FindElementByAccessibilityId("textBoxSN").SendKeys("2000801965");

            }


            Thread.Sleep(6000);

            session = FunctionLibrary.clickOnCloseTestFlow(session, "Program Selection");

            /*  session.SwitchTo().Window(session.WindowHandles.First());
              session.SwitchTo().ActiveElement();*/
            session = lib.waitUntilElementExists(session, "Stop", 0);
            session.FindElementByName("Stop").Click();


            // session.FindElementByName("Stop").Click();

            session.SwitchTo().Window(session.WindowHandles.First());
            Thread.Sleep(2000);
            session.FindElementByName("Next Round >>").Click();
            Thread.Sleep(2000);
            session.SwitchTo().Window(session.WindowHandles.First());
            Thread.Sleep(2000);
            session.FindElementByName("Stop").Click();
            Thread.Sleep(2000);
            session.SwitchTo().Window(session.WindowHandles.First());
            Thread.Sleep(2000);
            session.FindElementByName("Shutdown").Click();


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
                    Assert.Fail();

                    break;


                }

                counter++;
            }

            Console.WriteLine("Line number: {0}", counter);

            file.Close();

            Thread.Sleep(4000);
            //   Console.ReadLine();

            // session.FindElementByAccessibilityId("button1").Click();

            // session.FindElementById("comboBoxUserName").Click();
            //session.FindElementById("textBoxPassword").Click();




            //session.FindElementById("textBoxPassword").SendKeys("1234");
            /*   var ele = session.FindElementsByClassName("WindowsForms10.EDIT.app.0.3c3ecb7_r9_ad1");

               string text = ele[0].GetAttribute("AutomationId");*/

            /*  session.FindElementByAccessibilityId("textBoxPassword").SendKeys("1234");
              session.FindElementByName("Continue >>").Click();
  */

            /* foreach (var ele2 in ele)
            {
                ele2.Click();

                Console.WriteLine(ele2.Text.ToString());
            }

            WindowsElement pwd = session.FindElementByName("Group:");
            Thread.Sleep(2000);
            pwd.SendKeys("1234");*/


            /* Console.WriteLine(ele[0].GetAttribute("AutomationId"));

             ele[0].SendKeys("1234");*/

            //  session.FindElementByName("Continue >>").Click();

            /* ele = session.FindElementsByClassName("WindowsForms10.BUTTON.app.0.3c3ecb7_r9_ad1");

             Console.WriteLine(ele[1].GetAttribute("Name").ToString());

             ele[1].Click();*/



            /*
                        session.FindElementById("textBoxFiltertextBoxFilter").Click();

                        session.FindElementById("textBoxFilter").SendKeys("RT962-DRW");*/

        }

        /* This to work with FSW */

        [Test,Order(2)]
        public void launchFSW()
        {
            test = extent.CreateTest("Fitting HI through FSW");

            FunctionLibrary lib = new FunctionLibrary();
            

           ApplicationPath = "C:\\Program Files (x86)\\ReSound\\SmartFit\\SmartFitSA.exe";
           Thread.Sleep(2000);

            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", ApplicationPath);
            appCapabilities.SetCapability("deviceName", "WindowsPC");
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            Thread.Sleep(10000);
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            Thread.Sleep(12000);
            session.Manage().Window.Maximize();


            /*Thread.Sleep(2000);*/

            var wait = new WebDriverWait(session, TimeSpan.FromSeconds(20));
            var div = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("ListBoxItem")));

            var text_Button = session.FindElementsByClassName("ListBoxItem");

            test.Log(Status.Pass, "FSW is launched successfully");

            // string text2 = text_Button[1].GetAttribute("AutomationId");
            int counter = 0;
            string PatientName = null;
            string PatientDescription = null;
            foreach (var element in text_Button)
            {
                if (counter== 5)
                {
                    PatientName = element.GetAttribute("AutomationId");
                    PatientDescription=element.GetAttribute("Name");
                }
                 
                counter = counter + 1;
            }

            lib.clickOnAutomationId(session,PatientDescription, PatientName);

            /* Click on Fit patient*/

            lib.clickOnAutomationId(session, "Fit Patient", "StandAloneAutomationIds.DetailsAutomationIds.FitAction");

            test.Pass("Patient is clicked");

            Thread.Sleep(12000);

            session.Close();

            ApplicationPath = "C:\\Program Files (x86)\\ReSound\\SmartFit\\SmartFit.exe";

            appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", ApplicationPath);
            appCapabilities.SetCapability("deviceName", "WindowsPC");
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            Thread.Sleep(10000);
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);

            try

            {
                lib.clickOnElementWithIdonly(session,"ConnectionAutomationIds.ConnectAction");
                test.Pass("Connect button is clicked");

            }

            catch (Exception e)
            {
                
                Console.WriteLine(e.Message);
                test.Fail(e.Message);

            }

            // Thread.Sleep(20000);

            
          

            int buttonCount = 0;
            try
            {

                WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(60));
                waitForMe.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("Button")));

                do
                {
                    session.SwitchTo().ActiveElement();

                    if (buttonCount>=1)
                    {
                        session.SwitchTo().ActiveElement();
                        session= ModuleFunctions.getControlsOfParentWindow(session, "ScrollViewer", test);
                        try
                        {
                            session.FindElementByAccessibilityId("StateMachineAutomationIds.ContinueAction").Click();
                           
                             waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(40));
                             waitForMe.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("Button")));


                        }
                        catch {

                            

                           // Thread.Sleep(2000);
                        }

                    }

                    buttonCount =buttonCount+1;

                  //  Thread.Sleep(10000);

                  /*  WebDriverWait waitForMe = new WebDriverWait(session, new TimeSpan(20));
                    var txtLocation = session.FindElementByAccessibilityId("StateMachineAutomationIds.ContinueAction");
                    waitForMe.Until(pred => txtLocation.Displayed);*/

                } while (session.FindElementByAccessibilityId("StateMachineAutomationIds.ContinueAction").Enabled);
            }

            //while (session.FindElementByAccessibilityId("StateMachineAutomationIds.ContinueAction").Enabled);
            catch(Exception e)
            {
                // Thread.Sleep(4000);
                /*Click on Fit Patient in Profile screen */

                test.Log(Status.Info,"No Intermediate Windows popped up");
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
                    lib.clickOnAutomationId(session,"All","ContentTextBlock");
                    lib.clickOnElementWithIdonly(session,"FittingAutomationIds.GainAutomationIds.AdjustmentItemsAutomationIds.Increase");

                    int increment = 0;
                    do
                    {
                        lib.clickOnElementWithIdonly(session, "FittingAutomationIds.GainAutomationIds.AdjustmentItemsAutomationIds.Increase");
                        increment = increment + 1;
                        test.Pass("Increment Gain is clicked for :"+increment+" times");

                    } while (increment <= 4);

                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }

                try
                {
                    Thread.Sleep(10000);
                    lib.clickOnElementWithIdonly(session,"FittingAutomationIds.SaveAction");

                    try
                    {
                        lib.clickOnElementWithIdonly(session, "SkipButton");

                    }
                    catch(Exception e1)
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
                test.Pass("Click on FSW Exit button");

                Thread.Sleep(8000);

                lib.processKill("SmartFitSA");

      

            }

           
        }

        [Test,Order(3)]
        /* This is to launch S&R tool to perform actions*/
        public void SandRTest()
        {
            //   ModuleFunctions.verifyIfReportsExisted(test);

            test = extent.CreateTest("S&R WorkFlow");

            try
            {
                System.IO.Directory.Delete(@"C:\CaptureBase\Reports", true);

                test.Pass("All files are deleted");

            }
            catch (Exception e)
            {
                test.Fail("No files found to be deleted");
            }
            

            FunctionLibrary lib = new FunctionLibrary();

            DesiredCapabilities desktopCapabilities = new DesiredCapabilities();
            desktopCapabilities.SetCapability("app", "Root");
            DesktopSession = new IOSDriver<IOSElement>(new Uri(WindowsApplicationDriverUrl), desktopCapabilities);
            DesktopSession.FindElementByName("Service & Repair Tool  4.6").Click();

            // DesktopSession.Keyboard.PressKey("TAB");

            Thread.Sleep(8000);

            DesktopSession.Close();

            session = ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\GN Hearing\\Lucan\\App\\Lucan.App.UI.exe", "C:\\Program Files (x86)\\GN Hearing\\Lucan\\App");

            test.Log(Status.Pass, "S&R Tool launched successfully");
            session.FindElementByName("Device Info").Click();
            Thread.Sleep(2000);


             lib = new FunctionLibrary();

               lib.getDeviceInfo(session);
               Thread.Sleep(1000);

            // FunctionLibrary lib = new FunctionLibrary();

            /*
                   //     lib.getDeviceInfo(session);
                        Thread.Sleep(1000);


                        session.FindElementByName("Settings").Click();
                        Thread.Sleep(2000);


                        *//*Identifying Color*//*

                        session.FindElementByName("Settings").Click();
                        Thread.Sleep(2000);
                        var ele = session.FindElementsByClassName("ComboBox");
                        ele[0].Click();
                        Thread.Sleep(2000);

                        WindowsElement red = session.FindElementByName("Blue");
                        red.Click();
                        Thread.Sleep(10000);
                        Console.WriteLine(ele[0].Text.ToString());

                        //Click check box

                        session.FindElementByName("Connect to hearing instrument automatically").Click();

                       *//*  Identifying Side *//*

                        ele = session.FindElementsByClassName("ComboBox");
                        ele[2].Click();
                        Thread.Sleep(10000);
                        // Side.FindElementByClassName("ListBoxItem").SetImmediateValue(" ");
                        WindowsElement RightSide = session.FindElementByName("Right");
                        RightSide.Click();
                        Console.WriteLine(ele[2].Text.ToString());
                        ele[2].Click();
                        WindowsElement LeftSide = session.FindElementByName("Left");
                        LeftSide.Click();
                        Console.WriteLine(ele[2].Text.ToString());
                        Thread.Sleep(5000);

                        *//* Identifying checkbox *//*

                        session.FindElementByName("Connect to hearing instrument automatically").Click();
                        Thread.Sleep(5000);

                     *//*   Identifying Device *//*

                        ele = session.FindElementsByClassName("ComboBox");
                        do
                        {

                        } while (!ele[1].Enabled);


                        ele[1].Click();
                        Thread.Sleep(8000);

                        WindowsElement Speedlink = session.FindElementByName("SpeedLink");

                        Thread.Sleep(2000);

                        Speedlink.Click();
                       // Speedlink.SendKeys(Keys.Enter);

                        Console.WriteLine(ele[1].Text.ToString());
                        //ele[1].Click();
                        Thread.Sleep(1000);
            */


            /* Capture SERVICES by Surya */
            session.FindElementByName("Services").Click();
            test.Log(Status.Pass, "Clicked on Services.");

            session = lib.functionWaitForName(session, "Capture");
            test.Log(Status.Pass, "Clicked on Capture.");


            session = lib.functionWaitForName(session, "LOGIN REQUIRED");

            lib.clickOnElementWithIdonly(session, "PasswordBox");
            session.FindElementByAccessibilityId("PasswordBox").SendKeys("112233");

            Thread.Sleep(2000);
            session.FindElementByName("Login").Click();
            session = lib.functionWaitForName(session, "CAPTURE");

            ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\GN Hearing\\Lucan\\App\\Lucan.App.UI.exe", "C:\\Program Files (x86)\\GN Hearing\\Lucan\\App");

            session = lib.waitForElement(session, "OK");

            Thread.Sleep(4000);
            string path = (@"C:\Users\Public\Documents\Camelot\Logs\DKCPHHPF2PNBPF-prapuv-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");

            lib.fileVerify(path, test,"Capturing the hearing");
            Thread.Sleep(2000);
            var btnClose = session.FindElementByAccessibilityId("PART_Close");
            btnClose.Click();
            Thread.Sleep(2000);

            /* Algo Tet Lab */

            test = extent.CreateTest("Test ALT WorkFlow");

/*            ModuleFunctions.altTestLab(session, test,device);
*/            Thread.Sleep(2000);

            /*Peforming Restore*/

           // ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\GN Hearing\\Lucan\\App\\Lucan.App.UI.exe");
            desktopCapabilities = new DesiredCapabilities();
            desktopCapabilities.SetCapability("app", "Root");
            DesktopSession = new IOSDriver<IOSElement>(new Uri(WindowsApplicationDriverUrl), desktopCapabilities);
            DesktopSession.FindElementByName("Service & Repair Tool 4.6").Click();

            // DesktopSession.Keyboard.PressKey("TAB");

            Thread.Sleep(8000);

            DesktopSession.Close();

            session = ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\GN Hearing\\Lucan\\App\\Lucan.App.UI.exe", "C:\\Program Files (x86)\\GN Hearing\\Lucan\\App");

            test.Log(Status.Pass, "S&R Tool launched successfully");
            session.FindElementByName("Device Info").Click();
            Thread.Sleep(2000);
            session.FindElementByName("Services").Click();

          
            /********************************************/

            Thread.Sleep(2000);

            session = lib.functionWaitForName(session, "Restore");
            Thread.Sleep(2000);

            session = lib.functionWaitForName(session, "LOGIN REQUIRED");

            lib.clickOnElementWithIdonly(session, "PasswordBox");
            session.FindElementByAccessibilityId("PasswordBox").SendKeys("112233");

            Thread.Sleep(2000);
            session.FindElementByName("Login").Click();
            session = lib.functionWaitForName(session, "READ");
            session = lib.functionWaitForName(session, "FIND");
            session = lib.waitForElement(session, "SELECT");
            session = lib.functionWaitForName(session, "RESTORE");

            //  session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);

            ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\GN Hearing\\Lucan\\App\\Lucan.App.UI.exe", "C:\\Program Files (x86)\\GN Hearing\\Lucan\\App");
            session = lib.waitForElement(session, "OK");
            test.Log(Status.Pass, "Restore is successful.");
            var btncls = session.FindElementByAccessibilityId("PART_Close");
            btncls.Click();

            Thread.Sleep(1000);

            /** Verify AlgoTet Lab**/
            ModuleFunctions.checkADLValue(session, test, "", "");

            Thread.Sleep(1000);
            path = (@"C:\Users\Public\Documents\Camelot\Logs\DKCPHHPF2PNBPF-prapuv-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");

            lib.fileVerify(path, test,"Restoring the hearing");

            /* This is to check if Capture and Restore files are existing*/
            /************************************************************/
            ModuleFunctions.verifyIfReportsExisted(test);

            /*Kill Acrobat reader*/

            Process[] processCollection = Process.GetProcesses();

            foreach (Process proc in processCollection)
            {

                Console.WriteLine(proc);
                if (proc.ProcessName == "Acrobat")
                {
                    proc.Kill();

                }
            }


            Assert.Pass();//Custom[@Class Name='ChannelsView']
        }

       
       

        /* Storage Layout */
        [Test,Order(4)]
        public void storageLayOut()
        {
            test = extent.CreateTest("Verify Storage Layout");


            FunctionLibrary lib = new FunctionLibrary();
            InputSimulator sim = new InputSimulator();


            session = ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\ReSound\\Palpatine6.7.4.21-RP-S\\StorageLayoutViewer.exe","C:\\Program Files(x86)\\ReSound\\Palpatine6.7.4.21 - RP - S");
            //    ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\ReSound\\Palpatine6.7.4.21-RP-S\\AlgoLabtest.Palpatine.exe");

            lib.waitUntilElementExists(session, "File", 0);
         //   lib.clickOnAutomationName(session, "File");

            Thread.Sleep(4000);

            var ext = session.FindElementsByXPath("(//*[@ClassName='"+storageLayOutDate+"'])[12]//*[@Name='File']");
            ext[0].Click();
            Thread.Sleep(2000);
            ext = session.FindElementsByXPath("(//*[@ClassName='"+storageLayOutDate+"'])[12]//*[@Name='File']//*[@LocalizedControlType='menu item'][2]");
            Actions action = new Actions(session);
            action.MoveToElement(ext[0]).Build().Perform();
            Thread.Sleep(2000);
            session.Keyboard.PressKey(Keys.Enter);

            Thread.Sleep(2000);
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

            // text= session.FindElementByClassName("WindowsForms10.Window.8.app.0.2804c64_r17_ad1").FindElementByName("Value Row 0").GetAttribute("Value");
            //  var text1 = session.FindElementsByXPath("(//*[@ControlType='ControlType.Custom'])");
            //  var text1 = session.FindElementsByXPath("(//*[@ClassName='WindowsForms10.Window.8.app.0.2804c64_r17_ad1' and @ControlType='ControlType.Table']//*[@ControlType='ControlType.Custom'])");
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


            string tableName = session.FindElementByXPath("(//*[@ClassName='"+storageLayOutDate+"'])[11]").GetAttribute("ControlType"); ;


            Console.WriteLine("Table Index value :" + Counter + tableName);

            tableName = session.FindElementByXPath("(//*[@ClassName='"+storageLayOutDate+"'])[11]").GetAttribute("AutomationId"); ;

            Console.WriteLine("Table Index value :" + Counter + tableName);

            /* tableName = session.FindElementByXPath("(//*[@ClassName='WindowsForms10.Window.8.app.0.2804c64_r17_ad1'])[11]//*[@ControlType='ControlType.Custom']").GetAttribute("Value");

             Console.WriteLine("Date Table Index value :" + Counter + tableName);*/


            tableName = session.FindElementByXPath("(//*[@ClassName='"+storageLayOutDate+"'])[11]").FindElementByName("Top Row").GetAttribute("Value");
            var childTable = session.FindElementsByXPath("(//*[@ClassName='"+storageLayOutDate+"'])[11]//*[@Name='Row 0']//*[@Name='Value Row 0']");

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

            // session.FindElementByName("Read from SpeedLink:0/Left").Click();
            //lib.clickOnAutomationName(session, "Read from SpeedLink:0/Left");

            /* Mini identification */

            var txt1 = session.FindElementsByName("2a000:0026 MiniIdentification");

            foreach (var item in txt1)
            {
                Console.WriteLine(item.GetAttribute("Name"));
                item.Click();

                test.Pass("2a000:0026 MiniIdentification is selected");
            }

            Thread.Sleep(2000);



            childTable = session.FindElementsByXPath("(//*[@ClassName='"+storageLayOutDate+"'])[11]//*[@Name='Row 6']//*[@Name='Value Row 6']");



            Thread.Sleep(2000);

            Counter = 0;
            foreach (var item in childTable)
            {
                Console.WriteLine("Index value :" + Counter + item.Text);
                Console.WriteLine("Index value :" + Counter + item.GetAttribute("Name"));
                Console.WriteLine("Index value :" + Counter + item.GetAttribute("Value"));
                Console.WriteLine("Index value :" + Counter + item.GetAttribute("ControlType"));
                Console.WriteLine("Child Table Value is " + item.GetAttribute("HelpText"));
                test.Log(Status.Pass,"Saved value for date +" + item.Text);
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

            /* Write Data*/
            lib.clickOnAutomationName(session, "File");

            Thread.Sleep(4000);

            ext = session.FindElementsByXPath("(//*[@ClassName='"+storageLayOutDate+"'])[12]//*[@Name='File']//*[@LocalizedControlType='menu item'][3]");
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

            catch(Exception e)
            {
                test.Pass("Writing Presets is done successfully.");
            }

            /*De select the check boxes*/

            txt[0].Click();
            Thread.Sleep(4000);
             sim.Keyboard.KeyPress(VirtualKeyCode.SPACE);
            Thread.Sleep(4000);

            txt1[0].Click();
            Thread.Sleep(4000);
            sim.Keyboard.KeyPress(VirtualKeyCode.SPACE);

            Thread.Sleep(4000);



            session.CloseApp();

            /*  tableName = session.FindElementByXPath("(//*[@ClassName='WindowsForms10.Window.8.app.0.2804c64_r17_ad1'])[11]").GetAttribute("ControlType"); ;


             Console.WriteLine("Table Index value :" + Counter + tableName);

             tableName = session.FindElementByXPath("(//*[@ClassName='WindowsForms10.Window.8.app.0.2804c64_r17_ad1'])[11]").GetAttribute("AutomationId"); ;

             Console.WriteLine("Table Index value :" + Counter + tableName);

             *//* tableName = session.FindElementByXPath("(//*[@ClassName='WindowsForms10.Window.8.app.0.2804c64_r17_ad1'])[11]//*[@ControlType='ControlType.Custom']").GetAttribute("Value");

              Console.WriteLine("Date Table Index value :" + Counter + tableName);*//*


             tableName = session.FindElementByXPath("(//*[@ClassName='WindowsForms10.Window.8.app.0.2804c64_r17_ad1'])[11]").FindElementByName("Top Row").GetAttribute("Value");
            */
            /*   childTable = session.FindElementsByXPath("(//*[@ClassName='WindowsForms10.Window.8.app.0.2804c64_r17_ad1'])[11]//*[@Name='Row 6']//*[@Name='Value Row 6']");


               foreach (var item in childTable)

               {
                   Console.WriteLine("Child Table Value is " + item.GetAttribute("Value"));
                   Console.WriteLine("Child Table Value is " + item.GetAttribute("Name"));
                   Console.WriteLine("Child Table Value is " + item.GetAttribute("ControlType"));
                   Console.WriteLine("Child Table Value is " + item.GetAttribute("HelpText"));
               }*/

        }         /* End of Storage Layout */



        [Test, Order(5)]
        public void aLgoTestLab()
        {
            FunctionLibrary lib= new FunctionLibrary();
            test = extent.CreateTest("Test ALT WorkFlow");

            session = null;

/*            ModuleFunctions.altTestLab(session, test);
*/

        }

        /* Installation/Un Installation Script  */

        public void installSandR()
        {


            FunctionLibrary lib = new FunctionLibrary();

            session = ModuleFunctions.sessionInitializeWODirectory("F:\\S&R\\S&R Tool 4.6 (Beta 2)\\S&R Tool Setup.exe");
            //    ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\ReSound\\Palpatine6.7.4.21-RP-S\\AlgoLabtest.Palpatine.exe");

            //  Thread.Sleep(4000);
            InputSimulator sim = new InputSimulator();


            //  sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);

            //   session.FindElementByAccessibilityId("1027").Click();

            lib.waitUntilElementExists(session, "Install", 0);
            lib.clickOnAutomationName(session, "Install");

            /*  lib.waitUntilElementExists(session, "File", 0);
              lib.clickOnAutomationName(session, "File");*/


        /*lib.waitUntilElementExists(session, "Close", 0);
        Thread.Sleep(2000);
        lib.clickOnAutomationName(session, "Close");*/

        lib.waitUntilElementExists(session, "Installation Successfully Completed", 0);
            lib.clickOnAutomationName(session, "Close");
            Thread.Sleep(8000);

            session = ModuleFunctions.sessionInitializeWODirectory("F:\\S&R\\S&R Tool 4.6 (Beta 2)\\S&R Tool Setup.exe");

            lib.waitUntilElementExists(session, "Uninstall", 0);
            lib.clickOnAutomationName(session, "Uninstall");

            sim.Keyboard.KeyPress(VirtualKeyCode.LEFT);
            sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);


            lib.waitUntilElementExists(session, "Uninstall Successfully Completed", 0);
            lib.clickOnAutomationName(session, "Close");

        }        /* End of Installation Script */

        /*This is demo Test Method*/

        [Test]
        public void demo()
        {
            string dir = "C:\\CaptureBase\\Reports";
            string filename = null;

            string today = DateTime.Now.ToString("yyyy-MM-dd");

            string[] files = Directory.GetFiles(dir + "\\" + today);

            foreach (string file in files)
            {
                Console.WriteLine(Path.GetFileName(file));
                filename = file;
                var splitVal = filename.Split("-");

                switch (splitVal[3])

                {
                    case "capture report":

                        test.Log(Status.Pass, "!!!!****Capture report generated****!!!!");
                        test.Log(Status.Pass, "File Name :" + filename);
                        // Extracting Image and Text content from Pdf Documents

                        // open a 128 bit encrypted PDF
                        var pdf = PdfDocument.FromFile(filename, "password");
                        //Get all text to put in a search index
                        string AllText = pdf.ExtractAllText();

                        //Get all Images
                        IEnumerable<System.Drawing.Image> AllImages = pdf.ExtractAllImages();

                        //Or even find the precise text and images for each page in the document
                        for (var index = 0; index < pdf.PageCount; index++)
                        {
                            int PageNumber = index + 1;
                            string Text = pdf.ExtractTextFromPage(index);
                            IEnumerable<System.Drawing.Image> Images = pdf.ExtractImagesFromPage(index);



                            // Taking a string
                            String str = Text;
                            // This is to capture all the lable names and write them to report
                            String[] seperator = { "Capture Report Capture specification" };

                            // using the method
                            String[] strlist = str.Split(seperator,
                               StringSplitOptions.RemoveEmptyEntries);

                            foreach (String s in strlist)
                            {
                                Console.WriteLine(s);
                            }

                            String[] seperatorLables = { "\r\n" };
                            String[] lableNames = strlist[0].Split("\r\n");

                            foreach (String s in lableNames)
                            {
                                test.Log(Status.Pass, s + " is found.");
                            }

                            //This is to write lable values in the report
                            String[] spearator1 = { " " };

                            String[] reportValues = strlist[1].Split(spearator1,
                               StringSplitOptions.RemoveEmptyEntries);

                            foreach (String s in reportValues)
                            {
                                test.Log(Status.Pass, s + " is found.");

                            }

                        }


                        break;

                    case "restoration report":

                        test.Log(Status.Pass, "!!!!****Restoration report generated****!!!!");
                        test.Log(Status.Pass, "File Name +" + filename);
                        // Extracting Image and Text content from Pdf Documents

                        // open a 128 bit encrypted PDF
                        pdf = PdfDocument.FromFile(filename, "password");
                        //Get all text to put in a search index
                        AllText = pdf.ExtractAllText();

                        //Get all Images
                        AllImages = pdf.ExtractAllImages();

                        //Or even find the precise text and images for each page in the document
                        for (var index = 0; index < pdf.PageCount; index++)
                        {
                            int PageNumber = index + 1;
                            string Text = pdf.ExtractTextFromPage(index);
                            IEnumerable<System.Drawing.Image> Images = pdf.ExtractImagesFromPage(index);



                            // Taking a string
                            String str = Text;

                            String[] spearator = { "Restoration Report (original device or clone)" };

                            // using the method
                            String[] strlist = str.Split(spearator,
                               StringSplitOptions.RemoveEmptyEntries);

                            foreach (String s in strlist)
                            {
                                Console.WriteLine(s);
                            }

                            String[] seperatorLables = { "\r\n" };
                            String[] lableNames = strlist[0].Split("\r\n");

                            foreach (String s in lableNames)
                            {
                                test.Log(Status.Pass, s + " is found.");
                            }

                            //This is to write lable values in the report
                            String[] spearator1 = { " " };

                            String[] reportValues = strlist[1].Split(spearator1,
                               StringSplitOptions.RemoveEmptyEntries);

                            foreach (String s in reportValues)
                            {
                                test.Log(Status.Pass, s + " is found.");

                            }

                        }

                        break;
                }
            }



        }/* End of demo*/

        [OneTimeTearDown]
        public void GenerateReport()
        {
            extent.Flush();
        }
    }
}