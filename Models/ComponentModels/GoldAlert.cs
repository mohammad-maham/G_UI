using System.Collections.Generic;

namespace G_APIs.Models.ComponentModels
{
    public class GoldAlert
    {
        public string AlertTitleIcon { get; set; }

        public GoldAlertTypes AlertType { get; set; }

        public string AlertTitleText { get; set; }

        public List<string> AlertContentTexts { get; set; }
    }

    public enum GoldAlertTypes
    {
        Info = 1,
        Warning = 2,
        Danger = 3,
        Success = 4
    }
}