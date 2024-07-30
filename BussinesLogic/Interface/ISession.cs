using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_APIs.BussinesLogic.Interface
{
    public interface ISession
    {
        void Set(string key, string value);
        string Get(string key);
        void Set<T>(string key, T value);
        T Get<T>(string key);
    }
}
