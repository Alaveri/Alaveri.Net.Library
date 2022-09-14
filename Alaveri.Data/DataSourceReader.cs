namespace Alaveri.Data
{
    /// <summary>
    /// Represents a base class for data source readers.
    /// </summary>
    public abstract class DataSourceReader : IDataSourceReader
    {
        /// <summary>
        /// A string used to identify the data source reader.
        /// </summary>
        public virtual string Id { get; } = string.Empty;
    }
}
