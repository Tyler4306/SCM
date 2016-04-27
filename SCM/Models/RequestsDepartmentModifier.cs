using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SCM.Models
{
    public class RequestsDepartmentModifier
    {
        [Required]
        [Display(Name = "RequestsDepartmentMod_DepartmentId1", ResourceType = typeof(AppViews))]
        public int DepartmentId1 { get; set; }

        [Required]
        [Display(Name = "RequestsDepartmentMod_DepartmentId2", ResourceType = typeof(AppViews))]
        public int DepartmentId2 { get; set; }
    }
}