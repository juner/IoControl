using static System.Linq.Enumerable;
using System.Runtime.InteropServices;

namespace IoControl.Controller
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct TaskFile
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        readonly byte[] _Previous;
        public byte[] Previous => (_Previous ?? Empty<byte>()).Concat(Repeat<byte>(0, 8)).Take(8).ToArray();
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        readonly byte[] _Current;
        public byte[] Current => (_Current ?? Empty<byte>()).Concat(Repeat<byte>(0, 8)).Take(8).ToArray();
        public TaskFile(AtaFlags AtaFlags, ushort Feature = default, ushort SectorCouont = default, ushort SectorNumber = default, uint Cylinder = default, byte DeviceHead = default, byte Command = default, ushort Reserved = default)
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
