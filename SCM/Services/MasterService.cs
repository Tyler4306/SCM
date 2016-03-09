using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SCM.Services
{
    public class MasterService 
    {
        public IEnumerable<Center> GetCenters()
        {
            string objectNameInCache = "Centers";

            if (Utils.AppCache.GetCached<IEnumerable<Center>>(objectNameInCache) != null)
                return Utils.AppCache.GetCached<IEnumerable<Center>>(objectNameInCache);
            else
            {
                var ctx = new SCMContext();
                var qry = ctx.Centers.OrderBy(x => x.Name);

                Utils.AppCache.SetCached(objectNameInCache, qry.ToList());
                return Utils.AppCache.GetCached<IEnumerable<Center>>(objectNameInCache);
            }
        }

    }
}