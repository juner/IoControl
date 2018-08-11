namespace IoControl.MassStorage
{
    /// <summary>
    /// _STORAGE_BUS_RESET_REQUEST structure
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddstor/ns-ntddstor-_storage_bus_reset_request
    /// Define the structures for scsi resets
    /// </summary>
    public readonly struct StorageBusResetRequest {
        /// <summary>
        /// Indicates the number of the bus to be reset.
        /// </summary>
        public readonly byte PathId;
        public StorageBusResetRequest(byte PathId) => this.PathId = PathId;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PathId"></param>
        public static explicit operator StorageBusResetRequest(in byte PathId) => new StorageBusResetRequest(PathId);
        public static explicit operator byte(in StorageBusResetRequest request) => request.PathId;
    }
}
