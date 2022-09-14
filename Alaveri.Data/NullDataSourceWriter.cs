namespace Alaveri.Data
{
    /// <summary>
    /// Represents a DataSourceWriter that does nothing.
    /// </summary>
    public class NullDataSourceWriter : IDataSourceWriter
    {
        public string Id => string.Empty;
        public static readonly NullDataSourceWriter Instance = new();
    }
}
