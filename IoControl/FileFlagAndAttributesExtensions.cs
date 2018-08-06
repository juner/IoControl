using System.IO;

namespace IoControl
{
    public static class FileFlagAndAttributesExtensions {
        public static FileFlagAndAttributes Create(FileAttributes FileAttributes) => (FileFlagAndAttributes)FileAttributes;
        public static FileFlagAndAttributes Create(FileFlags FileFlags) => (FileFlagAndAttributes)FileFlags;
        public static FileFlagAndAttributes Create(FileAttributes FileAttributes, FileFlags FileFlags) => (FileFlagAndAttributes)(((uint)FileAttributes) | ((uint)FileFlags));
        public static FileAttributes GetAttributes(this FileFlagAndAttributes FileFlagAndAttributes) => (FileAttributes)((uint)FileFlagAndAttributes & 0x000FFFFF);
        public static FileFlags GetFlags(this FileFlagAndAttributes FileFlagAndAttributes) => (FileFlags)((uint)FileFlagAndAttributes & 0xFFF00000);
        public static void Deconstruct(this FileFlagAndAttributes FileFlagAndAttributes, out FileFlags FileFlags, FileAttributes FileAttributes)
            => (FileFlags, FileAttributes) = (GetFlags(FileFlagAndAttributes), GetAttributes(FileFlagAndAttributes));
    }
}
