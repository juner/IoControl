namespace IoControl.Controller
{
    public static class AtaPassThroughUtils
    {
        public static (byte[] CurrentTaskFile, byte[] PreviousTaskFile) CreateTaskFiles(AtaFlags AtaFlags, ushort Feature, ushort SectorCouont, ushort SectorNumber, uint Cylinder, byte DeviceHead, byte Command, ushort Reserved)
        {
            if ((AtaFlags & AtaFlags.AF_48BIT_COMMAND) == AtaFlags.AF_48BIT_COMMAND)
            {
                unchecked
                {
                    return (CurrentTaskFile: new byte[8] {
                        (byte)(Feature & 0x00FF),
                        (byte)(SectorCouont & 0x00FF),
                        (byte)(SectorNumber & 0x00FF),
                        (byte)(Cylinder & 0x0000_00FF),
                        (byte)((Cylinder & 0x0000_FF00) >> 8),
                        DeviceHead,
                        Command,
                        (byte)(Reserved & 0x00FF),
                    }, PreviousTaskFile: new byte[8] {
                        (byte)((Feature & 0xFF00) >> 8),
                        (byte)((SectorCouont & 0xFF00) >> 8),
                        (byte)((SectorNumber & 0xFF00) >> 8),
                        (byte)((Cylinder & 0x00FF_0000) >> 16),
                        (byte)((Cylinder & 0xFF00_0000) >> 24),
                        DeviceHead,
                        Command,
                        (byte)(Reserved & 0x00FF),
                });
                }
            }
            else
            {
                unchecked
                {
                    return (CurrentTaskFile: new byte[8] {
                        (byte)(Feature & 0x00FF),
                        (byte)(SectorCouont & 0x00FF),
                        (byte)(SectorNumber & 0x00FF),
                        (byte)(Cylinder & 0x0000_00FF),
                        (byte)((Cylinder & 0x0000_FF00) >> 8),
                        DeviceHead,
                        Command,
                        (byte)(Reserved & 0x00FF),
                    }, PreviousTaskFile: new byte[8]);
                }
            }
        }
        public static (byte[] CurrentTaskFile, byte[] PreviousTaskFile) CreateTaskFiles(AtaFlags AtaFlags, ushort Feature, ushort SectorCouont, ulong Lba, byte DeviceHead, byte Command, ushort Reserved)
        {
            if ((AtaFlags & AtaFlags.AF_48BIT_COMMAND) == AtaFlags.AF_48BIT_COMMAND)
            {
                unchecked
                {
                    return (CurrentTaskFile: new byte[8] {
                        (byte)(Feature & 0x00FF),
                        (byte)(SectorCouont & 0x00FF),
                        (byte)(Lba & 0x0000_0000_0000_00FF),
                        (byte)((Lba & 0x0000_0000_0000_FF00) >> 8),
                        (byte)((Lba & 0x0000_0000_00FF_0000) >> 16),
                        DeviceHead,
                        Command,
                        (byte)(Reserved & 0x00FF),
                    }, PreviousTaskFile: new byte[8] {
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
                    return (CurrentTaskFile: new byte[8] {
                        (byte)(Feature & 0x00FF),
                        (byte)(SectorCouont & 0x00FF),
                        (byte)(Lba & 0x00FF),
                        (byte)((Lba & 0x0000_FF00) >> 8),
                        (byte)((Lba & 0x00FF_0000) >> 16),
                        DeviceHead,
                        Command,
                        (byte)(Reserved & 0x00FF),
                    }, PreviousTaskFile: new byte[8]);
                }
            }
        }
    }
}
