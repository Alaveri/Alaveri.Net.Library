namespace Alaveri.Data
{
    /// <summary>
    /// Represents a generic data source.
    /// </summary>
    /// <typeparam name="TWriter">The type of the writer used to write to the data source.</typeparam>
    /// <typeparam name="TReader">The type of the reader used to read from to the data source.</typeparam>
    public abstract class DataSource<TReader, TWriter> : IDataSource<TReader, TWriter>
        where TReader: IDataSourceReader
        where TWriter: IDataSourceWriter
    {
        /// <summary>
        /// The reader used to access the data source.
        /// </summary>
        public virtual TReader Reader { get; protected set; }

        /// <summary>
        /// The writer used to access the data source.
        /// </summary>
        public virtual TWriter Writer { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the DataSource class using the specified reader and writer.
        /// </summary>
        /// <param name="reader">The reader used to read from the data source.</param>
        /// <param name="writer">The writer used to write to the data source.</param>
        public DataSource(TReader reader, TWriter writer)
        {
            Reader = reader;
            Writer = writer;
        }
    }
}
