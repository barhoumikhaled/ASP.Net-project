using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindIt.Models.Manager;
using FindIt.Models.Entities;

namespace FindIt.Controllers
{
    [Authorize(Roles = "Admin,Employee,Buyer")]
    public class SupplierController : Controller
    {

        public ActionResult Index()
        {
            List<Supplier> list = SupplierManager.GetAll();
            return View(list);
        }

        public ActionResult Details(int id)
        {
            Supplier s = SupplierManager.GetById(id);
            return View(s);
        }

        public ActionResult Create(int id)
        {
            Supplier s = new Supplier();
            s.AddressId = id;
            return View(s);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Supplier s)
        {
            if (ModelState.IsValid)
            {
                SupplierManager.Add(s);
                return RedirectToAction("Index", "Supplier");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            Supplier s = SupplierManager.GetById(id);
            return View(s);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Supplier s)
        {
            if (ModelState.IsValid)
            {
                Supplier supplier = new Supplier();
                supplier.Id = s.Id;
                supplier.Name = s.Name;
                supplier.Phone = s.Phone;
                supplier.Products = s.Products;
                supplier.AddressId = s.AddressId;
                supplier.Address = s.Address;
                supplier.ContactRessource = s.ContactRessource;
                supplier.Description = s.Description;
                supplier.Email = s.Email;
                SupplierManager.Modify(s);
                return RedirectToAction("Index", "Supplier");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            SupplierManager.Delete(id);
            return RedirectToAction("Index", "Supplier");
        }
    }
}
