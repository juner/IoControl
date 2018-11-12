namespace IoControl.Controller
{
    public interface IAtaPassThroughEx
    {
        IAtaPassThroughEx Header { get; }
        ushort Length { get; }
        AtaFlags AtaFlags { get; }
        byte PathId { get; }
        byte TargetId { get; }
        byte Lun { get; }
        byte ReservedAsUchar { get; }
        uint DataTransferLength { get; }
        uint TimeOutValue { get; }
        uint ReservedAsUlong { get; }
        long DataBufferOffset { get; }
        byte[] PreviousTaskFile { get; }
        byte[] CurrentTaskFile { get; }
    }
    public interface IAtaPassThroughEx<T> : IAtaPassThroughEx
        where T : struct
    {
        T Data { get; }
    }
}
