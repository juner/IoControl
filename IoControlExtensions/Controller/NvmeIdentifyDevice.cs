using System.Linq;
using System.Runtime.InteropServices;

namespace IoControl.Controller
{
    [StructLayout(LayoutKind.Sequential, Pack =1, Size = 512)]
    public readonly struct NvmeIdentifyDevice
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst =4)]
        readonly byte[] _Reserved1;
        public byte[] Reserved1 => (_Reserved1 ?? Enumerable.Empty<byte>()).Concat(Enumerable.Repeat<byte>(0, 4)).ToArray();
        [MarshalAs(UnmanagedType.ByValArray, SizeConst =20)]
        readonly byte[] __SerialNumber;
        byte[] _SerialNumber => (_SerialNumber ?? Enumerable.Empty<byte>()).Concat(Enumerable.Repeat<byte>(0, 20)).Take(20).ToArray();
        public string SerialNumber => System.Text.Encoding.ASCII.GetString(_SerialNumber);
        [MarshalAs(UnmanagedType.ByValArray, SizeConst =40)]
        readonly byte[] __Model;
        byte[] _Model => (_Model ?? Enumerable.Empty<byte>()).Concat(Enumerable.Repeat<byte>(0, 40)).Take(40).ToArray();
        public string Model => System.Text.Encoding.ASCII.GetString(_Model);
        [MarshalAs(UnmanagedType.ByValArray, SizeConst =8)]
        readonly byte[] __FirmwareRev;
        byte[] _FirmwareRev => (_FirmwareRev ?? Enumerable.Empty<byte>()).Concat(Enumerable.Repeat<byte>(0, 8)).Take(8).ToArray();
        public string FirmwareRev => System.Text.Encoding.ASCII.GetString(_FirmwareRev);
        [MarshalAs(UnmanagedType.ByValArray, SizeConst =9)]
        readonly byte[] _Reserved2;
        public byte[] Reserved2 => (_Reserved2 ?? Enumerable.Empty<byte>()).Concat(Enumerable.Repeat<byte>(0, 9)).Take(9).ToArray();
        public readonly byte MinorVersion;
        public readonly byte MajorVersion;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 428)]
        readonly byte[] _Reserved3;
        public byte[] Reserved3 => (_Reserved3 ?? Enumerable.Empty<byte>()).Concat(Enumerable.Repeat<byte>(0, 428)).Take(428).ToArray();
        public override string ToString()
            => $"{nameof(NvmeIdentifyDevice)}{{"
            + $"{nameof(Reserved1)}:[{string.Join(" ", Reserved1.Select(v => $"{v:X2}"))}]"
            + $"{nameof(SerialNumber)}:{SerialNumber} [{string.Join(" ", _SerialNumber.Select(v => $"{v:X2}"))}]"
            + $"{nameof(Model)}:{Model} [{string.Join(" ", _Model.Select(v => $"{v:X2}"))}]"
            + $"{nameof(FirmwareRev)}:{FirmwareRev} [{string.Join(" ", _FirmwareRev.Select(v => $"{v:X2}"))}]"
            + $"{nameof(Reserved2)}:[{string.Join(" ",Reserved2.Select(v => $"{v:X2}"))}"
            + $"{nameof(MinorVersion)}:{MinorVersion}"
            + $"{nameof(MajorVersion)}:{MajorVersion}"
            + $"{nameof(Reserved3)}:[{string.Join(" ",Reserved3.Select(v => $"{v:X2}"))}]"
            + $"}}";
    }
}
