using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace DataLayer
{
    public class Database : IDatabase
    {
        private static readonly object padlock = new object();
        private static IDatabase instance;
        private SqlCommand command;
        private SqlConnection connection;
        private SqlDataReader reader;

        #region Singleton stuff

        private Database()
        {
            OpenConnection();
        }

        public static IDatabase GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new Database();
                }
                return instance;
            }
        }

        #endregion Singleton stuff

        #region Public methods

        public void CloseConnection()
        {
            connection.Close();
        }

        public SqlConnection GetConnection()
        {
            return connection;
        }

        public void OpenConnection()
        {
            connection = new SqlConnection();

            // connection.txt should be where the 'watson_hotel.sln' file is.
            // View README.md for information on how to connect to the databse.
            connection.ConnectionString = File.ReadAllLines(@"..\..\..\connection.txt")[0];

            try
            {
                connection.Open();
            }
            catch
            {
                throw new Exception("Failed to open a connection to the database");
            }
        }

        #endregion Public methods

        #region Query executors

        public bool ExecuteInsert(string sql, SqlParameter[] sqlParams)
        {
            try
            {
                return ExecuteNonQuery(sql, sqlParams);
            }
            catch
            {
                throw new Exception("Failed to insert the data into the database");
            }
        }

        public bool ExecuteUpdate(string sql, SqlParameter[] sqlParams)
        {
            try
            {
                return ExecuteNonQuery(sql, sqlParams);
            }
            catch
            {
                throw new Exception("Failed to update the data in the database");
            }
        }

        public bool ExecuteDelete(string sql, SqlParameter[] sqlParams)
        {
            try
            {
                return ExecuteNonQuery(sql, sqlParams);
            }
            catch
            {
                throw new Exception("Failed to delete the data from the database");
            }
        }

        public List<T> ExecuteSelectMultiple<T>(string sql, Func<SqlDataReader, T> readerRowToType)
        {
            List<T> list = new List<T>();

            try
            {
                // Construct the prepared statement (command).
                using (command = new SqlCommand(sql, connection))
                {
                    command.Prepare();

                    // Execute the command.
                    using (reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                list.Add(readerRowToType(reader));
                            }
                        }
                    }
                }

                return list;
            }
            catch
            {
                throw new Exception("Failed to retrieve data from the database");
            }
            finally
            {
                command.Parameters.Clear();
            }
        }

        public List<T> ExecuteSelectMultiple<T>(string sql, SqlParameter[] sqlParams, Func<SqlDataReader, T> readerRowToType)
        {
            List<T> list = new List<T>();

            try
            {
                // Construct the prepared statement (command).
                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(sqlParams);
                    command.Prepare();

                    // Execute the command.
                    using (reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                list.Add(readerRowToType(reader));
                            }
                        }
                    }
                }

                return list;
            }
            catch
            {
                throw new Exception("Failed to retrieve data from the database");
            }
            finally
            {
                command.Parameters.Clear();
            }
        }

        public T ExecuteSelectSingle<T>(string sql, SqlParameter[] sqlParams, Func<SqlDataReader, T> readerRowToType)
        {
            // Declaring the objects that will be used and the sql statement.
            T returnObject;

            try
            {
                // Construct the prepared statement (command).
                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(sqlParams);
                    command.Prepare();

                    // Execute the command.
                    using (reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            // Reads the next record of the result of the command (in this case it's the first row).
                            reader.Read();

                            // Set the employee object that we will return).
                            returnObject = readerRowToType(reader);
                        }
                        else
                        {
                            throw new Exception("Failed to retrieve data from the database");
                        }
                    }
                }

                return returnObject;
            }
            catch
            {
                throw new Exception("Failed to retrieve data from the database");
            }
            finally
            {
                command.Parameters.Clear();
            }
        }

        private bool ExecuteNonQuery(string sql, SqlParameter[] sqlParams)
        {
            try
            {
                int rowsAffected = 0;

                // Construct the prepared statement (command).
                using (command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(sqlParams);
                    command.Prepare();

                    // Execute the command.
                    rowsAffected = command.ExecuteNonQuery();
                }

                return rowsAffected > 0;
            }
            finally
            {
                command.Parameters.Clear();
            }
        }

        #endregion Query executors
    }
}