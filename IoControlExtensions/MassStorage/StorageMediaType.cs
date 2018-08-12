namespace IoControl.MassStorage
{
    /// <summary>
    /// STORAGE_MEDIA_TYPE
    /// </summary>
    public enum StorageMediaType : uint
    {

        //

        // Following are defined in ntdddisk.h in the MEDIA_TYPE enum

        //

        Unknown = 0x00,                // Format is unknown

        F5_1Pt2_512,            // 5.25", 1.2MB,  512 bytes/sector

        F3_1Pt44_512,           // 3.5",  1.44MB, 512 bytes/sector

        F3_2Pt88_512,           // 3.5",  2.88MB, 512 bytes/sector

        F3_20Pt8_512,           // 3.5",  20.8MB, 512 bytes/sector

        F3_720_512,             // 3.5",  720KB,  512 bytes/sector

        F5_360_512,             // 5.25", 360KB,  512 bytes/sector

        F5_320_512,             // 5.25", 320KB,  512 bytes/sector

        F5_320_1024,            // 5.25", 320KB,  1024 bytes/sector

        F5_180_512,             // 5.25", 180KB,  512 bytes/sector

        F5_160_512,             // 5.25", 160KB,  512 bytes/sector

        RemovableMedia,         // Removable media other than floppy

        FixedMedia,             // Fixed hard disk media

        F3_120M_512,            // 3.5", 120M Floppy

        F3_640_512,             // 3.5" ,  640KB,  512 bytes/sector

        F5_640_512,             // 5.25",  640KB,  512 bytes/sector

        F5_720_512,             // 5.25",  720KB,  512 bytes/sector

        F3_1Pt2_512,            // 3.5" ,  1.2Mb,  512 bytes/sector

        F3_1Pt23_1024,          // 3.5" ,  1.23Mb, 1024 bytes/sector

        F5_1Pt23_1024,          // 5.25",  1.23MB, 1024 bytes/sector

        F3_128Mb_512,           // 3.5" MO 128Mb   512 bytes/sector

        F3_230Mb_512,           // 3.5" MO 230Mb   512 bytes/sector

        F8_256_128,             // 8",     256KB,  128 bytes/sector

        F3_200Mb_512,           // 3.5",   200M Floppy (HiFD)
        
        DDS_4mm = 0x20,            // Tape - DAT DDS1,2,... (all vendors)

        MiniQic,                   // Tape - miniQIC Tape

        Travan,                    // Tape - Travan TR-1,2,3,...

        QIC,                       // Tape - QIC

        MP_8mm,                    // Tape - 8mm Exabyte Metal Particle

        AME_8mm,                   // Tape - 8mm Exabyte Advanced Metal Evap

        AIT1_8mm,                  // Tape - 8mm Sony AIT

        DLT,                       // Tape - DLT Compact IIIxt, IV

        NCTP,                      // Tape - Philips NCTP

        IBM_3480,                  // Tape - IBM 3480

        IBM_3490E,                 // Tape - IBM 3490E

        IBM_Magstar_3590,          // Tape - IBM Magstar 3590

        IBM_Magstar_MP,            // Tape - IBM Magstar MP

        STK_DATA_D3,               // Tape - STK Data D3

        SONY_DTF,                  // Tape - Sony DTF

        DV_6mm,                    // Tape - 6mm Digital Video

        DMI,                       // Tape - Exabyte DMI and compatibles

        SONY_D2,                   // Tape - Sony D2S and D2L

        CLEANER_CARTRIDGE,         // Cleaner - All Drive types that support Drive Cleaners

        CD_ROM,                    // Opt_Disk - CD

        CD_R,                      // Opt_Disk - CD-Recordable (Write Once)

        CD_RW,                     // Opt_Disk - CD-Rewriteable

        DVD_ROM,                   // Opt_Disk - DVD-ROM

        DVD_R,                     // Opt_Disk - DVD-Recordable (Write Once)

        DVD_RW,                    // Opt_Disk - DVD-Rewriteable

        MO_3_RW,                   // Opt_Disk - 3.5" Rewriteable MO Disk

        MO_5_WO,                   // Opt_Disk - MO 5.25" Write Once

        MO_5_RW,                   // Opt_Disk - MO 5.25" Rewriteable (not LIMDOW)

        MO_5_LIMDOW,               // Opt_Disk - MO 5.25" Rewriteable (LIMDOW)

        PC_5_WO,                   // Opt_Disk - Phase Change 5.25" Write Once Optical

        PC_5_RW,                   // Opt_Disk - Phase Change 5.25" Rewriteable

        PD_5_RW,                   // Opt_Disk - PhaseChange Dual Rewriteable

        ABL_5_WO,                  // Opt_Disk - Ablative 5.25" Write Once Optical

        PINNACLE_APEX_5_RW,        // Opt_Disk - Pinnacle Apex 4.6GB Rewriteable Optical

        SONY_12_WO,                // Opt_Disk - Sony 12" Write Once

        PHILIPS_12_WO,             // Opt_Disk - Philips/LMS 12" Write Once

        HITACHI_12_WO,             // Opt_Disk - Hitachi 12" Write Once

        CYGNET_12_WO,              // Opt_Disk - Cygnet/ATG 12" Write Once

        KODAK_14_WO,               // Opt_Disk - Kodak 14" Write Once

        MO_NFR_525,                // Opt_Disk - Near Field Recording (Terastor)

        NIKON_12_RW,               // Opt_Disk - Nikon 12" Rewriteable

        IOMEGA_ZIP,                // Mag_Disk - Iomega Zip

        IOMEGA_JAZ,                // Mag_Disk - Iomega Jaz

        SYQUEST_EZ135,             // Mag_Disk - Syquest EZ135

        SYQUEST_EZFLYER,           // Mag_Disk - Syquest EzFlyer

        SYQUEST_SYJET,             // Mag_Disk - Syquest SyJet

        AVATAR_F2,                 // Mag_Disk - 2.5" Floppy

        MP2_8mm,                   // Tape - 8mm Hitachi

        DST_S,                     // Ampex DST Small Tapes

        DST_M,                     // Ampex DST Medium Tapes

        DST_L,                     // Ampex DST Large Tapes

        VXATape_1,                 // Ecrix 8mm Tape

        VXATape_2,                 // Ecrix 8mm Tape

        STK_9840,                  // STK 9840

        LTO_Ultrium,               // IBM, HP, Seagate LTO Ultrium

        LTO_Accelis,               // IBM, HP, Seagate LTO Accelis

        DVD_RAM,                   // Opt_Disk - DVD-RAM

        AIT_8mm,                   // AIT2 or higher

        ADR_1,                     // OnStream ADR Mediatypes

        ADR_2,

        STK_9940,                  // STK 9940

        SAIT,                      // SAIT Tapes

        VXATape                    // VXA (Ecrix 8mm) Tape

    }

} 