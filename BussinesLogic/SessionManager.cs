using System.Web;
using System.Web.SessionState;
using G_APIs.BussinesLogic.Interface;
using Newtonsoft.Json;

namespace G_APIs.BussinesLogic
{

    public class SessionManager : ISession
    {
        private readonly HttpSessionState _session;

        public SessionManager()
        {
            _session = HttpContext.Current.Session;
        }

        public void Set(string key, string value)
        {
            _session[key] = value;
        }

        public string Get(string key)
        {
            return _session[key] as string;
        }

        public void Set<T>(string key, T value)
        {
            _session[key] = JsonConvert.SerializeObject(value);
        }

        public T Get<T>(string key)
        {
            var value = _session[key] as string;
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
