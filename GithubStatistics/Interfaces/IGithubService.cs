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
        int Test();
        HttpResponseMessage SearchUser(string name);
        HttpResponseMessage GetUserDetails(string name);

    }
}
