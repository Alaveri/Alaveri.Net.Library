namespace ACL.Data
{
    /// <summary>
    /// Represents a DataSourceReader that does nothing.
    /// </summary>
    public class NullDataSourceReader : IDataSourceWriter
    {
        public string Id => string.Empty;
        public static readonly NullDataSourceWriter Instance = new();
    }
}
