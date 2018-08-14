using System;
using System.Linq;
using System.Runtime.InteropServices;
namespace IoControl.MassStorage
{
    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct StorageDeviceIdDescriptor : IStorageDescriptor
    {
        uint IStorageDescriptor.Version => Version;
        uint IStorageDescriptor.Size => Size;
        public readonly uint Version;
        public readonly uint Size;
        public readonly uint NumberOfIdentifiers;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        readonly byte[] _Identifiers;
        public byte[] Identifiers => (_Identifiers ?? Enumerable.Empty<byte>()).Take((int)NumberOfIdentifiers).ToArray();
        public StorageDeviceIdDescriptor(uint Version, uint Size, byte[] Identifiers)
            => (this.Version, this.Size, this.NumberOfIdentifiers, _Identifiers)
            = (Version, Size, (uint)(Identifiers?.Length ?? 0), (Identifiers?.Length ?? 0) == 0 ? new byte[1] : Identifiers);
        public StorageDeviceIdDescriptor Set(uint? Version = null, uint? Size = null, byte[] Identifiers = null)
            => Version == null && Size == null && Identifiers == null ? this
            : new StorageDeviceIdDescriptor(Version ?? this.Version, Size ?? this.Size, Identifiers ?? _Identifiers);
        public StorageDeviceIdDescriptor(IntPtr IntPtr, uint Size)
        {
            this = (StorageDeviceIdDescriptor)Marshal.PtrToStructure(IntPtr, typeof(StorageDeviceIdDescriptor));
            if (NumberOfIdentifiers == 0)
                return;
            _Identifiers = new byte[NumberOfIdentifiers];
            Marshal.Copy(IntPtr.Add(IntPtr, (int)Marshal.OffsetOf<StorageDeviceIdDescriptor>(nameof(_Identifiers))), _Identifiers, 0, (int)NumberOfIdentifiers);
            
        }
        public override string ToString()
            => $"{nameof(StorageDeviceIdDescriptor)}{{{nameof(Version)}:{Version}, {nameof(Size)}:{Size}, {nameof(NumberOfIdentifiers)}:{NumberOfIdentifiers}, {nameof(Identifiers)}:[{string.Join(" ",Identifiers.Select(v => $"{v:X2}"))}]}}";
    }
}
