using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    /// <summary>
    /// DISK_DETECTION_INFO Strcuture ( https://docs.microsoft.com/en-us/windows/desktop/api/winioctl/ns-winioctl-_disk_detection_info )
    /// Contains detected drive parameters.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct DiskDetectionInfo {
        /// <summary>
        /// The size of the structure, in bytes.
        /// </summary>
        public readonly uint SizeOfDetectInfo;
        /// <summary>
        /// The detected partition type.
        /// </summary>
        public readonly DetectionType DetectionType;
        private readonly DiskDetectionInfoUnion Info;
        /// <summary>
        /// If <see cref="DetectionType"/> is <see cref="DetectionType.Int13"/>, the union is a <see cref="DiskInt13Info"/> structure.
        /// </summary>
        public DiskInt13Info Int13 => Info.Int13;
        /// <summary>
        /// If <see cref="DetectionType"/> is <see cref="DetectionType.ExInt13"/>, the union is a <see cref="DiskExInt13Info"/> structure.
        /// </summary>
        public DiskExInt13Info ExInt13 => Info.ExInt13;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SizeOfDetectInfo"></param>
        /// <param name="DetectionType"></param>
        public DiskDetectionInfo(uint SizeOfDetectInfo, DetectionType DetectionType)
            => (this.SizeOfDetectInfo, this.DetectionType, Info) = (SizeOfDetectInfo, DetectionType, default);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SizeOfDetectInfo"></param>
        /// <param name="DetectionType"></param>
        /// <param name="Int13"></param>
        public DiskDetectionInfo(uint SizeOfDetectInfo, DetectionType DetectionType, DiskInt13Info Int13)
            => (this.SizeOfDetectInfo, this.DetectionType, Info) = (SizeOfDetectInfo, DetectionType, Int13);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SizeOfDetectInfo"></param>
        /// <param name="DetectionType"></param>
        /// <param name="ExInt13"></param>
        public DiskDetectionInfo(uint SizeOfDetectInfo, DetectionType DetectionType, DiskExInt13Info ExInt13)
            => (this.SizeOfDetectInfo, this.DetectionType, Info) = (SizeOfDetectInfo, DetectionType, ExInt13);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SizeOfDetectInfo"></param>
        /// <param name="DetectionType"></param>
        public void Deconstruct(out uint SizeOfDetectInfo, out DetectionType DetectionType)
            => (SizeOfDetectInfo, DetectionType) = (this.SizeOfDetectInfo, this.DetectionType);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SizeOfDetectInfo"></param>
        /// <param name="DetectionType"></param>
        /// <param name="Int13"></param>
        public void Deconstruct(out uint SizeOfDetectInfo, out DetectionType DetectionType, out DiskInt13Info Int13)
            => (SizeOfDetectInfo, DetectionType, Int13) = (this.SizeOfDetectInfo, this.DetectionType, Info);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SizeOfDetectInfo"></param>
        /// <param name="DetectionType"></param>
        /// <param name="ExInt13"></param>
        public void Deconstruct(out uint SizeOfDetectInfo, out DetectionType DetectionType, out DiskExInt13Info ExInt13)
            => (SizeOfDetectInfo, DetectionType, ExInt13) = (this.SizeOfDetectInfo, this.DetectionType, Info);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{nameof(DiskDetectionInfo)}{{{nameof(SizeOfDetectInfo)}:{SizeOfDetectInfo}, {nameof(DetectionType)}:{DetectionType}, {(DetectionType == DetectionType.Int13 ? $"{nameof(Int13)}:{Int13}" : DetectionType == DetectionType.ExInt13 ? $"{nameof(ExInt13)}:{ExInt13}": "")}}}";
        /// <summary>
        /// DISK_INT13_INFO or DISK_EX_INT13_INFO
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        private readonly struct DiskDetectionInfoUnion
        {
            [FieldOffset(0)]
            public readonly DiskInt13Info Int13;
            [FieldOffset(0)]
            public readonly DiskExInt13Info ExInt13;
            public DiskDetectionInfoUnion(DiskInt13Info Int13) : this() => this.Int13 = Int13;
            public DiskDetectionInfoUnion(DiskExInt13Info ExInt13) : this() => this.ExInt13 = ExInt13;
            public static implicit operator DiskDetectionInfoUnion(in DiskInt13Info Int13) => new DiskDetectionInfoUnion(Int13);
            public static implicit operator DiskInt13Info(in DiskDetectionInfoUnion Union) => Union.Int13;
            public static implicit operator DiskDetectionInfoUnion(in DiskExInt13Info ExInt13) => new DiskDetectionInfoUnion(ExInt13);
            public static implicit operator DiskExInt13Info(in DiskDetectionInfoUnion Union) => Union.ExInt13;
        }
    }
}
