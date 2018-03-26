using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JKMWindowsService.Utility
{
    /// <summary>
    /// Class Name      : SerializeHttpResponse
    /// Author          : Pratik Soni
    /// Creation Date   : 14 Feb 2018
    /// Purpose         : To deserialize HttpResponseMessage
    /// Revision        : 
    /// </summary> 
    public static class SerializeHttpResponse<T>
    {
        public async static Task<T> Deserialize(HttpResponseMessage responseMessage)
        {
            string apiresponseString = await responseMessage.Content.ReadAsStringAsync();

            apiresponseString = JToken.Parse(apiresponseString).ToString();
            T response = JsonConvert.DeserializeObject<T>(apiresponseString);

            return response;
        }
    }
}
