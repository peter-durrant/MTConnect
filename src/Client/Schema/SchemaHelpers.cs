using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Hdd.MTConnect.Client.Schema
{
    public static class SchemaHelpers
    {
        public static void LoadSchemaAndValidateDoc(this XDocument doc, ISchemaMap schemaMap, string schemaRootPath)
        {
            var xmlNamespace = doc.Root.Attribute("xmlns").Value;
            if (!schemaMap.Mapping.ContainsKey(xmlNamespace))
            {
                throw new InvalidOperationException("Namespace mapping not configured");
            }

            var schemaPath = Path.Combine(schemaRootPath, schemaMap.Mapping[xmlNamespace]);

            var reader = new XmlTextReader(schemaPath);
            var schema = XmlSchema.Read(reader, ValidationCallback);

            var schemaSet = new XmlSchemaSet();
            schemaSet.Add(schema);

            doc.Validate(schemaSet, ValidationCallback);
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