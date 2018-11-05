using System.Linq;
using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    public readonly struct Driverstatus
    {
        public readonly byte DriverError;
        public readonly byte IDEError;
        [MarshalAs(UnmanagedType.ByValArray,SizeConst =2)]
        readonly byte[] _Reserved1;
        public byte[] Reserved1 => (_Reserved1 ?? Enumerable.Empty<byte>()).Concat(Enumerable.Repeat<byte>(0, 2)).Take(2).ToArray();
        [MarshalAs(UnmanagedType.ByValArray, SizeConst =2)]
        readonly uint[] _Reserved2;
        public uint[] Reserved2 => (_Reserved2 ?? Enumerable.Empty<uint>()).Concat(Enumerable.Repeat(0u, 2)).Take(2).ToArray();
        public Driverstatus(byte DriverError = default, byte IDEError = default, byte[] Reserved1 = null, uint[] Reserved2 = null)
            => (this.DriverError, this.IDEError, _Reserved1, _Reserved2)
            = (DriverError, IDEError
                , (Reserved1 ?? Enumerable.Empty<byte>()).Concat(Enumerable.Repeat<byte>(0, 2)).Take(2).ToArray()
                , (Reserved2 ?? Enumerable.Empty<uint>()).Concat(Enumerable.Repeat(0u, 2)).Take(2).ToArray());
        public Driverstatus Set(byte? DriverError = null, byte? IDEError = null, byte[] Reserved1 = null, uint[] Reserved2 = null)
            => DriverError == null && IDEError == null && Reserved1 == null && Reserved2 == null ? this
            : new Driverstatus(DriverError ?? this.DriverError, IDEError ?? this.IDEError, Reserved1 ?? _Reserved1, Reserved2 ?? _Reserved2);
        public override string ToString()
            => $"{nameof(Driverstatus)}{{"
            + $"{nameof(DriverError)}:{DriverError:X2}({DriverError})"
            + $", {nameof(IDEError)}:{IDEError:X2}({IDEError})"
            + $", {nameof(Reserved1)}:[{string.Join(" ", Reserved1.Select(v => $"{v:X2}"))}]"
            + $", {nameof(Reserved2)}:[{string.Join(" ", Reserved2.Select(v => $"{v:X8}"))}]"
            + $"}}";
    }
}
