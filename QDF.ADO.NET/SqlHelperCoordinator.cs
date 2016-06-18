using System.Data;
using System.Data.SqlClient;

namespace QDF.ADO.NET
{
    public sealed class SqlHelperCoordinator
    {
        public static int ExecuteNonQuery(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            var db=DbSelector.SelectDb(commandText);
            return SqlHelper.ExecuteNonQuery(db.ConnectionString, commandType, commandText, commandParameters);
        }
        public static object ExecuteScalar(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            var db = DbSelector.SelectDb(commandText);
            return SqlHelper.ExecuteScalar(db.ConnectionString, commandType, commandText, commandParameters);
        }
        public static SqlDataReader ExecuteReader(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            var db = DbSelector.SelectDb(commandText);
            return SqlHelper.ExecuteReader(db.ConnectionString, commandType, commandText, commandParameters);
        }
    }
}