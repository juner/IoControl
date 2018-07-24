using System.Linq;
using System.Runtime.InteropServices;

namespace IoControl.Controller
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly struct AtaIdentifyDevice
    {
        public readonly ushort GeneralConfiguration;                  //0
        public readonly ushort LogicalCylinders;                      //1	Obsolete
        public readonly ushort SpecificConfiguration;                 //2
        public readonly ushort LogicalHeads;                          //3 Obsolete
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public readonly ushort[] Retired1;                           //4-5
        public readonly ushort LogicalSectors;                            //6 Obsolete
        public readonly uint ReservedForCompactFlash;              //7-8
        public readonly ushort Retired2;                              //9
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public readonly string SerialNumber;                      //10-19
        public readonly ushort Retired3;                              //20
        public readonly ushort BufferSize;                                //21 Obsolete
        public readonly ushort Obsolute4;                             //22
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public readonly string FirmwareRev;                            //23-26
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public readonly string Model;                             //27-46
        public readonly ushort MaxNumPerInterupt;                     //47
        public readonly ushort Reserved1;                             //48
        public readonly ushort Capabilities1;                         //49
        public readonly ushort Capabilities2;                         //50
        public readonly uint Obsolute5;                                //51-52
        public readonly ushort Field88and7064;                            //53
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public readonly ushort[] Obsolute6;                          //54-58
        public readonly ushort MultSectorStuff;                       //59
        public readonly uint TotalAddressableSectors;              //60-61
        public readonly ushort Obsolute7;                             //62
        public readonly ushort MultiWordDma;                          //63
        public readonly ushort PioMode;                               //64
        public readonly ushort MinMultiwordDmaCycleTime;              //65
        public readonly ushort RecommendedMultiwordDmaCycleTime;      //66
        public readonly ushort MinPioCycleTimewoFlowCtrl;             //67
        public readonly ushort MinPioCycleTimeWithFlowCtrl;           //68
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public readonly ushort[] Reserved2;                          //69-74
        public readonly ushort QueueDepth;                                //75
        public readonly ushort SerialAtaCapabilities;                 //76
        public readonly ushort SerialAtaAdditionalCapabilities;       //77
        public readonly ushort SerialAtaFeaturesSupported;                //78
        public readonly ushort SerialAtaFeaturesEnabled;              //79
        public readonly ushort MajorVersion;                          //80
        public readonly ushort MinorVersion;                          //81
        public readonly ushort CommandSetSupported1;                  //82
        public readonly ushort CommandSetSupported2;                  //83
        public readonly ushort CommandSetSupported3;                  //84
        public readonly ushort CommandSetEnabled1;                        //85
        public readonly ushort CommandSetEnabled2;                        //86
        public readonly ushort CommandSetDefault;                     //87
        public readonly ushort UltraDmaMode;                          //88
        public readonly ushort TimeReqForSecurityErase;               //89
        public readonly ushort TimeReqForEnhancedSecure;              //90
        public readonly ushort CurrentPowerManagement;                    //91
        public readonly ushort MasterPasswordRevision;                    //92
        public readonly ushort HardwareResetResult;                   //93
        public readonly ushort AcoustricManagement;                   //94
        public readonly ushort StreamMinRequestSize;                  //95
        public readonly ushort StreamingTimeDma;                      //96
        public readonly ushort StreamingAccessLatency;                    //97
        public readonly uint StreamingPerformance;                 //98-99
        public readonly ulong MaxUserLba;                               //100-103
        public readonly ushort StremingTimePio;                       //104
        public readonly ushort Reserved3;                             //105
        public readonly ushort SectorSize;                                //106
        public readonly ushort InterSeekDelay;                            //107
        public readonly ushort IeeeOui;                               //108
        public readonly ushort UniqueId3;                             //109
        public readonly ushort UniqueId2;                             //110
        public readonly ushort UniqueId1;                             //111
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public readonly ushort[] Reserved4;                          //112-115
        public readonly ushort Reserved5;                             //116
        public readonly uint WordsPerLogicalSector;                    //117-118
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public readonly ushort[] Reserved6;                          //119-126
        public readonly ushort RemovableMediaStatus;                  //127
        public readonly ushort SecurityStatus;                            //128
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 31)]
        public readonly ushort[] VendorSpecific;                        //129-159
        public readonly ushort CfaPowerMode1;                         //160
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
        public readonly ushort[] ReservedForCompactFlashAssociation; //161-167
        public readonly ushort DeviceNominalFormFactor;               //168
        public readonly ushort DataSetManagement;                     //169
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public readonly ushort[] AdditionalProductIdentifier;            //170-173
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public readonly ushort[] Reserved7;                          //174-175
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 60)]
        public readonly string CurrentMediaSerialNo;              //176-205
        public readonly ushort SctCommandTransport;                   //206
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public readonly ushort[] ReservedForCeAta1;                  //207-208
        public readonly ushort AlignmentOfLogicalBlocks;              //209
        public readonly uint WriteReadVerifySectorCountMode3;      //210-211
        public readonly uint WriteReadVerifySectorCountMode2;      //212-213
        public readonly ushort NvCacheCapabilities;                   //214
        public readonly uint NvCacheSizeLogicalBlocks;             //215-216
        public readonly ushort NominalMediaRotationRate;              //217
        public readonly ushort Reserved8;                             //218
        public readonly ushort NvCacheOptions1;                       //219
        public readonly ushort NvCacheOptions2;                       //220
        public readonly ushort Reserved9;                             //221
        public readonly ushort TransportMajorVersionNumber;           //222
        public readonly ushort TransportMinorVersionNumber;           //223
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public readonly ushort[] ReservedForCeAta2;                 //224-233
        public readonly ushort MinimumBlocksPerDownloadMicrocode;     //234
        public readonly ushort MaximumBlocksPerDownloadMicrocode;     //235
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 19)]
        public readonly ushort[] Reserved10;                            //236-254
        public readonly ushort IntegrityWord;                         //255
        public override string ToString()
            =>  $"{nameof(AtaIdentifyDevice)}{{"
                + $"{nameof(GeneralConfiguration)}: {GeneralConfiguration}"
                + $", {nameof(LogicalCylinders)}:{LogicalCylinders}"
                + $", {nameof(SpecificConfiguration)}:{SpecificConfiguration}"
                + $", {nameof(LogicalHeads)}:{LogicalHeads}"
                + $", {nameof(Retired1)}:[{string.Join(" ", (Retired1 ?? Enumerable.Empty<ushort>()).Select(v => $"{v:X4}"))}]"
                + $", {nameof(LogicalSectors)}:{LogicalSectors}"
                + $", {nameof(ReservedForCompactFlash)}:{ReservedForCompactFlash}"
                + $", {nameof(Retired2)}:{Retired2}"
                + $", {nameof(SerialNumber)}:{SerialNumber}"
                + $", {nameof(Retired3)}:{Retired3}"
                + $", {nameof(BufferSize)}:{BufferSize}"
                + $", {nameof(Obsolute4)}:{Obsolute4}"
                + $", {nameof(FirmwareRev)}:{FirmwareRev}"
                + $", {nameof(Model)}:{Model}"
                + $", {nameof(MaxNumPerInterupt)}:{MaxNumPerInterupt}"
                + $", {nameof(Reserved1)}:{Reserved1}"
                + $", {nameof(Capabilities1)}:{Capabilities1}"
                + $", {nameof(Capabilities2)}:{Capabilities2}"
                + $", {nameof(Obsolute5)}:{Obsolute5}"
                + $", {nameof(Field88and7064)}:{Field88and7064}"
                + $", {nameof(Obsolute6)}:[{string.Join(" ", (Obsolute6 ?? Enumerable.Empty<ushort>()).Select(v => $"{v:X4}"))}]"
                + $", {nameof(MultSectorStuff)}:{MultSectorStuff}"
                + $", {nameof(TotalAddressableSectors)}:{TotalAddressableSectors}"
                + $", {nameof(Obsolute7)}:{Obsolute7}"
                + $", {nameof(MultiWordDma)}:{MultiWordDma}"
                + $", {nameof(PioMode)}:{PioMode}"
                + $", {nameof(MinMultiwordDmaCycleTime)}:{MinMultiwordDmaCycleTime}"
                + $", {nameof(RecommendedMultiwordDmaCycleTime)}:{RecommendedMultiwordDmaCycleTime}"
                + $", {nameof(MinPioCycleTimewoFlowCtrl)}:{MinPioCycleTimewoFlowCtrl}"
                + $", {nameof(MinPioCycleTimeWithFlowCtrl)}:{MinPioCycleTimeWithFlowCtrl}"
                + $", {nameof(Reserved2)}:[{string.Join(" ", (Reserved2 ?? Enumerable.Empty<ushort>()).Select(v => $"{v:X4}"))}]"
                + $", {nameof(QueueDepth)}:{QueueDepth}"
                + $", {nameof(SerialAtaCapabilities)}:{SerialAtaCapabilities}"
                + $", {nameof(SerialAtaAdditionalCapabilities)}:{SerialAtaAdditionalCapabilities}"
                + $", {nameof(SerialAtaFeaturesSupported)}:{SerialAtaFeaturesSupported}"
                + $", {nameof(SerialAtaFeaturesEnabled)}:{SerialAtaFeaturesEnabled}"
                + $", {nameof(MajorVersion)}:{MajorVersion}"
                + $", {nameof(MinorVersion)}:{MinorVersion}"
                + $", {nameof(CommandSetSupported1)}:{CommandSetSupported1}"
                + $", {nameof(CommandSetSupported2)}:{CommandSetSupported2}"
                + $", {nameof(CommandSetSupported3)}:{CommandSetSupported3}"
                + $", {nameof(CommandSetEnabled1)}:{CommandSetEnabled1}"
                + $", {nameof(CommandSetEnabled2)}:{CommandSetEnabled2}"
                + $", {nameof(CommandSetDefault)}:{CommandSetDefault}"
                + $", {nameof(UltraDmaMode)}:{UltraDmaMode}"
                + $", {nameof(TimeReqForSecurityErase)}:{TimeReqForSecurityErase}"
                + $", {nameof(TimeReqForEnhancedSecure)}:{TimeReqForEnhancedSecure}"
                + $", {nameof(CurrentPowerManagement)}:{CurrentPowerManagement}"
                + $", {nameof(MasterPasswordRevision)}:{MasterPasswordRevision}"
                + $", {nameof(HardwareResetResult)}:{HardwareResetResult}"
                + $", {nameof(AcoustricManagement)}:{AcoustricManagement}"
                + $", {nameof(StreamMinRequestSize)}:{StreamMinRequestSize}"
                + $", {nameof(StreamingTimeDma)}:{StreamingTimeDma}"
                + $", {nameof(StreamingAccessLatency)}:{StreamingAccessLatency}"
                + $", {nameof(StreamingPerformance)}:{StreamingPerformance}"
                + $", {nameof(MaxUserLba)}:{MaxUserLba}"
                + $", {nameof(StremingTimePio)}:{StremingTimePio}"
                + $", {nameof(Reserved3)}:{Reserved3}"
                + $", {nameof(SectorSize)}:{SectorSize}"
                + $", {nameof(InterSeekDelay)}:{InterSeekDelay}"
                + $", {nameof(IeeeOui)}:{IeeeOui}"
                + $", {nameof(UniqueId3)}:{UniqueId3}"
                + $", {nameof(UniqueId2)}:{UniqueId2}"
                + $", {nameof(UniqueId1)}:{UniqueId1}"
                + $", {nameof(Reserved4)}:[{string.Join(" ", (Reserved4 ?? Enumerable.Empty<ushort>()).Select(v => $"{v:X4}"))}]"
                + $", {nameof(Reserved5)}:{Reserved5}"
                + $", {nameof(WordsPerLogicalSector)}:{WordsPerLogicalSector}"
                + $", {nameof(Reserved6)}:[{string.Join(" ", (Reserved6 ?? Enumerable.Empty<ushort>()).Select(v => $"{v:X4}"))}]"
                + $", {nameof(RemovableMediaStatus)}:{RemovableMediaStatus}"
                + $", {nameof(SecurityStatus)}:{SecurityStatus}"
                + $", {nameof(VendorSpecific)}:[{string.Join(" ", (VendorSpecific ?? Enumerable.Empty<ushort>()).Select(v => $"{v:X4}"))}]"
                + $", {nameof(CfaPowerMode1)}:{CfaPowerMode1}"
                + $", {nameof(ReservedForCompactFlashAssociation)}:[{string.Join(" ", (ReservedForCompactFlashAssociation ?? Enumerable.Empty<ushort>()).Select(v => $"{v:X4}"))}]"
                + $", {nameof(DeviceNominalFormFactor)}:{DeviceNominalFormFactor}"
                + $", {nameof(DataSetManagement)}:{DataSetManagement}"
                + $", {nameof(AdditionalProductIdentifier)}:[{string.Join(" ", (AdditionalProductIdentifier ?? Enumerable.Empty<ushort>()).Select(v => $"{v:X4}"))}]"
                + $", {nameof(Reserved7)}:[{string.Join(" ", (Reserved7 ?? Enumerable.Empty<ushort>()).Select(v => $"{v:X4}"))}]"
                + $", {nameof(CurrentMediaSerialNo)}:{CurrentMediaSerialNo}"
                + $", {nameof(SctCommandTransport)}:{SctCommandTransport}"
                + $", {nameof(ReservedForCeAta1)}:[{string.Join(" ", (ReservedForCeAta1 ?? Enumerable.Empty<ushort>()).Select(v => $"{v:X4}"))}]"
                + $", {nameof(AlignmentOfLogicalBlocks)}:{AlignmentOfLogicalBlocks}"
                + $", {nameof(WriteReadVerifySectorCountMode3)}:{WriteReadVerifySectorCountMode3}"
                + $", {nameof(WriteReadVerifySectorCountMode2)}:{WriteReadVerifySectorCountMode2}"
                + $", {nameof(NvCacheCapabilities)}:{NvCacheCapabilities}"
                + $", {nameof(NvCacheSizeLogicalBlocks)}:{NvCacheSizeLogicalBlocks}"
                + $", {nameof(NominalMediaRotationRate)}:{NominalMediaRotationRate}"
                + $", {nameof(Reserved8)}:{Reserved8}"
                + $", {nameof(NvCacheOptions1)}:{NvCacheOptions1}"
                + $", {nameof(NvCacheOptions2)}:{NvCacheOptions2}"
                + $", {nameof(Reserved9)}:{Reserved9}"
                + $", {nameof(TransportMajorVersionNumber)}:{TransportMajorVersionNumber}"
                + $", {nameof(TransportMinorVersionNumber)}:{TransportMinorVersionNumber}"
                + $", {nameof(ReservedForCeAta2)}:[{string.Join(" ", (ReservedForCeAta2 ?? Enumerable.Empty<ushort>()).Select(v => $"{v:X4}"))}]"
                + $", {nameof(MinimumBlocksPerDownloadMicrocode)}:{MinimumBlocksPerDownloadMicrocode}"
                + $", {nameof(MaximumBlocksPerDownloadMicrocode)}:{MaximumBlocksPerDownloadMicrocode}"
                + $", {nameof(Reserved10)}:[{string.Join(" ", (Reserved10 ?? Enumerable.Empty<ushort>()).Select(v => $"{v:X4}"))}]"
                + $", {nameof(IntegrityWord)}:{IntegrityWord}"
                + $"}}";
    }
}
