using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EF.DataProtection.Sample.Tests.Common
{
    public static class HttpContentExtensions
    {
        public static async Task<T> ReadAsAsync<T>(this HttpContent content)
        { 
            var stream = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(stream);
        }
    }
}