using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace IoControl.Controller
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly struct AtaPassThroughEx
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
        public readonly IntPtr DataBufferOffset;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public readonly byte[] PreviousTaskFile;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public readonly byte[] CurrentTaskFile;
        public AtaPassThroughEx(AtaFlags AtaFlags = default, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, uint DataTransferLength =default, IntPtr DataBufferOffset = default, ushort Feature = default, ushort SectorCouont = default,ushort SectorNumber = default, uint Cylinder = default, byte DeviceHead = default, byte Command = default, ushort Reserved = default)
        {
            Length = (ushort)Marshal.SizeOf<AtaPassThroughEx>();
            this.AtaFlags = AtaFlags;
            this.PathId = PathId;
            this.TargetId = TargetId;
            this.Lun = Lun;
            this.ReservedAsUchar = ReservedAsUchar;
            this.TimeOutValue = TimeOutValue;
            this.ReservedAsUlong = ReservedAsUlong;
            this.DataTransferLength = DataTransferLength;
            this.DataBufferOffset = DataBufferOffset;
            if ((this.AtaFlags & AtaFlags.AF_48BIT_COMMAND) == AtaFlags.AF_48BIT_COMMAND)
            {
                CurrentTaskFile = new byte[8] {
                        (byte)(Feature & 0x00FF),
                        (byte)(SectorCouont & 0x00FF),
                        (byte)(SectorNumber & 0x00FF),
                        (byte)(Cylinder & 0x0000_00FF),
                        (byte)((Cylinder & 0x0000_FF00) >> 8),
                        DeviceHead,
                        Command,
                        (byte)(Reserved & 0x00FF),
                    };
                PreviousTaskFile = new byte[8] {
                        (byte)((Feature & 0xFF00) >> 8),
                        (byte)((SectorCouont & 0xFF00) >> 8),
                        (byte)((SectorNumber & 0xFF00) >> 8),
                        (byte)((Cylinder & 0x00FF_0000) >> 16),
                        (byte)((Cylinder & 0xFF00_0000) >> 24),
                        DeviceHead,
                        Command,
                        (byte)(Reserved & 0x00FF),
                };
            }
            else
            {
                unchecked
                {
                    CurrentTaskFile = new byte[8] {
                        (byte)(Feature & 0x00FF),
                        (byte)(SectorCouont & 0x00FF),
                        (byte)(SectorNumber & 0x00FF),
                        (byte)(Cylinder & 0x0000_00FF),
                        (byte)((Cylinder & 0x0000_FF00) >> 8),
                        DeviceHead,
                        Command,
                        (byte)(Reserved & 0x00FF),
                    };
                    PreviousTaskFile = new byte[8];
                }
            }
        }
        public AtaPassThroughEx(AtaFlags AtaFlags = default, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, uint DataTransferLength = default, IntPtr DataBufferOffset = default, ushort Feature = default, ushort SectorCouont = default, ulong Lba = default,byte DeviceHead = default, byte Command = default, ushort Reserved = default)
        {
            Length = (ushort)Marshal.SizeOf<AtaPassThroughEx>();
            this.AtaFlags = AtaFlags;
            this.PathId = PathId;
            this.TargetId = TargetId;
            this.Lun = Lun;
            this.ReservedAsUchar = ReservedAsUchar;
            this.TimeOutValue = TimeOutValue;
            this.ReservedAsUlong = ReservedAsUlong;
            this.DataTransferLength = DataTransferLength;
            this.DataBufferOffset = DataBufferOffset;
            if ((this.AtaFlags & AtaFlags.AF_48BIT_COMMAND) == AtaFlags.AF_48BIT_COMMAND)
            {
                unchecked
                {

                    CurrentTaskFile = new byte[8] {
                        (byte)(Feature & 0x00FF),
                        (byte)(SectorCouont & 0x00FF),
                        (byte)(Lba & 0x0000_0000_0000_00FF),
                        (byte)((Lba & 0x0000_0000_0000_FF00) >> 8),
                        (byte)((Lba & 0x0000_0000_00FF_0000) >> 16),
                        DeviceHead,
                        Command,
                        (byte)(Reserved & 0x00FF),
                    };
                    PreviousTaskFile = new byte[8] {
                        (byte)((Feature & 0xFF00) >> 8),
                        (byte)((SectorCouont & 0xFF00) >> 8),
                        (byte)((Lba & 0x0000_0000_FF00_0000) >> 24),
                        (byte)((Lba & 0x0000_00FF_0000_0000) >> 32),
                        (byte)((Lba & 0x0000_FF00_0000_0000) >> 40),
                        DeviceHead,
                        Command,
                        (byte)((Reserved & 0xFF00) >> 8),
                    };
                }
            }
            else
            {
                unchecked
                {
                    CurrentTaskFile = new byte[8] {
                        (byte)(Feature & 0x00FF),
                        (byte)(SectorCouont & 0x00FF),
                        (byte)(Lba & 0x00FF),
                        (byte)((Lba & 0x0000_FF00) >> 8),
                        (byte)((Lba & 0x00FF_0000) >> 16),
                        DeviceHead,
                        Command,
                        (byte)(Reserved & 0x00FF),
                    };
                    PreviousTaskFile = new byte[8];
                }
            }
        }
        public override string ToString()
            => $"{nameof(AtaPassThroughEx)}{{{nameof(Length)}:{Length}, {nameof(AtaFlags)}:{AtaFlags}, {nameof(PathId)}:{PathId}, {nameof(TargetId)}:{TargetId}, {nameof(Lun)}:{Lun}, {nameof(ReservedAsUchar)}:{ReservedAsUchar}, {nameof(DataTransferLength)}:{DataTransferLength}, {nameof(TimeOutValue)}:{TimeOutValue}, {nameof(ReservedAsUlong)}:{ReservedAsUlong}, {nameof(DataBufferOffset)}:{DataBufferOffset}, {nameof(PreviousTaskFile)}:[{string.Join(" ", (PreviousTaskFile ?? Enumerable.Empty<byte>()).Select(v => $"{v:X2}"))}], {nameof(CurrentTaskFile)}:[{string.Join(" ", (CurrentTaskFile ?? Enumerable.Empty<byte>()).Select(v => $"{v:X2}"))}]}}";
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal readonly struct AtaIdentifyDeviceQuery
    {
        public readonly AtaPassThroughEx Header;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public readonly byte[] Data;
        public AtaIdentifyDeviceQuery(AtaPassThroughEx Header, byte[] Data) => (this.Header, this.Data) = (Header, Data);
        public AtaIdentifyDeviceQuery(AtaFlags AtaFlags = default, byte PathId = default, byte TargetId = default, byte Lun = default, byte ReservedAsUchar = default, uint TimeOutValue = default, uint ReservedAsUlong = default, IntPtr DataBufferOffset = default, ushort Feature = default, ushort SectorCouont = default, ushort SectorNumber = default, uint Cylinder = default, byte DeviceHead = default, byte Command = default, ushort Reserved = default, byte[] Data = default)
        {
            this.Data = Data ?? new byte[0];
            Header = new AtaPassThroughEx(
                    AtaFlags: AtaFlags,
                    PathId: PathId,
                    TargetId: TargetId,
                    Lun: Lun,
                    ReservedAsUchar: ReservedAsUchar,
                    TimeOutValue: TimeOutValue,
                    ReservedAsUlong: ReservedAsUlong,
                    DataTransferLength: Data == null ? 0 : (uint)Data.Length,
                    DataBufferOffset: Data == null ? IntPtr.Zero : Marshal.OffsetOf<AtaIdentifyDeviceQuery>(nameof(Data)),
                    Feature: Feature,
                    SectorCouont: SectorCouont,
                    SectorNumber: SectorNumber,
                    Cylinder: Cylinder,
                    DeviceHead: DeviceHead,
                    Command: Command,
                    Reserved: Reserved
                );
        }
        public override string ToString()
            => $"{nameof(AtaIdentifyDeviceQuery)}{{ {nameof(Header)}:{Header}, {nameof(Data)}:[{string.Join(" ", (Data ?? Enumerable.Empty<byte>()).Select(v => $"{v:X2}"))}] }}";
    }
}
