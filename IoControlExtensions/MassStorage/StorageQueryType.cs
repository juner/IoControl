namespace IoControl.MassStorage
{
    public enum StorageQueryType : uint
    {
        /// <summary>Instructs the driver to return an appropriate descriptor.</summary>
        StandardQuery = 0,
        /// <summary>Instructs the driver to report whether the descriptor is supported.</summary>
        ExistsQuery = 1,
        /// <summary>Used to retrieve a mask of writeable fields in the descriptor. Not currently supported. Do not use.</summary>
        MaskQuery = 2,
        /// <summary>Specifies the upper limit of the list of query types. This is used to validate the query type.</summary>
        QueryMaxDefined = 3
    }
}
