namespace IoControl.Disk
{
    /// <summary>
    /// DETECTION_TYPE Enumeration
    /// </summary>
    public enum DetectionType
    {
        /// <summary>
        /// Indicates that the disk contains neither an INT 13h partition nor an extended INT 13h partition.
        /// </summary>
        None = 0,
        /// <summary>
        /// Indicates that the disk has a standard INT 13h partition.
        /// </summary>
        Int13 = 1,
        /// <summary>
        /// Indicates that the disk contains an extended INT 13 partition.
        /// </summary>
        ExInt13 = 2,
    };
}
