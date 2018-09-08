using System;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hdd.MTConnect.Client
{
    public class Client : IClient
    {
        public async Task<XDocument> Read(string uri)
        {
            _ = uri ?? throw new ArgumentNullException(nameof(uri));
            return await GetAsync(uri);
        }

        private static async Task<XDocument> GetAsync(string uri)
        {
            XDocument doc = null;
            await Task.Run(() => { doc = XDocument.Load(uri); });
            return doc;
        }
    }
}