﻿using ServiceCollectionAPI.Helpers.Interfaces;

namespace ServiceCollectionAPI.Helpers
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}
