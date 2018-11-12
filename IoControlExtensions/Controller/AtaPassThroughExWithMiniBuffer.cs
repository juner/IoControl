using static System.Linq.Enumerable;
using System.Runtime.InteropServices;
using static IoControl.Controller.AtaPassThroughUtils;

namespace IoControl.Controller
{
    [StructLayout(LayoutKind.Sequential, Pack =8)]
    internal readonly struct AtaPassThroughExWithMiniBuffer : IAtaPassThroughEx<Buffer5>
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
        public readonly TaskFile TaskFile;
        public byte[] PreviousTaskFile => TaskFile.Previous;
        public byte[] CurrentTaskFile => TaskFile.Current;
        public readonly Buffer5 Buffer;
        IAtaPassThroughEx IAtaPassThroughEx.Header => this;
        Buffer5 IAtaPassThroughEx<Buffer5>.Data => Buffer;
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
        public AtaPassThroughExWithMiniBuffer(AtaFlags AtaFlags = default, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, TaskFile TaskFile = default,byte[] Buffer = default)
            => (Length, this.AtaFlags, this.PathId, this.TargetId, this.Lun, this.ReservedAsUchar, this.TimeOutValue, this.ReservedAsUlong, this.DataTransferLength, this.DataBufferOffset, this.TaskFile , this.Buffer)
                = ((ushort)Marshal.SizeOf<AtaPassThroughEx>(), AtaFlags, PathId, TargetId, Lun, ReservedAsUchar, TimeOutValue, ReservedAsUlong, (uint)Marshal.SizeOf<Buffer5>(), Marshal.SizeOf<AtaPassThroughEx>(), TaskFile, Buffer);
       public override string ToString()
            => $"{nameof(AtaPassThroughExWithMiniBuffer)}{{{nameof(Length)}:{Length}, {nameof(AtaFlags)}:{AtaFlags}, {nameof(PathId)}:{PathId}, {nameof(TargetId)}:{TargetId}, {nameof(Lun)}:{Lun}, {nameof(ReservedAsUchar)}:{ReservedAsUchar}, {nameof(DataTransferLength)}:{DataTransferLength}, {nameof(TimeOutValue)}:{TimeOutValue}, {nameof(ReservedAsUlong)}:{ReservedAsUlong}, {nameof(DataBufferOffset)}:{DataBufferOffset}, {nameof(PreviousTaskFile)}:[{string.Join(" ", PreviousTaskFile.Select(v => $"{v:X2}"))}], {nameof(CurrentTaskFile)}:[{string.Join(" ", CurrentTaskFile.Select(v => $"{v:X2}"))}]}}";
    }
    /// <summary>
    /// ATA_PASS_THROUGH_EX structure ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddscsi/ns-ntddscsi-_ata_pass_through_ex )
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
     internal readonly struct AtaPassThroughEx32WithMiniBuffer : IAtaPassThroughEx<Buffer5>
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
        private readonly TaskFile TaskFile;
        public byte[] PreviousTaskFile => TaskFile.Previous;
        public byte[] CurrentTaskFile => TaskFile.Current;
        public readonly Buffer5 Buffer;
        IAtaPassThroughEx IAtaPassThroughEx.Header => this;
        Buffer5 IAtaPassThroughEx<Buffer5>.Data => Buffer;
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
        public AtaPassThroughEx32WithMiniBuffer(AtaFlags AtaFlags = default, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, TaskFile TaskFile = default, byte[] Buffer = default)
            => (Length, this.AtaFlags, this.PathId, this.TargetId, this.Lun, this.ReservedAsUchar, this.TimeOutValue, this.ReservedAsUlong, DataTransferLength, DataBufferOffset, this.TaskFile, this.Buffer)
            = ((ushort)Marshal.SizeOf<AtaPassThroughEx32>(), AtaFlags, PathId, TargetId, Lun, ReservedAsUchar, TimeOutValue, ReservedAsUlong, (uint)Marshal.SizeOf<Buffer5>(), Marshal.SizeOf<AtaPassThroughEx32>(), TaskFile, Buffer);
        public override string ToString()
            => $"{nameof(AtaPassThroughEx32WithMiniBuffer)}{{{nameof(Length)}:{Length}, {nameof(AtaFlags)}:{AtaFlags}, {nameof(PathId)}:{PathId}, {nameof(TargetId)}:{TargetId}, {nameof(Lun)}:{Lun}, {nameof(ReservedAsUchar)}:{ReservedAsUchar}, {nameof(DataTransferLength)}:{DataTransferLength}, {nameof(TimeOutValue)}:{TimeOutValue}, {nameof(ReservedAsUlong)}:{ReservedAsUlong}, {nameof(DataBufferOffset)}:{DataBufferOffset}, {nameof(PreviousTaskFile)}:[{string.Join(" ", PreviousTaskFile.Select(v => $"{v:X2}"))}], {nameof(CurrentTaskFile)}:[{string.Join(" ", CurrentTaskFile.Select(v => $"{v:X2}"))}]}}";
    }
    [StructLayout(LayoutKind.Sequential)]
    internal readonly struct Buffer5{
        [MarshalAs(UnmanagedType.ByValArray, SizeConst =5)]
        readonly byte[] _Buffer;
        Buffer5(byte[] Buffer) => _Buffer = Buffer;
        public static implicit operator byte[](Buffer5 buffer) => (buffer._Buffer ?? Empty<byte>()).Concat(Repeat<byte>(0, 5)).Take(5).ToArray();
        public static implicit operator Buffer5(byte[] buffer) => new Buffer5((buffer ?? Empty<byte>()).Concat(Repeat<byte>(0, 5)).Take(5).ToArray());
    }
}
