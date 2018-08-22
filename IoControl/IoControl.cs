using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using IoControl.DataUtils;

namespace IoControl
{
    public class IoControl : IDisposable
    {
        readonly SafeFileHandle Handle;
        readonly string Filename;
        readonly bool Disposable;
        /// <summary>
        /// 
        /// </summary>
        public bool IsInvalid => Handle.IsInvalid;
        /// <summary>
        /// 
        /// </summary>
        public bool IsClosed => Handle.IsClosed;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IntPtr DangerousGetHandle() => Handle.DangerousGetHandle();
        public FileType FileType => NativeMethod.GetFileType(Handle);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Handle"></param>
        /// <param name="Diposable"></param>
        /// <param name="Filename"></param>
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
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool CancelIoEx(SafeFileHandle hFile, IntPtr lpOverlapped = default);
            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.U4)]
            public static extern FileType GetFileType(SafeFileHandle hFile);
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
        /// <param name="InputData"></param>
        /// <param name="OutputData"></param>
        /// <param name="ReturnBytes"></param>
        /// <returns></returns>
        public bool DeviceIoControl(IOControlCode IoControlCode, DataPtr InputData, DataPtr OutputData, out uint ReturnBytes)
        {
            using (InputData.GetPtrAndSize(out var InputPtr, out var InputSize))
            using (OutputData.GetPtrAndSize(out var OutputPtr, out var OutputSize))
                return DeviceIoControl(IoControlCode, InputPtr, InputSize, OutputPtr, OutputSize, out ReturnBytes);
        }
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
        public async Task<(bool Result,uint ReturnBytes)> DeviceIoControlAsync(IOControlCode IoControlCode, IntPtr InputPtr, uint InputSize, IntPtr OutputPtr, uint OutputSize, CancellationToken Token = default)
        {
            Token.ThrowIfCancellationRequested();
            using (var deviceIoOverlapped = new DeviceIoOverlapped())
            using (var hEvent = new ManualResetEvent(false))
            {
                deviceIoOverlapped.ClearAndSetEvent(hEvent.SafeWaitHandle.DangerousGetHandle());
                var result = NativeMethod.DeviceIoControl(Handle, IoControlCode, InputPtr, InputSize, OutputPtr, OutputSize, out var ret, deviceIoOverlapped.GlobalOverlapped);
                if (result)
                    return (result, (ushort)ret);
                using (Token.Register(() => NativeMethod.CancelIoEx(Handle, deviceIoOverlapped.GlobalOverlapped)))    
                    await hEvent.WaitOneAsync(Token);
                return (NativeMethod.GetOverlappedResult(Handle, deviceIoOverlapped, out var ret2, false), ret2);
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
        public Task<(bool Result, uint ReturnBytes)> DeviceIoControlAsync(IOControlCode IoControlCode, IntPtr InOutPtr, uint InOutSize, CancellationToken Token = default) => DeviceIoControlAsync(IoControlCode, InOutPtr, InOutSize, InOutPtr, InOutSize, Token);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IoControlCode"></param>
        /// <param name="InOutDataPtr"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public async Task<(bool Result,uint ReturnBytes)> DeviceIoControlAsync(IOControlCode IoControlCode, DataPtr InOutDataPtr, CancellationToken Token = default)
        {
            using (InOutDataPtr.GetPtrAndSize(out var InOutPtr, out var InOutSize))
            {
                var result = await DeviceIoControlAsync(IoControlCode, InOutPtr, InOutSize, Token);
                InOutDataPtr.SetPtr(InOutPtr, result.ReturnBytes);
                return result;
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
            using (InOutBuffer.CreatePtr(out var inoutPtr, out var inoutSize))
            {
                var result = DeviceIoControl(IoControlCode, inoutPtr, inoutSize, out ReturnBytes);
                InOutBuffer.SetPtr(inoutPtr);
                return result;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IoControlCode"></param>
        /// <param name="InOutData"></param>
        /// <param name="ReturnBytes"></param>
        /// <returns></returns>
        public bool DeviceIoControl(IOControlCode IoControlCode, DataPtr InOutData, out uint ReturnBytes)
        {
            using (InOutData.GetPtrAndSize(out var InOutPtr, out var Size))
            {
                var result = DeviceIoControl(IoControlCode, InOutPtr, Size, out ReturnBytes);
                InOutData.SetPtr(InOutPtr, Size);
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
            using (InBuffer.CreatePtr(out var inoutPtr, out var inoutSize))
            {
                var (result, ReturnBytes) = await DeviceIoControlAsync(IoControlCode, inoutPtr, inoutSize, Token);
                return (result, InBuffer.SetPtr(inoutPtr));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IoControlCode"></param>
        /// <param name="InOutBuffer"></param>
        /// <param name="ReturnBytes"></param>
        /// <returns></returns>
        public bool DeviceIoControl(IOControlCode IoControlCode, byte[] InOutBuffer, out uint ReturnBytes) => DeviceIoControl(IoControlCode, new BytesPtr(InOutBuffer), out ReturnBytes);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IoControlCode"></param>
        /// <param name="InOutBuffer"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public Task<(bool Result, uint ReturnBytes)> DeviceIoControlAsync(IOControlCode IoControlCode, byte[] InOutBuffer, CancellationToken Token = default) => DeviceIoControlAsync(IoControlCode, new BytesPtr(InOutBuffer), Token);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IoControlCode"></param>
        /// <param name="InDataPtr"></param>
        /// <param name="OutDataPtr"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public async Task<(bool Result, uint ReturnBytes)> DeviceIoControlAsync(IOControlCode IoControlCode, DataPtr InDataPtr, DataPtr OutDataPtr, CancellationToken Token = default) {
            using (InDataPtr.GetPtrAndSize(out var InPtr, out var InSize))
            using (OutDataPtr.GetPtrAndSize(out var OutPtr, out var OutSize))
            {
                var result = await DeviceIoControlAsync(IoControlCode, InPtr, InSize, OutPtr, OutSize);
                OutDataPtr.SetPtr(OutPtr, result.ReturnBytes);
                return result;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TIN"></typeparam>
        /// <typeparam name="TOUT"></typeparam>
        /// <param name="IoControlCode"></param>
        /// <param name="InBuffer"></param>
        /// <param name="OutBuffer"></param>
        /// <param name="ReturnBytes"></param>
        /// <returns></returns>
        public bool DeviceIoControl<TIN, TOUT>(IOControlCode IoControlCode, in TIN InBuffer, out TOUT OutBuffer, out uint ReturnBytes)
            where TIN : struct
            where TOUT : struct
        {
            var OutDataPtr = new StructPtr<TOUT>();
            var result = DeviceIoControl(IoControlCode, new StructPtr<TIN>(InBuffer), OutDataPtr, out ReturnBytes);
            OutBuffer = OutDataPtr.Get();
            return result;
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TIN"></typeparam>
        /// <typeparam name="TOUT"></typeparam>
        /// <param name="IoControlCode"></param>
        /// <param name="InBuffer"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public async Task<TOUT?> DeviceIoControlAsync<TIN,TOUT>(IOControlCode IoControlCode, TIN InBuffer, CancellationToken Token = default)
            where TIN : struct
            where TOUT : struct
        {
            var OutDataPtr = new StructPtr<TOUT>();
            var (result, ReturnBytes) = await DeviceIoControlAsync(IoControlCode, new StructPtr<TIN>(InBuffer), OutDataPtr, Token);
            return result ? OutDataPtr.Get() : (TOUT?)null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dwIoControlCode"></param>
        /// <param name="ReturnBytes"></param>
        /// <returns></returns>
        public bool DeviceIoControl(IOControlCode dwIoControlCode, out uint ReturnBytes)
            => NativeMethod.DeviceIoControl(Handle, dwIoControlCode, IntPtr.Zero, 0, IntPtr.Zero, 0, out ReturnBytes);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dwIoControlCode"></param>
        /// <param name="millisecondTimeout"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public async Task<bool> DeviceIoControlAsync(IOControlCode IoControlCode, CancellationToken Token = default) => (await DeviceIoControlAsync(IoControlCode, IntPtr.Zero, 0u, IntPtr.Zero, 0u, Token)).Result;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dwIoControlCode"></param>
        /// <param name="InPtr"></param>
        /// <param name="InSize"></param>
        /// <param name="ReturnBytes"></param>
        /// <returns></returns>
        public bool DeviceIoControlInOnly(IOControlCode IoControlCode, IntPtr InPtr, uint InSize, out uint ReturnBytes)
            => NativeMethod.DeviceIoControl(Handle, IoControlCode, InPtr, InSize, IntPtr.Zero, 0u, out ReturnBytes);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IoControlCode"></param>
        /// <param name="InPtr"></param>
        /// <param name="InSize"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public async Task<bool> DeviceIoControlInOnlyAsync(IOControlCode IoControlCode, IntPtr InPtr, uint InSize, CancellationToken Token = default)
            => (await DeviceIoControlAsync(IoControlCode, InPtr, InSize, IntPtr.Zero, 0, Token)).Result;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IoControlCode"></param>
        /// <param name="DataPtr"></param>
        /// <param name="ReturnBytes"></param>
        /// <returns></returns>
        public bool DeviceIoControlInOnly(IOControlCode IoControlCode, DataPtr DataPtr,out uint ReturnBytes)
        {
            using (DataPtr.GetPtrAndSize(out var InPtr, out var InSize))
                return DeviceIoControlInOnly(IoControlCode, InPtr, InSize, out ReturnBytes);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TIN"></typeparam>
        /// <param name="IoControlCode"></param>
        /// <param name="InBuffer"></param>
        /// <param name="ReturnBytes"></param>
        /// <returns></returns>
        public bool DeviceIoControlInOnly<TIN>(IOControlCode IoControlCode, in TIN InBuffer, out uint ReturnBytes)
            where TIN : struct => DeviceIoControlInOnly(IoControlCode, new StructPtr<TIN>(InBuffer), out ReturnBytes);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IoControlCode"></param>
        /// <param name="OutputPtr"></param>
        /// <param name="OutputSize"></param>
        /// <param name="ReturnBytes"></param>
        /// <returns></returns>
        public bool DeviceIoControlOutOnly(IOControlCode IoControlCode, IntPtr OutputPtr, uint OutputSize, out uint ReturnBytes)
            => DeviceIoControl(IoControlCode, IntPtr.Zero, 0, OutputPtr, OutputSize, out ReturnBytes);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IoControlCode"></param>
        /// <param name="OutPtr"></param>
        /// <param name="OutSize"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public Task<(bool Result, uint ReturnBytes)> DeviceIoControlIOutOnlyAsync(IOControlCode IoControlCode, IntPtr OutPtr, uint OutSize, CancellationToken Token = default)
            => DeviceIoControlAsync(IoControlCode, IntPtr.Zero, 0, OutPtr, OutSize, Token);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IoControlCode"></param>
        /// <param name="DataPtr"></param>
        /// <param name="ReturnBytes"></param>
        /// <returns></returns>
        public bool DeviceIoControlOutOnly(IOControlCode IoControlCode, DataPtr DataPtr, out uint ReturnBytes)
        {
            using (DataPtr.GetPtrAndSize(out var OutPtr, out var OutSize))
            {
                var result = DeviceIoControlOutOnly(IoControlCode, OutPtr, OutSize, out ReturnBytes);
                DataPtr.SetPtr(OutPtr, ReturnBytes);
                return result;
            }
        }
        public async Task<(bool Result, uint ReturnBytes)> DeviceIoControlIOutOnlyAsync(IOControlCode IoControlCode, DataPtr DataPtr, CancellationToken Token = default)
        {
            using (DataPtr.GetPtrAndSize(out var OutPtr, out var OutSize))
                return await DeviceIoControlIOutOnlyAsync(IoControlCode, OutPtr, OutSize, Token);
        }
        public bool DeviceIoControlOutOnly<TOUT>(IOControlCode IoControlCode, out TOUT OutBuffer, out uint ReturnBytes)
            where TOUT : struct
        {
            var data = new StructPtr<TOUT>();
            var result = DeviceIoControlOutOnly(IoControlCode, data, out ReturnBytes);
            OutBuffer = data.Get();
            return result;
        }
        public async  Task<(bool Result, TOUT Output, uint ReturnBytes)> DeviceIoControlIOutOnlyAsync<TOUT>(IOControlCode IoControlCode, CancellationToken Token = default)
            where TOUT : struct
        {
            var data = new StructPtr<TOUT>();
            var (Result, ReturnBytes) = await DeviceIoControlIOutOnlyAsync(IoControlCode, data, Token);
            return (Result, data.Get(), ReturnBytes);
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
        public bool DeviceIoControlOutOnly<T>(IOControlCode IoControlCode, out T output, Func<IntPtr, uint, T> Creater, out uint ReturnBytes)
            where T : struct
        {
            var Size = (uint)Marshal.SizeOf<T>();
            ReturnBytes = 0u;
            while (ReturnBytes == 0u)
            {
                var Ptr = Marshal.AllocCoTaskMem((int)Size);
                using (global::IoControl.Disposable.Create(() => Marshal.FreeCoTaskMem(Ptr)))
                {
                    var result = DeviceIoControlOutOnly(IoControlCode, Ptr, Size, out ReturnBytes);
                    if (result)
                    {
                        output = Creater(Ptr, ReturnBytes);
                        return result;
                    }

                    var ErrorCode = Marshal.GetHRForLastWin32Error();
                    if (ErrorCode == ERROR_INSUFFICIENT_BUFFER)
                    {
                        Size *= 2;
                        continue;
                    }
                    break;
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
    internal static class StructExtensions {
        public static IDisposable CreatePtr<T>(ref this T self, out IntPtr IntPtr, out uint Size)
            where T : struct
        {
            var _Size = Marshal.SizeOf<T>();
            var _IntPtr = Marshal.AllocCoTaskMem(_Size);
            var Disposable = global::IoControl.Disposable.Create(() => Marshal.FreeCoTaskMem(_IntPtr));
            try
            {
                Marshal.StructureToPtr(self, _IntPtr, false);
            }
            catch 
            {
                Disposable?.Dispose();
                throw;
            }
            IntPtr = _IntPtr;
            Size = (uint)_Size;
            return Disposable;
        }
        public static ref T SetPtr<T>(ref this T self, IntPtr IntPtr)
            where T : struct
        {
            self = (T)Marshal.PtrToStructure(IntPtr, typeof(T));
            return ref self;
        }
    }
    public enum FileType : uint
    {
        Unknown = 0x0000,
        Disk = 0x0001,
        Char = 0x0002,
        Pipe = 0x0003,
        Remote = 0x0004,

    }
}
