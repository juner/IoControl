using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    [StructLayout(LayoutKind.Explicit)]
    public readonly struct DeviceMediaBusSpecificData
    {
        [FieldOffset(0)]
        public readonly DeviceMediaScsiInformation ScsiInformation;
        public DeviceMediaBusSpecificData(DeviceMediaScsiInformation ScsiInformation)
            => this.ScsiInformation = ScsiInformation;
    }
}
