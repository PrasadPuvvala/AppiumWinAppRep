using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Text;
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
using OpenQA.Selenium.Appium;
using com.sun.xml.@internal.bind.v2.model.core;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using com.sun.rowset.@internal;
using How = SeleniumExtras.PageObjects.How;

namespace AppiumWinApp.PageFactory
{
    public static class WorkFlowPageFactory
    {
        

        [SeleniumExtras.PageObjects.FindsBy(How = How.Id, Using = "logout")]
        private static IWebElement logoutElement;

        /*WorkFlow Product Selection*/

        public static By workFlowProductSelection = MobileBy.AccessibilityId("treeView");

        /*File Menu*/
        public static By fileMenu = By.XPath("(//*[@AutomationId= 'MainForm' ])//*[@Name='File']"); 
        
        /*Channel Menu*/
        public static By channel = By.XPath("(//*[@AutomationId= 'MainForm' ])//*[@Name='Channel']");

        /*Click on Speedlink Channel Menu*/

        public static By inter = By.XPath("(//*[@AutomationId='MainForm'])//*[@Name='Channel']//*[@LocalizedControlType='menu item'][3]");

        /*Click on Speedlink Channel Menu*/

        public static By domainInterface = By.XPath("(//*[@AutomationId='MainForm'])//*[@Name='Channel']//*[@LocalizedControlType='menu item'][2]");

        /*Click on Interface under channel*/

        public static By InterfaceSel = By.Name("SpeedLink:0");

        /*Click on Read HI*/
        public static By readHI = By.XPath("(//*[@AutomationId='MainForm'])//*[@Name='File']//*[@LocalizedControlType='menu item'][2]");

        /*Click on Write HI*/
        public static By writeHI = By.XPath("(//*[@AutomationId='MainForm'])//*[@Name='File']//*[@LocalizedControlType='menu item'][3]");

        /*Click on Read HI*/
        public static By checkNodes = By.XPath("(//*[@AutomationId='MainForm'])//*[@Name='File']//*[@LocalizedControlType='menu item'][8]");

        /*Click on Dump HI*/

        public static By dumpHI = By.XPath("(//*[@AutomationId='MainForm'])//*[@Name='File']//*[@LocalizedControlType='menu item'][6]");

        /*Click on Dump HI for P6*/

        public static By dumpP6HI = By.XPath("(//*[@AutomationId='MainForm'])//*[@Name='File']//*[@LocalizedControlType='menu item'][5]");

        /*Work Flow Filter Text Box*/
        public static By filterBox = MobileBy.AccessibilityId("textBoxFilter");

        /* Click on DiscoveryFailed window */
        public static By FDTSdiscoverywindow = MobileBy.ClassName("WindowsForms10.STATIC.app.0.27a2811_r7_ad1");

        /*Work Flow serialnumber Text Box*/
        public static By serialNumbertextBox = MobileBy.AccessibilityId("SerialNumberTextBox");



        



    }

    public partial class SystemSettings
    {
        public SystemSettings() { }

        public static SystemSettings Default { get; set; }




    }
}
