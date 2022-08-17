namespace ACL.Data
{
    /// <summary>
    /// Represents an object that writes to a data source.
    /// </summary>
    public abstract class DataSourceWriter : IDataSourceWriter
    {
        /// <summary>
        /// A string used to identify the data source writer.
        /// </summary>
        public virtual string Id { get; } = string.Empty;
    }
}
