using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace IoControl.DataUtils
{
    /// <summary>
    /// struct ベースの データ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StructPtr<T> : IDataPtr
        where T : struct
    {
        protected T Struct;
        public StructPtr(T Struct) => this.Struct = Struct;
        public StructPtr() => Struct = default;
        public virtual IDisposable CreatePtr(out IntPtr IntPtr, out uint Size)
        {
            if (typeof(T).GetMethod(nameof(CreatePtr),new Type[]{ typeof(IntPtr).MakeByRefType(), typeof(uint).MakeByRefType() }) is MethodInfo MethodInfo)
            {
                var parameter = new object[2] { null, null };
                var Dispose = (IDisposable)MethodInfo.Invoke(Struct, parameter);
                IntPtr = (IntPtr)parameter[0];
                Size = (uint)parameter[1];
                return Dispose;
            }
            var _Size = Marshal.SizeOf<T>();
            var _IntPtr = Marshal.AllocCoTaskMem(_Size);
            var Disposable = global::IoControl.Disposable.Create(() => Marshal.FreeCoTaskMem(_IntPtr));
            if (!Struct.Equals(default))
                try
                {
                    Marshal.StructureToPtr(Struct, _IntPtr, false);
                }
                catch
                {
                    Disposable?.Dispose();
                    throw;
                }
            IntPtr = _IntPtr;
            Size = (ushort)_Size;
            return Disposable; ;
        }
        void IDataPtr.SetPtr(IntPtr IntPtr, uint Size) => SetPtr(IntPtr, Size);
        /// <summary>
        /// <paramref name="IntPtr"/>と<paramref name="Size"/>をもとにインスタンスを生成する。同じ引数のコンストラクタが定義されていたとき、
        /// </summary>
        /// <param name="IntPtr"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        public virtual ref T SetPtr(IntPtr IntPtr, uint Size)
        {
            Struct = (typeof(T).GetConstructor(new Type[] { typeof(IntPtr), typeof(uint) }) is ConstructorInfo c)
                ? (T)c.Invoke(new object[] { IntPtr, Size })
                : (T)Marshal.PtrToStructure(IntPtr, typeof(T));
            return ref Struct;
        }
        public ref T Get() => ref Struct;
        object IDataPtr.Get() => Get();
    }
}
