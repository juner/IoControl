using System;
using System.Runtime.InteropServices;
using static System.Linq.Enumerable;

namespace IoControl.Controller
{
    /// <summary>
    /// SCSI_PASS_THROUGH
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct ScsiPassThrough : IScsiPathThrough
    {
        public readonly ushort Length;
        public readonly byte PathId;
        public readonly byte TargetId;
        public readonly byte Lun;
        public readonly byte CdbLength;
        public readonly byte SenseInfoLength;
        public readonly ScsiData DataIn;
        public readonly uint DataTransferLength;
        public readonly uint TimeOutValue;
        public readonly IntPtr DataBufferOffset;
        public readonly uint SenseInfoOffset;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst =16)]
        public readonly byte[] Cdb;
        public ScsiPassThrough(ushort Length = default, byte PathId = default, byte TargetId = default, byte Lun = default, byte CdbLength = default, byte SenseInfoLength = default, ScsiData DataIn = default, uint DataTransferLength = default, uint TimeOutValue = default, IntPtr DataBufferOffset = default, uint SenseInfoOffset = default, byte[] Cdb = null)
            => (this.Length, this.PathId, this.TargetId, this.Lun, this.CdbLength, this.SenseInfoLength, this.DataIn, this.DataTransferLength, this.TimeOutValue, this.DataBufferOffset, this.SenseInfoOffset, this.Cdb)
            = (Length, PathId, TargetId, Lun, CdbLength, SenseInfoLength, DataIn, DataTransferLength, TimeOutValue, DataBufferOffset, SenseInfoOffset
            , (Cdb ?? Empty<byte>()).Concat(Repeat<byte>(0, 16)).Take(16).ToArray());
        public ScsiPassThrough Set(ushort? Length = null, byte? PathId = null, byte? TargetId = null, byte? Lun = null, byte? CdbLength = null, byte? SenseInfoLength = default, ScsiData? DataIn = null, uint? DataTransferLength = null, uint? TimeOutValue = default, IntPtr? DataBufferOffset = null, uint? SenseInfoOffset = null, byte[] Cdb = null)
            => Length == null && PathId == null && TargetId == null && Lun == null && CdbLength == null && SenseInfoLength == null && DataIn == null && DataTransferLength == null && TimeOutValue == null && DataBufferOffset == null && SenseInfoOffset == null && Cdb == null ? this
            : new ScsiPassThrough(Length ?? this.Length, PathId ?? this.PathId, TargetId ?? this.TargetId, Lun ?? this.Lun, CdbLength ?? this.CdbLength, SenseInfoLength ?? this.SenseInfoLength, DataIn ?? this.DataIn, DataTransferLength ?? this.DataTransferLength, TimeOutValue ?? this.TimeOutValue, DataBufferOffset ?? this.DataBufferOffset, SenseInfoOffset ?? this.SenseInfoOffset, Cdb ?? this.Cdb);
        public override string ToString()
            => $"{nameof(ScsiPassThrough)}{{"
            + $"{nameof(Length)}:{Length}"
            + $", {nameof(PathId)}:{PathId}"
            + $", {nameof(TargetId)}:{TargetId}"
            + $", {nameof(Lun)}:{Lun}"
            + $", {nameof(CdbLength)}:{CdbLength}"
            + $", {nameof(SenseInfoLength)}:{SenseInfoLength}"
            + $", {nameof(DataIn)}:{DataIn}"
            + $", {nameof(DataTransferLength)}:{DataTransferLength}"
            + $", {nameof(TimeOutValue)}:{TimeOutValue}"
            + $", {nameof(DataBufferOffset)}:{DataBufferOffset}"
            + $", {nameof(SenseInfoOffset)}:{SenseInfoOffset}"
            + $", {nameof(Cdb)}:[{string.Join(" ", (Cdb ?? Empty<byte>()).Concat(Repeat<byte>(0,16)).Take(16).Select(v => $"{v:X2}"))}]"
            + $"}}";
    }
}
