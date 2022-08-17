namespace ACL.Data
{
    /// <summary>
    /// Interface to an object that reads from a data source.
    /// </summary>
    public interface IDataSourceReader
    {
        /// <summary>
        /// A string used to identify the data source reader.
        /// </summary>
        string Id { get; }
    }
}