using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace IoControl.Volume
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public readonly struct VolumeDiskExtent : IEnumerable<DiskExtent>
    {
        /// <summary>
        /// Number Of Disk Extents
        /// </summary>
        public readonly uint NumberOfDiskExtents;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        private readonly DiskExtent[] _Extents;
        /// <summary>
        /// Extents
        /// </summary>
        public DiskExtent[] Extents => (_Extents ?? Enumerable.Empty<DiskExtent>()).Take((int)NumberOfDiskExtents).ToArray();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Extents"></param>
        public VolumeDiskExtent(DiskExtent[] Extents) => (NumberOfDiskExtents, this._Extents) = ((uint)(Extents?.Length ?? 0), (Extents?.Length ?? 0) == 0 ? new DiskExtent[1] : Extents);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IntPtr"></param>
        /// <param name="Size"></param>
        public VolumeDiskExtent(IntPtr IntPtr, uint Size)
        {
            this = (VolumeDiskExtent)Marshal.PtrToStructure(IntPtr, typeof(VolumeDiskExtent));
            if (NumberOfDiskExtents <= 1)
                return;
            var VolumeDiskExtentSize = Marshal.SizeOf<VolumeDiskExtent>();
            var DiskExtentSize = Marshal.SizeOf<DiskExtent>();
            var _Size = ((int)Size) - VolumeDiskExtentSize;
            if (_Size / DiskExtentSize < (((int)NumberOfDiskExtents) - 1))
                return;
            var ArrayPtr = IntPtr.Add(IntPtr, (int)Marshal.OffsetOf<VolumeDiskExtent>(nameof(_Extents)));
            _Extents = Enumerable.Range(0, (int)NumberOfDiskExtents)
                .Select(index => (DiskExtent)Marshal.PtrToStructure(IntPtr.Add(ArrayPtr, DiskExtentSize * index), typeof(DiskExtent)))
                .ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{nameof(VolumeDiskExtent)}{{{nameof(NumberOfDiskExtents)}:{NumberOfDiskExtents}, [{string.Join(" , ", Extents)}]}}";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<DiskExtent> GetEnumerator() => ((IEnumerable<DiskExtent>)Extents).GetEnumerator();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
