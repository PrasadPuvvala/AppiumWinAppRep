using System;
using System.Collections.Generic;
using System.Text;
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
using IronPdf;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AppiumWinApp.PageFactory;
using RazorEngine.Compilation.ImpromptuInterface.Dynamic;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Appium;
using System.Collections.ObjectModel;
using Microsoft.SqlServer.Management.XEvent;
using AventStack.ExtentReports.Model;
using Microsoft.Identity.Client.Extensions.Msal;
using sun.security.x509;
using javax.swing.plaf;
using javax.swing;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Management.Smo;
using Newtonsoft.Json;

namespace AppiumWinApp
{
    internal class ModuleFunctions
    {
        protected static WindowsDriver<WindowsElement> session;
        private const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        public static ExtentReports extent;
        private static ExtentHtmlReporter htmlReporter;
        private static ExtentTest test;
        public static ExtentReports extent1;
        public static string computer_name = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
        public static appconfigsettings config;
        static string configsettingpath = System.IO.Directory.GetParent(@"../../../").FullName
        + Path.DirectorySeparatorChar + "appconfig.json";

        /** Application launchhing **/
        public static WindowsDriver<WindowsElement> sessionInitialize(string name, string path)
        {
            string ApplicationPath = name;
            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", ApplicationPath);
            appCapabilities.SetCapability("platformName", "Windows");
            appCapabilities.SetCapability("deviceName", "WindowsPC");
            appCapabilities.SetCapability("appWorkingDir", path);
            appCapabilities.SetCapability("appArguments", "--run-as-administrator");
            appCapabilities.SetCapability("ms:waitForAppLaunch", "25");
            Thread.Sleep(8000);
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            Thread.Sleep(8000);
            session.Manage().Window.Maximize();
            return session;
        }

        /** FDTS Application launching **/
        public static WindowsDriver<WindowsElement> sessionInitialize1(string name, string path)
        { 
            string ApplicationPath = name;
            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", ApplicationPath);
            appCapabilities.SetCapability("platformName", "Windows");
            appCapabilities.SetCapability("deviceName", "WindowsPC");
            appCapabilities.SetCapability("appWorkingDir", path);
            appCapabilities.SetCapability("appArguments", "--run-as-administrator");
            Thread.Sleep(8000);
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            Thread.Sleep(8000);
            return session;
        }

            /**  FDTS Application launching from Bat files  **/
            public static WindowsDriver<WindowsElement> launchApp(string name, string dir)
        {
            string ApplicationPath = name;
            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", ApplicationPath);
            appCapabilities.SetCapability("platformName", "Windows");
            appCapabilities.SetCapability("deviceName", "WindowsPC");
            appCapabilities.SetCapability("appWorkingDir", dir);
            appCapabilities.SetCapability("appArguments", "--run-as-administrator");
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            Thread.Sleep(8000);
            return session;
        }

        public static WindowsDriver<WindowsElement> sessionInitializeWODirectory(string name)
        {
            string ApplicationPath = name;
            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", ApplicationPath);
            appCapabilities.SetCapability("platformName", "Windows");
            appCapabilities.SetCapability("deviceName", "WindowsPC");
            appCapabilities.SetCapability("appArguments", "--run-as-administrator");
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            Thread.Sleep(4000);
            return session;
        }


        /** Continue buttons click operation in fsw Connection flow **/
        
        public static WindowsDriver<WindowsElement> getControlsOfParentWindow(WindowsDriver<WindowsElement> session, string name, ExtentTest stepName)
        {
            var childTable = session.FindElementsByXPath("//*[@ClassName='" + name + "']//Text[@ClassName='TextBlock']");
            int counter = 0;
            foreach (var child in childTable)
            {
                if (counter == 1)
                {
                    stepName.Log(Status.Pass, "Continue is clicked in the screen +" + child.GetAttribute("Name"));

                }

                counter = counter + 1;
            }

            return session;
        }


        /** To verify the Extent reports and Capture & restore reports in alocated location **/

        public static void verifyIfReportsExisted(ExtentTest test)
        {

            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());

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

                        stepName.Log(Status.Pass, "!!!!****Capture report generated****!!!!");
                        stepName.Log(Status.Pass, "File Name :" + filename);
                        var pdf = PdfDocument.FromFile(filename, "password");
                        string AllText = pdf.ExtractAllText();
                        IEnumerable<System.Drawing.Image> AllImages = pdf.ExtractAllImages();
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
                                stepName.Log(Status.Pass, s + " is found.");
                            }
                            //This is to write lable values in the report
                            String[] spearator1 = { " " };

                            String[] reportValues = strlist[1].Split(spearator1,
                               StringSplitOptions.RemoveEmptyEntries);
                            foreach (String s in reportValues)
                            {
                                stepName.Log(Status.Pass, s + " is found.");

                            }

                        }
                        break;

                    case "restoration report":

                        stepName.Log(Status.Pass, "!!!!****Restoration report generated****!!!!");
                        stepName.Log(Status.Pass, "File Name +" + filename);
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
                                stepName.Log(Status.Pass, s + " is found.");
                            }
                            //This is to write lable values in the report
                            String[] spearator1 = { " " };
                            String[] reportValues = strlist[1].Split(spearator1,
                               StringSplitOptions.RemoveEmptyEntries);
                            foreach (String s in reportValues)
                            {
                                stepName.Log(Status.Pass, s + " is found.");

                            }

                        }
                        break;
                }
            }
        } /*End of verifyIfReportExisted*/



        /* AlgoLabTest Alter Value */

        public static void altTestLab(WindowsDriver<WindowsElement> session, ExtentTest stepName, string device, string DeviceNo,string DeviceType)
        {
            string jsonString = File.ReadAllText(configsettingpath);
            Dictionary<string, Dictionary<string, string>> algo = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(jsonString);

            FunctionLibrary lib = new FunctionLibrary();


            foreach (var algoPath in algo["Algo"])
            {
                if (device == algoPath.Key)
                {
                    Console.WriteLine($"{algoPath.Value}");
                    foreach (var workingDirectory in algo["WorkingDirectory"])
                    {
                        if (device == workingDirectory.Key)
                        {
                            Console.WriteLine($"{workingDirectory.Value}");
                            session = ModuleFunctions.sessionInitialize(algoPath.Value, workingDirectory.Value);

                            //if (device.Contains("RT") || device.Contains("RU")|| device.Contains("NX"))
                            if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
                            {
                                try
                                {


                                    ModuleFunctions.socketA(session, test, DeviceType);

                                }
                                catch { }
                                Thread.Sleep(2000);

                                string computer_name = System.Environment.GetEnvironmentVariable("COMPUTERNAME");

                                //if (device.Contains("RT"))
                                //{
                                //    session = ModuleFunctions.sessionInitialize(config.algo.Dooku2, config.workingdirectory.Dooku2);

                                //}

                                //else if (device.Contains("RU"))
                                //{
                                //    session = ModuleFunctions.sessionInitialize(config.algo.Dooku3, config.workingdirectory.Dooku3);
                                //}

                                //else
                                //{
                                //   session = ModuleFunctions.sessionInitialize(config.algo.Megnesium, config.workingdirectory.Megnesium);
                                //}

                                //session = ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\ReSound\\Dooku2.9.78.1\\AlgoLabtest.Dooku", "C:\\Program Files (x86)\\ReSound\\Dooku2.9.78.1");           
                                Actions actions = new Actions(session);
                                stepName.Log(Status.Pass, "Algo test lab is launched successfully.");
                                Thread.Sleep(2000);
                                session.FindElementByName("ADL").Click();
                                stepName.Log(Status.Pass, "Moved to ADL page successfully.");
                                Thread.Sleep(2000);
                                session.FindElementByAccessibilityId("FINDICON").Click();
                                Thread.Sleep(2000);
                                session.FindElementByAccessibilityId("FINDICON").Click();
                                Thread.Sleep(15000);
                                var SN = session.FindElementsByClassName("DataGrid");
                                Thread.Sleep(10000);
                                var SO = SN[1].FindElementsByClassName("TextBlock");
                                foreach (WindowsElement value in SO)
                                {
                                    string S = value.Text;

                                    if (S.Contains(DeviceNo))

                                    {
                                        //value.Text.Contains(DeviceNo);
                                        value.Click();
                                    }

                                }
                                lib.functionWaitForName(session, "Connect");
                                try
                                {
                                    lib.functionWaitForName(session, "Dooku2.C6.TDI.9.78.0.0");

                                    Thread.Sleep(2000);

                                    lib.functionWaitForName(session, "Use when connecting next time");

                                    Thread.Sleep(2000);

                                    lib.functionWaitForName(session, "Connect");
                                }
                                catch (Exception)
                                { }
                                session = lib.waitUntilElementExists(session, "Gatt database detected - save of presets will be disabled until presets are read", 0);
                                lib.waitForElement(session, "Persist ADL to device when writing presets");
                                lib.clickOnElementWithIdonly(session, "textBox1_5");
                                Thread.Sleep(2000);
                                session.FindElementByAccessibilityId("textBox1_5").Clear();
                                Thread.Sleep(2000);
                                session.FindElementByAccessibilityId("textBox1_5").SendKeys("1");
                                stepName.Log(Status.Pass, "Altered value is 1");
                                /* Save changes in Fitting tab*/
                                lib.clickOnAutomationName(session, "Fitting");
                                Thread.Sleep(4000);
                                var RW = session.FindElementsByClassName("Button");
                                Thread.Sleep(5000);
                                RW[23].Click();
                                Thread.Sleep(1000);
                                session.FindElementByName("OK").Click();
                                session = lib.waitUntilElementExists(session, "Preset Programs read on left side.", 0);
                                RW[24].Click();
                                session.FindElementByName("OK").Click();
                                session = lib.waitUntilElementExists(session, "Preset Programs stored on left side.", 0);
                                lib.clickOnAutomationName(session, "ADL");
                                lib.clickOnElementWithIdonly(session, "ClearNodeButton");
                                Thread.Sleep(4000);
                                lib.functionWaitForName(session, "Connect");
                                session = lib.waitUntilElementExists(session, "Gatt database detected - save of presets will be disabled until presets are read", 0);

                                try
                                {
                                    if ((session.FindElementByAccessibilityId("textBox1_5").Text.ToString()) == "1.000" || (session.FindElementByAccessibilityId("textBox1_5").Text.ToString()) == "1")
                                    {
                                        Console.WriteLine("Saved Value is :" + session.FindElementByAccessibilityId("textBox1_5").Text.ToString());
                                        stepName.Log(Status.Pass, "Saved Value is :" + session.FindElementByAccessibilityId("textBox1_5").Text.ToString());
                                        Assert.Pass();
                                        session.CloseApp();
                                    }
                                    else
                                    {
                                        stepName.Log(Status.Pass, "Saved Value is :" + session.FindElementByAccessibilityId("textBox1_5").Text.ToString());

                                        Assert.Fail();

                                        session.CloseApp();
                                    }
                                    lib.clickOnAutomationName(session, "OK");
                                    session.CloseApp();
                                }
                                catch (Exception)
                                { }
                                session.CloseApp();
                                lib.clickOnAutomationName(session, "OK");
                                session.CloseApp();
                            }
                            if (DeviceType.Equals("Wired") || DeviceType.Equals("D1rechargeableWired"))
                            {
                                //if (device.Contains("LT"))
                                //{
                                //    session = ModuleFunctions.sessionInitialize(config.algo.Palpatine6, config.workingdirectory.Palpatine6);
                                //    //session = ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\ReSound\\Palpatine6.7.4.21-RP-S\\AlgoLabtest.Palpatine.exe", "C:\\Program Files (x86)\\ReSound\\Palpatine6.7.4.21-RP-S");
                                //}
                                //else
                                //{
                                //    session = ModuleFunctions.sessionInitialize(config.algo.Dooku1, config.workingdirectory.Dooku1);
                                //    //session = ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\ReSound\\Dooku1.1.20.1\\AlgoLabtest.Dooku.exe", "C:\\Program Files (x86)\\ReSound\\Dooku1.1.20.1");

                                //}
                                stepName.Log(Status.Pass, "Algo test lab is launched successfully.");
                                Thread.Sleep(2000);
                                session.FindElementByName("ADL").Click();
                                stepName.Log(Status.Pass, "Moved to ADL page successfully.");

                                /*Select Speedlink from the interface selection pop up*/

                                try
                                {
                                    if (session.FindElementByName("Activated device Hipro").Enabled)
                                    {
                                        lib.clickOnElementWithIdonly(session, "BUTTON");
                                        var interfaceButton = session.FindElementsByClassName("Image");

                                        Console.WriteLine(interfaceButton.ToString());
                                        int counter = 0;
                                        foreach (var item in interfaceButton)
                                        {
                                            Console.WriteLine("Indexvalue is" + counter + ":" + item.GetAttribute("HelpText"));
                                            counter = counter + 1;
                                            stepName.Log(Status.Info, "Interfaces Available for selection: " + item.GetAttribute("HelpText"));
                                        }
                                        Console.WriteLine(interfaceButton.ToString());
                                        Thread.Sleep(2000);
                                        interfaceButton[12].Click();
                                        stepName.Log(Status.Info, "Speedlink is selected");
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.ToString());
                                    stepName.Log(Status.Pass, "Selected Interface is Speedlink");
                                }

                                /*Getting all button from ADL screen*/

                                var button = session.FindElementsByClassName("Button");
                                int Counter = 0;
                                foreach (var item in button)
                                {
                                    string btnName = item.GetAttribute("Value");
                                    Counter = Counter + 1;
                                    Console.WriteLine(Counter.ToString());
                                }
                                if (device.Contains("LT"))
                                {
                                    button[29].Click();
                                }
                                else
                                {
                                    lib.clickOnElementWithIdonly(session, "ConnectLeftRightBothUserControlLeftButton");

                                    try
                                    {
                                        session = lib.waitUntilElementExists(session, "Gatt database detected - save of presets will be disabled until presets are read", 0);

                                        if (computer_name.Equals("FSWIRAY80"))
                                        {
                                            session.Keyboard.PressKey(Keys.Enter);
                                        }

                                    }
                                    catch (Exception e)
                                    {

                                    }
                                }
                                session = lib.waitUntilElementExists(session, "Gatt database detected - save of presets will be disabled until presets are read", 0);

                                try
                                {
                                    session.FindElementByName("OK").Click();
                                }
                                catch (Exception)
                                {

                                }

                                lib.waitForElement(session, "Persist ADL to device when writing presets");
                                lib.clickOnElementWithIdonly(session, "textBox1_5");
                                Thread.Sleep(2000);
                                session.FindElementByAccessibilityId("textBox1_5").Clear();
                                Thread.Sleep(2000);
                                session.FindElementByAccessibilityId("textBox1_5").SendKeys("1");
                                stepName.Log(Status.Pass, "Altered value is 1");

                                /* Save changes in Fitting tab*/

                                lib.clickOnAutomationName(session, "Fitting");
                                Thread.Sleep(2000);
                                button = session.FindElementsByClassName("Button");
                                Counter = 0;
                                foreach (var item in button)
                                {
                                    string btnName = item.GetAttribute("Value");
                                    Console.WriteLine("Index" + Counter + "Is" + item.GetAttribute("HelpText"));
                                    Counter = Counter + 1;
                                    Console.WriteLine(Counter.ToString());
                                    Console.WriteLine(item.GetAttribute("HelpText"));
                                }

                                if (device.Contains("LT"))
                                {
                                    button[22].Click();
                                }
                                else
                                {
                                    try
                                    {
                                        Thread.Sleep(4000);
                                        button[25].Click();
                                        session.Keyboard.PressKey(Keys.Enter);
                                        session = lib.waitUntilElementExists(session, "Preset Programs read on left side.", 0);
                                    }
                                    catch (Exception e)
                                    {

                                    }
                                }

                                try
                                {
                                    session.FindElementByName("OK").Click();
                                }
                                catch (Exception)
                                { }
                                button = session.FindElementsByClassName("Button");
                                Counter = 0;
                                foreach (var item in button)
                                {
                                    string btnName = item.GetAttribute("Value");
                                    Console.WriteLine("Index of write" + Counter + "Is" + item.GetAttribute("HelpText"));
                                    Counter = Counter + 1;
                                    Console.WriteLine(Counter.ToString());
                                    Console.WriteLine(item.GetAttribute("HelpText"));
                                }
                                session = lib.waitUntilElementExists(session, "Preset Programs read on left side.", 0);
                                if (device.Contains("LT"))
                                {
                                    button[23].Click();
                                }
                                else
                                {
                                    try
                                    {
                                        button[26].Click();
                                        session.Keyboard.PressKey(Keys.Enter);
                                    }
                                    catch (Exception e)
                                    {

                                    }
                                }

                                try
                                {
                                    session.FindElementByName("OK").Click();
                                }
                                catch (Exception)
                                { }
                                session = lib.waitUntilElementExists(session, "Preset Programs stored on left side.", 0);
                                lib.clickOnAutomationName(session, "ADL");
                                session = lib.waitUntilElementExists(session, "buttonReloadData", 1);

                                /*Disconnecting left HI*/

                                if (device.Contains("LT"))
                                {
                                    button[29].Click();
                                }
                                else
                                {
                                    lib.clickOnElementWithIdonly(session, "ConnectLeftRightBothUserControlLeftButton");
                                    try
                                    {
                                        session = lib.waitUntilElementExists(session, "Left side disconnected", 0);
                                    }
                                    catch (Exception e)
                                    {

                                    }
                                }
                                session = lib.waitUntilElementExists(session, "Left side disconnected", 0);

                                /*Reconnecting left HI*/

                                if (device.Contains("LT"))
                                {
                                    button[29].Click();
                                }
                                else
                                {
                                    lib.clickOnElementWithIdonly(session, "ConnectLeftRightBothUserControlLeftButton");
                                    try
                                    {
                                        session = lib.waitUntilElementExists(session, "Gatt database detected - save of presets will be disabled until presets are read", 0);

                                        if (computer_name.Equals("FSWIRAY80"))
                                        {
                                            session.Keyboard.PressKey(Keys.Enter);

                                        }
                                    }
                                    catch (Exception e)
                                    {

                                    }
                                }
                                session = lib.waitUntilElementExists(session, "Gatt database detected - save of presets will be disabled until presets are read", 0);

                                try
                                {
                                    session.FindElementByName("OK").Click();
                                }
                                catch (Exception)
                                {

                                }

                                try
                                {
                                    if ((session.FindElementByAccessibilityId("textBox1_5").Text.ToString()) == "1.000" || (session.FindElementByAccessibilityId("textBox1_5").Text.ToString()) == "1")
                                    {

                                        Console.WriteLine("Saved Value is :" + session.FindElementByAccessibilityId("textBox1_5").Text.ToString());
                                        stepName.Log(Status.Pass, "Saved Value is :" + session.FindElementByAccessibilityId("textBox1_5").Text.ToString());

                                        Assert.Pass();
                                        session.CloseApp();

                                    }
                                    else
                                    {
                                        stepName.Log(Status.Pass, "Saved Value is :" + session.FindElementByAccessibilityId("textBox1_5").Text.ToString());

                                        Assert.Fail();

                                        session.CloseApp();

                                    }
                                }

                                catch (Exception e)
                                {
                                    session.CloseApp();

                                    if (device.Contains("RE"))
                                    {
                                        session.Keyboard.PressKey(Keys.Enter);

                                    }
                                    else
                                    {

                                    }
                                }
                            }//End of AlgoTets Lab  

                            //if (device.Contains("LT") || device.Contains("RE"))
                        }
                    }
                }
            }             
        }



        /* Calling Socketbox and passing commands */

        public static void socket(WindowsDriver<WindowsElement> session, ExtentTest test, string DeviceType)

        {
            FunctionLibrary lib = new FunctionLibrary(); 
            Thread.Sleep(10000); 
            
            try
            {
                session = ModuleFunctions.launchApp(Directory.GetCurrentDirectory() + "\\LaunchSocket.bat", Directory.GetCurrentDirectory());
                Thread.Sleep(2000);
            }

            catch (System.InvalidOperationException e) 
            
            { }

            DesiredCapabilities desktopCapabilities = new DesiredCapabilities();
            desktopCapabilities.SetCapability("platformName", "Windows");
            desktopCapabilities.SetCapability("app", "Root");
            desktopCapabilities.SetCapability("deviceName", "WindowsPC");
            session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), desktopCapabilities);
            WindowsElement applicationWindow = null;

            var openWindows = session.FindElementsByClassName("ConsoleWindowClass"); 
            
            foreach (var window in openWindows)
            {
                
                if (window.GetAttribute("Name").StartsWith("WinAppDriver"))
                { }

                else
                {
                    applicationWindow = window;
                    break;
                }
            }

            var topLevelWindowHandle = applicationWindow.GetAttribute("NativeWindowHandle");
            topLevelWindowHandle = int.Parse(topLevelWindowHandle).ToString("X");
            DesiredCapabilities capabilities = new DesiredCapabilities();
            capabilities.SetCapability("deviceName", "WindowsPC");
            capabilities.SetCapability("appTopLevelWindow", topLevelWindowHandle);
            session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), capabilities);           
            Thread.Sleep(2000); 
            
            try
            {
               // if (device.Contains("RT962-DRW"))
               if(DeviceType.Equals("Non-Rechargeable"))
                {
                    session.Keyboard.SendKeys("3");
                    session.Keyboard.SendKeys(Keys.Enter);
                    Thread.Sleep(3000);
                    session.Keyboard.SendKeys("A");
                    session.Keyboard.SendKeys(Keys.Enter);
                    Thread.Sleep(3000);
                }

               // else if(device.Contains("RT")|| device.Contains("NX")|| device.Contains("RU") && device.Contains("C"))
               else if(DeviceType.Equals("Rechargeable"))
                {
                    session.Keyboard.SendKeys("3");
                    session.Keyboard.SendKeys(Keys.Enter);
                    Thread.Sleep(3000);
                    session.Keyboard.SendKeys("A");
                    session.Keyboard.SendKeys(Keys.Enter);
                    Thread.Sleep(10000);
                    session.Keyboard.SendKeys("a");
                    session.Keyboard.SendKeys(Keys.Enter);
                    Thread.Sleep(5000);
                }   
                
            }

            catch (Exception e)
            {
                if (e.GetType().ToString() == "System.InvalidOperationException")
                {
                    try
                    {
                        session = ModuleFunctions.launchApp(Directory.GetCurrentDirectory() + "\\LaunchSocket.bat", Directory.GetCurrentDirectory());
                        Thread.Sleep(2000);
                    }
                    catch (Exception) { }
                    { }
                    desktopCapabilities = new DesiredCapabilities();
                    desktopCapabilities.SetCapability("platformName", "Windows");
                    desktopCapabilities.SetCapability("app", "Root");
                    desktopCapabilities.SetCapability("deviceName", "WindowsPC");
                    session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), desktopCapabilities);
                    applicationWindow = null;
                    openWindows = session.FindElementsByClassName("ConsoleWindowClass"); 
                    
                    foreach (var window in openWindows)
                    {
                        if (window.GetAttribute("Name").StartsWith("WinAppDriver"))
                        { 
                        
                        }
                        else
                        {
                            applicationWindow = window;
                            break;
                        }
                    }
                    topLevelWindowHandle = applicationWindow.GetAttribute("NativeWindowHandle");
                    topLevelWindowHandle = int.Parse(topLevelWindowHandle).ToString("X");
                    capabilities = new DesiredCapabilities();
                    capabilities.SetCapability("deviceName", "WindowsPC");
                    capabilities.SetCapability("appTopLevelWindow", topLevelWindowHandle);
                    session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), capabilities); Thread.Sleep(2000); 
                    
                    try
                    {
                        if (DeviceType.Equals("Non-Rechargeable"))
                        {
                            session.Keyboard.SendKeys("3");
                            session.Keyboard.SendKeys(Keys.Enter);
                            session.Keyboard.SendKeys("A");
                            session.Keyboard.SendKeys(Keys.Enter);
                        }
                        if (DeviceType.Equals("Rechargeable"))
                        {
                            session.Keyboard.SendKeys("3");
                            session.Keyboard.SendKeys(Keys.Enter);
                            Thread.Sleep(3000);
                            session.Keyboard.SendKeys("A");
                            session.Keyboard.SendKeys(Keys.Enter);
                            Thread.Sleep(6000);
                            session.Keyboard.SendKeys("a");
                            session.Keyboard.SendKeys(Keys.Enter);
                            Thread.Sleep(5000);
                        }
                    }
                    catch (Exception) { }
                }
            }
        }



        /* Passing Commands to Socketbox for Left Side Device */

        public static void socketA(WindowsDriver<WindowsElement> session, ExtentTest test, string DeviceType)

        
        {
            FunctionLibrary lib = new FunctionLibrary();
            Thread.Sleep(10000);
            DesiredCapabilities desktopCapabilities = new DesiredCapabilities();
            desktopCapabilities.SetCapability("platformName", "Windows");
            desktopCapabilities.SetCapability("app", "Root");
            desktopCapabilities.SetCapability("deviceName", "WindowsPC");
            session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), desktopCapabilities);
            WindowsElement applicationWindow = null;

            var openWindows = session.FindElementsByClassName("ConsoleWindowClass");

            foreach (var window in openWindows)
            {

                if (window.GetAttribute("Name").StartsWith("WinAppDriver"))
                { }

                else
                {
                    applicationWindow = window;
                    break;
                }
            }

            var topLevelWindowHandle = applicationWindow.GetAttribute("NativeWindowHandle");
            topLevelWindowHandle = int.Parse(topLevelWindowHandle).ToString("X");
            DesiredCapabilities capabilities = new DesiredCapabilities();
            capabilities.SetCapability("deviceName", "WindowsPC");
            capabilities.SetCapability("appTopLevelWindow", topLevelWindowHandle);
            session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), capabilities);
            Thread.Sleep(2000);

            try
            {

                //if (device.Contains("RT962-DRW"))
                if (DeviceType.Equals("Non-Rechargeable"))
                {
                    session.Keyboard.SendKeys("B");
                    Thread.Sleep(8000);
                    session.Keyboard.SendKeys(Keys.Enter);
                    Thread.Sleep(8000);
                    session.Keyboard.SendKeys("a");
                    Thread.Sleep(8000);
                    session.Keyboard.SendKeys(Keys.Enter);
                    Thread.Sleep(8000);
                    session.Keyboard.SendKeys("A");
                    Thread.Sleep(8000);
                    session.Keyboard.SendKeys(Keys.Enter);
                    Thread.Sleep(8000);
                }

                //else if (device.Contains("RT") || device.Contains("NX") || device.Contains("RU") && device.Contains("C"))
                else if (DeviceType.Equals("Rechargeable"))
                {
                    //session.Keyboard.SendKeys("B");
                    //Thread.Sleep(2000);
                    //session.Keyboard.SendKeys(Keys.Enter);
                    //Thread.Sleep(2000);
                    session.Keyboard.SendKeys("A");
                    Thread.Sleep(8000);
                    session.Keyboard.SendKeys(Keys.Enter);
                    Thread.Sleep(2000);
                    session.Keyboard.SendKeys("a");
                    Thread.Sleep(8000);
                    session.Keyboard.SendKeys(Keys.Enter);
                    Thread.Sleep(2000);
                    session.Keyboard.SendKeys("A");
                    Thread.Sleep(8000);
                    session.Keyboard.SendKeys(Keys.Enter);
                    Thread.Sleep(2000);
                    session.Keyboard.SendKeys("a");
                    Thread.Sleep(4000);
                    session.Keyboard.SendKeys(Keys.Enter);
                }
            }

            catch (Exception e)
            {
                if (e.GetType().ToString() == "System.InvalidOperationException")
                {
                    desktopCapabilities = new DesiredCapabilities();
                    desktopCapabilities.SetCapability("platformName", "Windows");
                    desktopCapabilities.SetCapability("app", "Root");
                    desktopCapabilities.SetCapability("deviceName", "WindowsPC");
                    session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), desktopCapabilities);
                    applicationWindow = null;
                    openWindows = session.FindElementsByClassName("ConsoleWindowClass");

                    foreach (var window in openWindows)
                    {
                        if (window.GetAttribute("Name").StartsWith("WinAppDriver"))
                        {

                        }
                        else
                        {
                            applicationWindow = window;
                            break;
                        }
                    }

                    topLevelWindowHandle = applicationWindow.GetAttribute("NativeWindowHandle");
                    topLevelWindowHandle = int.Parse(topLevelWindowHandle).ToString("X");
                    capabilities = new DesiredCapabilities();
                    capabilities.SetCapability("deviceName", "WindowsPC");
                    capabilities.SetCapability("appTopLevelWindow", topLevelWindowHandle);
                    session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), capabilities); Thread.Sleep(2000); 
                    
                    
                    try
                    {

                        if (DeviceType.Equals("Non-Rechargeable"))
                        {
                            session.Keyboard.SendKeys("b");
                            Thread.Sleep(8000);
                            session.Keyboard.SendKeys(Keys.Enter);
                            Thread.Sleep(8000);
                            session.Keyboard.SendKeys("a");
                            Thread.Sleep(8000);
                            session.Keyboard.SendKeys(Keys.Enter);
                            Thread.Sleep(8000);
                            session.Keyboard.SendKeys("A");
                            Thread.Sleep(8000);
                            session.Keyboard.SendKeys(Keys.Enter);
                            Thread.Sleep(8000);
                        }

                        // else if (device.Contains("RT") || device.Contains("NX") || device.Contains("RU") && device.Contains("C"))
                        if (DeviceType.Equals("Rechargeable"))
                        {
                            //session.Keyboard.SendKeys("B");
                            //Thread.Sleep(2000);
                            //session.Keyboard.SendKeys(Keys.Enter);
                            //Thread.Sleep(2000);
                            session.Keyboard.SendKeys("A");
                            Thread.Sleep(8000);
                            session.Keyboard.SendKeys(Keys.Enter);
                            Thread.Sleep(2000);
                            session.Keyboard.SendKeys("a");
                            Thread.Sleep(8000);
                            session.Keyboard.SendKeys(Keys.Enter);
                            Thread.Sleep(2000);
                            session.Keyboard.SendKeys("A");
                            Thread.Sleep(8000);
                            session.Keyboard.SendKeys(Keys.Enter);
                            Thread.Sleep(2000);
                            session.Keyboard.SendKeys("a");
                            Thread.Sleep(4000);
                            session.Keyboard.SendKeys(Keys.Enter);
                        }
                    }
                    catch (Exception) { }
                }
            }
        }



        /* Passing Commands for SocketBox for Right Side Device B */


        public static void socketB(WindowsDriver<WindowsElement> session, ExtentTest test, string DeviceType)

        {
            FunctionLibrary lib = new FunctionLibrary();
            Thread.Sleep(10000);         
            DesiredCapabilities desktopCapabilities = new DesiredCapabilities();
            desktopCapabilities.SetCapability("platformName", "Windows");
            desktopCapabilities.SetCapability("app", "Root");
            desktopCapabilities.SetCapability("deviceName", "WindowsPC");
            session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), desktopCapabilities);
            WindowsElement applicationWindow = null;

            var openWindows = session.FindElementsByClassName("ConsoleWindowClass");

            foreach (var window in openWindows)
            {

                if (window.GetAttribute("Name").StartsWith("WinAppDriver"))
                { }

                else
                {
                    applicationWindow = window;
                    break;
                }
            }

            var topLevelWindowHandle = applicationWindow.GetAttribute("NativeWindowHandle");
            topLevelWindowHandle = int.Parse(topLevelWindowHandle).ToString("X");
            DesiredCapabilities capabilities = new DesiredCapabilities();
            capabilities.SetCapability("deviceName", "WindowsPC");
            capabilities.SetCapability("appTopLevelWindow", topLevelWindowHandle);
            session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), capabilities);

            Thread.Sleep(2000);

            try
            {
                //
                //if (device.Contains("RT962-DRW"))
                if (DeviceType.Equals("Non-Rechargeable"))
                {
                    session.Keyboard.SendKeys("A");
                    Thread.Sleep(8000);
                    session.Keyboard.SendKeys(Keys.Enter);
                    Thread.Sleep(8000);
                    session.Keyboard.SendKeys("b");
                    Thread.Sleep(8000);
                    session.Keyboard.SendKeys(Keys.Enter);
                    Thread.Sleep(8000);
                    session.Keyboard.SendKeys("B");
                    Thread.Sleep(8000);
                    session.Keyboard.SendKeys(Keys.Enter);
                    Thread.Sleep(8000);
                }

               // else if(device.Contains("RT") || device.Contains("NX") || device.Contains("RU") && device.Contains("C"))
                else if (DeviceType.Equals("Rechargeable"))
                {

                    //session.Keyboard.SendKeys("A");
                    //Thread.Sleep(2000);                   
                    //session.Keyboard.SendKeys(Keys.Enter);
                    //Thread.Sleep(2000);
                    //session.Keyboard.SendKeys("b");
                    //Thread.Sleep(2000);
                    session.Keyboard.SendKeys(Keys.Enter);
                    Thread.Sleep(2000);
                    session.Keyboard.SendKeys("B");                   
                    session.Keyboard.SendKeys(Keys.Enter);
                    Thread.Sleep(8000);
                    session.Keyboard.SendKeys("b");                   
                    session.Keyboard.SendKeys(Keys.Enter);
                    Thread.Sleep(2000);
                    session.Keyboard.SendKeys("B");
                    session.Keyboard.SendKeys(Keys.Enter);
                    Thread.Sleep(8000);
                    session.Keyboard.SendKeys("b");
                    session.Keyboard.SendKeys(Keys.Enter);

                }
            }

            catch (Exception e)
            {
                if (e.GetType().ToString() == "System.InvalidOperationException")
                {
                    desktopCapabilities = new DesiredCapabilities();
                    desktopCapabilities.SetCapability("platformName", "Windows");
                    desktopCapabilities.SetCapability("app", "Root");
                    desktopCapabilities.SetCapability("deviceName", "WindowsPC");
                    session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), desktopCapabilities);
                    applicationWindow = null;
                    openWindows = session.FindElementsByClassName("ConsoleWindowClass");

                    foreach (var window in openWindows)
                    {
                        if (window.GetAttribute("Name").StartsWith("WinAppDriver"))
                        {

                        }
                        else
                        {
                            applicationWindow = window;
                            break;
                        }
                    }

                    topLevelWindowHandle = applicationWindow.GetAttribute("NativeWindowHandle");
                    topLevelWindowHandle = int.Parse(topLevelWindowHandle).ToString("X");
                    capabilities = new DesiredCapabilities();
                    capabilities.SetCapability("deviceName", "WindowsPC");
                    capabilities.SetCapability("appTopLevelWindow", topLevelWindowHandle);
                    session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), capabilities);                                        
                    Thread.Sleep(2000); 
                    
                    try
                    {
                        //if (device.Contains("RT"))
                        if (DeviceType.Equals("Non-Rechargeable"))
                        {
                            session.Keyboard.SendKeys("a");
                            Thread.Sleep(8000);
                            session.Keyboard.SendKeys(Keys.Enter);
                            Thread.Sleep(8000);
                            session.Keyboard.SendKeys("b");
                            Thread.Sleep(8000);
                            session.Keyboard.SendKeys(Keys.Enter);
                            Thread.Sleep(8000);
                            session.Keyboard.SendKeys("B");
                            Thread.Sleep(8000);
                            session.Keyboard.SendKeys(Keys.Enter);
                            Thread.Sleep(8000);
                        }

                        //else if (device.Contains("RT") || device.Contains("NX") || device.Contains("RU") && device.Contains("C"))
                        else if (DeviceType.Equals("Rechargeable"))
                        {
                            session.Keyboard.SendKeys("a");
                            Thread.Sleep(1000);
                            session.Keyboard.SendKeys(Keys.Enter);
                            Thread.Sleep(1000);
                            session.Keyboard.SendKeys("b");
                            Thread.Sleep(1000);
                            session.Keyboard.SendKeys(Keys.Enter);
                            Thread.Sleep(1000);
                            session.Keyboard.SendKeys("B");
                            Thread.Sleep(1000);
                            session.Keyboard.SendKeys(Keys.Enter);
                            Thread.Sleep(4000);
                            session.Keyboard.SendKeys("b");
                            Thread.Sleep(2000);
                            session.Keyboard.SendKeys(Keys.Enter);
                            Thread.Sleep(1000);
                            session.Keyboard.SendKeys("B");
                            session.Keyboard.SendKeys(Keys.Enter);
                            Thread.Sleep(8000);
                            session.Keyboard.SendKeys("b");
                            session.Keyboard.SendKeys(Keys.Enter);
                        }
                    }
                    catch (Exception) { }
                }
            }
        }



        public static void socketC(WindowsDriver<WindowsElement> session, ExtentTest test, string DeviceType)

        {
            FunctionLibrary lib = new FunctionLibrary();
            Thread.Sleep(10000);
            DesiredCapabilities desktopCapabilities = new DesiredCapabilities();
            desktopCapabilities.SetCapability("platformName", "Windows");
            desktopCapabilities.SetCapability("app", "Root");
            desktopCapabilities.SetCapability("deviceName", "WindowsPC");
            session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), desktopCapabilities);
            WindowsElement applicationWindow = null;

            var openWindows = session.FindElementsByClassName("ConsoleWindowClass");

            foreach (var window in openWindows)
            {

                if (window.GetAttribute("Name").StartsWith("WinAppDriver"))
                { }

                else
                {
                    applicationWindow = window;
                    break;
                }
            }

            var topLevelWindowHandle = applicationWindow.GetAttribute("NativeWindowHandle");
            topLevelWindowHandle = int.Parse(topLevelWindowHandle).ToString("X");
            DesiredCapabilities capabilities = new DesiredCapabilities();
            capabilities.SetCapability("deviceName", "WindowsPC");
            capabilities.SetCapability("appTopLevelWindow", topLevelWindowHandle);
            session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), capabilities);

            Thread.Sleep(2000);

            try
            {
                //if (device.Contains("RT962-DRW"))
                if (DeviceType.Equals("Non-Rechargeable"))
                {
                    session.Keyboard.SendKeys("A");
                    Thread.Sleep(8000);
                    session.Keyboard.SendKeys(Keys.Enter);
                    Thread.Sleep(8000);
                    session.Keyboard.SendKeys("b");
                    Thread.Sleep(8000);
                    session.Keyboard.SendKeys(Keys.Enter);
                    Thread.Sleep(8000);
                    session.Keyboard.SendKeys("B");
                    Thread.Sleep(8000);
                    session.Keyboard.SendKeys(Keys.Enter);
                    Thread.Sleep(8000);
                }

              //  else if (device.Contains("RT") && device.Contains("C"))
              else if (DeviceType.Equals("Rechargeable"))
                {

                    session.Keyboard.SendKeys("A");
                    Thread.Sleep(2000);
                    session.Keyboard.SendKeys(Keys.Enter);
                    Thread.Sleep(2000);
                    //session.Keyboard.SendKeys("b");
                    //Thread.Sleep(2000);
                    session.Keyboard.SendKeys(Keys.Enter);
                    Thread.Sleep(2000);
                    session.Keyboard.SendKeys("B");
                    Thread.Sleep(8000);
                    session.Keyboard.SendKeys(Keys.Enter);
                    Thread.Sleep(8000);
                    session.Keyboard.SendKeys("C");
                    Thread.Sleep(8000);
                    session.Keyboard.SendKeys(Keys.Enter);
                    Thread.Sleep(8000);
                    session.Keyboard.SendKeys("c");
                    Thread.Sleep(8000);
                    session.Keyboard.SendKeys(Keys.Enter);
                    Thread.Sleep(8000);

                }
            }

            catch (Exception e)
            {
                if (e.GetType().ToString() == "System.InvalidOperationException")
                {
                    desktopCapabilities = new DesiredCapabilities();
                    desktopCapabilities.SetCapability("platformName", "Windows");
                    desktopCapabilities.SetCapability("app", "Root");
                    desktopCapabilities.SetCapability("deviceName", "WindowsPC");
                    session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), desktopCapabilities);
                    applicationWindow = null;
                    openWindows = session.FindElementsByClassName("ConsoleWindowClass");

                    foreach (var window in openWindows)
                    {
                        if (window.GetAttribute("Name").StartsWith("WinAppDriver"))
                        {

                        }
                        else
                        {
                            applicationWindow = window;
                            break;
                        }
                    }

                    topLevelWindowHandle = applicationWindow.GetAttribute("NativeWindowHandle");
                    topLevelWindowHandle = int.Parse(topLevelWindowHandle).ToString("X");
                    capabilities = new DesiredCapabilities();
                    capabilities.SetCapability("deviceName", "WindowsPC");
                    capabilities.SetCapability("appTopLevelWindow", topLevelWindowHandle);
                    session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), capabilities);
                    Thread.Sleep(2000);

                    try
                    {
                        // if (device.Contains("RT") && device.Contains("RU"))
                        if (DeviceType.Equals("Non-Rechargeable"))
                        {
                            session.Keyboard.SendKeys("a");
                            Thread.Sleep(8000);
                            session.Keyboard.SendKeys(Keys.Enter);
                            Thread.Sleep(8000);
                            session.Keyboard.SendKeys("b");
                            Thread.Sleep(8000);
                            session.Keyboard.SendKeys(Keys.Enter);
                            Thread.Sleep(8000);
                            session.Keyboard.SendKeys("B");
                            Thread.Sleep(8000);
                            session.Keyboard.SendKeys(Keys.Enter);
                            Thread.Sleep(8000);
                        }

                       // else if (device.Contains("RT") && device.Contains("C"))
                        else if (DeviceType.Equals("Rechargeable"))
                        {
                            session.Keyboard.SendKeys("a");
                            Thread.Sleep(1000);
                            session.Keyboard.SendKeys(Keys.Enter);
                            Thread.Sleep(1000);
                            session.Keyboard.SendKeys("b");
                            Thread.Sleep(1000);
                            session.Keyboard.SendKeys(Keys.Enter);
                            Thread.Sleep(1000);
                            session.Keyboard.SendKeys("B");
                            Thread.Sleep(1000);
                            session.Keyboard.SendKeys(Keys.Enter);
                            Thread.Sleep(4000);
                            session.Keyboard.SendKeys("b");
                            Thread.Sleep(2000);
                            session.Keyboard.SendKeys(Keys.Enter);
                            Thread.Sleep(1000);
                        }
                    }
                    catch (Exception) { }
                }
            }
        }



        /* Passing Commands for SocketBox for Right Side Device B */
        public static void socket1(WindowsDriver<WindowsElement> session, ExtentTest test)


        {
            FunctionLibrary lib = new FunctionLibrary(); 
            Thread.Sleep(10000); 
            
            try
            {
                session = ModuleFunctions.launchApp(Directory.GetCurrentDirectory() + "\\LaunchSocket.bat", Directory.GetCurrentDirectory());
                Thread.Sleep(2000);
            }
            catch (System.InvalidOperationException e) 
            { }

            DesiredCapabilities desktopCapabilities = new DesiredCapabilities();
            desktopCapabilities.SetCapability("platformName", "Windows");
            desktopCapabilities.SetCapability("app", "Root");
            desktopCapabilities.SetCapability("deviceName", "WindowsPC");
            session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), desktopCapabilities);
            WindowsElement applicationWindow = null;
            var openWindows = session.FindElementsByClassName("ConsoleWindowClass"); 
            
            foreach (var window in openWindows)
            {
                if (window.GetAttribute("Name").StartsWith("WinAppDriver"))
                { 
                
                }
                else
                {
                    applicationWindow = window;
                    break;
                }
            }

            var topLevelWindowHandle = applicationWindow.GetAttribute("NativeWindowHandle");
            topLevelWindowHandle = int.Parse(topLevelWindowHandle).ToString("X");
            DesiredCapabilities capabilities = new DesiredCapabilities();
            capabilities.SetCapability("deviceName", "WindowsPC");
            capabilities.SetCapability("appTopLevelWindow", topLevelWindowHandle);
            session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), capabilities);
            Thread.Sleep(2000);
            
            try
            {
                session.Keyboard.SendKeys("3"); 
                session.Keyboard.SendKeys(Keys.Enter); 
                session.Keyboard.SendKeys("B");
                session.Keyboard.SendKeys(Keys.Enter); 
                Thread.Sleep(3000);
            }
            catch (Exception e)
            {
                if (e.GetType().ToString() == "System.InvalidOperationException")
                {
                    try
                    {
                        session = ModuleFunctions.launchApp(Directory.GetCurrentDirectory() + "\\LaunchSocket.bat", Directory.GetCurrentDirectory());
                        Thread.Sleep(2000);
                    }
                    catch (Exception) { }
                    { }
                    desktopCapabilities = new DesiredCapabilities();
                    desktopCapabilities.SetCapability("platformName", "Windows");
                    desktopCapabilities.SetCapability("app", "Root");
                    desktopCapabilities.SetCapability("deviceName", "WindowsPC");
                    session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), desktopCapabilities);
                    applicationWindow = null;
                    openWindows = session.FindElementsByClassName("ConsoleWindowClass");
                    
                    foreach (var window in openWindows)
                    {
                        if (window.GetAttribute("Name").StartsWith("WinAppDriver"))
                        {
                        
                        }
                        else
                        {
                            applicationWindow = window;
                            break;
                        }
                    }

                    topLevelWindowHandle = applicationWindow.GetAttribute("NativeWindowHandle");
                    topLevelWindowHandle = int.Parse(topLevelWindowHandle).ToString("X");
                    capabilities = new DesiredCapabilities();
                    capabilities.SetCapability("deviceName", "WindowsPC");
                    capabilities.SetCapability("appTopLevelWindow", topLevelWindowHandle);
                    session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), capabilities); Thread.Sleep(2000); try

                    {
                        session.Keyboard.SendKeys("3");
                        session.Keyboard.SendKeys(Keys.Enter);
                        session.Keyboard.SendKeys("B"); 
                        session.Keyboard.SendKeys(Keys.Enter);
                        Thread.Sleep(3000);
                    }
                    catch (Exception) { }
                }
            }
        }

        /**  SocketBox Killing  **/
        public static void socketkill(WindowsDriver<WindowsElement> session, ExtentTest test)

        {
            FunctionLibrary lib = new FunctionLibrary(); 
            DesiredCapabilities desktopCapabilities = new DesiredCapabilities();
            desktopCapabilities.SetCapability("platformName", "Windows");
            desktopCapabilities.SetCapability("app", "Root");
            desktopCapabilities.SetCapability("deviceName", "WindowsPC");
            session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), desktopCapabilities); WindowsElement applicationWindow = null;
            var openWindows = session.FindElementsByClassName("ConsoleWindowClass"); 
            
            foreach (var window in openWindows)
            {
                if (window.GetAttribute("Name").StartsWith("WinAppDriver"))
                { 
                
                }
                else
                {
                    applicationWindow = window;
                    break;
                }
            }


            var topLevelWindowHandle = applicationWindow.GetAttribute("NativeWindowHandle");
            topLevelWindowHandle = int.Parse(topLevelWindowHandle).ToString("X");
            DesiredCapabilities capabilities = new DesiredCapabilities();
            capabilities.SetCapability("deviceName", "WindowsPC");
            capabilities.SetCapability("appTopLevelWindow", topLevelWindowHandle);
            session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), capabilities);             
            Thread.Sleep(2000);             
            session.CloseApp();
        }


        /* Verifying ADL values in AlgoLabTest */

        public static void checkADLValue(WindowsDriver<WindowsElement> session, ExtentTest stepName, string device, string DeviceNo,string DeviceType)
        {
            string jsonString = File.ReadAllText(configsettingpath);
            Dictionary<string, Dictionary<string, string>> algo = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(jsonString);
            FunctionLibrary lib = new FunctionLibrary();

            foreach (var algoPath in algo["Algo"])
            {
                if (device == algoPath.Key)
                {
                    Console.WriteLine($"{algoPath.Value}");
                    foreach (var workingDirectory in algo["WorkingDirectory"])
                    {
                        if (device == workingDirectory.Key)
                        {
                            Console.WriteLine($"{workingDirectory.Value}");
                            session = ModuleFunctions.sessionInitialize(algoPath.Value, workingDirectory.Value);

                            //if (device.Contains("RT") || device.Contains("RU") || device.Contains("NX"))
                            if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
                            {
                                ModuleFunctions.socketA(session, test, DeviceType);
                                Thread.Sleep(2000);
                                string computer_name = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
                                //if (device.Contains("RT"))
                                //{
                                //    session = ModuleFunctions.sessionInitialize(config.algo.Dooku2, config.workingdirectory.Dooku2);
                                //}
                                //else if(device.Contains("RU"))
                                //{
                                //    session = ModuleFunctions.sessionInitialize(config.algo.Dooku3, config.workingdirectory.Dooku3);

                                //}
                                //else if (device.Contains("NX"))
                                //{
                                //    session = ModuleFunctions.sessionInitialize(config.algo.Megnesium, config.workingdirectory.Megnesium);
                                //}

                                //session = ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\ReSound\\Dooku2.9.78.1\\AlgoLabtest.Dooku", "C:\\Program Files (x86)\\ReSound\\Dooku2.9.78.1");               
                                Actions actions = new Actions(session);
                                stepName.Log(Status.Pass, "Algo test lab is launched successfully.");
                                Thread.Sleep(2000);
                                session.FindElementByName("ADL").Click();
                                stepName.Log(Status.Pass, "Moved to ADL page successfully.");
                                Thread.Sleep(5000);
                                session.FindElementByAccessibilityId("FINDICON").Click();
                                Thread.Sleep(2000);
                                session.FindElementByAccessibilityId("FINDICON").Click();
                                Thread.Sleep(15000);
                                var SN = session.FindElementsByClassName("DataGrid");
                                Thread.Sleep(10000);
                                var SO = SN[1].FindElementsByClassName("TextBlock");

                                foreach (WindowsElement value in SO)
                                {
                                    string S = value.Text;
                                    if (S.Contains(DeviceNo))
                                    {
                                        value.Click();
                                    }

                                }

                                lib.functionWaitForName(session, "Connect");

                                try
                                {
                                    lib.functionWaitForName(session, "Dooku2.C6.TDI.9.78.0.0");
                                    Thread.Sleep(2000);
                                    lib.functionWaitForName(session, "Use when connecting next time");
                                    Thread.Sleep(2000);
                                    lib.functionWaitForName(session, "Connect");
                                }

                                catch (Exception)
                                { }
                                session = lib.waitUntilElementExists(session, "Gatt database detected - save of presets will be disabled until presets are read", 0);
                            }
                        }
                    }
                }
            }
            //if (device.Contains("LT"))
            //{

            //    session = ModuleFunctions.sessionInitialize(config.algo.Palpatine6, config.workingdirectory.Palpatine6);
            //    //session = ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\ReSound\\Palpatine6.7.4.21-RP-S\\AlgoLabtest.Palpatine.exe", "C:\\Program Files (x86)\\ReSound\\Palpatine6.7.4.21-RP-S");
            //}
            
            //else if(device.Contains("RE"))
            //{

            //    session = ModuleFunctions.sessionInitialize(config.algo.Dooku1, config.workingdirectory.Dooku1);
            //    //session = ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\ReSound\\Dooku1.1.20.1\\AlgoLabtest.Dooku.exe", "C:\\Program Files (x86)\\ReSound\\Dooku1.1.20.1");

            //}
            stepName.Log(Status.Pass, "Algo test lab is launched successfully.");
            Thread.Sleep(2000);
            session.FindElementByName("ADL").Click();
            stepName.Log(Status.Pass, "Moved to ADL page successfully.");
            
            /*Select Speedlink from the interface selection pop up*/

            try
            {
                if (session.FindElementByName("Activated device Hipro").Enabled)
                {
                    lib.clickOnElementWithIdonly(session, "BUTTON");
                    var interfaceButton = session.FindElementsByClassName("Image");

                    Console.WriteLine(interfaceButton.ToString());
                    int counter = 0;
                    foreach (var item in interfaceButton)
                    {
                        Console.WriteLine("Indexvalue is" + counter + ":" + item.GetAttribute("HelpText"));
                        counter = counter + 1;                     
                        stepName.Log(Status.Info, "Interfaces Available for selection: " + item.GetAttribute("HelpText"));
                    }
                    Console.WriteLine(interfaceButton.ToString());
                    Thread.Sleep(2000);
                    interfaceButton[12].Click();
                    stepName.Log(Status.Info, "Speedlink is selected");
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                stepName.Log(Status.Pass, "Selected Interface is Speedlink");
            }

            /*Getting all button from ADL screen*/

            var button = session.FindElementsByClassName("Button");
            int Counter = 0;
            foreach (var item in button)
            {
                string btnName = item.GetAttribute("Value");
                Counter = Counter + 1;
                Console.WriteLine(Counter.ToString());
            }

            if (device.Contains("LT"))
            {
                button[29].Click();
            }
            else if(device.Contains("RE"))
            {
                lib.clickOnElementWithIdonly(session, "ConnectLeftRightBothUserControlLeftButton");

                try
                {                   
                    session = lib.waitUntilElementExists(session, "Gatt database detected - save of presets will be disabled until presets are read", 0);

                    if (computer_name.Equals("FSWIRAY80"))

                    {
                        session.Keyboard.PressKey(Keys.Enter);

                    }
                }
                catch (Exception e)
                {

                }
            }

            session = lib.waitUntilElementExists(session, "Gatt database detected - save of presets will be disabled until presets are read", 0);

            try
            {
                session.FindElementByName("OK").Click();
            }
            catch (Exception)
            {

            }

            try
            {
                if ((session.FindElementByAccessibilityId("textBox1_5").Text.ToString()) == "1.000")
                {
                    Console.WriteLine("Saved Value is :" + session.FindElementByAccessibilityId("textBox1_5").Text.ToString());
                    stepName.Log(Status.Fail, "Saved Value is :" + session.FindElementByAccessibilityId("textBox1_5").Text.ToString());
                    Assert.Pass();
                    session.CloseApp();
                }
                else
                {
                    stepName.Log(Status.Pass, "Saved Value is :" + session.FindElementByAccessibilityId("textBox1_5").Text.ToString());
                    session.CloseApp();
                }
            }
            catch (Exception e)
            {
                session.CloseApp();
            }          
        }//End of Verify ADL Value



        /*This is to take the dump the device image from storage layout*/

        public static void processKill(string name)
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

        
        public static void takeDeviceDumpImage(WindowsDriver<WindowsElement> session, ExtentTest stepName, string device, String fileName, String side, string DeviceNo, string DeviceType)
        {

            Console.WriteLine("test");
            Thread.Sleep(5000);
            string computer_name = System.Environment.GetEnvironmentVariable("COMPUTERNAME");

            FunctionLibrary lib = new FunctionLibrary();

            string jsonString = File.ReadAllText(configsettingpath);

            Dictionary<string, Dictionary<string, string>> slv = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(jsonString);
            //if (device.Contains("RT") || device.Contains("RU") || device.Contains("NX"))
            foreach (var slvPath in slv["SLV"])
            {
                if (device == slvPath.Key)
                {
                    Console.WriteLine($"{slvPath.Value}");
                    foreach (var workingDirectory in slv["WorkingDirectory"])
                    {
                        if (device == workingDirectory.Key)
                        {
                            Thread.Sleep(3000);
                            //Console.WriteLine($"{workingDirectory.Value}");
                            session=sessionInitialize(slvPath.Value, workingDirectory.Value);

                            /** To Connect the device( RT or RU) to Stroragelayout viewr **/

                            //if (device.Contains("RT") || device.Contains("RU") || device.Contains("NX"))
                            if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
                            {
                              
                                Actions actions = new Actions(session);

                                var wait = new WebDriverWait(session, TimeSpan.FromSeconds(10));

                                Thread.Sleep(5000);


                                var cancelButton = wait.Until(ExpectedConditions.ElementToBeClickable(session.FindElementByAccessibilityId("FINDICON")));

                                cancelButton.Click();

                                //wait = new WebDriverWait(session, TimeSpan.FromSeconds(10));

                                Thread.Sleep(10000);

                                var DetectButton = wait.Until(ExpectedConditions.ElementToBeClickable(session.FindElementByAccessibilityId("FINDICON")));

                                DetectButton.Click();


                                //wait = new WebDriverWait(session, TimeSpan.FromSeconds(10));
                                var Popup = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("Popup")));

                                int height = Popup.Size.Height;


                                var drag = Popup.FindElements(By.ClassName("Thumb"));

                                if (drag.Count >= 7)
                                {
                                    actions.MoveToElement(drag[6]).Perform();
                                    actions.ClickAndHold(drag[6]).MoveByOffset(0, height * 3).Release().Perform();

                                }
                              
                                Thread.Sleep(15000);
                             
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
                                           // element.Click();
                                            //break;
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
                                        
                                        try
                                        {
                                            processKill("StorageLayoutViewer.exe");
                                        }

                                        catch (Exception e) { Console.WriteLine(e.Message); }

                                            if (side.Equals("Left"))
                                            {
                                                Thread.Sleep(4000);

                                                //if (device.Contains("RT") || device.Contains("NX") || device.Contains("C"))
                                                if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
                                                {

                                                    socketA(session, test, DeviceType);
                                            }
                                            else if (side.Equals("Right"))
                                            {
                                                socketB(session, test, DeviceType);
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
                                                        session = sessionInitialize(slvLaunch.Value, slvWorkingDirectory.Value);

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
                                ext = session.FindElements(WorkFlowPageFactory.fileMenu);
                                ext[0].Click();
                                Thread.Sleep(2000);
                                ext = session.FindElements(WorkFlowPageFactory.dumpHI);
                                Actions act = new Actions(session);
                                act.MoveToElement(ext[0]).Build().Perform();
                                Thread.Sleep(2000);
                                session.Keyboard.PressKey(Keys.Enter);
                                Thread.Sleep(4000);
                                session.FindElementByClassName("Edit").SendKeys("C:\\" + fileName + ".xml");
                                Thread.Sleep(4000);

                                /** To save the Dump in Xml file **/

                                session.FindElementByName("Save").Click();
                                Thread.Sleep(4000);
                                session.SwitchTo().Window(session.WindowHandles.First());
                                session.SwitchTo().ActiveElement();
                                WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(80));
                                Thread.Sleep(8000);
                                session.SwitchTo().Window(session.WindowHandles.First());
                                Thread.Sleep(30000);

                                try
                                {
                                    if (session.WindowHandles.Count() > 0)
                                    {
                                        session.SwitchTo().Window(session.WindowHandles.First());
                                        session.SwitchTo().ActiveElement();

                                        session.FindElementByAccessibilityId("checkBoxIgnoreAll").Click();
                                        Thread.Sleep(2000);
                                        session.FindElementByAccessibilityId("buttonOk").Click();
                                    }
                                }
                                catch (Exception e)
                                {
                                    if (e.GetType().ToString() == "System.InvalidOperationException")
                                    {
                                        var simu = new InputSimulator();
                                        simu.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_T);
                                        Thread.Sleep(2000);
                                        simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                        Thread.Sleep(2000);
                                        simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                        Thread.Sleep(2000); simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                        Thread.Sleep(2000); simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                        Thread.Sleep(2000); simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                        Thread.Sleep(2000);
                                        Thread.Sleep(2000);
                                        simu.Keyboard.KeyPress(VirtualKeyCode.UP);
                                        Thread.Sleep(2000); simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                        Thread.Sleep(2000); simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                        Thread.Sleep(2000); simu.Keyboard.KeyPress(VirtualKeyCode.RETURN);
                                        Thread.Sleep(2000); session.SwitchTo().Window(session.WindowHandles.First());
                                        session.SwitchTo().ActiveElement(); Thread.Sleep(2000);
                                        session.FindElementByAccessibilityId("checkBoxIgnoreAll").Click();
                                        Thread.Sleep(2000);
                                        session.FindElementByAccessibilityId("buttonOk").Click();
                                    }
                                }
                                Thread.Sleep(200000);

                                session.SwitchTo().Window(session.WindowHandles.First());
                                session.SwitchTo().ActiveElement();

                                try
                                {
                                    if (session.WindowHandles.Count() > 1)

                                    {
                                        session.SwitchTo().Window(session.WindowHandles.First());
                                        session.SwitchTo().ActiveElement();
                                        session.FindElementByAccessibilityId("buttonOk").Click();
                                    }
                                }
                                catch (Exception e)
                                {
                                    if (e.GetType().ToString() == "System.InvalidOperationException")
                                    {
                                        var simu = new InputSimulator();
                                        simu.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_T);
                                        Thread.Sleep(2000);
                                        simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                        Thread.Sleep(2000);
                                        simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                        Thread.Sleep(2000); simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                        Thread.Sleep(2000); simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                        Thread.Sleep(2000); simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                        Thread.Sleep(2000);
                                        Thread.Sleep(2000);
                                        simu.Keyboard.KeyPress(VirtualKeyCode.UP);
                                        Thread.Sleep(2000); simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                        Thread.Sleep(2000); simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                        Thread.Sleep(2000); simu.Keyboard.KeyPress(VirtualKeyCode.RETURN);
                                        Thread.Sleep(2000);
                                        session.SwitchTo().Window(session.WindowHandles.First());
                                        session.SwitchTo().ActiveElement();
                                        Thread.Sleep(2000);
                                        session.FindElementByAccessibilityId("buttonOk").Click();
                                    }
                                }
                                session.SwitchTo().Window(session.WindowHandles.First());
                                Thread.Sleep(2000);
                                session.FindElementByName("OK").Click();
                                session.SwitchTo().Window(session.WindowHandles.First());
                                Thread.Sleep(2000);
                                session.CloseApp();
                                Thread.Sleep(4000);
                            }

                            /** To Connect the device(LT) to Stroragelayout viewr **/

                            // if (device.Contains("LT"))
                            if (DeviceType.Equals("Wired") || DeviceType.Equals("D1rechargeableWired"))
                            {


                                try
                                {

                                    if (session.FindElementByName("Channel").Displayed == true)
                                    {
                                        //session = ModuleFunctions.sessionInitialize(config.slv.Palpatine6, config.workingdirectory.Palpatine6);

                                        //session = ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\ReSound\\Palpatine6.7.4.21-RP-S\\StorageLayoutViewer.exe", "C:\\Program Files (x86)\\ReSound\\Palpatine6.7.4.21-RP-S");
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
                                                action.MoveToElement(ext[0]).Build().Perform();
                                                Thread.Sleep(2000);
                                                session.Keyboard.PressKey(Keys.Enter);
                                                Thread.Sleep(2000);
                                                session.Keyboard.PressKey(Keys.ArrowDown);
                                                Thread.Sleep(2000);
                                                session.Keyboard.PressKey(Keys.ArrowDown);
                                                Thread.Sleep(2000);
                                                session.Keyboard.PressKey(Keys.Enter);
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
                                                session.Keyboard.PressKey(Keys.ArrowDown);
                                                Thread.Sleep(2000);
                                                session.Keyboard.PressKey(Keys.Enter);
                                                Thread.Sleep(2000);
                                            }
                                        }

                                        session.Keyboard.PressKey(Keys.Enter);
                                        stepName.Log(Status.Pass, side + ": is selected");

                                        /** selecting file menu and read **/

                                        ext = session.FindElements(WorkFlowPageFactory.fileMenu);
                                        ext[0].Click();

                                        /** selecting read option **/

                                        ext = session.FindElements(WorkFlowPageFactory.readHI);
                                        action = new Actions(session);
                                        action.MoveToElement(ext[0]).Build().Perform();
                                        Thread.Sleep(2000);
                                        session.Keyboard.PressKey(Keys.Enter);
                                        Thread.Sleep(4000);

                                        /** selecting file menu and CheckNodes **/

                                        ext = session.FindElements(WorkFlowPageFactory.fileMenu);
                                        ext[0].Click();
                                        Thread.Sleep(2000);
                                        ext = session.FindElements(WorkFlowPageFactory.checkNodes);
                                        action = new Actions(session);
                                        action.MoveToElement(ext[0]).Build().Perform();
                                        Thread.Sleep(2000);
                                        session.Keyboard.PressKey(Keys.Enter);
                                        Thread.Sleep(4000);

                                        /** selecting dump option **/

                                        ext = session.FindElements(WorkFlowPageFactory.fileMenu);
                                        ext[0].Click();
                                        Thread.Sleep(2000);
                                        ext = session.FindElements(WorkFlowPageFactory.dumpP6HI);
                                        action = new Actions(session);
                                        action.MoveToElement(ext[0]).Build().Perform();
                                        Thread.Sleep(2000);
                                        session.Keyboard.PressKey(Keys.Enter);
                                        Thread.Sleep(4000);
                                        session.FindElementByClassName("Edit").SendKeys("C:\\" + fileName + ".xml");
                                        Thread.Sleep(4000);

                                        /** To save the Dump in Xml file **/

                                        session.FindElementByName("Save").Click();
                                        Thread.Sleep(4000);
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
                                            Thread.Sleep(10000);
                                        }
                                    }

                                    /** To Connect the device(RE) to Stroragelayout viewr **/
                                }
                                catch (Exception e)
                                {
                                    InputSimulator sim = new InputSimulator();
                                    string storageLayOutDate = "WindowsForms10.Window.8.app.0.2804c64_r9_ad1";

                                    //session = ModuleFunctions.sessionInitialize(config.slv.Dooku1, config.workingdirectory.Dooku1);
                                    //session = ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\ReSound\\Dooku1.1.20.1\\StorageLayoutViewer.exe", "C:\\Program Files (x86)\\ReSound\\Dooku1.1.20.1");
                                    Thread.Sleep(8000);
                                    //Actions actions = new Actions(session);
                                    Thread.Sleep(4000);

                                    if (side.Equals("Left"))
                                    {
                                        var left = session.FindElementByAccessibilityId("elementHost1");
                                        ReadOnlyCollection<AppiumWebElement> selection = (ReadOnlyCollection<AppiumWebElement>)
                                        left.FindElementsByClassName("Button");
                                        selection[6].Click();
                                    }
                                    else
                                    {
                                        var right = session.FindElementByAccessibilityId("elementHost1");
                                        ReadOnlyCollection<AppiumWebElement> selection = (ReadOnlyCollection<AppiumWebElement>)
                                        right.FindElementsByClassName("Button");
                                        selection[5].Click();
                                    }

                                    lib.waitUntilElementExists(session, "File", 0);
                                    Thread.Sleep(4000);
                                    var ext = session.FindElements(WorkFlowPageFactory.fileMenu);
                                    ext[0].Click();
                                    Thread.Sleep(2000);
                                    ext = session.FindElements(WorkFlowPageFactory.readHI);
                                    Actions actions = new Actions(session);
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
                                    ext = session.FindElements(WorkFlowPageFactory.fileMenu);
                                    ext[0].Click();
                                    Thread.Sleep(2000);
                                    ext = session.FindElements(WorkFlowPageFactory.dumpHI);
                                    actions = new Actions(session);
                                    actions.MoveToElement(ext[0]).Build().Perform();
                                    Thread.Sleep(2000);
                                    session.Keyboard.PressKey(Keys.Enter);
                                    Thread.Sleep(4000);
                                    session.FindElementByClassName("Edit").SendKeys("C:\\" + fileName + ".xml");
                                    Thread.Sleep(4000);

                                    /** To save the Dump in Xml file **/

                                    session.FindElementByName("Save").Click();
                                    Thread.Sleep(4000);
                                    session.SwitchTo().Window(session.WindowHandles.First());
                                    session.SwitchTo().ActiveElement();
                                    WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(80));
                                    Thread.Sleep(8000);
                                    session.SwitchTo().Window(session.WindowHandles.First());
                                    Thread.Sleep(2000);

                                    /** To check the "Ignore checkbox" in the flow of Dump saving **/

                                    try
                                    {
                                        do
                                        {
                                            if (session.WindowHandles.Count() > 0)
                                            {
                                                session.SwitchTo().Window(session.WindowHandles.First());
                                                session.SwitchTo().ActiveElement();
                                                session.FindElementByAccessibilityId("checkBoxIgnoreAll").Click();
                                                Thread.Sleep(2000);
                                                session.FindElementByAccessibilityId("buttonOk").Click();

                                            }
                                        } while (session.FindElementByAccessibilityId("WorkerDialog").Enabled);
                                    }
                                    catch (Exception ex) { }

                                    Thread.Sleep(10000);

                                    /**This is to handle child windows whlie saving CDI**/

                                    session.SwitchTo().Window(session.WindowHandles.First());
                                    session.SwitchTo().ActiveElement();

                                    /** To click the Ok button in the flow of Dump saving **/

                                    try
                                    {

                                        do
                                        {

                                            if (session.WindowHandles.Count() > 1)

                                            {
                                                session.SwitchTo().Window(session.WindowHandles.First());
                                                session.SwitchTo().ActiveElement();
                                                session.FindElementByAccessibilityId("buttonOk").Click();
                                            }


                                        } while (session.FindElementByAccessibilityId("WorkerDialog").Enabled);
                                    }

                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.ToString());
                                    }

                                }
                                try
                                {
                                    session.SwitchTo().Window(session.WindowHandles.First());

                                }
                                catch (Exception e) { Console.WriteLine(e.Message); }

                                Thread.Sleep(2000);

                                try
                                {
                                    session.CloseApp();

                                }
                                catch (Exception ex) { Console.WriteLine(ex.Message); }


                                Thread.Sleep(4000);
                            }



                        }
                   
                    }
                
                }
            
            } 
            
            
            
        }
        /** Take Device Dump Image function **/


       /** This is to initiate extent report **/
        
        public static ExtentReports callbyextentreport(ExtentReports extent2)
        {
            extent = extent2;
            return extent;
        }

      
        /*This is to initiate config variable*/

        public static appconfigsettings callbyAlgoTestLabVariables(appconfigsettings config1)
        {
            config = config1;
            return config;
                                                                                                                                                             }

        /** This is to modify the values in Miniidentification
         * and Production test data in storagelayout to get 
         * display cloud Icon in S&R Tool Under Device Info **/


        public static WindowsDriver<WindowsElement> storagelayoutD1(WindowsDriver<WindowsElement> session, ExtentTest stepName, string deivce, string side)

        {
            //test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());
            //ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
            FunctionLibrary lib = new FunctionLibrary();
            InputSimulator sim = new InputSimulator();
            string computer_name = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
            string storageLayOutDate = "WindowsForms10.Window.8.app.0.2804c64_r9_ad1";

           // session = ModuleFunctions.sessionInitialize(config.slv.Dooku1, config.workingdirectory.Dooku1);
            //session = ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\ReSound\\Dooku1.1.20.1\\StorageLayoutViewer.exe", "C:\\Program Files (x86)\\ReSound\\Dooku1.1.20.1");
            Thread.Sleep(8000);
            Actions actions = new Actions(session);

            /** Interface selection drop down **/

            session.FindElementByAccessibilityId("ToggleButton").Click();
            Thread.Sleep(2000);

            /** Spped link selection **/

            var speeedlink = session.FindElementByAccessibilityId("SpeedLink:0");
            speeedlink.Click();
            Thread.Sleep(2000);

            /** If connected device is Left**/

            if (side.Equals("Left"))
            {
                var left = session.FindElementByAccessibilityId("elementHost1");
                ReadOnlyCollection<AppiumWebElement> selection = (ReadOnlyCollection<AppiumWebElement>)
                left.FindElementsByClassName("Button");
                selection[6].Click();
            }

            else
            {
                var right = session.FindElementByAccessibilityId("elementHost1");
                ReadOnlyCollection<AppiumWebElement> selection = (ReadOnlyCollection<AppiumWebElement>)
                right.FindElementsByClassName("Button");
                selection[5].Click();
            }

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
            Thread.Sleep(2000);
            var txt = session.FindElementsByName("0f8e00:0004a ProductionTestData");
            stepName.Log(Status.Pass, "0f8e00:0004a ProductionTestData " + "is selected");

            foreach (var item in txt)
            {
                Console.WriteLine(item.GetAttribute("Name"));
                item.Click();
                stepName.Log(Status.Pass, "0f8e00:0004a ProductionTestData " + "is selected");

            }

            Thread.Sleep(2000);
            var data = session.FindElementByName("DataGridView");
            data.Click();
            var row = session.FindElementByName("Value  -   from SpeedLink:0/Left Row 0");
            row.Click();

            /** To change the Date and Time in product test data **/

            row.SendKeys("2022-08-01 12:45:54Z");
            var miniidentification = session.FindElementByName("_Write to");
            miniidentification.Click();
            Thread.Sleep(3000);
            var min = session.FindElementByName("057000:00026 MiniIdentification");
            min.Click();
            Thread.Sleep(2000);
            data = session.FindElementByName("DataGridView");
            data.Click();
            row = session.FindElementByName("Row 6");
            row.Click();

            /** Enter the Unix time stamp id in modification tab **/

            row.SendKeys("1652118942");
            Thread.Sleep(2000);
            min = session.FindElementByName("_Write to");
            min.Click();
            Thread.Sleep(3000);

            
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

            return session;
        
        }



        /* This is to perform FDST flashing even 
         * if device not discover through airlink, 
         * it tries again to get disover untill device get
         * detects to FDTS */

        public static void discoveryFailed(WindowsDriver<WindowsElement> session, ExtentTest test, string textDir, string device, string side, string DeviceNo, string DeviceType)

        {
            // test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
            do
                {
                //Console.WriteLine("Window name is" +session.FindElementByClassName("WindowsForms10.STATIC.app.0.27a2811_r7_ad1").Text);

                if (device.Contains("RT") || device.Contains("RU") || device.Contains("NX"))

                {
                    session.FindElementByName("Stop").Click();
                    Thread.Sleep(3000);
                    session.SwitchTo().Window(session.WindowHandles[0]);
                    session.FindElementByName("Shutdown").Click();
                    Thread.Sleep(3000);


                   
                        // if (device.Contains("RT") || device.Contains("RU") || device.Contains("NX"))
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
                }

                /** launching the FDTS **/

                try
                        {
                            session = launchApp(textDir + "\\LaunchFDTS.bat", textDir);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }

                       // test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());

                        session = ModuleFunctions.sessionInitialize1("C:\\Program Files (x86)\\GN Hearing\\Camelot\\WorkflowRuntime\\Camelot.WorkflowRuntime.exe", "C:\\Program Files (x86)\\GN Hearing\\Camelot\\WorkflowRuntime");
                        string ApplicationPath = "C:\\Program Files (x86)\\GN Hearing\\Camelot\\WorkflowRuntime\\Camelot.WorkflowRuntime.exe";
                        Thread.Sleep(2000);
                        DesiredCapabilities appCapabilities = new DesiredCapabilities();
                        appCapabilities.SetCapability("app", ApplicationPath);
                        appCapabilities.SetCapability("deviceName", "WindowsPC");
                        session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                        Thread.Sleep(8000);
                        session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);

                        stepName.Log(Status.Pass, "Test Work Flow launched successfully");


                     /** To delete camlotlog files If it is exists in the alocated path **/

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
                        var prdName = session.FindElements(WorkFlowPageFactory.workFlowProductSelection);
                        var Name = prdName[0].FindElementByXPath("*/*");
                        var txt = Name.GetAttribute("Name");
                        Name.Click();
                        Actions action = new Actions(session);

                        if (device.Contains("RT") || device.Contains("RU") || device.Contains("NX"))
                        {
                            action.MoveToElement(Name).Click().DoubleClick().Build().Perform();
                            if (device.Contains("RT961-DRWC"))
                            {
                                session.FindElementByName(devName + " [9] (Final)").Click();
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

                            /** Entering the Serial number **/

                            session.FindElementByAccessibilityId("textBoxSN").SendKeys(DeviceNo);
                            session.FindElementByName("Continue >>").Click();
                        }  
                    Thread.Sleep(30000);
                    session.SwitchTo().Window(session.WindowHandles.First());
                    session.SwitchTo().ActiveElement();
                } while (session.FindElementByAccessibilityId("MessageForm").Displayed);
            
        }


        public static void Recovery(WindowsDriver<WindowsElement> session, ExtentTest stepname, string DeviceType, string DeviceNo,string side)
        {
            FunctionLibrary lib = new FunctionLibrary();


            try
            {
                if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
                {
                    if (side.Equals("Left"))
                    {
                        socketA(session, stepname, DeviceType);
                    }
                    else if (side.Equals("Right"))
                    {
                        socketB(session, stepname, DeviceType);
                    }
                }
            }
            catch { }

            try
            {
                session = launchApp(Directory.GetCurrentDirectory() + "\\LaunchSandR.bat", Directory.GetCurrentDirectory());

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            //Thread.Sleep(5000);

            session = sessionInitialize(config.ApplicationPath.SandRAppPath, config.workingdirectory.SandR);
            stepname.Log(Status.Pass, "S&R Tool launched successfully");
            lib.waitUntilElementExists(session, "Device Info", 0);
            session.FindElementByName("Device Info").Click();
            //Thread.Sleep(2000);

            if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
            {
                session.FindElementByName("Discover").Click();
                stepname.Log(Status.Pass, "Clicked on Discover.");
                session.SwitchTo().Window(session.WindowHandles.First());
                session.SwitchTo().ActiveElement();


                try
                {
                    session.FindElementByAccessibilityId("SerialNumberTextBox").SendKeys(DeviceNo);
                    lib.functionWaitForName(session, "Search");
                }

                catch (Exception ex)
                {

                }        

            }

        }

    } /**End of ModuleFunctions*/ 
    
} //End of name space




