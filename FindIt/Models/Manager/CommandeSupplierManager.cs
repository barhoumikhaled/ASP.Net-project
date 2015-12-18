using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindIt.Models.Entities;

namespace FindIt.Models.Manager
{
    public class CommandeSupplierManager
    {
        public static int Add(CommandeSupplier commandeSupplier)
        {
            int nb = 0;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.CommandeSupplier.Add(commandeSupplier);
                nb = db.SaveChanges();
            }

            return nb;
        }

        public static CommandeSupplier GetById(int id, ApplicationDbContext db = null)
        {
            CommandeSupplier commandeSupplier = null;
            if (db != null)
            {
                commandeSupplier = db.CommandeSupplier.Include("Employee").Include("Supplier").Include("EntryCommandeSupplier").Where(v => v.Id == id).FirstOrDefault();
            }
            else
            {
                using (db = new ApplicationDbContext())
                {
                    commandeSupplier = db.CommandeSupplier.Include("Employee").Include("Supplier").Include("EntryCommandeSupplier").Where(v => v.Id == id).FirstOrDefault();
                }
            }
            return commandeSupplier;
        }

        public static void Delete(int commandeSupplierId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                CommandeSupplier address = GetById(commandeSupplierId, db);

                if (address != null)
                {
                    db.CommandeSupplier.Remove(address);
                }
                db.SaveChanges();
            }
        }

        public static void Modify(CommandeSupplier newCommandeSupplier)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                CommandeSupplier commandeSupplier = GetById(newCommandeSupplier.Id, db);

                commandeSupplier.DateCommande = newCommandeSupplier.DateCommande;
                commandeSupplier.EmployeeId = newCommandeSupplier.EmployeeId;
                commandeSupplier.SupplierId = newCommandeSupplier.SupplierId;
                commandeSupplier.Supplier = newCommandeSupplier.Supplier;

                db.SaveChanges();
            }
        }

        public static List<CommandeSupplier> GetAll()
        {
            List<CommandeSupplier> listCommandeSupplier = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                listCommandeSupplier = db.CommandeSupplier.Include("Employee").Include("Supplier").OrderBy(c => c.Id).ToList();
            }
            return listCommandeSupplier;
        }
    }
}