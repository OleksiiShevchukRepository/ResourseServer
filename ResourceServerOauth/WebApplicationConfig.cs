using System.Configuration;
using MySolution.Core;

namespace ResourceServerOauth
{
    public class WebApplicationConfig : IWebApplicationConfig
    {
        public string MongoDbName => ConfigurationManager.AppSettings.Get("MongoDbName");
        public string SqlDbConnectionName => ConfigurationManager.AppSettings.Get("SqlUsers");
        public string MongoDbConnection => ConfigurationManager.AppSettings.Get("MongoDbConnection").Replace("{DB_NAME}", MongoDbName);
    }
}