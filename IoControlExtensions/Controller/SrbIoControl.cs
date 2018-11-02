using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace IoControl.Controller
{
    /// <summary>
    /// SRB_IO_CONTROL structure
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddscsi/ns-ntddscsi-_srb_io_control
    /// </summary>
    public readonly struct SrbIoControl {
        public readonly uint HeaderLength;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst =8)]
        readonly byte[] __Signature;
        byte[] _Signature => (__Signature ?? Enumerable.Empty<byte>()).Concat(Enumerable.Repeat<byte>(0, 8)).Take(8).ToArray();
        public string Signagure => Encoding.ASCII.GetString(_Signature);
        public readonly uint Timeout;
        public readonly IOControlCode ControlCode;
        public readonly uint ReturnCode;
        public readonly uint Length;
        public SrbIoControl(uint HeaderLength = default, string Signagure = null, uint Timeout = default, IOControlCode ControlCode = default, uint ReturnCode = default, uint Length = default)
            => (this.HeaderLength, __Signature, this.Timeout, this.ControlCode, this.ReturnCode, this.Length)
            = (HeaderLength
            , (string.IsNullOrEmpty(Signagure) ? Enumerable.Empty<byte>() : Encoding.ASCII.GetBytes(Signagure)).Concat(Enumerable.Repeat<byte>(0, 8)).Take(8).ToArray()
            , Timeout, ControlCode, ReturnCode, Length);
        public SrbIoControl Set(uint? HeaderLength = null, string Signagure = null, uint? Timeout = null, IOControlCode? ControlCode = null, uint? ReturnCode = null, uint? Length = null)
            => HeaderLength == null && Signagure == null && Timeout == null && ControlCode == null && ReturnCode == null && Length == null ? this
            : new SrbIoControl(HeaderLength ?? this.HeaderLength, Signagure ?? this.Signagure, Timeout ?? this.Timeout, ControlCode ?? this.ControlCode, ReturnCode ?? this.ReturnCode, Length ?? this.Length);
        public void Deconstruct(out uint HeaderLength, out string Signagure, out uint Timeout, out IOControlCode ControlCode, out uint ReturnCode, out uint Length)
            => (HeaderLength, Signagure, Timeout, ControlCode, ReturnCode, Length)
            = (this.HeaderLength, this.Signagure, this.Timeout, this.ControlCode, this.ReturnCode, this.Length);
        public override string ToString()
            => $"{nameof(SrbIoControl)} {{"
            + $"{nameof(HeaderLength)}:{HeaderLength}"
            + $", {nameof(Signagure)}:{Signagure} [{string.Join(" ",_Signature.Select(v => $"{v:X2}"))}]"
            + $", {nameof(Timeout)}:{Timeout}"
            + $", {nameof(ControlCode)}:{ControlCode}"
            + $", {nameof(ReturnCode)}:{ReturnCode}"
            + $", {nameof(Length)}:{Length}"
            + $"}}";
    }
}
