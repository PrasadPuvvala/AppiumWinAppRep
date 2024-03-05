Feature: SandRWorkFlow

A short summary of the feature

@tag1
Scenario Outline: 01Test Case ID 1537268: Verify that battery ADL data is restored on original device
	
	Given Lauch socket Driver "<DeviceId>"and"<Devicetype>"
	#Given [Change channel side in FDTS<DeviceLeft>]
	#Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceLeftSlNo>" and "<FlashHI>" and "<DeviceLeft>"and"<Devicetype>"
	When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"]
 	When [Verify StorageLayout Scenario By Changing Date and Confirm Cloud Icon "<DeviceId>" and "<DeviceLeft>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
	When [Cleaning up Capture and Restore Reports Before Launch SandR]
	When [Change communication channel in S and R<DeviceLeft>]
	When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
	When [Go to Device Info tab and capture device info in excel then verify the device information is shown correctly "<Devicetype>"]
	When [Come back to Settings and wait till controls enabled]
	When [Perform Capture"<DeviceId>"and"<Devicetype>"]
	When [Go to logs and verify capturing time]
	#When [Launch algo and alter ADL value "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
	When [Perform Restore with above captured image "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
	#Given [Change channel side in FDTS<DeviceLeft>]    Added For D2 Family
	#Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceLeftSlNo>" and "<FlashHI>" and "<DeviceLeft>"and"<Devicetype>" Added For D2 Family
	#When [Launch algo lab and check the ADL value "<DeviceId>" and "<DeviceLeftSlNo>"and "<DeviceLeftSlNo>"]
	When [Go to log file for verifying Restore time] 
	And  [Open Capture and Restore report and log info in report]
	Then [done]
	
	Examples:

	 | DeviceId  | DeviceLeftSlNo | FlashHI | DeviceRight | DeviceLeft |  Devicetype      |
    #| LT961-DRW-UP | 2000800436   | Yes     | Right       | Left      |     Wired        |
     #| RE962-DRW |  1900812197    | Yes     | Right       | Left       |     Wired        |
	#| RE962-DRWT | 2000803069     | Yes     | Right       | Left      |     Wired        |
 	#| RT962-DRW | 2000800246    | Yes     | Right       | Left        |Non-Rechargeable  |
	#| LT988-DW | 1700800149     | Yes     | Right       | Left        |    Wired         |
	 #| RT961-DRWC | 2000816936     | Yes     | Right       | Left      |Rechargeable      |	
	 | NX977-DWC | 2300806615     | Yes     | Right       | Left       | Rechargeable     |
	#| NX960S-DRWC | 2300807477     | Yes     | Right    | Left        |Rechargeable      |
	#| RU960-DRWC | 4483181561     | Yes     | Right       | Left      |Rechargeable      |
	#| RE961-DRWC | 4483181561     | Yes     | Right       | Left      |D1rechageableWired|

@tag2
Scenario Outline: 02Test Case ID 1103972: Verify device information is shown correctly

	
	#When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"]
	When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
	When [Go to Device Info tab and capture device info in excel then verify the device information is shown correctly "<Devicetype>"]
	When [Come back to Settings and wait till controls enabled]
	Then [Close SandR tool]
	


	Examples:
	 | DeviceId  | DeviceLeftSlNo | FlashHI | DeviceRight | DeviceLeft |  Devicetype      |
    #| LT961-DRW-UP | 2000800436   | Yes     | Right       | Left      |     Wired        |
     #| RE962-DRW |  1900812195    | Yes     | Right       | Left       |     Wired        |
	#| RE962-DRWT | 2000803069     | Yes     | Right       | Left      |     Wired        |
 	#| RT962-DRW | 2000800246    | Yes     | Right       | Left        |Non-Rechargeable  |
	#| LT988-DW | 1700800149     | Yes     | Right       | Left        |    Wired         |
	#| RT961-DRWC | 2000816936     | Yes     | Right       | Left      |Rechargeable      |	
	 | NX977-DWC | 2300806615     | Yes     | Right       | Left       | Rechargeable     |
	#| NX960S-DRWC | 2300807477     | Yes     | Right    | Left        |Rechargeable      |
	#| RU960-DRWC | 4483181561     | Yes     | Right       | Left      |Rechargeable      |
	#| RE961-DRWC | 4483181561     | Yes     | Right       | Left      |D1rechageableWired|




@tag3

Scenario Outline: 03Test Case ID 1105474: Verify capture operation is performed within desired time

    #When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"] #Added for D2 Family
	When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
	When [Come back to Settings and wait till controls enabled]
	When [Perform Capture"<DeviceId>"and"<Devicetype>"]
	When [Go to logs and verify capturing time]

	Examples:
	 | DeviceId  | DeviceLeftSlNo | FlashHI | DeviceRight | DeviceLeft |  Devicetype      |
    #| LT961-DRW-UP | 2000800436   | Yes     | Right       | Left      |     Wired        |
     #| RE962-DRW |  1900812197    | Yes     | Right       | Left       |     Wired        |
	#| RE962-DRWT | 2000803069     | Yes     | Right       | Left      |     Wired        |
 	#| RT962-DRW | 2000800246    | Yes     | Right       | Left        |Non-Rechargeable  |
	#| LT988-DW | 1700800149     | Yes     | Right       | Left        |    Wired         |
	#| RT961-DRWC | 2000816936     | Yes     | Right       | Left      |Rechargeable      |	
	 | NX977-DWC | 2300806615     | Yes     | Right       | Left       | Rechargeable     |
	#| NX960S-DRWC | 2300807477     | Yes     | Right    | Left        |Rechargeable      |
	#| RU960-DRWC | 4483181561     | Yes     | Right       | Left      |Rechargeable      |
	#| RE961-DRWC | 4483181561     | Yes     | Right       | Left      |D1rechageableWired|

@tag4

Scenario Outline: 04Test Case ID 1103482: Verify supported PC configuration

	#When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"] #Added for D2 Family
	When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
	When [Come back to Settings and wait till controls enabled]
	When [Perform Capture"<DeviceId>"and"<Devicetype>"]
	#When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"] #Added for D2 Family
	When [Perform Restore with above captured image "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
	#Given [Change channel side in FDTS<DeviceLeft>]   #Added for D2 Family
	#Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceLeftSlNo>" and "<FlashHI>" and "<DeviceLeft>"and"<Devicetype>"  #Added for D2 Family

	Examples:

	| DeviceId  | DeviceLeftSlNo | FlashHI | DeviceRight | DeviceLeft |  Devicetype      |
    #| LT961-DRW-UP | 2000800436   | Yes     | Right       | Left      |     Wired        |
     #| RE962-DRW |  1900812197    | Yes     | Right       | Left       |     Wired        |
	#| RE962-DRWT | 2000803069     | Yes     | Right       | Left      |     Wired        |
 	#| RT962-DRW | 2000800246    | Yes     | Right       | Left        |Non-Rechargeable  |
	#| LT988-DW | 1700800149     | Yes     | Right       | Left        |    Wired         |
	#| RT961-DRWC | 2000816936     | Yes     | Right       | Left      |Rechargeable      |	
	 | NX977-DWC | 2300806615     | Yes     | Right       | Left       | Rechargeable     |
	#| NX960S-DRWC | 2300807477     | Yes     | Right    | Left        |Rechargeable      |
	#| RU960-DRWC | 4483181561     | Yes     | Right       | Left      |Rechargeable      |
	#| RE961-DRWC | 4483181561     | Yes     | Right       | Left      |D1rechageableWired|


@tag5

Scenario Outline: 05Test Case ID 1103833: Verify channel can be changed while S&R tool is running

	#When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"] #Added for D2 Family
	When [Change communication channel in S and R<DeviceLeft>]

	Examples:
	
	| DeviceId  | DeviceLeftSlNo | FlashHI | DeviceRight | DeviceLeft |  Devicetype      |
    #| LT961-DRW-UP | 2000800436   | Yes     | Right       | Left      |     Wired        |
     #| RE962-DRW |  1900812197    | Yes     | Right       | Left       |     Wired        |
	#| RE962-DRWT | 2000803069     | Yes     | Right       | Left      |     Wired        |
 	#| RT962-DRW | 2000800246    | Yes     | Right       | Left        |Non-Rechargeable  |
	#| LT988-DW | 1700800149     | Yes     | Right       | Left        |    Wired         |
	#| RT961-DRWC | 2000816936     | Yes     | Right       | Left      |Rechargeable      |	
	 | NX977-DWC | 2300806615     | Yes     | Right       | Left       | Rechargeable     |
	#| NX960S-DRWC | 2300807477     | Yes     | Right    | Left        |Rechargeable      |
	#| RU960-DRWC | 4483181561     | Yes     | Right       | Left      |Rechargeable      |
	#| RE961-DRWC | 4483181561     | Yes     | Right       | Left      |D1rechageableWired|

	
	



@tag6

Scenario Outline: 06Test Case ID 1104002: Verify HI capture/restoration report

	#When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"] #Added for D2 Family
	When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
	When [Cleaning up Capture and Restore Reports Before Launch SandR]
	When [Go to Device Info tab and capture device info in excel then verify the device information is shown correctly "<Devicetype>"]
	When [Come back to Settings and wait till controls enabled]
	When [Perform Capture"<DeviceId>"and"<Devicetype>"]
	#When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"]#Added for D2 Family
	When [Perform Restore with above captured image "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
	#Given [Change channel side in FDTS<DeviceLeft>]  #Added for D2 Family
	#Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceLeftSlNo>" and "<FlashHI>" and "<DeviceLeft>"and"<Devicetype>"  #Added for D2 Family
	#When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"]  #Added for D2 Family
	And  [Open Capture and Restore report and log info in report]

	Examples:
	
	| DeviceId  | DeviceLeftSlNo | FlashHI | DeviceRight | DeviceLeft |  Devicetype      |
    #| LT961-DRW-UP | 2000800436   | Yes     | Right       | Left      |     Wired        |
     #| RE962-DRW |  1900812197    | Yes     | Right       | Left       |     Wired        |
	#| RE962-DRWT | 2000803069     | Yes     | Right       | Left      |     Wired        |
 	#| RT962-DRW | 2000800246    | Yes     | Right       | Left        |Non-Rechargeable  |
	#| LT988-DW | 1700800149     | Yes     | Right       | Left        |    Wired         |
	 #| RT961-DRWC | 2000816936     | Yes     | Right       | Left      |Rechargeable      |	
	 | NX977-DWC | 2300806615     | Yes     | Right       | Left       | Rechargeable     |
	#| NX960S-DRWC | 2300807477     | Yes     | Right    | Left        |Rechargeable      |
	#| RU960-DRWC | 4483181561     | Yes     | Right       | Left      |Rechargeable      |
	#| RE961-DRWC | 4483181561     | Yes     | Right       | Left      |D1rechageableWired|





#@tag8
#Scenario Outline: 08Test Case 1103981: Verify device information is cleared when HI is disconnected
#
#		When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
#		When [Go to Device Info tab and capture device info in excel then verify the device information is shown correctly "<Devicetype>"]
#		When [Come back to Settings and wait till controls enabled]
#		When [Clicks on disconnect and verify device information is cleared "<Devicetype>"]
#
#		Examples:
#
#
#	| DeviceId     | DeviceLeftSlNo | FlashHI | DeviceRight | DeviceLeft | Devicetype       |
#	| LT961-DRW-UP | 2000800436     | Yes     | Right       | Left       | Wired            |
#	| RE962-DRW    | 2026335111     | Yes     | Right       | Left       | Wired            |
#	| RE962-DRWT   | 2000803069     | Yes     | Right       | Left       | Wired            |
#	| RT962-DRW    | 2000800247     | Yes     | Right       | Left       | Non-Rechargeable |
#	| LT988-DW     | 1600806099     | Yes     | Right       | Left       | Wired            |
#	| RT961-DRWC   | 2000801965     | Yes     | Right       | Left       | Rechargeable     |




@tag7

Scenario Outline: 07Test Case ID 1105498: Verify that S&R Tool properly sets listening test settings
		        
				#When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"]  #Added for D2 Family
				#Given [Change channel side in FDTS<DeviceLeft>]     #Added for D2 Family
                #Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceLeftSlNo>" and "<FlashHI>" and "<DeviceLeft>"and"<Devicetype>"   #Added for D2 Family
				#Given [Change channel side in FDTS<DeviceRight>]       #Added for D2 Family
		        #Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceSlNo>" and "<FlashHI>" and "<DeviceRight>"and"<Devicetype>"  #Added for D2 Family
				When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"]
				When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
				When [Go to Device Info tab and capture device info in excel then verify the device information is shown correctly "<Devicetype>"]
				When [Come back to Settings and wait till controls enabled]
				When [Perform Capture with listening test settings]
			    Then [Launch FSW and check the added programs "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"]


				Examples:
				| DeviceId  | DeviceLeftSlNo | FlashHI | DeviceRight | DeviceLeft | DeviceSlNo | Devicetype |
				 #| LT961-DRW-UP | 2000800436   | Yes     | Right       | Left       |1700800900|  Wired    |
				 #| RE962-DRWT | 2000803069     | Yes     | Right       | Left       | 2000803066 |Wired    |
				 #| RE962-DRW |    1900812197  | Yes     | Right       | Left       | 1900812195 |  Wired    |
				 #| RT962-DRW | 2000800246     | Yes     | Right       | Left       | 2000800269 | Non-Rechargeable    |
				  #| LT988-DW | 1700800149     | Yes     | Right       | Left       | 1700800066 |Wired    |
				 #| RT961-DRWC  | 2000816936     | Yes     | Right       | Left       | 2000816934 | Rechargeable |
				  #| NX960S-DRWC | 2300807477     | Yes     | Right       | Left       | 2300803315 |Rechargeable |
				  | NX977-DWC | 2300806615     | Yes     | Right       | Left       | 2300806546 |Rechargeable |
				  #| RU960-DRWC  | 4483181561     | Yes     | Right       | Left       | 4483070777 | Rechargeable |
				  #| RE961-DRWC | 4483181561     | Yes     | Right       | Left      |2300806645 |D1rechageableWired|



@tag09

Scenario Outline: 09Test case ID 1629628: Verify that firmware is upgraded if conditions apply

			#Given [Change channel side in FDTS<DeviceLeft>]
			#Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceLeftSlNo>" and "<FlashHI>" and "<DeviceLeft>"and"<Devicetype>"
			When [Create a Patient and Fitting HI In FSW "<AlterFSW>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"]
			When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
			When [Come back to Settings and wait till controls enabled]
			When [Perform Capture"<DeviceId>"and"<Devicetype>"]
			#When [Create a Patient and Fitting HI In FSW "<AlterFSW>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"]  #Added for D2 Family
			When [Perform Restore with above captured image using SWAP option "<DeviceSlNo>" and "<DeviceLeftSlNo>" and "<DeviceId>" and "<DeviceLeft>"and"<Devicetype>"]
			#When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"]  #Added for D2 Family
			When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
			Then [Compare firmware version is upgraded successfully "<DeviceId>"and"<Devicetype>"]
			Then [Close SandR tool]
			#Given [Change channel side in FDTS<DeviceLeft>]    #Added for D2 Family
			#Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceLeftSlNo>" and "<FlashHI>" and "<DeviceLeft>"and"<Devicetype>"   #Added for D2 Family




   Examples: 
   | DeviceId  | DeviceLeft | DeviceRight | DumpA    | DumpB    | DumpC    | DumpD    | DeviceTemp | AlterFSW | AlterFSWNo | DeviceSlNo | NoDevice | DeviceLeftSlNo | FlashHI | Devicetype |

    #| RE962-DRW | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 1900812195  | NoDev        | 1900812197     | Yes   | Wired    |
#	| RE962-DRWT   | Left       | Right       | Device A| Device B | Device C | Device D | Temp       | Yes      | No         | 2000803066 | NoDev    | 2000803069     | Yes     | Wired    |
#	| LT961-DRW-UP | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 1700800900 | NoDev    | 2000800436     | Yes     | Wired    |
	#| LT988-DW | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 1700800066 | NoDev    | 1700800149     | Yes     | Wired    |
#	| RE961-DRWC | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 2156716945 | NoDev    | 2156716944       | Yes     | D1rechageableWired    |
	#| RT962-DRW | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 2000800269 | NoDev    | 2000800246       | Yes     | Non-Rechargeable |
	#| RT961-DRWC | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 2000816934 | NoDev    | 2000816936     | Yes     |Rechargeable |
     | NX977-DWC | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 2300806546 | NoDev    | 2300806615     | Yes     |Rechargeable |
	#| NX960S-DRWC | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 2300803315 | NoDev    | 2300807477     | Yes     |Rechargeable |
	#| RU960-DRWC | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 4483070777 | NoDev    | 4483181561     | Yes     |Rechargeable |

@tag10

Scenario Outline: 10Test Case ID 1629629: Verify that firmware is downgraded if conditions apply


			#Given [Change channel side in FDTS<DeviceLeft>]
			#Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceLeftSlNo>" and "<FlashHI>" and "<DeviceLeft>"and"<Devicetype>""
			When [Create a Patient and Fitting HI In FSW "<AlterFSW>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"]
			When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
			When [Come back to Settings and wait till controls enabled]
			When [Perform Capture"<DeviceId>"and"<Devicetype>"]
			When [Create a Patient and Fitting HI In FSW "<AlterFSW>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"]
			When [Perform Restore with above captured image using SWAP option "<DeviceSlNo>" and "<DeviceLeftSlNo>" and "<DeviceId>" and "<DeviceLeft>"and"<Devicetype>"]
			When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
			Then [Compare firmware version is upgraded successfully "<DeviceId>"and"<Devicetype>"]
			Then [Close SandR tool]
			#Given [Change channel side in FDTS<DeviceLeft>]
			#Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceLeftSlNo>" and "<FlashHI>" and "<DeviceLeft>"and"<Devicetype>"

			
	 Examples: 
   | DeviceId  | DeviceLeft | DeviceRight | DumpA    | DumpB    | DumpC    | DumpD    | DeviceTemp | AlterFSW | AlterFSWNo | DeviceSlNo | NoDevice | DeviceLeftSlNo | FlashHI | Devicetype |

    #| RE962-DRW | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 1900812195  | NoDev        | 1900812197     | Yes   | Wired    |
#	| RE962-DRWT   | Left       | Right       | Device A| Device B | Device C | Device D | Temp       | Yes      | No         | 2000803066 | NoDev    | 2000803069     | Yes     | Wired    |
#	| LT961-DRW-UP | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 1700800900 | NoDev    | 2000800436     | Yes     | Wired    |
	#| LT988-DW | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 1700800066 | NoDev    | 1700800149     | Yes     | Wired    |
#	| RE961-DRWC | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 2156716945 | NoDev    | 2156716944       | Yes     | D1rechageableWired    |
	#| RT962-DRW | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 2000800269 | NoDev    | 2000800246       | Yes     | Non-Rechargeable |
	#| RT961-DRWC | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 2000816934 | NoDev    | 2000816936     | Yes     |Rechargeable |
     | NX977-DWC | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 2300806546 | NoDev    | 2300806615     | Yes     |Rechargeable |
	#| NX960S-DRWC | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 2300803315 | NoDev    | 2300807477     | Yes     |Rechargeable |
	#| RU960-DRWC | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 4483070777 | NoDev    | 4483181561     | Yes     |Rechargeable |


	@tag11
	
	Scenario Outline: 11Test Case ID 1105470: Verify the data saved during capture and cleaning in Camelot Cloud

	When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"]
	When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
	When [Go to Device Info tab and capture device info in excel then verify the device information is shown correctly "<Devicetype>"]
	When [Come back to Settings and wait till controls enabled]
	When [Perform Capture"<DeviceId>"and"<Devicetype>"]
	Given [Download and verify azure storage files "<ScenarioTitle>" and "<DeviceLeftSlNo>"]
	#When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"]   #Added for D2 Family	
	When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
	Then [Do the Comparison between Azure Data and SandR Data]
	Then [Close SandR tool]



Examples:
	| DeviceId  | DeviceLeftSlNo | FlashHI | DeviceRight | DeviceLeft | DeviceSlNo | ScenarioTitle | Devicetype |
    #| LT961-DRW-UP | 2000800436   | Yes     | Right       | Left       |1700800900| capture       |  Wired|
	#| RE962-DRWT | 2000803069     | Yes     | Right       | Left       | 2000803066 | capture     |   Wired|
	 #| RE962-DRW | 1900812197     | Yes     | Right       | Left       | 1900812195 | capture      | Wired   |
	 #| RT962-DRW | 2000800246     | Yes     | Right       | Left       | 2000800269 |  capture    |Non-Rechargeable |
	#| LT988-DW | 1700800149     | Yes     | Right       | Left       | 1700800066   | capture      |   Wired    |
	#| RT961-DRWC  | 2000816936     | Yes     | Right       | Left       | 2000816934  | capture    |  Rechargeable |
	 | NX977-DWC | 2300806615     | Yes     | Right       | Left       | 2300806546 | capture       |Rechargeable |
	#| NX960S-DRWC | 2300807477     | Yes     | Right       | Left       | 2300803315 |capture      |Rechargeable |
	 #| NX977-DWC | 2300806615     | Yes     | Right       | Left       | 2300806645 | capture       |Rechargeable |
	#| RU960-DRWC  | 4483181561     | Yes     | Right       | Left       | 4483070777  | capture | Rechargeable |

		@tag12

	Scenario Outline: 12Test Case ID 1101758: Verify device information is uploaded to Camelot cloud correctly

	#When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"] #Added for D2 Family	
	#Given [Change channel side in FDTS<DeviceLeft>]  #Added for D2 Family	
	#Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceLeftSlNo>" and "<FlashHI>" and "<DeviceLeft>"and"<Devicetype>"   #Added for D2 Family	
	When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"]  
    When [Verify StorageLayout Scenario By Changing Date and Confirm Cloud Icon "<DeviceId>" and "<DeviceLeft>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
	When [Change communication channel in S and R<DeviceLeft>]
	When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
	Then [Close SandR tool]
	Given [Download and verify azure storage files "<ScenarioTitle>" and "<DeviceLeftSlNo>"]
	#When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"]  #Added for D2 Family	
	When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
	Then [Do the Comparison between Azure Data and SandR Data]
	Then [Close SandR tool]



	Examples:
	| DeviceId   | DeviceLeftSlNo | FlashHI | DeviceRight | DeviceLeft | DeviceSlNo | ScenarioTitle |Devicetype  |
    #| LT961-DRW-UP | 2000800436   | Yes     | Right       | Left       |1700800900| service records |  Wired    |
	#| RE962-DRWT | 2000803069     | Yes     | Right       | Left       | 2000803066 | service records |  Wired  |
	 #| RE962-DRW | 1900812197    | Yes     | Right       | Left       | 1900812195 |  service records  |    Wired|
	 #| RT962-DRW | 2000800246     | Yes     | Right       | Left       | 2000800269  | service records |Non-Rechargeable |
	 #| LT988-DW | 1700800149     | Yes     | Right       | Left       | 1700800066   | service records |Wired        |
	#| RT961-DRWC  | 2000816936     | Yes     | Right       | Left       | 2000816934  | service records | Rechargeable |
	 | NX977-DWC | 2300806615     | Yes     | Right       | Left       | 2300806546 | service records  | Rechargeable  |
	#| NX960S-DRWC | 2300807477     | Yes     | Right       | Left       | 2300803315  |service records  | Rechargeable |
	#| RU960-DRWC  | 4483181561     | Yes     | Right       | Left       | 4483070777  | service records | Rechargeable |
	#| RE961-DRWC | 4483181561     | Yes     | Right       | Left     | 4483070777    |service records | D1rechageableWired|

	
	
	@tag13

	Scenario Outline: 13Test Case ID 1105521: Verify the data saved during restore in Camelot Cloud

	#When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"] #Added for D2 Family	
	When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
	When [Go to Device Info tab and capture device info in excel then verify the device information is shown correctly "<Devicetype>"]
	When [Come back to Settings and wait till controls enabled]
	Then [Close SandR tool]
    When [Perform Restore with above captured image "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
	#Given [Change channel side in FDTS<DeviceLeft>]   #Added for D2 Family	
	#Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceLeftSlNo>" and "<FlashHI>" and "<DeviceLeft>"and"<Devicetype>"  #Added for D2 Family	
	Given [Download and verify azure storage files "<ScenarioTitle>" and "<DeviceLeftSlNo>"]
	When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
	Then [Do the Comparison between Azure Data and SandR Data]
    Then [Close SandR tool]

	

	Examples:
	| DeviceId  | DeviceLeftSlNo | FlashHI | DeviceRight | DeviceLeft | DeviceSlNo | ScenarioTitle | Devicetype |
    #| LT961-DRW-UP | 2000800436   | Yes     | Right       | Left       |1700800900| restore        |  Wired |
	#| RE962-DRWT | 2000803069     | Yes     | Right       | Left       | 2000803066 | restore       |  Wired |
	#| RE962-DRW | 1900812197     | Yes     | Right       | Left       | 1900812195 |  restore      |Wired |
	# | RT962-DRW | 2000800246     | Yes     | Right       | Left       | 2000800269  | restore   |Non-Rechargeable |
	#| LT988-DW | 1700800149     | Yes     | Right       | Left       | 1700800066   | restore     |Wired        |
	#| RT961-DRWC  | 2000816936     | Yes     | Right       | Left       | 2000816934 | restore   | Rechargeable |
	#| NX977-DWC | 2426512940     | Yes     | Right       | Left       | 2426512941 | restore     |Rechargeable |
	#| NX960S-DRWC | 2300807477     | Yes     | Right       | Left       | 2300803315 |restore    |Rechargeable |
	 | NX977-DWC | 2300806615     | Yes     | Right       | Left       | 2300806546 | restore      |Rechargeable |
	#| RU960-DRWC  | 4483181561     | Yes     | Right       | Left       | 4483070777 | restore    | Rechargeable |
	#| RE961-DRWC | 4483181561     | Yes     | Right       | Left     | 4483070777    |restore    | D1rechageableWired|
		
@tag14

	Scenario Outline: 14Test Case ID 1103983: Verify cloud icon is shown when device information in saved in cloud

	#When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"] #Added for D2 Family
	#Given [Change channel side in FDTS<DeviceLeft>]  #Added for D2 Family
	#Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceLeftSlNo>" and "<FlashHI>" and "<DeviceLeft>"and"<Devicetype>"  #Added for D2 Family
	#When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"]  #Added for D2 Family
    When [Verify StorageLayout Scenario By Changing Date and Confirm Cloud Icon "<DeviceId>" and "<DeviceLeft>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
	When [Change communication channel in S and R<DeviceLeft>]
	When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
	Given [Download and verify azure storage files "<ScenarioTitle>" and "<DeviceLeftSlNo>"]
	Then [Close SandR tool]

	
	Examples:
	| DeviceId  | DeviceLeftSlNo | FlashHI | DeviceRight | DeviceLeft | DeviceSlNo | ScenarioTitle | Devicetype |
    #| LT961-DRW-UP | 2000800436   | Yes     | Right       | Left       |1700800900| service records        |  Wired |
	#| RE962-DRWT | 2000803069     | Yes     | Right       | Left       | 2000803066 | service records       |  Wired |
	#| RE962-DRW | 1900812197     | Yes     | Right       | Left       | 1900812195 |  service records      |Wired |
	# | RT962-DRW | 2000800246     | Yes     | Right       | Left       | 2000800269  | service records   |Non-Rechargeable |
	#| LT988-DW | 1700800149     | Yes     | Right       | Left       | 1700800066   | service records     |Wired        |
	#| RT961-DRWC  | 2000816936     | Yes     | Right       | Left       | 2000816934 | service records   | Rechargeable |
	 | NX977-DWC | 2300806615     | Yes     | Right       | Left       | 2300806546 | service records     |Rechargeable |
	#| NX960S-DRWC | 2300807477     | Yes     | Right       | Left       | 2300803315 |service records    |Rechargeable |
	#| NX977-DWC | 2300806615     | Yes     | Right       | Left       | 2300806645 | service records      |Rechargeable |
	#| RU960-DRWC  | 4483181561     | Yes     | Right       | Left       | 4483070777 | service records    | Rechargeable |
	#| RE961-DRWC | 4483181561     | Yes     | Right       | Left     | 4483070777    |service records    | D1rechageableWired|


	@tag15

Scenario Outline: 15Test Case ID 1105696: Verify that fitting data is properly restored during restoration on new device (RTS)

	Given [Cleaning up dumps before execution starts]
	#Given [Change channel side in FDTS<DeviceLeft>]   #Added for D2 Family
	#Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceLeftSlNo>" and "<FlashHI>" and "<DeviceLeft>"and"<Devicetype>"   #Added for D2 Family
	#Given [Change channel side in FDTS<DeviceRight>]   #Added for D2 Family
	#Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceSlNo>" and "<FlashHI>" and "<DeviceRight>"and"<Devicetype>"  #Added for D2 Family
	When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"]
	When [Cleaning up Capture and Restore Reports Before Launch SandR]
	When [Change communication channel in S and R<DeviceLeft>]
	When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
	When [Go to Device Info tab and capture device info in excel then verify the device information is shown correctly "<Devicetype>"]
	When [Come back to Settings and wait till controls enabled]
	When [Perform Capture"<DeviceId>"and"<Devicetype>"]
	When [Go to logs and verify capturing time]
	#When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"]  #Added for D2 Family
    When [Get the dump of connected device by storage layout "<DeviceId>" and "<DeviceLeft>" and "<DeviceLeftSlNo>"and"<Devicetype>"] 
	And  [Open Capture and Restore report and log info in report]	
	#When [Create a Patient and Fitting HI In FSW "<AlterFSW>" and "<DeviceId>" and "<DeviceSlNo>" and "<DeviceRight>"and"<Devicetype>"]	  #Added for D2 Family
	#Given [Change channel side in FDTS<DeviceRight>]
	#Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceSlNo>" and "<FlashHI>" and "<DeviceRight>"and"<Devicetype>"
 	When [Perform Restore with above captured image using RTS option "<DeviceLeftSlNo>" and "<DeviceSlNo>" and "<DeviceId>" and "<DeviceRight>"and"<Devicetype>"]	
	#When [Create a Patient and Fitting HI In FSW "<AlterFSW>" and "<DeviceId>" and "<DeviceSlNo>" and "<DeviceRight>"and"<Devicetype>"]  #Added for D2 Family
    When [Get the dump of connected device by storage layout "<DeviceId>" and "<DeviceRight>" and "<DeviceSlNo>"and"<Devicetype>"]
	Then [Do the dump comparison between two device dumps<DumpB>]	
	#Given [Change channel side in FDTS<DeviceRight>]   #Added for D2 Family
	#Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceSlNo>" and "<FlashHI>" and "<DeviceRight>"and"<Devicetype>"   #Added for D2 Family
	#Given [Change channel side in FDTS<DeviceRight>]   #Added for D2 Family
	#Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceSlNo>" and "<FlashHI>" and "<DeviceRight>"and"<Devicetype>"  #Added for D2 Family
	When [Change communication channel in S and R<DeviceLeft>]
	#When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"]  #Added for D2 Family
	When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
	When [Come back to Settings and wait till controls enabled]
	When [Perform Capture"<DeviceId>"and"<Devicetype>"]
	When [Go to logs and verify capturing time]
	#When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"]  #Added for D2 Family
	When [Create a Patient and Fitting HI In FSW "<AlterFSW>" and "<DeviceId>" and "<DeviceSlNo>" and "<DeviceRight>"and"<Devicetype>"]  
    When [Get the dump of connected device by storage layout "<DeviceId>" and "<DeviceTemp>" and "<DeviceSlNo>"and"<Devicetype>"]
	Then [Do the dump comparison between two device dumps<DumpC>]
	When [Perform Restore with above captured image using RTS option "<DeviceLeftSlNo>" and "<DeviceSlNo>" and "<DeviceId>" and "<DeviceRight>"and"<Devicetype>"]
	#When [Create a Patient and Fitting HI In FSW "<AlterFSW>" and "<DeviceId>" and "<DeviceSlNo>" and "<DeviceRight>"and"<Devicetype>"] #Added for D2 Family
    When [Get the dump of connected device by storage layout "<DeviceId>" and "<DeviceTemp>" and "<DeviceSlNo>"and"<Devicetype>"]
	Then [Do the dump comparison between two device dumps<DumpC>]

	Examples:
	| DeviceId  | DeviceLeft | DeviceRight | DumpA    | DumpB    | DumpC    | DeviceTemp | AlterFSW | AlterFSWNo | DeviceSlNo | NoDevice | DeviceLeftSlNo | FlashHI | DeviceCSlno | DeviceC | Devicetype |

	 #| RE962-DRW   | Left       | Right       | Device A | Device B | Device C | Temp       | Yes      | No         | 1900812195 | NoDev    | 1900812197       | Yes    | Cdevice  | Cdevice   |  Wired |
#	 | RE962-DRWT   | Left       | Right       | Device A | Device B | Device C | Temp       | Yes      | No         | 2000803066 | NoDev    | 2000803069       | Yes   | Cdevice  | Cdevice   |  Wired |
#	 | LT961-DRW-UP | Left       | Right       | Device A | Device B | Device C | Temp       | Yes      | No         | 1700800900 | NoDev    | 2000800436       | Yes   | Cdevice  | Cdevice   |  Wired |
	  #| LT988-DW | Left       | Right       | Device A | Device B | Device C | Temp       | Yes      | No         | 1700800066 | NoDev    | 1700800149       | Yes     | Cdevice  |  Cdevice  |  Wired |
#	 | RE961-DRWC | Left       | Right       | Device A | Device B | Device C | Temp       | Yes      | No         | 2156716945 | NoDev    | 2156716944       | Yes     | Cdevice  | Cdevice   | D1rechageableWired|
	 #| RT962-DRW | Left       | Right       | Device A | Device B | Device C | Temp       | Yes      | No         | 2000800269 | NoDev    | 2000800246       | Yes     | Cdevice  | Cdevice   |Non-Rechargeable |
	 #| RU960-DRWC | Left       | Right       | Device A | Device B | Device C | Temp       | Yes      | No         | 4483070777 | NoDev    | 4483181561     | Yes     | 2000816933  | Cdevice   |Rechargeable |
	  | NX977-DWC | Left       | Right       | Device A | Device B | Device C | Temp       | Yes      | No         | 2300806546 | NoDev    | 2300806615     | Yes     | 2000816933  | Cdevice |Rechargeable |
	 #| NX960S-DRWC | Left       | Right       | Device A | Device B | Device C | Temp       | Yes      | No         | 2300803315 | NoDev    | 2300807477     | Yes     | 2000816933  | Cdevice   |Rechargeable |
	 #| RT961-DRWC | Left       | Right       | Device A | Device B | Device C | Temp       | Yes      | No         | 2000816934 | NoDev    | 2000816936     | Yes     | 2000816933  | Cdevice   |Rechargeable |


	@tag16

	Scenario Outline: 16Test Case ID 1105669: Verify that fitting data is properly restored during restoration on original device or Clone (SWAP)

		Given [Cleaning up dumps before execution starts]
		#When [Create a Patient and Fitting HI In FSW "<AlterFSW>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"] #Added for D2 Family
		#Given [Change channel side in FDTS<DeviceLeft>]
		#Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceLeftSlNo>" and "<FlashHI>" and "<DeviceLeft>"and"<Devicetype>"
		#Given [Change channel side in FDTS<DeviceRight>]    
		#Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceSlNo>" and "<FlashHI>" and "<DeviceRight>"and"<Devicetype>"
		When [Create a Patient and Fitting HI In FSW "<AlterFSW>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"]
	 	When [Get the dump of connected device by storage layout "<DeviceId>" and "<DeviceLeft>" and "<DeviceLeftSlNo>"and"<Devicetype>"] 
        When [Cleaning up Capture and Restore Reports Before Launch SandR]
        When [Change communication channel in S and R<DeviceLeft>]
		#When [Create a Patient and Fitting HI In FSW "<AlterFSW>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"] #Added for D2 Family
		When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
        When [Go to Device Info tab and capture device info in excel then verify the device information is shown correctly "<Devicetype>"]
        When [Come back to Settings and wait till controls enabled]
        When [Perform Capture"<DeviceId>"and"<Devicetype>"]
		When [Go to logs and verify capturing time]
		When [Create a Patient and Fitting HI In FSW "<AlterFSW>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"] 
		Given [Change channel side in FDTS<DeviceLeft>]
    	#Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceLeftSlNo>" and "<FlashHI>" and "<DeviceLeft>"and"<Devicetype>" #Added for D2 Family
		#When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"] #Added for D2 Family
		When [Get the dump of connected device left of DumpB by storage layout "<DeviceId>" and "<DeviceLeft>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
        Then [Do the dump comparison between two device dumps<DumpB>]
		#When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"] #Added for D2 Family
        When [Perform Restore with above captured image using SWAP option "<DeviceSlNo>" and "<DeviceLeftSlNo>" and "<DeviceId>" and "<DeviceLeft>"and"<Devicetype>"]
		#When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"]
        When [Get the dump of connected device of left DumpC by storage layout "<DeviceId>" and "<DeviceLeft>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
        Then [Do the dump comparison between two device dumps<DumpC>]
        When [Change communication channel in S and R<DeviceLeft>]
		#When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"] #Added for D2 Family
        When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
        When [Go to Device Info tab and capture device info in excel then verify the device information is shown correctly "<Devicetype>"]
        When [Come back to Settings and wait till controls enabled]
        When [Perform Capture"<DeviceId>"and"<Devicetype>"]
        When [Go to logs and verify capturing time]
		#When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"] #Added for D2 Family
		#Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceLeftSlNo>" and "<FlashHI>" and "<DeviceLeft>"and"<Devicetype>" #Added for D2 Family
		#Given [Change channel side in FDTS<DeviceRight>]  #Added for D2 Family
  #      Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceSlNo>" and "<FlashHI>" and "<DeviceRight>"and"<Devicetype>" #Added for D2 Family
        When [Perform Restore with above captured image using SWAP with left "<DeviceSlNo>" and "<DeviceLeftSlNo>" and "<DeviceId>" and "<DeviceRight>"and"<Devicetype>"]
        When [Get the dump of connected device of DumpD by storage layout "<DeviceId>" and "<DeviceRight>" and "<DeviceLeftSlNo>"and"<Devicetype>"] 
		#This above step modified from leftsl no to right sl no
        Then [Do the dump comparison between two device DeviceC and DeviceD dumps<DumpD>]

     Examples:
    | DeviceId  | DeviceLeft | DeviceRight | DumpA    | DumpB    | DumpC    | DumpD    | DeviceTemp | AlterFSW | AlterFSWNo | DeviceSlNo | NoDevice | DeviceLeftSlNo | FlashHI | Devicetype |

   #| RE962-DRW | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 1900812195  | NoDev        | 1900812197     | Yes     |Wired |
#	| RE962-DRWT   | Left       | Right       | Device A| Device B | Device C | Device D | Temp       | Yes      | No         | 2000803066 | NoDev    | 2000803069     | Yes     |Wired |
#	| LT961-DRW-UP | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 1700800900 | NoDev    | 2000800436     | Yes     |Wired |
	#| LT988-DW | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 1700800066 | NoDev    | 1700800149     | Yes     |Wired |
#	| RE961-DRWC | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 2156716945 | NoDev    | 2156716944       | Yes     |D1rechageableWired|
	 #| RT962-DRW | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 2000800269 | NoDev    | 2000800246       | Yes     |Non-Rechargeable |
	#| RT961-DRWC | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 2000816934 | NoDev    | 2000816936    | Yes     |Rechargeable |
     | NX977-DWC | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 2300806546 | NoDev    | 2300806615     | Yes     |Rechargeable |
	#| NX960S-DRWC | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 2300803315 | NoDev    | 2300807477    | Yes     |Rechargeable |
     #| RU960-DRWC | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 4483070777 | NoDev    | 4483181561     | Yes     |Rechargeable |


	@tag17

	Scenario Outline: 17Test Case ID 1142328: PC_Verify HI can be PC programmed properly.

	 #When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"]
	 Given [Change channel side in FDTS<DeviceLeft>]
	 Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceLeftSlNo>" and "<FlashHI>" and "<DeviceLeft>"and"<Devicetype>"
	 When [Create a Patient and Fitting HI In FSW "<AlterFSW>" and "<DeviceId>" and "<DeviceSlNo>" and "<DeviceRight>"and"<Devicetype>"]
	 Given [Change channel side in FDTS<DeviceRight>]
	 Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceSlNo>" and "<FlashHI>" and "<DeviceRight>"and"<Devicetype>"

	Examples:
	| DeviceId  | DeviceLeftSlNo | FlashHI | DeviceRight | DeviceLeft | DeviceSlNo | Devicetype|
##	| LT961-DRW-UP | 2000800436     | Yes     | Right       | Left       |            |Wired |
    #| RE962-DRW    | 1900812197     | Yes     | Right       | Left       | 1900812195 |Wired |
##	| RE962-DRWT   | 2000803069     | Yes     | Right       | Left       | 2000803066 |Wired |
	#| RT962-DRW    | 2000800246     | Yes     | Right       | Left       | 2000800269 |Non-Rechargeable |
    #| LT988-DW     | 1700800149     | Yes     | Right       | Left       | 1700800066 |Wired |
	#| RT961-DRWC   | 2000816936     | Yes     | Right       | Left       | 2000816934 |Rechargeable |
	#|NX960S-DRWC | 2300807477     | Yes     | Right       | Left       | 2300803315  |Rechargeable |
	 | NX977-DWC | 2300806615     | Yes     | Right       | Left       | 2300806546 |Rechargeable |
	#| RU960-DRWC   | 4483181561     | Yes     | Right       | Left       | 4483070777 |Rechargeable |
	#| RE961-DRWC   | 4483181561     | Yes     | Right       | Left       | 4483070777 |D1rechageableWired|