using System.Runtime.InteropServices;
using static System.Linq.Enumerable;
namespace IoControl.Controller
{
    /// <summary>
    /// SCSI_PASS_THROUGH_WITH_BUFFERS
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct ScsiPassThroughIdentifyDevice : IScsiPathThrough
    {

        public readonly ScsiPassThrough Spt;
        /// <summary>
        /// realign buffers to double word boundary
        /// </summary>
        public readonly uint Filter;
        [MarshalAs(UnmanagedType.ByValArray,SizeConst =32)]
        public readonly byte[] SenseBuf;
        public readonly IdentifyDevice DataBuf;
        public ScsiPassThroughIdentifyDevice(ScsiPassThrough Spt = default, uint Filter= default, byte[] SenseBuf = null, IdentifyDevice DataBuf = default)
            => (this.Spt, this.Filter, this.SenseBuf, this.DataBuf) = (Spt, Filter
            , (SenseBuf ?? Empty<byte>()).Concat(Repeat<byte>(0,32)).Take(32).ToArray()
            , DataBuf);
        public ScsiPassThroughIdentifyDevice Set(ScsiPassThrough? Spt = null, uint? Filter = null, byte[] SenseBuf = null, IdentifyDevice? DataBuf = null)
            => Spt == null && Filter == null && SenseBuf == null && DataBuf == null ? this
            : new ScsiPassThroughIdentifyDevice(Spt ?? this.Spt, Filter ?? this.Filter, SenseBuf ?? this.SenseBuf, DataBuf ?? this.DataBuf);
        public override string ToString()
            => $"{nameof(ScsiPassThroughIdentifyDevice)}{{"
            + $"{nameof(Spt)}:{Spt}"
            + $"{nameof(Filter)}:{Filter}"
            + $"{nameof(SenseBuf)}:{string.Join(" ",(SenseBuf ?? Empty<byte>()).Concat(Repeat<byte>(0,32)).Take(32))}"
            + $"{nameof(DataBuf)}:{DataBuf}"
            + $"}}";
    }

}
