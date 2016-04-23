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
        public static object requestsLock = new Object();
        public static object customersLock = new Object();
        public static object citiesLock = new Object();
        public static object regionsLock = new Object();
        public static object departmentsLock = new Object();
        public static object productsLock = new Object();
        public static object engineersLock = new Object();
        public static object tagsLock = new Object();

        public static IEnumerable<ServiceRequest> Requests()
        {
            Customers();
            lock(requestsLock)
            {
                if (HttpContext.Current.Cache["Requests"] == null)
                {
                    var ctx = new SCMContext();

                    var qry = (from x in ctx.ServiceRequests.Include(x => x.Customer).Include(x => x.Product).Include(x => x.Engineer).Include(x => x.PendingReason).Include(x => x.CancelReason).Include(x => x.Tags).Include(x => x.Customer.Tags)
                               where (x.StatusId < 90 && !x.IsDeleted) ||
                                (x.StatusId >= 90 && x.ClosingDate.HasValue && x.ClosingDate.Value.Year == DateTime.Today.Year && !x.IsDeleted)
                               orderby x.RequestDate descending
                               select x);


                    //HttpContext.Current.Cache["Requests"] = qry.ToList();
                    HttpContext.Current.Cache.Insert("Requests",
                        qry.ToList(), null,
                        System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 15, 0));


                }
            }

            return (IEnumerable <ServiceRequest>) HttpContext.Current.Cache["Requests"] ;
        }

        public static void AddRequest(int id)
        {
            lock(requestsLock)
            {
                if (HttpContext.Current.Cache["Requests"] != null)
                {
                    var list = (List<ServiceRequest>)HttpContext.Current.Cache["Requests"];
                    var ctx = new SCMContext();
                    var request = ctx.ServiceRequests.Find(id);
                    if (request != null)
                    {
                        list.Add(request);
                        HttpContext.Current.Cache["Requests"] = list.OrderByDescending(x => x.RequestDate).ToList();
                    }
                }
            }
        }

        public static void ChangeRequest(int id)
        {
            Requests();
            lock (requestsLock)
            {
                if (HttpContext.Current.Cache["Requests"] != null)
                {
                    var list = (List<ServiceRequest>)HttpContext.Current.Cache["Requests"];
                    var ctx = new SCMContext();
                    var request = ctx.ServiceRequests.Find(id);
                    if (request != null)
                    {
                        
                        int index = list.FindIndex(x => x.Id == id);
                        list.RemoveAt(index);
                        
                        if (request.StatusId < 90 || (request.StatusId >= 90 && request.ClosingDate.HasValue && request.ClosingDate.Value.Year == DateTime.Now.Year))
                        {
                            list.Insert(index, request);
                        }
                        HttpContext.Current.Cache["Requests"] = list.OrderByDescending(x => x.RequestDate).ToList();
                    }
                }
            }
        }

        public static void ResetRequests()
        {
            HttpContext.Current.Cache.Remove("Requests");
            Requests();
        }

        public static IEnumerable<Customer> Customers()
        {
            lock(customersLock)
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
            }

            return (IEnumerable<Customer>)HttpContext.Current.Cache["Customers"];
        }

        public static void AddCustomer(int id)
        {
            lock (customersLock)
            {
                if (HttpContext.Current.Cache["Customers"] != null)
                {
                    var list = (List<Customer>)HttpContext.Current.Cache["Customers"];
                    var ctx = new SCMContext();
                    var customer = ctx.Customers.Find(id);
                    if (customer != null)
                    {
                        list.Add(customer);
                        HttpContext.Current.Cache["Customers"] = list.OrderByDescending(x => x.Name).ToList();
                    }
                }
            }
        }

        public static void ChangeCustomer(int id)
        {
            Customers();
            lock (customersLock)
            {
                if (HttpContext.Current.Cache["Customers"] != null)
                {
                    var list = (List<Customer>)HttpContext.Current.Cache["Customers"];
                    var ctx = new SCMContext();
                    var customer = ctx.Customers.Find(id);
                    if (customer != null)
                    {
                        list.RemoveAll(x => x.Id == id);
                        list.Add(customer);
                        
                        HttpContext.Current.Cache["Customers"] = list.OrderByDescending(x => x.Name).ToList();
                        var relatedRequests = customer.ServiceRequests.Where(x => x.StatusId < 90).Select(x => x.Id).ToList();
                        if (relatedRequests.Count > 0)
                        {
                            foreach(var i in relatedRequests)
                            {
                                ChangeRequest(i);
                            }
                        }
                    }
                }
            }
        }

        public static void ResetCustomers()
        {
            HttpContext.Current.Cache.Remove("Customers");
            Customers();
        }

        public static IEnumerable<Tag> Tags()
        {
            lock(tagsLock)
            {
                if (HttpContext.Current.Cache["Tags"] == null)
                {
                    var ctx = new SCMContext();
                    var qry = ctx.Tags.OrderBy(x => x.Name);

                    HttpContext.Current.Cache.Insert("Tags",
                        qry.ToList(), null,
                        System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 25, 0));
                }
            }

            return HttpContext.Current.Cache["Tags"] as IEnumerable<Tag >;
        }

        public static void ResetTags()
        {
            HttpContext.Current.Cache.Remove("Tags");
            Tags();
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
            lock(citiesLock)
            {
                if (HttpContext.Current.Cache["Cities"] == null)
                {
                    var ctx = new SCMContext();
                    var qry = ctx.Cities.OrderBy(x => x.Name);

                    HttpContext.Current.Cache.Insert("Cities",
                        qry.ToList(), null,
                        System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 25, 0));
                }
            }

            return HttpContext.Current.Cache["Cities"] as IEnumerable<City>;
        }

        public static void ResetCities()
        {
            HttpContext.Current.Cache.Remove("Cities");
            Cities();
        }

        public static IEnumerable<Region> Regions()
        {
            lock(regionsLock)
            {
                if (HttpContext.Current.Cache["Regions"] == null)
                {
                    var ctx = new SCMContext();
                    var qry = ctx.Regions.OrderBy(x => x.Name);

                    HttpContext.Current.Cache.Insert("Regions",
                        qry.ToList(), null,
                        System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 25, 0));
                }
            }
            return HttpContext.Current.Cache["Regions"] as IEnumerable<Region>;
        }

        public static void ResetRegions()
        {
            HttpContext.Current.Cache.Remove("Regions");
            Regions();
        }

        public static IEnumerable<Department> Departments()
        {
            lock(departmentsLock)
            {
                if (HttpContext.Current.Cache["Departments"] == null)
                {
                    var ctx = new SCMContext();
                    var qry = ctx.Departments.OrderBy(x => x.Name);

                    HttpContext.Current.Cache.Insert("Departments",
                        qry.ToList(), null,
                        System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 20, 0));
                }
            }
            return HttpContext.Current.Cache["Departments"] as IEnumerable<Department>;
        }

        public static void ResetDepartments()
        {
            HttpContext.Current.Cache.Remove("Departments");
            Departments();
        }

        public static IEnumerable<Product> Products()
        {
            lock(productsLock)
            {
                if (HttpContext.Current.Cache["Products"] == null)
                {
                    var ctx = new SCMContext();
                    var qry = ctx.Products.OrderBy(x => x.Name);

                    HttpContext.Current.Cache.Insert("Products",
                        qry.ToList(), null,
                        System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 20, 0));
                }
            }
            return HttpContext.Current.Cache["Products"] as IEnumerable<Product>;
        }

        public static void ResetProducts()
        {
            HttpContext.Current.Cache.Remove("Products");
            Products();
        }

        public static IEnumerable<Engineer> Engineers()
        {
            lock(engineersLock)
            {
                if (HttpContext.Current.Cache["Engineers"] == null)
                {
                    var ctx = new SCMContext();
                    var qry = ctx.Engineers.OrderBy(x => x.Name); ;

                    HttpContext.Current.Cache.Insert("Engineers",
                        qry.ToList(), null,
                        System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 20, 0));
                }
            }
            return HttpContext.Current.Cache["Engineers"] as IEnumerable<Engineer>;
        }

        public static void ResetEngineers()
        {
            HttpContext.Current.Cache.Remove("Engineers");
            Engineers();
        }
    }
}