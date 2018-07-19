using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace IoControl.MassStorage
{
    public static class MassStorageExtensions
    {
        public static StorageDeviceNumber StorageGetDeviceNumber(this IoControl IoControl)
        {
            var result = IoControl.DeviceIoControlOutOnly(IOControlCode.StorageGetDeviceNumber, out StorageDeviceNumber number, out var _);
            if (!result)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return number;
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct StorageDeviceNumber
    {
        public FileDevice DeviceType;
        public uint DeviceNumber;
        public uint PartitionNumber;
        public override string ToString()
            => $"{nameof(StorageDeviceNumber)}{{{nameof(DeviceType)}:{DeviceType}, {nameof(DeviceNumber)}:{DeviceNumber}, {nameof(PartitionNumber)}:{PartitionNumber}}}";
    }
}
