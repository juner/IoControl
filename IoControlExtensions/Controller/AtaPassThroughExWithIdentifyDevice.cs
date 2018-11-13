using static System.Linq.Enumerable;
using System.Runtime.InteropServices;
using static IoControl.Controller.AtaPassThroughUtils;

namespace IoControl.Controller
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public readonly struct AtaPassThroughExWithIdentifyDevice : IAtaPassThroughEx<IdentifyDevice>
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
        readonly TaskFile TaskFile;
        public byte[] PreviousTaskFile => TaskFile.Previous;
        public byte[] CurrentTaskFile => TaskFile.Current;
        IAtaPassThroughEx IAtaPassThroughEx.Header => new AtaPassThroughEx(this);
        IdentifyDevice IAtaPassThroughEx<IdentifyDevice>.Data => IdentifyDevice;
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
        public readonly IdentifyDevice IdentifyDevice;
        public AtaPassThroughExWithIdentifyDevice(AtaFlags AtaFlags = default, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, TaskFile TaskFile = default, IdentifyDevice IdentifyDevice = default)
            => (Length, this.AtaFlags, this.PathId, this.TargetId, this.Lun, this.ReservedAsUchar, this.TimeOutValue, this.ReservedAsUlong, this.DataTransferLength, this.DataBufferOffset, this.TaskFile, this.IdentifyDevice)
                = ((ushort)Marshal.SizeOf<AtaPassThroughEx>(), AtaFlags, PathId, TargetId, Lun, ReservedAsUchar, TimeOutValue, ReservedAsUlong, (uint)Marshal.SizeOf<IdentifyDevice>(), Marshal.SizeOf<AtaPassThroughEx>(), TaskFile, IdentifyDevice);
        public AtaPassThroughExWithIdentifyDevice(AtaFlags AtaFlags = default, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, ushort Feature = default, ushort SectorCouont = default, ushort SectorNumber = default, uint Cylinder = default, byte DeviceHead = default, byte Command = default, ushort Reserved = default, IdentifyDevice IdentifyDevice = default)
            : this(AtaFlags, PathId, TargetId, Lun, ReservedAsUchar, TimeOutValue, ReservedAsUlong, new TaskFile(AtaFlags, Feature, SectorCouont, SectorNumber, Cylinder, DeviceHead, Command, Reserved), IdentifyDevice) { }
        public AtaPassThroughExWithIdentifyDevice(AtaFlags AtaFlags = default, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, uint DataTransferLength = default, int DataBufferOffset = default, ushort Feature = default, ushort SectorCouont = default, ulong Lba = default, byte DeviceHead = default, byte Command = default, ushort Reserved = default, IdentifyDevice IdentifyDevice = default)
            : this(AtaFlags, PathId, TargetId, Lun, ReservedAsUchar, TimeOutValue, ReservedAsUlong, new TaskFile(AtaFlags, Feature, SectorCouont, Lba, DeviceHead, Command, Reserved), IdentifyDevice) { }

        public override string ToString()
            => $"{nameof(AtaPassThroughExWithIdentifyDevice)}{{{nameof(Length)}:{Length}, {nameof(AtaFlags)}:{AtaFlags}, {nameof(PathId)}:{PathId}, {nameof(TargetId)}:{TargetId}, {nameof(Lun)}:{Lun}, {nameof(ReservedAsUchar)}:{ReservedAsUchar}, {nameof(DataTransferLength)}:{DataTransferLength}, {nameof(TimeOutValue)}:{TimeOutValue}, {nameof(ReservedAsUlong)}:{ReservedAsUlong}, {nameof(DataBufferOffset)}:{DataBufferOffset}, {nameof(PreviousTaskFile)}:[{string.Join(" ", PreviousTaskFile.Select(v => $"{v:X2}"))}], {nameof(CurrentTaskFile)}:[{string.Join(" ", CurrentTaskFile.Select(v => $"{v:X2}"))}], {nameof(IdentifyDevice)}:{IdentifyDevice}}}";
    }
    [StructLayout(LayoutKind.Sequential, Pack =4)]
    public readonly struct AtaPassThroughEx32WithIdentifyDevice : IAtaPassThroughEx<IdentifyDevice>
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
        readonly TaskFile TaskFile;
        public byte[] PreviousTaskFile => TaskFile.Previous;
        public byte[] CurrentTaskFile => TaskFile.Current;
        public readonly IdentifyDevice IdentifyDevice;
        IAtaPassThroughEx IAtaPassThroughEx.Header => new AtaPassThroughEx32(this);
        IdentifyDevice IAtaPassThroughEx<IdentifyDevice>.Data => IdentifyDevice;
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
        public AtaPassThroughEx32WithIdentifyDevice(AtaFlags AtaFlags = default, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, TaskFile TaskFile = default, IdentifyDevice IdentifyDevice = default)
            => (Length, this.AtaFlags, this.PathId, this.TargetId, this.Lun, this.ReservedAsUchar, this.TimeOutValue, this.ReservedAsUlong, this.DataTransferLength, this.DataBufferOffset, this.TaskFile, this.IdentifyDevice)
                = ((ushort)Marshal.SizeOf<AtaPassThroughEx32>(), AtaFlags, PathId, TargetId, Lun, ReservedAsUchar, TimeOutValue, ReservedAsUlong, (uint)Marshal.SizeOf<IdentifyDevice>(), Marshal.SizeOf<AtaPassThroughEx32>(), TaskFile, IdentifyDevice);
        public AtaPassThroughEx32WithIdentifyDevice(AtaFlags AtaFlags = default, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, ushort Feature = default, ushort SectorCouont = default, ushort SectorNumber = default, uint Cylinder = default, byte DeviceHead = default, byte Command = default, ushort Reserved = default, IdentifyDevice IdentifyDevice = default)
            : this(AtaFlags, PathId, TargetId, Lun, ReservedAsUchar, TimeOutValue, ReservedAsUlong, new TaskFile(AtaFlags, Feature, SectorCouont, SectorNumber, Cylinder, DeviceHead, Command, Reserved), IdentifyDevice) { }
        public AtaPassThroughEx32WithIdentifyDevice(AtaFlags AtaFlags = default, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, uint DataTransferLength = default, int DataBufferOffset = default, ushort Feature = default, ushort SectorCouont = default, ulong Lba = default, byte DeviceHead = default, byte Command = default, ushort Reserved = default, IdentifyDevice IdentifyDevice = default)
            : this(AtaFlags, PathId, TargetId, Lun, ReservedAsUchar, TimeOutValue, ReservedAsUlong, new TaskFile(AtaFlags, Feature, SectorCouont, Lba, DeviceHead, Command, Reserved), IdentifyDevice) { }

        public override string ToString()
            => $"{nameof(AtaPassThroughExWithIdentifyDevice)}{{{nameof(Length)}:{Length}, {nameof(AtaFlags)}:{AtaFlags}, {nameof(PathId)}:{PathId}, {nameof(TargetId)}:{TargetId}, {nameof(Lun)}:{Lun}, {nameof(ReservedAsUchar)}:{ReservedAsUchar}, {nameof(DataTransferLength)}:{DataTransferLength}, {nameof(TimeOutValue)}:{TimeOutValue}, {nameof(ReservedAsUlong)}:{ReservedAsUlong}, {nameof(DataBufferOffset)}:{DataBufferOffset}, {nameof(PreviousTaskFile)}:[{string.Join(" ", PreviousTaskFile.Select(v => $"{v:X2}"))}], {nameof(CurrentTaskFile)}:[{string.Join(" ", CurrentTaskFile.Select(v => $"{v:X2}"))}], {nameof(IdentifyDevice)}:{IdentifyDevice}}}";
    }
}
