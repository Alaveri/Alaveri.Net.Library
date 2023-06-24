using System.Data;
using System.Data.Common;

namespace Alaveri.Data
{
    /// <summary>
    /// Interface to an object that accesses a database.
    /// </summary>
    public interface IDatabaseAccessor : IDisposable
    {
        /// <summary>
        /// Starts a transaction.
        /// </summary>
        /// <param name="name">The name of this transaction.</param>
        /// <param name="isolationLevel">The isolation level of the transaction.</param>
        /// <returns>the new transaction.</returns>
        IDbTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified, string name = "");

        /// <summary>
        /// If true, the accessor will truncate the length of string paramters to the specified
        /// parameter size.
        /// </summary>
        bool TruncateStringParameters { get; set; }

        /// <summary>
        /// The connection to the database.
        /// </summary>
        DbConnection Connection { get; }

        /// <summary>
        /// The connection string used to access the database.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the wait time (in seconds) before terminating the attempt to execute a command and generating an error.
        /// </summary>
        TimeSpan ConnectionTimeout { get; set; }

        /// <summary>
        /// Begins building of a stored procedure query.
        /// </summary>
        /// <param name="procedureName">The name of the stored procedure to execute.</param>
        /// <returns>An IDatabaseQueryOptions object containing chainable functions to further build the query.</returns>
        IDatabaseQuery StoredProcedure(string procedureName);
    }
}