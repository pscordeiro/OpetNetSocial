using System;
using System.Data;
using System.Data.Common;
using log4net;

namespace OpetNet.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWorkAdo
    {
        private readonly IDbConnection connection;
        private readonly ILog log;
        private IDbTransaction _transaction;

        public UnitOfWork(IDbConnection connection, ILog log)
        {
            this.connection = connection;
            this.log = log;
        }

        public virtual IDbTransaction Transaction
        {
            get
            {
                return _transaction;
            }
            private set
            {
                _transaction = value;
            }
        }

        /// <summary>
        /// Commits the changes
        /// </summary>
        /// <returns>True if the commit was performed successfully.</returns>
        public bool Commit()
        {
            if (Transaction == null)
            {
                log.Debug("The transactions was already committed or there's nothing to commit.");
                return false;
            }

            try
            {
                Transaction.Commit();
                Transaction = null;
                log.Debug("Transaction committed successfully.");
            }
            catch (Exception ex)
            {
                log.Error("An error ocurred commiting the transaction.", ex);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Creates a command to execute queries
        /// </summary>
        /// <returns>IDBCommand ready to execute a query. It is already over a connection and transaction.</returns>
        public DbCommand CreateCommand()
        {

            if (log.IsDebugEnabled) { log.Debug("Start Create Command"); }
            if (connection.State == ConnectionState.Closed)
            {
                if (log.IsDebugEnabled) { log.Debug("Start Create Command - State connection Closed."); }
                connection.Open();
            }

            if (Transaction == null)
            {
                if (log.IsDebugEnabled) { log.Debug("Start Create Command - Transaction == null."); }
                Transaction = connection.BeginTransaction();
            }

            if (log.IsDebugEnabled) { log.Debug("Start Create Command - connection.CreateCommand() as DbCommand."); }
            var command = connection.CreateCommand() as DbCommand;
            if (log.IsDebugEnabled) { log.Debug("Start Create Command - Transaction as DbTransaction"); }
            command.Transaction = Transaction as DbTransaction;
            if (log.IsDebugEnabled) { log.Debug($"Start Create Command - return command - {command}"); }
            return command;
        }

        /// <summary>
        /// Dispose to rollback uncommitted changes and close active connections
        /// </summary>
        public void Dispose()
        {
            if (Transaction != null)
            {
                Transaction.Rollback();
                Transaction = null;
            }

            if (connection != null && connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }
    }
}
