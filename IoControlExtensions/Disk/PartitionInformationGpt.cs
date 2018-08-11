using System;
using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    /// <summary>
    /// PARTITION_INFORMATION_GPT structure ( https://msdn.microsoft.com/library/windows/hardware/ff563763 )
    /// <see cref="PartitionInformationGpt"/> contains information for a GUID Partition Table partition that is not held in common with a Master Boot Record partition.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
    public readonly struct PartitionInformationGpt
    {
        /// <summary>
        /// Specifies a GUID that uniquely identifies the partition type. The GUID data type is described on the Using GUIDs in Drivers reference page. ( https://docs.microsoft.com/en-us/windows-hardware/drivers/kernel/using-guids-in-drivers )
        /// </summary>
        public readonly Guid PartitionType;
        /// <summary>
        /// Specifies a GUID that uniquely identifies the partition. The GUID data type is described on the Using GUIDs in Drivers reference page.  ( https://docs.microsoft.com/en-us/windows-hardware/drivers/kernel/using-guids-in-drivers )
        /// </summary>
        public readonly Guid PartitionId;
        /// <summary>
        /// Specifies the partition entry attributes used for diagnostics, recovery tools, and other firmware essential to the operation of the device. See Intel's Extensible Firmware Interface specification for further information.
        /// </summary>
        [MarshalAs(UnmanagedType.U8)]
        public readonly EFIPartitionAttributes Attributes;
        /// <summary>
        /// Specifies the partition name in Unicode.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public readonly string Name;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PartitionType"></param>
        /// <param name="PartitionId"></param>
        /// <param name="Attributes"></param>
        /// <param name="Name"></param>
        public PartitionInformationGpt(Guid PartitionType, Guid PartitionId, EFIPartitionAttributes Attributes, string Name)
            => (this.PartitionType, this.PartitionId, this.Attributes, this.Name)
            = (PartitionType, PartitionId, Attributes, Name);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PartitionType"></param>
        /// <param name="PartitionId"></param>
        /// <param name="Attributes"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public PartitionInformationGpt Set(Guid? PartitionType = null, Guid? PartitionId = null, EFIPartitionAttributes? Attributes = null, string Name = null)
            => new PartitionInformationGpt(PartitionType ?? this.PartitionType, PartitionId ?? this.PartitionId, Attributes ?? this.Attributes, Name ?? this.Name);
        public override string ToString() => $"{nameof(PartitionInformationGpt)}:{{" +
            $" {nameof(PartitionType)}:{PartitionType}" +
            $", {nameof(PartitionId)}:{PartitionId}" +
            $", {nameof(Attributes)}:{Attributes}" +
            $", {nameof(Name)}:{Name}" +
            $"}}";
    }
}
