using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hdd.MTConnect.Client.Constants;
using Moq;
using NUnit.Framework;

namespace Hdd.MTConnect.Client.Test
{
    [TestFixture]
    public class ClientTests
    {
        private const string MtConnectDevicesUri = "https://smstestbed.nist.gov/vds/probe";
        private readonly ISchemaMap _schemaMap = CreateSchemaMap();

        private static ISchemaMap CreateSchemaMap()
        {
            var mockSchemaMap = new Mock<ISchemaMap>();
            // https://smstestbed.nist.gov/vds/probe returns data with schema 1.3, but data errors require validation against the 1.4_1.0 schema
            mockSchemaMap.Setup(x => x.Mapping).Returns(new Dictionary<string, string> {{"urn:mtconnect.org:MTConnectDevices:1.3", "MTConnectDevices_1.4_1.0.xsd"}});
            return mockSchemaMap.Object;
        }

        [Test]
        public void Client_Ctor_SchemaMapNull_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new Client(null));
        }

        [Test]
        public void Client_Ctor_SchemaMapNotNull_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new Client(Mock.Of<ISchemaMap>()));
        }

        [Test]
        public void Client_Read_UriNull_Throws()
        {
            var client = new Client(_schemaMap);
            Assert.ThrowsAsync<ArgumentNullException>(async () => await client.Read(null));
        }

        [Test]
        public void Client_Read_InvalidUri_Throws()
        {
            var client = new Client(_schemaMap);
            Assert.ThrowsAsync<UriFormatException>(async () => await client.Read("http://not a valid uri"));
        }

        [Test]
        public async Task Client_ValidUri_Read_DoesNotThrow()
        {
            var client = new Client(_schemaMap);
            await client.Read(MtConnectDevicesUri);
        }

        [Test]
        public async Task Client_Read_HasData()
        {
            var client = new Client(_schemaMap);
            var response = await client.Read(MtConnectDevicesUri);
            Assert.IsNotEmpty(response.Elements());
        }

        [Test]
        public async Task Client_Read_Has_MTConnectDevices_InResponse()
        {
            var client = new Client(_schemaMap);
            var response = await client.Read(MtConnectDevicesUri);
            Assert.IsNotNull(response.Elements().First(element => element.Name.LocalName == ElementNames.MtConnectDevices));
        }
    }
}