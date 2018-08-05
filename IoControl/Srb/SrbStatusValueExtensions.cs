namespace IoControl.Srb
{
    public static class SrbStatusValueExtensions
    {
        public static SrbStatusValue ToValue(this SrbStatus Status, StatusFlags Flags = default) => (SrbStatusValue)((byte)Status | (byte)Flags);
        public static SrbStatus GetStatus(this SrbStatusValue Value) => (SrbStatus)(((byte)Value) & ~(byte)(StatusFlags.QueueFrozen | StatusFlags.AutosenseValid));
        public static StatusFlags GetFlags(this SrbStatusValue Value) => (StatusFlags)(((byte)Value) & (byte)(StatusFlags.QueueFrozen | StatusFlags.AutosenseValid));
        public static bool IsQueueFrozen(this SrbStatusValue Value) => (((byte)Value) & ((byte)StatusFlags.QueueFrozen)) == ((byte)StatusFlags.QueueFrozen);
        public static bool IsAutosenseValid(this SrbStatusValue Value) => (((byte)Value) & ((byte)StatusFlags.AutosenseValid)) == ((byte)StatusFlags.AutosenseValid);
        public static void Deconstruct(this SrbStatusValue Value, out SrbStatus Status, out StatusFlags Flags)
            => (Status, Flags) = (GetStatus(Value), GetFlags(Value));
    }
}
