using System;

namespace IoControl.Disk
{
    [Flags]
    public enum EFIPartitionAttributes : ulong
    {
        GetAttributePlatformRequired = 0x0000_0000_0000_0001,
        LegacyBIOSBootale = 0x0000_0000_0000_0004,
        GptBasicDataAttributeNoDriveLetter = 0x8000_0000_0000_0000,
        GetBasicDataAttributeHidden = 0x4000_0000_0000_0000,
        GetBasicDataAttributeShadowCopy = 0x2000_0000_0000_0000,
        GetBasicDataAttributeReadOnly = 0x1000_0000_0000_0000,
    }

#endregion
}
