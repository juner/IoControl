namespace IoControl.Disk
{
    /// <summary>
    /// IDEREGS structure
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ns-ntdddisk-_ideregs
    /// </summary>
    public readonly struct Ideregs
    {
        public readonly byte FeaturesReg;
        public readonly byte SectorCountReg;
        public readonly byte SectorNumberReg;
        public readonly byte CylLowReg;
        public readonly byte CylHighReg;
        public readonly byte DriveHeadReg;
        public readonly byte CommandReg;
        public readonly byte Reserved;
        public Ideregs(byte FeaturesReg = default, byte SectorCountReg = default, byte SectorNumberReg = default, byte CylLowReg = default, byte CylHighReg = default, byte DriveHeadReg = default, byte CommandReg = default, byte Reserved = default)
            => (this.FeaturesReg, this.SectorCountReg, this.SectorNumberReg, this.CylLowReg, this.CylHighReg, this.DriveHeadReg, this.CommandReg, this.Reserved)
            = (FeaturesReg, SectorCountReg, SectorNumberReg, CylLowReg, CylHighReg, DriveHeadReg, CommandReg, Reserved);
        public Ideregs Set(byte? FeaturesReg = null, byte? SectorCountReg = null, byte? SectorNumberReg = null, byte? CylLowReg = null, byte? CylHighReg = null, byte? DriveHeadReg = null, byte? CommandReg = null, byte? Reserved = null)
            => FeaturesReg == null && SectorCountReg == null && SectorNumberReg == null && CylLowReg == null && CylHighReg == null && DriveHeadReg == null && CommandReg == null && Reserved == null ? this :
            new Ideregs(FeaturesReg ?? this.FeaturesReg, SectorCountReg ?? this.SectorCountReg, SectorNumberReg ?? this.SectorNumberReg, CylLowReg ?? this.CylLowReg, CylHighReg ?? this.CylHighReg, DriveHeadReg ?? this.DriveHeadReg, CommandReg ?? this.CommandReg, Reserved ?? this.Reserved);
    }
}
