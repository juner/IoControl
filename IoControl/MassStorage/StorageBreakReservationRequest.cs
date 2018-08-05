using System.Runtime.InteropServices;
namespace IoControl.MassStorage
{
    /// <summary>
    /// STORAGE_BREAK_RESERVATION_REQUEST structure 
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddstor/ns-ntddstor-storage_break_reservation_request
    /// Break reservation is sent to the Adapter/FDO with the given lun information.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly struct StorageBreakReservationRequest
    {
        /// <summary>
        /// Contains the length of this structure in bytes.
        /// </summary>
        public readonly uint Length;
        /// <summary>
        /// Reserved. Do not use.
        /// </summary>
        private readonly byte _unused;
        /// <summary>
        /// Indicates the number of the bus to be reset.
        /// </summary>
        public readonly byte PathId;
        /// <summary>
        /// Contains the number of the target device.
        /// </summary>
        public readonly byte TargetId;
        /// <summary>
        /// Contains the logical unit number.
        /// </summary>
        public readonly byte Lun;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PathId"></param>
        /// <param name="TargetId"></param>
        /// <param name="Lun"></param>
        public StorageBreakReservationRequest(byte PathId, byte TargetId, byte Lun)
            => (Length, _unused, this.PathId, this.TargetId, this.Lun) = ((uint)Marshal.SizeOf<StorageBreakReservationRequest>(), 0, PathId, TargetId, Lun);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PathId"></param>
        /// <param name="TargetId"></param>
        /// <param name="Lun"></param>
        public void Deconstruct(out byte PathId, out byte TargetId, out byte Lun)
            => (PathId, TargetId, Lun) = (this.PathId, this.TargetId, this.Lun);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="op"></param>
        public static implicit operator StorageBreakReservationRequest(in (byte PathId, byte TargetId, byte Lun) op) => new StorageBreakReservationRequest(op.PathId, op.TargetId, op.Lun);
    }
}
