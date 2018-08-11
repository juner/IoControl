using System.Linq;
using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    public readonly struct StorageDescriptor
    {
        /// <summary>
        /// Contains the version of the data reported.
        /// </summary>
        public readonly uint Version;
        /// <summary>
        /// Indicates the quantity of data reported, in bytes.
        /// </summary>
        public readonly uint Size;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        private readonly byte[] _Data;
        public byte[] Data => _Data.Take(_Data.Length).ToArray();
    }
}
