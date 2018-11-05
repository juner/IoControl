namespace IoControl.Controller
{
    public enum CommandType
    {

        CmdTypeUnknown = 0,

        CmdTypePhysicalDrive,

        CmdTypeScsiMiniport,

        CmdTypeSiliconImage,
        /// <summary>
        ///  SAT = SCSI_ATA_TRANSLATION
        /// </summary>
        CmdTypeSat,

        CmdTypeSunplus,

        CmdTypeIoData,

        CmdTypeLogitec1,

        CmdTypeLogitec2,

        CmdTypeJmicron,

        CmdTypeCypress,

        CmdTypeProlific,          // Not imprement

        CmdTypeCsmi,              // CSMI = Common Storage Management Interface

        CmdTypeCsmiPhysicalDrive, // CSMI = Common Storage Management Interface 

        CmdTypeWmi,

        CmdTypeNvmeSamsung,

        CmdTypeNvmeIntel,

        CmdTypeNvmeStorageQuery,

        CmdTypeNvmeJmicron,

        CmdTypeNvmeAsmedia,

        CmdTypeDebug
    };
}
