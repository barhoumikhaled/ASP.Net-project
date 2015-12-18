using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindIt.Models.Entities;

namespace FindIt.Models.Manager
{
    public class EntryCommandeSupplierManager
    {
        public static int Add(EntryCommandeSupplier entryCommandeSupplier)
        {
           int retour = 0;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.EntryCommandeSupplier.Add(entryCommandeSupplier);
                retour = db.SaveChanges();
            }
            return retour;
        }

        public static EntryCommandeSupplier GetById(int id, ApplicationDbContext db = null)
        {
            EntryCommandeSupplier entryCommandeSupplier = null;
            if (db != null)
            {
                entryCommandeSupplier = db.EntryCommandeSupplier.Include("Products").Include("CommandeSupplier").Where(v => v.Id == id).FirstOrDefault();
            }
            else
            {
                using (db = new ApplicationDbContext())
                {
                    entryCommandeSupplier = db.EntryCommandeSupplier.Include("Products").Include("CommandeSupplier").Where(v => v.Id == id).FirstOrDefault();
                }
            }
            return entryCommandeSupplier;
        }

        public static void Delete(int entryCommandeSupplierId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                EntryCommandeSupplier entryCommandeSupplier = GetById(entryCommandeSupplierId, db);

                if (entryCommandeSupplier != null)
                {
                    db.EntryCommandeSupplier.Remove(entryCommandeSupplier);
                }
                db.SaveChanges();
            }
        }

        public static void Modify(EntryCommandeSupplier newEntryCommandeSupplier)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                EntryCommandeSupplier entryCommandeSupplier = GetById(newEntryCommandeSupplier.Id, db);

                entryCommandeSupplier.Quantity = newEntryCommandeSupplier.Quantity;
                entryCommandeSupplier.ProductId = newEntryCommandeSupplier.ProductId;
                entryCommandeSupplier.Products = newEntryCommandeSupplier.Products;
                entryCommandeSupplier.CommandeSupplierId = newEntryCommandeSupplier.CommandeSupplierId;
                entryCommandeSupplier.CommandeSupplier = newEntryCommandeSupplier.CommandeSupplier;

                db.SaveChanges();
            }
        }

        public static List<EntryCommandeSupplier> GetAll()
        {
            List<EntryCommandeSupplier> listEntryCommandeSupplier = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                listEntryCommandeSupplier = db.EntryCommandeSupplier.Include("Products").Include("CommandeSupplier").OrderBy(c => c.Id).ToList();
            }
            return listEntryCommandeSupplier;
        }

        public static List<EntryCommandeSupplier> GetByCommandeID(int id)
        {
            List<EntryCommandeSupplier> lst = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                lst = db.EntryCommandeSupplier.Include("Products").Where(e => e.CommandeSupplierId == id).ToList();
            }

            return lst;
        }
    }
}