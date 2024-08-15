using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace G_APIs.Models
{
    public class GoldRepositoryManagementVM
    {
        public GoldRepositoryStatusVM GoldRepositoryStatus { get; set; }
        [Display(Name = "نوع طلا")]
        public List<SelectListItem> GoldTypes { get; set; }
        [Display(Name = "عیار")]
        public List<SelectListItem> GoldCarats { get; set; }
        [Display(Name = "مقدار طلا")]
        public double Weight { get; set; }
    }
}