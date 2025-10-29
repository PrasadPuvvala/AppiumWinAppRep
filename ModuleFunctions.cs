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
using System.Xml.Linq;
using System.Xml;
using Microsoft.Win32;
using javax.tools;
using AppiumWinApp.StepDefinitions;
using com.sun.tools.javac.comp;
using java.awt.geom;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.Interactions;
using PointerInputDevice = OpenQA.Selenium.Appium.Interactions.PointerInputDevice;
using Reqnroll;

namespace AppiumWinApp
{
    internal class ModuleFunctions
    {
        protected static WindowsDriver<WindowsElement> session;
        private const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        public static ExtentReports extent;
        private static ExtentSparkReporter htmlReporter;
        private static ExtentTest test;
        public static ExtentReports extent1;
        public static string computer_name = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
        public static appconfigsettings config;
        static string configsettingpath = System.IO.Directory.GetParent(@"../../../").FullName
        + Path.DirectorySeparatorChar + "appconfig.json";
        public static string screenshot = string.Empty;
        
        private readonly ScenarioContext _scenarioContext;

        /** Application launchhing **/
        public static WindowsDriver<WindowsElement> sessionInitialize(string name, string path)
        {

            string ApplicationPath = name;

            AppiumOptions appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", ApplicationPath);
            appCapabilities.AddAdditionalCapability("platformName", "Windows");
            appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "30");
            appCapabilities.AddAdditionalCapability("appArguments", "--run-as-administrator");
            appCapabilities.AddAdditionalCapability("appArguments", "Test.exe");
            appCapabilities.AddAdditionalCapability("appWorkingDir", path);
            appCapabilities.AddAdditionalCapability("automationName", "Windows");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
            appCapabilities.AddAdditionalCapability("ms:experimental-webdriver", true);
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            Thread.Sleep(8000);
            //Thread.Sleep(5000);

            //session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            //Thread.Sleep(8000);
            session.Manage().Window.Maximize();
            return session;
        }

        /** FDTS Application launching **/
        public static WindowsDriver<WindowsElement> sessionInitialize1(string name, string path)
        {
            string ApplicationPath = name;
            //AppiumOptions appCapabilities = new AppiumOptions();
            //appCapabilities.AddAdditionalCapability("app", ApplicationPath);
            //appCapabilities.AddAdditionalCapability("platformName", "Windows");
            //appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
            //appCapabilities.AddAdditionalCapability("appWorkingDir", path);
            //appCapabilities.AddAdditionalCapability("appArguments", "--run-as-administrator");
            //appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "25");
            //appCapabilities.AddAdditionalCapability("appArguments", "Test.exe");
            ////appCapabilities.AddAdditionalCapability("ms:experimental-webdriver", true);
            ////appCapabilities.AddAdditionalCapability("noReset", true);
            ////appCapabilities.AddAdditionalCapability("newCommandTimeout", 600);
            AppiumOptions appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", ApplicationPath);
            appCapabilities.AddAdditionalCapability("platformName", "Windows");
            appCapabilities.AddAdditionalCapability("ms:waitForAppLaunch", "30");
            appCapabilities.AddAdditionalCapability("appArguments", "--run-as-administrator");
            //appCapabilities.AddAdditionalCapability("appPackage", "ReSound");
            appCapabilities.AddAdditionalCapability("appArguments", "Test.exe");
            appCapabilities.AddAdditionalCapability("appWorkingDir", path);

            //Thread.Sleep(8000);
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            //Thread.Sleep(8000);
            //session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            return session;
        }

        /**  FDTS Application launching from Bat files  **/
        public static WindowsDriver<WindowsElement> launchApp(string name, string dir)
        {
            string ApplicationPath = name;
            AppiumOptions appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", ApplicationPath);
            appCapabilities.AddAdditionalCapability("platformName", "Windows");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
            appCapabilities.AddAdditionalCapability("appWorkingDir", dir);
            appCapabilities.AddAdditionalCapability("appArguments", "--run-as-administrator");
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            Thread.Sleep(8000);
            return session;
        }

        public static WindowsDriver<WindowsElement> sessionInitializeWODirectory(string name)
        {
            string ApplicationPath = name;
            AppiumOptions appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", ApplicationPath);
            appCapabilities.AddAdditionalCapability("platformName", "Windows");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
            appCapabilities.AddAdditionalCapability("appArguments", "--run-as-administrator");
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            Thread.Sleep(4000);
            return session;
        }

        //public static string CaptureScreenshot(WindowsDriver<WindowsElement> windowsDriver)
        //{
        //    string screenshotDir = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");
        //    if (!Directory.Exists(screenshotDir))
        //        Directory.CreateDirectory(screenshotDir);

        //    string screenshotPath = Path.Combine(screenshotDir, $"screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png");
        //    Screenshot screenshot = ((ITakesScreenshot)windowsDriver).GetScreenshot();
        //    screenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);
        //    return screenshotPath;
        //}

        public static string CaptureScreenshot(WindowsDriver<WindowsElement> windowsDriver)
        {
            Screenshot screenshot = ((ITakesScreenshot)windowsDriver).GetScreenshot();
            return screenshot.AsBase64EncodedString;
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
            Thread.Sleep(3000);

            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());

            string dir = "C:\\CaptureBase\\Reports";
            string today = DateTime.Now.ToString("yyyy-MM-dd");

            // Get all PDF files in the directory for today's date
            string[] files = Directory.GetFiles(dir + "\\" + today);

            foreach (string file in files)
            {
                Console.WriteLine(Path.GetFileName(file));
                string filename = file;
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
                            // This is to capture all the label names and write them to the report
                            String[] seperator = { "Capture Report Capture specification" };
                            // using the method
                            String[] strlist = str.Split(seperator, StringSplitOptions.RemoveEmptyEntries);

                            foreach (String s in strlist)
                            {
                                Console.WriteLine(s);
                            }

                            if (strlist.Length >= 2)
                            {
                                String[] seperatorLables = { "\r\n" };
                                String[] lableNames = strlist[0].Split(seperatorLables, StringSplitOptions.RemoveEmptyEntries);

                                foreach (String s in lableNames)
                                {
                                    stepName.Log(Status.Pass, s + " is found.");
                                }

                                // This is to write label values in the report
                                String[] spearator1 = { " " };

                                String[] reportValues = strlist[1].Split(spearator1, StringSplitOptions.RemoveEmptyEntries);
                                foreach (String s in reportValues)
                                {
                                    stepName.Log(Status.Pass, s + " is found.");

                                }
                            }
                        }
                        File.Delete(filename);
                        break;

                    case "restoration report":

                        stepName.Log(Status.Pass, "!!!!****Restoration report generated****!!!!");
                        stepName.Log(Status.Pass, "File Name +" + filename);
                        // Extracting Image and Text content from Pdf Documents

                        // open a 128 bit encrypted PDF
                        pdf = PdfDocument.FromFile(filename, "password");
                        AllText = pdf.ExtractAllText();
                        AllImages = pdf.ExtractAllImages();
                        // Or even find the precise text and images for each page in the document
                        for (var index = 0; index < pdf.PageCount; index++)
                        {
                            int PageNumber = index + 1;
                            string Text = pdf.ExtractTextFromPage(index);
                            IEnumerable<System.Drawing.Image> Images = pdf.ExtractImagesFromPage(index);
                            // Taking a string
                            String str = Text;
                            String[] spearator = { "Restoration Report (original device or clone)" };
                            // using the method
                            String[] strlist = str.Split(spearator, StringSplitOptions.RemoveEmptyEntries);

                            foreach (String s in strlist)
                            {
                                Console.WriteLine(s);
                            }

                            if (strlist.Length >= 2)
                            {
                                String[] seperatorLables = { "\r\n" };
                                String[] lableNames = strlist[0].Split(seperatorLables, StringSplitOptions.RemoveEmptyEntries);
                                foreach (String s in lableNames)
                                {
                                    stepName.Log(Status.Pass, s + " is found.");
                                }
                                // This is to write label values in the report
                                String[] spearator1 = { " " };
                                String[] reportValues = strlist[1].Split(spearator1, StringSplitOptions.RemoveEmptyEntries);
                                foreach (String s in reportValues)
                                {
                                    stepName.Log(Status.Pass, s + " is found.");

                                }
                            }
                        }
                        File.Delete(filename);
                        break;
                }
            }

            //string dir = "C:\\CaptureBase\\Reports";
            //string filename = null;

            //string today = DateTime.Now.ToString("yyyy-MM-dd");

            //string[] files = Directory.GetFiles(dir + "\\" + today);

            //foreach (string file in files)
            //{
            //    Console.WriteLine(Path.GetFileName(file));
            //    filename = file;
            //    var splitVal = filename.Split("-");

            //    switch (splitVal[3])

            //    {
            //        case "capture report":

            //            stepName.Log(Status.Pass, "!!!!****Capture report generated****!!!!");
            //            stepName.Log(Status.Pass, "File Name :" + filename);
            //            var pdf = PdfDocument.FromFile(filename, "password");
            //            string AllText = pdf.ExtractAllText();
            //            IEnumerable<System.Drawing.Image> AllImages = pdf.ExtractAllImages();
            //            for (var index = 0; index < pdf.PageCount; index++)
            //            {
            //                int PageNumber = index + 1;
            //                string Text = pdf.ExtractTextFromPage(index);
            //                IEnumerable<System.Drawing.Image> Images = pdf.ExtractImagesFromPage(index);
            //                // Taking a string
            //                String str = Text;
            //                // This is to capture all the lable names and write them to report
            //                String[] seperator = { "Capture Report Capture specification" };
            //                // using the method
            //                String[] strlist = str.Split(seperator,
            //                   StringSplitOptions.RemoveEmptyEntries);

            //                foreach (String s in strlist)
            //                {
            //                    Console.WriteLine(s);
            //                }

            //                String[] seperatorLables = { "\r\n" };
            //                String[] lableNames = strlist[0].Split("\r\n");

            //                foreach (String s in lableNames)
            //                {
            //                    stepName.Log(Status.Pass, s + " is found.");
            //                }
            //                //This is to write lable values in the report
            //                String[] spearator1 = { " " };

            //                String[] reportValues = strlist[1].Split(spearator1,
            //                   StringSplitOptions.RemoveEmptyEntries);
            //                foreach (String s in reportValues)
            //                {
            //                    stepName.Log(Status.Pass, s + " is found.");

            //                }

            //            }
            //            File.Delete(filename);
            //            break;

            //        case "restoration report":

            //            stepName.Log(Status.Pass, "!!!!****Restoration report generated****!!!!");
            //            stepName.Log(Status.Pass, "File Name +" + filename);
            //            // Extracting Image and Text content from Pdf Documents

            //            // open a 128 bit encrypted PDF
            //            pdf = PdfDocument.FromFile(filename, "password");
            //            //Get all text to put in a search index
            //            AllText = pdf.ExtractAllText();
            //            //Get all Images
            //            AllImages = pdf.ExtractAllImages();
            //            //Or even find the precise text and images for each page in the document
            //            for (var index = 0; index < pdf.PageCount; index++)
            //            {
            //                int PageNumber = index + 1;
            //                string Text = pdf.ExtractTextFromPage(index);
            //                IEnumerable<System.Drawing.Image> Images = pdf.ExtractImagesFromPage(index);
            //                // Taking a string
            //                String str = Text;
            //                String[] spearator = { "Restoration Report (original device or clone)" };
            //                // using the method
            //                String[] strlist = str.Split(spearator,
            //                   StringSplitOptions.RemoveEmptyEntries);
            //                foreach (String s in strlist)
            //                {
            //                    Console.WriteLine(s);
            //                }
            //                String[] seperatorLables = { "\r\n" };
            //                String[] lableNames = strlist[0].Split("\r\n");
            //                foreach (String s in lableNames)
            //                {
            //                    stepName.Log(Status.Pass, s + " is found.");
            //                }
            //                //This is to write lable values in the report
            //                String[] spearator1 = { " " };
            //                String[] reportValues = strlist[1].Split(spearator1,
            //                   StringSplitOptions.RemoveEmptyEntries);
            //                foreach (String s in reportValues)
            //                {
            //                    stepName.Log(Status.Pass, s + " is found.");

            //                }

            //            }
            //            File.Delete(filename);
            //            break;
            //    }
            //}
        } /*End of verifyIfReportExisted*/



        /* AlgoLabTest Alter Value */

        public static void altTestLab(WindowsDriver<WindowsElement> session, ExtentTest stepName, string device, string DeviceNo, string DeviceType)
        {
            string jsonString = File.ReadAllText(configsettingpath);
            Dictionary<string, Dictionary<string, string>> algo = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(jsonString);

            FunctionLibrary lib = new FunctionLibrary();

            bool isDeviceConnected = false;

            while (!isDeviceConnected)
            {

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

                                if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
                                {
                                    try
                                    {


                                        ModuleFunctions.socketA(session, test, DeviceType);
                                    }
                                    catch { }
                                    Thread.Sleep(2000);
                                    session = sessionInitialize(algoPath.Value, workingDirectory.Value);
                                    string computer_name = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
                                    Actions actions = new Actions(session);
                                    stepName.Log(Status.Pass, "Algo test lab is launched successfully.");
                                    Thread.Sleep(5000);


                                    session.FindElementByName("ADL").Click();
                                    stepName.Log(Status.Pass, "Moved to ADL page successfully.");
                                    Thread.Sleep(2000);

                                    session.FindElementByAccessibilityId("FINDICON").Click();
                                    Thread.Sleep(2000);
                                    session.FindElementByAccessibilityId("FINDICON").Click();
                                    Thread.Sleep(4000);
                                    //int screenHeight = session.Manage().Window.Size.Height;

                                    //string xPathOfThumb = "//*[@ClassName='Thumb']";
                                    //WindowsElement Thumb = session.FindElement(By.XPath(xPathOfThumb));
                                    //int offsetY = -(screenHeight / 2);
                                    ////Actions actions = new Actions(session);
                                    //actions.ClickAndHold(Thumb).MoveByOffset(0, offsetY).Release().Perform();
                                    //Thread.Sleep(15000);
                                    // Attempting to get window size correctly
                                    //var windowSize = session.Manage().Window.Size;
                                    //int screenHeight = windowSize.Height;

                                    //// XPath for thumb element
                                    //string xPathOfThumb = "//*[@ClassName='Thumb']";
                                    //WindowsElement Thumb = session.FindElement(By.XPath(xPathOfThumb));

                                    //// Offset for dragging
                                    //int offsetY = -(screenHeight / 2);

                                    //// Action sequence for dragging
                                    ////Actions actions = new Actions(session);
                                    //actions.ClickAndHold(Thumb).MoveByOffset(0, offsetY).Release().Perform();

                                    // Pause for a while
                                    Thread.Sleep(15000);

                                    Actions action = new Actions(session);

                                    var wait = new WebDriverWait(session, TimeSpan.FromSeconds(10));

                                    var SN = session.FindElementsByClassName("DataGrid");

                                    Thread.Sleep(10000);

                                    var dataGrids = new[] { SN[0], SN[1] };

                                    try
                                    {



                                        foreach (var dataGrid in dataGrids)
                                        {
                                            var textBlocks = dataGrid.FindElementsByClassName("TextBlock");
                                            foreach (var Value in textBlocks)
                                            {
                                                if (Value.Displayed && Value.Enabled)
                                                {
                                                    string text = Value.Text;
                                                    if (text.Equals(DeviceNo))
                                                    {
                                                        wait.Until(d => Value.Displayed && Value.Enabled);
                                                        Value.Click();
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                        //  lib.functionWaitForName(session, "Connect");

                                        session.FindElementByName("Connect").Click();
                                    }
                                    catch
                                    {
                                        session.Quit();
                                        isDeviceConnected = false;
                                    }

                                    try
                                    {
                                        lib.functionWaitForName(session, "ReSound.Ratatosk.ToolApi.HearingInstrumentFactory.HearingInstrumentProxy");

                                        Thread.Sleep(2000);

                                        lib.functionWaitForName(session, "Use when connecting next time");

                                        Thread.Sleep(2000);

                                        lib.functionWaitForName(session, "Connect");
                                    }
                                    catch (Exception)
                                    { }



                                    Thread.Sleep(90000);

                                    string statusMessage = session.FindElementByAccessibilityId("StatusMessage").Text;

                                    if (statusMessage == "Connection failed ")
                                    {
                                        Assert.AreEqual("Connection failed ", statusMessage);
                                        session.Quit();
                                        isDeviceConnected = false;

                                    }
                                    else if (statusMessage == "Gatt database detected - save of presets will be disabled until presets are read")
                                    {
                                        Assert.AreEqual("Gatt database detected - save of presets will be disabled until presets are read", statusMessage);
                                        isDeviceConnected = true;
                                    }





                                    //session = lib.waitUntilElementExists(session, "Gatt database detected - save of presets will be disabled until presets are read", 0);
                                    //lib.waitForElement(session, "Persist ADL to device when writing presets");

                                    var scenarioContext = ScenarioContext.Current;
                                    var testcaseId = scenarioContext.Get<string>("TestCaseID");

                                    if (testcaseId == "1105675")
                                    {
                                        session.FindElementByName("Histogram on Use Time from Full Charge to Low Bat").FindElementByAccessibilityId("textBox1_5").Click();
                                        Thread.Sleep(2000);
                                        session.FindElementByName("Histogram on Use Time from Full Charge to Low Bat").FindElementByAccessibilityId("textBox1_5").Clear();
                                        Thread.Sleep(2000);
                                        session.FindElementByName("Histogram on Use Time from Full Charge to Low Bat").FindElementByAccessibilityId("textBox1_5").SendKeys("1");
                                        Thread.Sleep(2000);
                                        session.FindElementByAccessibilityId("NumberOfPresetSwitchesTextBox").Click();
                                        Thread.Sleep(2000);
                                        session.FindElementByAccessibilityId("NumberOfPresetSwitchesTextBox").Clear();
                                        Thread.Sleep(2000);
                                        session.FindElementByAccessibilityId("NumberOfPresetSwitchesTextBox").SendKeys("100");

                                        screenshot = CaptureScreenshot(session);

                                        stepName.Log(Status.Pass, "Histogram on Use Time from Full Charge to Low Bat set the feild 16-18h value is 1 and Number of Preset Switches value is 100", MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                                    }
                                    else if (testcaseId == "1413300")
                                    {
                                        var fullChargeToLowBatElement = session.FindElementByName("Histogram on Use Time from Full Charge to Low Bat").FindElementByAccessibilityId("textBox1_5");

                                        var scollViewr = session.FindElementByClassName("ScrollViewer");
                                        var scrollBar = scollViewr.FindElementByClassName("ScrollBar");
                                        var thumb = scrollBar.FindElementByClassName("Thumb");
                                        int scrollHeight = thumb.Size.Height;
                                        //Actions action = new Actions(session);

                                        fullChargeToLowBatElement.Click();
                                        Thread.Sleep(2000);
                                        fullChargeToLowBatElement.Clear();
                                        Thread.Sleep(2000);
                                        fullChargeToLowBatElement.SendKeys("1");
                                        Thread.Sleep(2000);
                                        action.ClickAndHold(thumb).MoveByOffset(0, scrollHeight).Release().Perform();
                                        var batteryLevelEnteringChargerElement = session.FindElementByName("Histogram on Battery Level when Entering Charger").FindElementByAccessibilityId("textBox1_6");

                                        batteryLevelEnteringChargerElement.Click();
                                        Thread.Sleep(2000);
                                        batteryLevelEnteringChargerElement.Clear();
                                        Thread.Sleep(2000);
                                        batteryLevelEnteringChargerElement.SendKeys("1");
                                        Thread.Sleep(2000);
                                        var batteryLevelExistingChargerElement = session.FindElementByName("Histogram on Battery Level when Exiting Charger").FindElementByAccessibilityId("textBox1_7");
                                        batteryLevelExistingChargerElement.Click();
                                        Thread.Sleep(2000);
                                        batteryLevelExistingChargerElement.Clear();
                                        Thread.Sleep(2000);
                                        batteryLevelExistingChargerElement.SendKeys("1");
                                        Thread.Sleep(2000);
                                        var chagingTimeElement = session.FindElementByName("Histogram on Charging Time").FindElementByAccessibilityId("textBox1_7");
                                        chagingTimeElement.Click();
                                        Thread.Sleep(2000);
                                        chagingTimeElement.Clear();
                                        Thread.Sleep(2000);
                                        chagingTimeElement.SendKeys("1");
                                        Thread.Sleep(2000);
                                        var idelTimeChargerElement = session.FindElementByName("Histogram on Idle Time in Charger").FindElementByAccessibilityId("textBox1_6");
                                        idelTimeChargerElement.Click();
                                        Thread.Sleep(2000);
                                        idelTimeChargerElement.Clear();
                                        Thread.Sleep(2000);
                                        idelTimeChargerElement.SendKeys("1");
                                        Thread.Sleep(2000);
                                        screenshot = CaptureScreenshot(session);

                                        stepName.Log(Status.Pass, "The Battery values are altered to 1", MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                                    }
                                    else
                                    {

                                        session.FindElementByName("Histogram on Use Time from Full Charge to Low Bat").FindElementByAccessibilityId("textBox1_5").Click();
                                        Thread.Sleep(2000);
                                        session.FindElementByName("Histogram on Use Time from Full Charge to Low Bat").FindElementByAccessibilityId("textBox1_5").Clear();
                                        Thread.Sleep(2000);
                                        session.FindElementByName("Histogram on Use Time from Full Charge to Low Bat").FindElementByAccessibilityId("textBox1_5").SendKeys("1");
                                        Thread.Sleep(2000);
                                        session.FindElementByAccessibilityId("NumberOfPresetSwitchesTextBox").Click();
                                        Thread.Sleep(2000);
                                        session.FindElementByAccessibilityId("NumberOfPresetSwitchesTextBox").Clear();
                                        Thread.Sleep(2000);
                                        session.FindElementByAccessibilityId("NumberOfPresetSwitchesTextBox").SendKeys("100");

                                        screenshot = CaptureScreenshot(session);

                                        stepName.Log(Status.Pass, "Histogram on Use Time from Full Charge to Low Bat set the feild 16-18h value is 1 and Number of Preset Switches value is 100", MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                                        //lib.clickOnElementWithIdonly(session, "textBox1_5");
                                        //Thread.Sleep(2000);
                                        //session.FindElementByAccessibilityId("textBox1_5").Clear();
                                        //Thread.Sleep(2000);
                                        //session.FindElementByAccessibilityId("textBox1_5").SendKeys("1");

                                        //screenshot = CaptureScreenshot(session);

                                        stepName.Log(Status.Pass, "Altered value is 1", MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                                    }

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

                                    if (testcaseId == "1105675")
                                    {
                                        try
                                        {
                                            if (((session.FindElementByName("Histogram on Use Time from Full Charge to Low Bat").FindElementByAccessibilityId("textBox1_5").Text.ToString()) == "1.000" || (session.FindElementByName("Histogram on Use Time from Full Charge to Low Bat").FindElementByAccessibilityId("textBox1_5").Text.ToString()) == "1") && ((session.FindElementByAccessibilityId("NumberOfPresetSwitchesTextBox").Text.ToString()) == "100"))
                                            {
                                                stepName.Log(Status.Pass, $"Saved Histogram on Use Time from Full Charge to Low Bat Value is :{session.FindElementByName("Histogram on Use Time from Full Charge to Low Bat").FindElementByAccessibilityId("textBox1_5").Text.ToString()} and Saved Number of Preset Switches value is : {session.FindElementByAccessibilityId("NumberOfPresetSwitchesTextBox").Text.ToString()}");
                                                Assert.Pass();
                                                session.CloseApp();
                                            }
                                            else
                                            {
                                                stepName.Log(Status.Fail, $"Saved Histogram on Use Time from Full Charge to Low Bat Value is :{session.FindElementByName("Histogram on Use Time from Full Charge to Low Bat").FindElementByAccessibilityId("textBox1_5").Text.ToString()} and Saved Number of Preset Switches value is : {session.FindElementByAccessibilityId("NumberOfPresetSwitchesTextBox").Text.ToString()}");
                                                Assert.Fail();
                                                session.CloseApp();
                                            }
                                        }
                                        catch (Exception)
                                        { }
                                    }
                                    else if (testcaseId == "1413300")
                                    {

                                    }
                                    else
                                    {
                                        try
                                        {
                                            screenshot = CaptureScreenshot(session);

                                            if ((session.FindElementByAccessibilityId("textBox1_5").Text.ToString()) == "1.000" || (session.FindElementByAccessibilityId("textBox1_5").Text.ToString()) == "1"|| (session.FindElementByAccessibilityId("textBox1_5").Text.ToString()) == "0"|| (session.FindElementByAccessibilityId("textBox1_5").Text.ToString()) == "0.000")
                                            {
                                                Console.WriteLine("Saved Value is :" + session.FindElementByAccessibilityId("textBox1_5").Text.ToString());
                                                stepName.Log(Status.Pass, "Saved Value is :" + session.FindElementByAccessibilityId("textBox1_5").Text.ToString(), MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                                                Assert.Pass();
                                                session.CloseApp();
                                            }
                                            else
                                            {
                                                stepName.Log(Status.Fail, "Saved Value is :" + session.FindElementByAccessibilityId("textBox1_5").Text.ToString(), MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());

                                                Assert.Fail();

                                                session.CloseApp();
                                            }
                                            lib.clickOnAutomationName(session, "OK");
                                            session.CloseApp();
                                        }
                                        catch (Exception ex)
                                        {
                                            stepName.Log(Status.Fail, $"Error message : {ex}");
                                        }
                                    }
                                    session.CloseApp();
                                    lib.clickOnAutomationName(session, "OK");
                                }


                                if (DeviceType.Equals("Wired") || DeviceType.Equals("D1rechargeableWired"))
                                {

                                    //string computer_name = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
                                    session = sessionInitialize(algoPath.Value, workingDirectory.Value);
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
                                            stepName.Log(Status.Fail, "Saved Value is :" + session.FindElementByAccessibilityId("textBox1_5").Text.ToString());

                                            Assert.Fail();

                                            session.CloseApp();
                                        }
                                        lib.clickOnAutomationName(session, "OK");
                                        session.CloseApp();
                                    }
                                    catch (Exception)
                                    { }

                                    session.CloseApp();
                                    Thread.Sleep(3000);
                                    lib.clickOnAutomationName(session, "OK");
                                    isDeviceConnected = true;

                                }//End of AlgoTets Lab  
                            }
                        }


                    }
                }
            }
        }

        /* Calling Socketbox and passing commands */

        public static void ADLvaluesForFreshDevice(WindowsDriver<WindowsElement> session, ExtentTest stepName, string device, string DeviceNo, string DeviceType)
        {
            string jsonString = File.ReadAllText(configsettingpath);
            Dictionary<string, Dictionary<string, string>> algo = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(jsonString);

            FunctionLibrary lib = new FunctionLibrary();

            bool isDeviceConnected = false;

            while (!isDeviceConnected)
            {

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

                                if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
                                {
                                    try
                                    {
                                        socketB(session, test, DeviceType);
                                    }
                                    catch { }
                                    Thread.Sleep(2000);

                                    string computer_name = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
                                    session = sessionInitialize(algoPath.Value, workingDirectory.Value);
                                    Actions actions = new Actions(session);
                                    stepName.Log(Status.Pass, "Algo test lab is launched successfully.");
                                    Thread.Sleep(2000);



                                    session.FindElementByName("ADL").Click();
                                    stepName.Log(Status.Pass, "Moved to ADL page successfully.");
                                    Thread.Sleep(2000);
                                    session.FindElementByAccessibilityId("FINDICON").Click();
                                    Thread.Sleep(2000);
                                    session.FindElementByAccessibilityId("FINDICON").Click();
                                    int screenHeight = session.Manage().Window.Size.Height;

                                    string xPathOfThumb = "//*[@ClassName='Thumb']";
                                    WindowsElement Thumb = session.FindElement(By.XPath(xPathOfThumb));
                                    int offsetY = -(screenHeight / 2);
                                    //Actions actions = new Actions(session);
                                    actions.ClickAndHold(Thumb).MoveByOffset(0, offsetY).Release().Perform();
                                    Thread.Sleep(15000);
                                    // Actions action = new Actions(session);

                                    var wait = new WebDriverWait(session, TimeSpan.FromSeconds(10));

                                    var SN = session.FindElementsByClassName("DataGrid");

                                    Thread.Sleep(10000);

                                    var dataGrids = new[] { SN[0], SN[1] };

                                    try
                                    {



                                        foreach (var dataGrid in dataGrids)
                                        {
                                            var textBlocks = dataGrid.FindElementsByClassName("TextBlock");
                                            foreach (var Value in textBlocks)
                                            {
                                                if (Value.Displayed && Value.Enabled)
                                                {
                                                    string text = Value.Text;
                                                    if (text.Equals(DeviceNo))
                                                    {
                                                        wait.Until(d => Value.Displayed && Value.Enabled);
                                                        Value.Click();
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                        // lib.functionWaitForName(session, "Connect");
                                        session.FindElementByName("Connect").Click();
                                    }
                                    catch
                                    {
                                        session.Quit();
                                        isDeviceConnected = false;

                                    }


                                    try
                                    {
                                        lib.functionWaitForName(session, "ReSound.Ratatosk.ToolApi.HearingInstrumentFactory.HearingInstrumentProxy");

                                        Thread.Sleep(2000);

                                        lib.functionWaitForName(session, "Use when connecting next time");

                                        Thread.Sleep(2000);

                                        lib.functionWaitForName(session, "Connect");
                                    }
                                    catch (Exception)
                                    { }

                                    Thread.Sleep(90000);


                                    string statusMessage = session.FindElementByAccessibilityId("StatusMessage").Text;

                                    if (statusMessage == "Connection failed ")
                                    {
                                        Assert.AreEqual("Connection failed ", statusMessage);
                                        session.Quit();
                                        isDeviceConnected = false;

                                    }
                                    else if (statusMessage == "Gatt database detected - save of presets will be disabled until presets are read")
                                    {
                                        Assert.AreEqual("Gatt database detected - save of presets will be disabled until presets are read", statusMessage);
                                        isDeviceConnected = true;
                                    }
                                }



                                session = lib.waitUntilElementExists(session, "Gatt database detected - save of presets will be disabled until presets are read", 0);
                                lib.waitForElement(session, "Persist ADL to device when writing presets");
                                Actions action = new Actions(session);

                                var scollViewr = session.FindElementByClassName("ScrollViewer");
                                var scrollBar = scollViewr.FindElementByClassName("ScrollBar");
                                var thumb = scrollBar.FindElementByClassName("Thumb");
                                int scrollHeight = thumb.Size.Height;
                                action.ClickAndHold(thumb).MoveByOffset(0, scrollHeight).Release().Perform();

                                screenshot = CaptureScreenshot(session);

                                stepName.Log(Status.Pass, "Validating the Battery values for fresh device", MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                                var FullChargeLowBatlistofElements = session.FindElementByName("Histogram on Use Time from Full Charge to Low Bat").FindElementsByClassName("TextBox");
                                var FullChargeLowBatValues = FullChargeLowBatlistofElements.Select(e => e.Text).ToArray();
                                int FullChargeLowBatstartIndex = 14;
                                int FullChargeLowBatendIndex = FullChargeLowBatValues.Length;
                                for (int i = FullChargeLowBatstartIndex; i <= FullChargeLowBatendIndex - 1; i++)
                                {
                                    if (FullChargeLowBatValues[i] == "0")
                                    {
                                        stepName.Log(Status.Pass, $"Histogram on Use Time from Full Charge to Low Bat values are displayed as : {FullChargeLowBatValues[i - 13]} => {FullChargeLowBatValues[i]}");
                                    }
                                    else
                                    {
                                        stepName.Log(Status.Fail, $"Histogram on Use Time from Full Charge to Low Bat values are displayed as : {FullChargeLowBatValues[i - 13]} => {FullChargeLowBatValues[i]}");
                                    }
                                }

                                var batteryLevelEnteringChargerlistofElements = session.FindElementByName("Histogram on Battery Level when Entering Charger").FindElementsByClassName("TextBox");
                                var batteryLevelEnteringChargerValues = batteryLevelEnteringChargerlistofElements.Select(e => e.Text).ToArray();
                                int batteryLevelEnteringChargerstartIndex = 13;
                                int batteryLevelEnteringChargerendIndex = batteryLevelEnteringChargerValues.Length;
                                for (int i = batteryLevelEnteringChargerstartIndex; i <= batteryLevelEnteringChargerendIndex - 1; i++)
                                {
                                    if (batteryLevelEnteringChargerValues[i] == "0")
                                    {
                                        stepName.Log(Status.Pass, $"Histogram on Battery Level when Entering Charger values are displayed as : {batteryLevelEnteringChargerValues[i - 12]} => {batteryLevelEnteringChargerValues[i]}");
                                    }
                                    else
                                    {
                                        stepName.Log(Status.Fail, $"Histogram on Battery Level when Entering Charger values are displayed as : {batteryLevelEnteringChargerValues[i - 12]} => {batteryLevelEnteringChargerValues[i]}");
                                    }
                                }

                                var batteryLevelExistingChargerlistofElements = session.FindElementByName("Histogram on Battery Level when Exiting Charger").FindElementsByClassName("TextBox");
                                var batteryLevelExistingChargerValues = batteryLevelExistingChargerlistofElements.Select(e => e.Text).ToArray();
                                int batteryLevelExistingChargerstartIndex = 13;
                                int batteryLevelExistingChargerendIndex = batteryLevelExistingChargerValues.Length;
                                for (int i = batteryLevelExistingChargerstartIndex; i <= batteryLevelExistingChargerendIndex - 1; i++)
                                {
                                    if (batteryLevelExistingChargerValues[i] == "0")
                                    {
                                        stepName.Log(Status.Pass, $"Histogram on Battery Level when Exiting Charger values are displayed as : {batteryLevelExistingChargerValues[i - 12]} => {batteryLevelExistingChargerValues[i]}");
                                    }
                                    else
                                    {
                                        stepName.Log(Status.Fail, $"Histogram on Battery Level when Exiting Charger values are displayed as : {batteryLevelExistingChargerValues[i - 12]} => {batteryLevelExistingChargerValues[i]}");
                                    }
                                }
                                var chagingTimelistofElements = session.FindElementByName("Histogram on Charging Time").FindElementsByClassName("TextBox");
                                var chagingTimeValues = chagingTimelistofElements.Select(e => e.Text).ToArray();
                                int chagingTimestartIndex = 12;
                                int chagingTimeendIndex = chagingTimeValues.Length;
                                for (int i = chagingTimestartIndex; i <= chagingTimeendIndex - 1; i++)
                                {
                                    if (chagingTimeValues[i] == "0")
                                    {
                                        stepName.Log(Status.Pass, $"Histogram on Charging Time values are displayed as : {chagingTimeValues[i - 11]} => {chagingTimeValues[i]}");
                                    }
                                    else
                                    {
                                        stepName.Log(Status.Fail, $"Histogram on Charging Time values are displayed as : {chagingTimeValues[i - 11]} => {chagingTimeValues[i]}");
                                    }
                                }
                                var idelTimeChargerlistofElements = session.FindElementByName("Histogram on Idle Time in Charger").FindElementsByClassName("TextBox");
                                var idelTimeChargerValues = idelTimeChargerlistofElements.Select(e => e.Text).ToArray();
                                int idelTimeChargerstartIndex = 10;
                                int idelTimeChargerendIndex = idelTimeChargerValues.Length;
                                for (int i = idelTimeChargerstartIndex; i <= idelTimeChargerendIndex - 1; i++)
                                {
                                    if (idelTimeChargerValues[i] == "0")
                                    {
                                        stepName.Log(Status.Pass, $"Histogram on Idle Time in Charger values are displayed as : {idelTimeChargerValues[i - 9]} => {idelTimeChargerValues[i]}");
                                    }
                                    else
                                    {
                                        stepName.Log(Status.Fail, $"Histogram on Idle Time in Charger values are displayed as : {idelTimeChargerValues[i - 9]} => {idelTimeChargerValues[i]}");
                                    }
                                }
                                session.CloseApp();
                            }
                        }
                    }
                }
            }
        }

        public static void ADLvaluesForDeviceB(WindowsDriver<WindowsElement> session, ExtentTest stepName, string device, string DeviceNo, string DeviceType)
        {
            string jsonString = File.ReadAllText(configsettingpath);
            Dictionary<string, Dictionary<string, string>> algo = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(jsonString);

            FunctionLibrary lib = new FunctionLibrary();

            bool isDeviceConnected = false;

            while (!isDeviceConnected)
            {

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

                                if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
                                {
                                    try
                                    {
                                        socketB(session, test, DeviceType);
                                    }
                                    catch { }
                                    Thread.Sleep(2000);

                                    string computer_name = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
                                    session = sessionInitialize(algoPath.Value, workingDirectory.Value);
                                    Actions actions = new Actions(session);
                                    stepName.Log(Status.Pass, "Algo test lab is launched successfully.");
                                    Thread.Sleep(2000);



                                    session.FindElementByName("ADL").Click();
                                    stepName.Log(Status.Pass, "Moved to ADL page successfully.");
                                    Thread.Sleep(2000);
                                    session.FindElementByAccessibilityId("FINDICON").Click();
                                    Thread.Sleep(2000);
                                    session.FindElementByAccessibilityId("FINDICON").Click();
                                    int screenHeight = session.Manage().Window.Size.Height;

                                    string xPathOfThumb = "//*[@ClassName='Thumb']";
                                    WindowsElement Thumb = session.FindElement(By.XPath(xPathOfThumb));
                                    int offsetY = -(screenHeight / 2);
                                    //Actions actions = new Actions(session);
                                    actions.ClickAndHold(Thumb).MoveByOffset(0, offsetY).Release().Perform();
                                    Thread.Sleep(15000);
                                    Actions action = new Actions(session);

                                    var wait = new WebDriverWait(session, TimeSpan.FromSeconds(10));

                                    var SN = session.FindElementsByClassName("DataGrid");

                                    Thread.Sleep(10000);

                                    var dataGrids = new[] { SN[0], SN[1] };

                                    try
                                    {



                                        foreach (var dataGrid in dataGrids)
                                        {
                                            var textBlocks = dataGrid.FindElementsByClassName("TextBlock");
                                            foreach (var Value in textBlocks)
                                            {
                                                if (Value.Displayed && Value.Enabled)
                                                {
                                                    string text = Value.Text;
                                                    if (text.Equals(DeviceNo))
                                                    {
                                                        wait.Until(d => Value.Displayed && Value.Enabled);
                                                        Value.Click();
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                        //lib.functionWaitForName(session, "Connect");
                                        session.FindElementByName("Connect").Click();
                                    }
                                    catch
                                    {
                                        session.Quit();
                                        isDeviceConnected = false;

                                    }

                                    try
                                    {
                                        lib.functionWaitForName(session, "ReSound.Ratatosk.ToolApi.HearingInstrumentFactory.HearingInstrumentProxy");

                                        Thread.Sleep(2000);

                                        lib.functionWaitForName(session, "Use when connecting next time");

                                        Thread.Sleep(2000);

                                        lib.functionWaitForName(session, "Connect");
                                    }
                                    catch (Exception)
                                    { }

                                    Thread.Sleep(90000);


                                    string statusMessage = session.FindElementByAccessibilityId("StatusMessage").Text;

                                    if (statusMessage == "Connection failed ")
                                    {
                                        Assert.AreEqual("Connection failed ", statusMessage);
                                        session.Quit();
                                        isDeviceConnected = false;

                                    }
                                    else if (statusMessage == "Gatt database detected - save of presets will be disabled until presets are read")
                                    {
                                        Assert.AreEqual("Gatt database detected - save of presets will be disabled until presets are read", statusMessage);
                                        isDeviceConnected = true;
                                    }
                                }

                                session = lib.waitUntilElementExists(session, "Gatt database detected - save of presets will be disabled until presets are read", 0);
                                lib.waitForElement(session, "Persist ADL to device when writing presets");

                                session.FindElementByName("Histogram on Use Time from Full Charge to Low Bat").FindElementByAccessibilityId("textBox1_5").Click();
                                Thread.Sleep(2000);
                                session.FindElementByName("Histogram on Use Time from Full Charge to Low Bat").FindElementByAccessibilityId("textBox1_5").Clear();
                                Thread.Sleep(2000);
                                session.FindElementByName("Histogram on Use Time from Full Charge to Low Bat").FindElementByAccessibilityId("textBox1_5").SendKeys("1");
                                Thread.Sleep(2000);
                                screenshot = CaptureScreenshot(session);

                                stepName.Log(Status.Pass, "Histogram on Use Time from Full Charge to Low Bat set the feild 16-18h value is 1", MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                                /* Save changes in Fitting tab*/
                                lib.clickOnAutomationName(session, "Fitting");
                                Thread.Sleep(4000);
                                var RW = session.FindElementsByClassName("Button");
                                Thread.Sleep(5000);
                                //RW[23].Click();
                                session.FindElementByAccessibilityId("RightToolBarControl").FindElementByAccessibilityId("PresetsReadWriteControlRight").FindElementByAccessibilityId("ReadPresets").FindElementByClassName("Button").Click();
                                Thread.Sleep(1000);
                                session.FindElementByName("OK").Click();
                                session = lib.waitUntilElementExists(session, "Preset Programs read on right side.", 0);
                                //RW[24].Click();
                                session.FindElementByAccessibilityId("RightToolBarControl").FindElementByAccessibilityId("PresetsReadWriteControlRight").FindElementByAccessibilityId("SavePresets").FindElementByClassName("Button").Click();
                                session.FindElementByName("OK").Click();
                                session = lib.waitUntilElementExists(session, "Preset Programs stored on right side.", 0);
                                lib.clickOnAutomationName(session, "ADL");
                                lib.clickOnElementWithIdonly(session, "ClearNodeButton");
                                Thread.Sleep(4000);
                                lib.functionWaitForName(session, "Connect");
                                session = lib.waitUntilElementExists(session, "Gatt database detected - save of presets will be disabled until presets are read", 0);

                                session.CloseApp();
                                lib.clickOnAutomationName(session, "OK");
                            }
                        }
                    }
                }
            }
        }

        public static void socket(WindowsDriver<WindowsElement> session, ExtentTest test, string DeviceType)

        {
            FunctionLibrary lib = new FunctionLibrary();
            Thread.Sleep(10000);




            try
            {
                //session = launchApp(Directory.GetCurrentDirectory() + "\\LaunchSocket.bat", Directory.GetCurrentDirectory());
                //Process.Start("C:\\Users\\iray3\\Desktop\\Socket_Power - Copy\\dist\\Socket_Box1\\Socket_Box1.exe");

                Process process = new Process();
                process.StartInfo.FileName = "C:\\Users\\iray3\\Desktop\\Socket_Power - Copy\\dist\\Socket_Box1\\Socket_Box1.exe"; // .bat file path
                process.StartInfo.WorkingDirectory = "C:\\Users\\iray3\\Desktop\\Socket_Power - Copy\\dist\\Socket_Box1"; // Set the working directory
                process.StartInfo.UseShellExecute = true; // Use shell to execute (important for running with administrator rights)
                process.StartInfo.Verb = "runas"; // This will run the batch file as administrator

                process.Start(); // Start the process
            }

            catch (System.InvalidOperationException e)

            { }




            AppiumOptions desktopCapabilities = new AppiumOptions();
            desktopCapabilities.AddAdditionalCapability("platformName", "Windows");
            desktopCapabilities.AddAdditionalCapability("app", "Root");
            desktopCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), desktopCapabilities);

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
            AppiumOptions capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability("deviceName", "WindowsPC");
            capabilities.AddAdditionalCapability("appTopLevelWindow", topLevelWindowHandle);
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), capabilities);
            Thread.Sleep(2000);

            try
            {
                if (DeviceType.Equals("Non-Rechargeable"))
                {
                    var TextArea = "//*[@Name='Text Area']";
                    var text = session.FindElement(By.XPath(TextArea));
                    text.Click();

                    text.SendKeys("3");
                    text.SendKeys(Keys.Enter);
                    Thread.Sleep(3000);
                    text.SendKeys("A");
                    text.SendKeys(Keys.Enter);
                    Thread.Sleep(3000);
                }

                else if (DeviceType.Equals("Rechargeable"))
                {
                    var TextArea = "//*[@Name='Text Area']";
                    var text = session.FindElement(By.XPath(TextArea));
                    text.Click();


                    text.SendKeys("3");
                    text.SendKeys(Keys.Enter);
                    Thread.Sleep(3000);
                    text.SendKeys("A");
                    text.SendKeys(Keys.Enter);
                    Thread.Sleep(10000);
                    text.SendKeys("a");
                    text.SendKeys(Keys.Enter);
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
                    desktopCapabilities = new AppiumOptions();
                    desktopCapabilities.AddAdditionalCapability("platformName", "Windows");
                    desktopCapabilities.AddAdditionalCapability("app", "Root");
                    desktopCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
                    session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), desktopCapabilities);
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
                    capabilities = new AppiumOptions();
                    capabilities.AddAdditionalCapability("deviceName", "WindowsPC");
                    capabilities.AddAdditionalCapability("appTopLevelWindow", topLevelWindowHandle);
                    session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), capabilities); Thread.Sleep(2000);

                    try
                    {
                        if (DeviceType.Equals("Non-Rechargeable"))
                        {
                            var TextArea = "//*[@Name='Text Area']";
                            var text = session.FindElement(By.XPath(TextArea));
                            text.Click();

                            text.SendKeys("3");
                            text.SendKeys(Keys.Enter);
                            session.Keyboard.SendKeys("A");
                            text.SendKeys(Keys.Enter);
                        }
                        if (DeviceType.Equals("Rechargeable"))
                        {
                            var TextArea = "//*[@Name='Text Area']";
                            var text = session.FindElement(By.XPath(TextArea));
                            text.Click();

                            text.SendKeys("3");
                            text.SendKeys(Keys.Enter);
                            Thread.Sleep(3000);
                            text.SendKeys("A");
                            text.SendKeys(Keys.Enter);
                            Thread.Sleep(6000);
                            text.SendKeys("a");
                            text.SendKeys(Keys.Enter);
                            Thread.Sleep(5000);
                        }
                    }
                    catch (Exception) { }
                }
            }
        }



        /* Passing Commands to Socketbox for Left Side Device */


        public static void socketA(WindowsDriver<WindowsElement> session, ExtentTest test, string deviceType)
        {
            FunctionLibrary lib = new FunctionLibrary();
            Thread.Sleep(10000);

            // Initialize capabilities for desktop session
            AppiumOptions desktopCapabilities = new AppiumOptions();
            desktopCapabilities.AddAdditionalCapability("platformName", "Windows");
            desktopCapabilities.AddAdditionalCapability("app", "Root");
            desktopCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
            desktopCapabilities.AddAdditionalCapability("appArguments", "--run-as-administrator");

            //session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), desktopCapabilities);
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), desktopCapabilities);
            WindowsElement applicationWindow = null;

            // Find the open windows
            var openWindows = session.FindElementsByClassName("ConsoleWindowClass");
            foreach (var window in openWindows)
            {
                if (!window.GetAttribute("Name").StartsWith("WinAppDriver"))
                {
                    applicationWindow = window;
                    break;
                }
            }

            // Get the top-level window handle
            var topLevelWindowHandle = applicationWindow.GetAttribute("NativeWindowHandle");
            topLevelWindowHandle = int.Parse(topLevelWindowHandle).ToString("X");

            // Reinitialize the session for the specific application window
            AppiumOptions capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability("deviceName", "WindowsPC");
            capabilities.AddAdditionalCapability("appTopLevelWindow", topLevelWindowHandle);

            //session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), capabilities);
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), capabilities);
            Thread.Sleep(2000);

            try
            {

                if (deviceType.Equals("Non-Rechargeable"))
                {
                    var TextArea = "//*[@Name='Text Area']";
                    var text = session.FindElement(By.XPath(TextArea));
                    text.Click();

                    text.SendKeys("B");
                    Thread.Sleep(8000);
                    text.SendKeys(Keys.Enter);
                    Thread.Sleep(8000);
                    text.SendKeys("a");
                    Thread.Sleep(8000);
                    text.SendKeys(Keys.Enter);
                    Thread.Sleep(8000);
                    text.SendKeys("A");
                    Thread.Sleep(8000);
                    text.SendKeys(Keys.Enter);
                    Thread.Sleep(8000);
                }

                else if (deviceType.Equals("Rechargeable"))
                {
                    var TextArea = "//*[@Name='Text Area']";
                    var text = session.FindElement(By.XPath(TextArea));
                    text.Click();

                    text.SendKeys("A");
                    Thread.Sleep(8000);
                    text.SendKeys(Keys.Enter);
                    Thread.Sleep(2000);
                    text.SendKeys("a");
                    Thread.Sleep(8000);
                    text.SendKeys(Keys.Enter);
                    Thread.Sleep(2000);
                    text.SendKeys("A");
                    Thread.Sleep(8000);
                    text.SendKeys(Keys.Enter);
                    Thread.Sleep(2000);
                    text.SendKeys("a");
                    Thread.Sleep(4000);
                    text.SendKeys(Keys.Enter);
                }
            }

            catch (Exception e)
            {
                if (e.GetType().ToString() == "System.InvalidOperationException")
                {
                    desktopCapabilities = new AppiumOptions();
                    desktopCapabilities.AddAdditionalCapability("platformName", "Windows");
                    desktopCapabilities.AddAdditionalCapability("app", "Root");
                    desktopCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
                    //session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), desktopCapabilities);
                    session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), desktopCapabilities);
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
                    capabilities = new AppiumOptions();
                    capabilities.AddAdditionalCapability("deviceName", "WindowsPC");
                    capabilities.AddAdditionalCapability("appTopLevelWindow", topLevelWindowHandle);
                    //session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), capabilities); Thread.Sleep(2000);
                    session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), capabilities);

                    try
                    {

                        if (deviceType.Equals("Non-Rechargeable"))
                        {
                            var TextArea = "//*[@Name='Text Area']";
                            var text = session.FindElement(By.XPath(TextArea));
                            text.Click();

                            text.SendKeys("b");
                            Thread.Sleep(8000);
                            text.SendKeys(Keys.Enter);
                            Thread.Sleep(8000);
                            text.SendKeys("a");
                            Thread.Sleep(8000);
                            text.SendKeys(Keys.Enter);
                            Thread.Sleep(8000);
                            text.SendKeys("A");
                            Thread.Sleep(8000);
                            text.SendKeys(Keys.Enter);
                            Thread.Sleep(8000);
                        }

                        if (deviceType.Equals("Rechargeable"))
                        {
                            var TextArea = "//*[@Name='Text Area']";
                            var text = session.FindElement(By.XPath(TextArea));
                            text.Click();

                            text.SendKeys("A");
                            Thread.Sleep(8000);
                            text.SendKeys(Keys.Enter);
                            Thread.Sleep(2000);
                            text.SendKeys("a");
                            Thread.Sleep(8000);
                            text.SendKeys(Keys.Enter);
                            Thread.Sleep(2000);
                            text.SendKeys("A");
                            Thread.Sleep(8000);
                            text.SendKeys(Keys.Enter);
                            Thread.Sleep(2000);
                            text.SendKeys("a");
                            Thread.Sleep(4000);
                            text.SendKeys(Keys.Enter);
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
            AppiumOptions desktopCapabilities = new AppiumOptions();
            desktopCapabilities.AddAdditionalCapability("platformName", "Windows");
            desktopCapabilities.AddAdditionalCapability("app", "Root");
            desktopCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), desktopCapabilities);
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
            AppiumOptions capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability("deviceName", "WindowsPC");
            capabilities.AddAdditionalCapability("appTopLevelWindow", topLevelWindowHandle);
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), capabilities);

            Thread.Sleep(2000);

            try
            {

                if (DeviceType.Equals("Non-Rechargeable"))
                {
                    var TextArea = "//*[@Name='Text Area']";
                    var text = session.FindElement(By.XPath(TextArea));
                    text.Click();


                    text.SendKeys("A");
                    Thread.Sleep(8000);
                    text.SendKeys(Keys.Enter);
                    Thread.Sleep(8000);
                    text.SendKeys("a");
                    Thread.Sleep(8000);
                    text.SendKeys(Keys.Enter);
                    Thread.Sleep(8000);
                    text.SendKeys("b");
                    Thread.Sleep(8000);
                    text.SendKeys(Keys.Enter);
                    Thread.Sleep(8000);
                    text.SendKeys("B");
                    Thread.Sleep(8000);
                    text.SendKeys(Keys.Enter);
                    Thread.Sleep(8000);
                }

                else if (DeviceType.Equals("Rechargeable"))
                {
                    var TextArea = "//*[@Name='Text Area']";
                    var text = session.FindElement(By.XPath(TextArea));
                    text.Click();


                    text.SendKeys(Keys.Enter);
                    Thread.Sleep(2000);
                    text.SendKeys("B");
                    text.SendKeys(Keys.Enter);
                    Thread.Sleep(8000);
                    text.SendKeys("b");
                    text.SendKeys(Keys.Enter);
                    Thread.Sleep(2000);
                    text.SendKeys("B");
                    text.SendKeys(Keys.Enter);
                    Thread.Sleep(8000);
                    text.SendKeys("b");
                    text.SendKeys(Keys.Enter);

                }
            }

            catch (Exception e)
            {
                if (e.GetType().ToString() == "System.InvalidOperationException")
                {
                    desktopCapabilities = new AppiumOptions();
                    desktopCapabilities.AddAdditionalCapability("platformName", "Windows");
                    desktopCapabilities.AddAdditionalCapability("app", "Root");
                    desktopCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
                    session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), desktopCapabilities);
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
                    capabilities = new AppiumOptions();
                    capabilities.AddAdditionalCapability("deviceName", "WindowsPC");
                    capabilities.AddAdditionalCapability("appTopLevelWindow", topLevelWindowHandle);
                    session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), capabilities);
                    Thread.Sleep(2000);

                    try
                    {
                        if (DeviceType.Equals("Non-Rechargeable"))
                        {
                            var TextArea = "//*[@Name='Text Area']";
                            var text = session.FindElement(By.XPath(TextArea));
                            text.Click();

                            text.SendKeys("a");
                            Thread.Sleep(8000);
                            text.SendKeys(Keys.Enter);
                            Thread.Sleep(8000);
                            text.SendKeys("b");
                            Thread.Sleep(8000);
                            text.SendKeys(Keys.Enter);
                            Thread.Sleep(8000);
                            text.SendKeys("B");
                            Thread.Sleep(8000);
                            text.SendKeys(Keys.Enter);
                            Thread.Sleep(8000);
                        }

                        else if (DeviceType.Equals("Rechargeable"))
                        {
                            var TextArea = "//*[@Name='Text Area']";
                            var text = session.FindElement(By.XPath(TextArea));
                            text.Click();


                            text.SendKeys("a");
                            Thread.Sleep(1000);
                            text.SendKeys(Keys.Enter);
                            Thread.Sleep(1000);
                            text.SendKeys("b");
                            Thread.Sleep(1000);
                            text.SendKeys(Keys.Enter);
                            Thread.Sleep(1000);
                            text.SendKeys("B");
                            Thread.Sleep(1000);
                            text.SendKeys(Keys.Enter);
                            Thread.Sleep(4000);
                            text.SendKeys("b");
                            Thread.Sleep(2000);
                            text.SendKeys(Keys.Enter);
                            Thread.Sleep(1000);
                            text.SendKeys("B");
                            text.SendKeys(Keys.Enter);
                            Thread.Sleep(8000);
                            text.SendKeys("b");
                            text.SendKeys(Keys.Enter);
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
            AppiumOptions desktopCapabilities = new AppiumOptions();
            desktopCapabilities.AddAdditionalCapability("platformName", "Windows");
            desktopCapabilities.AddAdditionalCapability("app", "Root");
            desktopCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), desktopCapabilities);
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
            AppiumOptions capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability("deviceName", "WindowsPC");
            capabilities.AddAdditionalCapability("appTopLevelWindow", topLevelWindowHandle);
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), capabilities);

            Thread.Sleep(2000);

            try
            {
                if (DeviceType.Equals("Non-Rechargeable"))
                {
                    var TextArea = "//*[@Name='Text Area']";
                    var text = session.FindElement(By.XPath(TextArea));
                    text.Click();

                    text.SendKeys("A");
                    Thread.Sleep(8000);
                    text.SendKeys(Keys.Enter);
                    Thread.Sleep(8000);
                    text.SendKeys("b");
                    Thread.Sleep(8000);
                    text.SendKeys(Keys.Enter);
                    Thread.Sleep(8000);
                    text.SendKeys("B");
                    Thread.Sleep(8000);
                    text.SendKeys(Keys.Enter);
                    Thread.Sleep(8000);
                }

                else if (DeviceType.Equals("Rechargeable"))
                {
                    var TextArea = "//*[@Name='Text Area']";
                    var text = session.FindElement(By.XPath(TextArea));
                    text.Click();

                    text.SendKeys("A");
                    Thread.Sleep(2000);
                    text.SendKeys(Keys.Enter);
                    Thread.Sleep(2000);
                    text.SendKeys(Keys.Enter);
                    Thread.Sleep(2000);
                    text.SendKeys("B");
                    Thread.Sleep(8000);
                    text.SendKeys(Keys.Enter);
                    Thread.Sleep(8000);
                    text.SendKeys("C");
                    Thread.Sleep(8000);
                    text.SendKeys(Keys.Enter);
                    Thread.Sleep(8000);
                    text.SendKeys("c");
                    Thread.Sleep(8000);
                    text.SendKeys(Keys.Enter);
                    Thread.Sleep(8000);

                }
            }

            catch (Exception e)
            {
                if (e.GetType().ToString() == "System.InvalidOperationException")
                {
                    desktopCapabilities = new AppiumOptions();
                    desktopCapabilities.AddAdditionalCapability("platformName", "Windows");
                    desktopCapabilities.AddAdditionalCapability("app", "Root");
                    desktopCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
                    session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), desktopCapabilities);
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
                    capabilities = new AppiumOptions();
                    capabilities.AddAdditionalCapability("deviceName", "WindowsPC");
                    capabilities.AddAdditionalCapability("appTopLevelWindow", topLevelWindowHandle);
                    session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), capabilities);
                    Thread.Sleep(2000);

                    try
                    {
                        if (DeviceType.Equals("Non-Rechargeable"))
                        {
                            var TextArea = "//*[@Name='Text Area']";
                            var text = session.FindElement(By.XPath(TextArea));
                            text.Click();

                            text.SendKeys("a");
                            Thread.Sleep(8000);
                            text.SendKeys(Keys.Enter);
                            Thread.Sleep(8000);
                            text.SendKeys("b");
                            Thread.Sleep(8000);
                            text.SendKeys(Keys.Enter);
                            Thread.Sleep(8000);
                            text.SendKeys("B");
                            Thread.Sleep(8000);
                            text.SendKeys(Keys.Enter);
                            Thread.Sleep(8000);
                        }

                        else if (DeviceType.Equals("Rechargeable"))
                        {
                            var TextArea = "//*[@Name='Text Area']";
                            var text = session.FindElement(By.XPath(TextArea));
                            text.Click();

                            text.SendKeys("a");
                            Thread.Sleep(1000);
                            text.SendKeys(Keys.Enter);
                            Thread.Sleep(1000);
                            text.SendKeys("b");
                            Thread.Sleep(1000);
                            text.SendKeys(Keys.Enter);
                            Thread.Sleep(1000);
                            text.SendKeys("B");
                            Thread.Sleep(1000);
                            text.SendKeys(Keys.Enter);
                            Thread.Sleep(4000);
                            text.SendKeys("b");
                            Thread.Sleep(2000);
                            text.SendKeys(Keys.Enter);
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

            AppiumOptions desktopCapabilities = new AppiumOptions();
            desktopCapabilities.AddAdditionalCapability("platformName", "Windows");
            desktopCapabilities.AddAdditionalCapability("app", "Root");
            desktopCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), desktopCapabilities);
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
            AppiumOptions capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability("deviceName", "WindowsPC");
            capabilities.AddAdditionalCapability("appTopLevelWindow", topLevelWindowHandle);
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), capabilities);
            Thread.Sleep(2000);

            try
            {
                var TextArea = "//*[@Name='Text Area']";
                var text = session.FindElement(By.XPath(TextArea));
                text.Click();

                text.SendKeys("3");
                text.SendKeys(Keys.Enter);
                text.SendKeys("B");
                text.SendKeys(Keys.Enter);
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
                    desktopCapabilities = new AppiumOptions();
                    desktopCapabilities.AddAdditionalCapability("platformName", "Windows");
                    desktopCapabilities.AddAdditionalCapability("app", "Root");
                    desktopCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
                    session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), desktopCapabilities);
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
                    capabilities = new AppiumOptions();
                    capabilities.AddAdditionalCapability("deviceName", "WindowsPC");
                    capabilities.AddAdditionalCapability("appTopLevelWindow", topLevelWindowHandle);
                    session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), capabilities); Thread.Sleep(2000); try

                    {
                        var TextArea = "//*[@Name='Text Area']";
                        var text = session.FindElement(By.XPath(TextArea));
                        text.Click();

                        text.SendKeys("3");
                        text.SendKeys(Keys.Enter);
                        text.SendKeys("B");
                        text.SendKeys(Keys.Enter);
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
            AppiumOptions desktopCapabilities = new AppiumOptions();
            desktopCapabilities.AddAdditionalCapability("platformName", "Windows");
            desktopCapabilities.AddAdditionalCapability("app", "Root");
            desktopCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), desktopCapabilities); WindowsElement applicationWindow = null;
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
            AppiumOptions capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability("deviceName", "WindowsPC");
            capabilities.AddAdditionalCapability("appTopLevelWindow", topLevelWindowHandle);
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), capabilities);
            Thread.Sleep(2000);
            session.CloseApp();
        }


        /* Verifying ADL values in AlgoLabTest */

        public static void checkADLValue(WindowsDriver<WindowsElement> session, ExtentTest stepName, string device, string DeviceNo, string DeviceType)
        {
            string jsonString = File.ReadAllText(configsettingpath);
            Dictionary<string, Dictionary<string, string>> algo = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(jsonString);
            FunctionLibrary lib = new FunctionLibrary();
            bool isDeviceConnected = false;

            while (!isDeviceConnected)
            {

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

                                if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
                                {
                                    try
                                    {
                                        socketA(session, test, DeviceType);
                                    }
                                    catch { }
                                    Thread.Sleep(2000);

                                    string computer_name = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
                                    session = sessionInitialize(algoPath.Value, workingDirectory.Value);
                                    Actions actions = new Actions(session);
                                    stepName.Log(Status.Pass, "Algo test lab is launched successfully.");
                                    Thread.Sleep(5000);


                                    session.FindElementByName("ADL").Click();
                                    stepName.Log(Status.Pass, "Moved to ADL page successfully.");
                                    Thread.Sleep(2000);

                                    session.FindElementByAccessibilityId("FINDICON").Click();
                                    Thread.Sleep(2000);
                                    session.FindElementByAccessibilityId("FINDICON").Click();
                                    int screenHeight = session.Manage().Window.Size.Height;

                                    string xPathOfThumb = "//*[@ClassName='Thumb']";
                                    WindowsElement Thumb = session.FindElement(By.XPath(xPathOfThumb));
                                    int offsetY = -(screenHeight / 2);
                                    //Actions actions = new Actions(session);
                                    actions.ClickAndHold(Thumb).MoveByOffset(0, offsetY).Release().Perform();
                                    Thread.Sleep(15000);
                                    Actions action = new Actions(session);

                                    var wait = new WebDriverWait(session, TimeSpan.FromSeconds(10));

                                    var SN = session.FindElementsByClassName("DataGrid");

                                    Thread.Sleep(10000);

                                    var dataGrids = new[] { SN[0], SN[1] };

                                    try
                                    {



                                        foreach (var dataGrid in dataGrids)
                                        {
                                            var textBlocks = dataGrid.FindElementsByClassName("TextBlock");
                                            foreach (var Value in textBlocks)
                                            {
                                                if (Value.Displayed && Value.Enabled)
                                                {
                                                    string text = Value.Text;
                                                    if (text.Equals(DeviceNo))
                                                    {
                                                        wait.Until(d => Value.Displayed && Value.Enabled);
                                                        Value.Click();
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                        //  lib.functionWaitForName(session, "Connect");

                                        session.FindElementByName("Connect").Click();
                                    }
                                    catch
                                    {
                                        session.Quit();
                                        isDeviceConnected = false;
                                    }

                                    try
                                    {
                                        lib.functionWaitForName(session, "ReSound.Ratatosk.ToolApi.HearingInstrumentFactory.HearingInstrumentProxy");

                                        Thread.Sleep(2000);

                                        lib.functionWaitForName(session, "Use when connecting next time");

                                        Thread.Sleep(2000);

                                        lib.functionWaitForName(session, "Connect");
                                    }
                                    catch (Exception)
                                    { }



                                    Thread.Sleep(90000);

                                    string statusMessage = session.FindElementByAccessibilityId("StatusMessage").Text;

                                    if (statusMessage == "Connection failed ")
                                    {
                                        Assert.AreEqual("Connection failed ", statusMessage);
                                        session.Quit();
                                        isDeviceConnected = false;

                                    }
                                    else if (statusMessage == "Gatt database detected - save of presets will be disabled until presets are read")
                                    {
                                        Assert.AreEqual("Gatt database detected - save of presets will be disabled until presets are read", statusMessage);
                                        isDeviceConnected = true;
                                    }
                                }
                                session = lib.waitUntilElementExists(session, "Gatt database detected - save of presets will be disabled until presets are read", 0);
                            }
                        }
                    }
                }
            }

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
            else if (device.Contains("RE"))
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
                var scenarioContext = ScenarioContext.Current;
                var testcaseId = scenarioContext.Get<string>("TestCaseID");

                screenshot = CaptureScreenshot(session);

                if (testcaseId == "1105675")
                {

                    Thread.Sleep(2000);
                    if ((session.FindElementByName("Histogram on Use Time from Full Charge to Low Bat").FindElementByAccessibilityId("textBox1_5").Text.ToString() == "0.000"))
                    {
                        stepName.Log(Status.Pass, "Histogram on Use Time from Full Charge to Low Bat set the feild 16-18h value is 0 and Number of Preset Switches value is 100", MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                    }
                    else
                    {
                        screenshot = CaptureScreenshot(session);
                        stepName.Log(Status.Fail, "Saved Value is :" + session.FindElementByAccessibilityId("textBox1_5").Text.ToString(), MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                        session.CloseApp();
                    }
                }
                else
                {
                    if ((session.FindElementByAccessibilityId("textBox1_5").Text.ToString()) == "1.000")
                    {
                        screenshot = CaptureScreenshot(session);
                        Console.WriteLine("Saved Value is :" + session.FindElementByAccessibilityId("textBox1_5").Text.ToString());
                        stepName.Log(Status.Fail, "Saved Value is :" + session.FindElementByAccessibilityId("textBox1_5").Text.ToString(), MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                        Assert.Pass();
                        session.CloseApp();
                    }
                    else
                    {
                        screenshot = CaptureScreenshot(session);
                        stepName.Log(Status.Pass, "Saved Value is :" + session.FindElementByAccessibilityId("textBox1_5").Text.ToString(), MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());

                        session.CloseApp();
                    }
                }
            }
            catch (Exception e)
            {
                session.CloseApp();
            }
        }//End of Verify ADL Value

        public static void checkADLValueRightDevice(WindowsDriver<WindowsElement> session, ExtentTest stepName, string device, string DeviceNo, string DeviceType, string side)
        {
            string jsonString = File.ReadAllText(configsettingpath);
            Dictionary<string, Dictionary<string, string>> algo = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(jsonString);
            FunctionLibrary lib = new FunctionLibrary();



            bool isDeviceConnected = false;

            while (!isDeviceConnected)
            {

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

                                if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
                                {
                                    try
                                    {

                                        if (side.Equals("Left"))

                                        {
                                            ModuleFunctions.socketA(session, test, DeviceType);
                                        }

                                        else if (side.Equals("Right"))

                                        {
                                            ModuleFunctions.socketB(session, test, DeviceType);
                                        }
                                        //ModuleFunctions.socketB(session, test, DeviceType);
                                    }
                                    catch { }
                                    Thread.Sleep(2000);

                                    string computer_name = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
                                    session = sessionInitialize(algoPath.Value, workingDirectory.Value);
                                    Actions actions = new Actions(session);
                                    stepName.Log(Status.Pass, "Algo test lab is launched successfully.");
                                    Thread.Sleep(5000);


                                    session.FindElementByName("ADL").Click();
                                    stepName.Log(Status.Pass, "Moved to ADL page successfully.");
                                    Thread.Sleep(2000);

                                    session.FindElementByAccessibilityId("FINDICON").Click();
                                    Thread.Sleep(2000);
                                    session.FindElementByAccessibilityId("FINDICON").Click();
                                    int screenHeight = session.Manage().Window.Size.Height;

                                    string xPathOfThumb = "//*[@ClassName='Thumb']";
                                    WindowsElement Thumb = session.FindElement(By.XPath(xPathOfThumb));
                                    int offsetY = -(screenHeight / 2);
                                    //Actions actions = new Actions(session);
                                    actions.ClickAndHold(Thumb).MoveByOffset(0, offsetY).Release().Perform();
                                    Thread.Sleep(15000);
                                    Actions action = new Actions(session);

                                    var wait = new WebDriverWait(session, TimeSpan.FromSeconds(10));

                                    var SN = session.FindElementsByClassName("DataGrid");

                                    Thread.Sleep(10000);

                                    var dataGrids = new[] { SN[0], SN[1] };

                                    try
                                    {



                                        foreach (var dataGrid in dataGrids)
                                        {
                                            var textBlocks = dataGrid.FindElementsByClassName("TextBlock");
                                            foreach (var Value in textBlocks)
                                            {
                                                if (Value.Displayed && Value.Enabled)
                                                {
                                                    string text = Value.Text;
                                                    if (text.Equals(DeviceNo))
                                                    {
                                                        wait.Until(d => Value.Displayed && Value.Enabled);
                                                        Value.Click();
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                        //  lib.functionWaitForName(session, "Connect");

                                        session.FindElementByName("Connect").Click();
                                    }
                                    catch
                                    {
                                        session.Quit();
                                        isDeviceConnected = false;
                                    }

                                    try
                                    {
                                        lib.functionWaitForName(session, "ReSound.Ratatosk.ToolApi.HearingInstrumentFactory.HearingInstrumentProxy");

                                        Thread.Sleep(2000);

                                        lib.functionWaitForName(session, "Use when connecting next time");

                                        Thread.Sleep(2000);

                                        lib.functionWaitForName(session, "Connect");
                                    }
                                    catch (Exception)
                                    { }



                                    Thread.Sleep(90000);

                                    string statusMessage = session.FindElementByAccessibilityId("StatusMessage").Text;

                                    if (statusMessage == "Connection failed ")
                                    {
                                        Assert.AreEqual("Connection failed ", statusMessage);
                                        session.Quit();
                                        isDeviceConnected = false;

                                    }
                                    else if (statusMessage == "Gatt database detected - save of presets will be disabled until presets are read")
                                    {
                                        Assert.AreEqual("Gatt database detected - save of presets will be disabled until presets are read", statusMessage);
                                        isDeviceConnected = true;
                                    }
                                }
                                session = lib.waitUntilElementExists(session, "Gatt database detected - save of presets will be disabled until presets are read", 0);
                                Thread.Sleep(2000);
                                if ((session.FindElementByName("Histogram on Use Time from Full Charge to Low Bat").FindElementByAccessibilityId("textBox1_5").Text.ToString() == "0"))
                                {
                                    stepName.Log(Status.Pass, "Histogram on Use Time from Full Charge to Low Bat set the feild 16-18h value is 0 and Number of Preset Switches value is 100", MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                                    session.CloseApp();
                                }
                                else
                                {
                                    screenshot = CaptureScreenshot(session);
                                    stepName.Log(Status.Fail, "Saved Value is :" + session.FindElementByAccessibilityId("textBox1_5").Text.ToString(), MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                                    session.CloseApp();
                                }
                            }
                        }
                    }
                }
            }
        }
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


        /*This is to take the dump the device image from storage layout*/

        public static void takeDeviceDumpImage(WindowsDriver<WindowsElement> session, ExtentTest stepName, string device, String fileName, String side, string DeviceNo, string DeviceType)
        {
            Console.WriteLine("test");
            //Thread.Sleep(5000);
            string computer_name = System.Environment.GetEnvironmentVariable("COMPUTERNAME");

            FunctionLibrary lib = new FunctionLibrary();

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
                            Thread.Sleep(3000);
                            session = sessionInitialize(slvPath.Value, workingDirectory.Value);

                            /** To Connect the device( RT or RU) to Stroragelayout viewr **/

                            if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
                            {

                                Actions actions = new Actions(session);
                                var wait = new WebDriverWait(session, TimeSpan.FromSeconds(10));
                                Thread.Sleep(5000);
                                var cancelButton = wait.Until(ExpectedConditions.ElementToBeClickable(session.FindElementByAccessibilityId("FINDICON")));
                                cancelButton.Click();
                                Thread.Sleep(10000);
                                var DetectButton = wait.Until(ExpectedConditions.ElementToBeClickable(session.FindElementByAccessibilityId("FINDICON")));
                                DetectButton.Click();
                                //var Popup = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("Popup")));
                                //int height = Popup.Size.Height;
                                //var drag = Popup.FindElements(By.ClassName("Thumb"));

                                //if (drag.Count >= 7)
                                //{
                                //    actions.MoveToElement(drag[6]).Perform();
                                //    actions.ClickAndHold(drag[6]).MoveByOffset(0, height * 3).Release().Perform();
                                //}

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
                                            //actions = new Actions(session);
                                            //actions.MoveToElement(element).Click().Perform();
                                            element.Click();
                                            stepName.Log(Status.Info, "Device selected successfully..");
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


                                        if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
                                        {

                                            if (side.Equals("Left"))
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
                                                            //Popup = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("Popup")));
                                                            //height = Popup.Size.Height;
                                                            //drag = Popup.FindElements(By.ClassName("Thumb"));

                                                            //if (drag.Count >= 7)
                                                            //{
                                                            //    actions.MoveToElement(drag[6]).Perform();
                                                            //    actions.ClickAndHold(drag[6]).MoveByOffset(0, height * 3).Release().Perform();
                                                            //}

                                                            Thread.Sleep(15000);

                                                            var GridCell = session.FindElementByClassName("DataGrid");
                                                            ReadOnlyCollection<AppiumWebElement> GridCells = GridCell.FindElementsByClassName("DataGridCell");
                                                            // Thread.Sleep(7000);
                                                            // Iterate through DataGridCell elements
                                                            foreach (var element in GridCells)
                                                            {
                                                                if (element.Text == DeviceNo)
                                                                {
                                                                    //actions = new Actions(session);
                                                                    //actions.MoveToElement(element).Click().Perform();
                                                                    element.Click();

                                                                    stepName.Log(Status.Info, "Device selected successfully..");
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
                                Thread.Sleep(5000);

                                lib.functionWaitForName(session, "Connect");

                                lib.waitUntilElementExists(session, "File", 0);
                                Thread.Sleep(10000);
                                var ext = session.FindElements(WorkFlowPageFactory.fileMenu);
                                //ext[0].Click();
                                //Thread.Sleep(5000);
                                //ext = session.FindElements(WorkFlowPageFactory.readHI);
                                //actions = new Actions(session);
                                //actions.MoveToElement(ext[0]).Build().Perform();
                                //Thread.Sleep(5000);
                                //session.Keyboard.PressKey(Keys.Enter);
                                var read = "//*[@Name='_Read from']";
                                var readFrom = session.FindElement(By.XPath(read));
                                actions.MoveToElement(readFrom).Click().Perform();
                                Thread.Sleep(15000);

                                stepName.Log(Status.Info, "Device connected successfully..");
                                /** Click on Uncheck button **/

                                session.FindElementByName("Uncheck All").Click();
                                Thread.Sleep(5000);
                                session.FindElementByAccessibilityId("1001").Click();
                                Thread.Sleep(5000);

                                /** Choose the All option in drop down **/

                                // Find the element by XPath

                                var all = "//*[@Name='All']";
                                var allClick = session.FindElement(By.XPath(all));
                                Thread.Sleep(5000);
                                Actions actions1 = new Actions(session);
                                actions1.MoveToElement(allClick).Click().Perform();

                                //var pointerInput = new PointerInputDevice(PointerKind.Mouse);

                                //var elements = session.FindElement(By.XPath(all));
                                //var actions1 = new ActionSequence(pointerInput, 0);
                                //actions1.AddAction(pointerInput.CreatePointerMove(elements, 0, 0, TimeSpan.FromMilliseconds(500))); // Move to the center of the element
                                //session.PerformActions(new List<ActionSequence> { actions1 });


                                //// Create a TouchAction instance
                                ////TouchAction touchAction = new TouchAction(session);
                                ////Thread.Sleep(5000);
                                ////// Move to the element and press the element to simulate click
                                ////touchAction.MoveTo(action, 5000);
                                ////action.Click();

                                //// Perform drag-and-drop

                                ////IWebElement rd = session.FindElementByName("All");
                                ////Thread.Sleep(5000);

                                ////// Create a TouchAction instance
                                ////TouchAction touchAction = new TouchAction(session);
                                ////Thread.Sleep(5000);
                                ////// Move to the element and perform a tap (click)
                                ////touchAction.MoveTo(rd).Press(rd).Perform();
                                ////rd.Click();
                                ////TouchActions touchActions = new TouchActions(session);
                                ////touchActions.MoveToElement(rd).Perform();

                                ////actions.MoveToElement(rd).Click().Perform();

                                Thread.Sleep(2000);

                                /** To Click the Apply selection button **/

                                session.FindElementByName("Apply selection").Click();
                                string file = "//*[@Name='File']";
                                var dropFile = session.FindElement(By.XPath(file));
                                dropFile.Click();

                                //ext = session.FindElements(WorkFlowPageFactory.fileMenu);
                                //ext[0].Click();
                                Thread.Sleep(2000);
                                //ext = session.FindElements(WorkFlowPageFactory.dumpHI);
                                Actions act = new Actions(session);
                                //act.MoveToElement(ext[0]).Build().Perform();
                                string Savefile = "//*[@Name='Save as CDI file']";
                                var xPtahOfFile = session.FindElement(By.XPath(Savefile));
                                act.MoveToElement(xPtahOfFile).Click().Perform();
                                //touchAction.MoveTo(xPtahOfFile).Press(xPtahOfFile).Perform();
                                //xPtahOfFile.Click();
                                Thread.Sleep(2000);
                                //session.Keyboard.PressKey(Keys.Enter);
                                Thread.Sleep(4000);
                                //var file = "//*[@Name='File']";
                                //session.FindElementByAccessibilityId(file).Click();
                                //Thread.Sleep(2000);
                                //var Savefile = "//*[@Name='Save as CDI file']";
                                //session.FindElement(By.XPath(Savefile)).Click();
                                Thread.Sleep(4000);
                                session.FindElementByClassName("Edit").SendKeys("C:\\" + fileName + ".xml");
                                Thread.Sleep(4000);

                                /** To save the Dump in Xml file **/

                                session.FindElementByName("Save").Click();
                                Thread.Sleep(4000);
                                session.SwitchTo().Window(session.WindowHandles.First());
                                ////session.SwitchTo().ActiveElement();
                                WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(80));
                                Thread.Sleep(8000);


                                session.SwitchTo().Window(session.WindowHandles.First());
                                Thread.Sleep(50000);

                                try
                                {
                                    if (session.WindowHandles.Count() > 0)
                                    {
                                        session.SwitchTo().Window(session.WindowHandles.First());
                                        ////session.SwitchTo().ActiveElement();
                                        session.FindElementByAccessibilityId("checkBoxIgnoreAll").Click();
                                        Thread.Sleep(2000);
                                        session.FindElementByAccessibilityId("buttonOk").Click();
                                    }
                                }
                                catch (Exception e)
                                {
                                    try
                                    {
                                        if (e.GetType().ToString() == "System.InvalidOperationException")
                                        {
                                            var simu = new InputSimulator();
                                            simu.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_T);
                                            Thread.Sleep(2000);
                                            simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                            Thread.Sleep(2000);
                                            simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                            Thread.Sleep(2000);
                                            simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                            Thread.Sleep(2000);
                                            simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                            Thread.Sleep(2000);
                                            simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                            Thread.Sleep(2000);
                                            //simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                            //Thread.Sleep(2000);
                                            simu.Keyboard.KeyPress(VirtualKeyCode.UP);
                                            Thread.Sleep(2000); simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                            Thread.Sleep(2000); simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                            Thread.Sleep(2000); simu.Keyboard.KeyPress(VirtualKeyCode.RETURN);
                                            Thread.Sleep(2000); session.SwitchTo().Window(session.WindowHandles.First());
                                            //session.SwitchTo().ActiveElement(); Thread.Sleep(2000);
                                            session.FindElementByAccessibilityId("checkBoxIgnoreAll").Click();
                                            Thread.Sleep(2000);
                                            session.FindElementByAccessibilityId("buttonOk").Click();
                                        }
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                }

                                try
                                {
                                    //if (e.GetType().ToString() == "System.InvalidOperationException")
                                    //{
                                        var simu = new InputSimulator();
                                        simu.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_T);
                                        Thread.Sleep(2000);
                                        simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                        Thread.Sleep(2000);
                                        simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                        Thread.Sleep(2000);
                                        simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                        Thread.Sleep(2000);
                                        simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                        Thread.Sleep(2000);
                                        simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                        Thread.Sleep(2000);
                                        //simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                        //Thread.Sleep(2000);
                                        simu.Keyboard.KeyPress(VirtualKeyCode.UP);
                                        Thread.Sleep(2000); simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                        Thread.Sleep(2000); simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                        Thread.Sleep(2000); simu.Keyboard.KeyPress(VirtualKeyCode.RETURN);
                                        Thread.Sleep(2000); session.SwitchTo().Window(session.WindowHandles.First());
                                        //session.SwitchTo().ActiveElement(); Thread.Sleep(2000);
                                        session.FindElementByAccessibilityId("checkBoxIgnoreAll").Click();
                                        Thread.Sleep(2000);
                                        session.FindElementByAccessibilityId("buttonOk").Click();
                                    //}
                                }
                                catch (Exception ex)
                                {

                                }


                                session.SwitchTo().Window(session.WindowHandles.First());
                                ////session.SwitchTo().ActiveElement();

                                screenshot = CaptureScreenshot(session);
                                stepName.Log(Status.Info, "Device dump image is in process", MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
                                Thread.Sleep(320000);

                                try
                                {
                                    if (session.WindowHandles.Count() > 1)

                                    {
                                        session.SwitchTo().Window(session.WindowHandles.First());
                                        ////session.SwitchTo().ActiveElement();
                                        session.FindElementByAccessibilityId("buttonOk").Click();

                                        session.SwitchTo().Window(session.WindowHandles.First());
                                        Thread.Sleep(2000);
                                        session.FindElementByName("OK").Click();
                                    }
                                }
                                catch (Exception e)
                                {
                                    try
                                    {
                                        if (e.GetType().ToString() == "System.InvalidOperationException")
                                        {
                                            var simu = new InputSimulator();
                                            simu.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_T);
                                            Thread.Sleep(2000);
                                            simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                            Thread.Sleep(2000);
                                            simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                            Thread.Sleep(2000);
                                            simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                            Thread.Sleep(2000);
                                            simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                            Thread.Sleep(2000);
                                            simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                            Thread.Sleep(2000);
                                            Thread.Sleep(2000);
                                            simu.Keyboard.KeyPress(VirtualKeyCode.UP);
                                            Thread.Sleep(2000);
                                            simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                            Thread.Sleep(2000);
                                            simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                            Thread.Sleep(2000);
                                            simu.Keyboard.KeyPress(VirtualKeyCode.RETURN);
                                            Thread.Sleep(2000);
                                            session.SwitchTo().Window(session.WindowHandles.First());
                                            ////session.SwitchTo().ActiveElement();
                                            Thread.Sleep(2000);
                                            session.FindElementByAccessibilityId("buttonOk").Click();

                                            session.SwitchTo().Window(session.WindowHandles.First());
                                            Thread.Sleep(2000);
                                            session.FindElementByName("OK").Click();
                                        }
                                    }
                                    catch
                                    {

                                    }
                                }

                                try
                                {
                                    //if (e.GetType().ToString() == "System.InvalidOperationException")
                                    //{
                                        var simu = new InputSimulator();
                                        simu.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_T);
                                        Thread.Sleep(2000);
                                        simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                        Thread.Sleep(2000);
                                        simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                        Thread.Sleep(2000);
                                        simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                        Thread.Sleep(2000);
                                        simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                        Thread.Sleep(2000);
                                        simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                        Thread.Sleep(2000);
                                        Thread.Sleep(2000);
                                        simu.Keyboard.KeyPress(VirtualKeyCode.UP);
                                        Thread.Sleep(2000);
                                        simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                        Thread.Sleep(2000);
                                        simu.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                                        Thread.Sleep(2000);
                                        simu.Keyboard.KeyPress(VirtualKeyCode.RETURN);
                                        Thread.Sleep(2000);
                                        session.SwitchTo().Window(session.WindowHandles.First());
                                        ////session.SwitchTo().ActiveElement();
                                        Thread.Sleep(2000);
                                        session.FindElementByAccessibilityId("buttonOk").Click();

                                        session.SwitchTo().Window(session.WindowHandles.First());
                                        Thread.Sleep(2000);
                                        session.FindElementByName("OK").Click();
                                    //}
                                }
                                catch
                                {

                                }

                                session.SwitchTo().Window(session.WindowHandles.First());
                                stepName.Log(Status.Info, "Device dump image is taken successfully..");
                                Thread.Sleep(4000);
                                session.CloseApp();
                                Thread.Sleep(4000);
                                session.Quit();
                                session.Dispose();
                            }

                            /** To Connect the device(LT) to Stroragelayout viewr **/

                            if (DeviceType.Equals("Wired") || DeviceType.Equals("D1rechargeableWired"))
                            {


                                try
                                {

                                    if (session.FindElementByName("Channel").Displayed == true)
                                    {

                                        lib.waitUntilElementExists(session, "Channel", 0);
                                        session.FindElementByName("Channel").Click();
                                     
                                        var ext = session.FindElements(WorkFlowPageFactory.channel);
                                        //ext[0].Click();
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
                                                //action.MoveToElement(ext[0]).Build().Perform();
                                                //Thread.Sleep(2000);
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
                                                //action.MoveToElement(ext[0]).Build().Perform();
                                                //Thread.Sleep(2000);
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
                                        //ext[0].Click();
                                        session.FindElementByName("File").Click();
                                        session.Keyboard.PressKey(Keys.Enter);
                                        Thread.Sleep(2000);
                                        session.Keyboard.PressKey(Keys.ArrowDown);
                                        Thread.Sleep(2000);
                                        session.Keyboard.PressKey(Keys.ArrowDown);
                                        Thread.Sleep(2000);
                                        session.Keyboard.PressKey(Keys.Enter);

                                        /** selecting read option **/

                                        //ext = session.FindElements(WorkFlowPageFactory.readHI);
                                        //action = new Actions(session);
                                        //action.MoveToElement(ext[0]).Build().Perform();
                                        //Thread.Sleep(2000);
                                        //session.Keyboard.PressKey(Keys.Enter);
                                        Thread.Sleep(4000);

                                        /** selecting file menu and CheckNodes **/

                                        //ext = session.FindElements(WorkFlowPageFactory.fileMenu);
                                        //ext[0].Click();
                                        //Thread.Sleep(2000);
                                        //ext = session.FindElements(WorkFlowPageFactory.checkNodes);
                                        //action = new Actions(session);
                                        //action.MoveToElement(ext[0]).Build().Perform();
                                        //Thread.Sleep(2000);
                                        //session.Keyboard.PressKey(Keys.Enter);
                                        //Thread.Sleep(4000);

                                        ///** selecting dump option **/

                                        //ext = session.FindElements(WorkFlowPageFactory.fileMenu);
                                        //ext[0].Click();
                                        //Thread.Sleep(2000);
                                        //ext = session.FindElements(WorkFlowPageFactory.dumpP6HI);
                                        //action = new Actions(session);
                                        //action.MoveToElement(ext[0]).Build().Perform();
                                        //Thread.Sleep(2000);
                                        //session.Keyboard.PressKey(Keys.Enter);
                                        //Thread.Sleep(4000);

                                        session.FindElementByName("File").Click();
                                        session.Keyboard.PressKey(Keys.Enter);
                                        Thread.Sleep(2000);
                                        session.Keyboard.PressKey(Keys.ArrowDown);
                                        Thread.Sleep(2000);
                                        session.Keyboard.PressKey(Keys.ArrowDown);
                                        Thread.Sleep(2000);
                                        session.Keyboard.PressKey(Keys.ArrowDown);
                                        Thread.Sleep(2000);
                                        session.Keyboard.PressKey(Keys.ArrowDown);
                                        Thread.Sleep(2000);
                                        session.Keyboard.PressKey(Keys.ArrowDown);
                                        Thread.Sleep(2000);
                                        session.Keyboard.PressKey(Keys.Enter);
                                        Thread.Sleep(2000);
                                        //session.SwitchTo().Window(session.WindowHandles.First());

                                        //session.FindElementByName("File name:").SendKeys("C:\\" + fileName + ".xml");
                                        string editFile = "//*[@ClassName='Edit']";
                                        WindowsElement fileNameEdit = session.FindElement(By.XPath(editFile));
                                        fileNameEdit.SendKeys("C:\\" + fileName + ".xml");
                                        Thread.Sleep(4000);

                                        /** To save the Dump in Xml file **/

                                        session.FindElementByName("Save").Click();
                                        Thread.Sleep(4000);
                                        session.SwitchTo().Window(session.WindowHandles.First());
                                        //session.SwitchTo().ActiveElement();

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
                                    Thread.Sleep(8000);
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
                                    //var ext = session.FindElements(WorkFlowPageFactory.fileMenu);
                                    //ext[0].Click();
                                    var read = "//*[@Name='_Read from']";
                                    session.FindElement(By.XPath(read)).Click();
                                    Thread.Sleep(2000);
                                    //ext = session.FindElements(WorkFlowPageFactory.readHI);
                                    Actions actions = new Actions(session);
                                    //actions.MoveToElement(ext[0]).Build().Perform();
                                    Thread.Sleep(2000);
                                    //session.Keyboard.PressKey(Keys.Enter);
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
                                    //ext = session.FindElements(WorkFlowPageFactory.fileMenu);
                                    //ext[0].Click();
                                    string file = "//*[@Name='File']";
                                    var dropFile = session.FindElement(By.XPath(file));
                                    dropFile.Click();

                                    //ext = session.FindElements(WorkFlowPageFactory.fileMenu);
                                    //ext[0].Click();
                                    Thread.Sleep(2000);
                                    //ext = session.FindElements(WorkFlowPageFactory.dumpHI);
                                    Actions act = new Actions(session);
                                    //act.MoveToElement(ext[0]).Build().Perform();
                                    string Savefile = "//*[@Name='Save as CDI file']";
                                    var xPtahOfFile = session.FindElement(By.XPath(Savefile));
                                    act.MoveToElement(xPtahOfFile).Click().Perform();
                                    //ext = session.FindElements(WorkFlowPageFactory.dumpHI);
                                    //actions = new Actions(session);
                                    //actions.MoveToElement(ext[0]).Build().Perform();
                                    Thread.Sleep(2000);
                                    //session.Keyboard.PressKey(Keys.Enter);
                                    Thread.Sleep(4000);
                                    session.FindElementByClassName("Edit").SendKeys("C:\\" + fileName + ".xml");
                                    Thread.Sleep(4000);

                                    /** To save the Dump in Xml file **/

                                    session.FindElementByName("Save").Click();
                                    Thread.Sleep(4000);
                                    session.SwitchTo().Window(session.WindowHandles.First());
                                    ////session.SwitchTo().ActiveElement();
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
                                                ////session.SwitchTo().ActiveElement();
                                                session.FindElementByAccessibilityId("checkBoxIgnoreAll").Click();
                                                Thread.Sleep(2000);
                                                session.FindElementByAccessibilityId("buttonOk").Click();



                                            }
                                        } while (session.FindElementByAccessibilityId("WorkerDialog").Enabled);
                                    }
                                    catch (Exception ex) { }

                                    Thread.Sleep(20000);

                                    /**This is to handle child windows whlie saving CDI**/

                                    session.SwitchTo().Window(session.WindowHandles.First());
                                    ////session.SwitchTo().ActiveElement();

                                    /** To click the Ok button in the flow of Dump saving **/
                                    screenshot = CaptureScreenshot(session);

                                    stepName.Log(Status.Info, "Device dump image is in process", MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());

                                    try
                                    {

                                        do
                                        {

                                            if (session.WindowHandles.Count() > 1)

                                            {
                                                session.SwitchTo().Window(session.WindowHandles.First());
                                                ////session.SwitchTo().ActiveElement();
                                                session.FindElementByAccessibilityId("buttonOk").Click();

                                                session.SwitchTo().Window(session.WindowHandles.First());
                                                Thread.Sleep(2000);
                                                session.FindElementByName("OK").Click();
                                            }


                                        } while (session.FindElementByAccessibilityId("WorkerDialog").Enabled);
                                    }

                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.ToString());
                                    }

                                }

                                screenshot = ModuleFunctions.CaptureScreenshot(session);

                                stepName.Log(Status.Info, "Device dump image process done successfully..", MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());

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
            FunctionLibrary lib = new FunctionLibrary();
            InputSimulator sim = new InputSimulator();
            string computer_name = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
            string storageLayOutDate = "WindowsForms10.Window.8.app.0.2804c64_r9_ad1";
            Thread.Sleep(8000);
            Actions actions = new Actions(session);

            /** Interface selection drop down **/
            //session.FindElement(By.Name("File")).Click();
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

            //lib.waitUntilElementExists(session, "File", 0);
            //Thread.Sleep(4000);
            //var ext = session.FindElements(WorkFlowPageFactory.fileMenu);
            //ext[0].Click();
            //Thread.Sleep(2000);
            //ext = session.FindElements(WorkFlowPageFactory.readHI);
            //actions = new Actions(session);
            //actions.MoveToElement(ext[0]).Build().Perform();
            //Thread.Sleep(2000);
            //session.Keyboard.PressKey(Keys.Enter);
            //Thread.Sleep(5000);

            ///** Click on Uncheck button **/

            //session.FindElementByName("Uncheck All").Click();
            //Thread.Sleep(3000);

            //session.FindElementByAccessibilityId("1001").Click();
            //Thread.Sleep(2000);

            ///** Choose the All option in drop down **/

            //var rd = session.FindElementByName("All");
            //actions.MoveToElement(rd).Click().Perform();
            //Thread.Sleep(2000);

            var read = "//*[@Name='_Read from']";
            var readFrom = session.FindElement(By.XPath(read));
            actions.MoveToElement(readFrom).Click().Perform();
            Thread.Sleep(10000);

            stepName.Log(Status.Info, "Device connected successfully..");
            /** Click on Uncheck button **/

            session.FindElementByName("Uncheck All").Click();
            Thread.Sleep(3000);
            session.FindElementByAccessibilityId("1001").Click();
            Thread.Sleep(4000);

            /** Choose the All option in drop down **/

            // Find the element by XPath

            var all = "//*[@Name='All']";
            var allClick = session.FindElement(By.XPath(all));
            Thread.Sleep(4000);
            Actions actions1 = new Actions(session);
            actions1.MoveToElement(allClick).Click().Perform();



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
            ExtentTest stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
            do
            {
                if (device.Contains("RT") || device.Contains("RU") || device.Contains("NX"))

                {
                    session.FindElementByName("Stop").Click();
                    Thread.Sleep(3000);
                    session.SwitchTo().Window(session.WindowHandles[0]);
                    session.FindElementByName("Shutdown").Click();
                    Thread.Sleep(3000);
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

                session = ModuleFunctions.sessionInitialize1("C:\\Program Files (x86)\\GN Hearing\\Camelot\\WorkflowRuntime\\Camelot.WorkflowRuntime.exe", "C:\\Program Files (x86)\\GN Hearing\\Camelot\\WorkflowRuntime");
                string ApplicationPath = "C:\\Program Files (x86)\\GN Hearing\\Camelot\\WorkflowRuntime\\Camelot.WorkflowRuntime.exe";
                Thread.Sleep(2000);
                AppiumOptions appCapabilities = new AppiumOptions();
                appCapabilities.AddAdditionalCapability("app", ApplicationPath);
                appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
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
                    //session.SwitchTo().ActiveElement();



                    /** Entering the Serial number **/

                    session.FindElementByAccessibilityId("textBoxSN").SendKeys(DeviceNo);
                    session.FindElementByName("Continue >>").Click();

                }
                Thread.Sleep(30000);
                session.SwitchTo().Window(session.WindowHandles.First());
                //session.SwitchTo().ActiveElement();

                try
                {
                    if (session.FindElementByName("Specify Parameters").Displayed)
                    {
                        session.FindElementByName("Continue >>").Click();
                    }
                }
                catch (Exception)
                {
                    //session.FindElementByName("Continue >>").Click();
                }

            } while (session.FindElementByAccessibilityId("MessageForm").Displayed);

        }


        public static void Recovery(WindowsDriver<WindowsElement> session, ExtentTest stepname, string DeviceType, string DeviceNo, string side)
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

            session = sessionInitialize(config.ApplicationPath.SandRAppPath, config.workingdirectory.SandR);
            stepname.Log(Status.Pass, "S&R Tool launched successfully");
            lib.waitUntilElementExists(session, "Device Info", 0);
            session.FindElementByName("Device Info").Click();

            if (DeviceType.Equals("Non-Rechargeable") || DeviceType.Equals("Rechargeable"))
            {
                session.FindElementByName("Discover").Click();
                stepname.Log(Status.Pass, "Clicked on Discover.");
                session.SwitchTo().Window(session.WindowHandles.First());
                //session.SwitchTo().ActiveElement();


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

        private static void UpdateAppSettings(string filePath, string key, string value)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            XmlNode node = xmlDoc.SelectSingleNode($"//appSettings/add[@key='{key}']");
            if (node != null)
            {
                node.Attributes["value"].Value = value;
            }
            xmlDoc.Save(filePath);
        }

        private static void UpdateNLogMinLevel(string filePath, string minLevel)
        {
            XDocument xmlDoc = XDocument.Load(filePath);
            XNamespace nlogNamespace = "http://www.nlog-project.org/schemas/NLog.xsd";
            XElement rulesElement = xmlDoc.Descendants(nlogNamespace + "rules").FirstOrDefault();
            if (rulesElement != null)
            {
                var loggerElements = rulesElement.Elements(nlogNamespace + "logger");
                foreach (var loggerElement in loggerElements)
                {
                    XAttribute minlevelAttribute = loggerElement.Attribute("minlevel");
                    if (minlevelAttribute != null)
                    {
                        minlevelAttribute.Value = minLevel;
                    }
                }
            }
            xmlDoc.Save(filePath);
        }

        public static void SandRenvironmentchange()
        {
            var appConfigFiles = new[]
            {
      @"C:\Program Files (x86)\GN Hearing\Lucan\App\Lucan.App.UI.exe.config",
      @"C:\Program Files (x86)\GN Hearing\Lucan\Settings\Lucan.SettingsRestoration.Runtime.exe.config",
      @"C:\Program Files (x86)\GN Hearing\Lucan.SettingsRestoration\SettingsRestoration\Lucan.SettingsRestoration.Runtime.exe.config",
      @"C:\Program Files (x86)\GN Hearing\Avalon\Device.DeviceInfos\Avalon.DeviceInfos.Runtime.exe.config",
      @"C:\Program Files (x86)\GN Hearing\Avalon.Lucan\DeviceInfos\Avalon.DeviceInfos.Runtime.exe.config"
  };

            var nlogConfigFiles = new[]
            {
      @"C:\Program Files (x86)\GN Hearing\Lucan\App\Nlog.config",
      @"C:\Program Files (x86)\GN Hearing\Lucan\Settings\Nlog.config",
      @"C:\Program Files (x86)\GN Hearing\Lucan.SettingsRestoration\SettingsRestoration\Nlog.config",
      @"C:\Program Files (x86)\GN Hearing\Avalon\Device.DeviceInfos\Nlog.config",
      @"C:\Program Files (x86)\GN Hearing\Avalon.Lucan\DeviceInfos\Nlog.config"
  };

            foreach (var filePath in appConfigFiles)
            {
                if (filePath == @"C:\Program Files (x86)\GN Hearing\Lucan\App\Lucan.App.UI.exe.config")
                {
                    UpdateAppSettings(filePath, "LocalSite", "TEST");
                }
                else
                {
                    UpdateAppSettings(filePath, "CloudEnvironment", "Verification");
                    UpdateAppSettings(filePath, "CloudLocation", "DevWestEurope");
                }
            }

            foreach (var filePath in nlogConfigFiles)
            {
                UpdateNLogMinLevel(filePath, "Trace");
            }

            Console.WriteLine("XML files updated successfully.");
        }

        public static void UninstallSandRTool(ExtentTest stepName)
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(config.sandRToolUninstallation.UninstallKeyWow64))
            {
                if (key == null)
                {
                    stepName.Log(Status.Fail, "Uninstall Registry key not found");
                    return;
                }

                var targetSubKey = key.GetSubKeyNames()
                                 .Select(subKeyName => key.OpenSubKey(subKeyName))
                                 .Where(appKey => appKey != null)
                                 .Select(appKey => new { appKey, displayName = appKey.GetValue("DisplayName") as string })
                                 .Where(x => !string.IsNullOrEmpty(x.displayName) && x.displayName.Contains($"Service & Repair Tool"))
                                 .ToList();

                if (!targetSubKey.Any())
                {
                    stepName.Log(Status.Fail, "Service & Repair Tool not found");
                    return;
                }

                foreach (var e in targetSubKey)
                {
                    if (e.displayName.Contains("Beta"))
                    {
                        string uninstallString = e.appKey.GetValue("UninstallString") as string;
                        if (string.IsNullOrEmpty(uninstallString))
                        {
                            stepName.Log(Status.Fail, $"UninstallString not found for {e.displayName}");
                            continue;
                        }
                        string trimmedUninstallString = uninstallString?.TrimEnd(" /uninstall".ToCharArray());
                        if (!string.IsNullOrEmpty(trimmedUninstallString))
                        {
                            try
                            {
                                // Create a process object
                                using (Process uninstallProcess = new Process())
                                {
                                    // Configure the process using StartInfo properties
                                    uninstallProcess.StartInfo.FileName = trimmedUninstallString;
                                    uninstallProcess.StartInfo.Verb = "runas";
                                    uninstallProcess.StartInfo.Arguments = "/uninstall /quiet"; // Adjust arguments as needed
                                    uninstallProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden; // Optional: hide the uninstall window
                                    uninstallProcess.StartInfo.UseShellExecute = false; // Ensure no shell is used for better control

                                    // Start the process
                                    uninstallProcess.Start();
                                    uninstallProcess.WaitForExit(); // Wait for the uninstallation to complete

                                    // Check the exit code if needed
                                    int exitCode = uninstallProcess.ExitCode;
                                    Console.WriteLine($"Uninstallation exit code: {exitCode}");
                                }

                                var displayNames = key.GetSubKeyNames()
                                     .Select(subKeyName => key.OpenSubKey(subKeyName))
                                     .Where(appKey => appKey != null)
                                     .Select(appKey => new { appKey, displayName = appKey.GetValue("DisplayName") as string })
                                     .Where(x => !string.IsNullOrEmpty(x.displayName) && x.displayName.Contains($"Service & Repair Tool"))
                                     .ToList();

                                if (displayNames.Any(app => app.displayName.Contains("Beta")))
                                {
                                    stepName.Log(Status.Fail, "Serivce & Repair Tool Beta version was not uninstalled");
                                }
                                else
                                {
                                    stepName.Log(Status.Pass, "Service & Repair Tool Beta version sucessfully uninstalled");
                                }
                            }
                            catch (Exception ex)
                            {
                                stepName.Log(Status.Fail, $"Error during uninstallation : {ex.Message}");
                            }
                        }
                    }
                }
            }
        }

        public static void InstallSandRTool(ExtentTest stepName)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(config.sandRToolUninstallation.UninstallKeyWow64);
            if (key == null)
            {
                stepName.Log(Status.Fail, "Uninstall registry key not found.");
                return;
            }

            var targetSubKey = key.GetSubKeyNames()
                                 .Select(subKeyName => key.OpenSubKey(subKeyName))
                                 .Where(appKey => appKey != null)
                                 .Select(appKey => new { appKey, displayName = appKey.GetValue("DisplayName") as string })
                                 .Where(x => !string.IsNullOrEmpty(x.displayName) && x.displayName.Contains($"Service & Repair Tool"))
                                 .ToList();

            if (targetSubKey.Any())
            {
                foreach (var item in targetSubKey)
                {
                    if (item.displayName.Contains("Beta"))
                    {
                        UninstallSandRTool(stepName);
                        break;
                    }
                }
            }

            // Path to your .exe file
            string filePath = $"C:\\Users\\iray3\\Downloads\\S&R Tool {config.sandRDownloadLinkUpdateParameters.Build} (Beta {config.sandRDownloadLinkUpdateParameters.Beta}).zip\\S&R Tool Setup.exe";
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = filePath,
                    Arguments = "/quiet",
                    Verb = "runas", // This prompts for administrative access
                    UseShellExecute = true
                };

                Process process = new Process
                {
                    StartInfo = startInfo
                };

                process.Start();
                process.WaitForExit(); // Optional: Wait for the process to exit if needed

                var displayNames = key.GetSubKeyNames()
                                  .Select(subKeyName => key.OpenSubKey(subKeyName))
                                  .Where(appKey => appKey != null)
                                  .Select(appKey => new { appKey, displayName = appKey.GetValue("DisplayName") as string })
                                  .Where(x => !string.IsNullOrEmpty(x.displayName) && x.displayName.Contains($"Service & Repair Tool {config.sandRDownloadLinkUpdateParameters.Build} (Beta {config.sandRDownloadLinkUpdateParameters.Beta})"))
                                  .ToList();

                if (displayNames.Any())
                {
                    foreach (var item in displayNames)
                    {
                        if (item.displayName == $"Service & Repair Tool {config.sandRDownloadLinkUpdateParameters.Build} (Beta {config.sandRDownloadLinkUpdateParameters.Beta})")
                        {
                            stepName.Log(Status.Pass, $"Service & Repair Tool {config.sandRDownloadLinkUpdateParameters.Build} (Beta {config.sandRDownloadLinkUpdateParameters.Beta}) Installed successfully");
                            //item.appKey.Close();  // Close the appKey after processing
                        }
                        else
                        {
                            stepName.Log(Status.Info, $"Service & Repair Tool {config.sandRDownloadLinkUpdateParameters.Build} (Beta {config.sandRDownloadLinkUpdateParameters.Beta}) is not Installed");
                            //item.appKey.Close();  // Close the appKey after processing
                        }

                    }
                }
                else
                {
                    stepName.Log(Status.Fail, $"Service & Repair Tool {config.sandRDownloadLinkUpdateParameters.Build} (Beta {config.sandRDownloadLinkUpdateParameters.Beta}) is not Installed");
                }
            }
            catch (Exception ex)
            {
                stepName.Log(Status.Fail, $"Error: {ex.Message}");
            }
        }
        public static void InstallSandRC4Extension(ExtentTest stepName)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(config.sandRToolUninstallation.UninstallKeyWow64);
            if (key == null)
            {
                stepName.Log(Status.Fail, "Uninstall registry key not found.");
                return;
            }

            var targetSubKey = key.GetSubKeyNames()
                                 .Select(subKeyName => key.OpenSubKey(subKeyName))
                                 .Where(appKey => appKey != null)
                                 .Select(appKey => new { appKey, displayName = appKey.GetValue("DisplayName") as string })
                                 .Where(x => !string.IsNullOrEmpty(x.displayName) && x.displayName.Contains($"Service & Repair Tool"))
                                 .ToList();

            if (targetSubKey.Any())
            {
                foreach (var item in targetSubKey)
                {
                    if (item.displayName.Contains("C4 Extension"))
                    {
                        UninstallSandRC4Extension(stepName);
                        break;
                    }
                }
            }

            // Path to your .exe file
            string filePath = $"C:\\Users\\iray3\\Downloads\\S&R Tool C4 Extension Pack 1.2.zip\\S&R Tool C4 Extension Pack.exe";
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = filePath,
                    Arguments = "/quiet",
                    Verb = "runas", // This prompts for administrative access
                    UseShellExecute = true
                };

                Process process = new Process
                {
                    StartInfo = startInfo
                };

                process.Start();
                process.WaitForExit(); // Optional: Wait for the process to exit if needed

                var displayNames = key.GetSubKeyNames()
                                  .Select(subKeyName => key.OpenSubKey(subKeyName))
                                  .Where(appKey => appKey != null)
                                  .Select(appKey => new { appKey, displayName = appKey.GetValue("DisplayName") as string })
                                  .Where(x => !string.IsNullOrEmpty(x.displayName) && x.displayName.Contains($"Service & Repair Tool C4 Extension"))
                                  .ToList();

                if (displayNames.Any())
                {
                    foreach (var item in displayNames)
                    {
                        if (item.displayName == $"Service & Repair Tool C4 Extension Pack 1.2")
                        {
                            stepName.Log(Status.Pass, $"Service & Repair Tool C4 Extension Installed successfully");
                            //item.appKey.Close();  // Close the appKey after processing
                        }
                        else
                        {
                            stepName.Log(Status.Info, $"Service & Repair Tool C4 Extension is not Installed");
                            //item.appKey.Close();  // Close the appKey after processing
                        }

                    }
                }
                else
                {
                    stepName.Log(Status.Fail, $"Service & Repair Tool C4 Extension is not Installed");
                }
            }
            catch (Exception ex)
            {
                stepName.Log(Status.Fail, $"Error: {ex.Message}");
            }
        }

        public static void UninstallSandRC4Extension(ExtentTest stepName)
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(config.sandRToolUninstallation.UninstallKeyWow64))
            {
                if (key == null)
                {
                    stepName.Log(Status.Fail, "Uninstall Registry key not found");
                    return;
                }

                var targetSubKey = key.GetSubKeyNames()
                                 .Select(subKeyName => key.OpenSubKey(subKeyName))
                                 .Where(appKey => appKey != null)
                                 .Select(appKey => new { appKey, displayName = appKey.GetValue("DisplayName") as string })
                                 .Where(x => !string.IsNullOrEmpty(x.displayName) && x.displayName.Contains($"Service & Repair Tool"))
                                 .ToList();

                if (!targetSubKey.Any())
                {
                    stepName.Log(Status.Fail, "Service & Repair Tool not found");
                    return;
                }

                foreach (var e in targetSubKey)
                {
                    if (e.displayName.Contains("C4 Extension"))
                    {
                        string uninstallString = e.appKey.GetValue("UninstallString") as string;
                        if (string.IsNullOrEmpty(uninstallString))
                        {
                            stepName.Log(Status.Fail, $"UninstallString not found for {e.displayName}");
                            continue;
                        }
                        string trimmedUninstallString = uninstallString?.TrimEnd(" /uninstall".ToCharArray());
                        if (!string.IsNullOrEmpty(trimmedUninstallString))
                        {
                            try
                            {
                                // Create a process object
                                using (Process uninstallProcess = new Process())
                                {
                                    // Configure the process using StartInfo properties
                                    uninstallProcess.StartInfo.FileName = trimmedUninstallString;
                                    uninstallProcess.StartInfo.Verb = "runas";
                                    uninstallProcess.StartInfo.Arguments = "/uninstall /quiet"; // Adjust arguments as needed
                                    uninstallProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden; // Optional: hide the uninstall window
                                    uninstallProcess.StartInfo.UseShellExecute = false; // Ensure no shell is used for better control

                                    // Start the process
                                    uninstallProcess.Start();
                                    uninstallProcess.WaitForExit(); // Wait for the uninstallation to complete

                                    // Check the exit code if needed
                                    int exitCode = uninstallProcess.ExitCode;
                                    Console.WriteLine($"Uninstallation exit code: {exitCode}");
                                }

                                var displayNames = key.GetSubKeyNames()
                                     .Select(subKeyName => key.OpenSubKey(subKeyName))
                                     .Where(appKey => appKey != null)
                                     .Select(appKey => new { appKey, displayName = appKey.GetValue("DisplayName") as string })
                                     .Where(x => !string.IsNullOrEmpty(x.displayName) && x.displayName.Contains($"Service & Repair Tool"))
                                     .ToList();

                                if (displayNames.Any(app => app.displayName.Contains("C4 Extension")))
                                {
                                    stepName.Log(Status.Fail, "Serivce & Repair Tool C4 Extension was not uninstalled");
                                }
                                else
                                {
                                    stepName.Log(Status.Pass, "Service & Repair Tool C4 Extension sucessfully uninstalled");
                                }
                            }
                            catch (Exception ex)
                            {
                                stepName.Log(Status.Fail, $"Error during uninstallation : {ex.Message}");
                            }
                        }
                    }
                }
            }
        }

    } /**End of ModuleFunctions*/

} //End of name space