using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindIt.Models;
using FindIt.Models.Entities;
using FindIt.Models.Manager;

namespace FindIt.Controllers
{
    [Authorize]
    public class AddressController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            //Uri url = Request.UrlReferrer;
            // url.
            return View();
        }

        public ActionResult CreateAddressForUser(string UserId)
        {
            Address address = new Address();
            address.UserId = UserId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Address newAddress)
        {
            int addId;
            if (ModelState.IsValid)
            {
                if (!AddressManager.ExistInDb(newAddress))
                {
                    AddressManager.Add(newAddress);
                    addId = newAddress.Id;
                }
                else
                {
                    addId = AddressManager.GetIDByData(newAddress);
                }
                return RedirectToAction("Create", "Supplier", new { id = addId });
            }
            else
            {
                return View(newAddress);
            }
        }

        public ActionResult Edit(int id, int supplierId)
        {
            Address address = AddressManager.GetById(id);
            address.SupplierId = supplierId;
            return View(address);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Address newAddress)
        {
            Supplier s = SupplierManager.GetById((int)newAddress.SupplierId.Value);
            if (ModelState.IsValid)
            {
                if (!AddressManager.ExistInDb(newAddress))
                {
                    AddressManager.Add(newAddress);
                    s.AddressId = newAddress.Id;
                    SupplierManager.Modify(s);
                }
                else
                {
                    s.AddressId = AddressManager.GetIDByData(newAddress);
                    SupplierManager.Modify(s);
                }
                return RedirectToAction("Index", "Supplier");
            }
            else
            {
                return View(newAddress);
            }
        }

        //Partial View for when Adding/Modifying Address of User/Supplier
        [AllowAnonymous]
        public ActionResult ProvinceView(int id)
        {
            ViewBag.countryId = id;
            return PartialView();
        }
    }
}
