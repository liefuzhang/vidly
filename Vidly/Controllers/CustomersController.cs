using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers {
    public class CustomersController : Controller {

        // GET: Customer
        public ActionResult Index() {
            var customers = GetCustomers();

            return View(customers);
        }

        [Route("customers/details/{id}")]
        public ActionResult Details(int id) {
            var customer = GetCustomers().SingleOrDefault((c) => { return c.Id == id; });
            
            if (customer == null) {
                return HttpNotFound();
            }

            return View(customer);
        }

        private IEnumerable<Customer> GetCustomers() {
            return new List<Customer> {
                new Customer {Id=1, Name="John Smith" },
                new Customer {Id = 2, Name = "Mary Williams" }
            };
        }
    }
}