using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    public interface ISendcmdinparams {
        uint BufferSize { get; }
        Ideregs DriveRegs { get; }
        byte DriveNumber { get; }
        byte[] Reserved1 { get; }
        uint[] Reserved2 { get; }
        byte[] Buffer { get; }

    }
    /// <summary>
    /// SENDCMDINPARAMS structure 
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ns-ntdddisk-_sendcmdinparams
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Sendcmdinparams : DataUtils.IPtrCreatable , ISendcmdinparams
    {
        public readonly uint BufferSize;
        uint ISendcmdinparams.BufferSize => BufferSize;
        public readonly Ideregs DriveRegs;
        Ideregs ISendcmdinparams.DriveRegs => DriveRegs;
        public readonly byte DriveNumber;
        byte ISendcmdinparams.DriveNumber => DriveNumber;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst =3)]
        readonly byte[] _Reserved1;
        public byte[] Reserved1 => (_Reserved1 ?? Enumerable.Empty<byte>()).Concat(Enumerable.Repeat<byte>(0, 3)).Take(3).ToArray();
        [MarshalAs(UnmanagedType.ByValArray, SizeConst =4)]
        readonly uint[] _Reserved2;
        public uint[] Reserved2 => (_Reserved2 ?? Enumerable.Empty<uint>()).Concat(Enumerable.Repeat<uint>(0, 4)).Take(4).ToArray();
        [MarshalAs(UnmanagedType.ByValArray, SizeConst =1)]
        readonly byte[] _Buffer;
        public byte[] Buffer => (_Buffer ?? Enumerable.Empty<byte>()).Concat(Enumerable.Empty<byte>()).ToArray();
        public override string ToString()
            => $"{nameof(Sendcmdinparams)}{{"
            + $"{nameof(BufferSize)}: {BufferSize}"
            + $", {nameof(DriveRegs)}: {DriveRegs}"
            + $", {nameof(DriveNumber)}: {DriveNumber}"
            + $", {nameof(Reserved1)}: [{string.Join(" ", Reserved1.Select(v => $"{v:X2}"))}]"
            + $", {nameof(Reserved2)}: [{string.Join(" ", Reserved2.Select(v => $"{v:X8}"))}]"
            + $", {nameof(Buffer)}: [{string.Join(" ", Buffer.Select(v => $"{v:X2}"))}]"
            + $"}}";
        public Sendcmdinparams(uint BufferSize = default, Ideregs DriveRegs = default, byte DriveNumber = default, byte[] Reserved1 = null, uint[] Reserved2 = null, byte[] Buffer = null)
            => (this.BufferSize, this.DriveRegs, this.DriveNumber, _Reserved1, _Reserved2, _Buffer)
            = (BufferSize, DriveRegs, DriveNumber
                , (Reserved1 ?? Enumerable.Empty<byte>()).Concat(Enumerable.Repeat<byte>(0, 3)).Take(3).ToArray()
                , (Reserved2 ?? Enumerable.Empty<uint>()).Concat(Enumerable.Repeat(0u,4)).Take(4).ToArray()
                , Buffer ?? Enumerable.Repeat<byte>(0,1).ToArray());
        public Sendcmdinparams Set(uint? BufferSize = null, Ideregs? DriveRegs = null, byte? DriveNumber = null, byte[] Reserved1 = null, uint[] Reserved2 = null, byte[] Buffer = null)
            => BufferSize == null && DriveRegs == null && DriveNumber == null && Reserved1 == null && Reserved2 == null && Buffer == null ? this
            : new Sendcmdinparams(BufferSize ?? this.BufferSize, DriveRegs ?? this.DriveRegs, DriveNumber ?? this.DriveNumber, Reserved1 ?? _Reserved1, Reserved2 ?? _Reserved2, Buffer ?? _Buffer);
        public Sendcmdinparams(IntPtr IntPtr, uint Size)
        {
            var _Size = Marshal.SizeOf<Sendcmdinparams>();
            this = (Sendcmdinparams)Marshal.PtrToStructure(IntPtr, typeof(Sendcmdinparams));
            if (Size > _Size)
            {
                var BufferOffset = Marshal.OffsetOf<Sendcmdinparams>(nameof(_Buffer));
                var BufferSize = (int)Size - _Size + 1;
                Marshal.Copy(_Buffer = new byte[BufferSize], 0, IntPtr.Add(IntPtr, (int)BufferOffset), BufferSize);
            }
        }
        public IDisposable CreatePtr(out IntPtr Ptr, out uint Size)
        {
            var _Size = Marshal.SizeOf<Sendcmdinparams>();
            var BufferLength = Buffer.Length;
            _Size += BufferLength - 1;
            var _IntPtr = Marshal.AllocCoTaskMem(_Size);
            var _Disposable = Disposable.Create(() => Marshal.FreeCoTaskMem(_IntPtr));
            try
            {
                Marshal.StructureToPtr(this, _IntPtr, false);
                if (BufferLength > 1)
                {
                    var BufferOffset = Marshal.OffsetOf<Sendcmdinparams>(nameof(_Buffer));
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
