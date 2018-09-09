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
        public void MTConnectDevices_Data_HasCorrectHeaderVersion()
        {
            var responseFilePath = AssemblyHelpers.AssemblyDirectory(Assembly.GetExecutingAssembly(), "TestData", "MTConnectDevices.xml");
            var response = XDocument.Load(responseFilePath);
            var devices = new MTConnectDevices(response);
            Assert.AreEqual("1.4.0.10", devices.Data.Header.version);
        }

        [Test]
        public void MTConnectDevices_Data_HasCorrectNumberOfDevices()
        {
            var responseFilePath = AssemblyHelpers.AssemblyDirectory(Assembly.GetExecutingAssembly(), "TestData", "MTConnectDevices.xml");
            var response = XDocument.Load(responseFilePath);
            var devices = new MTConnectDevices(response);
            Assert.AreEqual(8, devices.Data.Devices.Length);
        }

        [Test]
        public void MTConnectDevices_Data_DevicesHaveCorrectDeviceIds()
        {
            var responseFilePath = AssemblyHelpers.AssemblyDirectory(Assembly.GetExecutingAssembly(), "TestData", "MTConnectDevices.xml");
            var response = XDocument.Load(responseFilePath);
            var devices = new MTConnectDevices(response).Data.Devices;
            Assert.AreEqual("GFAgie01", devices[0].id);
            Assert.AreEqual("Mazak01", devices[1].id);
            Assert.AreEqual("Mazak03", devices[2].id);
            Assert.AreEqual("Hurco01", devices[3].id);
            Assert.AreEqual("Hurco02", devices[4].id);
            Assert.AreEqual("Hurco03", devices[5].id);
            Assert.AreEqual("Hurco04", devices[6].id);
            Assert.AreEqual("Hurco06", devices[7].id);
        }

        [Test]
        public void MTConnectDevices_Data_DevicesHaveCorrectNumberOfDataItems()
        {
            var responseFilePath = AssemblyHelpers.AssemblyDirectory(Assembly.GetExecutingAssembly(), "TestData", "MTConnectDevices.xml");
            var response = XDocument.Load(responseFilePath);
            var devices = new MTConnectDevices(response).Data.Devices;
            Assert.AreEqual(5, devices[0].DataItems.Length);
            Assert.AreEqual(3, devices[1].DataItems.Length);
            Assert.AreEqual(3, devices[2].DataItems.Length);
            Assert.AreEqual(3, devices[3].DataItems.Length);
            Assert.AreEqual(3, devices[4].DataItems.Length);
            Assert.AreEqual(3, devices[5].DataItems.Length);
            Assert.AreEqual(3, devices[6].DataItems.Length);
            Assert.AreEqual(3, devices[7].DataItems.Length);
        }
    }
}