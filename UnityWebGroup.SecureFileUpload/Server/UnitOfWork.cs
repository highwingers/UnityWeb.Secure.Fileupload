using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Data.SQLite;
using System.IO;
using System.Configuration;

namespace UnityWebGroup.SecureFileUpload.Server
{
    public class UnitOfWork : IDisposable
    {
        private IDbConnection connection;
        private IDbTransaction transaction;
        public UnitOfWork(bool requireTransaction = true)        {
           
            var path = Path.Combine( AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["db"]);
            connection = new SQLiteConnection("Data Source=" + path);
            connection.Open();
            if (requireTransaction)
            {
                BeginTransaction();
            }
        }

        public void BeginTransaction()
        {
            if (transaction != null)
            {
                return;
            }
            transaction = connection.BeginTransaction();
        }

        public void CommitChanges()
        {
            if (transaction != null)
            {
                transaction.Commit();
            }
        }

        public int Execute(string query, object param = null, CommandType commandType = CommandType.Text)
        {

            return connection.Execute(query, param, transaction, commandType: commandType);
        }

        public IEnumerable<T> Query<T>(string query, object param = null, CommandType commandType = CommandType.Text)
        {
            return connection.Query<T>(query, param, transaction, commandType: commandType);
        }

        public IEnumerable<dynamic> Query(string query, object param = null, CommandType commandType = CommandType.Text)
        {
            return connection.Query(query, param, transaction, commandType: commandType);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (transaction != null)
                {
                    transaction.Dispose();
                    transaction = null;
                }
                if (connection != null)
                {
                    connection.Dispose();
                    connection = null;
                }
            }
        }



        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
