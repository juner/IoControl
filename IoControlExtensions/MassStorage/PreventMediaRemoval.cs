namespace IoControl.MassStorage
{
    /// <summary>
    /// PREVENT_MEDIA_REMOVAL
    /// </summary>
    internal readonly struct PreventMediaRemoval {

        /// <summary>
        /// PreventMediaRemoval
        /// </summary>
        public readonly bool IsPreventMediaRemoval;
        public PreventMediaRemoval(bool IsPreventMediaRemoval) => this.IsPreventMediaRemoval = IsPreventMediaRemoval;
        public static implicit operator bool(in PreventMediaRemoval pmr) => pmr.IsPreventMediaRemoval;
        public static implicit operator PreventMediaRemoval(bool b) => new PreventMediaRemoval(b);
    }
}
