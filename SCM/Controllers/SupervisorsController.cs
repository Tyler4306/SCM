using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCM.Controllers
{
    public class SupervisorsController : Controller
    {
        
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var ctx = new SCM.Models.ApplicationDbContext();
            var users = ctx.Users.ToDictionary(x => x.Id, y => y.UserName);
            var roles = ctx.Roles.ToDictionary(x => x.Name, y => y.Id);
            string id = roles["Supervisor"];
            var supervisors = ctx.Roles.Where(x => x.Id == id).First().Users.Select(x => x.UserId).ToList();
            ;
            var model = new List<SCM.Models.SupervisorViewModel>();
            foreach(var item in users)
            {
                var svm = new SCM.Models.SupervisorViewModel()
                {
                    Id = item.Key,
                    Name = item.Value,
                    IsSupervisor = supervisors.Contains(item.Key)
                };
                model.Add(svm);
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public void Save(string idList)
        {
            var list = idList.Split(',');
            var sm = new Utils.SecurityManager();
            foreach (var id in idList.Split(','))
            {
                if(!string.IsNullOrEmpty(id))
                {
                    sm.ToggleUserRole(id, "Supervisor");
                }
            }
        }
    }
}