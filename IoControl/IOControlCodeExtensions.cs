using System.IO;
using System.Runtime.CompilerServices;

namespace IoControl
{
    public static class IOControlCodeExtensions
    {
        /// <summary>
        /// CTL_CODE
        /// </summary>
        /// <param name="DeviceType"></param>
        /// <param name="Function"></param>
        /// <param name="Method"></param>
        /// <param name="Access"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IOControlCode CtrlCode(FileDevice DeviceType, uint Function = default, Method Method = default, FileAccess Access = default)
       => (IOControlCode)CtrlCode((uint)DeviceType, Function, (uint)Method, (uint)Access);
        /// <summary>
        /// CTL_CODE
        /// </summary>
        /// <param name="DeviceType"></param>
        /// <param name="Function"></param>
        /// <param name="Method"></param>
        /// <param name="Access"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint CtrlCode(uint DeviceType, uint Function = default, uint Method = default, uint Access = default)
            => (DeviceType << 16) | (Function << 2) | Method | (Access << 14);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CtrlCode"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FileDevice GetDeviceType(this IOControlCode CtrlCode) => (FileDevice)(((uint)CtrlCode & 0xffff_0000) >> 16);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CtrlCode"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetFunction(this IOControlCode CtrlCode) => ((uint)CtrlCode & 0x0000_3FFC) >> 2;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CtrlCode"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Method GetMethod(this IOControlCode CtrlCode) => (Method)(((uint)CtrlCode & 0x0000_0003) >> 2);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CtrlCode"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FileAccess GetAccess(this IOControlCode CtrlCode) => (FileAccess)(((uint)CtrlCode & 0x0000_C000) >> 14);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CtrlCode"></param>
        /// <param name="DeviceType"></param>
        /// <param name="Function"></param>
        /// <param name="Method"></param>
        /// <param name="Access"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this IOControlCode CtrlCode, out FileDevice DeviceType, out uint Function, out Method Method, out FileAccess Access)
            => (DeviceType, Function, Method, Access) = (GetDeviceType(CtrlCode), GetFunction(CtrlCode), GetMethod(CtrlCode), GetAccess(CtrlCode));
    }
}
