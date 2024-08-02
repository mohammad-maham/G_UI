using System.Web;

namespace G_APIs.Models.ComponentModels
{
    public class FormTitle
    {
        public string Title { get; set; }
        /// <summary>
        ///  hint: just support svg tags
        /// </summary>
        public IHtmlString Icon { get; set; }
        public string Description { get; set; }
    }
}