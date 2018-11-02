using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    /// <summary>
    /// GETVERSIONINPARAMS structure
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ns-ntdddisk-_getversioninparams
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Getversioninparams : IEquatable<Getversioninparams>
    {
        public readonly byte Version;
        public readonly byte Revision;
        public readonly byte Reserved1;
        public readonly IDEDeviceMap IDEDeviceMap;
        public readonly uint Capabilities;
        [MarshalAs(UnmanagedType.ByValArray,SizeConst =4)]
        readonly uint[] _Reserved2;
        public uint[] Reserved2 => (_Reserved2 ?? Enumerable.Empty<uint>()).Concat(Enumerable.Repeat(0u, 4)).Take(4).ToArray();
        public Getversioninparams(byte Version = default, byte Revision = default, byte Reserved1 = default, IDEDeviceMap IDEDeviceMap = default, uint Capabilities = default, uint[] Reserved2 = null)
            => (this.Version, this.Revision, this.Reserved1, this.IDEDeviceMap, this.Capabilities, _Reserved2)
            = (Version, Revision, Reserved1, IDEDeviceMap, Capabilities, (Reserved2 ?? Enumerable.Empty<uint>()).Concat(Enumerable.Repeat(0u, 4)).Take(4).ToArray());
        public Getversioninparams Set(byte? Version = null, byte? Revision = null, byte? Reserved1 = null, IDEDeviceMap? IDEDeviceMap = null, uint? Capabilities = null, uint[] Reserved2 = null)
            => Version == null && Revision == null && Reserved1 == null && IDEDeviceMap == null && Capabilities == null && Reserved2 == null ? this
            : new Getversioninparams(Version ?? this.Version, Revision ?? this.Revision, Reserved1 ?? this.Reserved1, IDEDeviceMap ?? this.IDEDeviceMap, Capabilities ?? this.Capabilities, Reserved2 ?? _Reserved2);
        public override string ToString()
            => $"{nameof(Getversioninparams)} {{"
            + $"{nameof(Version)}:{Version}"
            + $", {nameof(Revision)}:{Revision}"
            + $", {nameof(Reserved1)}:{Reserved1}"
            + $", {nameof(IDEDeviceMap)}:[{(IDEDeviceMap == default ? "" :$"{IDEDeviceMap}")}]"
            + $", {nameof(Capabilities)}:{Capabilities}"
            + $", {nameof(Reserved2)}:[{string.Join(",", Reserved2)}]"
            + $"}}";

        public override bool Equals(object obj) => obj is Getversioninparams p && Equals(p);

        public bool Equals(Getversioninparams other)
            => Version == other.Version &&
                   Revision == other.Revision &&
                   Reserved1 == other.Reserved1 &&
                   IDEDeviceMap == other.IDEDeviceMap &&
                   Capabilities == other.Capabilities &&
                   EqualityComparer<uint[]>.Default.Equals(_Reserved2, other._Reserved2);

        public override int GetHashCode()
        {
            var hashCode = -95566877;
            hashCode = hashCode * -1521134295 + Version.GetHashCode();
            hashCode = hashCode * -1521134295 + Revision.GetHashCode();
            hashCode = hashCode * -1521134295 + Reserved1.GetHashCode();
            hashCode = hashCode * -1521134295 + IDEDeviceMap.GetHashCode();
            hashCode = hashCode * -1521134295 + Capabilities.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<uint[]>.Default.GetHashCode(_Reserved2);
            return hashCode;
        }

        public static bool operator ==(Getversioninparams getversioninparams1, Getversioninparams getversioninparams2)
            => getversioninparams1.Equals(getversioninparams2);

        public static bool operator !=(Getversioninparams getversioninparams1, Getversioninparams getversioninparams2)
            => !(getversioninparams1 == getversioninparams2);
    }
    [Flags]
    public enum IDEDeviceMap : byte
    {
        /// <summary>
        /// The device is either a SATA drive or an IDE drive. If it is an IDE drive, it is the master device on the primary channel. 
        /// </summary>
        IsSATAOrIDEMasterOnPrimaryChannel = 0b00000001,
        /// <summary>
        /// The device is an IDE drive, and it is the subordinate device on the primary channel. 
        /// </summary>
        IsIDESubordinateOnPrimaryChannel = 0b00000010,
        /// <summary>
        /// The device is an IDE drive, and it is the master device on the secondary channel. 
        /// </summary>
        IsIDEMasterOnSecondaryChannnel = 0b00000100,
        /// <summary>
        /// The device is an IDE drive, and it is the subordinate device on the secondary channel. 
        /// </summary>
        IsIDESubordinateOnSecondaryChannel = 0b00001000,
        /// <summary>
        /// The device is an ATAPI drive, and it is the master device on the primary channel. 
        /// </summary>
        IsATAPIMasterOnPrimaryChannel =0b00010000,
        /// <summary>
        /// The device is an ATAPI drive, and it is the subordinate device on the primary channel. 
        /// </summary>
        IsATAPISubordinateOnPrimaryChannel =0b00100000,
        /// <summary>
        /// The device is an ATAPI drive, and it is the master device on the secondary channel. 
        /// </summary>
        IsATAPIMasterOnSecondaryChannel =0b01000000,
        /// <summary>
        /// The device is an ATAPI drive, and it is the subordinate device on the secondary channel. 
        /// </summary>
        IsAPAPISubordinateOnSecondaryChannel = 0b10000000,
    }
}