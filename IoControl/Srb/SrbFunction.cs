namespace IoControl.Srb
{
    public enum SrbFunction :byte {
        /// <summary>
        /// SRB_FUNCTION_EXECUTE_SCSI
        /// A SCSI device I/O request should be executed on the target logical unit.
        /// </summary>
        ExecuteScsi = 0x00,
        /// <summary>
        /// SRB_FUNCTION_ABORT_COMMAND
        /// A SCSIMESS_ABORT message should be sent to cancel the request pointed to by the NextSrb member. If this is a tagged-queue request, a SCSIMESS_ABORT_WITH_TAG message should be used instead. If the indicated request has been completed, this request should be completed normally. Only the SRB Function, PathId, TargetId, Lun, and NextSrb members are valid.
        /// </summary>
        AbortCommand = 0x10,
        /// <summary>
        /// SRB_FUNCTION_RESET_DEVICE
        /// The ScsiPort driver no longer sends this SRB to its miniports. Only Storport miniport drivers use this SRB. The SCSI target controller should be reset using the SCSIMESS_BUS_DEVICE_RESET message. The miniport driver should complete any active requests for the target controller. Only the SRB Function, TargetId, and PathId members are valid.
        /// </summary>
        ResetDevice = 0x13,
        /// <summary>
        /// SRB_FUNCTION_RESET_LOGICAL_UNIT
        /// The logical unit should be reset, if possible. The HBA miniport driver should complete any active requests for the logical unit. Only the Function, PathId, TargetId, and Lun members of the SRB are valid. Storport supports this type of reset, but SCSI port does not. 
        /// </summary>
        ResetLogicalUnit = 0x20,
        /// <summary>
        /// SRB_FUNCTION_RESET_BUS
        /// The SCSI bus should be reset using the SCSIMESS_BUS_DEVICE_RESET message. A miniport driver receives this request only if a given request has timed out and a subsequent request to abort the timed-out request also has timed out. Only the SRB Function and PathId members are valid.
        /// </summary>
        ResetBus = 0x12,
        /// <summary>
        /// SRB_FUNCTION_TERMINATE_IO
        /// A SCSIMESS_TERMINATE_IO_PROCESS message should be sent to cancel the request pointed to by the NextSrb member. If the indicated request has already completed, this request should be completed normally. Only the SRB Function, PathId, TargetId, Lun, and NextSrb members are valid.
        /// </summary>
        TerminateIo = 0x14,
        /// <summary>
        /// SRB_FUNCTION_RELEASE_RECOVERY
        /// A SCSIMESS_RELEASE_RECOVERY message should be sent to the target controller. Only the SRB Function, PathId, TargetId, and Lun members are valid.
        /// </summary>
        ReleaseRecovery = 0x11,
        /// <summary>
        /// SRB_FUNCTION_RECEIVE_EVENT
        /// The HBA should be prepared to receive an asynchronous event notification from the addressed target. The SRB DataBuffer member indicates where the data should be placed.
        /// </summary>
        ReceiveEvent = 0x03,
        /// <summary>
        /// SRB_FUNCTION_SHUTDOWN
        /// The system is being shut down. This request is sent to a miniport driver only if it set CachesData to TRUE in the PORT_CONFIGURATION_INFORMATION for the HBA. Such a miniport driver can receive several of these notifications before all system activity actually stops. However, the last shutdown notification will occur after the last start I/O. Only the SRB Function, PathId, TargetId and Lun members are valid.
        /// </summary>
        Shutdown = 0x07,
        /// <summary>
        /// SRB_FUNCTION_FLUSH
        /// The miniport driver should flush any cached data for the target device. This request is sent to the miniport driver only if it set CachesData to TRUE in the PORT_CONFIGURATION_INFORMATION for the HBA. Only the SRB Function, PathId, TargetId and Lun members are valid.
        /// </summary>
        Flush = 0x08,
        /// <summary>
        /// SRB_FUNCTION_IO_CONTROL
        /// The request is an I/O control request, originating in a user-mode application with a dedicated HBA. The SRB DataBuffer points to an SRB_IO_CONTROL header followed by the data area. The value in DataBuffer can be used by the driver, regardless of the value of MapBuffers. Only the SRB Function, SrbFlags, TimeOutValue, DataBuffer, and DataTransferLength members are valid, along with the SrbExtension member if the miniport driver requested SRB extensions when it initialized. If a miniport driver controls an application-dedicated HBA so it supports this request, the miniport driver should execute the request and notify the OS-specific port driver when the SRB has completed, using the normal mechanism of calls to ScsiPortNotification with RequestComplete and NextRequest.
        /// </summary>
        IoControl = 0x02,
        /// <summary>
        /// SRB_FUNCTION_LOCK_QUEUE
        /// Holds requests queued by the port driver for a particular logical unit, typically while a power request is being processed. Only the SRB Length, Function, SrbFlags, and OriginalRequest members are valid. When the queue is locked, only requests with SrbFlags ORed with SRB_FLAGS_BYPASS_LOCKED_QUEUE will be processed. SCSI miniport drivers do not process SRB_FUNCTION_LOCK_QUEUE requests. 
        /// </summary>
        LockQueue = 0x18,
        /// <summary>
        /// SRB_FUNCTION_UNLOCK_QUEUE
        /// Releases the port driver's queue for a logical unit that was previously locked with SRB_FUNCTION_LOCK_QUEUE. The SrbFlags of the unlock request must be ORed with SRB_FLAGS_BYPASS_LOCKED_QUEUE. Only the SRB Length, Function, SrbFlags, and OriginalRequest members are valid. SCSI miniport drivers do not process SRB_FUNCTION_UNLOCK_QUEUE requests.
        /// </summary>
        UnlockQueue = 0x19,
        /// <summary>
        /// SRB_FUNCTION_DUMP_POINTERS
        /// A request with this function is sent to a Storport miniport driver that is used to control the disk that holds the crash dump data. The request collects information needed from the miniport driver to support crash dump and hibernation. See the MINIPORT_DUMP_POINTERS structure. A physical miniport driver must set the STOR_FEATURE_DUMP_POINTERS flag in the FeatureSupport member of its HW_INITIALIZATION_DATA to receive a request with this function.
        /// </summary>
        DumpPointers = 0x26,
        /// <summary>
        /// SRB_FUNCTION_FREE_DUMP_POINTERS
        /// A request with this function is sent to a Storport miniport driver to release any resources allocated during a previous request for SRB_FUNCTION_DUMP_POINTERS.
        /// </summary>
        FreeDumpPointers = 0x27,
    }
}
