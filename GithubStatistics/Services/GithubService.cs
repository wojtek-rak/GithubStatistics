using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using GithubStatistics.Interfaces;

namespace GithubStatistics.Services
{
    public class GithubService : IGithubService
    {
        public static readonly HttpClient client = new HttpClient();
        public GithubService()
        {
            client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "http://developer.github.com/v3/#user-agent-required");
        }

        public HttpResponseMessage SearchUser(string name)
        {
            return SendGetRequest($"https://api.github.com/search/users?q={name}");
        }

        public HttpResponseMessage GetUserDetails(string name)
        {
            return SendGetRequest($"https://api.github.com/users/{name}");
        }
        private HttpResponseMessage SendGetRequest(string url)
        {
            try
            {
                return client.GetAsync(url).Result;
            }
            catch (HttpRequestException e)
            {
                throw new ConnectionException();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int Test()
        {
            return 1;
        }

    }
}
