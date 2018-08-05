using System;

namespace IoControl.Srb
{
    [Flags]
    public enum StatusFlags : byte
    {
        /// <summary>
        /// SRB_STATUS_QUEUE_FROZEN
        /// </summary>
        QueueFrozen = 0x40,
        /// <summary>
        /// SRB_STATUS_AUTOSENSE_VALID
        /// </summary>
        AutosenseValid = 0x80,
    }
}
