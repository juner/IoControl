using System.Runtime.InteropServices;
using static System.Linq.Enumerable;

namespace IoControl.Disk
{
    /// <summary>
    /// SENDCMDOUTPARAMS structure
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ns-ntdddisk-_sendcmdoutparams
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct SmartReadDataOutData : ISendcmdoutparams
    {
        public readonly uint BufferSize;
        uint ISendcmdoutparams.BufferSize => BufferSize;
        public readonly Driverstatus DriverStatus;
        Driverstatus ISendcmdoutparams.DriverStatus => DriverStatus;
        public readonly Controller.SmartData Buffer;
        byte[] ISendcmdoutparams.Buffer {
            get {
                var bytes = new byte[Marshal.SizeOf<Controller.SmartData>()];
                var IntPtr = Marshal.AllocCoTaskMem(bytes.Length);
                using (Disposable.Create(() => Marshal.FreeCoTaskMem(IntPtr)))
                {
                    Marshal.StructureToPtr(Buffer, IntPtr, false);
                    Marshal.Copy(IntPtr, bytes, 0, bytes.Length);
                    return bytes;
                }
            }
        }
        public SmartReadDataOutData(uint BufferSize = DiskExtensions.IDENTIFY_BUFFER_SIZE, Driverstatus DriverStatus = default, Controller.SmartData Buffer = default)
            => (this.BufferSize, this.DriverStatus, this.Buffer)
            = (BufferSize, DriverStatus, Buffer);
        public SmartReadDataOutData Set(uint? BufferSize = null, Driverstatus? DriverStatus = null, Controller.SmartData? Buffer = null)
            => BufferSize == null && DriverStatus == null && Buffer == null ? this
            : new SmartReadDataOutData(BufferSize ?? this.BufferSize, DriverStatus ?? this.DriverStatus, Buffer ?? this.Buffer);
        public static implicit operator Controller.SmartAttribute[](SmartReadDataOutData data) => data.Buffer.Attributes;
        public override string ToString()
            => $"{nameof(SmartReadDataOutData)}{{"
            + $"{nameof(BufferSize)}: {BufferSize}"
            + $", {nameof(DriverStatus)}: {DriverStatus}"
            + $", {nameof(Buffer)}: {Buffer}"
            + $"}}";
    }
}
