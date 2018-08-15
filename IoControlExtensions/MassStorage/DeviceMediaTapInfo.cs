using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct DeviceMediaTapInfo
    {
        public readonly StorageMediaType MediaType;
        public readonly MediaFlags MediaCharacteristics;
        public readonly uint CurrentBlockSize;
        public readonly StorageBusType BusType;
        public readonly DeviceMediaBusSpecificData BusSpecificData;
        public DeviceMediaTapInfo(StorageMediaType MediaType, MediaFlags MediaCharacteristics, uint CurrentBlockSize, StorageBusType BusType, DeviceMediaBusSpecificData BusSpecificData)
            => (this.MediaType, this.MediaCharacteristics, this.CurrentBlockSize, this.BusType, this.BusSpecificData)
            = (MediaType, MediaCharacteristics, CurrentBlockSize, BusType, BusSpecificData);
        public DeviceMediaTapInfo Set(StorageMediaType? MediaType, MediaFlags? MediaCharacteristics, uint? CurrentBlockSize, StorageBusType? BusType, DeviceMediaBusSpecificData? BusSpecificData)
            => MediaType == null && MediaCharacteristics == null && CurrentBlockSize == null && BusType == null && BusSpecificData == null ? this
            : new DeviceMediaTapInfo(MediaType ?? this.MediaType, MediaCharacteristics ?? this.MediaCharacteristics, CurrentBlockSize ?? this.CurrentBlockSize, BusType ?? this.BusType, BusSpecificData ?? this.BusSpecificData);
        public override string ToString()
            => $"{nameof(DeviceMediaTapInfo)}{{{nameof(MediaType)}:{MediaType}, {nameof(MediaCharacteristics)}:{MediaCharacteristics}, {nameof(CurrentBlockSize)}:{CurrentBlockSize}, {nameof(BusType)}:{BusType}, {nameof(BusSpecificData)}:{BusSpecificData}}}";
    }
}
