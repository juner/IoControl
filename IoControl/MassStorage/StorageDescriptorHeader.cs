using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    /// <summary>
    /// _STORAGE_DESCRIPTOR_HEADER structure ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddstor/ns-ntddstor-_storage_descriptor_header )
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct StorageDescriptorHeader
    {
        public readonly uint Version;
        public readonly uint Size;
        public StorageDescriptorHeader(uint Version, uint Size) => (this.Version, this.Size) = (Version, Size);
        public void Deconstruct(out uint Version, out uint Size) => (Version, Size) = (this.Version, this.Size);
        public override string ToString()
            => $"{nameof(StorageDescriptorHeader)}{{{nameof(Version)}:{Version}, {nameof(Size)}:{Size}}}";
    }
}
