using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace IoControl
{
    public class IoControl : IDisposable
    {
        readonly SafeFileHandle Handle;
        readonly string Filename;
        readonly bool Disposable;
        public bool IsInvalid => Handle.IsInvalid;
        public bool IsClosed => Handle.IsClosed;
        public IoControl(SafeFileHandle Handle, bool Diposable = false, string Filename = null)
            => (this.Handle, this.Disposable, this.Filename) = (Handle, Diposable, Filename);
        public IoControl(string Filename, FileAccess FileAccess = default, FileShare FileShare = default, FileMode CreationDisposition = default, FileFlagAndAttributes FlagsAndAttributes = default)
            : this(CreateFile(Filename, FileAccess, FileShare, CreationDisposition, FlagsAndAttributes), true, Filename) { }
        public void Dispose()
        {
            if (Disposable && !(Handle?.IsClosed ?? true))
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
                 [MarshalAs(UnmanagedType.U4)] FileFlagAndAttributes flagsAndAttributes,
                 SafeFileHandle templateFile);

            [DllImport("kernel32.dll", SetLastError = true,
                CallingConvention = CallingConvention.StdCall,
                CharSet = CharSet.Unicode)]
            public static extern SafeFileHandle CreateFileW(
                 [MarshalAs(UnmanagedType.LPWStr)] string filename,
                 [MarshalAs(UnmanagedType.U4)] FileAccess access,
                 [MarshalAs(UnmanagedType.U4)] FileShare share,
                 IntPtr securityAttributes,
                 [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
                 [MarshalAs(UnmanagedType.U4)] FileFlagAndAttributes flagsAndAttributes,
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
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool GetOverlappedResult(
                  SafeFileHandle hFile,                       // ファイル、パイプ、通信デバイスのハンドル
                  DeviceIoOverlapped lpOverlapped,          // オーバーラップ構造体
                  out ushort lpNumberOfBytesTransferred, // 転送されたバイト数
                  bool bWait                          // 待機オプション
                );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IoControlCode"></param>
        /// <param name="InputPtr"></param>
        /// <param name="InputSize"></param>
        /// <param name="OutputPtr"></param>
        /// <param name="OutputSize"></param>
        /// <param name="ReturnBytes"></param>
        /// <returns></returns>
        public bool DeviceIoControl(IOControlCode IoControlCode, IntPtr InputPtr, uint InputSize, IntPtr OutputPtr, uint OutputSize, out uint ReturnBytes)
            => NativeMethod.DeviceIoControl(Handle, IoControlCode, InputPtr, InputSize, OutputPtr, OutputSize, out ReturnBytes);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IoControlCode"></param>
        /// <param name="InputPtr"></param>
        /// <param name="InputSize"></param>
        /// <param name="OutputPtr"></param>
        /// <param name="OutputSize"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public async Task<(bool Result,ushort ReturnBytes)> DeviceIoControlAsync(IOControlCode IoControlCode, IntPtr InputPtr, uint InputSize, IntPtr OutputPtr, uint OutputSize, CancellationToken Token = default)
        {
            using (var deviceIoOverlapped = new DeviceIoOverlapped())
            using (var hEvent = new ManualResetEvent(false))
            {
                deviceIoOverlapped.ClearAndSetEvent(hEvent.SafeWaitHandle.DangerousGetHandle());

                var result = NativeMethod.DeviceIoControl(Handle, IoControlCode, InputPtr, InputSize, OutputPtr, OutputSize, out var ret, deviceIoOverlapped.GlobalOverlapped);
                var hrCode = Marshal.GetHRForLastWin32Error();
                if (!result)
                    Marshal.ThrowExceptionForHR(hrCode);
                await hEvent.WaitOneAsync();

                result = NativeMethod.GetOverlappedResult(Handle, deviceIoOverlapped, out var ret2, false);
                hrCode = Marshal.GetHRForLastWin32Error();
                if (!result)
                    Marshal.ThrowExceptionForHR(hrCode);
                return (result, ret2);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IoControlCode"></param>
        /// <param name="InOutPtr"></param>
        /// <param name="InOutSize"></param>
        /// <param name="ReturnBytes"></param>
        /// <returns></returns>
        public bool DeviceIoControl(IOControlCode IoControlCode, IntPtr InOutPtr, uint InOutSize, out uint ReturnBytes)
            => NativeMethod.DeviceIoControl(Handle, IoControlCode, InOutPtr, InOutSize, InOutPtr, InOutSize, out ReturnBytes);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IoControlCode"></param>
        /// <param name="InOutPtr"></param>
        /// <param name="InOutSize"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public async Task<(bool Result, ushort ReturnBytes)> DeviceIoControlAsync(IOControlCode IoControlCode, IntPtr InOutPtr, uint InOutSize, CancellationToken Token = default)
        {
            using (var deviceIoOverlapped = new DeviceIoOverlapped())
            using (var hEvent = new ManualResetEvent(false))
            {
                deviceIoOverlapped.ClearAndSetEvent(hEvent.SafeWaitHandle.DangerousGetHandle());

                var result = NativeMethod.DeviceIoControl(Handle, IoControlCode, InOutPtr, InOutSize, InOutPtr, InOutSize, out var ret, deviceIoOverlapped.GlobalOverlapped);
                var hrCode = Marshal.GetHRForLastWin32Error();
                if (!result)
                    Marshal.ThrowExceptionForHR(hrCode);
                await hEvent.WaitOneAsync();

                result = NativeMethod.GetOverlappedResult(Handle, deviceIoOverlapped, out var ret2, false);
                hrCode = Marshal.GetHRForLastWin32Error();
                if (!result)
                    Marshal.ThrowExceptionForHR(hrCode);
                return (result, ret2);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TINOUT"></typeparam>
        /// <param name="IoControlCode"></param>
        /// <param name="InOutBuffer"></param>
        /// <param name="ReturnBytes"></param>
        /// <returns></returns>
        public bool DeviceIoControl<TINOUT>(IOControlCode IoControlCode, ref TINOUT InOutBuffer, out uint ReturnBytes)
            where TINOUT : struct
        {
            var inoutSize = (uint)Marshal.SizeOf(typeof(TINOUT));
            var inoutPtr = Marshal.AllocCoTaskMem((int)inoutSize);
            using (global::IoControl.Disposable.Create(()=> Marshal.FreeCoTaskMem(inoutPtr)))
            {
                Marshal.StructureToPtr(InOutBuffer, inoutPtr, false);
                var result = DeviceIoControl(IoControlCode, inoutPtr, inoutSize, out ReturnBytes);
                InOutBuffer = (TINOUT)Marshal.PtrToStructure(inoutPtr, typeof(TINOUT));
                return result;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TINOUT"></typeparam>
        /// <param name="IoControlCode"></param>
        /// <param name="InBuffer"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public async Task<(bool Result, TINOUT OutBuffer)> DeviceIoControlAsync<TINOUT>(IOControlCode IoControlCode, TINOUT InBuffer, CancellationToken Token = default)
            where TINOUT: struct
        {
            var inoutSize = (uint)Marshal.SizeOf(typeof(TINOUT));
            var inoutPtr = Marshal.AllocCoTaskMem((int)inoutSize);
            using (global::IoControl.Disposable.Create(() => Marshal.FreeCoTaskMem(inoutPtr)))
            {
                Marshal.StructureToPtr(InBuffer, inoutPtr, false);
                var (result,ReturnBytes) = await DeviceIoControlAsync(IoControlCode, inoutPtr, inoutSize, Token);
                return (result, (TINOUT)Marshal.PtrToStructure(inoutPtr, typeof(TINOUT)));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IoControlCode"></param>
        /// <param name="InOutBuffer"></param>
        /// <param name="ReturnBytes"></param>
        /// <returns></returns>
        public bool DeviceIoControl(IOControlCode IoControlCode, byte[] InOutBuffer, out uint ReturnBytes)
        {
            var inoutSize = (uint)InOutBuffer.Length * sizeof(byte);
            var iogch = GCHandle.Alloc(InOutBuffer, GCHandleType.Pinned);
            using (global::IoControl.Disposable.Create(iogch.Free))
            {
                var inoutPtr = iogch.AddrOfPinnedObject();
                return DeviceIoControl(IoControlCode, inoutPtr, inoutSize, out ReturnBytes);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IoControlCode"></param>
        /// <param name="InOutBuffer"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public async Task<(bool Result, ushort ReturnBytes)> DeviceIoControlAsync(IOControlCode IoControlCode, byte[] InOutBuffer, CancellationToken Token = default)
        {
            var inoutSize = (uint)InOutBuffer.Length * sizeof(byte);
            var iogch = GCHandle.Alloc(InOutBuffer, GCHandleType.Pinned);
            using (global::IoControl.Disposable.Create(iogch.Free))
            {
                var inoutPtr = iogch.AddrOfPinnedObject();
                return await DeviceIoControlAsync(IoControlCode, inoutPtr, inoutSize, Token);
            }
        }
        public bool DeviceIoControl<TIN, TOUT>(IOControlCode IoControlCode, in TIN InBuffer, out TOUT OutBuffer, out uint ReturnBytes)
            where TIN : struct
            where TOUT : struct
        {
            var inSize = (uint)Marshal.SizeOf(typeof(TIN));
            var inPtr = Marshal.AllocCoTaskMem((int)inSize);
            var outSize = (uint)Marshal.SizeOf(typeof(TOUT));
            var outPtr = Marshal.AllocCoTaskMem((int)outSize);
            using (global::IoControl.Disposable.Create(() => Marshal.FreeCoTaskMem(inPtr)))
            using (global::IoControl.Disposable.Create(() => Marshal.FreeCoTaskMem(outPtr)))
            {
                Marshal.StructureToPtr(InBuffer, inPtr, false);
                var result = DeviceIoControl(IoControlCode, inPtr, inSize, outPtr, outSize, out ReturnBytes);
                OutBuffer = (TOUT)Marshal.PtrToStructure(outPtr, typeof(TOUT));
                return result;
            }
        }
        public async Task<(bool Result, TOUT OutBuffer)> DeviceIoControlAsync<TIN,TOUT>(IOControlCode IoControlCode, TIN InBuffer, CancellationToken Token = default)
            where TIN : struct
            where TOUT : struct
        {
            var inSize = (uint)Marshal.SizeOf(typeof(TIN));
            var inPtr = Marshal.AllocCoTaskMem((int)inSize);
            var outSize = (uint)Marshal.SizeOf(typeof(TOUT));
            var outPtr = Marshal.AllocCoTaskMem((int)outSize);
            using (global::IoControl.Disposable.Create(() => Marshal.FreeCoTaskMem(inPtr)))
            using (global::IoControl.Disposable.Create(() => Marshal.FreeCoTaskMem(outPtr)))
            {
                Marshal.StructureToPtr(InBuffer, inPtr, false);
                var (result, ReturnBytes) = await DeviceIoControlAsync(IoControlCode, inPtr, inSize, outPtr, outSize, Token);
                return (result,(TOUT)Marshal.PtrToStructure(outPtr, typeof(TOUT)));
            }
        }
        public bool DeviceIoControl(IOControlCode dwIoControlCode, out uint ReturnBytes)
            => NativeMethod.DeviceIoControl(Handle, dwIoControlCode, IntPtr.Zero, 0, IntPtr.Zero, 0, out ReturnBytes);

        public async Task<bool> DeviceIoControlAsync(IOControlCode dwIoControlCode, int millisecondTimeout = Timeout.Infinite, CancellationToken Token = default)
        {

            using (var deviceIoOverlapped = new DeviceIoOverlapped())
            using (var hEvent = new ManualResetEvent(false))
            {
                deviceIoOverlapped.ClearAndSetEvent(hEvent.SafeWaitHandle.DangerousGetHandle());

                var result = NativeMethod.DeviceIoControl(Handle, dwIoControlCode, IntPtr.Zero, 0, IntPtr.Zero, 0, out var ret, deviceIoOverlapped.GlobalOverlapped);
                var hrCode = Marshal.GetHRForLastWin32Error();
                if (!result)
                    Marshal.ThrowExceptionForHR(hrCode);
                await hEvent.WaitOneAsync();

                result = NativeMethod.GetOverlappedResult(Handle, deviceIoOverlapped, out var ret2, false);
                hrCode = Marshal.GetHRForLastWin32Error();
                if (!result)
                    Marshal.ThrowExceptionForHR(hrCode);
                return result;
            }
        }

        public bool DeviceIoControlInOnly(IOControlCode dwIoControlCode, IntPtr InPtr, uint InSize, out uint ReturnBytes)
            => NativeMethod.DeviceIoControl(Handle, dwIoControlCode, InPtr, InSize, IntPtr.Zero, 0u, out ReturnBytes);
        public bool DeviceIoControlInOnly<TIN>(IOControlCode dwIoControlCode, in TIN InBuffer, out uint ReturnBytes)
            where TIN : struct
        {
            var inSize = (uint)Marshal.SizeOf(typeof(TIN));
            var inPtr = Marshal.AllocCoTaskMem((int)inSize);
            using (global::IoControl.Disposable.Create(() => Marshal.FreeCoTaskMem(inPtr)))
            {
                Marshal.StructureToPtr(InBuffer, inPtr, false);
                var result = DeviceIoControlInOnly(dwIoControlCode, inPtr, inSize, out ReturnBytes);
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
            using (global::IoControl.Disposable.Create(() => Marshal.FreeCoTaskMem(outPtr)))
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
            using (global::IoControl.Disposable.Create(ogch.Free))
            {
                var outPtr = ogch.AddrOfPinnedObject();
                var result = DeviceIoControlOutOnly(IoControlCode, outPtr, outSize, out ReturnBytes);
                return result;
            }
        }
        const int ERROR_INSUFFICIENT_BUFFER = unchecked((int)0x8007007A);
        public bool DeviceIoControlOutOnly<T>(IOControlCode IoControlCode, out T output, Func<IntPtr, T, T> Setter)
            where T : struct
        {
            var Size = (uint)Marshal.SizeOf<T>();
            var ReturnSize = 0u;
            while (ReturnSize == 0u)
            {
                var Ptr = Marshal.AllocCoTaskMem((int)Size);
                using (global::IoControl.Disposable.Create(() => Marshal.FreeCoTaskMem(Ptr)))
                {
                    var result = DeviceIoControlOutOnly(IoControlCode, Ptr, Size, out ReturnSize);
                    if (result)
                    {
                        output = (T)Marshal.PtrToStructure(Ptr, typeof(T));
                        output = Setter(Ptr, output);
                        return result;
                    }

                    var ErrorCode = Marshal.GetHRForLastWin32Error();
                    if (ErrorCode == ERROR_INSUFFICIENT_BUFFER)
                    {
                        Size *= 2;
                        continue;
                    }
                    else break;
                }
            }
            output = default;
            return false;
        }
        internal bool DeviceIoControlIs(IOControlCode IOControlCode, params uint[] NotErrorCodes)
        {
            var result = DeviceIoControl(IOControlCode, out var _);
            if (!result)
            {
                var win32error = Marshal.GetHRForLastWin32Error();
                if (!NotErrorCodes.Any(ErrorCode => unchecked((uint)win32error) == ErrorCode))
                    Marshal.ThrowExceptionForHR(win32error);
            }
            return result;
        }
        internal static SafeFileHandle CreateFile(string Filename, FileAccess FileAccess = default, FileShare FileShare = default, FileMode CreationDisposition = default, FileFlagAndAttributes FlagsAndAttributes = default, SafeFileHandle TemplateFile = default)
            => NativeMethod.CreateFile(Filename, FileAccess, FileShare, IntPtr.Zero, CreationDisposition, FlagsAndAttributes, TemplateFile);
        internal static SafeFileHandle CreateFile(string Filename, FileAccess FileAccess = default, FileShare FileShare = default, FileMode CreationDisposition = default, FileFlagAndAttributes FlagsAndAttributes = default)
            => NativeMethod.CreateFileW(Filename, FileAccess, FileShare, IntPtr.Zero, CreationDisposition, FlagsAndAttributes, IntPtr.Zero);
        public override string ToString()
            => $"{nameof(IoControl)}{{{(string.IsNullOrEmpty(Filename) ? string.Empty : $"{nameof(Filename)}:{Filename},")} {nameof(Disposable)}:{Disposable} {nameof(IsInvalid)}:{IsInvalid}, {nameof(IsClosed)}:{IsClosed}}}";
    }
}
