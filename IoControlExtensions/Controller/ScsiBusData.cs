using System.Runtime.InteropServices;

namespace IoControl.Controller
{
    [StructLayout(LayoutKind.Sequential,Pack =1)]
    public readonly struct ScsiBusData
    {
        public readonly byte NumberOfLogicalUnits;
        public readonly byte InitiatorBusId;
        public readonly uint InquiryDataOffset;
        public ScsiBusData(byte NumberOfLogicalUnits = default, byte InitiatorBusId = default, uint InquiryDataOffset = default)
            => (this.NumberOfLogicalUnits, this.InitiatorBusId, this.InquiryDataOffset)
            = (NumberOfLogicalUnits, InitiatorBusId, InquiryDataOffset);
        public ScsiBusData Set(byte? NumberOfLogicalUnits = null, byte? InitiatorBusId = null, uint? InquiryDataOffset = null)
            => NumberOfLogicalUnits == null && InitiatorBusId == null && InquiryDataOffset == null ? this
            : new ScsiBusData(NumberOfLogicalUnits ?? this.NumberOfLogicalUnits, InitiatorBusId ?? this.InitiatorBusId, InquiryDataOffset ?? this.InquiryDataOffset);
    }
}
