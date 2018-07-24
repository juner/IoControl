using System;
using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
    public struct PartitionInformationGpt
    {
        public Guid PartitionType;
        public Guid PartitionId;
        [MarshalAs(UnmanagedType.U8)]
        public EFIPartitionAttributes Attributes;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public string Name;
        public override string ToString() => $"{nameof(PartitionInformationGpt)}:{{" +
            $" {nameof(PartitionType)}:{PartitionType}" +
            $", {nameof(PartitionId)}:{PartitionId}" +
            $", {nameof(Attributes)}:{Attributes}" +
            $", {nameof(Name)}:{Name}" +
            $"}}";
    }
}
