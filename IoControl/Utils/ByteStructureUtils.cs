using System.Runtime.InteropServices;
namespace IoControl.Utils
{
    /// <summary>
    /// Byte[]関連の Util
    /// </summary>
    public static class ByteAndStructure {
        /// <summary>
        /// byte[] -> Structure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Bytes"></param>
        /// <param name="StartIndex"></param>
        /// <returns></returns>
        public static T ToStructure<T>(this byte[] Bytes, int StartIndex = default)
            where T : struct
        {
            var Size = Marshal.SizeOf<T>();
            var Ptr = Marshal.AllocCoTaskMem(Size);
            using (Disposable.Create(() => Marshal.FreeCoTaskMem(Ptr)))
            {
                Marshal.Copy(Bytes, StartIndex, Ptr, Size);
                return (T)Marshal.PtrToStructure(Ptr, typeof(T));
            }
        }
        /// <summary>
        /// byte[] &lt;- Structure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Bytes"></param>
        /// <param name="Structure"></param>
        /// <param name="StartIndex"></param>
        public static void FromStructure<T>(this byte[] Bytes, in T Structure, int StartIndex = default)
            where T : struct
        {
            var Size = Marshal.SizeOf<T>();
            var Ptr = Marshal.AllocCoTaskMem(Size);
            using (Disposable.Create(() => Marshal.FreeCoTaskMem(Ptr)))
            {
                Marshal.StructureToPtr(Structure, Ptr, false);
                Marshal.Copy(Ptr, Bytes, StartIndex, Size);
            }
        }
        /// <summary>
        /// Structure -> byte[]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Structure"></param>
        /// <param name="StartIndex"></param>
        /// <returns></returns>
        public static byte[] StructureToBytes<T>(in T Structure, int StartIndex = default, byte[] Bytes = default)
            where T : struct
        {
            var Size = Marshal.SizeOf<T>();
            var Ptr = Marshal.AllocCoTaskMem(Size);
            using (Disposable.Create(() => Marshal.FreeCoTaskMem(Ptr)))
            {
                Marshal.StructureToPtr(Structure, Ptr, false);
                if (Bytes == null)
                    Bytes = new byte[Size + StartIndex];
                Marshal.Copy(Ptr, Bytes, StartIndex, Size);
                return Bytes;
            }
        }
    }
}
