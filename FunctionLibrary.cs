using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using System.Diagnostics;
using System.Threading;
using OfficeOpenXml;
using System.IO;
using OpenQA.Selenium.Support.UI;
using com.sun.xml.@internal.bind.v2.model.core;
using System.Linq;
using jdk.nashorn.@internal.ir;
using WindowsInput.Native;
using WindowsInput;
using AventStack.ExtentReports;
using System.Xml;
using OpenQA.Selenium.Appium;
using AventStack.ExtentReports.Model;
using Microsoft.Office.Interop.Excel;
using Range = Microsoft.Office.Interop.Excel.Range;
using System.Collections.ObjectModel;
using TechTalk.SpecFlow;
using NUnit.Framework.Internal;
using Castle.Core.Internal;
using System.IO.Compression;
using Xamarin.Forms.Internals;
using com.sun.xml.@internal.ws.api.pipe;

namespace AppiumWinApp
{

    internal class FunctionLibrary
    {
        string[] strArray;
        string[] strArrayVal;
        private static ExtentTest test;
        public static String textDir = Directory.GetCurrentDirectory();

        string testPlanId = "1633245";
        string testSuiteId = "1633281";
        string testConfig = "GOP:Dooku2_BTE_RHI(C70)";

        private List<string> xmlFilePaths = new List<string>
        {
                 Path.Combine(Directory.GetCurrentDirectory(), @"XML\1537268.xml"),
                 Path.Combine(Directory.GetCurrentDirectory(), @"XML\1103972.xml"),
                 Path.Combine(Directory.GetCurrentDirectory(), @"XML\1105474.xml"),
                 Path.Combine(Directory.GetCurrentDirectory(), @"XML\1103482.xml"),
                 Path.Combine(Directory.GetCurrentDirectory(), @"XML\1103833.xml"),
                 Path.Combine(Directory.GetCurrentDirectory(), @"XML\1104002.xml"),
                 Path.Combine(Directory.GetCurrentDirectory(), @"XML\1142328.xml"),
                 Path.Combine(Directory.GetCurrentDirectory(), @"XML\1103981.xml"),
                 Path.Combine(Directory.GetCurrentDirectory(), @"XML\1105498.xml"),
                 Path.Combine(Directory.GetCurrentDirectory(), @"XML\1105696.xml"),
                 Path.Combine(Directory.GetCurrentDirectory(), @"XML\1105669.xml"),
                 Path.Combine(Directory.GetCurrentDirectory(), @"XML\1629628.xml"),
                 Path.Combine(Directory.GetCurrentDirectory(), @"XML\1629629.xml"),
        };

        /** Click operations by using Automation id's and names **/
        public void clickOnAutomationId(WindowsDriver<WindowsElement> session, string name, string id)
        {
            var wait = new WebDriverWait(session, TimeSpan.FromSeconds(20));
            var div = wait.Until(ExpectedConditions.ElementIsVisible(By.Name(name)));
            session.FindElementByAccessibilityId(id).Click();
        }

        /** Click operations by using Automation id's **/
        public void clickOnElementWithIdonly(WindowsDriver<WindowsElement> session, string id)
        {
            WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(80));
            var txtLocation = session.FindElementByAccessibilityId(id);
            waitForMe.Until(session => txtLocation.Enabled);
            session.FindElementByAccessibilityId(id).Click();
        }

        /* Click by using Name locator */

        public void clickOnAutomationName(WindowsDriver<WindowsElement> session, string name)
        {
            WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(50));
            var txtLocation = session.FindElementByName(name);
            waitForMe.Until(session => txtLocation.Displayed);
            session.FindElementByName(name).Click();
        }


        /** **/
        public WindowsDriver<WindowsElement> funtionRecurse(WindowsDriver<WindowsElement> session, string name)
        {

            try
            {
                InputSimulator sim = new InputSimulator();
                WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(70));
                var txtLocation = session.FindElementByAccessibilityId(name);
                waitForMe.Until(session => txtLocation.Displayed);
                Thread.Sleep(10000);

                sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            }
            catch (Exception e)
            {
                funtionRecurse(session, name);

            }

            Thread.Sleep(8000);
            return session;
        }

        /** Wait for the element name is enable **/
        public WindowsDriver<WindowsElement> functionWaitForName(WindowsDriver<WindowsElement> session, string name)
        {

            do
            {

                try
                {
                    WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(50));
                    var txtLocation = session.FindElementByName(name);
                    waitForMe.Until(session => txtLocation.Enabled);
                    session.FindElementByName(name).Click();
                    return session;
                }

                catch (Exception e)
                {
                    functionWaitForName(session, name);
                }
                break;

            } while (!(session.FindElementByName(name).Enabled));

            return session;
        }


        /** Wait for the Id is enable **/
        public WindowsDriver<WindowsElement> functionWaitForId(WindowsDriver<WindowsElement> session, string name)
        {
            do
            {
                try
                {
                    Thread.Sleep(8000);
                    WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(50));
                    var txtLocation = session.FindElementByAccessibilityId(name);
                    waitForMe.Until(session => txtLocation.Enabled);
                    session.FindElementByAccessibilityId(name).Click();
                    return session;
                }

                catch (Exception e)
                {
                    functionWaitForId(session, name);
                }
                break;

            } while (!(session.FindElementByAccessibilityId(name).Enabled));

            return session;
        }
        /** Wait for the duplicate Id is enable **/
        public WindowsDriver<WindowsElement> functionWaitForIdduplicate(WindowsDriver<WindowsElement> session, string name)
        {

            try
            {

                do
                {
                    session.SwitchTo().Window(session.WindowHandles.First());
                    Thread.Sleep(8000);
                } while (!(session.FindElementByAccessibilityId(name).Enabled));
            }

            catch (Exception e)
            {
                functionWaitForIdduplicate(session, name);
            }

            return session;
        }

        /** Wait for the element name is enable **/
        public WindowsDriver<WindowsElement> waitForElement(WindowsDriver<WindowsElement> session, string name)
        {

            do
            {
                try
                {
                    WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(50));
                    Thread.Sleep(4000);
                    var txtLocation = session.FindElementByName(name);
                    waitForMe.Until(session => txtLocation.Enabled);
                    session.FindElementByName(name).Click();
                    return session;
                }

                catch (Exception e)
                {
                    waitForElement(session, name);
                }
                break;

            } while (!(session.FindElementByName(name).Enabled));

            return session;
        }

        /** Wait for the Clickable element is enable **/
        public WindowsDriver<WindowsElement> waitForElementToBeClickable(WindowsDriver<WindowsElement> session, string name)
        {
            WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(400));
            var sts = waitForMe.Until(ExpectedConditions.ElementToBeClickable(By.Name(name)));
            session.FindElementByName(name).Click();
            return session;
        }
        /** Wait for the Clickable Id is enable **/
        public WindowsDriver<WindowsElement> waitForIdToBeClickable(WindowsDriver<WindowsElement> session, string name)
        {
            WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(400));
            var sts = waitForMe.Until(ExpectedConditions.ElementToBeClickable(MobileBy.AccessibilityId(name)));
            session.FindElementByAccessibilityId(name).Click();
            return session;
        }

        /** Wait until the existable element is display **/
        public WindowsDriver<WindowsElement> waitUntilElementExists(WindowsDriver<WindowsElement> session, string name, int id)
        {
            switch (id)
            {
                case 1:

                    do
                    {

                        try
                        {
                            WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(90));
                            Thread.Sleep(8000);
                            var txtLocation = session.FindElementByAccessibilityId(name);
                            waitForMe.Until(session => txtLocation.Displayed);
                            return session;
                        }

                        catch (Exception e)
                        {
                            waitUntilElementExists(session, name, id);
                        }
                        break;

                    } while (!(session.FindElementByAccessibilityId(name).Displayed));

                    break;
                case 0:

                    do
                    {

                        try
                        {
                            WebDriverWait waitForMe = new WebDriverWait(session, TimeSpan.FromSeconds(50));
                            Thread.Sleep(8000);
                            var txtLocation = session.FindElementByName(name);
                            waitForMe.Until(session => txtLocation.Displayed);
                            return session;
                        }

                        catch (Exception e)
                        {
                            waitUntilElementExists(session, name, id);
                        }
                        break;

                    } while (!(session.FindElementByName(name).Displayed));

                    break;
            }

            return session;
        }

        /** To click on the Close texts in Testwork flow **/
        public static WindowsDriver<WindowsElement> clickOnCloseTestFlow(WindowsDriver<WindowsElement> session, string windowName)
        {

            DesiredCapabilities desktopCapabilities = new DesiredCapabilities();
            desktopCapabilities.SetCapability("platformName", "Windows");
            desktopCapabilities.SetCapability("app", "Root");
            desktopCapabilities.SetCapability("deviceName", "WindowsPC");
            session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), desktopCapabilities);
            WindowsElement applicationWindow = null;
            Thread.Sleep(6000);
            var openWindows = session.FindElementsByClassName("Window");


            if (windowName == "Program Selection")
            {
                openWindows = session.FindElementsByName("Program Selection");
                Thread.Sleep(2000);
            }
            else
            {
                openWindows = session.FindElementsByClassName("Window");
                Thread.Sleep(2000);
            }


            foreach (var window in openWindows)
            {
                string txt = window.GetAttribute("Name");
                Thread.Sleep(4000);

                if (window.GetAttribute("Name").StartsWith(windowName))
                {
                    applicationWindow = window;
                    Thread.Sleep(2000);

                    break;
                }
            }

            Thread.Sleep(4000);

            // Attaching to existing Application Window

            string topLevelWindowHandle = null;

            try
            {
                topLevelWindowHandle = applicationWindow.GetAttribute("NativeWindowHandle");
            }

            catch (Exception e)
            {
                topLevelWindowHandle = applicationWindow.GetAttribute("NativeWindowHandle");
            }

            topLevelWindowHandle = int.Parse(topLevelWindowHandle).ToString("X");

            DesiredCapabilities capabilities = new DesiredCapabilities();
            capabilities.SetCapability("deviceName", "WindowsPC");
            capabilities.SetCapability("appTopLevelWindow", topLevelWindowHandle);
            session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), capabilities);


            return session;

        }

        /**To kill the opeartions **/
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



        /** To verify the files If file is there tehn select last elements in log file and verify the Capture & Restore time **/
        public void fileVerify(string path, ExtentTest stepName, string operationVar)
        {
            if (File.Exists(path))
            {
                string last = File.ReadLines(path).Last();
                Console.WriteLine(last);
                String str = last;
                var result = str.LastIndexOf(':');
                int lastDotIndex = str.LastIndexOf(":", System.StringComparison.Ordinal);
                string firstPart = str.Remove(lastDotIndex);
                string secondPart = str.Substring(lastDotIndex + 1, str.Length - firstPart.Length - 1);
                int timeTaken = 0;



                if (result != null)
                {
                    try
                    {
                        timeTaken = Convert.ToInt32(secondPart.Replace(" ", "").Replace("|", ""));
                    }
                    catch (Exception ex)
                    {

                        string[] lines = File.ReadAllLines(path);

                        foreach (string line in lines)
                        {
                            Console.WriteLine(line);

                            if (line.Contains(operationVar))
                            {
                                str = line;
                                result = str.LastIndexOf(':');
                                lastDotIndex = str.LastIndexOf(":", System.StringComparison.Ordinal);
                                firstPart = str.Remove(lastDotIndex);
                                secondPart = str.Substring(lastDotIndex + 1, str.Length - firstPart.Length - 1);
                                timeTaken = 0;
                                timeTaken = Convert.ToInt32(secondPart.Replace(" ", "").Replace("|", ""));

                            }
                        }
                    }


                    if (timeTaken < 41000)
                    {
                        Console.WriteLine(operationVar + " Operation performed in desired time");
                         stepName.Pass(operationVar + " Desired Time : " + timeTaken);
                    }
                    else
                    {
                        Console.WriteLine("Failed");
                        stepName.Fail(operationVar + " Desired Time : " + timeTaken);
                    }
                }
            }

            else
            {
                Console.WriteLine("file not found");
            }

        }

        public void LogElementStatus(ExtentTest test, string elementName, string textBoxValue)
        {
            if (string.IsNullOrEmpty(textBoxValue))
            {
                test.Log(Status.Fail, $"{elementName} is displayed as => {textBoxValue}");
            }
            else if (textBoxValue.Contains("?"))
            {
                test.Log(Status.Info, $"{elementName} is displayed as => {textBoxValue}");
            }
            else 
            {
                test.Log(Status.Pass, $"{elementName} is displayed as => {textBoxValue}");
            }
            
        }

        /** To get the Device information into the Excel sheet **/

        public void getDeviceInfo(WindowsDriver<WindowsElement> session,ExtentTest test,string deviceType)
        {
            session.FindElementByName("Device Info").Click();
            Thread.Sleep(2000);
            var textElements = session.FindElementsByClassName("Text");
            var textboxElements = session.FindElementsByClassName("TextBox");
            var textValues = textElements.Select(e => e.Text).ToArray();
            var textboxValues = textboxElements.Select(e => e.Text).ToArray();
            int startIndex = 3;
            int endIndex = textValues.Length;

            for (int i = startIndex; i <= endIndex - 1; i++)
            {
                if (deviceType == "Wired" || deviceType == "Non-Rechargeable")
                {
                    if (textValues[i] == "Tube Length")
                    {
                        continue;
                    }
                    if (i >= 16 && i <= 19)
                    {
                        continue;
                    }
                    LogElementStatus(test, textValues[i], textboxValues[i - startIndex]);
                }
                else if (deviceType == "Rechargeable" || deviceType == "D1rechargeableWired")
                {

                    if (textValues[i] == "Tube Length")
                    {
                        continue;
                    }

                    if (i == 16 && string.IsNullOrEmpty(textboxValues[i - startIndex]))
                    {
                        continue;
                    }
                    LogElementStatus(test, textValues[i], textboxValues[i - startIndex]);
                }
                else
                {
                    LogElementStatus(test, textValues[i], textboxValues[i - startIndex]);
                }
            }

            /*Excel */


            String textDir = Directory.GetCurrentDirectory();

            var path = textDir + "\\" + textboxValues[0] + ".xlsx";
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                //Set some properties of the Excel document
                excelPackage.Workbook.Properties.Author = "Test S&R Tool";
                excelPackage.Workbook.Properties.Title = "Device Info Parameters";
                excelPackage.Workbook.Properties.Subject = "Write in Excel";
                excelPackage.Workbook.Properties.Created = DateTime.Now;

                //Create the WorkSheet

                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");

                for ( int i = 0; i < textValues.Length; i++)
                {
                    worksheet.Cells[i + 1, 1].Value = textValues[i]; // Column= 1, Row =3
                }
                for (int i = 0; i < textboxValues.Length; i++)
                {
                    // Column= 1, Row =3
                    worksheet.Cells[i + 4, 2].Value = textboxValues[i]; //*adding data
                }

                //Save your file

                FileInfo fi = new FileInfo(path);
                excelPackage.SaveAs(fi);
            }
        } // Func getDevicInfo

        /**** Function to read values from CSV file ****/


        /** To get the device firmware into the Excel sheet **/
        public void getDeviceInfoForFirmware(WindowsDriver<WindowsElement> session)
        {
            session.FindElementByName("Device Info").Click();
            Thread.Sleep(2000);
            var labels = session.FindElementsByClassName("Text");
            strArray = new string[labels.Count];
            int i = 0;

            foreach (var label in labels)
            {
                Console.Write(label.Text);

                strArray[i] = label.Text;
                i = i + 1;
            }

            var labelValue = session.FindElementsByClassName("TextBox");
            i = 0;
            strArrayVal = new string[labelValue.Count];

            foreach (var label in labelValue)
            {
                Console.Write(label.Text.ToString());
                strArrayVal[i] = label.Text.ToString();
                i = i + 1;

            }


            /*Excel */


            String textDir = Directory.GetCurrentDirectory();

            var path = textDir + "\\" + strArrayVal[0] + "1" + ".xlsx";
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage excelPackage = new ExcelPackage())
            {

                //Set some properties of the Excel document

                excelPackage.Workbook.Properties.Author = "Test S&R Tool";
                excelPackage.Workbook.Properties.Title = "Device Info Parameters";
                excelPackage.Workbook.Properties.Subject = "Write in Excel";
                excelPackage.Workbook.Properties.Created = DateTime.Now;

                //Create the WorkSheet

                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");

                for (i = 0; i < strArray.Length; i++)
                {
                    worksheet.Cells[i + 1, 1].Value = strArray[i]; // Column= 1, Row =3

                }
                for (i = 0; i < strArrayVal.Length; i++)
                {
                    // Column= 1, Row =3

                    worksheet.Cells[i + 4, 2].Value = strArrayVal[i]; //*adding data


                }


                //Save your file

                FileInfo fi = new FileInfo(path);
                excelPackage.SaveAs(fi);
            }
        } // Func getDevicInfo


        /** To read the CSV files **/
        public static String[] readCSVFile()
        {

            using (var reader = new StreamReader(textDir + "\\TestData.csv"))
            {

                List<string> listA = new List<string>();
                List<string> listB = new List<string>();

                String[] csvVal = new string[100];
                int i = 0;

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    listA.Add(values[0]);
                    listB.Add(values[1]);

                    csvVal[i] = values[0];
                    csvVal[i + 1] = values[1];

                    i = i + 2;
                }
                return csvVal;
            }

        }// Read CSV File function


        /** To Compare the Dump files with attached attribute values **/
        public void dumpCompare1(string device, ExtentTest stepName)
        {

            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
            XmlDocument doc = new XmlDocument();
            // doc.Load("C:\\Device C.xml");
            doc.Load("C:\\" + device + ".xml");

            XmlDocument doc1 = new XmlDocument();
            doc1.Load("C:\\Device A.xml");

            /* XML file 1 Attributes */
            string[] DfsInitLayoutItem = new string[5];
            string[] FittingSoftwareInfoSpace = new string[5];
            string[] OsPreset = new string[5];
            string[] PresetTable = new string[5];
            string[] GlobalPreset = new string[5];
            string[] CombinedPreset = new string[5];
            string[] GattDatabase = new string[5];
            string[] PersistentDataSpace = new string[5];

            /* XML file 2 Attributes */
            string[] DfsInitLayoutItem1 = new string[5];
            string[] FittingSoftwareInfoSpace1 = new string[5];
            string[] OsPreset1 = new string[5];
            string[] PresetTable1 = new string[5];
            string[] GlobalPreset1 = new string[5];
            string[] CombinedPreset1 = new string[5];
            string[] GattDatabase1 = new string[5];
            string[] PersistentDataSpace1 = new string[5];


            string[] arrayVal = new string[] { "Type", "StartAddress", "Size", "IsSystem", "Data" };
            string displayName = null;
            string type = null;
            string startaddress = null;
            string size = null;
            string isSystem = null;
            string data = null;

            var txt = doc.DocumentElement.ChildNodes;


            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                displayName = node.Attributes["DisplayName"].InnerText; //or loop through its children as well


                switch (displayName)
                {
                    case "DfsInitLayoutItem":

                        DfsInitLayoutItem[0] = node.Attributes["Type"].InnerText;
                        DfsInitLayoutItem[1] = node.Attributes["StartAddress"].InnerText;
                        DfsInitLayoutItem[2] = node.Attributes["Size"].InnerText;
                        DfsInitLayoutItem[3] = node.Attributes["IsSystem"].InnerText;
                        DfsInitLayoutItem[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;

                    case "FittingSoftwareInfoSpace":

                        FittingSoftwareInfoSpace[0] = node.Attributes["Type"].InnerText;
                        FittingSoftwareInfoSpace[1] = node.Attributes["StartAddress"].InnerText;
                        FittingSoftwareInfoSpace[2] = node.Attributes["Size"].InnerText;
                        FittingSoftwareInfoSpace[3] = node.Attributes["IsSystem"].InnerText;
                        FittingSoftwareInfoSpace[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;

                    case "OsPreset":

                        OsPreset[0] = node.Attributes["Type"].InnerText;
                        OsPreset[1] = node.Attributes["StartAddress"].InnerText;
                        OsPreset[2] = node.Attributes["Size"].InnerText;
                        OsPreset[3] = node.Attributes["IsSystem"].InnerText;
                        OsPreset[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;

                    case "PresetTable":

                        PresetTable[0] = node.Attributes["Type"].InnerText;
                        PresetTable[1] = node.Attributes["StartAddress"].InnerText;
                        PresetTable[2] = node.Attributes["Size"].InnerText;
                        PresetTable[3] = node.Attributes["IsSystem"].InnerText;
                        PresetTable[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;

                    case "GlobalPreset":

                        GlobalPreset[0] = node.Attributes["Type"].InnerText;
                        GlobalPreset[1] = node.Attributes["StartAddress"].InnerText;
                        GlobalPreset[2] = node.Attributes["Size"].InnerText;
                        GlobalPreset[3] = node.Attributes["IsSystem"].InnerText;
                        GlobalPreset[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;

                    case "CombinedPreset":

                        CombinedPreset[0] = node.Attributes["Type"].InnerText;
                        CombinedPreset[1] = node.Attributes["StartAddress"].InnerText;
                        CombinedPreset[2] = node.Attributes["Size"].InnerText;
                        CombinedPreset[3] = node.Attributes["IsSystem"].InnerText;
                        CombinedPreset[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;

                    case "GattDatabase":

                        GattDatabase[0] = node.Attributes["Type"].InnerText;
                        GattDatabase[1] = node.Attributes["StartAddress"].InnerText;
                        GattDatabase[2] = node.Attributes["Size"].InnerText;
                        GattDatabase[3] = node.Attributes["IsSystem"].InnerText;
                        GattDatabase[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;

                    case "PersistentDataSpace":

                        PersistentDataSpace[0] = node.Attributes["Type"].InnerText;
                        PersistentDataSpace[1] = node.Attributes["StartAddress"].InnerText;
                        PersistentDataSpace[2] = node.Attributes["Size"].InnerText;
                        PersistentDataSpace[3] = node.Attributes["IsSystem"].InnerText;
                        PersistentDataSpace[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;


                }

            }

            /****File 2 Reading values *****/

            foreach (XmlNode node in doc1.DocumentElement.ChildNodes)
            {
                displayName = node.Attributes["DisplayName"].InnerText; //or loop through its children as well

                switch (displayName)
                {
                    case "DfsInitLayoutItem":

                        DfsInitLayoutItem1[0] = node.Attributes["Type"].InnerText;
                        DfsInitLayoutItem1[1] = node.Attributes["StartAddress"].InnerText;
                        DfsInitLayoutItem1[2] = node.Attributes["Size"].InnerText;
                        DfsInitLayoutItem1[3] = node.Attributes["IsSystem"].InnerText;
                        DfsInitLayoutItem1[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;

                    case "FittingSoftwareInfoSpace":

                        FittingSoftwareInfoSpace1[0] = node.Attributes["Type"].InnerText;
                        FittingSoftwareInfoSpace1[1] = node.Attributes["StartAddress"].InnerText;
                        FittingSoftwareInfoSpace1[2] = node.Attributes["Size"].InnerText;
                        FittingSoftwareInfoSpace1[3] = node.Attributes["IsSystem"].InnerText;
                        FittingSoftwareInfoSpace1[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;


                    case "OsPreset":

                        OsPreset1[0] = node.Attributes["Type"].InnerText;
                        OsPreset1[1] = node.Attributes["StartAddress"].InnerText;
                        OsPreset1[2] = node.Attributes["Size"].InnerText;
                        OsPreset1[3] = node.Attributes["IsSystem"].InnerText;
                        OsPreset1[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;


                    case "PresetTable":

                        PresetTable1[0] = node.Attributes["Type"].InnerText;
                        PresetTable1[1] = node.Attributes["StartAddress"].InnerText;
                        PresetTable1[2] = node.Attributes["Size"].InnerText;
                        PresetTable1[3] = node.Attributes["IsSystem"].InnerText;
                        PresetTable1[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;


                    case "GlobalPreset":

                        GlobalPreset1[0] = node.Attributes["Type"].InnerText;
                        GlobalPreset1[1] = node.Attributes["StartAddress"].InnerText;
                        GlobalPreset1[2] = node.Attributes["Size"].InnerText;
                        GlobalPreset1[3] = node.Attributes["IsSystem"].InnerText;
                        GlobalPreset1[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;


                    case "CombinedPreset":

                        CombinedPreset1[0] = node.Attributes["Type"].InnerText;
                        CombinedPreset1[1] = node.Attributes["StartAddress"].InnerText;
                        CombinedPreset1[2] = node.Attributes["Size"].InnerText;
                        CombinedPreset1[3] = node.Attributes["IsSystem"].InnerText;
                        CombinedPreset1[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;


                    case "GattDatabase":

                        GattDatabase1[0] = node.Attributes["Type"].InnerText;
                        GattDatabase1[1] = node.Attributes["StartAddress"].InnerText;
                        GattDatabase1[2] = node.Attributes["Size"].InnerText;
                        GattDatabase1[3] = node.Attributes["IsSystem"].InnerText;
                        GattDatabase1[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;


                    case "PersistentDataSpace":

                        PersistentDataSpace1[0] = node.Attributes["Type"].InnerText;
                        PersistentDataSpace1[1] = node.Attributes["StartAddress"].InnerText;
                        PersistentDataSpace1[2] = node.Attributes["Size"].InnerText;
                        PersistentDataSpace1[3] = node.Attributes["IsSystem"].InnerText;
                        PersistentDataSpace1[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;


                }

            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 4; j < 5; j++)
                {
                    try
                    {


                        switch (i)
                        {
                            case 0:
                                {

                                    if ((!DfsInitLayoutItem[j].Equals(DfsInitLayoutItem1[j])) && device.Equals("Device B"))
                                    {
                                        Console.WriteLine("Pass");
                                        stepName.Log(Status.Pass, "Compared Value of DFSInitLayoutItem :** " + "'" + arrayVal[j].ToUpper() + "'" + "**: is" + DfsInitLayoutItem[j] + "  " + "AND Actual Value is :" + DfsInitLayoutItem1[j]);
                                    }
                                    else if ((DfsInitLayoutItem[j].Equals(DfsInitLayoutItem1[j])) && device.Equals("Device C"))
                                    {
                                        Console.WriteLine("Pass");
                                        stepName.Log(Status.Pass, "Compared Value of DFSInitLayoutItem :** " + "'" + arrayVal[j].ToUpper() + "'" + "**: is" + DfsInitLayoutItem[j] + "  " + "AND Actual Value is :" + DfsInitLayoutItem1[j]);
                                    }
                                    else if ((DfsInitLayoutItem[j].Equals(DfsInitLayoutItem1[j])) && device.Equals("Device D"))
                                    {
                                        Console.WriteLine("Pass");
                                        stepName.Log(Status.Pass, "Compared Value of DFSInitLayoutItem :** " + "'" + arrayVal[j].ToUpper() + "'" + "**: is" + DfsInitLayoutItem[j] + "  " + "AND Actual Value is :" + DfsInitLayoutItem1[j]);
                                    }
                                    else
                                    {
                                        stepName.Log(Status.Info, "Compared Value of DFSInitLayoutItem Expected is: " + arrayVal[j] + ": is" + DfsInitLayoutItem[j] + "  " + "AND Actual Value is :" + DfsInitLayoutItem1[j]);
                                    }

                                    break;
                                }

                            case 1:

                                {
                                    if ((!FittingSoftwareInfoSpace[j].Equals(FittingSoftwareInfoSpace1[j])) && device.Equals("Device B"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of FittingSoftwareInfoSpace :** " + arrayVal[j].ToUpper() + "** : is" + FittingSoftwareInfoSpace[j] + "  " + "AND Actual Value is :" + FittingSoftwareInfoSpace1[j]);
                                    }
                                    else if ((FittingSoftwareInfoSpace[j].Equals(FittingSoftwareInfoSpace1[j])) && device.Equals("Device C"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of FittingSoftwareInfoSpace :** " + arrayVal[j].ToUpper() + "** : is" + FittingSoftwareInfoSpace[j] + "  " + "AND Actual Value is :" + FittingSoftwareInfoSpace1[j]);
                                    }
                                    else if ((FittingSoftwareInfoSpace[j].Equals(FittingSoftwareInfoSpace1[j])) && device.Equals("Device D"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of FittingSoftwareInfoSpace :** " + arrayVal[j].ToUpper() + "** : is" + FittingSoftwareInfoSpace[j] + "  " + "AND Actual Value is :" + FittingSoftwareInfoSpace1[j]);
                                    }
                                    else
                                    {
                                        stepName.Log(Status.Info, "Compared Value of FittingSoftwareInfoSpace Expected is: " + "'" + arrayVal[j] + "'" + " : is " + FittingSoftwareInfoSpace[j] + "  " + "AND Actual Value is :" + FittingSoftwareInfoSpace1[j]);
                                    }
                                    break;
                                }

                            case 2:

                                {
                                    if ((!OsPreset[j].Equals(OsPreset1[j])) && device.Equals("Device B"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of OsPreset :** " + arrayVal[j].ToUpper() + "** : is" + OsPreset[j] + "  " + "AND Actual Value is :" + OsPreset1[j]);
                                    }
                                    else if ((OsPreset[j].Equals(OsPreset1[j])) && device.Equals("Device C"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of OsPreset :** " + arrayVal[j].ToUpper() + "** : is" + OsPreset[j] + "  " + "AND Actual Value is :" + OsPreset1[j]);
                                    }
                                    else if ((OsPreset[j].Equals(OsPreset1[j])) && device.Equals("Device D"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of OsPreset :** " + arrayVal[j].ToUpper() + "** : is" + OsPreset[j] + "  " + "AND Actual Value is :" + OsPreset1[j]);
                                    }
                                    else
                                    {
                                        stepName.Log(Status.Info, "Compared Value of OsPreset Expected is: " + "'" + arrayVal[j] + "'" + " : is " + OsPreset[j] + "  " + "AND Actual Value is :" + OsPreset1[j]);
                                    }
                                    break;
                                }

                            case 3:

                                {
                                    if ((!PresetTable[j].Equals(PresetTable1[j])) && device.Equals("Device B"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of PresetTable :** " + arrayVal[j].ToUpper() + "** : is" + PresetTable[j] + "  " + "AND Actual Value is :" + PresetTable1[j]);
                                    }
                                    else if ((PresetTable[j].Equals(PresetTable1[j])) && device.Equals("Device C"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of PresetTable :** " + arrayVal[j].ToUpper() + "** : is" + PresetTable[j] + "  " + "AND Actual Value is :" + PresetTable1[j]);
                                    }
                                    else if ((PresetTable[j].Equals(PresetTable1[j])) && device.Equals("Device D"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of PresetTable :** " + arrayVal[j].ToUpper() + "** : is" + PresetTable[j] + "  " + "AND Actual Value is :" + PresetTable1[j]);
                                    }
                                    else
                                    {
                                        stepName.Log(Status.Info, "Compared Value of PresetTable Expected is: " + "'" + arrayVal[j] + "'" + " : is " + PresetTable[j] + "  " + "AND Actual Value is :" + PresetTable1[j]);
                                    }
                                    break;
                                }

                            case 4:

                                {
                                    if ((!GlobalPreset[j].Equals(GlobalPreset1[j])) && device.Equals("Device B"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of GlobalPreset :** " + arrayVal[j].ToUpper() + "** : is" + GlobalPreset[j] + "  " + "AND Actual Value is :" + GlobalPreset1[j]);
                                    }
                                    else if ((GlobalPreset[j].Equals(GlobalPreset1[j])) && device.Equals("Device C"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of GlobalPreset :** " + arrayVal[j].ToUpper() + "** : is" + GlobalPreset[j] + "  " + "AND Actual Value is :" + GlobalPreset1[j]);
                                    }
                                    else if ((GlobalPreset[j].Equals(GlobalPreset1[j])) && device.Equals("Device D"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of GlobalPreset :** " + arrayVal[j].ToUpper() + "** : is" + GlobalPreset[j] + "  " + "AND Actual Value is :" + GlobalPreset1[j]);
                                    }
                                    else
                                    {
                                        stepName.Log(Status.Info, "Compared Value of GlobalPreset Expected is: " + "'" + arrayVal[j] + "'" + " : is " + GlobalPreset[j] + "  " + "AND Actual Value is :" + GlobalPreset1[j]);
                                    }
                                    break;
                                }

                            //case 5:

                            //    {
                            //        if (CombinedPreset[j].Equals(CombinedPreset1[j]) && device.Equals("Device B"))
                            //        {
                            //            stepName.Log(Status.Pass, "Compared Value of CombinedPreset :** " + arrayVal[j].ToUpper() + "** : is" + CombinedPreset[j]);
                            //        }
                            //        if (!CombinedPreset[j].Equals(CombinedPreset1[j]) && device.Equals("Device C"))
                            //        {
                            //            stepName.Log(Status.Pass, "Compared Value of CombinedPreset :** " + arrayVal[j].ToUpper() + "** : is" + CombinedPreset[j]);
                            //        }
                            //        if (CombinedPreset[j].Equals(CombinedPreset1[j]) && device.Equals("Device D"))
                            //        {
                            //            stepName.Log(Status.Pass, "Compared Value of CombinedPreset :** " + arrayVal[j].ToUpper() + "** : is" + CombinedPreset[j]);
                            //        }
                            //        else
                            //        {
                            //            stepName.Log(Status.Fail, "Compared Value of CombinedPreset Expected is: " + "'" + arrayVal[j] + "'" + " : is " + CombinedPreset[j] + "AND Actual Value is :" + CombinedPreset1[j]);
                            //        }
                            //        break;
                            //    }

                            case 5:

                                {
                                    if ((!GattDatabase[j].Equals(GattDatabase1[j])) && device.Equals("Device B"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of GattDatabase :** " + arrayVal[j].ToUpper() + "** : is" + GattDatabase[j] + "  " + "AND Actual Value is :" + GattDatabase1[j]);
                                    }

                                    else if ((GattDatabase[j].Equals(GattDatabase1[j])) && device.Equals("Device C"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of GattDatabase :** " + arrayVal[j].ToUpper() + "** : is" + GattDatabase[j] + "  " + "AND Actual Value is :" + GattDatabase1[j]);
                                    }
                                    else if ((GattDatabase[j].Equals(GattDatabase1[j])) && device.Equals("Device D"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of GattDatabase :** " + arrayVal[j].ToUpper() + "** : is" + GattDatabase[j] + "  " + "AND Actual Value is :" + GattDatabase1[j]);
                                    }
                                    else
                                    {
                                        stepName.Log(Status.Info, "Compared Value of GattDatabase Expected is: " + "'" + arrayVal[j] + "'" + " : is " + GattDatabase[j] + "  " + "AND Actual Value is :" + GattDatabase1[j]);
                                    }
                                    break;
                                }

                            case 6:

                                {
                                    if ((!PersistentDataSpace[j].Equals(PersistentDataSpace1[j])) && device.Equals("Device B"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of PersistentDataSpace :** " + arrayVal[j].ToUpper() + "** : is" + PersistentDataSpace[j] + "  " + "AND Actual Value is :" + PersistentDataSpace1[j]);
                                    }
                                    else if ((PersistentDataSpace[j].Equals(PersistentDataSpace1[j])) && device.Equals("Device C"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of PersistentDataSpace :** " + arrayVal[j].ToUpper() + "** : is" + PersistentDataSpace[j] + "  " + "AND Actual Value is :" + PersistentDataSpace1[j]);
                                    }
                                    else if ((PersistentDataSpace[j].Equals(PersistentDataSpace1[j])) && device.Equals("Device D"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of PersistentDataSpace :** " + arrayVal[j].ToUpper() + "** : is" + PersistentDataSpace[j] + "  " + "AND Actual Value is :" + PersistentDataSpace1[j]);
                                    }
                                    else
                                    {
                                        stepName.Log(Status.Info, "Compared Value of PersistentDataSpace Expected is: " + "'" + arrayVal[j] + "'" + " : is " + PersistentDataSpace[j] + "  " + "AND Actual Value is :" + PersistentDataSpace1[j]);
                                    }
                                    break;
                                }


                        }
                    }
                    catch (Exception e) { }

                }
            }




            for (int i = 0; i < FittingSoftwareInfoSpace.Length; i++)
            {
                Console.WriteLine("FittingSoftwareInfoSpace " + arrayVal[i] + " : is :" + FittingSoftwareInfoSpace[i]);
            }


            foreach (var item in FittingSoftwareInfoSpace)
            {
                string txt1 = item;

            }
            Console.WriteLine(data);
            Console.WriteLine(type);
            Console.WriteLine(startaddress);
            Console.WriteLine(size);
            Console.WriteLine(isSystem);


            for (int i = 0; i < DfsInitLayoutItem.Length; i++)
            {
                Console.WriteLine("DfsInitLayoutItem " + arrayVal[i] + " : is :" + DfsInitLayoutItem[i]);
            }


            foreach (var item in DfsInitLayoutItem)
            {
                string txt1 = item;

            }
            Console.WriteLine(data);
            Console.WriteLine(type);
            Console.WriteLine(startaddress);
            Console.WriteLine(size);
            Console.WriteLine(isSystem);


            for (int i = 0; i < OsPreset.Length; i++)
            {
                Console.WriteLine("OsPreset " + arrayVal[i] + " : is :" + OsPreset[i]);
            }


            foreach (var item in OsPreset)
            {
                string txt1 = item;
            }
            Console.WriteLine(data);
            Console.WriteLine(type);
            Console.WriteLine(startaddress);
            Console.WriteLine(size);
            Console.WriteLine(isSystem);


            for (int i = 0; i < PresetTable.Length; i++)
            {
                Console.WriteLine("PresetTable " + arrayVal[i] + " : is :" + PresetTable[i]);
            }


            foreach (var item in PresetTable)
            {
                string txt1 = item;
            }
            Console.WriteLine(data);
            Console.WriteLine(type);
            Console.WriteLine(startaddress);
            Console.WriteLine(size);
            Console.WriteLine(isSystem);


            for (int i = 0; i < GlobalPreset.Length; i++)
            {
                Console.WriteLine("GlobalPreset " + arrayVal[i] + " : is :" + GlobalPreset[i]);
            }


            foreach (var item in GlobalPreset)
            {
                string txt1 = item;
            }
            Console.WriteLine(data);
            Console.WriteLine(type);
            Console.WriteLine(startaddress);
            Console.WriteLine(size);
            Console.WriteLine(isSystem);


            for (int i = 0; i < CombinedPreset.Length; i++)
            {
                Console.WriteLine("CombinedPreset " + arrayVal[i] + " : is :" + CombinedPreset[i]);
            }


            foreach (var item in CombinedPreset)
            {
                string txt1 = item;
            }
            Console.WriteLine(data);
            Console.WriteLine(type);
            Console.WriteLine(startaddress);
            Console.WriteLine(size);
            Console.WriteLine(isSystem);


            for (int i = 0; i < GattDatabase.Length; i++)
            {
                Console.WriteLine("GattDatabase " + arrayVal[i] + " : is :" + GattDatabase[i]);
            }


            foreach (var item in GattDatabase)
            {
                string txt1 = item;
            }
            Console.WriteLine(data);
            Console.WriteLine(type);
            Console.WriteLine(startaddress);
            Console.WriteLine(size);
            Console.WriteLine(isSystem);


            for (int i = 0; i < PersistentDataSpace.Length; i++)
            {
                Console.WriteLine("PersistentDataSpace " + arrayVal[i] + " : is :" + PersistentDataSpace[i]);
            }


            foreach (var item in PersistentDataSpace)
            {
                string txt1 = item;

            }
            Console.WriteLine(data);
            Console.WriteLine(type);
            Console.WriteLine(startaddress);
            Console.WriteLine(size);
            Console.WriteLine(isSystem);


        }


        /* Verifying device dumps and report errors incase any differences*/

        public void dumpCompare(string device, ExtentTest stepName)
        {


            test = ScenarioContext.Current["extentTest"] as ExtentTest;

            stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());

            XmlDocument doc = new XmlDocument();
            doc.Load("C:\\" + device + ".xml");

            XmlDocument doc1 = new XmlDocument();
            doc1.Load("C:\\Device A.xml");
            //string DeviceA = "Device A";

            /* XML file 1 Attributes */
            string[] DfsInitLayoutItem = new string[5];
            string[] FittingSoftwareInfoSpace = new string[5];
            string[] OsPreset = new string[5];
            string[] PresetTable = new string[5];
            string[] GlobalPreset = new string[5];
            string[] CombinedPreset = new string[5];
            string[] GattDatabase = new string[5];
            string[] PersistentDataSpace = new string[5];

            /* XML file 2 Attributes */
            string[] DfsInitLayoutItem1 = new string[5];
            string[] FittingSoftwareInfoSpace1 = new string[5];
            string[] OsPreset1 = new string[5];
            string[] PresetTable1 = new string[5];
            string[] GlobalPreset1 = new string[5];
            string[] CombinedPreset1 = new string[5];
            string[] GattDatabase1 = new string[5];
            string[] PersistentDataSpace1 = new string[5];


            string[] arrayVal = new string[] { "Type", "StartAddress", "Size", "IsSystem", "Data" };
            string displayName = null;
            string type = null;
            string startaddress = null;
            string size = null;
            string isSystem = null;
            string data = null;

            var txt = doc.DocumentElement.ChildNodes;


            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                displayName = node.Attributes["DisplayName"].InnerText; //or loop through its children as well           

                switch (displayName)
                {
                    case "DfsInitLayoutItem":

                        DfsInitLayoutItem[0] = node.Attributes["Type"].InnerText;
                        DfsInitLayoutItem[1] = node.Attributes["StartAddress"].InnerText;
                        DfsInitLayoutItem[2] = node.Attributes["Size"].InnerText;
                        DfsInitLayoutItem[3] = node.Attributes["IsSystem"].InnerText;
                        DfsInitLayoutItem[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;

                    case "FittingSoftwareInfoSpace":

                        FittingSoftwareInfoSpace[0] = node.Attributes["Type"].InnerText;
                        FittingSoftwareInfoSpace[1] = node.Attributes["StartAddress"].InnerText;
                        FittingSoftwareInfoSpace[2] = node.Attributes["Size"].InnerText;
                        FittingSoftwareInfoSpace[3] = node.Attributes["IsSystem"].InnerText;
                        FittingSoftwareInfoSpace[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;

                    case "OsPreset":

                        OsPreset[0] = node.Attributes["Type"].InnerText;
                        OsPreset[1] = node.Attributes["StartAddress"].InnerText;
                        OsPreset[2] = node.Attributes["Size"].InnerText;
                        OsPreset[3] = node.Attributes["IsSystem"].InnerText;
                        OsPreset[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;

                    case "PresetTable":

                        PresetTable[0] = node.Attributes["Type"].InnerText;
                        PresetTable[1] = node.Attributes["StartAddress"].InnerText;
                        PresetTable[2] = node.Attributes["Size"].InnerText;
                        PresetTable[3] = node.Attributes["IsSystem"].InnerText;
                        PresetTable[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;

                    case "GlobalPreset":

                        GlobalPreset[0] = node.Attributes["Type"].InnerText;
                        GlobalPreset[1] = node.Attributes["StartAddress"].InnerText;
                        GlobalPreset[2] = node.Attributes["Size"].InnerText;
                        GlobalPreset[3] = node.Attributes["IsSystem"].InnerText;
                        GlobalPreset[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;

                    case "CombinedPreset":

                        CombinedPreset[0] = node.Attributes["Type"].InnerText;
                        CombinedPreset[1] = node.Attributes["StartAddress"].InnerText;
                        CombinedPreset[2] = node.Attributes["Size"].InnerText;
                        CombinedPreset[3] = node.Attributes["IsSystem"].InnerText;
                        CombinedPreset[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;

                    case "GattDatabase":

                        GattDatabase[0] = node.Attributes["Type"].InnerText;
                        GattDatabase[1] = node.Attributes["StartAddress"].InnerText;
                        GattDatabase[2] = node.Attributes["Size"].InnerText;
                        GattDatabase[3] = node.Attributes["IsSystem"].InnerText;
                        GattDatabase[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;

                    case "PersistentDataSpace":

                        PersistentDataSpace[0] = node.Attributes["Type"].InnerText;
                        PersistentDataSpace[1] = node.Attributes["StartAddress"].InnerText;
                        PersistentDataSpace[2] = node.Attributes["Size"].InnerText;
                        PersistentDataSpace[3] = node.Attributes["IsSystem"].InnerText;
                        PersistentDataSpace[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;


                }

            }

            /****File 2 Reading values *****/

            foreach (XmlNode node in doc1.DocumentElement.ChildNodes)
            {
                displayName = node.Attributes["DisplayName"].InnerText; //or loop through its children as well

                switch (displayName)
                {
                    case "DfsInitLayoutItem":

                        DfsInitLayoutItem1[0] = node.Attributes["Type"].InnerText;
                        DfsInitLayoutItem1[1] = node.Attributes["StartAddress"].InnerText;
                        DfsInitLayoutItem1[2] = node.Attributes["Size"].InnerText;
                        DfsInitLayoutItem1[3] = node.Attributes["IsSystem"].InnerText;
                        DfsInitLayoutItem1[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;

                    case "FittingSoftwareInfoSpace":

                        FittingSoftwareInfoSpace1[0] = node.Attributes["Type"].InnerText;
                        FittingSoftwareInfoSpace1[1] = node.Attributes["StartAddress"].InnerText;
                        FittingSoftwareInfoSpace1[2] = node.Attributes["Size"].InnerText;
                        FittingSoftwareInfoSpace1[3] = node.Attributes["IsSystem"].InnerText;
                        FittingSoftwareInfoSpace1[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;


                    case "OsPreset":

                        OsPreset1[0] = node.Attributes["Type"].InnerText;
                        OsPreset1[1] = node.Attributes["StartAddress"].InnerText;
                        OsPreset1[2] = node.Attributes["Size"].InnerText;
                        OsPreset1[3] = node.Attributes["IsSystem"].InnerText;
                        OsPreset1[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;


                    case "PresetTable":

                        PresetTable1[0] = node.Attributes["Type"].InnerText;
                        PresetTable1[1] = node.Attributes["StartAddress"].InnerText;
                        PresetTable1[2] = node.Attributes["Size"].InnerText;
                        PresetTable1[3] = node.Attributes["IsSystem"].InnerText;
                        PresetTable1[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;


                    case "GlobalPreset":

                        GlobalPreset1[0] = node.Attributes["Type"].InnerText;
                        GlobalPreset1[1] = node.Attributes["StartAddress"].InnerText;
                        GlobalPreset1[2] = node.Attributes["Size"].InnerText;
                        GlobalPreset1[3] = node.Attributes["IsSystem"].InnerText;
                        GlobalPreset1[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;


                    case "CombinedPreset":

                        CombinedPreset1[0] = node.Attributes["Type"].InnerText;
                        CombinedPreset1[1] = node.Attributes["StartAddress"].InnerText;
                        CombinedPreset1[2] = node.Attributes["Size"].InnerText;
                        CombinedPreset1[3] = node.Attributes["IsSystem"].InnerText;
                        CombinedPreset1[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;


                    case "GattDatabase":

                        GattDatabase1[0] = node.Attributes["Type"].InnerText;
                        GattDatabase1[1] = node.Attributes["StartAddress"].InnerText;
                        GattDatabase1[2] = node.Attributes["Size"].InnerText;
                        GattDatabase1[3] = node.Attributes["IsSystem"].InnerText;
                        GattDatabase1[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;


                    case "PersistentDataSpace":

                        PersistentDataSpace1[0] = node.Attributes["Type"].InnerText;
                        PersistentDataSpace1[1] = node.Attributes["StartAddress"].InnerText;
                        PersistentDataSpace1[2] = node.Attributes["Size"].InnerText;
                        PersistentDataSpace1[3] = node.Attributes["IsSystem"].InnerText;
                        PersistentDataSpace1[4] = node.InnerText;
                        Console.WriteLine(data);
                        break;


                }

            }


            for (int i = 0; i < 7; i++)
            {
                for (int j = 4; j < 5; j++)
                {
                    try
                    {


                        switch (i)
                        {
                            case 0:
                                {

                                    if ((DfsInitLayoutItem[j].Equals(DfsInitLayoutItem1[j])) && device.Equals("Device B"))
                                    {
                                        Console.WriteLine("Pass");
                                        stepName.Log(Status.Pass, "Compared Value of DFSInitLayoutItem :** " + "'" + arrayVal[j].ToUpper() + "'" + "**: is" + DfsInitLayoutItem[j] + "  " + "AND Actual Value is :" + DfsInitLayoutItem1[j]);
                                    }
                                    else if ((!DfsInitLayoutItem[j].Equals(DfsInitLayoutItem1[j])) && device.Equals("Device C"))
                                    {
                                        Console.WriteLine("Pass");
                                        stepName.Log(Status.Pass, "Compared Value of DFSInitLayoutItem :** " + "'" + arrayVal[j].ToUpper() + "'" + "**: is" + DfsInitLayoutItem[j] + "  " + "AND Actual Value is :" + DfsInitLayoutItem1[j]);
                                    }
                                    else if ((DfsInitLayoutItem[j].Equals(DfsInitLayoutItem1[j])) && device.Equals("Device D"))
                                    {
                                        Console.WriteLine("Pass");
                                        stepName.Log(Status.Pass, "Compared Value of DFSInitLayoutItem :** " + "'" + arrayVal[j].ToUpper() + "'" + "**: is" + DfsInitLayoutItem[j] + "  " + "AND Actual Value is :" + DfsInitLayoutItem1[j]);
                                    }
                                    else
                                    {
                                        stepName.Log(Status.Info, "Compared Value of DFSInitLayoutItem Expected is: " + arrayVal[j] + ": is" + DfsInitLayoutItem[j] + "  " + "AND Actual Value is :" + DfsInitLayoutItem1[j]);
                                    }

                                    break;
                                }

                            case 1:

                                {
                                    if ((FittingSoftwareInfoSpace[j].Equals(FittingSoftwareInfoSpace1[j])) && device.Equals("Device B"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of FittingSoftwareInfoSpace :** " + arrayVal[j].ToUpper() + "** : is" + FittingSoftwareInfoSpace[j] + "  " + "AND Actual Value is :" + FittingSoftwareInfoSpace1[j]);
                                    }
                                    else if ((!FittingSoftwareInfoSpace[j].Equals(FittingSoftwareInfoSpace1[j])) && device.Equals("Device C"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of FittingSoftwareInfoSpace :** " + arrayVal[j].ToUpper() + "** : is" + FittingSoftwareInfoSpace[j] + "  " + "AND Actual Value is :" + FittingSoftwareInfoSpace1[j]);
                                    }
                                    else if ((FittingSoftwareInfoSpace[j].Equals(FittingSoftwareInfoSpace1[j])) && device.Equals("Device D"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of FittingSoftwareInfoSpace :** " + arrayVal[j].ToUpper() + "** : is" + FittingSoftwareInfoSpace[j] + "  " + "AND Actual Value is :" + FittingSoftwareInfoSpace1[j]);
                                    }
                                    else
                                    {
                                        stepName.Log(Status.Info, "Compared Value of FittingSoftwareInfoSpace Expected is: " + "'" + arrayVal[j] + "'" + " : is " + FittingSoftwareInfoSpace[j] + "  " + "AND Actual Value is :" + FittingSoftwareInfoSpace1[j]);
                                    }
                                    break;
                                }

                            case 2:

                                {
                                    if ((OsPreset[j].Equals(OsPreset1[j])) && device.Equals("Device B"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of OsPreset :** " + arrayVal[j].ToUpper() + "** : is" + OsPreset[j] + "  " + "AND Actual Value is :" + OsPreset1[j]);
                                    }
                                    else if ((!OsPreset[j].Equals(OsPreset1[j])) && device.Equals("Device C"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of OsPreset :** " + arrayVal[j].ToUpper() + "** : is" + OsPreset[j] + "  " + "AND Actual Value is :" + OsPreset1[j]);
                                    }
                                    else if ((OsPreset[j].Equals(OsPreset1[j])) && device.Equals("Device D"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of OsPreset :** " + arrayVal[j].ToUpper() + "** : is" + OsPreset[j] + "  " + "AND Actual Value is :" + OsPreset1[j]);
                                    }
                                    else
                                    {
                                        stepName.Log(Status.Info, "Compared Value of OsPreset Expected is: " + "'" + arrayVal[j] + "'" + " : is " + OsPreset[j] + "  " + "AND Actual Value is :" + OsPreset1[j]);
                                    }
                                    break;
                                }

                            case 3:

                                {
                                    if ((PresetTable[j].Equals(PresetTable1[j])) && device.Equals("Device B"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of PresetTable :** " + arrayVal[j].ToUpper() + "** : is" + PresetTable[j] + "  " + "AND Actual Value is :" + PresetTable1[j]);
                                    }
                                    else if ((!PresetTable[j].Equals(PresetTable1[j])) && device.Equals("Device C"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of PresetTable :** " + arrayVal[j].ToUpper() + "** : is" + PresetTable[j] + "  " + "AND Actual Value is :" + PresetTable1[j]);
                                    }
                                    else if ((PresetTable[j].Equals(PresetTable1[j])) && device.Equals("Device D"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of PresetTable :** " + arrayVal[j].ToUpper() + "** : is" + PresetTable[j] + "  " + "AND Actual Value is :" + PresetTable1[j]);
                                    }
                                    else
                                    {
                                        stepName.Log(Status.Info, "Compared Value of PresetTable Expected is: " + "'" + arrayVal[j] + "'" + " : is " + PresetTable[j] + "  " + "AND Actual Value is :" + PresetTable1[j]);
                                    }
                                    break;
                                }

                            case 4:

                                {
                                    if ((GlobalPreset[j].Equals(GlobalPreset1[j])) && device.Equals("Device B"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of GlobalPreset :** " + arrayVal[j].ToUpper() + "** : is" + GlobalPreset[j] + "  " + "AND Actual Value is :" + GlobalPreset1[j]);
                                    }
                                    else if ((!GlobalPreset[j].Equals(GlobalPreset1[j])) && device.Equals("Device C"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of GlobalPreset :** " + arrayVal[j].ToUpper() + "** : is" + GlobalPreset[j] + "  " + "AND Actual Value is :" + GlobalPreset1[j]);
                                    }
                                    else if ((GlobalPreset[j].Equals(GlobalPreset1[j])) && device.Equals("Device D"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of GlobalPreset :** " + arrayVal[j].ToUpper() + "** : is" + GlobalPreset[j] + "  " + "AND Actual Value is :" + GlobalPreset1[j]);
                                    }
                                    else
                                    {
                                        stepName.Log(Status.Info, "Compared Value of GlobalPreset Expected is: " + "'" + arrayVal[j] + "'" + " : is " + GlobalPreset[j] + "  " + "AND Actual Value is :" + GlobalPreset1[j]);
                                    }
                                    break;
                                }

                            //case 5:

                            //    {
                            //        if (CombinedPreset[j].Equals(CombinedPreset1[j]) && device.Equals("Device B"))
                            //        {
                            //            stepName.Log(Status.Pass, "Compared Value of CombinedPreset :** " + arrayVal[j].ToUpper() + "** : is" + CombinedPreset[j]);
                            //        }
                            //        if (!CombinedPreset[j].Equals(CombinedPreset1[j]) && device.Equals("Device C"))
                            //        {
                            //            stepName.Log(Status.Pass, "Compared Value of CombinedPreset :** " + arrayVal[j].ToUpper() + "** : is" + CombinedPreset[j]);
                            //        }
                            //        if (CombinedPreset[j].Equals(CombinedPreset1[j]) && device.Equals("Device D"))
                            //        {
                            //            stepName.Log(Status.Pass, "Compared Value of CombinedPreset :** " + arrayVal[j].ToUpper() + "** : is" + CombinedPreset[j]);
                            //        }
                            //        else
                            //        {
                            //            stepName.Log(Status.Fail, "Compared Value of CombinedPreset Expected is: " + "'" + arrayVal[j] + "'" + " : is " + CombinedPreset[j] + "AND Actual Value is :" + CombinedPreset1[j]);
                            //        }
                            //        break;
                            //    }

                            case 5:

                                {
                                    if ((GattDatabase[j].Equals(GattDatabase1[j])) && device.Equals("Device B"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of GattDatabase :** " + arrayVal[j].ToUpper() + "** : is" + GattDatabase[j] + "  " + "AND Actual Value is :" + GattDatabase1[j]);
                                    }

                                    else if ((!GattDatabase[j].Equals(GattDatabase1[j])) && device.Equals("Device C"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of GattDatabase :** " + arrayVal[j].ToUpper() + "** : is" + GattDatabase[j] + "  " + "AND Actual Value is :" + GattDatabase1[j]);
                                    }
                                    else if ((GattDatabase[j].Equals(GattDatabase1[j])) && device.Equals("Device D"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of GattDatabase :** " + arrayVal[j].ToUpper() + "** : is" + GattDatabase[j] + "  " + "AND Actual Value is :" + GattDatabase1[j]);
                                    }
                                    else
                                    {
                                        stepName.Log(Status.Info, "Compared Value of GattDatabase Expected is: " + "'" + arrayVal[j] + "'" + " : is " + GattDatabase[j] + "  " + "AND Actual Value is :" + GattDatabase1[j]);
                                    }
                                    break;
                                }

                            case 6:

                                {
                                    if ((PersistentDataSpace[j].Equals(PersistentDataSpace1[j])) && device.Equals("Device B"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of PersistentDataSpace :** " + arrayVal[j].ToUpper() + "** : is" + PersistentDataSpace[j] + "  " + "AND Actual Value is :" + PersistentDataSpace1[j]);
                                    }
                                    else if ((!PersistentDataSpace[j].Equals(PersistentDataSpace1[j])) && device.Equals("Device C"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of PersistentDataSpace :** " + arrayVal[j].ToUpper() + "** : is" + PersistentDataSpace[j] + "  " + "AND Actual Value is :" + PersistentDataSpace1[j]);
                                    }
                                    else if ((PersistentDataSpace[j].Equals(PersistentDataSpace1[j])) && device.Equals("Device D"))
                                    {
                                        stepName.Log(Status.Pass, "Compared Value of PersistentDataSpace :** " + arrayVal[j].ToUpper() + "** : is" + PersistentDataSpace[j] + "  " + "AND Actual Value is :" + PersistentDataSpace1[j]);
                                    }
                                    else
                                    {
                                        stepName.Log(Status.Info, "Compared Value of PersistentDataSpace Expected is: " + "'" + arrayVal[j] + "'" + " : is " + PersistentDataSpace[j] + "  " + "AND Actual Value is :" + PersistentDataSpace1[j]);
                                    }
                                    break;
                                }


                        }

                    }

                    catch (Exception e) { }
                }
            }




            for (int i = 0; i < FittingSoftwareInfoSpace.Length; i++)
            {
                Console.WriteLine("FittingSoftwareInfoSpace " + arrayVal[i] + " : is :" + FittingSoftwareInfoSpace[i]);
            }


            foreach (var item in FittingSoftwareInfoSpace)
            {
                string txt1 = item;

            }
            Console.WriteLine(data);
            Console.WriteLine(type);
            Console.WriteLine(startaddress);
            Console.WriteLine(size);
            Console.WriteLine(isSystem);


            for (int i = 0; i < DfsInitLayoutItem.Length; i++)
            {
                Console.WriteLine("DfsInitLayoutItem " + arrayVal[i] + " : is :" + DfsInitLayoutItem[i]);
            }


            foreach (var item in DfsInitLayoutItem)
            {
                string txt1 = item;
            }
            Console.WriteLine(data);
            Console.WriteLine(type);
            Console.WriteLine(startaddress);
            Console.WriteLine(size);
            Console.WriteLine(isSystem);


            for (int i = 0; i < OsPreset.Length; i++)
            {
                Console.WriteLine("OsPreset " + arrayVal[i] + " : is :" + OsPreset[i]);
            }


            foreach (var item in OsPreset)
            {
                string txt1 = item;
            }
            Console.WriteLine(data);
            Console.WriteLine(type);
            Console.WriteLine(startaddress);
            Console.WriteLine(size);
            Console.WriteLine(isSystem);


            for (int i = 0; i < PresetTable.Length; i++)
            {
                Console.WriteLine("PresetTable " + arrayVal[i] + " : is :" + PresetTable[i]);
            }


            foreach (var item in PresetTable)
            {
                string txt1 = item;
            }
            Console.WriteLine(data);
            Console.WriteLine(type);
            Console.WriteLine(startaddress);
            Console.WriteLine(size);
            Console.WriteLine(isSystem);


            for (int i = 0; i < GlobalPreset.Length; i++)
            {
                Console.WriteLine("GlobalPreset " + arrayVal[i] + " : is :" + GlobalPreset[i]);
            }


            foreach (var item in GlobalPreset)
            {
                string txt1 = item;
            }
            Console.WriteLine(data);
            Console.WriteLine(type);
            Console.WriteLine(startaddress);
            Console.WriteLine(size);
            Console.WriteLine(isSystem);


            for (int i = 0; i < CombinedPreset.Length; i++)
            {
                Console.WriteLine("CombinedPreset " + arrayVal[i] + " : is :" + CombinedPreset[i]);
            }


            foreach (var item in CombinedPreset)
            {
                string txt1 = item;
            }
            Console.WriteLine(data);
            Console.WriteLine(type);
            Console.WriteLine(startaddress);
            Console.WriteLine(size);
            Console.WriteLine(isSystem);


            for (int i = 0; i < GattDatabase.Length; i++)
            {
                Console.WriteLine("GattDatabase " + arrayVal[i] + " : is :" + GattDatabase[i]);
            }


            foreach (var item in GattDatabase)
            {
                string txt1 = item;
            }
            Console.WriteLine(data);
            Console.WriteLine(type);
            Console.WriteLine(startaddress);
            Console.WriteLine(size);
            Console.WriteLine(isSystem);


            for (int i = 0; i < PersistentDataSpace.Length; i++)
            {
                Console.WriteLine("PersistentDataSpace " + arrayVal[i] + " : is :" + PersistentDataSpace[i]);
            }


            foreach (var item in PersistentDataSpace)
            {
                string txt1 = item;
            }
            Console.WriteLine(data);
            Console.WriteLine(type);
            Console.WriteLine(startaddress);
            Console.WriteLine(size);
            Console.WriteLine(isSystem);

        }/* End of dumpCompare*/





        public void PassingXML(ExtentTest test)

        {

            foreach (string filePath in xmlFilePaths)

            {

                // Load the XML document

                XmlDocument doc = new XmlDocument();

                doc.Load(filePath);                                                                                                                                                                                                       // Replace with the path to your XML file

                // Select the nodes you want to update

                XmlNodeList nodes = doc.SelectNodes("//TFSTestResultsSet");

                foreach (XmlNode node in nodes)

                {
                    // Update TestPlanID, TestSuiteID, and TestConfiguration

                    XmlNode testPlanIdNode = node.SelectSingleNode("TestPlanID");

                    XmlNode testSuiteIdNode = node.SelectSingleNode("TestSuitID");

                    XmlNode testConfigNode = node.SelectSingleNode("TestConfiguration");


                    if (testPlanIdNode != null)

                    {

                        testPlanIdNode.InnerText = testPlanId;                                                                                      // Replace with the new TestPlanID value

                    }


                    if (testSuiteIdNode != null)
                    {

                        testSuiteIdNode.InnerText = testSuiteId;                                                                                    // Replace with the new TestSuiteID value

                    }

                    if (testConfigNode != null)

                    {

                        testConfigNode.InnerText = testConfig;                                                                   // Replace with the new TestConfiguration value

                    }

                }


                doc.Save(filePath);

            }// Save the updated XML document

        }   
        public void Azurefile(WindowsDriver<WindowsElement> session)
        {
            string directloc = textDir + "\\azurefiles";
            string[] fyles = Directory.GetFileSystemEntries(directloc, "*", SearchOption.AllDirectories);
            Console.WriteLine(String.Join(System.Environment.NewLine, fyles));
            string file = String.Join(System.Environment.NewLine, fyles);
            string sourceFile = file;
            System.IO.FileInfo fi = new System.IO.FileInfo(sourceFile);
            if (fi.Exists)
            {
                fi.MoveTo(file + ".rar");
            }
            ExtractZipFile(textDir + "\\azurefiles\\" + fi.Name, textDir + "\\azurefiles");
        }
        public XmlNodeList SelectNodesInXml(XmlDocument xmlDocument, string xPathQuery)
        {
            return xmlDocument.SelectNodes(xPathQuery);
        }
        [Obsolete]
        public void AzureFileCompare(WindowsDriver<WindowsElement> session, ExtentTest stepName)
        {
            test = ScenarioContext.Current["extentTest"] as ExtentTest;
            stepName = test.CreateNode(ScenarioStepContext.Current.StepInfo.Text.ToString());
            try
            {
                session = ModuleFunctions.launchApp(Directory.GetCurrentDirectory() + "\\LaunchSandR.bat", Directory.GetCurrentDirectory());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Thread.Sleep(5000);
            session = ModuleFunctions.sessionInitialize("C:\\Program Files (x86)\\GN Hearing\\Lucan\\App\\Lucan.App.UI.exe", "C:\\Program Files (x86)\\GN Hearing\\Lucan\\App");
            Azurefile(session);
            string sourceFile2 = GetSourceFileForComparison();
            CompareAzureFiles(stepName, sourceFile2, session);
        }
        private string GetSourceFileForComparison()
        {
            string directloc1 = textDir + "\\azurefiles";
            string[] fyles2 = Directory.GetFileSystemEntries(directloc1, "*", SearchOption.AllDirectories);
            return fyles2.Length > 1 ? String.Join(System.Environment.NewLine, fyles2[1]) : String.Join(System.Environment.NewLine, fyles2[0]);
        }
        private void CompareAzureFiles(ExtentTest stepName, string sourceFile2, WindowsDriver<WindowsElement> session)
        {
            var textElements = session.FindElementsByClassName("Text");
            var textboxElements = session.FindElementsByClassName("TextBox");
            var textValues = textElements.Select(e => e.Text).ToArray();
            var textboxValues = textboxElements.Select(e => e.Text).ToArray();
            int startIndex = 3;
            int endIndex = textValues.Length;
            System.IO.FileInfo fi2 = new System.IO.FileInfo(sourceFile2);
            if (fi2.Exists)
            {
                fi2.MoveTo(fi2.FullName + ".Xml");
            }
            stepName.Log(Status.Info, "Device information is captured in excel file");
            XmlDocument xmlDoc = new XmlDocument();
            string path = fi2.FullName;
            xmlDoc.Load(path);
            XmlNodeList node1 = SelectNodesInXml(xmlDoc, "//DeviceInfos");
            XmlNodeList node2 = SelectNodesInXml(xmlDoc, "//ServiceRecord");
            XmlNodeList nodes = node1.Count > 0 ? node1 : node2;
            stepName.Log(Status.Info, "To Start the checking of overall Azure data");
            foreach (XmlNode node in nodes)
            {
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    var data = node.ChildNodes[i].Name;
                    var data2 = node.ChildNodes[i].InnerText;

                    if (string.IsNullOrEmpty(data2))
                    {
                        stepName.Log(Status.Info, $"{data}={data2}");
                    }
                    else
                    {
                        stepName.Log(Status.Pass, $"{data}={data2}");
                    }
                }
            }
            stepName.Log(Status.Info, "To Completed the verification of overall Azure data");
            stepName.Log(Status.Info, "To start the Comparing of S&R Device Information data and Azure data");
            for (int i = startIndex; i <= endIndex - 1; i++)
            {
                string lableValue = textValues[i];
                string textValue = textboxValues[i - startIndex];
                foreach (XmlNode node in nodes)
                {
                    for (int j = 0; j < node.ChildNodes.Count; j++)
                    {
                        var nodeName = node.ChildNodes[j].Name;
                        string nodeValue = node.ChildNodes[j].InnerText;
                        if (nodeName == "PLCode")
                        {
                            nodeName = "PrivateLabel";
                            if (nodeValue == "0")
                            {
                                nodeValue = "No";
                            }
                        }
                        else if (nodeName == "FSWVersion")
                        {
                            nodeName = "FittingSoftware";
                        }
                        else if (nodeName == "FinalTestProgram")
                        {
                            nodeName = "TestProgram";
                        }
                        else if (nodeName == "FinalTestStation")
                        {
                            nodeName = "TestStation";
                        }
                        else if (nodeName == "HybridSerialNumber")
                        {
                            nodeName = "HybridS/N";
                        }
                        else if (nodeName == "FinalTestSite")
                        {
                            nodeName = "TestSite";
                        }
                        else if (nodeName == "CloudID")
                        {
                            nodeName = "CloudHIID";
                        }
                        else if (nodeName == "HasPushButton")
                        {
                            nodeName = "PushButton";
                            if (nodeValue == "True")
                            {
                                nodeValue = "Yes";
                            }
                        }
                        if (nodeName == lableValue.Replace(" ", ""))
{
     if (nodeName == "FirmwareVersion"||nodeName== "BatteryLevel")
     {
         stepName.Log(Status.Info, $"S&R value = {lableValue}={textValue} => Azure value = {nodeName}={nodeValue}");
     }
     else
     {
         if (textValue == nodeValue)
         {
             stepName.Log(Status.Pass, $"S&R value = {lableValue}={textValue} => Azure value = {nodeName}={nodeValue}");
         }
         else
         {
             stepName.Log(Status.Fail, $"S&R value = {lableValue}={textValue} => Azure value = {nodeName}={nodeValue}");
         }
     }
}
                    }
                }
            }
            stepName.Log(Status.Info, "Azure Xml data and S&R Device Info details Compared Successfully");
            string File;
            string File2;
            string path2 = textDir + "\\azurefiles";
            string[] fyles3 = Directory.GetFileSystemEntries(path2, "*", SearchOption.AllDirectories);
            Console.WriteLine(String.Join(System.Environment.NewLine, fyles3));
            string file3 = String.Join(System.Environment.NewLine, fyles3[0]);
            // Check if file is there    
            if (fyles3.Count() > 1)
            {
                File = String.Join(System.Environment.NewLine, fyles3[1]);
                File2 = file3;
            }
            else
            {
                File = file3;
                File2 = null;
            }
            System.IO.FileInfo anyfile1 = new System.IO.FileInfo(File);
            anyfile1.Delete();
            if (File2 != null)
            {
                System.IO.DirectoryInfo anyfile = new DirectoryInfo(File2);
                anyfile.Delete();
            }
        }
        private void ExtractZipFile(string zipFilePath, string extractPath)
        {
            ZipFile.ExtractToDirectory(zipFilePath, extractPath);
            Console.WriteLine("ZIP file extracted successfully.");
            System.IO.FileInfo rarfile = new System.IO.FileInfo(zipFilePath);
            if (rarfile.Exists)
            {
                rarfile.Delete();
            }
        }
    }
}
