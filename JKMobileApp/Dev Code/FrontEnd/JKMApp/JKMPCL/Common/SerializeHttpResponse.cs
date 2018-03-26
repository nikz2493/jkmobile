using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JKMPCL
{
    /// <summary>
    /// Class Name      : SerializeHttpResponse
    /// Author          : Hiren Patel
    /// Creation Date   : 29 Dec 2017
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
