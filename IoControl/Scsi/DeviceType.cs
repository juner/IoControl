using System;
using System.Collections.Generic;
using System.Text;

namespace IoControl.Scsi
{
    /// <summary>
    /// SCSI Type Codex
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/install/identifiers-for-scsi-devices
    /// </summary>
    public enum DeviceType : byte
    {
        DirectAccessDevice = 0x00,
        SequentialAccessDevice = 0x01,
        PrinterDevice = 0x02,
        ProcessorDevice = 0x03,
        WriteOnceReadMultipleDevice = 0x04,
        ReadOnlyDirectAccessDevice = 0x05,
        ScannerDevice = 0x06,
        OpticalDevice = 0x07,
        MediumChanger = 0x08,
        CommunicationDevice = 0x09,
        DefinedByASCIT8_1 = 0x0a,
        DefinedByASCIT8_2 = 0x0b,
        StorageArrayControllerDevice = 0x0c,
        EnclosureServicesDevice = 0x0d,
        SimplifiedDirectAccessDevice = 0x0e,
        OpticalCardReaderWriterDevice = 0x0f,
        ReservedForBridgingExpanders = 0x10,
        ObjectBasedStorageDevice = 0x11,
        AutomationDriveInterface = 0x12,
        SecurityManagerDevice = 0x13,
        HostManagedZonedBlockDevice = 0x14,
        Noname = 0x15,
        Reserved1 = 0x16,
        Reserved2 = 0x17,
        Reserved3 = 0x18,
        Reserved4 = 0x19,
        Reserved5 = 0x1a,
        Reserved6 = 0x1b,
        Reserved7 = 0x1c,
        Reserved8 = 0x1d,
        WellKnownLogicalUnit = 0x1e,
        UnknownOrNoDeviceType = 0x1c, 
    }
}
