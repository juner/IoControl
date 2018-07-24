using System;

namespace IoControl.Controller
{
    [Flags]
    public enum AtaFlags : ushort
    {
        DrdyRequired = (1 << 0),
        DataIn = (1 << 1),
        DataOut = (1 << 2),
        AF_48BIT_COMMAND = (1 << 3),
        UseDma = (1 << 4),
        NoMultiple = (1 << 5),
    }
}
