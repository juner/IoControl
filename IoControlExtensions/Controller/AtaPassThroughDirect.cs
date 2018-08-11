using System;
using System.Linq;
using System.Runtime.InteropServices;
using static IoControl.Controller.AtaPassThroughUtils;

namespace IoControl.Controller
{
    /// <summary>
    /// ATA_PASS_THROUGH_DIRECT structure
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddscsi/ns-ntddscsi-_ata_pass_through_direct
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly struct  AtaPassThroughDirect
    {
        public readonly ushort Length;
        public readonly AtaFlags AtaFlags;
        public readonly byte PathId;
        public readonly byte TargetId;
        public readonly byte Lun;
        public readonly byte ReservedAsUchar;
        public readonly uint DataTransferLength;
        public readonly uint TimeOutValue;
        public readonly uint ReservedAsUlong;
        private readonly int _DataBuffer;
        public IntPtr DataBuffer => new IntPtr(_DataBuffer);
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public readonly byte[] PreviousTaskFile;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public readonly byte[] CurrentTaskFile;

        public AtaPassThroughDirect(AtaFlags AtaFlags = default, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, uint DataTransferLength = default, IntPtr DataBuffer = default, ushort Feature = default, ushort SectorCouont = default, ushort SectorNumber = default, uint Cylinder = default, byte DeviceHead = default, byte Command = default, ushort Reserved = default)
            => (Length, this.AtaFlags, this.PathId, this.TargetId, this.Lun, this.ReservedAsUchar, this.TimeOutValue, this.ReservedAsUlong, this.DataTransferLength, _DataBuffer, (CurrentTaskFile, PreviousTaskFile))
                = ((ushort)Marshal.SizeOf<AtaPassThroughDirect>(), AtaFlags, PathId, TargetId, Lun, ReservedAsUchar, TimeOutValue, ReservedAsUlong, DataTransferLength, DataBuffer.ToInt32(), CreateTaskFiles(AtaFlags, Feature, SectorCouont, SectorNumber, Cylinder, DeviceHead, Command, Reserved));
        public AtaPassThroughDirect(AtaFlags AtaFlags = default, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, uint DataTransferLength = default, IntPtr DataBuffer = default, ushort Feature = default, ushort SectorCouont = default, ulong Lba = default, byte DeviceHead = default, byte Command = default, ushort Reserved = default)
            => (Length, this.AtaFlags, this.PathId, this.TargetId, this.Lun, this.ReservedAsUchar, this.TimeOutValue, this.ReservedAsUlong, this.DataTransferLength, _DataBuffer, (CurrentTaskFile, PreviousTaskFile))
                = ((ushort)Marshal.SizeOf<AtaPassThroughDirect>(), AtaFlags, PathId, TargetId, Lun, ReservedAsUchar, TimeOutValue, ReservedAsUlong, DataTransferLength, DataBuffer.ToInt32(), CreateTaskFiles(AtaFlags, Feature, SectorCouont, Lba, DeviceHead, Command, Reserved));
        public override string ToString()
            => $"{nameof(AtaPassThroughDirect)}{{{nameof(Length)}:{Length}, {nameof(AtaFlags)}:{AtaFlags}, {nameof(PathId)}:{PathId}, {nameof(TargetId)}:{TargetId}, {nameof(Lun)}:{Lun}, {nameof(ReservedAsUchar)}:{ReservedAsUchar}, {nameof(DataTransferLength)}:{DataTransferLength}, {nameof(TimeOutValue)}:{TimeOutValue}, {nameof(ReservedAsUlong)}:{ReservedAsUlong}, {nameof(DataBuffer)}:{DataBuffer}, {nameof(PreviousTaskFile)}:[{string.Join(" ", (PreviousTaskFile ?? Enumerable.Empty<byte>()).Select(v => $"{v:X2}"))}], {nameof(CurrentTaskFile)}:[{string.Join(" ", (CurrentTaskFile ?? Enumerable.Empty<byte>()).Select(v => $"{v:X2}"))}]}}";

    }
}
