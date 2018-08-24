using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    /// <summary>
    /// DRIVE_LAYOUT_INFORMATION structure ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ns-ntdddisk-_drive_layout_information )
    /// The <see cref="DriveLayoutInformation"/> structure is obsolete and is provided only to support existing drivers. New drivers must use <see cref="DriveLayoutInformationEx"/>. 
    /// The <see cref="DriveLayoutInformation"/> structure is used to report information about a disk drive and its partitions.It is also used to write new drive layout information to the disk.
    /// </summary>
    public readonly struct DriveLayoutInformation : IEquatable<DriveLayoutInformation>
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
        /// <summary>
        /// 
        /// </summary>
        public PartitionInformation[] PartitionEntry => (_PartitionEntry?? Enumerable.Empty<PartitionInformation>()).Take((int)PartitionCount).ToArray();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Signature"></param>
        /// <param name="PartitionEntry"></param>
        public DriveLayoutInformation(uint Signature, params PartitionInformation[] PartitionEntry)
            => (PartitionCount, this.Signature, _PartitionEntry) = ((uint)(PartitionEntry?.Length ?? 0), Signature, (PartitionEntry?.Length ?? 0) == 0 ?new PartitionInformation[1] : PartitionEntry);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IntPtr"></param>
        /// <param name="Size"></param>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IntPtr"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        public IDisposable CreatePtr(out IntPtr IntPtr, out uint Size)
        {
            var PartitionSize = Marshal.SizeOf<PartitionInformation>();
            Size = (uint)(Marshal.SizeOf<DriveLayoutInformation>()
                + PartitionSize * Math.Max(((int)PartitionCount - 1), 0));
            var _IntPtr = Marshal.AllocCoTaskMem((int)Size);
            var Dispose = Disposable.Create(() => Marshal.FreeCoTaskMem(_IntPtr));
            try
            {
                Marshal.StructureToPtr(this, _IntPtr, false);
                var _Ptr = IntPtr.Add(_IntPtr, (int)Marshal.OffsetOf<DriveLayoutInformation>(nameof(_PartitionEntry)));
                foreach (var (partition, index) in PartitionEntry.Select((p, i) => (p, i)).Skip(1))
                    Marshal.StructureToPtr(partition, IntPtr.Add(_Ptr, PartitionSize * index), false);
                IntPtr = _IntPtr;
                return Dispose;
            }catch(Exception)
            {
                Dispose.Dispose();
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Signature"></param>
        /// <param name="PartitionEntry"></param>
        public void Deconstruct(out uint Signature, out PartitionInformation[] PartitionEntry)
            => (Signature, PartitionEntry) = (this.Signature, this._PartitionEntry.Take((int)PartitionCount).ToArray());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Signature"></param>
        /// <param name="PartitionEntry"></param>
        /// <returns></returns>
        public DriveLayoutInformation Set(uint? Signature = null, PartitionInformation[] PartitionEntry = null)
            => new DriveLayoutInformation(Signature ?? this.Signature, PartitionEntry ?? this.PartitionEntry);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{nameof(DriveLayoutInformation)}{{{nameof(PartitionCount)}:{PartitionCount}, {nameof(Signature)}:{Signature}, {nameof(PartitionEntry)}:[{ string.Join(", ", PartitionEntry.Select(v => $"{v}"))}]}}";

        public override bool Equals(object obj) => obj is DriveLayoutInformation && Equals((DriveLayoutInformation)obj);

        public bool Equals(DriveLayoutInformation other) => PartitionCount == other.PartitionCount &&
                   Signature == other.Signature &&
                   EqualityComparer<PartitionInformation[]>.Default.Equals(PartitionEntry, other.PartitionEntry);

        public override int GetHashCode()
        {
            var hashCode = 1359194508;
            hashCode = hashCode * -1521134295 + PartitionCount.GetHashCode();
            hashCode = hashCode * -1521134295 + Signature.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<PartitionInformation[]>.Default.GetHashCode(PartitionEntry);
            return hashCode;
        }

        public static bool operator ==(DriveLayoutInformation information1, DriveLayoutInformation information2) => information1.Equals(information2);

        public static bool operator !=(DriveLayoutInformation information1, DriveLayoutInformation information2) => !(information1 == information2);
    }
}
