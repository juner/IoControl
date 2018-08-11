using System;
using System.Runtime.InteropServices;

namespace IoControl.Controller
{
    /// <summary>
    /// SCSI_REQUEST_BLOCK
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/srb/ns-srb-_scsi_request_block
    /// </summary>
    public struct ScsiRequestBlock
    {
        /// <summary>
        /// Specifies the size in bytes of this structure.
        /// </summary>
        public ushort Length;
        /// <summary>
        /// Specifies the operation to be performed, which can be one of the following values:
        /// </summary>
        public Srb.SrbFunction Function;
        /// <summary>
        /// Returns the status of the completed request. This member should be set by the miniport driver before it notifies the OS-specific driver that the request has completed by calling ScsiPortNotification with RequestComplete. The value of this member can be one of the following:
        /// </summary>
        public Srb.SrbStatusValue SrbStatus;
        /// <summary>
        /// Returns the SCSI status that was returned by the HBA or target device. If the status is not SUCCESS, the miniport driver should set the SrbStatus member to SRB_STATUS_ERROR.
        /// </summary>
        public byte ScsiStatus;
        /// <summary>
        /// Indicates the SCSI port or bus for the request. This value is zero-based.
        /// </summary>
        public byte PathId;
        /// <summary>
        /// Indicates the target controller or device on the bus.
        /// </summary>
        public byte TargetId;
        /// <summary>
        /// Indicates the logical unit number of the device.
        /// </summary>
        public byte Lun;
        /// <summary>
        /// Contains the queue-tag value assigned by the OS-specific port driver. If this member is used for tagged queuing, the HBA supports internal queuing of requests to LUs and the miniport driver set TaggedQueueing to TRUE in the PORT_CONFIGURATION_INFORMATION for this HBA.
        /// </summary>
        public byte QueueTag;
        /// <summary>
        /// Indicates the tagged-queuing message to be used when the <see cref="Srb.SrbFlags.QueueActionEnable"/> flag is set. The value can be one of the following: SRB_SIMPLE_TAG_REQUEST, SRB_HEAD_OF_QUEUE_TAG_REQUEST, or SRB_ORDERED_QUEUE_TAG_REQUEST, as defined according to the SCSI specification.
        /// </summary>
        public byte QueueAction;
        /// <summary>
        /// Indicates the size in bytes of the SCSI-2 or later command descriptor block.
        /// </summary>
        public byte CdbLength;
        /// <summary>
        /// Indicates the size in bytes of the request-sense buffer. If an underrun occurs, the miniport driver must update this member to the number of bytes actually transferred.
        /// </summary>
        public byte SenseInfoBufferLength;
        /// <summary>
        /// Indicates various parameters and options about the request. SrbFlags is read-only, except when <see cref="Srb.SrbFlags.UnspecifiedDirection"> is set and miniport drivers of subordinate DMA adapters are required to update <see cref="Srb.SrbFlags.DataIn"/> or <see cref="Srb.SrbFlags.DataOut"/>. This member can have one or more of the following flags set:
        /// </summary>
        public Srb.SrbFlags SrbFlags;
        /// <summary>
        /// Indicates the size in bytes of the data buffer. If an underrun occurs, the miniport driver must update this member to the number of bytes actually transferred.
        /// </summary>
        public uint DataTransferLength;
        /// <summary>
        /// Indicates the interval in seconds that the request can execute before the OS-specific port driver might consider it timed out. Miniport drivers are not required to time requests because the port driver already does.
        /// </summary>
        public uint TimeOutValue;
        /// <summary>
        /// Points to the data buffer. Miniport drivers should not use this value as a data pointer unless the miniport driver set MapBuffers to TRUE in the <see cref="Srb.QueueAction."/> PORT_CONFIGURATION_INFORMATION for the HBA. In the case of SRB_FUNCTION_IO_CONTROL requests, however, miniport drivers can use this value as a data pointer regardless of the value of MapBuffers.
        /// </summary>
        public IntPtr DataBuffer;
        /// <summary>
        /// Points to the request-sense buffer. A miniport driver is not required to provide request-sense data after a CHECK CONDITION.
        /// </summary>
        public IntPtr SenseInfoBuffer;
        /// <summary>
        /// Indicates the <see cref="ScsiRequestBlock"/> to which this request applies. Only a small subset of requests use a second SRB, for example <see cref="Srb.SrbFunction.AbortCommand"/>.
        /// </summary>
        public IntPtr NextSrb;
        /// <summary>
        /// Points to the IRP for this request. This member is irrelevant to miniport drivers
        /// </summary>
        public IntPtr OriginalRequest;
        /// <summary>
        /// Points to the Srb extension. A miniport driver must not use this member if it set SrbExtensionSize to zero in the SCSI_HW_INITIALIZATION_DATA. The memory at SrbExtension is not initialized by the OS-specific port driver, and the miniport driver-determined data can be accessed directly by the HBA. The corresponding physical address can be obtained by calling ScsiPortGetPhysicalAddress with the SrbExtension pointer.
        /// </summary>
        public IntPtr SrbExtension;
        private uint dummy;
        public uint InternalStatus { get => dummy; set => dummy = value; }
        public uint QueueSortKey { get => dummy; set => dummy = value; }
        public uint LinkTimeoutValue { get => dummy; set => dummy = value; }
        /// <summary>
        /// Reserved.
        /// </summary>
        public uint Reserved;
        /// <summary>
        /// Specifies the SCSI-2 or later command descriptor block to be sent to the target device.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] Cdb;
}
}
