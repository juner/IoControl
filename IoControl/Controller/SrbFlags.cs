namespace IoControl.Controller
{
    /// <summary>
    /// SCSISCAN_CMD.SrbFlags
    /// </summary>
    public enum SrbFlags : uint{
        NoDataTransfer = 0x00000000,
        DisableSynchTransfer = 0x00000008,
        DisableAutosense = 0x00000020,
        DataIn = 0x00000040,
        DataOut = 0x00000080,
    }
}
