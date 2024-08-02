namespace G_APIs.Models.ComponentModels
{
    public class GoldButton : BaseModel
    {
        public string Icon { get; set; } = "fa fa-home fa-2x";
        public string Class { get; set; } = "btn";
        public string Text { get; set; } = "ثبت";
        public GoldButtonTypes ButtonType { get; set; } = GoldButtonTypes.button;
        public GoldButtonSchemas Schema { get; set; } = GoldButtonSchemas.button;
        public bool Readonly { get; set; } = false;
        public bool Disabled { get; set; } = false;
        public bool IsRTL { get; set; } = true;
        public string LinkTarget { get; set; } = "_self";
        public string Href { get; set; } = "#";
        public string OnClick { get; set; }
    }

    public enum GoldButtonTypes
    {
        none = 0,
        button = 1,
        submit = 2,
        cancel = 3
    }

    public enum GoldButtonSchemas
    {
        link = 1,
        button = 2,
    }
}