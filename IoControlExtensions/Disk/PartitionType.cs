namespace IoControl.Disk
{
    /// <summary>
    /// https://www.win.tue.nl/~aeb/partitions/partition_types-1.html
    /// ? https://docs.microsoft.com/en-us/windows/desktop/fileio/disk-partition-types
    /// </summary>
    public enum PartitionType : byte
    {
        /// <summary>
        /// Entry unused
        /// </summary>
        Empty = 0x00,
        /// <summary>
        /// DOS 12-bit FAT
        /// </summary>
        Fat12 = 0x01,
        /// <summary>
        /// Xenix root
        /// </summary>
        XenixRoot = 0x02,
        /// <summary>
        /// Xenix /usr
        /// </summary>
        XenixUsr = 0x03,
        /// <summary>
        /// DOS 3.0+ 16-bit FAT (up to 32M)
        /// </summary>
        Fat16 = 0x04,
        /// <summary>
        /// DOS 3.3+ Extended Partition
        /// </summary>
        Extended = 0x05,
        /// <summary>
        /// DOS 3.31+ 16-bit FAT (over 32M)
        /// </summary>
        Fat16Over32M = 0x06,
        /// <summary>
        /// OS/2 IFS (e.g., HPFS) 
        /// or Windows NT NTFS 
        /// or exFAT 
        /// or Advanced Unix 
        /// or QNX2.x pre-1988 (see below under IDs 4d-4f)
        /// </summary>
        NTFS_ExFAT = 0x07,
        /// <summary>
        /// OS/2 (v1.0-1.3 only) or AIX boot partition 
        /// or SplitDrive 
        /// or Commodore DOS 
        /// or DELL partition spanning multiple drives
        /// or QNX 1.x and 2.x ("qny")
        /// </summary>
        OS2_1_3 = 0x08,
        /// <summary>
        /// AIX data partition 
        /// or Coherent filesystem
        /// or QNX 1.x and 2.x ("qnz")
        /// </summary>
        AIXData = 0x09,
        /// <summary>
        /// OS/2 Boot Manager 
        /// or Coherent swap partition
        /// or OPUS
        /// </summary>
        OS2BOOTMGR = 0x0A,
        /// <summary>
        /// WIN95 OSR2 FAT32
        /// </summary>
        Fat32 = 0x0B,
        /// <summary>
        /// WIN95 OSR2 FAT32, LBA-mapped
        /// </summary>
        Fat32Xint13 = 0x0C,
        /// <summary>
        /// SILICON SAFE
        /// </summary>
        SiliconSafe = 0x0d,
        /// <summary>
        /// WIN95: DOS 16-bit FAT, LBA-mapped
        /// </summary>
        Xint13 = 0x0E,
        /// <summary>
        /// WIN95: Extended partition, LBA-mapped
        /// </summary>
        Xint13Extend = 0x0F,
        /// <summary>
        /// OPUS (?)
        /// </summary>
        Opus = 0x10,
        /// <summary>
        /// Hidden DOS 12-bit FAT 
        /// or Leading Edge DOS 3.x logically sectored FAT
        /// </summary>
        HiddenFat12 = 0x11,
        /// <summary>
        /// Configuration/diagnostics partition
        /// </summary>
        ConfigrationDiagnotics = 0x12,
        /// <summary>
        /// Hidden DOS 16-bit FAT &lt;32M
        /// or AST DOS with logically sectored FAT
        /// </summary>
        HiddenFat16 = 0x14,
        /// <summary>
        /// Hidden DOS 16-bit FAT &gt;=32M
        /// </summary>
        HiddenFat16Over32M = 0x16,
        /// <summary>
        /// Hidden IFS (e.g., HPFS)
        /// </summary>
        HiddenIFS = 0x17,
        /// <summary>
        /// AST SmartSleep Partition
        /// </summary>
        ASTSmartSleep = 0x18,
        /// <summary>
        /// Unused
        /// </summary>
        Unused = 0x19,
        /// <summary>
        /// Hidden WIN95 OSR2 FAT32
        /// </summary>
        HiddenFat32 = 0x1b,
        /// <summary>
        /// Hidden WIN95 OSR2 FAT32, LBA-mapped
        /// </summary>
        HiddenFat32LBA = 0x1c,
        /// <summary>
        /// Hidden WIN95 16-bit FAT, LBA-mapped
        /// </summary>
        HiddenFat16LBA = 0x1e,
        /// <summary>
        /// Unused
        /// </summary>
        Unused2 = 0x20,
        /// <summary>
        /// Reserved
        /// </summary>
        Reserved = 0x21,
        /// <summary>
        /// Unused
        /// </summary>
        Unused3 = 0x22,
        /// <summary>
        /// Reserved
        /// </summary>
        Reserved2 = 0x23,
        /// <summary>
        /// NEC DOS 3.x
        /// </summary>
        NECDOS = 0x24,
        /// <summary>
        /// Reserved
        /// </summary>
        Reserved3 = 0x26,
        /// <summary>
        /// PQservice
        /// or Windows RE hidden partition
        /// or MirOS partition
        /// or RouterBOOT kernel partition
        /// </summary>
        WinReHidden = 0x27,
        /// <summary>
        /// AtheOS File System (AFS)
        /// </summary>
        AFS = 0x2a,
        /// <summary>
        /// SyllableSecure (SylStor)
        /// </summary>
        SylStor = 0x2b,
        /// <summary>
        /// Reserved
        /// </summary>
        Reserved4 = 0x31,
        /// <summary>
        /// NOS
        /// </summary>
        NOS = 0x32,
        /// <summary>
        /// Reserved
        /// </summary>
        Reserved5 = 0x33,
        /// <summary>
        /// Reserved
        /// </summary>
        Reserved6 = 0x34,
        /// <summary>
        ///  JFS on OS/2 or eCS
        /// </summary>
        JSF = 0x35,
        /// <summary>
        /// Reserved
        /// </summary>
        Reserved7 = 0x36,
        /// <summary>
        /// THEOS ver 3.2 2gb partition
        /// </summary>
        THEOS2G = 0x38,
        /// <summary>
        /// Plan 9 partition
        /// or THEOS ver 4 spanned partition
        /// </summary>
        Plan9 = 0x39,
        /// <summary>
        /// THEOS ver 4 4gb partition
        /// </summary>
        THEOS4G = 0x3a,
        /// <summary>
        /// THEOS ver 4 extended partition
        /// </summary>
        THEOSExtend = 0x3b,
        /// <summary>
        /// PartitionMagic recovery partition
        /// </summary>
        PartitionMagicRecovery = 0x3c,
        /// <summary>
        /// Hidden NetWare
        /// </summary>
        HiddenNetWare = 0x3d,
        /// <summary>
        /// Venix 80286
        /// or PICK
        /// </summary>
        Venix = 0x40,
        /// <summary>
        /// Linux/MINIX (sharing disk with DRDOS)
        /// or Personal RISC Boot
        /// or PPC PReP (Power PC Reference Platform) Boot
        /// </summary>
        Prep = 0x41,
        /// <summary>
        /// Linux swap (sharing disk with DRDOS)
        /// or SFS (Secure Filesystem)
        /// or Windows 2000 dynamic extended partition marker
        /// </summary>
        Ldm = 0x42,
        /// <summary>
        /// Linux native (sharing disk with DRDOS)
        /// </summary>
        LinuxNative = 0x43,
        /// <summary>
        /// GoBack partition
        /// </summary>
        GoBack = 0x44,
        /// <summary>
        /// Boot-US boot manager
        /// or Priam
        /// or EUMEL/Elan
        /// </summary>
        BootUs = 0x45,
        /// <summary>
        /// EUMEL/Elan
        /// </summary>
        EUMELElan2 = 0x46,
        /// <summary>
        /// EUMEL/Elan 
        /// </summary>
        EUMELElan3 = 0x47,
        /// <summary>
        /// EUMEL/Elan 
        /// </summary>
        EUMELElan4 = 0x48,
        /// <summary>
        /// Mark Aitchison's ALFS/THIN lightweight filesystem for DOS
        /// or AdaOS Aquila (Withdrawn)
        /// </summary>
        ALFSTHIN = 0x4a,
        /// <summary>
        /// Oberon partition
        /// </summary>
        Oberon = 0x4c,
        /// <summary>
        /// QNX4.x
        /// </summary>
        QNX4x = 0x4d,
        /// <summary>
        /// QNX4.x 2nd part
        /// </summary>
        QNX4x2 = 0x4e,
        /// <summary>
        /// QNX4.x 3rd part
        /// or Oberon partition
        /// </summary>
        QNX4x3 = 0x4f,
        /// <summary>
        /// OnTrack Disk Manager (older versions) RO
        /// or Lynx RTOS
        /// </summary>
        LynxRTOS = 0x50,
        Unix = 0x63, // Unix
        ValidNtft = 0xC0, // NTFT uses high order bits
        Ntft = 0x80,  // NTFT partition
        /// <summary>
        /// Prime
        /// or Solaris x86
        /// or Linux swap
        /// </summary>
        LinuxSwap = 0x82,
        /// <summary>
        /// Linux native partition
        /// </summary>
        LinuxNative2 = 0x83
    }
}
