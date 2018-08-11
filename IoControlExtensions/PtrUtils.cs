using System;
using System.Runtime.InteropServices;

namespace IoControl
{
    internal static class PtrUtils
    {
        public static T PtrToStructure<T>(IntPtr IntPtr, uint Size) where T : struct => (T)Marshal.PtrToStructure(IntPtr, typeof(T));
        public static IDisposable CreatePtr(uint Size, out IntPtr IntPtr)
        {
            var _IntPtr = Marshal.AllocCoTaskMem((int)Size);
            var Dispose = Disposable.Create(() => Marshal.FreeCoTaskMem(_IntPtr));
            IntPtr = _IntPtr;
            return Dispose;
        }
        public static IDisposable CreatePtr<T>(out IntPtr IntPtr, out uint Size) where T : struct => CreatePtr(Size = (uint)Marshal.SizeOf<T>(), out IntPtr);
        public static IDisposable CreatePtr<T>(in T t,out IntPtr IntPtr, out uint Size)
            where T : struct
        {
            var Dispose = CreatePtr<T>(out IntPtr, out Size);
            try
            {
                StructureToPtr(t, IntPtr);
                return Dispose;
            }
            catch
            {
                Dispose.Dispose();
                throw;
            }
        }
        public static void StructureToPtr<T>(in T t, IntPtr IntPtr) => Marshal.StructureToPtr(t, IntPtr, false);
    }
}
