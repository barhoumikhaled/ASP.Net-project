using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindIt.Models.Entities;
using System.Web.Mvc;

namespace FindIt.Models.Manager
{
    public class SupplierManager
    {
        public static void Add(Supplier supplier)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Supplier.Add(supplier);
                db.SaveChanges();
            }
        }

        public static Supplier GetById(int id, ApplicationDbContext db = null)
        {
            Supplier supplier = null;
            if (db != null)
            {
                supplier = db.Supplier.Include("Address").Include("Products").Where(v => v.Id == id).FirstOrDefault();
            }
            else
            {
                using (db = new ApplicationDbContext())
                {
                    supplier = db.Supplier.Include("Address").Include("Products").Where(v => v.Id == id).FirstOrDefault();
                }
            }
            return supplier;
        }

        public static void Delete(int supplierId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Supplier supplier = GetById(supplierId, db);

                if (supplier != null)
                {
                    db.Supplier.Remove(supplier);
                }
                db.SaveChanges();
            }
        }

        public static void Modify(Supplier newSupplier)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                Supplier supplier = GetById(newSupplier.Id, db);

                supplier.Name = newSupplier.Name;
                supplier.Description = newSupplier.Description;
                supplier.Phone = newSupplier.Phone;
                supplier.ContactRessource = newSupplier.ContactRessource;
                supplier.Email = newSupplier.Email;
                supplier.AddressId = newSupplier.AddressId;

                db.SaveChanges();
            }
        }

        public static List<Supplier> GetAll()
        {
            List<Supplier> lisSupplier = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                lisSupplier = db.Supplier.Include("Address").Include("Products").OrderBy(c => c.Id).ToList();
            }
            return lisSupplier;
        }

        public static MultiSelectList GetSelectList(int? id)
        {
            int selectedValue = id.HasValue ? id.Value : -1;
            IEnumerable<Supplier> listSupplier = GetAll().OrderBy(s => s.Name);

            return new MultiSelectList(listSupplier, "Id", "Name");
        }

        public static IEnumerable<SelectListItem> GetSelectListItem(int? id)
        {
            int selectedValue = id.HasValue ? id.Value : -1;
            IEnumerable<Supplier> listSupplier = GetAll();
            IEnumerable<SelectListItem> selectListItemSupplier = listSupplier.OrderBy(s => s.Name).Select(s =>
                new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString(),
                    Selected = s.Id == selectedValue
                });
            return selectListItemSupplier;
        }


        public static Supplier GetByCommande(int id)
        {
            CommandeSupplier cs = null;
            Supplier s = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                cs = db.CommandeSupplier.Where(c => c.Id == id).FirstOrDefault();
                s = db.Supplier.Where(c => c.Id == cs.SupplierId).FirstOrDefault();
            }

            return s;
        }
    }
}