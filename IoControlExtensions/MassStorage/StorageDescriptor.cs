using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct StorageDescriptor : DataUtils.IPtrCreatable
    {
        /// <summary>
        /// Contains the version of the data reported.
        /// </summary>
        public readonly uint Version;
        /// <summary>
        /// Indicates the quantity of data reported, in bytes.
        /// </summary>
        public readonly uint Size;
        /// <summary>
        /// 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        private readonly byte[] _Data;
        public byte[] Data => _Data.Take(_Data.Length).ToArray();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Version"></param>
        /// <param name="Size"></param>
        /// <param name="Data"></param>
        public StorageDescriptor(uint Version, uint Size, byte[] Data)
            => (this.Version, this.Size, _Data) = (Version, Size, Data ?? new byte[0]);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Version"></param>
        /// <param name="Size"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public StorageDescriptor Set(uint? Version = null, uint? Size = null, byte[] Data = null)
            => Version == null && Size == null && Data == null ? this : new StorageDescriptor(Version ?? this.Version, Size ?? this.Size, Data ?? this.Data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Version"></param>
        /// <param name="Size"></param>
        /// <param name="Data"></param>
        public void Deconstruct(out uint Version, out uint Size, out byte[] Data)
            => (Version, Size, Data) = (this.Version, this.Size, this.Data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IntPtr"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        public StorageDescriptor(IntPtr IntPtr, uint Size)
        {
            var BaseSize = Marshal.SizeOf<StorageDescriptor>();
            if (Size < BaseSize)
                throw new ArgumentException($"引数:{nameof(Size)} の値が {BaseSize} よりも小さいです。");
            var ByteSize = (int)(Size - (BaseSize - 1));
            this = (StorageDescriptor)Marshal.PtrToStructure(IntPtr, typeof(StorageDescriptor));
            var Data = new byte[ByteSize];
            Marshal.Copy(IntPtr.Add(IntPtr, (int)Marshal.OffsetOf<StorageDescriptor>(nameof(_Data))), Data, 0, ByteSize);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IntPtr"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        public IDisposable CreatePtr(out IntPtr IntPtr, out uint Size)
        {
            var BaseSize = Marshal.SizeOf<StorageDescriptor>();
            Size = (uint)(BaseSize + (_Data?.Length ?? 1) - 1);
            var dispose = PtrUtils.CreatePtr(Size, out IntPtr);
            try
            {
                Marshal.StructureToPtr(this, IntPtr, false);
                Marshal.Copy(_Data, 0, IntPtr.Add(IntPtr, (int)Marshal.OffsetOf<StorageDescriptor>(nameof(_Data))), _Data.Length);
                return dispose;
            }
            catch
            {
                dispose.Dispose();
                throw;
            }
        }
    }
}
