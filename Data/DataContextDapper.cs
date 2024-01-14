using System.Data; // called directives
using System.Globalization;
using Dapper;
using Microsoft.Data.SqlClient;
// using Microsoft.Data.SqlClient;

namespace DotnetAPI
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
    }
}