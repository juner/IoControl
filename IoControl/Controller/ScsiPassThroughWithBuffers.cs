using System.Runtime.InteropServices;

namespace IoControl.Controller
{
    /// <summary>
    /// SCSI_PASS_THROUGH_WITH_BUFFERS
    /// </summary>
    public struct ScsiPassThroughWithBuffers {

        public ScsiPassThrough Spt;
        /// <summary>
        /// realign buffers to double word boundary
        /// </summary>
        public uint Filler;
        [MarshalAs(UnmanagedType.ByValArray,SizeConst =32)]
        public byte[] SenseBuf;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst =512)]
        public byte[] DataBuf;

    }

}
