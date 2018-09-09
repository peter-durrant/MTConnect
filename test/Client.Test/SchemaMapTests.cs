using System.Collections.Generic;
using System.Reflection;
using Hdd.Utility;
using NUnit.Framework;

namespace Hdd.MTConnect.Client.Test
{
    [TestFixture]
    public class SchemaMapTests
    {
        [Test]
        public void SchemaMap_ReadTestData_CreatedMapping()
        {
            var expectedMapping = new Dictionary<string, string> {{"a", "1"}, {"b", "2"}};
            var path = AssemblyHelpers.AssemblyDirectory(Assembly.GetExecutingAssembly(), "TestData", "SchemaMap.json");
            var schemaMap = new SchemaMap(path);
            CollectionAssert.AreEqual(expectedMapping, schemaMap.Mapping);
        }
    }
}