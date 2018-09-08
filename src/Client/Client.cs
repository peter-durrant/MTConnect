using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
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

                var xmlNamespace = doc.Root.Attribute("xmlns").Value;
                if (!_schemaMap.Mapping.ContainsKey(xmlNamespace))
                {
                    throw new InvalidOperationException("Namespace mapping not configured");
                }

                var schema = LoadSchema(_schemaMap.Mapping[xmlNamespace]);
                doc.Validate(schema, ValidationCallback);
            });

            return doc;
        }

        private static XmlSchemaSet LoadSchema(string schemaFileName)
        {
            var schemaPath = Path.Combine(AssemblyHelpers.AssemblyDirectory, "Schema", schemaFileName);
            var reader = new XmlTextReader(schemaPath);
            var schema = XmlSchema.Read(reader, ValidationCallback);

            var schemaSet = new XmlSchemaSet();
            schemaSet.Add(schema);
            return schemaSet;
        }

        private static void ValidationCallback(object sender, ValidationEventArgs args)
        {
            // could tolerate warning - stay strict for now
            if (args.Severity == XmlSeverityType.Warning || args.Severity == XmlSeverityType.Error)
            {
                throw new XmlSchemaValidationException(args.Message, args.Exception);
            }
        }
    }
}