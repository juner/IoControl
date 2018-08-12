using System;

namespace IoControl.MassStorage
{
    [Flags]
    public enum MediaFlags : uint{
        /// <summary>
        /// MEDIA_ERASEABLE
        /// </summary>
        Eraseable = 0x00000001,
        /// <summary>
        /// MEDIA_WRITE_ONCE
        /// </summary>
        WriteOnce = 0x00000002,
        /// <summary>
        /// MEDIA_READ_ONLY
        /// </summary>
        ReadOnly = 0x00000004,
        /// <summary>
        /// MEDIA_READ_WRITE
        /// </summary>
        ReadWrite = 0x00000008,
        /// <summary>
        /// MEDIA_WRITE_PROTECTED
        /// </summary>
        WriteProtected = 0x00000100,
        /// <summary>
        /// MEDIA_CURRENTLY_MOUNTED
        /// </summary>
        CurrentlyMounted = 0x80000000,
    }

            } 