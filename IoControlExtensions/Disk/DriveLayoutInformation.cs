using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    /// <summary>
    /// DRIVE_LAYOUT_INFORMATION structure ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ns-ntdddisk-_drive_layout_information )
    /// The <see cref="DriveLayoutInformation"/> structure is obsolete and is provided only to support existing drivers. New drivers must use <see cref="DriveLayoutInformationEx"/>. 
    /// The <see cref="DriveLayoutInformation"/> structure is used to report information about a disk drive and its partitions.It is also used to write new drive layout information to the disk.
    /// </summary>
    public readonly struct DriveLayoutInformation
    {
        /// <summary>
        /// Contains the number of partitions on the drive.
        /// </summary>
        public readonly uint PartitionCount;
        /// <summary>
        /// Contains the disk signature.
        /// </summary>
        public readonly uint Signature;
        /// <summary>
        /// Contains a variable-length array of <see cref="PartitionInformation"/> structures, one for each partition on the drive.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst =1)]
        internal readonly PartitionInformation[] _PartitionEntry;
        public PartitionInformation[] PartitionEntry => (_PartitionEntry?? Enumerable.Empty<PartitionInformation>()).Take((int)PartitionCount).ToArray();
        public DriveLayoutInformation(uint Signature, params PartitionInformation[] PartitionEntry)
            => (PartitionCount, this.Signature, this._PartitionEntry) = ((uint)(PartitionEntry?.Length ?? 0), Signature, (PartitionEntry?.Length ?? 0) == 0 ?new PartitionInformation[1] : PartitionEntry);
        public DriveLayoutInformation(IntPtr IntPtr, uint Size)
        {
            this = (DriveLayoutInformation)Marshal.PtrToStructure(IntPtr, typeof(DriveLayoutInformation));
            if (PartitionCount <= 1)
                return;
            var ArrayPtr = IntPtr.Add(IntPtr, Marshal.OffsetOf<DriveLayoutInformation>(nameof(_PartitionEntry)).ToInt32());
            var PartitionSize = Marshal.SizeOf<PartitionInformation>();
            _PartitionEntry = Enumerable
                .Range(0, (int)PartitionCount)
                .Select(index => (PartitionInformation)Marshal.PtrToStructure(IntPtr.Add(ArrayPtr, PartitionSize * index), typeof(PartitionInformation)))
                .ToArray();
        }
        public void Deconstruct(out uint Signature, out PartitionInformation[] PartitionEntry)
            => (Signature, PartitionEntry) = (this.Signature, this._PartitionEntry.Take((int)PartitionCount).ToArray());
        public DriveLayoutInformation Set(uint? Signature = null, PartitionInformation[] PartitionEntry = null)
            => new DriveLayoutInformation(Signature ?? this.Signature, PartitionEntry ?? this.PartitionEntry);
        public override string ToString()
            => $"{nameof(DriveLayoutInformation)}{{{nameof(PartitionCount)}:{PartitionCount}, {nameof(Signature)}:{Signature}, {nameof(PartitionEntry)}:[{ string.Join(", ", PartitionEntry.Select(v => $"{v}"))}]}}";
    }
}
