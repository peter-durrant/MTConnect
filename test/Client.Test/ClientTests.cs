using System;
using System.Threading.Tasks;
using Hdd.MTConnect.Client.Constants;
using NUnit.Framework;

namespace Hdd.MTConnect.Client.Test
{
    [TestFixture]
    public class ClientTests
    {
        private const string MtConnectDevicesUri = "https://smstestbed.nist.gov/vds/probe";

        [Test]
        public void Client_Read_UriNull_Throws()
        {
            var client = new Client();
            Assert.ThrowsAsync<ArgumentNullException>(async () => await client.Read(null));
        }

        [Test]
        public void Client_Read_InvalidUri_Throws()
        {
            var client = new Client();
            Assert.ThrowsAsync<UriFormatException>(async () => await client.Read("http://not a valid uri"));
        }

        [Test]
        public async Task Client_ValidUri_Read_DoesNotThrow()
        {
            var client = new Client();
            await client.Read(MtConnectDevicesUri);
        }

        [Test]
        public async Task Client_Read_HasData()
        {
            var client = new Client();
            var response = await client.Read(MtConnectDevicesUri);
            Assert.IsNotEmpty(response.Elements());
        }

        [Test]
        public async Task Client_Read_Has_MTConnectDevices_InResponse()
        {
            var client = new Client();
            var response = await client.Read(MtConnectDevicesUri);
            Assert.IsNotNull(response.Elements(ElementNames.MtConnectDevices));
        }
    }
}