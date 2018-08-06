using System;

namespace IoControl
{
    [Flags]
    public enum FileFlags : uint{
        /// <summary>
        /// FILE_FLAG_OPEN_NO_RECALL
        /// </summary>
        OpenNoRecall = 0x00100000,
        /// <summary>
        /// FILE_FLAG_OPEN_REPARSE_POINT
        /// </summary>
        OpenReparsePoint = 0x00200000,
        /// <summary>
        /// FILE_FLAG_POSIX_SEMANTICS
        /// </summary>
        PosixSemantics = 0x01000000,
        /// <summary>
        /// FILE_FLAG_BACKUP_SEMANTICS
        /// </summary>
        BackupSemantics = 0x02000000,
        /// <summary>
        /// FILE_FLAG_DELETE_ON_CLOSE
        /// </summary>
        DeleteOnClose = 0x04000000,
        /// <summary>
        /// FILE_FLAG_SEQUENTIAL_SCAN
        /// </summary>
        SequentialScan = 0x08000000,
        /// <summary>
        /// FILE_FLAG_RANDOM_ACCESS
        /// </summary>
        RandomAccess = 0x10000000,
        /// <summary>
        /// FILE_FLAG_NO_BUFFERING
        /// </summary>
        NoBuffering = 0x20000000,
        /// <summary>
        /// FILE_FLAG_OVERLAPPED
        /// </summary>
        Overlapped = 0x40000000,
        /// <summary>
        /// FILE_FLAG_WRITE_THROUGH
        /// </summary>
        WriteThrough = 0x80000000,
    }
}
