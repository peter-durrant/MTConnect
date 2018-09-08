using System.Collections.Generic;

namespace Hdd.MTConnect.Client
{
    public interface ISchemaMap
    {
        Dictionary<string, string> Mapping { get; }
    }
}