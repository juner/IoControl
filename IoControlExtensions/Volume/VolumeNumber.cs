using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

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
        public uint Number;
        /// <summary>
        /// Specifies the name of the volume manager driver.  If this is less than 8, it is padded with blanks.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public ushort[] _VolumeManagerName;
        public string VolumeManagerName => Encoding.Unicode.GetString((_VolumeManagerName ?? Enumerable.Empty<ushort>()).SelectMany(v => BitConverter.GetBytes(v)).ToArray());
        /// <summary>
        /// 
        /// </summary>
        /// <param name="VolumeNumber"></param>
        /// <param name="VolumeManagerName"></param>
        public VolumeNumber(uint VolumeNumber, ushort[] VolumeManagerName)
            => (Number, this._VolumeManagerName) = (VolumeNumber, VolumeManagerName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="VolumeNumber"></param>
        /// <param name="VolumeManagerName"></param>
        public void Deconstruct(out uint VolumeNumber, out ushort[] VolumeManagerName)
            => (VolumeNumber, VolumeManagerName) = (Number, this._VolumeManagerName);
        public override string ToString()
            => $"{nameof(VolumeNumber)}{{{nameof(Number)}:{Number}, {nameof(VolumeManagerName)}:{VolumeManagerName}}}";
    }
}
