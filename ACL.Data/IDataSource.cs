namespace ACL.Data
{
    /// <summary>
    /// Interface to an object that represents a data source.
    /// </summary>
    /// <typeparam name="TWriter">The type of the writer used to write to the data source.</typeparam>
    /// <typeparam name="TReader">The type of the reader used to read from to the data source.</typeparam>
    public interface IDataSource<TReader, TWriter>
        where TReader: IDataSourceReader
        where TWriter: IDataSourceWriter
    {
        /// <summary>
        /// The reader used to access the data source.
        /// </summary>
        TReader Reader { get; }

        /// <summary>
        /// The writer used to access the data source.
        /// </summary>
        TWriter Writer { get; }
    }
}