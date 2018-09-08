using System.Collections.Generic;
using System.IO;
using Hdd.Utility;
using NUnit.Framework;

namespace Hdd.MTConnect.Client.Test
{
    [TestFixture]
    public class SchemaMapTests
    {
        [Test]
        public void SchemaMap()
        {
            var expectedMapping = new Dictionary<string, string> {{"a", "1"}, {"b", "2"}};
            var path = Path.Combine(AssemblyHelpers.AssemblyDirectory, "TestData", "SchemaMap.json");
            var schemaMap = new SchemaMap(path);
            CollectionAssert.AreEqual(expectedMapping, schemaMap.Mapping);
        }
    }
}