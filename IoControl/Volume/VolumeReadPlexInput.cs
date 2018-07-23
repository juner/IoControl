using System.Runtime.InteropServices;

namespace IoControl.Volume
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VolumeReadPlexInput
    {
        /// <summary>
        /// Specifies the offset within the volume from where to read data.
        /// </summary>
        public long ByteOffset;
        /// <summary>
        /// Specifies the amount of data in bytes to be read in.
        /// </summary>
        public uint Length;

        //
        // Specifies the plex from which the
        // data is to be read in.
        //
        public uint PlexNumber;

    }
}
