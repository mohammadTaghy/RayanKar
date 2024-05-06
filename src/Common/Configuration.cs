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
    }
    public class MongoDatabaseOption
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

    }
}
