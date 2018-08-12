using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct DeviceMediaScsiInformation
    {
        public readonly byte MediumType;
        public readonly byte DensityCode;
        public DeviceMediaScsiInformation(byte MediumType, byte DensityCode)
            => (this.MediumType, this.DensityCode) = (MediumType, DensityCode);
    }
}
