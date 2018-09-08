using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hdd.MTConnect.Client
{
    public interface IClient
    {
        Task<XDocument> Read(string uri);
    }
}