using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCM.Models
{
    public class RequestsEngineerModifier
    {
        [Required]
        [Display(Name = "Department", ResourceType = typeof(AppViews))]
        public int DepartmentId { get; set; }

        [Required]
        [Display(Name = "RequestsEngineerMod_EngineerId1", ResourceType = typeof(AppViews))]
        public int EngineerId1 { get; set; }

        [Required]
        [Display(Name = "RequestsEngineerMod_EngineerId2", ResourceType = typeof(AppViews))]
        public int EngineerId2 { get; set; }
    }
}