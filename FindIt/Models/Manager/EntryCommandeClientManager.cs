using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindIt.Models.Entities;

namespace FindIt.Models.Manager
{
    public class EntryCommandeClientManager
    {
        public static void Add(EntryCommandeClient entryCommandeClient)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.EntryCommandeClient.Add(entryCommandeClient);
                db.SaveChanges();
            }
        }

        public static EntryCommandeClient GetById(int id, ApplicationDbContext db = null)
        {
            EntryCommandeClient entryCommandeClient = null;
            if (db != null)
            {
                entryCommandeClient = db.EntryCommandeClient.Include("Products").Include("CommandeClient").Where(v => v.Id == id).FirstOrDefault();
            }
            else
            {
                using (db = new ApplicationDbContext())
                {
                    entryCommandeClient = db.EntryCommandeClient.Include("Products").Include("CommandeClient").Where(v => v.Id == id).FirstOrDefault();
                }
            }
            return entryCommandeClient;
        }

        public static void Delete(int entryCommandeClientId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                EntryCommandeClient entryCommandeClient = GetById(entryCommandeClientId, db);

                if (entryCommandeClient != null)
                {
                    db.EntryCommandeClient.Remove(entryCommandeClient);
                }
                db.SaveChanges();
            }
        }

        public static void Modify(EntryCommandeClient newEntryCommandeClient)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                EntryCommandeClient entryCommandeClient = GetById(newEntryCommandeClient.Id, db);

                entryCommandeClient.Quantity = newEntryCommandeClient.Quantity;
                entryCommandeClient.ProductId = newEntryCommandeClient.ProductId;
                entryCommandeClient.Products = newEntryCommandeClient.Products;
                entryCommandeClient.CommandeClientId = newEntryCommandeClient.CommandeClientId;
                entryCommandeClient.CommandeClient = newEntryCommandeClient.CommandeClient;

                db.SaveChanges();
            }
        }

        public static List<EntryCommandeClient> GetAll()
        {
            List<EntryCommandeClient> listEntryCommandeClient = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                listEntryCommandeClient = db.EntryCommandeClient.Include("Products").Include("CommandeClient").OrderBy(c => c.Id).ToList();
            }
            return listEntryCommandeClient;
        }
    }
}