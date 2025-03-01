Feature: SandRInterations

A short summary of the feature


Scenario: 01Test Case ID 1703234: S&R [VA] -  Verify that a connection string with a format other than Base64 is not acceptable.


    Given [Launch SandRTool]
	When [Navigate to settings tab and set the system role to "<SystemRole>"]
	Then [Click on set sales order connection string and input the invalid base string "<InvalidBase64Value>"]
	Then [Verify the error message on the connection string pop-up window "<Message>"]
	Then [Verify that a "<InvalidBase64Value>" string in the user config file in the local machine]

	Examples: 

	| SystemRole | InvalidBase64Value | Message                                  |
	|     VA     | rtikertgey         | Connection string must be Base64 encoded |



Scenario: 02Test Case ID 1703237: S&R [VA] -  Verify that a valid connection string is saved in the user config file in the local machine


     Given [Launch SandRTool]
     When [Navigate to settings tab and set the system role to "<SystemRole>"]
	 Then [Click on set sales order connection string and input the valid base string "<ValidBase64Value>"]
	 Then [Verify that a "<ValidBase64Value>" string in the user config file in the local machine]

	 Examples: 

	 | SystemRole | ValidBase64Value |
	 |     VA     | lkjhgfdunukg     |



Scenario: 03Test Case ID 1703238: S&R [VA] -  Verify that a connection string is overridden when a new valid connection string is input.


     Given [Launch SandRTool]
    When [Navigate to settings tab and set the system role to "<SystemRole>"]
	 Then [Click on set sales order connection string and input the valid base string "<ValidBase64Value1>"]
	 Then [Verify that a "<ValidBase64Value1>" string in the user config file in the local machine]
	 Then [Click on set sales order connection string and input the valid base string "<ValidBase64Value2>"]
	 Then [Verify that a "<ValidBase64Value2>" string in the user config file in the local machine]

	 Examples: 
	 | SystemRole | ValidBase64Value1 | ValidBase64Value2 |
	 |     VA     | lokjhtrevbiarfjh  | mfsvywnf          |


Scenario: 04Test Case ID 1703240: S&R [VA] -  Verify that Set Sales Order Connection String feature is available only for VA role

     Given [Launch SandRTool]
	 When [Navigate to settings tab and set the system role to "<SystemRole1>"]
	 Then [Verify the visibility of connection string]
	 When [Navigate to settings tab and set the system role to "<SystemRole2>"]
	 Then [Verify the visibility of connection string other than VA system role]

	 Examples: 
	 | SystemRole1 | SystemRole2 |
	 |      VA     |   Repairs   |



Scenario: 05Test Case ID 1696030: S&R [VA] - Inputting, Validating, and Retaining Connection String.


      Given [Launch SandRTool]
	  When [Navigate to settings tab and set the system role to "<SystemRole>"]
	  Then [Click on set sales order connection string and input the valid base string "<ValidBase64Value>"]
	  Then [Verify that a "<ValidBase64Value>" string in the user config file in the local machine]
	  When [Uninstall the current S&R Tool]
	  When [Install the latest S&R Tool]
	  Given [Launch SandRTool]
	  Then [Validate the previous SystemRole "<SystemRole>" and valid base connection string "<ValidBase64Value>" and "<ConnectionStringButton>" is preserved to latest S&R]
	  Then [Verify that a "<ValidBase64Value>" string in the user config file in the local machine]

	  Examples: 
	 | SystemRole | ValidBase64Value     |     ConnectionStringButton        |
	 | VA         | malounbeopwnksymclau | Set sales order connection string |
