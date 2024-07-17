﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace AppiumWinApp.FeatureFiles
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("SandRInterations")]
    public partial class SandRInterationsFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
#line 1 "SandRIntegrations.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "FeatureFiles", "SandRInterations", "A short summary of the feature", ProgrammingLanguage.CSharp, featureTags);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<NUnit.Framework.TestContext>(NUnit.Framework.TestContext.CurrentContext);
        }
        
        public void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("01TestCaseID 1703234 Verify that a connection string with a format other than Bas" +
            "e64 is not acceptable.")]
        [NUnit.Framework.TestCaseAttribute("VA", "rtikertgey", "Connection string must be Base64 encoded", null)]
        public void _01TestCaseID1703234VerifyThatAConnectionStringWithAFormatOtherThanBase64IsNotAcceptable_(string systemRole, string invalidBase64Value, string message, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("SystemRole", systemRole);
            argumentsOfScenario.Add("InvalidBase64Value", invalidBase64Value);
            argumentsOfScenario.Add("Message", message);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("01TestCaseID 1703234 Verify that a connection string with a format other than Bas" +
                    "e64 is not acceptable.", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 6
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 8
    testRunner.Given("[Launch SandRTool]", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 9
 testRunner.When(string.Format("[Navigate to settings tab and set the system role to \"{0}\"]", systemRole), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 10
 testRunner.Then(string.Format("[Click on set sales order connection string and input the invalid base string \"{0" +
                            "}\"]", invalidBase64Value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 11
 testRunner.Then(string.Format("[Verify the error message on the connection string pop-up window \"{0}\"]", message), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 12
 testRunner.Then(string.Format("[Verify that a \"{0}\" string in the user config file in the local machine]", invalidBase64Value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("02TestCaseID 1703237 Verify that a valid string in the user config file in the lo" +
            "cal machine")]
        [NUnit.Framework.TestCaseAttribute("VA", "lkjhgfdunukg", null)]
        public void _02TestCaseID1703237VerifyThatAValidStringInTheUserConfigFileInTheLocalMachine(string systemRole, string validBase64Value, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("SystemRole", systemRole);
            argumentsOfScenario.Add("ValidBase64Value", validBase64Value);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("02TestCaseID 1703237 Verify that a valid string in the user config file in the lo" +
                    "cal machine", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 20
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 22
     testRunner.Given("[Launch SandRTool]", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 23
     testRunner.When(string.Format("[Navigate to settings tab and set the system role to \"{0}\"]", systemRole), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 24
  testRunner.Then(string.Format("[Click on set sales order connection string and input the valid base string \"{0}\"" +
                            "]", validBase64Value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 25
  testRunner.Then(string.Format("[Verify that a \"{0}\" string in the user config file in the local machine]", validBase64Value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("03TestCaseID 1703238 Verify that a connection string is overridden when a valid c" +
            "onnection string in input")]
        [NUnit.Framework.TestCaseAttribute("VA", "lokjhtrevbiarfjh", "mfsvywnf", null)]
        public void _03TestCaseID1703238VerifyThatAConnectionStringIsOverriddenWhenAValidConnectionStringInInput(string systemRole, string validBase64Value1, string validBase64Value2, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("SystemRole", systemRole);
            argumentsOfScenario.Add("ValidBase64Value1", validBase64Value1);
            argumentsOfScenario.Add("ValidBase64Value2", validBase64Value2);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("03TestCaseID 1703238 Verify that a connection string is overridden when a valid c" +
                    "onnection string in input", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 33
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 35
     testRunner.Given("[Launch SandRTool]", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 36
     testRunner.When(string.Format("[Navigate to settings tab and set the system role to \"{0}\"]", systemRole), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 37
  testRunner.Then(string.Format("[Click on set sales order connection string and input the valid base string \"{0}\"" +
                            "]", validBase64Value1), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 38
  testRunner.Then(string.Format("[Verify that a \"{0}\" string in the user config file in the local machine]", validBase64Value1), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 39
  testRunner.Then(string.Format("[Click on set sales order connection string and input the valid base string \"{0}\"" +
                            "]", validBase64Value2), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 40
  testRunner.Then(string.Format("[Verify that a \"{0}\" string in the user config file in the local machine]", validBase64Value2), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("04TestCaseID 1703240 Verify that Set Sales Order Connection String feature is ava" +
            "ilable only for VA role")]
        [NUnit.Framework.TestCaseAttribute("VA", "Repairs", null)]
        public void _04TestCaseID1703240VerifyThatSetSalesOrderConnectionStringFeatureIsAvailableOnlyForVARole(string systemRole1, string systemRole2, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("SystemRole1", systemRole1);
            argumentsOfScenario.Add("SystemRole2", systemRole2);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("04TestCaseID 1703240 Verify that Set Sales Order Connection String feature is ava" +
                    "ilable only for VA role", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 47
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 49
     testRunner.Given("[Launch SandRTool]", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 50
  testRunner.When(string.Format("[Navigate to settings tab and set the system role to \"{0}\"]", systemRole1), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 51
  testRunner.Then("[Verify the visibility of connection string]", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 52
  testRunner.When(string.Format("[Navigate to settings tab and set the system role to \"{0}\"]", systemRole2), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 53
  testRunner.Then("[Verify the visibility of connection string other than VA system role]", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("05TestCaseID 1696030 Inputting, Validating, and Retaining Connection String")]
        [NUnit.Framework.TestCaseAttribute("VA", "malounbeopwnksymclau", "Set sales order connection string", null)]
        public void _05TestCaseID1696030InputtingValidatingAndRetainingConnectionString(string systemRole, string validBase64Value, string connectionStringButton, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("SystemRole", systemRole);
            argumentsOfScenario.Add("ValidBase64Value", validBase64Value);
            argumentsOfScenario.Add("ConnectionStringButton", connectionStringButton);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("05TestCaseID 1696030 Inputting, Validating, and Retaining Connection String", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 60
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 62
      testRunner.Given("[Launch SandRTool]", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 63
   testRunner.When(string.Format("[Navigate to settings tab and set the system role to \"{0}\"]", systemRole), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 64
   testRunner.Then(string.Format("[Click on set sales order connection string and input the valid base string \"{0}\"" +
                            "]", validBase64Value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 65
   testRunner.Then(string.Format("[Verify that a \"{0}\" string in the user config file in the local machine]", validBase64Value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 66
   testRunner.When("[Uninstall the current S&R Tool]", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 67
   testRunner.When("[Install the latest S&R Tool]", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 68
   testRunner.Given("[Launch SandRTool]", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 69
   testRunner.Then(string.Format("[Validate the previous SystemRole \"{0}\" and valid base connection string \"{1}\" an" +
                            "d \"{2}\" is preserved to latest S&R]", systemRole, validBase64Value, connectionStringButton), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 70
   testRunner.Then(string.Format("[Verify that a \"{0}\" string in the user config file in the local machine]", validBase64Value), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
