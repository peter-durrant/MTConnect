using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hdd.MTConnect.Client
{
    public sealed class SchemaMap : ISchemaMap
    {
        private readonly string _configFilePath;

        public SchemaMap(string configFilePath)
        {
            _configFilePath = configFilePath ?? throw new ArgumentNullException(nameof(configFilePath));

            Load();
        }

        public Dictionary<string, string> Mapping { get; private set; }

        private void Load()
        {
            using (var reader = File.OpenText(_configFilePath))
            {
                var json = (JObject) JToken.ReadFrom(new JsonTextReader(reader));
                Mapping = JsonConvert.DeserializeObject<Dictionary<string, string>>(json["schema"].ToString());
            }
        }
    }
}