using System.Collections.Generic;
using QDF.Exceptions;

namespace QDF.Configuration
{
    public class DispatcherConfiguration
    {
        public static Db WritableDb { get; set; }

        public static List<Db> ReadDbs { get; set; }

        public DispatcherConfiguration()
        {
            ReadDbs = new List<Db>();
        }

        public static void Init()
        {
            var configElement = ConfigManager.LoadConfig();
            var root = configElement.XElement.Element("SQLDispatcher");

            if (root == null)
                throw new QdfException("SQLDispatcher Node not exists");

            var xWriteDbElement = root.Element("WritableDb");
            if (xWriteDbElement == null)
                throw new QdfException("WritableDb Node not exists");
            var xReadDbElement = root.Element("ReadDBs");
            if (xReadDbElement == null)
                throw new QdfException("ReadDBs Node not exists");

            WritableDb = new Db() { ConnectionString = xWriteDbElement.Value };

            foreach (var elm in xReadDbElement.Elements("DB"))
            {
                ReadDbs.Add(new Db { ConnectionString = elm.Value });
            }
        }
    }

    public class Db
    {
        public string ConnectionString { get; set; } 
    }
}