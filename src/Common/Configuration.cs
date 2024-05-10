using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class AppSettings
    {
        public string SecretKey { get; set; }
        public string RabbitMQHostName { get; set; }
        public AppSettings() : this("", "")
        {

        }
        public AppSettings(string secretKey, string rabbitMQHostName)
        {
            SecretKey = secretKey;
            RabbitMQHostName = rabbitMQHostName;
        }

    }
    public class MongoDatabaseOption
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public MongoDatabaseOption() : this("", "")
        {

        }
        public MongoDatabaseOption(string connectionString, string databaseName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
        }

    }
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public DatabaseSettings():this("","")
        {
            
        }
        public DatabaseSettings(string connectionString, string databaseName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
        }


    }
}
