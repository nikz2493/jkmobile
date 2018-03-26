using System.Net.Http;

namespace JKMWindowsService.Utility
{
    public interface IClientHelper
    {
        HttpClient GetAuthenticateClient();
    }
}