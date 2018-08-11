using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    /// <summary>
    /// MEDIA_SERIAL_NUMBER_DATA structure ( https://msdn.microsoft.com/library/windows/hardware/ff562213 )
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct MediaSerialNumberData
    {
        public readonly uint SerialNumberLength;
        public readonly uint Result;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst =2)]
        public readonly uint[] Reserved;
        [MarshalAs(UnmanagedType.ByValArray,SizeConst = 1)]
        public readonly byte[] SerialNumberData;

        MediaSerialNumberData(uint SerialNumberLength, uint Result, uint[] Reserved, byte[] SerialNumberData)
            => (this.SerialNumberLength, this.Result, this.Reserved, this.SerialNumberData) = (SerialNumberLength, Result, Reserved, SerialNumberData ?? new byte[1]);
        public MediaSerialNumberData(uint SerialNumberLength, uint Result, byte[] SerialNumberData) : this(SerialNumberLength, Result, new uint[2], SerialNumberData) { }
        public MediaSerialNumberData(uint Result, byte[] SerialNumberData) : this((uint)SerialNumberData.Length, Result, new uint[2], SerialNumberData) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="SerialNumberData"></param>
        /// <returns></returns>
        public MediaSerialNumberData Set(uint? Result = null, byte[] SerialNumberData = null) => new MediaSerialNumberData((uint)((SerialNumberData ?? this.SerialNumberData)?.Length ?? 0), Result ?? this.Result, new uint[2], SerialNumberData ?? this.SerialNumberData);
    }

}
