Feature: VA Prefit

A short summary of the feature

@tag1
Scenario: 01Test Case ID 511987: Verify binaural prefitting can be performed
    Given [Cleaning up dumps before execution starts]
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
    | NX960S-DRWC  | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No          | 2400802586 | NoDev    |   2300804242     | Yes     | 2300804242  | Cdevice | Rechargeable        |
    #| RT961-DRWC   | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2000816934 | NoDev    | 2000816936     | Yes     | 2000816933  | Cdevice | Rechargeable        |
     #| RE988-DWT    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2026489200 | NoDev    | 2026467497     | Yes     | Cdevice     | Cdevice | Wired               |
    #| NX9ITC-DW-MP | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2476130209 | NoDev    | 2476130208     | Yes     | Cdevice     | Cdevice | Non-Rechargeable    |
    #| NX961-DRW    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2400800489 | NoDev    | 2400800488     | Yes     | 2000816933  | Cdevice | Non-Rechargeable    |
    #| CX160S-DRWC  | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2400801281 | NoDev    | 2400801280     | Yes     | 2000816933  | Cdevice | Rechargeable        |
    #| RT977-DWC    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2100819768 | NoDev    | 2100817051     | Yes     | 2000816933  | Cdevice | Rechargeable        |
    #| VI962-DRW    | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         | 2400812375 | NoDev    | 2400812374     | Yes     | 2400812374  | Cdevice | Non-Rechargeable    |
    #| VI960S-DRWC  | Left       | Right       | Device A | Device B | Device C | Device D | Yes      | No         |   2500805695 | NoDev    |   2500805694     | Yes     |   2400811738  | Cdevice | Rechargeable        |