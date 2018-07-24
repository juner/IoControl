using System.Runtime.InteropServices;

namespace IoControl.Controller
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct ScsiAddress
    {
        public readonly uint Length;
        public readonly byte PortNumber;
        public readonly byte PathId;
        public readonly byte TargetId;
        public readonly byte Lun;
        public ScsiAddress(uint Length, byte PortNumber, byte PathId, byte TargetId, byte Lun)
            => (this.Length, this.PortNumber, this.PathId, this.TargetId, this.Lun) = (Length, PortNumber, PathId, TargetId, Lun);
        public void Deconstruct(out uint Length, out byte PortNumber, out byte PathId, out byte TargetId, out byte Lun)
            => (Length, PortNumber, PathId, TargetId, Lun) = (this.Length, this.PortNumber, this.PathId, this.TargetId, this.Lun);
        public override string ToString()
            => $"{nameof(ScsiAddress)}{{{nameof(Length)}:{Length}, {nameof(PortNumber)}:{PortNumber}, {nameof(PathId)},{PathId}, {nameof(TargetId)}:{TargetId}, {nameof(Lun)}:{Lun}}}";
    }
}
