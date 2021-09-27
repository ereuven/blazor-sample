namespace Talent.Configuration
{
    public class MongoDBSettings
    {
        public const string ConfigSectionName = "Mongo";

        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }
}
