namespace ACL.Data
{
    /// <summary>
    /// Interface to an object that writes to a data source.
    /// </summary>
    public interface IDataSourceWriter
    {
        /// <summary>
        /// A string used to identify the data source writer.
        /// </summary>
        string Id { get; }
    }
}