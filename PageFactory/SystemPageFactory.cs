using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using javax.swing.plaf;
using System.IO;
using RazorEngine.Compilation.ImpromptuInterface.Dynamic;
using OpenQA.Selenium.Appium.iOS;
using sun.security.util;
using TestStack.BDDfy.Annotations;
using OpenQA.Selenium.Interactions;
using Microsoft.SqlServer.Management.XEvent;
using System.Linq;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace AppiumWinApp.PageFactory
{

    public class SystemPageFactory
    {
        protected static WindowsDriver<WindowsElement> session;
        private const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        private static ExtentReports extent;
        private static ExtentHtmlReporter htmlReporter;
        private static ExtentTest test;
        public static String textDir = Directory.GetCurrentDirectory();

        public static void launchSystemSettings(String sideSelection, ExtentReports extent1, ExtentTest stepName)
        {
            extent = extent1;
            String ApplicationPath = "C:\\Program Files (x86)\\GN Hearing\\Camelot\\System Configuration\\Camelot.SystemConfiguration.exe";
            Thread.Sleep(2000);
            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", ApplicationPath);
            appCapabilities.SetCapability("deviceName", "WindowsPC");
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            Thread.Sleep(2000);
            standardElements.test(session);
            SystemConfigurationSettings.systemConfig(session);
            systemSettingsPage.changeChannel(session, sideSelection);
            stepName.Log(Status.Pass, "Systems settings are set ");

        }

        public static void launchSystemSettingsServiceGROC(ExtentReports extent1, ExtentTest stepName)
        {
            extent = extent1;
            String ApplicationPath = "C:\\Program Files (x86)\\GN Hearing\\Camelot\\System Configuration\\Camelot.SystemConfiguration.exe";
            Thread.Sleep(2000);
            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", ApplicationPath);
            appCapabilities.SetCapability("deviceName", "WindowsPC");
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            Thread.Sleep(2000);
            standardElements.test(session);
            SystemConfigurationSettings.systemConfig(session);
            systemSettingsPage.ServiceGROC(session);
            //systemSettingsPage.changeChannel(session, sideSelection);

            stepName.Log(Status.Pass, "Basic settings are set Service GROC");
        }

        public static void launchSystemSettingsDevelopmentAndVerification(ExtentReports extent1, ExtentTest stepName)
        {
            extent = extent1;
            String ApplicationPath = "C:\\Program Files (x86)\\GN Hearing\\Camelot\\System Configuration\\Camelot.SystemConfiguration.exe";
            Thread.Sleep(2000);
            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", ApplicationPath);
            appCapabilities.SetCapability("deviceName", "WindowsPC");
            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            Thread.Sleep(2000);
            standardElements.test(session);
            SystemConfigurationSettings.systemConfig(session);
            systemSettingsPage.DevelopmentAndVerification(session);
            //systemSettingsPage.changeChannel(session, sideSelection);

            stepName.Log(Status.Pass, "Basic settings are set Development and Verification");
        }

        public partial class testElements : SystemPageFactory
        {
            public static WindowsElement password = session.FindElementByAccessibilityId("textBoxPassword");
            public static WindowsElement continueButton = session.FindElementByAccessibilityId("buttonContinue");
        }

        public partial class standardElements : testElements
        {
            public static void test(WindowsDriver<WindowsElement> session)
            {
                Thread.Sleep(4000);
                FunctionLibrary lib = new FunctionLibrary();
                lib.clickOnElementWithIdonly(session, "textBoxPassword");
                password = session.FindElementByAccessibilityId("textBoxPassword");
                continueButton = session.FindElementByAccessibilityId("buttonContinue");
                password.SendKeys("1234");
                continueButton.Click();
                Thread.Sleep(2000);
                session.SwitchTo().Window(session.WindowHandles.First());
                session.SwitchTo().ActiveElement();
            }


        } /*standard ELements*/

        public partial class SystemConfigurationSettings
        {

            public static WindowsElement systemSettings = session.FindElementByAccessibilityId("textBoxSystemSettings");
            public static void systemConfig(WindowsDriver<WindowsElement> session)
            {
                FunctionLibrary lib = new FunctionLibrary();
                lib.clickOnElementWithIdonly(session, "textBoxSystemSettings");
                session.SwitchTo().Window(session.WindowHandles.First());
                session.SwitchTo().ActiveElement();
                Thread.Sleep(4000);
            }


        }//SystemConfigurationSettings


        public partial class systemSettingsPage
        {

            public static WindowsElement commDevice = session.FindElementByName("Communication Device");
            public static WindowsElement commChannel = session.FindElementByClassName("ComboBox");
            public static WindowsElement commSide = session.FindElementByClassName("ListBoxItem");
            public static WindowsElement Leftside = session.FindElementByName("Left");
            public static WindowsElement RightSide = session.FindElementByName("Right");
            public static WindowsElement closeBtn = session.FindElementByAccessibilityId("Close");
            public static void changeChannel(WindowsDriver<WindowsElement> session, string sideSel)
            {
                FunctionLibrary lib = new FunctionLibrary();
                var expand = session.FindElementsByAccessibilityId("Expander");
                expand[3].Click();
                Thread.Sleep(2000);
                var combo = session.FindElementsByClassName("ComboBox");
                foreach (var element in combo)
                {
                    String txt1 = element.GetAttribute("Name");
                }

                combo[1].Click();
                var item = session.FindElementsByClassName("ListBoxItem");
                String txt = null;

                if (sideSel.Contains("Left"))
                {
                    txt = item[1].GetAttribute("Name"); //right
                    Thread.Sleep(2000);
                    session.Keyboard.PressKey(Keys.ArrowUp);
                    Thread.Sleep(2000);
                    session.Keyboard.PressKey(Keys.Enter);
                    Thread.Sleep(2000);
                    expand[3].Click();
                    Thread.Sleep(2000);
                }
                else
                {
                    txt = item[0].GetAttribute("Name"); //left
                    Thread.Sleep(2000);
                    session.Keyboard.PressKey(Keys.ArrowDown);
                    Thread.Sleep(2000);
                    session.Keyboard.PressKey(Keys.Enter);
                    Thread.Sleep(2000);
                    expand[3].Click();
                    Thread.Sleep(2000);
                }

                /*Close the System Configuration Window*/
                var items = session.FindElementsByClassName("Button");
                items[1].Click();
                Thread.Sleep(2000);

                /*Close the System Settings Window*/
                items[0].Click();

            }

            public static void ServiceGROC(WindowsDriver<WindowsElement> session)
            {
                session.FindElementByName("Basic Settings").FindElementByAccessibilityId("Expander").Click();
                var combo = session.FindElementsByClassName("ComboBox");
                foreach (var item in combo)
                {
                    if (item.Text == "Service GROC")
                    {
                        var items = session.FindElementsByClassName("Button");
                        items[1].Click();
                        Thread.Sleep(2000);

                        /*Close the System Settings Window*/
                        items[0].Click();
                        break;
                    }
                    else
                    {
                        combo[0].Click();
                        string developmentandVerification_Xpath = "//*[@Name='Service GROC']";
                        WindowsElement comboBoxItem = session.FindElement(By.XPath(developmentandVerification_Xpath));
                        Actions actionscomboBoxItem = new Actions(session);
                        actionscomboBoxItem.MoveToElement(comboBoxItem).Click().Perform();
                        var items = session.FindElementsByClassName("Button");
                        items[1].Click();
                        Thread.Sleep(2000);

                        /*Close the System Settings Window*/
                        items[0].Click();
                        break;
                    }
                }
            }

            public static void DevelopmentAndVerification(WindowsDriver<WindowsElement> session)
            {
                session.FindElementByName("Basic Settings").FindElementByAccessibilityId("Expander").Click();
                var combo = session.FindElementsByClassName("ComboBox");

                foreach( var item in combo )
                {
                    if(item.Text=="Development and Verification")
                    {
                        var items = session.FindElementsByClassName("Button");
                        items[1].Click();
                        Thread.Sleep(2000);

                        /*Close the System Settings Window*/
                        items[0].Click();
                        break;
                    }
                    else
                    {
                        combo[0].Click();
                        string developmentandVerification_Xpath = "//*[@Name='Development and Verification']";
                        WindowsElement comboBoxItem = session.FindElement(By.XPath(developmentandVerification_Xpath));
                        Actions actionscomboBoxItem = new Actions(session);
                        actionscomboBoxItem.MoveToElement(comboBoxItem).Click().Perform();
                        var items = session.FindElementsByClassName("Button");
                        items[1].Click();
                        Thread.Sleep(2000);

                        /*Close the System Settings Window*/
                        items[0].Click();
                        break;
                    }
                }    
            }
        }

    } /*Internal class*/
}




