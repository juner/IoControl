namespace IoControl.Scanner
{
    public enum Scsiscan
    {
        /// <summary>
        /// SCSISCAN_RESERVED
        /// </summary>
        Reserved = 0x000,
        /// <summary>
        /// SCSISCAN_CMD_CODE
        /// </summary>
        CmdCode = 0x004,
        /// <summary>
        /// SCSISCAN_LOCKDEVICE
        /// </summary>
        Lockdevice = 0x005,
        /// <summary>
        /// SCSISCAN_UNLOCKDEVICE
        /// </summary>
        Unlockdevice = 0x006,
        /// <summary>
        /// SCSISCAN_SET_TIMEOUT
        /// </summary>
        SetTimeout = 0x007,
        /// <summary>
        /// SCSISCAN_GET_INFO
        /// </summary>
        GetInfo = 0x008,
    }
}
