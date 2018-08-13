namespace IoControl.MassStorage
{
    public interface IStorageDescriptor
    {
        uint Version { get; }
        uint Size { get; }
    }

}
