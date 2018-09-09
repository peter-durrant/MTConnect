using System;
using System.Xml.Linq;
using Hdd.MTConnect.Client.Schema;

namespace Hdd.MTConnect.Client
{
    public class Header
    {
        public Header(XDocument doc)
        {
            _ = doc ?? throw new ArgumentNullException(nameof(doc));
            var xmlns = doc.GetDefaultSchemaNamespace();

            Version = doc.Element(xmlns + "MTConnectDevices").Element(xmlns + "Header").Attribute("version").Value;
        }

        public string Version { get; }
    }
}