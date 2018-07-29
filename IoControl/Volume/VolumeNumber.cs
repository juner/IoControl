using System.Linq;
using System.Runtime.InteropServices;

namespace IoControl.Volume
{
    /// <summary>
    /// VOLUME_NUMBER structure
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct VolumeNumber
    {
        /// <summary>
        /// Specifies the  number associated with this volume for the current session.
        /// </summary>
        public uint VolumeNum;
        /// <summary>
        /// Specifies the name of the volume manager driver.  If this is less than 8, it is padded with blanks.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public ushort[] VolumeManagerName;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="VolumeNumber"></param>
        /// <param name="VolumeManagerName"></param>
        public VolumeNumber(uint VolumeNumber, ushort[] VolumeManagerName)
            => (VolumeNum, this.VolumeManagerName) = (VolumeNumber, VolumeManagerName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="VolumeNumber"></param>
        /// <param name="VolumeManagerName"></param>
        public void Deconstruct(out uint VolumeNumber, out ushort[] VolumeManagerName)
            => (VolumeNumber, VolumeManagerName) = (this.VolumeNum, this.VolumeManagerName);
        public override string ToString()
            => $"{nameof(VolumeNumber)}{{{nameof(VolumeNum)}:{VolumeNum}, {nameof(VolumeManagerName)}:[{string.Join("", (VolumeManagerName ?? Enumerable.Empty<ushort>()).Select(v => char.ConvertFromUtf32(v)))}]}}";
    }
}
