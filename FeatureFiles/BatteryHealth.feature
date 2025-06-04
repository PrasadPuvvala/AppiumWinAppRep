Feature: Battery Health

#Background:
#   Given Importing Test Cases to Excel from TFS TestPlanID "1696074" equivalent to Testcase Configuration "GOP: Dooku3_BTE(Rhodium)" to Create XML.

Scenario: 01Test Case ID 1685572: Verify that capture is not allowed on device set for listening test

    Given Launch socket Driver "<DeviceId>"and"<Devicetype>"
	When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"]
	When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"and "<DeviceLeft>"]
	When [Come back to Settings and wait till controls enabled]
	When [Perform Capture with listening test settings]
	And  [Open Capture and Restore report and log info in report]
	Then [Close SandR tool]
	When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"and "<DeviceLeft>"]
	When [Come back to Settings and wait till controls enabled]
	When [Perform Capture with listening test settings]
	Then [Close SandR tool]
	When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"and "<DeviceLeft>"]
	When [Come back to Settings and wait till controls enabled]
	When [Perform Capture with listening test settings]
	Then [Close SandR tool]

Examples:
	| DeviceId     | DeviceLeftSlNo | FlashHI | DeviceRight | DeviceLeft | DeviceSlNo | Devicetype       |
				
	##| NX9ITC-DW-MP | 2476130453     | Yes     | Right       | Left       | 2476130454 | Non-Rechargeable |
	#| NX960S-DRWC | 2400802588     | Yes     | Right       | Left       | 2400802587 | Rechargeable |
	| NX977-DWC | 2300809943     | Yes     | Right       | Left       | 2300809945 | Rechargeable |

Scenario: 02Test Case ID 1105675: Verify that the battery health of a RHI is reset on clone (SWAP)

	When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"]
	When [Launch algo and alter ADL value "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
	When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"and "<DeviceLeft>"]
	When [Go to Device Info tab and capture device info in excel then verify the device information is shown correctly "<Devicetype>"]
	When [Come back to Settings and wait till controls enabled]
	When [Perform Capture"<DeviceId>"and"<Devicetype>"]
	And [Open Capture and Restore report and log info in report]
	Then [Close SandR tool]
	When [Perform Restore with above captured image using RTS option "<DeviceLeftSlNo>" and "<DeviceSlNo>" and "<DeviceId>" and "<DeviceRight>"and"<Devicetype>"]
	And [Open Capture and Restore report and log info in report]
	When [Launch algo lab and check the ADL value "<DeviceId>" and "<DeviceSlNo>"and "<Devicetype>" and "<DeviceRight>"]
	When [Perform Restore with above captured image using SWAP option "<DeviceSlNo>" and "<DeviceLeftSlNo>" and "<DeviceId>" and "<DeviceRight>"and"<Devicetype>"]
	And [Open Capture and Restore report and log info in report]
	Given [Set Development and Verification System Role in Basic Setting for System Configuration]
	Given [Change channel side in FDTS<DeviceRight>]
	Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceLeftSlNo>" and "<FlashHI>" and "<DeviceRight>"and"<Devicetype>"
	When [Perform Restore with above captured image using SWAP with left "<DeviceLeftSlNo>" and "<DeviceLeftSlNo>" and "<DeviceId>" and "<DeviceRight>"and"<Devicetype>"]
	And [Open Capture and Restore report and log info in report]
	When [Launch algo lab and check the ADL value "<DeviceId>" and "<DeviceLeftSlNo>"and "<Devicetype>" and "<DeviceRight>"]

Examples:
	| DeviceId     | DeviceLeft | DeviceRight | DumpA    | DumpB    | DumpC    | DumpD    | AlterFSW | AlterFSWNo | DeviceSlNo | NoDevice | DeviceLeftSlNo | FlashHI | DeviceCSlno | DeviceC | Devicetype       |

	##| NX9ITC-DW-MP | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2476130454 | NoDev    | 2476130453     | Yes     | Cdevice     | Cdevice | Non-Rechargeable |
	| NX977-DWC | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2300809945 | NoDev    | 2300809943     | Yes     | Cdevice     | Cdevice | Rechargeable |
	#| NX960S-DRWC | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2400802587 | NoDev    | 2400802588     | Yes     | Cdevice     | Cdevice | Rechargeable |

Scenario: 03Test Case ID 1413300: Verify that battery ADL data is reset during restoration on new device

    Given Launch HIRegistration Tool to Unregister Cloud Info for Device A "<DeviceLeftSlNo>"
	Given [Set Service GROC System Role in Basic Setting for System Configuration]
	Given [Change channel side in FDTS<DeviceLeft>]
	Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceLeftSlNo>" and "<FlashHI>" and "<DeviceLeft>"and"<Devicetype>"
	Given [Set Development and Verification System Role in Basic Setting for System Configuration]
	Given [Change channel side in FDTS<DeviceLeft>]
	Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceLeftSlNo>" and "<FlashHI>" and "<DeviceLeft>"and"<Devicetype>"
	Given [Set Development and Verification System Role in Basic Setting for System Configuration]
	Given [Change channel side in FDTS<DeviceRight>]
	Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceSlNo>" and "<FlashHI>" and "<DeviceRight>"and"<Devicetype>"
	When [Create a Patient and Fitting HI In FSW "<AlterFSW>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"and"<Devicetype>"]
	When [Launch algo and alter ADL value "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"]
	When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"and "<DeviceLeft>"]
	When [Go to Device Info tab and capture device info in excel then verify the device information is shown correctly "<Devicetype>"]
	When [Come back to Settings and wait till controls enabled]
	When [Perform Capture"<DeviceId>"and"<Devicetype>"]
	And [Open Capture and Restore report and log info in report]
	Then [Close SandR tool]
	Then [Launch Algo with fresh device B and validate the ADL Battery values "<DeviceId>" and "<DeviceSlNo>" and "<Devicetype>"]
	When [Perform Restore with above captured image using SWAP option "<DeviceSlNo>" and "<DeviceLeftSlNo>" and "<DeviceId>" and "<DeviceRight>"and"<Devicetype>"]
	And [Open Capture and Restore report and log info in report]
	Given [Change channel side in FDTS<DeviceRight>]
	Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceLeftSlNo>" and "<FlashHI>" and "<DeviceRight>"and"<Devicetype>"
	When [Perform Restore with above captured image using SWAP with left "<DeviceLeftSlNo>" and "<DeviceLeftSlNo>" and "<DeviceId>" and "<DeviceRight>"and"<Devicetype>"]
	And [Open Capture and Restore report and log info in report]
	Then [Launch Algo with fresh device B and validate the ADL Battery values "<DeviceId>" and "<DeviceLeftSlNo>" and "<Devicetype>"]
	Given [Change channel side in FDTS<DeviceRight>]
	Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceSlNo>" and "<FlashHI>" and "<DeviceRight>"and"<Devicetype>"
	When [Launch algo and alter ADL value for device B "<DeviceId>" and "<DeviceSlNo>"and"<Devicetype>"]
	When [Perform Restore with above captured image using SWAP option "<DeviceSlNo>" and "<DeviceLeftSlNo>" and "<DeviceId>" and "<DeviceRight>"and"<Devicetype>"]
	And [Open Capture and Restore report and log info in report]
	Given [Change channel side in FDTS<DeviceRight>]
	Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceLeftSlNo>" and "<FlashHI>" and "<DeviceRight>"and"<Devicetype>"
	When [Perform Restore with above captured image using SWAP with left "<DeviceLeftSlNo>" and "<DeviceLeftSlNo>" and "<DeviceId>" and "<DeviceRight>"and"<Devicetype>"]
	And [Open Capture and Restore report and log info in report]
	Then [Launch Algo with fresh device B and validate the ADL Battery values "<DeviceId>" and "<DeviceLeftSlNo>" and "<Devicetype>"]

Examples:
	| DeviceId     | DeviceLeft | DeviceRight | DumpA    | DumpB    | DumpC    | DumpD    | AlterFSW | AlterFSWNo | DeviceSlNo | NoDevice | DeviceLeftSlNo | FlashHI | DeviceCSlno | DeviceC | Devicetype       |

	##| NX9ITC-DW-MP | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2476130454 | NoDev    | 2476130453     | Yes     | Cdevice     | Cdevice | Non-Rechargeable |
	| NX977-DWC | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2300809945 | NoDev    | 2300809943     | Yes     | Cdevice     | Cdevice | Rechargeable |
	#| NX960S-DRWC | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2400802587 | NoDev    | 2400802588     | Yes     | Cdevice     | Cdevice | Rechargeable |

Scenario: 04Test Case ID 1539304: Verify that capture and restore on devices with low battery is handled correctly

    When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"and "<DeviceLeft>"]
	When [Go to Device Info tab and capture device info in excel then verify the device information is shown correctly "<Devicetype>"]
	When [Come back to Settings and wait till controls enabled]
	When [Perform Capture"<DeviceId>"and"<Devicetype>"]
	And [Open Capture and Restore report and log info in report]

Examples:
	| DeviceId     | DeviceLeft | DeviceRight | DumpA    | DumpB    | DumpC    | DumpD    | AlterFSW | AlterFSWNo | DeviceSlNo | NoDevice | DeviceLeftSlNo | FlashHI | DeviceCSlno | DeviceC | Devicetype       |

	| NX9ITC-DW-MP | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2476130454 | NoDev    | 2476130453     | Yes     | Cdevice     | Cdevice | Non-Rechargeable |