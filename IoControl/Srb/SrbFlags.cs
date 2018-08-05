namespace IoControl.Srb
{
    /// <summary>
    /// SCSISCAN_CMD.SrbFlags
    /// </summary>
    public enum SrbFlags : uint{
        /// <summary>
        /// SRB_FLAGS_NO_DATA_TRANSFER
        /// </summary>
        NoDataTransfer = 0x00000000,
        /// <summary>
        /// SRB_FLAGS_QUEUE_ACTION_ENABLE
        /// </summary>
        QueueActionEnable = 0x00000002,
        /// <summary>
        /// SRB_FLAGS_DISABLE_DISCONNECT
        /// </summary>
        DisableDisconnect =      0x00000004,
        /// <summary>
        /// SRB_FLAGS_DISABLE_SYNCH_TRANSFER
        /// </summary>
        DisableSynchTransfer = 0x00000008,
        DisableAutosense = 0x00000020,
        DataIn = 0x00000040,
        DataOut = 0x00000080,
        /// <summary>
        /// SRB_FLAGS_UNSPECIFIED_DIRECTION
        /// </summary>
        UnspecifiedDirection = DataIn | DataOut,
        /// <summary>
        /// SRB_FLAGS_NO_QUEUE_FREEZE
        /// </summary>
        NoQueueFreeze = 0x00000100,
        /// <summary>
        /// SRB_FLAGS_ADAPTER_CACHE_ENABLE
        /// </summary>
        AdapterCacheEnable = 0x00000200,
        /// <summary>
        /// SRB_FLAGS_FREE_SENSE_BUFFER
        /// </summary>
        FreeSenseBuffer = 0x00000400,
        /// <summary>
        /// SRB_FLAGS_IS_ACTIVE
        /// </summary>
        IsActive = 0x00010000,
        /// <summary>
        /// SRB_FLAGS_ALLOCATED_FROM_ZONE
        /// </summary>
        AllocatedFromZone = 0x00020000,
        /// <summary>
        /// SRB_FLAGS_SGLIST_FROM_POOL
        /// </summary>
        SglistFromPool = 0x00040000,
        /// <summary>
        /// SRB_FLAGS_BYPASS_LOCKED_QUEUE
        /// </summary>
        BypassLockedQueue = 0x00080000,
        /// <summary>
        /// SRB_FLAGS_NO_KEEP_AWAKE
        /// </summary>
        NoKeepAwake = 0x00100000,
        /// <summary>
        /// SRB_FLAGS_PORT_DRIVER_ALLOCSENSE
        /// </summary>
        PortDriverAllocsense = 0x00200000,
        /// <summary>
        /// SRB_FLAGS_PORT_DRIVER_SENSEHASPORT
        /// </summary>
        PortDriverSensehasport = 0x00400000,
        /// <summary>
        /// SRB_FLAGS_DONT_START_NEXT_PACKET
        /// </summary>
        DontStartNextPacket = 0x00800000,
        /// <summary>
        /// SRB_FLAGS_PORT_DRIVER_RESERVED
        /// </summary>
        PortDriverReserved = 0x0F000000,
        /// <summary>
        /// SRB_FLAGS_CLASS_DRIVER_RESERVED
        /// </summary>
        ClassDriverReserved = 0xF0000000,
    }
}
