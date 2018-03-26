using System.Net.Http;
using System.Threading.Tasks;

namespace JKMWindowsService.Utility
{
    public interface IAPIHelper
    {
        string GetAPIResponseStatusCodeMessage(HttpResponseMessage responseMessage);
        Task<HttpResponseMessage> InvokeDeleteAPI(string apiName);
        HttpResponseMessage InvokeGetAPI(string apiName);
        HttpResponseMessage InvokePostAPI(string apiName, string requestBody);
        Task<HttpResponseMessage> InvokePutAPI<T>(string apiName, T body);
    }
}