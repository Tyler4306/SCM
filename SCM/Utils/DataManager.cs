using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using SCM.Models;

namespace SCM.Utils
{
    public class DataManager
    {
        public static IEnumerable<ServiceRequest> Requests()
        {
            if (HttpContext.Current.Cache["Requests"] == null)
            {
                var ctx = new SCMContext();

                var qry = (from x in ctx.ServiceRequests.Include(x => x.Customer).Include(x => x.Product).Include(x => x.Tags).Include(x => x.Customer.Tags)
                           where x.StatusId < 90 && !x.IsDeleted
                           orderby x.RequestDate descending
                           select x);

                
                //HttpContext.Current.Cache["Requests"] = qry.ToList();
                HttpContext.Current.Cache.Insert("Requests", 
                    qry.ToList(), null, 
                    System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 15, 0));


            }

            return (IEnumerable <ServiceRequest>) HttpContext.Current.Cache["Requests"] ;
        }

        public static IEnumerable<Customer> Customers()
        {
            if (HttpContext.Current.Cache["Customers"] == null)
            {
                var ctx = new SCMContext();

                var qry = (from x in ctx.Customers.Include(x => x.Region).Include(x => x.City).Include(x => x.Tags)
                           select x);


                //HttpContext.Current.Cache["Customers"] = qry.ToList();
                HttpContext.Current.Cache.Insert("Customers",
                    qry.ToList(), null,
                    System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 25, 0));

            }

            return (IEnumerable<Customer>)HttpContext.Current.Cache["Customers"];
        }

        public static IEnumerable<Tag> Tags()
        {
            if(HttpContext.Current.Cache["Tags"] == null)
            {
                var ctx = new SCMContext();
                var qry = ctx.Tags.OrderBy(x=>x.Name);

                HttpContext.Current.Cache.Insert("Tags",
                    qry.ToList(), null,
                    System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 25, 0));
            }

            return HttpContext.Current.Cache["Tags"] as IEnumerable<Tag >;
        }

        public static IEnumerable<Center> Centers()
        {
            if (HttpContext.Current.Cache["Centers"] == null)
            {
                var ctx = new SCMContext();
                var qry = ctx.Centers;

                HttpContext.Current.Cache.Insert("Centers",
                    qry.ToList(), null,
                    System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(1, 0, 0));
            }

            return HttpContext.Current.Cache["Centers"] as IEnumerable<Center>;
        }



        public static IEnumerable<City> Cities()
        {
            if (HttpContext.Current.Cache["Cities"] == null)
            {
                var ctx = new SCMContext();
                var qry = ctx.Cities.OrderBy(x=>x.Name);

                HttpContext.Current.Cache.Insert("Cities",
                    qry.ToList(), null,
                    System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 25, 0));
            }

            return HttpContext.Current.Cache["Cities"] as IEnumerable<City>;
        }

        public static IEnumerable<Region> Regions()
        {
            if (HttpContext.Current.Cache["Regions"] == null)
            {
                var ctx = new SCMContext();
                var qry = ctx.Regions.OrderBy(x=> x.Name);

                HttpContext.Current.Cache.Insert("Regions",
                    qry.ToList(), null,
                    System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 25, 0));
            }

            return HttpContext.Current.Cache["Regions"] as IEnumerable<Region>;
        }

        public static IEnumerable<Department> Departments()
        {
            if (HttpContext.Current.Cache["Departments"] == null)
            {
                var ctx = new SCMContext();
                var qry = ctx.Departments.OrderBy(x => x.Name);

                HttpContext.Current.Cache.Insert("Departments",
                    qry.ToList(), null,
                    System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 20, 0));
            }
            return HttpContext.Current.Cache["Departments"] as IEnumerable<Department>;
        }

        public static IEnumerable<Product> Products()
        {
            if (HttpContext.Current.Cache["Products"] == null)
            {
                var ctx = new SCMContext();
                var qry = ctx.Products.OrderBy(x=>x.Name);

                HttpContext.Current.Cache.Insert("Products",
                    qry.ToList(), null,
                    System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 20, 0));
            }
            return HttpContext.Current.Cache["Products"] as IEnumerable<Product>;
        }

        public static IEnumerable<Engineer> Engineers()
        {
            if (HttpContext.Current.Cache["Engineers"] == null)
            {
                var ctx = new SCMContext();
                var qry = ctx.Engineers.OrderBy(x => x.Name); ;

                HttpContext.Current.Cache.Insert("Engineers",
                    qry.ToList(), null,
                    System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 20, 0));
            }
            return HttpContext.Current.Cache["Engineers"] as IEnumerable<Engineer>;
        }
    }
}