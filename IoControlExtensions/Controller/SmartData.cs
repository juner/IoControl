using static System.Linq.Enumerable;
using System.Runtime.InteropServices;

namespace IoControl.Controller
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly struct SmartData
    {
        // http://www.t13.org/documents/UploadedDocuments/docs2006/D1699r3f-ATA8-ACS.pdf
        /// <summary>
        /// Version (000 - 361 Vendor specific) 
        /// </summary>
        public readonly ushort Version;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
        readonly Controller.SmartAttribute[] _Attributes;
        /// <summary>
        /// 001 - 36 Vendor specific  Smart Attribute[30]
        /// </summary>
        public Controller.SmartAttribute[] Attributes => (_Attributes ?? Empty<Controller.SmartAttribute>()).Concat(Repeat<Controller.SmartAttribute>(default, 30)).Take(30).ToArray();
        /// <summary>
        ///  362 Off-line data collection status
        /// </summary>
        public readonly byte OffLineDataCollectionStatus;
        /// <summary>
        /// 363 Self-test execution status byte
        /// </summary>
        public readonly byte SelfTestExecutionStatus;
        /// <summary>
        /// 364- 265 Total time in seconds to complete off-line data collection activity 
        /// </summary>
        public readonly ushort TotalTimeInSecondsToCompleteOffLineDataCollectionActivity;
        /// 366 Vendor specific 
        public readonly byte VendorSpecific2;
        /// 367 Off-line data collection capability
        public readonly byte OffLineDataCollectionCapability;
        /// 368 - 369 SMART capability 
        public readonly ushort SmartCapability;
        /// 370 Error logging capability 
        public readonly byte ErrorLoggingCapability;
        /// 371 Vendor specific
        public readonly byte VendorSpecific3;
        /// 372 Short self-test routine recommended polling time (in minutes) 
        public readonly byte ShortSelfTestRoutineRecommendedPollingTimeInMinutes;
        /// 373 Extended self-test routine recommended polling time (7:0) in minutes.  If FFh, use bytes 375 and 376 for the polling time. 
        public readonly byte ExtendedSelfTestRoutineRecommendedPollingInMinutes;
        /// 374 Conveyance self-test routine recommended polling time (in minutes) 
        public readonly byte ConveyanceSelfTestRoutineRecommendedPollingInMinutes;
        /// 375 Extended self-test routine recommended polling time (7:0) in minutes 
        /// 376 Extended self-test routine recommended polling time (15:8) in minutes 
        public readonly ushort ExtendedSelfTestRoutineRecommendedPollingTimeInMinutes;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst =9)]
        readonly byte[] _Reserved1;
        /// <summary>
        /// 377 - 385 Reserved
        /// </summary>
        public byte[] Reserved1 => (_Reserved1 ?? Empty<byte>()).Concat(Repeat<byte>(0, 9)).ToArray();
        [MarshalAs(UnmanagedType.ByValArray, SizeConst =125)]
        readonly byte[] _VendorSpecific4;
        /// <summary>
        /// 386 - 510 Vendor specific
        /// </summary>
        public byte[] VendorSpecific4 => (_VendorSpecific4 ?? Empty<byte>()).Concat(Repeat<byte>(0, 125)).ToArray();
        /// 511 Data structure checksum
        public readonly byte Checksum;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Version"></param>
        /// <param name="Attributes"></param>
        /// <param name="OffLineDataCollectionStatu"></param>
        /// <param name="SelfTestExecutionStatus"></param>
        /// <param name="TotalTimeInSecondsToCompleteOffLineDataCollectionActivity"></param>
        /// <param name="VendorSpecific2"></param>
        /// <param name="OffLineDataCollectionCapability"></param>
        /// <param name="SmartCapability"></param>
        /// <param name="ErrorLoggingCapability"></param>
        /// <param name="VendorSpecific3"></param>
        /// <param name="ShortSelfTestRoutineRecommendedPollingTimeInMinutes"></param>
        /// <param name="ExtendedSelfTestRoutineRecommendedPollingInMinutes"></param>
        /// <param name="ConveyanceSelfTestRoutineRecommendedPollingInMinutes"></param>
        /// <param name="ExtendedSelfTestRoutineRecommendedPollingTimeInMinutes"></param>
        /// <param name="Reserved1"></param>
        /// <param name="VendorSpecific4"></param>
        /// <param name="Checksum"></param>
        public SmartData(ushort Version = default
            , SmartAttribute[] Attributes = default
            , byte OffLineDataCollectionStatus = default
            , byte SelfTestExecutionStatus = default
            , ushort TotalTimeInSecondsToCompleteOffLineDataCollectionActivity = default
            , byte VendorSpecific2 = default
            , byte OffLineDataCollectionCapability = default
            , ushort SmartCapability = default
            , byte ErrorLoggingCapability = default
            , byte VendorSpecific3 = default
            , byte ShortSelfTestRoutineRecommendedPollingTimeInMinutes = default
            , byte ExtendedSelfTestRoutineRecommendedPollingInMinutes = default
            , byte ConveyanceSelfTestRoutineRecommendedPollingInMinutes = default
            , ushort ExtendedSelfTestRoutineRecommendedPollingTimeInMinutes = default
            , byte[] Reserved1 = default
            , byte[] VendorSpecific4 = default
            , byte Checksum = default)
            => (this.Version
            , _Attributes
            , this.OffLineDataCollectionStatus
            , this.SelfTestExecutionStatus
            , this.TotalTimeInSecondsToCompleteOffLineDataCollectionActivity
            , this.VendorSpecific2
            , this.OffLineDataCollectionCapability
            , this.SmartCapability
            , this.ErrorLoggingCapability
            , this.VendorSpecific3
            , this.ShortSelfTestRoutineRecommendedPollingTimeInMinutes
            , this.ExtendedSelfTestRoutineRecommendedPollingInMinutes
            , this.ConveyanceSelfTestRoutineRecommendedPollingInMinutes
            , this.ExtendedSelfTestRoutineRecommendedPollingTimeInMinutes
            , _Reserved1
            , _VendorSpecific4
            , this.Checksum)
            = (Version
            , (Attributes ?? Empty<Controller.SmartAttribute>()).Concat(Repeat<Controller.SmartAttribute>(default, 30)).Take(30).ToArray()
            , OffLineDataCollectionStatus
            , SelfTestExecutionStatus
            , TotalTimeInSecondsToCompleteOffLineDataCollectionActivity
            , VendorSpecific2
            , OffLineDataCollectionCapability
            , SmartCapability
            , ErrorLoggingCapability
            , VendorSpecific3
            , ShortSelfTestRoutineRecommendedPollingTimeInMinutes
            , ExtendedSelfTestRoutineRecommendedPollingInMinutes
            , ConveyanceSelfTestRoutineRecommendedPollingInMinutes
            , ExtendedSelfTestRoutineRecommendedPollingTimeInMinutes
            , (Reserved1 ?? Empty<byte>()).Concat(Repeat<byte>(0, 9)).ToArray()
            , (VendorSpecific4 ?? Empty<byte>()).Concat(Repeat<byte>(0,125)).ToArray()
            , Checksum);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Version"></param>
        /// <param name="Attributes"></param>
        /// <param name="OffLineDataCollectionStatus"></param>
        /// <param name="SelfTestExecutionStatus"></param>
        /// <param name="TotalTimeInSecondsToCompleteOffLineDataCollectionActivity"></param>
        /// <param name="VendorSpecific2"></param>
        /// <param name="OffLineDataCollectionCapability"></param>
        /// <param name="SmartCapability"></param>
        /// <param name="ErrorLoggingCapability"></param>
        /// <param name="VendorSpecific3"></param>
        /// <param name="ShortSelfTestRoutineRecommendedPollingTimeInMinutes"></param>
        /// <param name="ExtendedSelfTestRoutineRecommendedPollingInMinutes"></param>
        /// <param name="ConveyanceSelfTestRoutineRecommendedPollingInMinutes"></param>
        /// <param name="ExtendedSelfTestRoutineRecommendedPollingTimeInMinutes"></param>
        /// <param name="Reserved1"></param>
        /// <param name="VendorSpecific4"></param>
        /// <param name="Checksum"></param>
        /// <returns></returns>
        public SmartData Set(ushort? Version = default
            , SmartAttribute[] Attributes = default
            , byte? OffLineDataCollectionStatus = default
            , byte? SelfTestExecutionStatus = default
            , ushort? TotalTimeInSecondsToCompleteOffLineDataCollectionActivity = default
            , byte? VendorSpecific2 = default
            , byte? OffLineDataCollectionCapability = default
            , ushort? SmartCapability = default
            , byte? ErrorLoggingCapability = default
            , byte? VendorSpecific3 = default
            , byte? ShortSelfTestRoutineRecommendedPollingTimeInMinutes = default
            , byte? ExtendedSelfTestRoutineRecommendedPollingInMinutes = default
            , byte? ConveyanceSelfTestRoutineRecommendedPollingInMinutes = default
            , ushort? ExtendedSelfTestRoutineRecommendedPollingTimeInMinutes = default
            , byte[] Reserved1 = default
            , byte[] VendorSpecific4 = default
            , byte? Checksum = default)
            => Version == null
            && Attributes == null
            && OffLineDataCollectionStatus == null
            && SelfTestExecutionStatus == null
            && TotalTimeInSecondsToCompleteOffLineDataCollectionActivity == null
            && VendorSpecific2 == null
            && OffLineDataCollectionCapability == null
            && SmartCapability == null
            && ErrorLoggingCapability == null
            && VendorSpecific3 == null
            && ShortSelfTestRoutineRecommendedPollingTimeInMinutes == null
            && ExtendedSelfTestRoutineRecommendedPollingInMinutes == null
            && ConveyanceSelfTestRoutineRecommendedPollingInMinutes == null
            && ExtendedSelfTestRoutineRecommendedPollingTimeInMinutes == null
            && Reserved1 == null
            && VendorSpecific4 == null
            && Checksum == null ? this
            : new SmartData(Version ?? this.Version
            , Attributes ?? _Attributes
            , OffLineDataCollectionStatus ?? this.OffLineDataCollectionStatus
            , SelfTestExecutionStatus ?? this.SelfTestExecutionStatus
            , TotalTimeInSecondsToCompleteOffLineDataCollectionActivity ?? this.TotalTimeInSecondsToCompleteOffLineDataCollectionActivity
            , VendorSpecific2 ?? this.VendorSpecific2
            , OffLineDataCollectionCapability ?? this.OffLineDataCollectionCapability
            , SmartCapability ?? this.SmartCapability
            , ErrorLoggingCapability ?? this.ErrorLoggingCapability
            , VendorSpecific3 ?? this.VendorSpecific3
            , ShortSelfTestRoutineRecommendedPollingTimeInMinutes ?? this.ShortSelfTestRoutineRecommendedPollingTimeInMinutes
            , ExtendedSelfTestRoutineRecommendedPollingInMinutes ?? this.ExtendedSelfTestRoutineRecommendedPollingInMinutes
            , ConveyanceSelfTestRoutineRecommendedPollingInMinutes ?? this.ConveyanceSelfTestRoutineRecommendedPollingInMinutes
            , ExtendedSelfTestRoutineRecommendedPollingTimeInMinutes ?? this.ExtendedSelfTestRoutineRecommendedPollingTimeInMinutes
            , Reserved1 ?? _Reserved1
            , VendorSpecific4 ?? _VendorSpecific4
            , Checksum ?? this.Checksum);
        public override string ToString()
            => $"{nameof(SmartData)}{{"
            + $"{nameof(Version)}:{Version}"
            + $", {nameof(Attributes)}:[{string.Join(" ", Attributes)}]"
            + $", {nameof(OffLineDataCollectionStatus)}:{OffLineDataCollectionStatus}"
            + $", {nameof(SelfTestExecutionStatus)}:{SelfTestExecutionStatus}"
            + $", {nameof(TotalTimeInSecondsToCompleteOffLineDataCollectionActivity)}:{TotalTimeInSecondsToCompleteOffLineDataCollectionActivity}"
            + $", {nameof(VendorSpecific2)}:{VendorSpecific2}"
            + $", {nameof(OffLineDataCollectionCapability)}:{OffLineDataCollectionCapability}"
            + $", {nameof(SmartCapability)}:{SmartCapability}"
            + $", {nameof(ErrorLoggingCapability)}:{ErrorLoggingCapability}"
            + $", {nameof(VendorSpecific3)}:{VendorSpecific3}"
            + $", {nameof(ShortSelfTestRoutineRecommendedPollingTimeInMinutes)}:{ShortSelfTestRoutineRecommendedPollingTimeInMinutes}"
            + $", {nameof(ExtendedSelfTestRoutineRecommendedPollingInMinutes)}:{ExtendedSelfTestRoutineRecommendedPollingInMinutes}"
            + $", {nameof(ConveyanceSelfTestRoutineRecommendedPollingInMinutes)}:{ConveyanceSelfTestRoutineRecommendedPollingInMinutes}"
            + $", {nameof(ExtendedSelfTestRoutineRecommendedPollingTimeInMinutes)}:{ExtendedSelfTestRoutineRecommendedPollingTimeInMinutes}"
            + $", {nameof(Reserved1)}:[{string.Join(" ", Reserved1.Select(v => $"{v:X2}"))}]"
            + $", {nameof(VendorSpecific4)}:[{string.Join(" ",VendorSpecific4.Select(v => $"{v:X2}"))}"
            + $", {nameof(Checksum)}:{Checksum}"
            + $"}}";
    }
}