using System;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Hdd.MTConnect.Client
{
    public class MTConnectDevices
    {
        private readonly XDocument _doc;

        public MTConnectDevices(XDocument doc)
        {
            _doc = doc ?? throw new ArgumentNullException(nameof(doc));
            ReadData();
        }

        public MTConnectDevicesType Data { get; private set; }

        private void ReadData()
        {
            using (var reader = XmlReader.Create(_doc.Root.CreateReader(), new XmlReaderSettings()))
            {
                reader.MoveToContent();
                var serializer = new XmlSerializer(typeof(MTConnectDevicesType));
                Data = (MTConnectDevicesType) serializer.Deserialize(reader);
                reader.Close();
            }
        }
    }
}