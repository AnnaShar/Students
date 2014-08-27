﻿using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using HospitalLib.DatebaseModel;

namespace HospitalLib.Providers
{
    public class DatabaseProvider : IDatabaseProvider
    {
        private const string DatabaseFileName = "Database\\HospitalDatabase.mdf";

        public void PushData(string query)
        {
            var connection = CreateConection();
            connection.Open();
            var command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
        }

        public void PushData(SqlCommand command)
        {
            var connection = CreateConection();
            connection.Open();
            command.Connection = connection;
            command.ExecuteNonQuery();
        }

        public SqlDataReader GetData(string query)
        {
            var connection = CreateConection();
            connection.Open();
            var command = new SqlCommand(query, connection);
            var read = command.ExecuteReader();

            return read;
        }

        public SqlDataReader GetData(SqlCommand command)
        {
            var connection = CreateConection();
            connection.Open();
            command.Connection = connection;
            var read = command.ExecuteReader();

            return read;
        }

        public int GetDataScalar(string query)
        {
            var connection = CreateConection();
            connection.Open();
            var command = new SqlCommand(query, connection);
            var reader = command.ExecuteScalar();

            return (int) reader;
        }

        private static SqlConnection CreateConection()
        {
            var outputFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (outputFolder == null) return null;
            var attachDbFilename = Path.Combine(outputFolder, DatabaseFileName);
            var connectionString =
                string.Format(
                    @"Data Source=(LocalDB)\v11.0;Initial Catalog=HospitalDatabase;AttachDbFilename=""{0}"";Integrated Security=True",
                    attachDbFilename);

            return new SqlConnection(connectionString);
        }
    }
}
