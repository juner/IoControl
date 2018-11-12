using System;
using static System.Linq.Enumerable;
using System.Runtime.InteropServices;
using static IoControl.Controller.AtaPassThroughUtils;

namespace IoControl.Controller
{
    /// <summary>
    /// ATA_PASS_THROUGH_EX structure ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddscsi/ns-ntddscsi-_ata_pass_through_ex )
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct AtaPassThroughEx : IAtaPassThroughEx
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
        private readonly long DataBufferOffset;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        readonly byte[] _PreviousTaskFile;
        public byte[] PreviousTaskFile => (_PreviousTaskFile ?? Empty<byte>()).Concat(Repeat<byte>(0, 8)).Take(8).ToArray();
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        readonly byte[] _CurrentTaskFile;
        public byte[] CurrentTaskFile => (_CurrentTaskFile ?? Empty<byte>()).Concat(Repeat<byte>(0, 8)).Take(8).ToArray();
        IAtaPassThroughEx IAtaPassThroughEx.Header => this;
        ushort IAtaPassThroughEx.Length => Length;
        AtaFlags IAtaPassThroughEx.AtaFlags => AtaFlags;
        byte IAtaPassThroughEx.PathId => PathId;
        byte IAtaPassThroughEx.TargetId => TargetId;
        byte IAtaPassThroughEx.Lun => Lun;
        byte IAtaPassThroughEx.ReservedAsUchar => ReservedAsUchar;
        uint IAtaPassThroughEx.DataTransferLength => DataTransferLength;
        uint IAtaPassThroughEx.TimeOutValue => TimeOutValue;
        uint IAtaPassThroughEx.ReservedAsUlong => ReservedAsUlong;
        long IAtaPassThroughEx.DataBufferOffset => DataBufferOffset;
        byte[] IAtaPassThroughEx.PreviousTaskFile => PreviousTaskFile;
        byte[] IAtaPassThroughEx.CurrentTaskFile => CurrentTaskFile;
        public AtaPassThroughEx(AtaFlags AtaFlags = default, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, uint DataTransferLength = default, long DataBufferOffset = default, ushort Feature = default, ushort SectorCouont = default, ushort SectorNumber = default, uint Cylinder = default, byte DeviceHead = default, byte Command = default, ushort Reserved = default)
            => (Length, this.AtaFlags, this.PathId, this.TargetId, this.Lun, this.ReservedAsUchar, this.TimeOutValue, this.ReservedAsUlong, this.DataTransferLength, this.DataBufferOffset, (_CurrentTaskFile, _PreviousTaskFile))
                = ((ushort)Marshal.SizeOf<AtaPassThroughEx>(), AtaFlags, PathId, TargetId, Lun, ReservedAsUchar, TimeOutValue, ReservedAsUlong, DataTransferLength, DataBufferOffset, CreateTaskFiles(AtaFlags, Feature, SectorCouont, SectorNumber, Cylinder, DeviceHead, Command, Reserved));
        public AtaPassThroughEx(AtaFlags AtaFlags = default, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, uint DataTransferLength = default, uint DataBufferOffset = default, ushort Feature = default, ushort SectorCouont = default, ulong Lba = default, byte DeviceHead = default, byte Command = default, ushort Reserved = default)
            => (Length, this.AtaFlags, this.PathId, this.TargetId, this.Lun, this.ReservedAsUchar, this.TimeOutValue, this.ReservedAsUlong, this.DataTransferLength, this.DataBufferOffset, (_CurrentTaskFile, _PreviousTaskFile))
                = ((ushort)Marshal.SizeOf<AtaPassThroughEx>(), AtaFlags, PathId, TargetId, Lun, ReservedAsUchar, TimeOutValue, ReservedAsUlong, DataTransferLength, DataBufferOffset, CreateTaskFiles(AtaFlags, Feature, SectorCouont, Lba, DeviceHead, Command, Reserved));
        public AtaPassThroughEx(IAtaPassThroughEx self)
            => (Length, AtaFlags, PathId, TargetId, Lun, ReservedAsUchar, TimeOutValue, ReservedAsUlong, DataTransferLength, DataBufferOffset, _CurrentTaskFile, _PreviousTaskFile)
            = (self.Length, self.AtaFlags, self.PathId, self.TargetId, self.Lun, self.ReservedAsUchar, self.TimeOutValue, self.ReservedAsUlong, self.DataTransferLength, self.DataBufferOffset, self.CurrentTaskFile, self.PreviousTaskFile);
        public override string ToString()
            => $"{nameof(AtaPassThroughEx)}{{{nameof(Length)}:{Length}, {nameof(AtaFlags)}:{AtaFlags}, {nameof(PathId)}:{PathId}, {nameof(TargetId)}:{TargetId}, {nameof(Lun)}:{Lun}, {nameof(ReservedAsUchar)}:{ReservedAsUchar}, {nameof(DataTransferLength)}:{DataTransferLength}, {nameof(TimeOutValue)}:{TimeOutValue}, {nameof(ReservedAsUlong)}:{ReservedAsUlong}, {nameof(DataBufferOffset)}:{DataBufferOffset}, {nameof(PreviousTaskFile)}:[{string.Join(" ", PreviousTaskFile.Select(v => $"{v:X2}"))}], {nameof(_CurrentTaskFile)}:[{string.Join(" ", CurrentTaskFile.Select(v => $"{v:X2}"))}]}}";
    }
    /// <summary>
    /// ATA_PASS_THROUGH_EX structure ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddscsi/ns-ntddscsi-_ata_pass_through_ex )
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct AtaPassThroughEx32 : IAtaPassThroughEx
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
        private readonly int DataBufferOffset;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        readonly byte[] _PreviousTaskFile;
        public byte[] PreviousTaskFile => (_PreviousTaskFile ?? Empty<byte>()).Concat(Repeat<byte>(0, 8)).Take(8).ToArray();
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        readonly byte[] _CurrentTaskFile;
        public byte[] CurrentTaskFile => (_CurrentTaskFile ?? Empty<byte>()).Concat(Repeat<byte>(0, 8)).Take(8).ToArray();
        IAtaPassThroughEx IAtaPassThroughEx.Header => this;
        ushort IAtaPassThroughEx.Length => Length;
        AtaFlags IAtaPassThroughEx.AtaFlags => AtaFlags;
        byte IAtaPassThroughEx.PathId => PathId;
        byte IAtaPassThroughEx.TargetId => TargetId;
        byte IAtaPassThroughEx.Lun => Lun;
        byte IAtaPassThroughEx.ReservedAsUchar => ReservedAsUchar;
        uint IAtaPassThroughEx.DataTransferLength => DataTransferLength;
        uint IAtaPassThroughEx.TimeOutValue => TimeOutValue;
        uint IAtaPassThroughEx.ReservedAsUlong => ReservedAsUlong;
        long IAtaPassThroughEx.DataBufferOffset => DataBufferOffset;
        byte[] IAtaPassThroughEx.PreviousTaskFile => PreviousTaskFile;
        byte[] IAtaPassThroughEx.CurrentTaskFile => CurrentTaskFile;
        public AtaPassThroughEx32(AtaFlags AtaFlags = default, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, uint DataTransferLength = default, int DataBufferOffset = default, ushort Feature = default, ushort SectorCouont = default, ushort SectorNumber = default, uint Cylinder = default, byte DeviceHead = default, byte Command = default, ushort Reserved = default)
            => (Length, this.AtaFlags, this.PathId, this.TargetId, this.Lun, this.ReservedAsUchar, this.TimeOutValue, this.ReservedAsUlong, this.DataTransferLength, this.DataBufferOffset, (_CurrentTaskFile, _PreviousTaskFile))
                = ((ushort)Marshal.SizeOf<AtaPassThroughEx32>(), AtaFlags, PathId, TargetId, Lun, ReservedAsUchar, TimeOutValue, ReservedAsUlong, DataTransferLength, DataBufferOffset, CreateTaskFiles(AtaFlags, Feature, SectorCouont, SectorNumber, Cylinder, DeviceHead, Command, Reserved));
        public AtaPassThroughEx32(AtaFlags AtaFlags = default, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, uint DataTransferLength = default, int DataBufferOffset = default, ushort Feature = default, ushort SectorCouont = default, ulong Lba = default, byte DeviceHead = default, byte Command = default, ushort Reserved = default)
            => (Length, this.AtaFlags, this.PathId, this.TargetId, this.Lun, this.ReservedAsUchar, this.TimeOutValue, this.ReservedAsUlong, this.DataTransferLength, this.DataBufferOffset, (_CurrentTaskFile, _PreviousTaskFile))
                = ((ushort)Marshal.SizeOf<AtaPassThroughEx32>(), AtaFlags, PathId, TargetId, Lun, ReservedAsUchar, TimeOutValue, ReservedAsUlong, DataTransferLength, DataBufferOffset, CreateTaskFiles(AtaFlags, Feature, SectorCouont, Lba, DeviceHead, Command, Reserved));
        public AtaPassThroughEx32(IAtaPassThroughEx self)
            => (Length, AtaFlags, PathId, TargetId, Lun, ReservedAsUchar, TimeOutValue, ReservedAsUlong, DataTransferLength, DataBufferOffset, _CurrentTaskFile, _PreviousTaskFile)
            = (self.Length, self.AtaFlags, self.PathId, self.TargetId, self.Lun, self.ReservedAsUchar, self.TimeOutValue, self.ReservedAsUlong, self.DataTransferLength, (int)self.DataBufferOffset, self.CurrentTaskFile, self.PreviousTaskFile);
        public override string ToString()
            => $"{nameof(AtaPassThroughEx32)}{{{nameof(Length)}:{Length}, {nameof(AtaFlags)}:{AtaFlags}, {nameof(PathId)}:{PathId}, {nameof(TargetId)}:{TargetId}, {nameof(Lun)}:{Lun}, {nameof(ReservedAsUchar)}:{ReservedAsUchar}, {nameof(DataTransferLength)}:{DataTransferLength}, {nameof(TimeOutValue)}:{TimeOutValue}, {nameof(ReservedAsUlong)}:{ReservedAsUlong}, {nameof(DataBufferOffset)}:{DataBufferOffset}, {nameof(PreviousTaskFile)}:[{string.Join(" ", PreviousTaskFile.Select(v => $"{v:X2}"))}], {nameof(_CurrentTaskFile)}:[{string.Join(" ", CurrentTaskFile.Select(v => $"{v:X2}"))}]}}";
    }
}
