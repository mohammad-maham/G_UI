using System;

namespace G_APIs.Models
{
    public class MessageContext
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public MessageType Type { get; set; }
        public string SessionId { get; set; }
        public DateTime Date { get; set; }

        public MessageContext(string message, string title = "Information", MessageType type = MessageType.Info)
        {
            this.Message = message;
            this.Title = title;
            this.Type = type;
            this.Date = DateTime.Now;
        }
    }

    public enum MessageType
    {
        Info = 0,
        Success = 1,
        Warning = 2,
        Error = 3
    }
}