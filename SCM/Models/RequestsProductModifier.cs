using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCM.Models
{
    public class RequestsProductModifier
    {
        [Required]
        [Display(Name = "Department", ResourceType = typeof(AppViews))]
        public int DepartmentId { get; set; }

        [Required]
        [Display(Name = "RequestsProductMod_ProductId1", ResourceType = typeof(AppViews))]
        public string ProductId1 { get; set; }

        [Required]
        [Display(Name = "RequestsProductMod_ProductId2", ResourceType = typeof(AppViews))]
        public string ProductId2 { get; set; }
    }
}