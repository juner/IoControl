using System;
using System.Linq;
using System.Runtime.InteropServices;
using static IoControl.PtrUtils;

namespace IoControl.MassStorage
{
    /// <summary>
    /// GET_MEDIA_TYPES
    /// </summary>
    public readonly struct GetMediaTypes
    {
        public readonly FileDevice DeviceType;
        public readonly uint MediaInfoCount;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst =1)]
        private readonly DeviceMediaInfo[] _MediaInfo;
        public DeviceMediaInfo[] MediaInfo => (_MediaInfo ?? Enumerable.Empty<DeviceMediaInfo>()).Take((int)MediaInfoCount).ToArray();
        public GetMediaTypes(FileDevice DeviceType, DeviceMediaInfo[] MediaInfo)
            => (this.DeviceType, MediaInfoCount, this._MediaInfo) = (DeviceType, (uint)(MediaInfo?.Length ?? 0), (MediaInfo?.Length ?? 0) == 0 ? new DeviceMediaInfo[1] : MediaInfo);
        public GetMediaTypes Set(FileDevice? DeviceType = null, DeviceMediaInfo[] MediaInfo = null)
            => DeviceType == null && MediaInfo == null ? this : new GetMediaTypes(DeviceType ?? this.DeviceType, MediaInfo ?? this._MediaInfo);
        public override string ToString()
            => $"{nameof(GetMediaTypes)}{{{nameof(DeviceType)}:{DeviceType}, {nameof(MediaInfoCount)}:{MediaInfoCount}, [{string.Join(", ", MediaInfo)}]}}";
        public GetMediaTypes(IntPtr IntPtr, uint Size)
            => this = PtrToStructure<GetMediaTypes>(IntPtr, Size) is GetMediaTypes MediaTypes
                && Marshal.SizeOf<DeviceMediaInfo>() is int ArrayItemSize
                && IntPtr.Add(IntPtr, (int)Marshal.OffsetOf<GetMediaTypes>(nameof(MediaTypes._MediaInfo))) is IntPtr ArrayPtr
                ? MediaTypes.Set(MediaInfo: Enumerable.Range(0, (int)MediaTypes.MediaInfoCount)
                .Select(index => PtrToStructure<DeviceMediaInfo>(IntPtr.Add(ArrayPtr, index * ArrayItemSize), (uint)ArrayItemSize))
                .ToArray()) : default;
    }
}
