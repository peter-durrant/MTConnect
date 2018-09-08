using System;
using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Hdd.MTConnect.Client.Test
{
    [TestFixture]
    public class ClientTests
    {
        private ClientServicePointManager _clientServicePointManager;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _clientServicePointManager = new ClientServicePointManager(SecurityProtocolType.Tls12);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _clientServicePointManager.Dispose();
        }

        [Test]
        public void Client_Ctor_UriNull_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new Client(null));
        }

        [Test]
        public void Client_InvalidUri_Read_Throws()
        {
            var client = new Client("not a valid uri");
            Assert.ThrowsAsync<UriFormatException>(async () => await client.Read());
        }

        [Test]
        public async Task Client_ValidUri_Read_DoesNotThrow()
        {
            var client = new Client("https://smstestbed.nist.gov/vds/probe");
            await client.Read();
        }

        [Test]
        public async Task Client_Read_HasData()
        {
            var client = new Client("https://smstestbed.nist.gov/vds/probe");
            var response = await client.Read();
            Assert.IsNotEmpty(response);
        }

        [Test]
        public async Task Client_Read_Has_MTConnectDevices_InResponse()
        {
            var client = new Client("https://smstestbed.nist.gov/vds/probe");

            var response = await client.Read();
            Console.WriteLine(response);
            Assert.IsTrue(response.Contains("<MTConnectDevices"));
            Assert.IsTrue(response.Contains("</MTConnectDevices>"));
        }
    }
}