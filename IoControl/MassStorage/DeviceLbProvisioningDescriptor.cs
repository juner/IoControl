using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    /// <summary>
    /// _DEVICE_LB_PROVISIONING_DESCRIPTOR structure ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddstor/ns-ntddstor-_device_lb_provisioning_descriptor )
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct DeviceLbProvisioningDescriptor 
    {
        public readonly uint Version;
        public readonly uint Size;
        public readonly bool ThinProvisioningEnabled;
        public readonly bool ThinProvisioningReadZeros;
        public readonly bool AnchorSupported;
        public readonly bool UnmapGranularityAlignmentValid;
        public readonly byte Reserved0;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
        public readonly byte[] Reserved1;
        public readonly ulong OptimalUnmapGranularity;
        public readonly ulong UnmapGranularityAlignment;
        public readonly uint MaxUnmapLbaCount;
        public readonly uint MaxUnmapBlockDescriptorCount;
}
}
