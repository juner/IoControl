namespace IoControl.Disk
{
    public enum PartitionType : byte
    {
        EntryUnused = 0x00, // Entry unused
        Fat12 = 0x01, // 12-bit FAT entries
        Xenix1 = 0x02, // Xenix
        Xenix2 = 0x03, // Xenix
        Fat16 = 0x04, // 16-bit FAT entries
        Extended = 0x05, // Extended partition entry
        Huge = 0x06, // Huge partition MS-DOS V4
        Ifs = 0x07, // IFS Partition
        OS2BOOTMGR = 0x0A, // OS/2 Boot Manager/OPUS/Coherent swap
        Fat32 = 0x0B, // FAT32
        Fat32Xint13 = 0x0C, // FAT32 using extended int13 services
        Xint13 = 0x0E, // Win95 partition using extended int13 services
        Xint13Extend = 0x0F, // Same as type 5 but uses extended int13 services
        Prep = 0x41, // PowerPC Reference Platform (PReP) Boot Partition
        Ldm = 0x42, // Logical Disk Manager partition
        Unix = 0x63, // Unix
        ValidNtft = 0xC0, // NTFT uses high order bits
        Ntft = 0x80,  // NTFT partition
        LinuxSwap = 0x82, //An ext2/ext3/ext4 swap partition
        LinuxNative = 0x83 //An ext2/ext3/ext4 native partition
    }
}
