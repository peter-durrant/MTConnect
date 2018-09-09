using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Hdd.MTConnect.Client.Schema;

namespace Hdd.MTConnect.Client
{
    public class MTConnectDevices
    {
        private readonly XDocument _doc;

        public MTConnectDevices(XDocument doc)
        {
            _doc = doc ?? throw new ArgumentNullException(nameof(doc));
            Header = new Header(_doc);
            ReadDevices();
        }

        public Header Header { get; }
        public IEnumerable<Device> Devices { get; private set; }

        private void ReadDevices()
        {
            var xmlns = _doc.GetDefaultSchemaNamespace();

            var xmlDevices = _doc.Element(xmlns + "MTConnectDevices").Element(xmlns + "Devices").Elements(xmlns + "Device");
            Devices = xmlDevices.Select(xElement => new Device()).ToList();
        }
    }
}