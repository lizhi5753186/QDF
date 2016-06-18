using System;
using QDF.Configuration;
using QDF.Utils;

namespace QDF.ADO.NET
{
    public class DbSelector
    {
        public static Db SelectDb(string sql)
        {
            var redirect2WritableDb = false;
            sql = sql.Trim().TrimStart('\r').TrimStart('\n');
            if (sql.IndexOf("UPDATE", StringComparison.OrdinalIgnoreCase) >= 0)
                redirect2WritableDb = true;
            if (sql.IndexOf("DELETE", StringComparison.OrdinalIgnoreCase) >= 0)
                redirect2WritableDb = true;
            if (sql.IndexOf("INSERT", StringComparison.OrdinalIgnoreCase) >= 0)
                redirect2WritableDb = true;
            if (sql.IndexOf("--WRITE", StringComparison.OrdinalIgnoreCase) == 0)    //强制sql方式进入写db操作
                redirect2WritableDb = true;

            if (redirect2WritableDb)
                return DispatcherConfiguration.WritableDb;

            var random = RandomHelper.GetRandom(0, 1001);
            var dbIndex = random % DispatcherConfiguration.ReadDbs.Count;
            return DispatcherConfiguration.ReadDbs[dbIndex];
        }
    }
}
