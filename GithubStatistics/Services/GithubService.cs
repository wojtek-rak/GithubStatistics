using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GithubStatistics.Interfaces;

namespace GithubStatistics.Services
{
    public class GithubService : IGithubService
    {
        private static readonly HttpClient client = new HttpClient();
        public string SearchUser()
        {
            var responseString = client.GetStringAsync("http://www.example.com/recepticle.aspx");
            return responseString.Result;
        }

        public int Test()
        {
            return 1;
        }
    }
}
