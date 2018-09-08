using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Hdd.MTConnect.Client
{
    public class Client
    {
        private readonly string _uri;

        public Client(string uri)
        {
            _uri = uri ?? throw new ArgumentNullException(nameof(uri));
        }

        public async Task<string> Read()
        {
            return await GetAsync();
        }

        private async Task<string> GetAsync()
        {
            var request = (HttpWebRequest) WebRequest.Create(_uri);

            using (var response = (HttpWebResponse) await request.GetResponseAsync())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        return await reader.ReadToEndAsync();
                    }
                }
            }
        }
    }
}