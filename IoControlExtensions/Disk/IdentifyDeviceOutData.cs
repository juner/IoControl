using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    /// <summary>
    /// SENDCMDOUTPARAMS structure
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ns-ntdddisk-_sendcmdoutparams
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct IdentifyDeviceOutData : ISendcmdoutparams
    {
        public readonly uint BufferSize;
        uint ISendcmdoutparams.BufferSize => BufferSize;
        public readonly Driverstatus DriverStatus;
        Driverstatus ISendcmdoutparams.DriverStatus => DriverStatus;
        public readonly Controller.IdentifyDevice Buffer;
        byte[] ISendcmdoutparams.Buffer => Buffer.B;
        public IdentifyDeviceOutData(uint BufferSize = DiskExtensions.IDENTIFY_BUFFER_SIZE, Driverstatus DriverStatus = default, Controller.IdentifyDevice Buffer = default)
            => (this.BufferSize, this.DriverStatus, this.Buffer)
            = (BufferSize, DriverStatus, Buffer);
        public IdentifyDeviceOutData Set(uint? BufferSize = null, Driverstatus? DriverStatus = null, Controller.IdentifyDevice? Buffer = null)
            => BufferSize == null && DriverStatus == null && Buffer == null ? this
            : new IdentifyDeviceOutData(BufferSize ?? this.BufferSize, DriverStatus ?? this.DriverStatus, Buffer ?? this.Buffer);
        public static implicit operator Controller.IdentifyDevice(IdentifyDeviceOutData data) => data.Buffer;
        public override string ToString()
            => $"{nameof(IdentifyDeviceOutData)}{{"
            + $"{nameof(BufferSize)}: {BufferSize}"
            + $", {nameof(DriverStatus)}: {DriverStatus}"
            + $", {nameof(Buffer)}: {Buffer}"
            + $"}}";
    }
}
