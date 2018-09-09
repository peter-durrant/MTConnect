using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using Hdd.MTConnect.Client.Schema;
using Hdd.Utility;

namespace Hdd.MTConnect.Client
{
    public class Client : IClient
    {
        private readonly ISchemaMap _schemaMap;

        public Client(ISchemaMap schemaMap)
        {
            _schemaMap = schemaMap ?? throw new ArgumentNullException(nameof(schemaMap));
        }

        public async Task<XDocument> Read(string uri)
        {
            _ = uri ?? throw new ArgumentNullException(nameof(uri));
            return await GetAsync(uri);
        }

        private async Task<XDocument> GetAsync(string uri)
        {
            XDocument doc = null;
            await Task.Run(() =>
            {
                doc = XDocument.Load(uri);
                var schemaRootPath = AssemblyHelpers.AssemblyDirectory(Assembly.GetExecutingAssembly(), "Schema");
                doc.LoadSchemaAndValidateDoc(_schemaMap, schemaRootPath);
            });

            return doc;
        }
    }
}