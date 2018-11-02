using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace IoControl.Controller
{
    public readonly struct IdentifyDevice
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst =512)]
        readonly byte[] Bin;
        IdentifyDevice(byte[] Bin) => this.Bin = Bin;
        public AtaIdentifyDevice A => ToAtaIdentifyDevice();
        public NvmeIdentifyDevice N => ToMvmeIdentifyDevice();
        public byte[] B => (Bin ?? Enumerable.Empty<byte>()).Concat(Enumerable.Repeat<byte>(0, 512)).ToArray();
        NvmeIdentifyDevice ToMvmeIdentifyDevice()
        {
            var Size = Marshal.SizeOf<IdentifyDevice>();
            IntPtr IntPtr = Marshal.AllocCoTaskMem(Size);
            using (Disposable.Create(() => Marshal.FreeCoTaskMem(IntPtr)))
            {
                Marshal.Copy(Bin, 0, IntPtr, Bin.Length);
                return (NvmeIdentifyDevice)Marshal.PtrToStructure(IntPtr, typeof(NvmeIdentifyDevice));
            }
        }
        static IdentifyDevice From(in NvmeIdentifyDevice IdentifyDevice)
        {
            var Size = Marshal.SizeOf<NvmeIdentifyDevice>();
            IntPtr IntPtr = Marshal.AllocCoTaskMem(Size);
            using (Disposable.Create(() => Marshal.FreeCoTaskMem(IntPtr)))
            {
                Marshal.StructureToPtr(IdentifyDevice, IntPtr, false);
                var Bin = new byte[Size];
                Marshal.Copy(IntPtr, Bin, 0, Size);
                return new IdentifyDevice(Bin);
            }
        }
        AtaIdentifyDevice ToAtaIdentifyDevice()
        {
            var Size = Marshal.SizeOf<IdentifyDevice>();
            IntPtr IntPtr = Marshal.AllocCoTaskMem(Size);
            using (Disposable.Create(() => Marshal.FreeCoTaskMem(IntPtr)))
            {
                Marshal.Copy(Bin, 0, IntPtr, Bin.Length);
                return (AtaIdentifyDevice)Marshal.PtrToStructure(IntPtr, typeof(AtaIdentifyDevice));
            }
        }
        static IdentifyDevice From(in AtaIdentifyDevice IdentifyDevice)
        {
            var Size = Marshal.SizeOf<AtaIdentifyDevice>();
            IntPtr IntPtr = Marshal.AllocCoTaskMem(Size);
            using (Disposable.Create(() => Marshal.FreeCoTaskMem(IntPtr)))
            {
                Marshal.StructureToPtr(IdentifyDevice, IntPtr, false);
                var Bin = new byte[Size];
                Marshal.Copy(IntPtr, Bin, 0, Size);
                return new IdentifyDevice(Bin);
            }
        }
        public static implicit operator IdentifyDevice(in NvmeIdentifyDevice IdentifyDevice) => From(IdentifyDevice);
        public static implicit operator IdentifyDevice(in AtaIdentifyDevice IdentifyDevice) => From(IdentifyDevice);
        public static implicit operator NvmeIdentifyDevice(in IdentifyDevice IdentifyDevice) => IdentifyDevice.N;
        public static implicit operator AtaIdentifyDevice(in IdentifyDevice IdentifyDevice) => IdentifyDevice.A;
    }
}
