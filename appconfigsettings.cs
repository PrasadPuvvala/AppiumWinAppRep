using System;
using System.Collections.Generic;
using System.Text;

namespace AppiumWinApp
{
    public class appconfigsettings
    {
        public Algo algo { get; set; }
        public SLV slv { get; set; }
        public WorkingDirectory workingdirectory { get; set; }
        public ApplicationPath ApplicationPath { get; set; }
        public TestEnvironment TestEnvironment { get; set; }
        public ConnectionStringPath connectionStringPath { get; set; }
        public SandRToolUninstallation sandRToolUninstallation { get; set; }
        public SandRDownloadLinkUpdateParameters sandRDownloadLinkUpdateParameters { get; set; }
    }

    public class Algo

    {
        public string Palpatine6 { get; set; } = String.Empty;
        public string Dooku1 { get; set; } = String.Empty;
        public string Dooku2 { get; set; } = String.Empty;
        public string Dooku3 { get; set; } = String.Empty;
        public string Megnesium { get; set; } = String.Empty;
    }

    public class SLV
    {
        public string Palpatine6 { get; set; } = String.Empty;
        public string Dooku1 { get; set; } = String.Empty;
        public string Dooku2 { get; set; } = String.Empty;
        public string Dooku3 { get; set; } = String.Empty;
        public string Megnesium { get; set; } = String.Empty;
    }

    public class WorkingDirectory
    {
        public string Palpatine6 { get; set; } = String.Empty;
        public string Dooku1 { get; set; } = String.Empty;
        public string Dooku2 { get; set; } = String.Empty;
        public string Dooku3 { get; set; } = String.Empty;
        public string Megnesium { get; set; } = String.Empty;
        public string FDTS { get; set; } = String.Empty;
        public string SandR { get; set; } = String.Empty;
        public string TestRuntime { get; set; } = String.Empty;
        public string HiRegistration { get; set; } = String.Empty;
        public string FSWWorkingPath { get; set; } = String.Empty;
    }
    public class ApplicationPath
    {
        public string FDTSAppPath { get; set; } = string.Empty;
        public string SandRAppPath { get; set; } = string.Empty;
        public string FSWAppPath { get; set; } = string.Empty;
        public string TestRuntimePC { get; set; } = string.Empty;
        public string SmartFitAppPath { get; set; } = string.Empty;
        public string HiRegistrationPath { get; set; } = string.Empty;
    }
    public class TestEnvironment
    {
        public string WinappDriverUrl { get; set; } = string.Empty;
        public string WinAppDriverPath { get; set; } = string.Empty;
    }
    public class ConnectionStringPath
    {
        public string PathtoValue { get; set; } = string.Empty;
    }
    public class SandRToolUninstallation
    {
        public string UninstallKeyWow64 { get; set; } = string.Empty;
    }
    public class SandRDownloadLinkUpdateParameters
    {
        public string Build { get; set; } = string.Empty;
        public string Beta { get; set; } = string.Empty;
    }
}