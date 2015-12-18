using FindIt.Models.Entities;
using FindIt.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace FindIt.Controllers
{
    public class CommandeSupplierController : Controller
    {
        // GET: CommandeSupplier
        public ActionResult Index()
        {
            List<CommandeSupplier> listCommandeSupplier = CommandeSupplierManager.GetAll();
            return View(listCommandeSupplier);
        }

        public ActionResult Add()
        {
            ViewBag.SupplierList = SupplierManager.GetSelectListItem(null);
            string idUserCurrent = User.Identity.GetUserId();
            return View();
        }

        public ActionResult Details(int id)
        {
            CommandeSupplier cs = CommandeSupplierManager.GetById(id);
            List<EntryCommandeSupplier> listEntryCommandeSupplier = EntryCommandeSupplierManager.GetByCommandeID(id);
            Address addresse = AddressManager.GetByCommandeID(id);
            cs.EntryCommandeSupplier = listEntryCommandeSupplier;
            cs.Supplier.Address = addresse;
            if (cs != null)
            {
                return View(cs);
            }
            else
            {
                return View("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(CommandeSupplier commande)
        {
            ViewBag.SupplierList = SupplierManager.GetSelectListItem(null);
            string idUserCurrent = User.Identity.GetUserId();
            DateTime date = DateTime.Now;
            
            if (ModelState.IsValid)
            {
                commande.EmployeeId = idUserCurrent;
                commande.DateCommande = date;
                int nb = 0;
                nb = CommandeSupplierManager.Add(commande);
                return RedirectToAction("Index");
            }
            else
            {
                return View(commande);
            }

        }

        public ActionResult AddEntry()
        {
            List<Supplier> listSupplier = SupplierManager.GetAll();
            ViewBag.SupplierList = SupplierManager.GetSelectListItem(null);
            ViewBag.ProductList = ProductManager.GetSelectListItem(null);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEntry(EntryCommandeSupplier entry)
        {
            CommandeSupplier cs = CommandeSupplierManager.GetById(entry.Id);
            Supplier supplier = SupplierManager.GetByCommande(entry.Id);
            //ViewBag.SelectedSupplierID = KeyNotFoundException;
            ViewBag.SupplierList = SupplierManager.GetSelectListItem(null);
            ViewBag.ProductList = ProductManager.GetSelectListItem(null);

            if (ModelState.IsValid)
            {
                entry.CommandeSupplierId = entry.Id;
                
                int nb = 0;
                nb = EntryCommandeSupplierManager.Add(entry);
                return RedirectToAction("Add");
            }
            else
            {
                return View(entry);
            }
        }
    }
}