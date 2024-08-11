using G_APIs.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G_APIs.Services
{
    public static class AlertMessaging
    {
        private static List<MessageContext> _toasts = new List<MessageContext>();

        private static string GetSession()
        {
            return HttpContext.Current.Session.SessionID;
        }
        public static void AddToUserQueue(MessageContext toastr)
        {
            toastr.SessionId = GetSession();
            _toasts.Add(toastr);
        }
        public static void AddToUserQueue(string message, string title, MessageType type)
        {

            var toast = new MessageContext(message, title, type);
            toast.SessionId = GetSession();
            AddToUserQueue(toast);
        }
        public static bool HasQueue()
        {
            return _toasts.Any(t => t.SessionId == GetSession());
        }
        public static void RemoveUserQueue()
        {
            _toasts.RemoveAll(t => t.SessionId == GetSession());
        }
        public static void ClearAll()
        {
            _toasts.Clear();
        }
        public static List<MessageContext> GetUserQueue()
        {
            if (HasQueue())
                return _toasts.Where(t => t.SessionId == GetSession())
                            .OrderByDescending(x => x.Date)
                            .ToList();
            return null;
        }
        public static List<MessageContext> GetAndRemoveUserQueue()
        {
            var list = GetUserQueue();
            RemoveUserQueue();

            return list;
        }
    }
}