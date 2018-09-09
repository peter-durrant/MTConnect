using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Hdd.Utility;
using NUnit.Framework;

namespace Hdd.MTConnect.Client.Test
{
    [TestFixture]
    public class MTConnectDevicesTests
    {
        [Test]
        public void MTConnectDevices_ReadsHeaderVersion()
        {
            var responseFilePath = AssemblyHelpers.AssemblyDirectory(Assembly.GetExecutingAssembly(), "TestData", "MTConnectDevices.xml");
            var response = XDocument.Load(responseFilePath);
            var devices = new MTConnectDevices(response);
            Assert.AreEqual("1.4.0.10", devices.Header.Version);
        }

        [Test]
        public void MTConnectDevices_HasCorrectNumberOfDevices()
        {
            var responseFilePath = AssemblyHelpers.AssemblyDirectory(Assembly.GetExecutingAssembly(), "TestData", "MTConnectDevices.xml");
            var response = XDocument.Load(responseFilePath);
            var devices = new MTConnectDevices(response);
            Assert.AreEqual(8, devices.Devices.Count());
        }
    }
}