using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    /// <summary>
    /// CREATE_DISK_MBR structure ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ns-ntdddisk-_create_disk_mbr )
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CreateDiskMbr
    {
        public uint Signature;
        public override string ToString()
            => $"{nameof(CreateDiskMbr)}{{{nameof(Signature)}:{Signature}}}";
    }
}
