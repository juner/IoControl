using static System.Linq.Enumerable;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace IoControl.Controller
{
    [StructLayout(LayoutKind.Sequential,Pack =1)]
    public readonly struct ScsiAdapterBusInfo{
        public readonly byte NumberOfBuses;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst =1)]
        readonly ScsiBusData[] _BusData;
        public ScsiBusData[] BusData => (_BusData ?? Empty<ScsiBusData>()).Concat(Repeat<ScsiBusData>(default, NumberOfBuses)).Take(NumberOfBuses).ToArray();
        ScsiAdapterBusInfo(byte NumberOfBuses, ScsiBusData[] BusData)
            => (this.NumberOfBuses, _BusData)
            = (NumberOfBuses, ((BusData ?? Empty<ScsiBusData>()) is IEnumerable<ScsiBusData> data ? data : Repeat<ScsiBusData>(default, 1)).ToArray());
        public ScsiAdapterBusInfo(ScsiBusData[] BusData) : this((byte)BusData.Length, BusData) { }
        public static implicit operator ScsiBusData[](ScsiAdapterBusInfo info)=> info.BusData;
        public static implicit operator ScsiAdapterBusInfo(ScsiBusData[] data) => new ScsiAdapterBusInfo(data);
    }
}
