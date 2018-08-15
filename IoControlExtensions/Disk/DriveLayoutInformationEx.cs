using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    /// <summary>
    /// DRIVE_LAYOUT_INFORMATION_EX structure ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ns-ntdddisk-_drive_layout_information_ex )
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct DriveLayoutInformationEx
    {
        /// <summary>
        /// Takes a <see cref="PartitionStyle"/> enumerated value that specifies the type of partition table the disk contains.
        /// </summary>
        public readonly PartitionStyle PartitionStyle;
        /// <summary>
        /// Indicates the number of partitions detected on the disk.
        /// </summary>
        public readonly uint PartitionCount;
        /// <summary>
        /// <see cref="DriveLayoutInformationMbr"/> or <see cref="DriveLayoutInformationGpt"/>
        /// </summary>
        private readonly DriveLayoutInformationUnion Info;
        /// <summary>
        /// Indicates the drive layout information for a disk with a Master Boot Record. This member is valid when <see cref="PartitionStyle"/> is <see cref="PartitionStyle.Mbr"/>. See the definition of <see cref="DriveLayoutInformationMbr"/> for more information.
        /// </summary>
        public DriveLayoutInformationMbr Mbr => Info;
        /// <summary>
        /// Indicates the drive layout information for a disk with a GUID Partition Table. This member is valid when <see cref="PartitionStyle"/> is <see cref="PartitionStyle.Gpt"/>. See definition of <see cref="DriveLayoutInformationGpt"/> for more information.
        /// </summary>
        public DriveLayoutInformationGpt Gpt => Info;
        /// <summary>
        /// Contains a variable-length array of <see cref="PartitionInformationEx"/> structures, one for each partition on the drive.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 1)]
        internal readonly PartitionInformationEx[] _PartitionEntry;
        public PartitionInformationEx[] PartitionEntry => _PartitionEntry.Take((int)PartitionCount).ToArray();
        private DriveLayoutInformationEx(PartitionStyle PartitionStyle, DriveLayoutInformationUnion Info, PartitionInformationEx[] PartitionEntry)
            => (this.PartitionStyle, PartitionCount, this.Info, _PartitionEntry) = (PartitionStyle, (uint)(PartitionEntry?.Length ?? 0), Info, (PartitionEntry?.Length ?? 0) == 0 ? new PartitionInformationEx[1] : PartitionEntry);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Mbr"></param>
        /// <param name="PartitionEntry"></param>
        public DriveLayoutInformationEx(DriveLayoutInformationMbr Mbr, params PartitionInformationEx[] PartitionEntry) : this(PartitionStyle.Mbr, Mbr, PartitionEntry) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Gpt"></param>
        /// <param name="PartitionEntry"></param>
        public DriveLayoutInformationEx(DriveLayoutInformationGpt Gpt, params PartitionInformationEx[] PartitionEntry) : this(PartitionStyle.Gpt, Gpt, PartitionEntry) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PartitionEntry"></param>
        public DriveLayoutInformationEx(params PartitionInformationEx[] PartitionEntry) : this(PartitionStyle.Raw, default, PartitionEntry) { }
        public DriveLayoutInformationEx(IntPtr IntPtr, uint Size)
        {
            this = (DriveLayoutInformationEx)Marshal.PtrToStructure(IntPtr, typeof(DriveLayoutInformationEx));
            if (PartitionCount <= 1)
                return;
            var ArrayPtr = IntPtr.Add(IntPtr, (int)Marshal.OffsetOf<DriveLayoutInformationEx>(nameof(_PartitionEntry)));
            var PartitionSize = Marshal.SizeOf<PartitionInformationEx>();
            _PartitionEntry = Enumerable.Range(0, (int)PartitionCount)
                .Select(index => (PartitionInformationEx)Marshal.PtrToStructure(IntPtr.Add(ArrayPtr, PartitionSize), typeof(PartitionInformationEx)))
                .ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PartitionStyle"></param>
        /// <param name="Mbr"></param>
        /// <param name="Gpt"></param>
        /// <param name="PartitionEntry"></param>
        /// <returns></returns>
        public DriveLayoutInformationEx Set(PartitionStyle? PartitionStyle = null, DriveLayoutInformationMbr? Mbr = null, DriveLayoutInformationGpt? Gpt = null, PartitionInformationEx[] PartitionEntry = null)
            => PartitionStyle == Disk.PartitionStyle.Raw || (Mbr == null && Gpt == null && this.PartitionStyle == Disk.PartitionStyle.Raw) ? new DriveLayoutInformationEx(PartitionEntry ?? this._PartitionEntry)
                : Mbr != null || PartitionStyle == Disk.PartitionStyle.Mbr || (PartitionStyle == null && this.PartitionStyle == Disk.PartitionStyle.Mbr) ? new DriveLayoutInformationEx(Mbr ?? this.Mbr, PartitionEntry ?? this._PartitionEntry)
                : Gpt != null || PartitionStyle == Disk.PartitionStyle.Gpt || (PartitionStyle == null && this.PartitionStyle == Disk.PartitionStyle.Gpt) ? new DriveLayoutInformationEx(Gpt ?? this.Gpt, PartitionEntry ?? this._PartitionEntry)
                : this;

        public override string ToString()
            => $"{nameof(DriveLayoutInformationEx)}{{ {nameof(PartitionStyle)}:{PartitionStyle}, {nameof(PartitionCount)}:{PartitionCount}, {(PartitionStyle == PartitionStyle.Gpt ? $"{nameof(Gpt)}:{Gpt}" : PartitionStyle == PartitionStyle.Mbr ? $"{nameof(Mbr)}:{Mbr}" : null)}, {nameof(PartitionEntry)}:[{string.Join(", ", (_PartitionEntry ?? Enumerable.Empty<PartitionInformationEx>()).Take((int)PartitionCount).Select(v => $"{v}"))}] }}";
        /// <summary>
        /// inner structure <see cref="DriveLayoutInformationMbr"/> or <see cref="DriveLayoutInformationGpt"/>
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        private readonly struct DriveLayoutInformationUnion
        {
            [FieldOffset(0)]
            public readonly DriveLayoutInformationMbr Mbr;
            [FieldOffset(0)]
            public readonly DriveLayoutInformationGpt Gpt;
            public DriveLayoutInformationUnion(in DriveLayoutInformationMbr Mbr) : this() => this.Mbr = Mbr;
            public DriveLayoutInformationUnion(in DriveLayoutInformationGpt Gpt) : this() => this.Gpt = Gpt;
            public static implicit operator DriveLayoutInformationUnion(in DriveLayoutInformationMbr Mbr) => new DriveLayoutInformationUnion(Mbr);
            public static implicit operator DriveLayoutInformationMbr(in DriveLayoutInformationUnion Union) => Union.Mbr;
            public static implicit operator DriveLayoutInformationUnion(in DriveLayoutInformationGpt Gpt) => new DriveLayoutInformationUnion(Gpt);
            public static implicit operator DriveLayoutInformationGpt(in DriveLayoutInformationUnion Union) => Union.Gpt;
        }
    }
}
