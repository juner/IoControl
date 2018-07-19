using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace IoControl
{
    public partial class IoControl : IDisposable
    {
        readonly SafeFileHandle Handle;
        readonly bool Diposable;
        public bool IsInvalid => Handle.IsInvalid;
        public bool IsClosed => Handle.IsClosed;
        public IoControl(SafeFileHandle Handle, bool Diposable = false)
            => (this.Handle, this.Diposable) = (Handle, Diposable);
        public IoControl(string Filename, FileAccess FileAccess = default, FileShare FileShare = default, FileMode CreationDisposition = default, FileAttributes FlagsAndAttributes = default)
            : this(CreateFile(Filename, FileAccess, FileShare, CreationDisposition, FlagsAndAttributes), true) { }
        public void Dispose()
        {
            if (Diposable && !(Handle?.IsClosed ?? true))
                Handle.Dispose();
        }
        class NativeMethod
        {
            [DllImport("kernel32.dll", SetLastError = true,
                CallingConvention = CallingConvention.StdCall,
                CharSet = CharSet.Auto)]
            public static extern SafeFileHandle CreateFile(
                 [MarshalAs(UnmanagedType.LPTStr)] string filename,
                 [MarshalAs(UnmanagedType.U4)] FileAccess access,
                 [MarshalAs(UnmanagedType.U4)] FileShare share,
                 IntPtr securityAttributes,
                 [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
                 [MarshalAs(UnmanagedType.U4)] FileAttributes flagsAndAttributes,
                 SafeFileHandle templateFile);

            [DllImport("kernel32.dll", SetLastError = true,
                CallingConvention = CallingConvention.StdCall,
                CharSet = CharSet.Auto)]
            public static extern SafeFileHandle CreateFile(
                 [MarshalAs(UnmanagedType.LPTStr)] string filename,
                 [MarshalAs(UnmanagedType.U4)] FileAccess access,
                 [MarshalAs(UnmanagedType.U4)] FileShare share,
                 IntPtr securityAttributes,
                 [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
                 [MarshalAs(UnmanagedType.U4)] FileAttributes flagsAndAttributes,
                 IntPtr templateFile);

            [DllImport("kernel32.dll", SetLastError = true,
                CharSet = CharSet.Auto)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool DeviceIoControl(
                SafeFileHandle hDevice,
                IOControlCode IoControlCode,
                IntPtr InBuffer,
                uint nInBufferSize,
                IntPtr OutBuffer,
                uint nOutBufferSize,
                out uint pBytesReturned,
                IntPtr Overlapped = default
            );
            [DllImport("kernel32.dll", SetLastError = true,
                CharSet = CharSet.Auto)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool DeviceIoControl(
                SafeFileHandle hDevice,
                IOControlCode IoControlCode,
                IntPtr InBuffer,
                uint nInBufferSize,
                IntPtr OutBuffer,
                uint nOutBufferSize,
                IntPtr pBytesReturned = default,
                IntPtr Overlapped = default
            );
        }

        public bool DeviceIoControl(IOControlCode IoControlCode, IntPtr InputPtr, uint InputSize, IntPtr OutputPtr, uint OutputSize, out uint ReturnBytes)
            => NativeMethod.DeviceIoControl(Handle, IoControlCode, InputPtr, InputSize, OutputPtr, OutputSize, out ReturnBytes);
        public bool DeviceIoControl(IOControlCode IoControlCode, IntPtr InputPtr, uint InputSize, IntPtr OutputPtr, uint OutputSize)
            => NativeMethod.DeviceIoControl(Handle, IoControlCode, InputPtr, InputSize, OutputPtr, OutputSize);

        public bool DeviceIoControl<TINOUT>(IOControlCode IoControlCode, ref TINOUT InOutBuffer, out uint ReturnBytes)
            where TINOUT : struct
        {
            var inoutSize = (uint)Marshal.SizeOf(typeof(TINOUT));
            var inoutPtr = Marshal.AllocCoTaskMem((int)inoutSize);
            using (Disposable.Create(()=>Marshal.FreeCoTaskMem(inoutPtr)))
            {
                Marshal.StructureToPtr(InOutBuffer, inoutPtr, false);
                var result = DeviceIoControl(IoControlCode, inoutPtr, inoutSize, inoutPtr, inoutSize, out ReturnBytes);
                InOutBuffer = (TINOUT)Marshal.PtrToStructure(inoutPtr, typeof(TINOUT));
                return result;
            }
        }
        public bool DeviceIoControl(IOControlCode IoControlCode, byte[] InOutBuffer, out uint ReturnBytes)
        {
            var inoutSize = (uint)InOutBuffer.Length * sizeof(byte);
            var iogch = GCHandle.Alloc(InOutBuffer, GCHandleType.Pinned);
            using (Disposable.Create(iogch.Free))
            {
                var inoutPtr = iogch.AddrOfPinnedObject();
                var result = DeviceIoControl(IoControlCode, inoutPtr, inoutSize, inoutPtr, inoutSize, out ReturnBytes);
                return result;
            }
        }
        public bool DeviceIoControl<TIN, TOUT>(IOControlCode IoControlCode, ref TIN InBuffer, out TOUT OutBuffer, out uint ReturnBytes)
            where TIN : struct
            where TOUT : struct
        {
            var inSize = (uint)Marshal.SizeOf(typeof(TIN));
            var _inBuffer = new byte[inSize];
            var igch = GCHandle.Alloc(_inBuffer, GCHandleType.Pinned);
            var outSize = (uint)Marshal.SizeOf(typeof(TOUT));
            var _outBuffer = new byte[outSize];
            var ogch = GCHandle.Alloc(_outBuffer, GCHandleType.Pinned);
            using (Disposable.Create(igch.Free))
            using (Disposable.Create(ogch.Free))
            {
                var inPtr = igch.AddrOfPinnedObject();
                var outPtr = ogch.AddrOfPinnedObject();
                Marshal.StructureToPtr(InBuffer, inPtr, false);
                var result = DeviceIoControl(IoControlCode, inPtr, inSize, outPtr, outSize, out ReturnBytes);
                InBuffer = (TIN)Marshal.PtrToStructure(inPtr, typeof(TIN));
                OutBuffer = (TOUT)Marshal.PtrToStructure(outPtr, typeof(TOUT));
                return result;
            }
        }
        public bool DeviceIoControl(IOControlCode dwIoControlCode, out uint ReturnBytes)
            => DeviceIoControl(dwIoControlCode, IntPtr.Zero, 0, IntPtr.Zero, 0, out ReturnBytes);
        public bool DeviceIoControlInOnly<TIN>(IOControlCode dwIoControlCode, in TIN InBuffer, out uint ReturnBytes)
            where TIN : struct
        {
            var inSize = (uint)Marshal.SizeOf(typeof(TIN));
            var inPtr = Marshal.AllocCoTaskMem((int)inSize);
            using (Disposable.Create(() => Marshal.FreeCoTaskMem(inPtr)))
            {
                Marshal.StructureToPtr(InBuffer, inPtr, false);
                var result = DeviceIoControl(dwIoControlCode, inPtr, inSize, IntPtr.Zero, 0u, out ReturnBytes);
                return result;
            }
        }
        public bool DeviceIoControlOutOnly(IOControlCode IoControlCode, IntPtr OutputPtr, uint OutputSize, out uint ReturnBytes)
            => DeviceIoControl(IoControlCode, IntPtr.Zero, 0, OutputPtr, OutputSize, out ReturnBytes);
        public bool DeviceIoControlOutOnly<TOUT>(IOControlCode IoControlCode, out TOUT OutBuffer, out uint ReturnBytes)
            where TOUT : struct
        {
            var outSize = (uint)Marshal.SizeOf(typeof(TOUT));
            var outPtr = Marshal.AllocCoTaskMem((int)outSize);
            using (Disposable.Create(() => Marshal.FreeCoTaskMem(outPtr)))
            {
                bool result = DeviceIoControlOutOnly(IoControlCode, outPtr, outSize, out ReturnBytes);
                OutBuffer = (TOUT)Marshal.PtrToStructure(outPtr, typeof(TOUT));
                return result;
            }
        }
        public bool DeviceIoControlOutOnly(IOControlCode IoControlCode, byte[] OutBuffer, out uint ReturnBytes)
        {
            var outSize = (uint)OutBuffer.Length * sizeof(byte);
            var ogch = GCHandle.Alloc(OutBuffer, GCHandleType.Pinned);
            using (Disposable.Create(ogch.Free))
            {
                var outPtr = ogch.AddrOfPinnedObject();
                var result = DeviceIoControlOutOnly(IoControlCode, outPtr, outSize, out ReturnBytes);
                return result;
            }
        }
        internal static SafeFileHandle CreateFile(string Filename, FileAccess FileAccess = default, FileShare FileShare = default, FileMode CreationDisposition = default, FileAttributes FlagsAndAttributes = default, SafeFileHandle TemplateFile = default)
            => NativeMethod.CreateFile(Filename, FileAccess, FileShare, IntPtr.Zero, CreationDisposition, FlagsAndAttributes, TemplateFile);
        internal static SafeFileHandle CreateFile(string Filename, FileAccess FileAccess = default, FileShare FileShare = default, FileMode CreationDisposition = default, FileAttributes FlagsAndAttributes = default)
            => NativeMethod.CreateFile(Filename, FileAccess, FileShare, IntPtr.Zero, CreationDisposition, FlagsAndAttributes, IntPtr.Zero);
    }
}
