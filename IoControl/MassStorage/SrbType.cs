namespace IoControl.MassStorage
{
    public enum SrbType : byte
    {
        /// <summary>
        /// The HBA uses SCSI request blocks. 
        /// </summary>
        ScsiRequestBlock = 0x00,
        /// <summary>
        /// The HBA uses extended SCSI request blocks. 
        /// </summary>
        StorageRequestBlock = 0x01,
    }

}
