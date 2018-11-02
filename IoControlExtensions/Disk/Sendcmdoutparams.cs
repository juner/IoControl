using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    /// <summary>
    /// SENDCMDOUTPARAMS structure
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ns-ntdddisk-_sendcmdoutparams
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Sendcmdoutparams
    {
        public readonly uint BufferSize;
        public readonly Driverstatus DriverStatus;
        [MarshalAs(UnmanagedType.ByValArray,SizeConst = 1)]
        internal readonly byte[] _Buffer;
        public byte[] Buffer => (_Buffer ?? Enumerable.Empty<byte>()).Concat(Enumerable.Empty<byte>()).ToArray();
        public Sendcmdoutparams(uint BufferSize = DiskExtensions.IDENTIFY_BUFFER_SIZE, Driverstatus DriverStatus = default, byte[] Buffer = null)
            => (this.BufferSize, this.DriverStatus, _Buffer)
            = (BufferSize, DriverStatus
                , (Buffer ?? Enumerable.Repeat<byte>(0, 1)).Concat(Enumerable.Empty<byte>()).ToArray());
        public Sendcmdoutparams Set(uint? BufferSize = null, Driverstatus? DriverStatus = null, byte[] Buffer = null)
            => BufferSize == null && DriverStatus == null && Buffer == null ? this
            : new Sendcmdoutparams(BufferSize ?? this.BufferSize, DriverStatus ?? this.DriverStatus, Buffer ?? _Buffer);
        public Sendcmdoutparams(IntPtr IntPtr, uint Size)
        {
            var _Size = Marshal.SizeOf<Sendcmdinparams>();
            this = (Sendcmdoutparams)Marshal.PtrToStructure(IntPtr, typeof(Sendcmdoutparams));
            if (Size > _Size)
            {
                var BufferOffset = Marshal.OffsetOf<Sendcmdoutparams>(nameof(_Buffer));
                var BufferSize = (int)Size - _Size + 1;
                Marshal.Copy(_Buffer = new byte[BufferSize], 0, IntPtr.Add(IntPtr, (int)BufferOffset), BufferSize);
            }
        }
        public IDisposable ToPtr(out IntPtr Ptr, out uint Size)
        {
            var _Size = Marshal.SizeOf<Sendcmdoutparams>();
            var BufferLength = Buffer.Length;
            _Size += BufferLength - 1;
            var _IntPtr = Marshal.AllocCoTaskMem(_Size);
            var _Disposable = Disposable.Create(() => Marshal.FreeCoTaskMem(_IntPtr));
            try
            {
                Marshal.StructureToPtr(this, _IntPtr, false);
                if (BufferLength > 1)
                {
                    var BufferOffset = Marshal.OffsetOf<Sendcmdoutparams>(nameof(_Buffer));
                    Marshal.Copy(_Buffer, 0, IntPtr.Add(_IntPtr, (int)BufferOffset), BufferLength);
                }
                Ptr = _IntPtr;
                Size = (uint)_Size;
            }
            catch
            {
                _Disposable.Dispose();
                throw;
            }
            return _Disposable;
        }
    }
}
