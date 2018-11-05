namespace IoControl.Disk
{
    public interface ISendcmdoutparams
    {
        uint BufferSize { get; }
        Driverstatus DriverStatus { get; }
        byte[] Buffer { get; }
    }
}
