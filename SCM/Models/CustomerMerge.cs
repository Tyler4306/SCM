using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCM.Models
{
    public class CustomerMerge
    {
        [Required]
        [Display(Name = "CustomerMerge_CustomerId1", ResourceType = typeof(AppViews))]
        public int CustomerId1 { get; set; }
        [Required]
        [Display(Name = "CustomerMerge_CustomerId2", ResourceType = typeof(AppViews))]
        public int CustomerId2 { get; set; }
    }
}