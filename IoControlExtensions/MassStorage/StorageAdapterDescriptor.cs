using System;
using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    /// <summary>
    /// STORAGE_ADAPTER_DESCRIPTOR
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddstor/ns-ntddstor-_storage_adapter_descriptor
    /// </summary>
    /// <remarks>
    /// Storage class drivers issue a device-control request with the I/O control code <see cref="IOControlCode.StorageQueryProperty"/> to retrieve this structure, which contains configuration information from the HBA for data transfer operations. The structure can be retrieved either from the device object for the bus or from a functional device object (FDO), which forwards the request to the underlying bus.
    /// If excessive protocol errors occur on an HBA that supports synchronous transfers(<see cref="StorageAdapterDescriptor.AcceleratedTransfer"/> is <see cref="true"/>), the storage class driver can disable synchronous transfers by setting SRB_FLAGS_DISABLE_SYNCH_TRANSFER in SRBs.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct StorageAdapterDescriptor : IStorageDescriptor
    {
        uint IStorageDescriptor.Size => Size;
        uint IStorageDescriptor.Version => Version;
        /// <summary>
        /// Contains the version of the structure STORAGE_ADAPTER_DESCRIPTOR. The value of this member will change as members are added to the structure.
        /// </summary>
        public readonly uint Version;
        /// <summary>
        /// Specifies the total size of the descriptor, in bytes.
        /// </summary>
        public readonly uint Size;
        /// <summary>
        /// Specifies the maximum number of bytes the host bus adapter (HBA) can transfer in a single operation.
        /// </summary>
        public readonly uint MaximumTransferLength;
        /// <summary>
        /// Specifies the maximum number of discontinuous physical pages the HBA can manage in a single transfer (in other words, the extent of its scatter/gather support).
        /// </summary>
        public readonly uint MaximumPhysicalPages;
        /// <summary>
        /// Specifies the HBA's alignment requirements for transfers. A storage class driver sets the AlignmentRequirement field in its device objects to this value. The alignment mask indicates alignment restrictions for buffers required by the HBA for transfer operations. The valid mask values are 0 (byte aligned), 1 (word aligned), 3 (DWORD aligned), and 7 (double DWORD aligned).
        /// </summary>
        public readonly uint AlignmentMask;
        /// <summary>
        /// Indicates when TRUE that the HBA uses Programmed Input/Output (PIO) and requires the use of system-space virtual addresses mapped to physical memory for data buffers. When FALSE, the HBA does not use PIO.
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public readonly bool AdapterUsesPio;
        /// <summary>
        /// Indicates when TRUE that the HBA scans down for BIOS devices, that is, the HBA begins scanning with the highest device number rather than the lowest. When FALSE, the HBA begins scanning with the lowest device number. This member is reserved for legacy miniport drivers.
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public readonly bool AdapterScansDown;
        /// <summary>
        /// Indicates when TRUE that the HBA supports SCSI-tagged queuing and/or per-logical-unit internal queues, or the non-SCSI equivalent. When FALSE, the HBA neither supports SCSI-tagged queuing nor per-logical-unit internal queues.
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public readonly bool CommandQueueing;
        /// <summary>
        /// Indicates when TRUE that the HBA supports synchronous transfers as a way of speeding up I/O. When FALSE, the HBA does not support synchronous transfers as a way of speeding up I/O.
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public readonly bool AcceleratedTransfer;
        /// <summary>
        /// If this member is TRUE, the storage adapter supports synchronous transfers as a way of speeding up I/O. When this member is FALSE, the storage adapter does not support synchronous transfers as a way of speeding up I/O.
        /// </summary>
        public readonly StorageBusType BusType;
        /// <summary>
        /// Specifies the major version number, if any, of the HBA.
        /// </summary>
        public readonly ushort BusMajorVersion;
        /// <summary>
        /// Specifies the minor version number, if any, of the HBA.
        /// </summary>
        public readonly ushort BusMinorVersion;
        /// <summary>
        /// Specifies the SCSI request block (SRB) type used by the HBA.
        /// </summary>
        public readonly SrbType SrbType;
        /// <summary>
        /// Specifies the address type of the HBA.
        /// </summary>
        public readonly StorageAddressType AddressType;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MaximumTransferLength"></param>
        /// <param name="MaximumPhysicalPages"></param>
        /// <param name="AlignmentMask"></param>
        /// <param name="AdapterUsesPio"></param>
        /// <param name="AdapterScansDown"></param>
        /// <param name="CommandQueueing"></param>
        /// <param name="AcceleratedTransfer"></param>
        /// <param name="BusType"></param>
        /// <param name="BusMajorVersion"></param>
        /// <param name="BusMinorVersion"></param>
        /// <param name="SrbType"></param>
        /// <param name="AddressType"></param>
        public StorageAdapterDescriptor(uint MaximumTransferLength, uint MaximumPhysicalPages, uint AlignmentMask, bool AdapterUsesPio, bool AdapterScansDown, bool CommandQueueing, bool AcceleratedTransfer, StorageBusType BusType, ushort BusMajorVersion, ushort BusMinorVersion, SrbType SrbType, StorageAddressType AddressType)
            => (Version, Size, this.MaximumTransferLength, this.MaximumPhysicalPages, this.AlignmentMask, this.AdapterUsesPio, this.AdapterScansDown, this.CommandQueueing, this.AcceleratedTransfer, this.BusType, this.BusMajorVersion, this.BusMinorVersion, this.SrbType, this.AddressType)
                = ((uint)Marshal.SizeOf<StorageAdapterDescriptor>(), (uint)Marshal.SizeOf<StorageAdapterDescriptor>(), MaximumTransferLength, MaximumPhysicalPages, AlignmentMask, AdapterUsesPio, AdapterScansDown, CommandQueueing, AcceleratedTransfer, BusType, BusMajorVersion, BusMinorVersion, SrbType, AddressType);
        /// <summary>
        /// Ptr to Structure
        /// </summary>
        /// <param name="IntPtr"></param>
        /// <param name="Size"></param>
        public StorageAdapterDescriptor(IntPtr IntPtr, uint Size) => this = (StorageAdapterDescriptor)Marshal.PtrToStructure(IntPtr, typeof(StorageAdapterDescriptor));
        public override string ToString()
            => $"{nameof(StorageAdapterDescriptor)}{{{nameof(Version)}:{Version}, {nameof(Size)}:{Size}, {nameof(MaximumTransferLength)}:{MaximumTransferLength}, {nameof(MaximumPhysicalPages)}:{MaximumPhysicalPages}, {nameof(AlignmentMask)}:{AlignmentMask}, {nameof(AdapterUsesPio)}:{AdapterUsesPio}, {nameof(AdapterScansDown)}:{AdapterScansDown}, {nameof(CommandQueueing)}:{CommandQueueing}, {nameof(AcceleratedTransfer)}:{AcceleratedTransfer}, {nameof(BusType)}:{BusType}, {nameof(BusMajorVersion)}:{BusMajorVersion}, {nameof(BusMinorVersion)}:{BusMinorVersion}, {nameof(SrbType)}:{SrbType}, {nameof(AddressType)}:{AddressType}}}";
    }

}
