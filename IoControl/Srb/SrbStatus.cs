namespace IoControl.Srb
{
    public enum SrbStatus : byte
    {
        /// <summary>
        /// SRB_STATUS_PENDING
        /// </summary>
        Pending = 0x00,
        /// <summary>
        /// SRB_STATUS_SUCCESS
        /// </summary>
        Success = 0x01,
        /// <summary>
        /// SRB_STATUS_ABORTED
        /// </summary>
        Aborted = 0x02,
        /// <summary>
        /// SRB_STATUS_ABORT_FAILED
        /// </summary>
        AbortFailed = 0x03,
        /// <summary>
        /// SRB_STATUS_ERROR
        /// </summary>
        Error = 0x04,
        /// <summary>
        /// SRB_STATUS_BUSY
        /// </summary>
        Busy = 0x05,
        /// <summary>
        /// SRB_STATUS_INVALID_REQUEST
        /// </summary>
        InvalidRequest = 0x06,
        /// <summary>
        /// SRB_STATUS_INVALID_PATH_ID
        /// </summary>
        InvalidPathId = 0x07,
        /// <summary>
        /// SRB_STATUS_NO_DEVICE
        /// </summary>
        NoDevice = 0x08,
        /// <summary>
        /// SRB_STATUS_TIMEOUT
        /// </summary>
        Timeout = 0x09,
        /// <summary>
        /// SRB_STATUS_SELECTION_TIMEOUT
        /// </summary>
        SelectionTimeout = 0x0A,
        /// <summary>
        /// SRB_STATUS_COMMAND_TIMEOUT
        /// </summary>
        CommandTimeout = 0x0B,
        /// <summary>
        /// SRB_STATUS_MESSAGE_REJECTED
        /// </summary>
        MessageRejected = 0x0D,
        /// <summary>
        /// SRB_STATUS_BUS_RESET
        /// </summary>
        BusReset = 0x0E,
        /// <summary>
        /// SRB_STATUS_PARITY_ERROR
        /// </summary>
        ParityError = 0x0F,
        /// <summary>
        /// SRB_STATUS_REQUEST_SENSE_FAILED
        /// </summary>
        RequestSenseFailed = 0x10,
        /// <summary>
        /// SRB_STATUS_NO_HBA
        /// </summary>
        NoHba = 0x11,
        /// <summary>
        /// SRB_STATUS_DATA_OVERRUN
        /// </summary>
        DataOverrun = 0x12,
        /// <summary>
        /// SRB_STATUS_UNEXPECTED_BUS_FREE
        /// </summary>
        UnexpectedBusFree = 0x13,
        /// <summary>
        /// SRB_STATUS_PHASE_SEQUENCE_FAILURE
        /// </summary>
        PhaseSequenceFailure = 0x14,
        /// <summary>
        /// SRB_STATUS_BAD_SRB_BLOCK_LENGTH
        /// </summary>
        BadSrbBlockLength = 0x15,
        /// <summary>
        /// SRB_STATUS_REQUEST_FLUSHED
        /// </summary>
        RequestFlushed = 0x16,
        /// <summary>
        /// SRB_STATUS_INVALID_LUN
        /// </summary>
        InvalidLun = 0x20,
        /// <summary>
        /// SRB_STATUS_INVALID_TARGET_ID
        /// </summary>
        InvalidTargetId = 0x21,
        /// <summary>
        /// SRB_STATUS_BAD_FUNCTION
        /// </summary>
        BadFunction = 0x22,
        /// <summary>
        /// SRB_STATUS_ERROR_RECOVERY
        /// </summary>
        ErrorRecovery = 0x23,
    }
}
