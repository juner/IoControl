using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    /// <summary>
    /// DISK_PERFORMANCE structure ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ns-ntdddisk-_disk_performance )
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct DiskPerformance
    {
        public readonly long BytesRead;
        public readonly long BytesWritten;
        public readonly long ReadTime;
        public readonly long WriteTime;
        public readonly long IdleTime;
        public readonly uint ReadCount;
        public readonly uint WriteCount;
        public readonly uint QueueDepth;
        public readonly uint SplitCount;
        public readonly long QueryTime;
        public readonly uint StorageDeviceNumber;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        readonly ushort[] _StorageManagerName;
        public string StorageManagerName
            => System.Text.Encoding.Unicode.GetString(_StorageManagerName.SelectMany(v => BitConverter.GetBytes(v)).ToArray());
        public DiskPerformance(long BytesRead = default, long BytesWritten = default, long ReadTime = default, long WriteTime = default, long IdleTime = default, uint ReadCount = default, uint WriteCount = default, uint QueueDepth = default,uint SplitCount = default, long QueryTime = default, uint StorageDeviceNumber = default, string StorageManagerName = default)
            => (this.BytesRead, this.BytesWritten, this.ReadTime, this.WriteTime, this.IdleTime, this.ReadCount, this.WriteCount, this.QueueDepth, this.SplitCount, this.QueryTime, this.StorageDeviceNumber, _StorageManagerName)
            = (BytesRead, BytesWritten, ReadTime, WriteTime, IdleTime, ReadCount, WriteCount, QueueDepth, SplitCount, QueryTime, StorageDeviceNumber 
            , System.Text.Encoding.Unicode.GetBytes(StorageManagerName ?? string.Empty).Select((Value,Index) => (Value,Index)).GroupBy(v => v.Index / 2, v => v.Value).Select(v => BitConverter.ToUInt16(v.ToArray(),0)).Concat(Enumerable.Repeat<ushort>(0,8)).Take(8).ToArray());
        public override string ToString()
            => $"{nameof(DiskPerformance)}{{{nameof(BytesRead)}:{BytesRead}, {nameof(BytesWritten)}:{BytesWritten}, {nameof(ReadTime)}:{ReadTime}, {nameof(WriteTime)}:{WriteTime}, {nameof(IdleTime)}:{IdleTime}, {nameof(ReadCount)}:{ReadCount}, {nameof(WriteCount)}:{WriteCount}, {nameof(QueueDepth)}:{QueueDepth}, {nameof(SplitCount)}:{SplitCount}, {nameof(QueryTime)}:{QueryTime}, {nameof(StorageDeviceNumber)}:{StorageDeviceNumber}, {nameof(StorageManagerName)}:{StorageManagerName}}}";
    }
}
