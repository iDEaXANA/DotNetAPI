using System.Data; // called directives
using System.Data.Common;
using System.Globalization;
using Dapper;
using Microsoft.Data.SqlClient;
// using Microsoft.Data.SqlClient;

namespace DotnetAPI.Data
{
    class DataContextDapper
    {
        private readonly IConfiguration _config;

        public DataContextDapper(IConfiguration config) //Constructor accesses settings i.e connection strings
        {
            _config = config; // private makes it available within class.
        }

        public IEnumerable<T> LoadData<T>(string sql) // T allows for all data types.
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.Query<T>(sql);
        }

        public T LoadDataSingle<T>(string sql)
        {

            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.QuerySingle<T>(sql);
        }

        public bool ExecuteSql(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.Execute(sql) > 0;
        }

        public int ExecuteSqlWithRowCount(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.Execute(sql);
        }

        public bool ExecuteSqlWithParameters(string sql, DynamicParameters parameters)
        {

            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.Execute(sql, parameters) > 0;

            // SqlCommand commandWithParams = new SqlCommand(sql);

            // foreach (SqlParameter parameter in parameters)
            // {
            //     commandWithParams.Parameters.Add(parameter);
            // }
            // SqlConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection")); // Sql instead of IDb to match 3 lines down.
            // dbConnection.Open();

            // commandWithParams.Connection = dbConnection;

            // int rowsAffected = commandWithParams.ExecuteNonQuery(); // doesn't return result set 

            // dbConnection.Close();

            // return rowsAffected > 0;
        }

        public IEnumerable<T> LoadDataWithParameters<T>(string sql, DynamicParameters parameters) // T allows for all data types.
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.Query<T>(sql, parameters);
        }

        public T LoadDataSingleWithParameters<T>(string sql, DynamicParameters parameters)
        {

            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.QuerySingle<T>(sql, parameters);
        }
    }
}