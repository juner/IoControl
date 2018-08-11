using System.Linq;
using System.Runtime.InteropServices;
namespace IoControl.MassStorage
{
    internal struct StorageDeviceNumbers
    {
        public readonly uint NumberOfDevices;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public readonly StorageDeviceNumber[] Devices;
        public StorageDeviceNumbers(StorageDeviceNumber[] Devices)
            => (NumberOfDevices, this.Devices) = ((uint)(Devices?.Length ?? 0), (Devices?.Length ?? 0) == 0 ? new StorageDeviceNumber[1] : Devices);
        public static implicit operator StorageDeviceNumber[](StorageDeviceNumbers numbers) => numbers.Devices.Take((int)numbers.NumberOfDevices).ToArray();
        public static implicit operator StorageDeviceNumbers(StorageDeviceNumber[] numbers) => new StorageDeviceNumbers(numbers);
    }
}
