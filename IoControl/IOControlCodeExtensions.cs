using System.IO;
using System.Runtime.CompilerServices;

namespace IoControl
{
    public static class IOControlCodeExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IOControlCode CtrlCode(FileDevice DeviceType, uint Function, Method Method, FileAccess Access)
       => (IOControlCode)CtrlCode((uint)DeviceType, Function, (uint)Method, (uint)Access);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint CtrlCode(uint DeviceType, uint Function, uint Method, uint Access)
            => (DeviceType << 16) | (Function << 2) | Method | (Access << 14);
        public static (FileDevice DeviceType, uint Function, Method Method, FileAccess Access) UnCtrlCode(uint CtrlCode)
            => ((FileDevice)((CtrlCode & 0xffff_0000) >> 16), (CtrlCode & 0x0000_3FFC) >> 2, (Method)((CtrlCode & 0x0000_0003) >> 2), (FileAccess)((CtrlCode & 0x0000_C000) >> 14));
        public static void Deconstruct(this IOControlCode CtrlCode, out FileDevice DeviceType, out uint Function, out Method Method, out FileAccess Access)
            => (DeviceType, Function, Method, Access) = UnCtrlCode((uint)CtrlCode);
    }
}
