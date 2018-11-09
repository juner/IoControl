using System.Linq;
using System.Runtime.InteropServices;

namespace IoControl.Controller
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly struct SmartAttribute
    {
        public readonly byte Id;
        public readonly ushort StatusFlags;
        public readonly byte CurrentValue;
        public readonly byte WorstValue;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst =6)]
        public readonly byte[] RawValue;
        public readonly byte Reserved;
        public override string ToString()
            => $"{nameof(SmartAttribute)}{{"
            + $"{nameof(Id)}:{Id:###}({Id:X2})"
            + $", {nameof(StatusFlags)}:{StatusFlags:X4}"
            + $", {nameof(CurrentValue)}:{CurrentValue}"
            + $", {nameof(WorstValue)}:{WorstValue}"
            + $", {nameof(RawValue)}:[{string.Join(" ",(RawValue ?? Enumerable.Empty<byte>()).Select(v => $"{v:X2}"))}]"
            + $", {nameof(Reserved)}:{Reserved}"
            + $"}}";
    };
}
