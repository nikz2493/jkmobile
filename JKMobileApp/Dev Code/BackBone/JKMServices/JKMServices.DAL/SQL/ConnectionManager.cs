using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Resources;
using Utility.Logger;

namespace JKMServices.DAL.SQL
{
    public static class ConnectionManager
    {
        public enum SqlCommandType : byte { Query = 1, StoreProcedure };

        private static SqlConnection sqlConnection;
        private static SqlCommand sqlCommand;
        private static readonly ResourceManager resourceManager;
        private static readonly Logger loggerObject;
        private static readonly LoggerStackTrace loggerStackTrace;

        static ConnectionManager()
        {
            resourceManager = new System.Resources.ResourceManager("JKMServices.DAL.Resource", System.Reflection.Assembly.GetExecutingAssembly());
            //Create the objects for logger as new. This object can't be injected in static class. 
            loggerStackTrace = new LoggerStackTrace();
            loggerObject = new Logger(loggerStackTrace);
        }

        /// <summary>
        /// Property Name   : GetSQLConnection
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 29 Nov 2017
        /// Purpose         : Get SQL Connection
        /// Revision        : 
        /// </summary>
        private static void GetSQLConnection()
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["JKMDBContext"].ConnectionString);
            sqlConnection.Open();
            loggerObject.Info(resourceManager.GetString("sqlConnectionOpened"));
            sqlCommand = new SqlCommand
            {
                Connection = sqlConnection
            };
        }

        /// <summary>
        /// Property Name   : CloseSQLConnection
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 29 Nov 2017
        /// Purpose         : Close SQL Connection 
        /// Revision        : 
        /// </summary>
        private static void CloseSQLConnection()
        {
            if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
            {
                loggerObject.Info(resourceManager.GetString("sqlConnectionClosed"));
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// Property Name   : GetQuery
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 29 Nov 2017
        /// Purpose         : Close SQL Connection 
        /// Revision        : 
        /// </summary>
        private static string GetQuery(string queryName)
        {
            string sqlQuery = resourceManager.GetString(queryName);
            return sqlQuery;
        }

        /// <summary>
        /// Property Name   : ModifyData
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 29 Nov 2017
        /// Purpose         : Save data by using dynamic command
        /// Revision        : By Ranjana Singh on 02nd Dec 2017 : Changed method name from SaveData to ModifyData.
        /// 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public static int ModifyData(string queryName, Dictionary<string, dynamic> ParametersList, bool executeMultipleUpdate = false, int queryType = (int)SqlCommandType.Query)
        {
            int returnValue;
            SqlTransaction sqlTransaction = null;
            try
            {
                GetSQLConnection();
                sqlTransaction = sqlConnection.BeginTransaction(IsolationLevel.ReadCommitted);
                sqlCommand.Transaction = sqlTransaction;
                sqlCommand.CommandText = GetQuery(queryName);
                if (ParametersList != null)
                {
                    if (executeMultipleUpdate)
                    {
                        // Query must accept parameter values in {0} and {1} format.
                        sqlCommand.CommandText = string.Format(sqlCommand.CommandText, ParametersList["UpdateValues"], ParametersList["WhereConditionValue"]);
                    }
                    else
                    {
                        foreach (var Parameter in ParametersList.Keys)
                        {
                            sqlCommand.Parameters.AddWithValue(Parameter.ToString(), ParametersList[Parameter]);
                        }
                    }
                }
                returnValue = 0;
                if (queryType == (int)SqlCommandType.StoreProcedure)
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                }
                returnValue = sqlCommand.ExecuteNonQuery();
                sqlTransaction.Commit();
                loggerObject.Info(resourceManager.GetString("sqlQuerySuccessful") + sqlCommand.CommandText);
                return returnValue;
            }
            catch (Exception exceptionObject)
            {
                loggerObject.Info(resourceManager.GetString("sqlQueryFailed"), exceptionObject);
                sqlTransaction.Rollback();
                return -1;
            }
            finally
            {
                CloseSQLConnection();
            }
        }

        /// <summary>
        /// Property Name   : GetData
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 29 Nov 2017
        /// Purpose         : Get data by using dynamic command
        /// Revision        : By Ranjana Singh on 04th Dec 2017 : Changed return type from default ex to null.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public static DataTable GetData(string queryName, Dictionary<string, dynamic> parametersList)
        {
            SqlDataAdapter sqlDataAdapter;
            DataSet dsReturn;
            try
            {
                GetSQLConnection();
                sqlCommand.CommandText = GetQuery(queryName);

                if (parametersList != null)
                {
                    foreach (var Parameter in parametersList.Keys)
                    {
                        sqlCommand.Parameters.AddWithValue(Parameter.ToString(), parametersList[Parameter]);
                    }
                }
                sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = sqlCommand;
                dsReturn = new DataSet();
                sqlDataAdapter.Fill(dsReturn);
                loggerObject.Info(resourceManager.GetString("sqlQuerySuccessful") + sqlCommand.CommandText);
                return dsReturn.Tables[0];
            }
            catch (Exception exceptionObject)
            {
                loggerObject.Info(resourceManager.GetString("sqlQueryFailed"), exceptionObject);
                return null;
            }
            finally
            {
                CloseSQLConnection();
            }
        }
    }
}
