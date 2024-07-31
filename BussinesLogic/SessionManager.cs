using G_APIs.BussinesLogic.Interface;
using Newtonsoft.Json;
using System.Web;
using System.Web.SessionState;

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
            return !(_session[key] is string value) ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
