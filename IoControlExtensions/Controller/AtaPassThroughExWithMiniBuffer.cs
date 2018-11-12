﻿using static System.Linq.Enumerable;
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
            => $"{nameof(AtaPassThroughExWithMiniBuffer)}{{{nameof(Length)}:{Length}, {nameof(AtaFlags)}:{AtaFlags}, {nameof(PathId)}:{PathId}, {nameof(TargetId)}:{TargetId}, {nameof(Lun)}:{Lun}, {nameof(ReservedAsUchar)}:{ReservedAsUchar}, {nameof(DataTransferLength)}:{DataTransferLength}, {nameof(TimeOutValue)}:{TimeOutValue}, {nameof(ReservedAsUlong)}:{ReservedAsUlong}, {nameof(DataBufferOffset)}:{DataBufferOffset}, {nameof(PreviousTaskFile)}:[{string.Join(" ", PreviousTaskFile.Select(v => $"{v:X2}"))}], {nameof(_CurrentTaskFile)}:[{string.Join(" ", CurrentTaskFile.Select(v => $"{v:X2}"))}]}}";
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
            => $"{nameof(AtaPassThroughEx32WithMiniBuffer)}{{{nameof(Length)}:{Length}, {nameof(AtaFlags)}:{AtaFlags}, {nameof(PathId)}:{PathId}, {nameof(TargetId)}:{TargetId}, {nameof(Lun)}:{Lun}, {nameof(ReservedAsUchar)}:{ReservedAsUchar}, {nameof(DataTransferLength)}:{DataTransferLength}, {nameof(TimeOutValue)}:{TimeOutValue}, {nameof(ReservedAsUlong)}:{ReservedAsUlong}, {nameof(DataBufferOffset)}:{DataBufferOffset}, {nameof(PreviousTaskFile)}:[{string.Join(" ", PreviousTaskFile.Select(v => $"{v:X2}"))}], {nameof(_CurrentTaskFile)}:[{string.Join(" ", CurrentTaskFile.Select(v => $"{v:X2}"))}]}}";
    }
    [StructLayout(LayoutKind.Sequential)]
    internal readonly struct Buffer5{
        [MarshalAs(UnmanagedType.ByValArray, SizeConst =5)]
        readonly byte[] _Buffer;
        Buffer5(byte[] Buffer) => _Buffer = Buffer;
        public static implicit operator byte[](Buffer5 buffer) => (buffer._Buffer ?? Empty<byte>()).Concat(Repeat<byte>(0, 5)).Take(5).ToArray();
        public static implicit operator Buffer5(byte[] buffer) => new Buffer5((buffer ?? Empty<byte>()).Concat(Repeat<byte>(0, 5)).Take(5).ToArray());
    }
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct TaskFile
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        readonly byte[] _Previous;
        public byte[] Previous => (_Previous ?? Empty<byte>()).Concat(Repeat<byte>(0, 8)).Take(8).ToArray();
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        readonly byte[] _Current;
        public byte[] Current => (_Current ?? Empty<byte>()).Concat(Repeat<byte>(0, 8)).Take(8).ToArray();
        public TaskFile(AtaFlags AtaFlags, ushort Feature, ushort SectorCouont, ushort SectorNumber, uint Cylinder, byte DeviceHead, byte Command, ushort Reserved)
        {
            if ((AtaFlags & AtaFlags.AF_48BIT_COMMAND) == AtaFlags.AF_48BIT_COMMAND)
            {
                unchecked
                {
                    (_Current, _Previous)
                        = (new byte[8] {
                            (byte)(Feature & 0x00FF),
                            (byte)(SectorCouont & 0x00FF),
                            (byte)(SectorNumber & 0x00FF),
                            (byte)(Cylinder & 0x0000_00FF),
                            (byte)((Cylinder & 0x0000_FF00) >> 8),
                            DeviceHead,
                            Command,
                            (byte)(Reserved & 0x00FF),
                        }, new byte[8] {
                            (byte)((Feature & 0xFF00) >> 8),
                            (byte)((SectorCouont & 0xFF00) >> 8),
                            (byte)((SectorNumber & 0xFF00) >> 8),
                            (byte)((Cylinder & 0x00FF_0000) >> 16),
                            (byte)((Cylinder & 0xFF00_0000) >> 24),
                            DeviceHead,
                            Command,
                            (byte)(Reserved & 0x00FF),
                        });
                    return;
                }
            }
            else
            {
                unchecked
                {
                    (_Current, _Previous)
                        = (new byte[8] {
                            (byte)(Feature & 0x00FF),
                            (byte)(SectorCouont & 0x00FF),
                            (byte)(SectorNumber & 0x00FF),
                            (byte)(Cylinder & 0x0000_00FF),
                            (byte)((Cylinder & 0x0000_FF00) >> 8),
                            DeviceHead,
                            Command,
                            (byte)(Reserved & 0x00FF),
                        }, new byte[8]);
                }
            }
        }
        public TaskFile(AtaFlags AtaFlags, ushort Feature, ushort SectorCouont, ulong Lba, byte DeviceHead, byte Command, ushort Reserved)
        {
            if ((AtaFlags & AtaFlags.AF_48BIT_COMMAND) == AtaFlags.AF_48BIT_COMMAND)
            {
                unchecked
                {
                    (_Current, _Previous)
                        = (new byte[8] {
                            (byte)(Feature & 0x00FF),
                            (byte)(SectorCouont & 0x00FF),
                            (byte)(Lba & 0x0000_0000_0000_00FF),
                            (byte)((Lba & 0x0000_0000_0000_FF00) >> 8),
                            (byte)((Lba & 0x0000_0000_00FF_0000) >> 16),
                            DeviceHead,
                            Command,
                            (byte)(Reserved & 0x00FF),
                        }, new byte[8] {
                            (byte)((Feature & 0xFF00) >> 8),
                            (byte)((SectorCouont & 0xFF00) >> 8),
                            (byte)((Lba & 0x0000_0000_FF00_0000) >> 24),
                            (byte)((Lba & 0x0000_00FF_0000_0000) >> 32),
                            (byte)((Lba & 0x0000_FF00_0000_0000) >> 40),
                            DeviceHead,
                            Command,
                            (byte)((Reserved & 0xFF00) >> 8),
                        });
                }
            }
            else
            {
                unchecked
                {
                    (_Current, _Previous)
                        = (new byte[8] {
                        (byte)(Feature & 0x00FF),
                        (byte)(SectorCouont & 0x00FF),
                        (byte)(Lba & 0x00FF),
                        (byte)((Lba & 0x0000_FF00) >> 8),
                        (byte)((Lba & 0x00FF_0000) >> 16),
                        DeviceHead,
                        Command,
                        (byte)(Reserved & 0x00FF),
                    }, new byte[8]);
                }
            }
        }
    }
}
}
