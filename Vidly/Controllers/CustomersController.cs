using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers {
    public class CustomersController : Controller {
        private ApplicationDbContext _context;
        public CustomersController() {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing) {
            _context.Dispose(); 
        }

        public ActionResult New() {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new NewCustomerViewModel {
                MembershipTypes = membershipTypes
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Customer customer) {

            return View();
        }

        // GET: Customer
        public ActionResult Index() {
            var customers = _context.Customers.Include(c => c.MembershipType).ToList(); 
             
            return View(customers);
        }

        [Route("customers/details/{id}")]
        public ActionResult Details(int id) {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault((c) => c.Id == id);
            
            if (customer == null) {
                return HttpNotFound();
            }

            return View(customer);
        }
        
    }
}