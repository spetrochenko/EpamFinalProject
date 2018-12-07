﻿using System.Data;
using System.Data.SqlClient;
using EC.DataAccess.Helpers.Interface;

namespace EC.DataAccess.Helpers.Implementation
{
    public class CreateQuery : ICreateQuery
    {
        private IDbCommand _sqlCommand;
        private IDbConnection _sqlConnection;

        public ICreateQuery CreateConnection()
        {
            _sqlConnection = new SqlConnection(DbConstants.ConnectionString);

            return this;
        }

        public ICreateQuery CreateCommand(string name)
        {
            _sqlCommand = new SqlCommand(name, _sqlConnection as SqlConnection)
            {
                CommandType = CommandType.StoredProcedure
            };

            return this;
        }

        public ICreateQuery AddParameters(params IDataParameter[] parameters)
        {
            foreach (var item in parameters)
            {
                _sqlCommand.Parameters.Add(item);
            }

            return this;
        }

        public void ExecuteQuery()
        {
            using (_sqlConnection)
            {
                _sqlConnection.Open();

                _sqlCommand.ExecuteNonQuery();
            }
        }

        public IDataReader ExecuteReader()
        {
            IDataReader executedQuery = null;

            using (_sqlConnection)
            {
                _sqlConnection.Open();

                executedQuery = _sqlCommand.ExecuteReader();
            }

            return executedQuery;
        }

        public int ExecuteScalar()
        {
            int id = default(int);

            using (_sqlConnection)
            {
                _sqlConnection.Open();

                id = (int)_sqlCommand.ExecuteScalar();
            }

            return id;
        }
    }
}
