using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Proxy.Azure
{
    public interface IAzureProxy
    {
        Task<T> Get<T>(string url);
        Task<T> Post<T>(string url, object body);
    }
}
