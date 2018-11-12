namespace IoControl.Controller
{
    public interface IAtaPassThroughEx
    {
        AtaPassThroughEx Header { get; }
        ushort Length { get; }
        AtaFlags AtaFlags { get; }
        byte PathId { get; }
        byte TargetId { get; }
        byte Lun { get; }
        byte ReservedAsUchar { get; }
        uint DataTransferLength { get; }
        uint TimeOutValue { get; }
        uint ReservedAsUlong { get; }
        int DataBufferOffset { get; }
        byte[] PreviousTaskFile { get; }
        byte[] CurrentTaskFile { get; }
    }
    public interface IAtaPassThroughEx<T> : IAtaPassThroughEx
        where T : struct
    {
        T Data { get; }
    }
}
