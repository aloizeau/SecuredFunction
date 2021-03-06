using Microsoft.Azure.Services.AppAuthentication;
using System.Net.Http.Headers;
namespace BlazorWebApp.Data
{
    public class FunctionAPI
    {
        public async Task<string> GetSampleAsync()
        {
            // Acquire the access token.
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            string accessToken = await azureServiceTokenProvider.GetAccessTokenAsync("api://08e708ba-8da4-411d-8c7e-8aff9ddb6c3e");

            // Use the access token to call a protected web API.
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            client.DefaultRequestHeaders.Add("x-functions-key", "ZGfh3gf66aTWcwNff0fMwMmR_M41kZl5gbeKYIufP8CRAzFuUAR-qA==");
            var response = await client.GetAsync("https://imi-securedfunction.azurewebsites.net/api/Sample");
            return await response.Content.ReadAsStringAsync() + "| accessToken: " + accessToken;
        }
    }
}