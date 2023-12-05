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


        /** Application launchhing **/
        public static WindowsDriver<WindowsElement> sessionInitialize(string name, string path)
        {
            string ApplicationPath = name;
            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", ApplicationPath);
            appCapabilities.SetCapability("platformName", "Windows");
            appCapabilities.SetCapability("deviceName", "WindowsPC");
            appCapabilities.SetCapability("appWorkingDir", path);
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
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            Thread.Sleep(4000);
            return session;
        }


        /** Continue buttons click operation in fsw Connection flow **/
        
        public static WindowsDriver<WindowsElement> getControlsOfParentWindow(WindowsDriver<WindowsElement> session, string name, ExtentTest test)
        {
            var childTable = session.FindElementsByXPath("//*[@ClassName='" + name + "']//Text[@ClassName='TextBlock']");
            int counter = 0;
            foreach (var child in childTable)
            {
                if (counter == 1)
                {
                    test.Log(Status.Pass, "Continue is clicked in the screen +" + child.GetAttribute("Name"));

                }

                counter = counter + 1;
            }

            return session;
        }


        /** To verify the Extent reports and Capture & restore reports in alocated location **/

        public static void verifyIfReportsExisted(ExtentTest test)
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
        } /*End of verifyIfReportExisted*/



        /* AlgoLabTest Alter Value */

        public static void altTestLab(WindowsDriver<WindowsElement> session, ExtentTest test, string device, string DeviceNo)
        {

            FunctionLibrary lib = new FunctionLibrary();

            if (device.Contains("RT") || device.Contains("RU"))

            {
                try
                {


                    ModuleFunctions.socketA(session, test, device);

                }
                catch { }
                Thread.Sleep(2000);

                string computer_name = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
                session = ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\ReSound\\Dooku2.9.78.1\\AlgoLabtest.Dooku", "C:\\Program Files (x86)\\ReSound\\Dooku2.9.78.1");           
                Actions actions = new Actions(session);
                test.Log(Status.Pass, "Algo test lab is launched successfully.");
                Thread.Sleep(2000);
                session.FindElementByName("ADL").Click();
                test.Log(Status.Pass, "Moved to ADL page successfully.");
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
                test.Log(Status.Pass, "Altered value is 1");
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
                        test.Log(Status.Pass, "Saved Value is :" + session.FindElementByAccessibilityId("textBox1_5").Text.ToString());
                        Assert.Pass();
                        session.CloseApp();
                    }
                    else
                    {
                        test.Log(Status.Pass, "Saved Value is :" + session.FindElementByAccessibilityId("textBox1_5").Text.ToString());

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


            if (device.Contains("LT") || device.Contains("RE"))
            {
                if (device.Contains("LT"))
                {
                    session = ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\ReSound\\Palpatine6.7.4.21-RP-S\\AlgoLabtest.Palpatine.exe", "C:\\Program Files (x86)\\ReSound\\Palpatine6.7.4.21-RP-S");
                }
                else
                {
                    session = ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\ReSound\\Dooku1.1.20.1\\AlgoLabtest.Dooku.exe", "C:\\Program Files (x86)\\ReSound\\Dooku1.1.20.1");

                }
                test.Log(Status.Pass, "Algo test lab is launched successfully.");
                Thread.Sleep(2000);
                session.FindElementByName("ADL").Click();
                test.Log(Status.Pass, "Moved to ADL page successfully.");

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
                            test.Log(Status.Info, "Interfaces Available for selection: " + item.GetAttribute("HelpText"));
                        }
                        Console.WriteLine(interfaceButton.ToString());
                        Thread.Sleep(2000);
                        interfaceButton[12].Click();
                        test.Log(Status.Info, "Speedlink is selected");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    test.Log(Status.Pass, "Selected Interface is Speedlink");
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
                catch(Exception)
                {

                }

                lib.waitForElement(session, "Persist ADL to device when writing presets");
                lib.clickOnElementWithIdonly(session, "textBox1_5");
                Thread.Sleep(2000);
                session.FindElementByAccessibilityId("textBox1_5").Clear();
                Thread.Sleep(2000);
                session.FindElementByAccessibilityId("textBox1_5").SendKeys("1");
                test.Log(Status.Pass, "Altered value is 1");

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
                catch(Exception)
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
                catch(Exception)
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
                        test.Log(Status.Pass, "Saved Value is :" + session.FindElementByAccessibilityId("textBox1_5").Text.ToString());

                        Assert.Pass();
                        session.CloseApp();

                    }
                    else
                    {
                        test.Log(Status.Pass, "Saved Value is :" + session.FindElementByAccessibilityId("textBox1_5").Text.ToString());

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
        }



        /* Calling Socketbox and passing commands */

        public static void socket(WindowsDriver<WindowsElement> session, ExtentTest test, string device)

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
                if (device.Contains("RT962-DRW"))

                {
                    session.Keyboard.SendKeys("3");
                    session.Keyboard.SendKeys(Keys.Enter);
                    Thread.Sleep(3000);
                    session.Keyboard.SendKeys("A");
                    session.Keyboard.SendKeys(Keys.Enter);
                    Thread.Sleep(3000);
                }

                else if(device.Contains("RT") && device.Contains("C"))

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
                        if (device.Contains("RT"))

                        {
                            session.Keyboard.SendKeys("3");
                            session.Keyboard.SendKeys(Keys.Enter);
                            session.Keyboard.SendKeys("A");
                            session.Keyboard.SendKeys(Keys.Enter);
                        }
                        else if (device.Contains("RT") && device.Contains("C"))

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

        public static void socketA(WindowsDriver<WindowsElement> session, ExtentTest test, string device)

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

                if (device.Contains("RT962-DRW"))
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

                else if (device.Contains("RT") && device.Contains("C"))
                {
                    session.Keyboard.SendKeys("b");
                    Thread.Sleep(1000);
                    session.Keyboard.SendKeys(Keys.Enter);
                    Thread.Sleep(1000);
                    session.Keyboard.SendKeys("a");
                    Thread.Sleep(1000);
                    session.Keyboard.SendKeys(Keys.Enter);
                    Thread.Sleep(1000);
                    session.Keyboard.SendKeys("A");
                    Thread.Sleep(2000);
                    session.Keyboard.SendKeys(Keys.Enter);
                    Thread.Sleep(4000);
                    session.Keyboard.SendKeys("a");
                    Thread.Sleep(2000);
                    session.Keyboard.SendKeys(Keys.Enter);
                    Thread.Sleep(1000);
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

                        if (device.Contains("RT") && device.Contains("RU"))
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

                        else if (device.Contains("RT") && device.Contains("C"))
                        {
                            session.Keyboard.SendKeys("b");
                            Thread.Sleep(1000);
                            session.Keyboard.SendKeys(Keys.Enter);
                            Thread.Sleep(1000);
                            session.Keyboard.SendKeys("a");
                            Thread.Sleep(1000);
                            session.Keyboard.SendKeys(Keys.Enter);
                            Thread.Sleep(1000);
                            session.Keyboard.SendKeys("A");
                            Thread.Sleep(2000);
                            session.Keyboard.SendKeys(Keys.Enter);
                            Thread.Sleep(4000);
                            session.Keyboard.SendKeys("a");
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


        public static void socketB(WindowsDriver<WindowsElement> session, ExtentTest test, string device)

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
                if (device.Contains("RT962-DRW"))
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

                else if(device.Contains("RT") && device.Contains("C"))
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
                        if (device.Contains("RT") && device.Contains("RU"))
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

                        else if (device.Contains("RT") && device.Contains("C"))
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

        public static void checkADLValue(WindowsDriver<WindowsElement> session, ExtentTest test, string device, string DeviceNo)
        {
            FunctionLibrary lib = new FunctionLibrary();

            if (device.Contains("RT") || device.Contains("RU"))
            {
                ModuleFunctions.socketA(session, test, device);
                Thread.Sleep(2000);
                string computer_name = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
                session = ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\ReSound\\Dooku2.9.78.1\\AlgoLabtest.Dooku", "C:\\Program Files (x86)\\ReSound\\Dooku2.9.78.1");               
                Actions actions = new Actions(session);
                test.Log(Status.Pass, "Algo test lab is launched successfully.");
                Thread.Sleep(2000);
                session.FindElementByName("ADL").Click();
                test.Log(Status.Pass, "Moved to ADL page successfully.");
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

            if (device.Contains("LT"))
            {
                session = ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\ReSound\\Palpatine6.7.4.21-RP-S\\AlgoLabtest.Palpatine.exe", "C:\\Program Files (x86)\\ReSound\\Palpatine6.7.4.21-RP-S");
            }
            
            else if(device.Contains("RE"))
            {
                session = ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\ReSound\\Dooku1.1.20.1\\AlgoLabtest.Dooku.exe", "C:\\Program Files (x86)\\ReSound\\Dooku1.1.20.1");

            }
            test.Log(Status.Pass, "Algo test lab is launched successfully.");
            Thread.Sleep(2000);
            session.FindElementByName("ADL").Click();
            test.Log(Status.Pass, "Moved to ADL page successfully.");
            
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
                        test.Log(Status.Info, "Interfaces Available for selection: " + item.GetAttribute("HelpText"));
                    }
                    Console.WriteLine(interfaceButton.ToString());
                    Thread.Sleep(2000);
                    interfaceButton[12].Click();
                    test.Log(Status.Info, "Speedlink is selected");
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                test.Log(Status.Pass, "Selected Interface is Speedlink");
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
                    test.Log(Status.Fail, "Saved Value is :" + session.FindElementByAccessibilityId("textBox1_5").Text.ToString());
                    Assert.Pass();
                    session.CloseApp();
                }
                else
                {
                    test.Log(Status.Pass, "Saved Value is :" + session.FindElementByAccessibilityId("textBox1_5").Text.ToString());
                    session.CloseApp();
                }
            }
            catch (Exception e)
            {
                session.CloseApp();
            }          
        }//End of Verify ADL Value



        /*This is to take the dump the device image from storage layout*/


        public static void takeDeviceDumpImage(WindowsDriver<WindowsElement> session, ExtentTest test, string device, String fileName, String side, string DeviceNo)

        {
            Console.WriteLine("test");

            /** To Connect the device( RT or RU) to Stroragelayout viewr **/

            if (device.Contains("RT") || device.Contains("RU"))
            {
                Thread.Sleep(5000);
                string computer_name = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
                session = ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\ReSound\\Dooku2.9.78.1\\StorageLayoutViewer.exe", "C:\\Program Files (x86)\\ReSound\\Dooku2.9.78.1");
                FunctionLibrary lib = new FunctionLibrary();
                Actions actions = new Actions(session);
                Thread.Sleep(5000);
                session.FindElementByAccessibilityId("FINDICON").Click();
                Thread.Sleep(8000);
                session.FindElementByAccessibilityId("FINDICON").Click();
                Thread.Sleep(8000);
                session.SwitchTo().Window(session.WindowHandles.First());
                session.SwitchTo().ActiveElement();

                try
                {

                    do
                    {
                        var non = session.FindElementByClassName("DataGrid");
                        var h = session.FindElementsByClassName("DataGridCell");
                        ReadOnlyCollection<AppiumWebElement> boxs = (ReadOnlyCollection<AppiumWebElement>)non.FindElementsByClassName("DataGridCell");

                        /** Identifying and selection of HI Serial Number **/


                        foreach (var element in boxs)
                        {
                            if (element.Text == DeviceNo)
                            {
                                element.Click();
                                break;
                            }
                        }
                    } while (session.FindElementByName("_Read from").Displayed);
                }
                catch                
                {
                    do
                    {
                        var non = session.FindElementByClassName("DataGrid");
                        var h = session.FindElementsByClassName("DataGridCell");
                        ReadOnlyCollection<AppiumWebElement> boxs = (ReadOnlyCollection<AppiumWebElement>)non.FindElementsByClassName("DataGridCell");

                        /** Identifying and selection of HI Serial Number **/


                        foreach (var element in boxs)
                        {
                            if (element.Text == DeviceNo)
                            {
                                element.Click();
                                break;
                            }
                        }
                    } while (session.FindElementByName("_Read from").Displayed);
                }                
                lib.functionWaitForName(session,"Connect");
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
                Thread.Sleep(150000);            
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
                        simu.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LWIN,VirtualKeyCode.VK_T);
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

            if (device.Contains("LT"))
            {
                string computer_name = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
                session = ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\ReSound\\Palpatine6.7.4.21-RP-S\\StorageLayoutViewer.exe", "C:\\Program Files (x86)\\ReSound\\Palpatine6.7.4.21-RP-S");
                FunctionLibrary lib = new FunctionLibrary();
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
                test.Log(Status.Pass, side + ": is selected"); 
                
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

            else if (device.Contains("RE"))

            {
                FunctionLibrary lib = new FunctionLibrary();
                InputSimulator sim = new InputSimulator();
                string computer_name = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
                string storageLayOutDate = "WindowsForms10.Window.8.app.0.2804c64_r9_ad1";
                session = ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\ReSound\\Dooku1.1.20.1\\StorageLayoutViewer.exe", "C:\\Program Files (x86)\\ReSound\\Dooku1.1.20.1");
                Thread.Sleep(8000);
                Actions actions = new Actions(session);           
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
                catch(Exception ex) { }

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
            catch(Exception e) { Console.WriteLine(e.Message); }
            
            Thread.Sleep(2000);
            
            try
            {
                session.CloseApp();

            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }
            Thread.Sleep(4000);

        }
        /** Take Device Dump Image function **/


       /** This is to initiate extent report **/
        
        public static ExtentReports callbyextentreport(ExtentReports extent2)
        {
            extent = extent2;
            return extent;
        }


        /** This is to modify the values in Miniidentification
         * and Production test data in storagelayout to get 
         * display cloud Icon in S&R Tool Under Device Info **/


        public static WindowsDriver<WindowsElement> storagelayoutD1(WindowsDriver<WindowsElement> session, ExtentTest test, string deivce, string side)

        {
            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());
            FunctionLibrary lib = new FunctionLibrary();
            InputSimulator sim = new InputSimulator();
            string computer_name = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
            string storageLayOutDate = "WindowsForms10.Window.8.app.0.2804c64_r9_ad1";
            session = ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\ReSound\\Dooku1.1.20.1\\StorageLayoutViewer.exe", "C:\\Program Files (x86)\\ReSound\\Dooku1.1.20.1");
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
            test.Log(Status.Pass, "0f8e00:0004a ProductionTestData " + "is selected");

            foreach (var item in txt)
            {
                Console.WriteLine(item.GetAttribute("Name"));
                item.Click();
                test.Log(Status.Pass, "0f8e00:0004a ProductionTestData " + "is selected");

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
                test.Pass("Writing Presets is done successfully.");
            }

            return session;
        
    }



        /* This is to perform FDST flashing even 
         * if device not discover through airlink, 
         * it tries again to get disover untill device get
         * detects to FDTS */


        public static WindowsDriver<WindowsElement> discoveryFailed(WindowsDriver<WindowsElement> session, ExtentTest test, string textDir, string device, string side, string DeviceNo)

        {
            test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());
                do
                {
                    Console.WriteLine("Window name is" +session.FindElementByClassName("WindowsForms10.STATIC.app.0.27a2811_r7_ad1").Text);

                        if (device.Contains("RT") || device.Contains("RU"))

                        {
                            session.FindElementByName("Stop").Click();
                            Thread.Sleep(3000);
                            session.SwitchTo().Window(session.WindowHandles[0]);
                            session.FindElementByName("Shutdown").Click();
                            Thread.Sleep(3000);


                            if (side.Equals("Left"))


                            {
                                ModuleFunctions.socketA(session, test, device);
                            }
                            else if (side.Equals("Right"))
                            {
                                ModuleFunctions.socketB(session, test, device);
                            }
                        }

                        /** launching the FDTS **/

                        try
                        {
                            session = ModuleFunctions.launchApp(textDir + "\\LaunchFDTS.bat", textDir);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }

                        test = extent.CreateTest(ScenarioStepContext.Current.StepInfo.Text.ToString());
                        session = ModuleFunctions.sessionInitialize1("C:\\Program Files (x86)\\GN Hearing\\Camelot\\WorkflowRuntime\\Camelot.WorkflowRuntime.exe", "C:\\Program Files (x86)\\GN Hearing\\Camelot\\WorkflowRuntime");
                        string ApplicationPath = "C:\\Program Files (x86)\\GN Hearing\\Camelot\\WorkflowRuntime\\Camelot.WorkflowRuntime.exe";
                        Thread.Sleep(2000);
                        DesiredCapabilities appCapabilities = new DesiredCapabilities();
                        appCapabilities.SetCapability("app", ApplicationPath);
                        appCapabilities.SetCapability("deviceName", "WindowsPC");
                        session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                        Thread.Sleep(8000);
                        session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);

                        test.Log(Status.Pass, "Test Work Flow launched successfully");


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

                        if (device.Contains("RT") || device.Contains("RU"))
                        {
                            action.MoveToElement(Name).Click().DoubleClick().Build().Perform();
                            if (device.Contains("RT961-DRWC"))
                            {
                                session.FindElementByName(devName + " [9] (Final)").Click();
                            }
                            else
                            {
                                session.FindElementByName(devName + " [9] (Final)").Click();
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
                } while (session.FindElementByClassName("WindowsForms10.STATIC.app.0.27a2811_r7_ad1").Text == "Discovery Failed");

            return session;
        }  

        } /**End of ModuleFunctions*/ 
    
    } //End of name space




