Feature: VA Prefit

A short summary of the feature

@tag1
Scenario: 01Test Case ID 511987: Verify binaural prefitting can be performed
    Given Launch socket Driver "<DeviceId>"and"<Devicetype>"
    Given [Change channel side in FDTS<DeviceLeft>]
    Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceLeftSlNo>" and "<FlashHI>" and "<DeviceRight>"and"<Devicetype>"
    Given [Change channel side in FDTS<DeviceRight>]
    Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceSlNo>" and "<FlashHI>" and "<DeviceRight>"and"<Devicetype>"
    Then [Set EnableModelValidation to false in app settings]
    When [Launch S and R set sales order connection string and set System Role to Prefit]
    Then [Validate Sales Order Connection String matches configuration]
    When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"and "<DeviceLeft>"]
    When [Come back to Settings and wait till controls enabled]
    Then [Perform pre-fitting by clicking the Prefit or Prefit Wireless button if device is wireless "<DeviceLeftSlNo>" and "<DeviceSlNo>" and "<DeviceId>" and "<DeviceRight>"and"<Devicetype>"]
    Then [Close SandR tool]
    When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"and "<DeviceLeft>"]
	When [Go to Device Info tab and capture device info in excel then verify the device information is shown correctly "<Devicetype>"]
	When [Come back to Settings and wait till controls enabled]
	Then [Close SandR tool]
     When [Launch SandR "<DeviceId>" and "<DeviceSlNo>"and"<Devicetype>"and "<DeviceRight>"]
	When [Go to Device Info tab and capture device info in excel then verify the device information is shown correctly "<Devicetype>"]
	When [Come back to Settings and wait till controls enabled]
	Then [Close SandR tool]

    Examples:
    | DeviceId     | DeviceLeft | DeviceRight | DumpA    | DumpB    | DumpC    | DumpD    | AlterFSW | AlterFSWNo | DeviceSlNo | NoDevice | DeviceLeftSlNo | FlashHI | DeviceCSlno | DeviceC | Devicetype          |

    #| RE962-DRW    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2026335124 | NoDev    | 2026335111     | Yes     | Cdevice     | Cdevice | Wired               |
    #| RE962-DRWT   | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2000803066 | NoDev    | 2000803069     | Yes     | Cdevice     | Cdevice | Wired               |
    #| LT961-DRW-UP | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 1600805063 | NoDev    | 1700803025     | Yes     | Cdevice     | Cdevice | Wired               |
    #| LT988-DW     | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 1600807189 | NoDev    | 1926274988     | Yes     | Cdevice     | Cdevice | Wired               |
    #| RE961-DRWC   | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2066070058 | NoDev    | 2026793947     | Yes     | Cdevice     | Cdevice | D1rechargeableWired |
    #| RT962-DRW    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2026335124 | NoDev    | 2026335111     | Yes     | Cdevice     | Cdevice | Non-Rechargeable    |
    #| RU960-DRWC   | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2326310144 | NoDev    | 2326310145     | Yes     | 2000816933  | Cdevice | Rechargeable        |
    #| EI998-DWHC    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2500808481 | NoDev    | 2500808480     | Yes     | 2500808480  | Cdevice | Rechargeable        |
    #| NX960S-DRWC  | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No          | 2403011503 | NoDev    |   2403011502     | Yes     | 2403011502  | Cdevice | Rechargeable        |
    #| RT961-DRWC   | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2000816934 | NoDev    | 2000816936     | Yes     | 2000816933  | Cdevice | Rechargeable        |
     #| RE988-DWT    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2026489200 | NoDev    | 2026467497     | Yes     | Cdevice     | Cdevice | Wired               |
    #| NX9ITC-DW-MP | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2476130209 | NoDev    | 2476130208     | Yes     | Cdevice     | Cdevice | Non-Rechargeable    |
    #| NX961-DRW    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2400800489 | NoDev    | 2400800488     | Yes     | 2000816933  | Cdevice | Non-Rechargeable    |
    #| CX160S-DRWC  | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2400801281 | NoDev    | 2400801280     | Yes     | 2000816933  | Cdevice | Rechargeable        |
    #| RT977-DWC    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2100819768 | NoDev    | 2100817051     | Yes     | 2000816933  | Cdevice | Rechargeable        |
    #| VI962-DRW    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2400812375 | NoDev    | 2400812374     | Yes     | 2400812374  | Cdevice | Non-Rechargeable    |
    | VI960S-DRWC  | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         |   2586136545 | NoDev    |   2586146387     | Yes     |   2586146387  | Cdevice | Rechargeable        |


@tag2
Scenario: 02Test Case ID 511962: Verify supported FSW version
    Given [Uninstall the current SmartFit]
    When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"and "<DeviceLeft>"]
    When [Come back to Settings and wait till controls enabled]
    Then [Perform pre-fitting by clicking the Prefit or Prefit Wireless button if device is wireless "<DeviceLeftSlNo>" and "<DeviceSlNo>" and "<DeviceId>" and "<DeviceRight>"and"<Devicetype>"]
    Then [Close SandR tool]
     Examples:
    | DeviceId     | DeviceLeft | DeviceRight | DumpA    | DumpB    | DumpC    | DumpD    | AlterFSW | AlterFSWNo | DeviceSlNo | NoDevice | DeviceLeftSlNo | FlashHI | DeviceCSlno | DeviceC | Devicetype          |

    #| RE962-DRW    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2026335124 | NoDev    | 2026335111     | Yes     | Cdevice     | Cdevice | Wired               |
    #| RE962-DRWT   | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2000803066 | NoDev    | 2000803069     | Yes     | Cdevice     | Cdevice | Wired               |
    #| LT961-DRW-UP | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 1600805063 | NoDev    | 1700803025     | Yes     | Cdevice     | Cdevice | Wired               |
    #| LT988-DW     | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 1600807189 | NoDev    | 1926274988     | Yes     | Cdevice     | Cdevice | Wired               |
    #| RE961-DRWC   | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2066070058 | NoDev    | 2026793947     | Yes     | Cdevice     | Cdevice | D1rechargeableWired |
    #| RT962-DRW    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2026335124 | NoDev    | 2026335111     | Yes     | Cdevice     | Cdevice | Non-Rechargeable    |
    #| RU960-DRWC   | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2326310144 | NoDev    | 2326310145     | Yes     | 2000816933  | Cdevice | Rechargeable        |
    #| EI998-DWHC    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2500808481 | NoDev    | 2500808480     | Yes     | 2500808480  | Cdevice | Rechargeable        |
    #| NX960S-DRWC  | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No          | 2403011503 | NoDev    |   2403011502     | Yes     | 2403011502  | Cdevice | Rechargeable        |
    #| RT961-DRWC   | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2000816934 | NoDev    | 2000816936     | Yes     | 2000816933  | Cdevice | Rechargeable        |
     #| RE988-DWT    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2026489200 | NoDev    | 2026467497     | Yes     | Cdevice     | Cdevice | Wired               |
    #| NX9ITC-DW-MP | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2476130209 | NoDev    | 2476130208     | Yes     | Cdevice     | Cdevice | Non-Rechargeable    |
    #| NX961-DRW    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2400800489 | NoDev    | 2400800488     | Yes     | 2000816933  | Cdevice | Non-Rechargeable    |
    #| CX160S-DRWC  | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2400801281 | NoDev    | 2400801280     | Yes     | 2000816933  | Cdevice | Rechargeable        |
    #| RT977-DWC    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2100819768 | NoDev    | 2100817051     | Yes     | 2000816933  | Cdevice | Rechargeable        |
    #| VI962-DRW    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2400812375 | NoDev    | 2400812374     | Yes     | 2400812374  | Cdevice | Non-Rechargeable    |
    | VI960S-DRWC  | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         |   2586136545 | NoDev    |   2586146387     | Yes     |   2586146387  | Cdevice | Rechargeable        |


@tag3
Scenario: 03Test Case ID 511966: Verify information stored in Camelot Cloud during Prefitting
     Given [Change channel side in FDTS<DeviceLeft>]
    Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceLeftSlNo>" and "<FlashHI>" and "<DeviceRight>"and"<Devicetype>"
    Given [Change channel side in FDTS<DeviceRight>]
    Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceSlNo>" and "<FlashHI>" and "<DeviceRight>"and"<Devicetype>" 
    When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"and "<DeviceLeft>"]
    When [Come back to Settings and wait till controls enabled]
    Then [Perform pre-fitting by clicking the Prefit or Prefit Wireless button if device is wireless "<DeviceLeftSlNo>" and "<DeviceSlNo>" and "<DeviceId>" and "<DeviceRight>"and"<Devicetype>"]
    Then [Close SandR tool]
    When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"and"<Devicetype>"and "<DeviceLeft>"]
	When [Go to Device Info tab and capture device info in excel then verify the device information is shown correctly "<Devicetype>"]
	When [Come back to Settings and wait till controls enabled]
	Then [Close SandR tool]
    When [Launch SandR "<DeviceId>" and "<DeviceSlNo>"and"<Devicetype>"and "<DeviceRight>"]
	When [Go to Device Info tab and capture device info in excel then verify the device information is shown correctly "<Devicetype>"]
	When [Come back to Settings and wait till controls enabled]
	Then [Close SandR tool]
    Examples:
    | DeviceId     | DeviceLeft | DeviceRight | DumpA    | DumpB    | DumpC    | DumpD    | AlterFSW | AlterFSWNo | DeviceSlNo | NoDevice | DeviceLeftSlNo | FlashHI | DeviceCSlno | DeviceC | Devicetype          |

    #| RE962-DRW    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2026335124 | NoDev    | 2026335111     | Yes     | Cdevice     | Cdevice | Wired               |
    #| RE962-DRWT   | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2000803066 | NoDev    | 2000803069     | Yes     | Cdevice     | Cdevice | Wired               |
    #| LT961-DRW-UP | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 1600805063 | NoDev    | 1700803025     | Yes     | Cdevice     | Cdevice | Wired               |
    #| LT988-DW     | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 1600807189 | NoDev    | 1926274988     | Yes     | Cdevice     | Cdevice | Wired               |
    #| RE961-DRWC   | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2066070058 | NoDev    | 2026793947     | Yes     | Cdevice     | Cdevice | D1rechargeableWired |
    #| RT962-DRW    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2026335124 | NoDev    | 2026335111     | Yes     | Cdevice     | Cdevice | Non-Rechargeable    |
    #| RU960-DRWC   | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2326310144 | NoDev    | 2326310145     | Yes     | 2000816933  | Cdevice | Rechargeable        |
    #| EI998-DWHC    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2500808481 | NoDev    | 2500808480     | Yes     | 2500808480  | Cdevice | Rechargeable        |
    #| NX960S-DRWC  | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No          | 2403011503 | NoDev    |   2403011502     | Yes     | 2403011502  | Cdevice | Rechargeable        |
    #| RT961-DRWC   | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2000816934 | NoDev    | 2000816936     | Yes     | 2000816933  | Cdevice | Rechargeable        |
     #| RE988-DWT    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2026489200 | NoDev    | 2026467497     | Yes     | Cdevice     | Cdevice | Wired               |
    #| NX9ITC-DW-MP | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2476130209 | NoDev    | 2476130208     | Yes     | Cdevice     | Cdevice | Non-Rechargeable    |
    #| NX961-DRW    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2400800489 | NoDev    | 2400800488     | Yes     | 2000816933  | Cdevice | Non-Rechargeable    |
    #| CX160S-DRWC  | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2400801281 | NoDev    | 2400801280     | Yes     | 2000816933  | Cdevice | Rechargeable        |
    #| RT977-DWC    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2100819768 | NoDev    | 2100817051     | Yes     | 2000816933  | Cdevice | Rechargeable        |
    #| VI962-DRW    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2400812375 | NoDev    | 2400812374     | Yes     | 2400812374  | Cdevice | Non-Rechargeable    |
    | VI960S-DRWC  | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         |   2586114318 | NoDev    |   2586146387     | Yes     |   2586146387  | Cdevice | Rechargeable        |



@tag4
Scenario: 04Test Case ID 511923: Verify pre-fitting flow works as expected
    Given [Change channel side in FDTS<DeviceRight>]
	Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceSlNo>" and "<FlashHI>" and "<DeviceRight>"and"<Devicetype>"
    Then [Launch FSW and Verify that audiogram settings "<DeviceId>" and "<DeviceSlNo>" and "<DeviceRight>"and"<Devicetype>"]
    When [Launch SandR "<DeviceId>" and "<DeviceSlNo>"and"<Devicetype>"and "<DeviceRight>"]
    When [Come back to Settings and wait till controls enabled]
    Then [Perform monaural pre-fitting by clicking the Prefit or Prefit Wireless button if device is wireless "<DeviceLeftSlNo>" and "<DeviceSlNo>" and "<DeviceId>" and "<DeviceRight>"and"<Devicetype>"]
    Then [Close SandR tool]
    Then [Launch FSW and Verify that audiogram settings "<DeviceId>" and "<DeviceSlNo>" and "<DeviceRight>"and"<Devicetype>"]

   
    Examples:
    | DeviceId     | DeviceLeft | DeviceRight | DumpA    | DumpB    | DumpC    | DumpD    | AlterFSW | AlterFSWNo | DeviceSlNo | NoDevice | DeviceLeftSlNo | FlashHI | DeviceCSlno | DeviceC | Devicetype          |

    #| RE962-DRW    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2026335124 | NoDev    | 2026335111     | Yes     | Cdevice     | Cdevice | Wired               |
    #| RE962-DRWT   | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2000803066 | NoDev    | 2000803069     | Yes     | Cdevice     | Cdevice | Wired               |
    #| LT961-DRW-UP | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 1600805063 | NoDev    | 1700803025     | Yes     | Cdevice     | Cdevice | Wired               |
    #| LT988-DW     | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 1600807189 | NoDev    | 1926274988     | Yes     | Cdevice     | Cdevice | Wired               |
    #| RE961-DRWC   | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2066070058 | NoDev    | 2026793947     | Yes     | Cdevice     | Cdevice | D1rechargeableWired |
    #| RT962-DRW    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2026335124 | NoDev    | 2026335111     | Yes     | Cdevice     | Cdevice | Non-Rechargeable    |
    #| RU960-DRWC   | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2326310144 | NoDev    | 2326310145     | Yes     | 2000816933  | Cdevice | Rechargeable        |
    #| EI998-DWHC    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2500808481 | NoDev    | 2500808480     | Yes     | 2500808480  | Cdevice | Rechargeable        |
    #| NX960S-DRWC  | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No          | 2403011503 | NoDev    |   2403011502     | Yes     | 2403011502  | Cdevice | Rechargeable        |
    #| RT961-DRWC   | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2000816934 | NoDev    | 2000816936     | Yes     | 2000816933  | Cdevice | Rechargeable        |
     #| RE988-DWT    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2026489200 | NoDev    | 2026467497     | Yes     | Cdevice     | Cdevice | Wired               |
    #| NX9ITC-DW-MP | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2476130209 | NoDev    | 2476130208     | Yes     | Cdevice     | Cdevice | Non-Rechargeable    |
    #| NX961-DRW    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2400800489 | NoDev    | 2400800488     | Yes     | 2000816933  | Cdevice | Non-Rechargeable    |
    #| CX160S-DRWC  | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2400801281 | NoDev    | 2400801280     | Yes     | 2000816933  | Cdevice | Rechargeable        |
    #| RT977-DWC    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2100819768 | NoDev    | 2100817051     | Yes     | 2000816933  | Cdevice | Rechargeable        |
    #| VI962-DRW    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2400812375 | NoDev    | 2400812374     | Yes     | 2400812374  | Cdevice | Non-Rechargeable    |
    | VI960S-DRWC  | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         |   2586114318 | NoDev    |   2586146387     | Yes     |   2586146387  | Cdevice | Rechargeable        |



@tag5
Scenario: 05Test Case ID 591670: Verify that physical properties are set correctly during Prefit
    Given [Change channel side in FDTS<DeviceRight>]
	Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceSlNo>" and "<FlashHI>" and "<DeviceRight>"and"<Devicetype>"
    Given [Change channel side in FDTS<DeviceRight>]
	Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceSlNo>" and "<FlashHI>" and "<DeviceRight>"and"<Devicetype>"
    When [Launch SandR "<DeviceId>" and "<DeviceSlNo>"and"<Devicetype>"and "<DeviceRight>"]
    When [Come back to Settings and wait till controls enabled]
    Then [Perform pre-fitting by clicking the Prefit or Prefit Wireless "<DeviceLeftSlNo>" and "<DeviceSlNo>" and "<DeviceId>" and "<DeviceRight>"and"<Devicetype>"]
    Then [Close SandR tool]
    When [Connect the device to SmartFit and inspect the fitted dome "<DeviceId>" and "<DeviceSlNo>" and "<DeviceRight>"and"<Devicetype>"and"<DOME>"]
     Examples:
    | DeviceId    | DeviceLeft | DeviceRight | DumpA    | DumpB    | DumpC    | DumpD    | AlterFSW | AlterFSWNo | DeviceSlNo | NoDevice | DeviceLeftSlNo | FlashHI | DeviceCSlno | DeviceC | Devicetype   | DOME  |

    #| RE962-DRW    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2026335124 | NoDev    | 2026335111     | Yes     | Cdevice     | Cdevice | Wired               |Tulip-Dome |
    #| RE962-DRWT   | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2000803066 | NoDev    | 2000803069     | Yes     | Cdevice     | Cdevice | Wired               |Tulip-Dome |
    #| LT961-DRW-UP | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 1600805063 | NoDev    | 1700803025     | Yes     | Cdevice     | Cdevice | Wired               |Tulip-Dome |
    #| LT988-DW     | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 1600807189 | NoDev    | 1926274988     | Yes     | Cdevice     | Cdevice | Wired               |Tulip-Dome |
    #| RE961-DRWC   | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2066070058 | NoDev    | 2026793947     | Yes     | Cdevice     | Cdevice | D1rechargeableWired |Tulip-Dome |
    #| RT962-DRW    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2026335124 | NoDev    | 2026335111     | Yes     | Cdevice     | Cdevice | Non-Rechargeable    |Tulip-Dome |
    #| RU960-DRWC   | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2326310144 | NoDev    | 2326310145     | Yes     | 2000816933  | Cdevice | Rechargeable        |Tulip-Dome |
    #| EI998-DWHC    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2500808481 | NoDev    | 2500808480     | Yes     | 2500808480  | Cdevice | Rechargeable        |Tulip-Dome |
    #| NX960S-DRWC  | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No          | 2403011503 | NoDev    |   2403011502     | Yes     | 2403011502  | Cdevice | Rechargeable        |Tulip-Dome |
    #| RT961-DRWC   | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2000816934 | NoDev    | 2000816936     | Yes     | 2000816933  | Cdevice | Rechargeable        |Tulip-Dome |
     #| RE988-DWT    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2026489200 | NoDev    | 2026467497     | Yes     | Cdevice     | Cdevice | Wired               |Tulip-Dome |
    #| NX9ITC-DW-MP | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2476130209 | NoDev    | 2476130208     | Yes     | Cdevice     | Cdevice | Non-Rechargeable    |Tulip-Dome |
    #| NX961-DRW    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2400800489 | NoDev    | 2400800488     | Yes     | 2000816933  | Cdevice | Non-Rechargeable    |Tulip-Dome |
    #| CX160S-DRWC  | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2400801281 | NoDev    | 2400801280     | Yes     | 2000816933  | Cdevice | Rechargeable        |Tulip-Dome |
    #| RT977-DWC    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2100819768 | NoDev    | 2100817051     | Yes     | 2000816933  | Cdevice | Rechargeable        |Tulip-Dome |
    #| VI962-DRW    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2400812375 | NoDev    | 2400812374     | Yes     | 2400812374  | Cdevice | Non-Rechargeable    |Tulip-Dome |
    | VI960S-DRWC | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2526629430 | NoDev    |     2526629431 | Yes     |  2586146387 | Cdevice | Rechargeable | Tulip-Dome |


@tag6
Scenario: 06Test Case ID 672415: Verify that it is possible to perform a monaural prefit of a CROS device
    Given [Change channel side in FDTS<DeviceRight>]
	Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceSlNo>" and "<FlashHI>" and "<DeviceRight>"and"<Devicetype>"
    When [Launch SandR "<DeviceId>" and "<DeviceSlNo>"and"<Devicetype>"and "<DeviceRight>"]
    When [Come back to Settings and wait till controls enabled]
    Then [Perform monaural pre-fitting by clicking the Prefit or Prefit Wireless button if device is wireless "<DeviceLeftSlNo>" and "<DeviceSlNo>" and "<DeviceId>" and "<DeviceRight>"and"<Devicetype>"]
    Then [Close SandR tool]
     When [Launch SandR "<DeviceId>" and "<DeviceSlNo>"and"<Devicetype>"and "<DeviceRight>"]
	When [Go to Device Info tab and capture device info in excel then verify the device information is shown correctly "<Devicetype>"]
	When [Come back to Settings and wait till controls enabled]
	Then [Close SandR tool]
    Examples:
    | DeviceId     | DeviceLeft | DeviceRight | DumpA    | DumpB    | DumpC    | DumpD    | AlterFSW | AlterFSWNo | DeviceSlNo | NoDevice | DeviceLeftSlNo | FlashHI | DeviceCSlno | DeviceC | Devicetype          |

    #| RE962-DRW    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2026335124 | NoDev    | 2026335111     | Yes     | Cdevice     | Cdevice | Wired               |
    #| RE962-DRWT   | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2000803066 | NoDev    | 2000803069     | Yes     | Cdevice     | Cdevice | Wired               |
    #| LT961-DRW-UP | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 1600805063 | NoDev    | 1700803025     | Yes     | Cdevice     | Cdevice | Wired               |
    #| LT988-DW     | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 1600807189 | NoDev    | 1926274988     | Yes     | Cdevice     | Cdevice | Wired               |
    #| RE961-DRWC   | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2066070058 | NoDev    | 2026793947     | Yes     | Cdevice     | Cdevice | D1rechargeableWired |
    #| RT962-DRW    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2026335124 | NoDev    | 2026335111     | Yes     | Cdevice     | Cdevice | Non-Rechargeable    |
    #| RU960-DRWC   | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2326310144 | NoDev    | 2326310145     | Yes     | 2000816933  | Cdevice | Rechargeable        |
    #| EI998-DWHC    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2500808481 | NoDev    | 2500808480     | Yes     | 2500808480  | Cdevice | Rechargeable        |
    #| NX960S-DRWC  | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No          | 2403011503 | NoDev    |   2403011502     | Yes     | 2403011502  | Cdevice | Rechargeable        |
    #| RT961-DRWC   | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2000816934 | NoDev    | 2000816936     | Yes     | 2000816933  | Cdevice | Rechargeable        |
     #| RE988-DWT    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2026489200 | NoDev    | 2026467497     | Yes     | Cdevice     | Cdevice | Wired               |
    #| NX9ITC-DW-MP | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2476130209 | NoDev    | 2476130208     | Yes     | Cdevice     | Cdevice | Non-Rechargeable    |
    #| NX961-DRW    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2400800489 | NoDev    | 2400800488     | Yes     | 2000816933  | Cdevice | Non-Rechargeable    |
    #| CX160S-DRWC  | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2400801281 | NoDev    | 2400801280     | Yes     | 2000816933  | Cdevice | Rechargeable        |
    #| RT977-DWC    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2100819768 | NoDev    | 2100817051     | Yes     | 2000816933  | Cdevice | Rechargeable        |
    #| VI962-DRW    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2400812375 | NoDev    | 2400812374     | Yes     | 2400812374  | Cdevice | Non-Rechargeable    |
    | VI960S-DRWC  | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         |   2586114318 | NoDev    |   2586146387     | Yes     |   2586146387  | Cdevice | Rechargeable        |


