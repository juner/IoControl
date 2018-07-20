using System;
using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode, Pack = 4)]
    public struct PartitionInformationGpt
    {
        [FieldOffset(0)]
        public Guid PartitionType;
        [FieldOffset(16)]
        public Guid PartitionId;
        [FieldOffset(32)]
        [MarshalAs(UnmanagedType.U8)]
        public EFIPartitionAttributes Attributes;
        [FieldOffset(40)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public string Name;
        public override string ToString() => $"{nameof(PartitionInformationGpt)}:{{" +
            $" {nameof(PartitionType)}:{PartitionType}" +
            $", {nameof(PartitionId)}:{PartitionId}" +
            $", {nameof(Attributes)}:{Attributes}" +
            $", {nameof(Name)}:{Name}" +
            $"}}";
    }

#endregion
}
