using static System.Linq.Enumerable;
using System.Runtime.InteropServices;
using static IoControl.Controller.AtaPassThroughUtils;

namespace IoControl.Controller
{
    [StructLayout(LayoutKind.Sequential, Pack =8)]
    public readonly struct AtaPassThroughExWithSmartData : IAtaPassThroughEx<SmartData>
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
        public readonly SmartData SmartData;
        IAtaPassThroughEx IAtaPassThroughEx.Header => new AtaPassThroughEx(this);
        SmartData IAtaPassThroughEx<SmartData>.Data => SmartData;
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
        public AtaPassThroughExWithSmartData(AtaFlags AtaFlags = default, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, TaskFile TaskFile = default, SmartData SmartData = default)
            => (Length, this.AtaFlags, this.PathId, this.TargetId, this.Lun, this.ReservedAsUchar, this.TimeOutValue, this.ReservedAsUlong, DataTransferLength, DataBufferOffset, this.TaskFile, this.SmartData)
                = ((ushort)Marshal.SizeOf<AtaPassThroughEx>(), AtaFlags, PathId, TargetId, Lun, ReservedAsUchar, TimeOutValue, ReservedAsUlong, (uint)Marshal.SizeOf<SmartData>(), Marshal.SizeOf<AtaPassThroughEx>(), TaskFile, SmartData);
        public AtaPassThroughExWithSmartData(AtaFlags AtaFlags = default, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, ushort Feature = default, ushort SectorCouont = default, ushort SectorNumber = default, uint Cylinder = default, byte DeviceHead = default, byte Command = default, ushort Reserved = default, SmartData SmartData = default)
            : this(AtaFlags, PathId, TargetId, Lun, ReservedAsUchar, TimeOutValue, ReservedAsUlong, new TaskFile(AtaFlags, Feature, SectorCouont, SectorNumber, Cylinder, DeviceHead, Command, Reserved), SmartData) { }
        public AtaPassThroughExWithSmartData(AtaFlags AtaFlags = default, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, ushort Feature = default, ushort SectorCouont = default, ulong Lba = default, byte DeviceHead = default, byte Command = default, ushort Reserved = default, SmartData SmartData = default)
            : this(AtaFlags, PathId, TargetId, Lun, ReservedAsUchar, TimeOutValue, ReservedAsUlong, new TaskFile(AtaFlags, Feature, SectorCouont, Lba, DeviceHead, Command, Reserved), SmartData) { }

        public override string ToString()
            => $"{nameof(AtaPassThroughExWithSmartData)}{{{nameof(Length)}:{Length}, {nameof(AtaFlags)}:{AtaFlags}, {nameof(PathId)}:{PathId}, {nameof(TargetId)}:{TargetId}, {nameof(Lun)}:{Lun}, {nameof(ReservedAsUchar)}:{ReservedAsUchar}, {nameof(DataTransferLength)}:{DataTransferLength}, {nameof(TimeOutValue)}:{TimeOutValue}, {nameof(ReservedAsUlong)}:{ReservedAsUlong}, {nameof(DataBufferOffset)}:{DataBufferOffset}, {nameof(PreviousTaskFile)}:[{string.Join(" ", PreviousTaskFile.Select(v => $"{v:X2}"))}], {nameof(CurrentTaskFile)}:[{string.Join(" ", CurrentTaskFile.Select(v => $"{v:X2}"))}], {nameof(SmartData)}:{SmartData}}}";
    }
    [StructLayout(LayoutKind.Sequential, Pack =4)]
    public readonly struct AtaPassThroughEx32WithSmartData : IAtaPassThroughEx<SmartData>
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
        public readonly SmartData SmartData;
        IAtaPassThroughEx IAtaPassThroughEx.Header => new AtaPassThroughEx32(this);
        SmartData IAtaPassThroughEx<SmartData>.Data => SmartData;
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
        public AtaPassThroughEx32WithSmartData(AtaFlags AtaFlags = default, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, TaskFile TaskFile = default, SmartData SmartData = default)
            => (Length, this.AtaFlags, this.PathId, this.TargetId, this.Lun, this.ReservedAsUchar, this.TimeOutValue, this.ReservedAsUlong, DataTransferLength, DataBufferOffset, this.TaskFile, this.SmartData)
                = ((ushort)Marshal.SizeOf<AtaPassThroughEx>(), AtaFlags, PathId, TargetId, Lun, ReservedAsUchar, TimeOutValue, ReservedAsUlong, (uint)Marshal.SizeOf<SmartData>(), Marshal.SizeOf<AtaPassThroughEx>(), TaskFile, SmartData);
        public AtaPassThroughEx32WithSmartData(AtaFlags AtaFlags = default, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, ushort Feature = default, ushort SectorCouont = default, ushort SectorNumber = default, uint Cylinder = default, byte DeviceHead = default, byte Command = default, ushort Reserved = default, SmartData SmartData = default)
            : this(AtaFlags, PathId, TargetId, Lun, ReservedAsUchar, TimeOutValue, ReservedAsUlong, new TaskFile(AtaFlags, Feature, SectorCouont, SectorNumber, Cylinder, DeviceHead, Command, Reserved), SmartData) { }
        public AtaPassThroughEx32WithSmartData(AtaFlags AtaFlags = default, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, ushort Feature = default, ushort SectorCouont = default, ulong Lba = default, byte DeviceHead = default, byte Command = default, ushort Reserved = default, SmartData SmartData = default)
            : this(AtaFlags, PathId, TargetId, Lun, ReservedAsUchar, TimeOutValue, ReservedAsUlong, new TaskFile(AtaFlags, Feature, SectorCouont, Lba, DeviceHead, Command, Reserved), SmartData) { }

        public override string ToString()
            => $"{nameof(AtaPassThroughEx32WithSmartData)}{{{nameof(Length)}:{Length}, {nameof(AtaFlags)}:{AtaFlags}, {nameof(PathId)}:{PathId}, {nameof(TargetId)}:{TargetId}, {nameof(Lun)}:{Lun}, {nameof(ReservedAsUchar)}:{ReservedAsUchar}, {nameof(DataTransferLength)}:{DataTransferLength}, {nameof(TimeOutValue)}:{TimeOutValue}, {nameof(ReservedAsUlong)}:{ReservedAsUlong}, {nameof(DataBufferOffset)}:{DataBufferOffset}, {nameof(PreviousTaskFile)}:[{string.Join(" ", PreviousTaskFile.Select(v => $"{v:X2}"))}], {nameof(CurrentTaskFile)}:[{string.Join(" ", CurrentTaskFile.Select(v => $"{v:X2}"))}], {nameof(SmartData)}:{SmartData}}}";
    }
}
