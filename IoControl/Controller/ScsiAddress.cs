using System;
using System.Runtime.InteropServices;

namespace IoControl.Controller
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct ScsiAddress
    {
        public readonly uint Length;
        public readonly byte PortNumber;
        public readonly byte PathId;
        public readonly byte TargetId;
        public readonly byte Lun;
        public ScsiAddress(uint Length, byte PortNumber, byte PathId, byte TargetId, byte Lun)
            => (this.Length, this.PortNumber, this.PathId, this.TargetId, this.Lun) = (Length, PortNumber, PathId, TargetId, Lun);
        public void Deconstruct(out uint Length, out byte PortNumber, out byte PathId, out byte TargetId, out byte Lun)
            => (Length, PortNumber, PathId, TargetId, Lun) = (this.Length, this.PortNumber, this.PathId, this.TargetId, this.Lun);
        public override string ToString()
            => $"{nameof(ScsiAddress)}{{{nameof(Length)}:{Length}, {nameof(PortNumber)}:{PortNumber}, {nameof(PathId)},{PathId}, {nameof(TargetId)}:{TargetId}, {nameof(Lun)}:{Lun}}}";
    }
    public enum SrbStatusValue : byte {

    }
    public static class SrbStatusValueExtensions {
        public static SrbStatusValue ToValue(this SrbStatus Status, SrbStatusFlags Flags = default) => (SrbStatusValue)((byte)Status | (byte)Flags);
        public static SrbStatus GetStatus(this SrbStatusValue Value) => (SrbStatus)(((byte)Value) & ~(byte)(SrbStatusFlags.QueueFrozen | SrbStatusFlags.AutosenseValid));
        public static SrbStatusFlags GetFlags(this SrbStatusValue Value) => (SrbStatusFlags)(((byte)Value) & (byte)(SrbStatusFlags.QueueFrozen | SrbStatusFlags.AutosenseValid));
        public static bool IsQueueFrozen(this SrbStatusValue Value) => (((byte)Value) & ((byte)SrbStatusFlags.QueueFrozen)) == ((byte)SrbStatusFlags.QueueFrozen);
        public static bool IsAutosenseValid(this SrbStatusValue Value) => (((byte)Value) & ((byte)SrbStatusFlags.AutosenseValid)) == ((byte)SrbStatusFlags.AutosenseValid);
        public static void Deconstruct(this SrbStatusValue Value, out SrbStatus Status, out SrbStatusFlags Flags)
            => (Status, Flags) = (GetStatus(Value), GetFlags(Value));
    }
    [Flags]
    public enum SrbStatusFlags : byte{
        /// <summary>
        /// SRB_STATUS_QUEUE_FROZEN
        /// </summary>
        QueueFrozen = 0x40,
        /// <summary>
        /// SRB_STATUS_AUTOSENSE_VALID
        /// </summary>
        AutosenseValid = 0x80,
    }
    public enum SrbStatus : byte {
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
    public readonly struct _SCSISCAN_CMD
    {

        public uint Reserved1;

        public uint Size;

        public SrbFlags SrbFlags;

        public byte CdbLength;

        public byte SenseLength;

        public byte Reserved2;

        public byte Reserved3;

        public uint TransferLength;

        UCHAR Cdb[16];

        PUCHAR pSrbStatus;

        PUCHAR pSenseBuffer;

    }
}
