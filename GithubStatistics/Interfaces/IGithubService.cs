using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GithubStatistics.Interfaces
{
    public interface IGithubService
    {
        HttpResponseMessage SearchUser(string name);
        HttpResponseMessage GetUserDetails(string name);

    }
}
