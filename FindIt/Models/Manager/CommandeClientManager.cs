using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindIt.Models.Entities;

namespace FindIt.Models.Manager
{
    public class CommandeClientManager
    {
        public static void Add(CommandeClient commandeClient)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.CommandeClient.Add(commandeClient);
                db.SaveChanges();
            }
        }

        public static CommandeClient GetById(int id, ApplicationDbContext db = null)
        {
            CommandeClient commandeClient = null;
            if (db != null)
            {
                commandeClient = db.CommandeClient.Include("Client").Include("Address").Where(v => v.Id == id).FirstOrDefault();
            }
            else
            {
                using (db = new ApplicationDbContext())
                {
                    commandeClient = db.CommandeClient.Include("Client").Include("Address").Where(v => v.Id == id).FirstOrDefault();
                }
            }
            return commandeClient;
        }

        public static void Delete(int commandeClientId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                CommandeClient commandeClient = GetById(commandeClientId, db);

                if (commandeClient != null)
                {
                    db.CommandeClient.Remove(commandeClient);
                }
                db.SaveChanges();
            }
        }

        //TODO ?? On pourrait en modifier l'adresse de livraison du client, dans le cas ou ce que client s'aurait trompé dans son adresse
        public static void Modify(CommandeClient newCommandeClient)
        {
           /* using (ApplicationDbContext db = new ApplicationDbContext())
            {

                CommandeClient commandeClient = GetById(newCommandeClient.Id, db);

                //commandeClient.DateCommande = newCommandeClient.DateCommande;
                //commandeClient.ClientId = newCommandeClient.ClientId;
                //commandeClient.DeliveryAddressId = newCommandeClient.DeliveryAddressId;

                db.SaveChanges();
            }*/
        }

        public static List<CommandeClient> GetAll(Filtre filtre)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var listCommandeClient = db.CommandeClient.Include("Client").Include("DeliveryAddress").OrderBy(c => c.Id);
                if (filtre != null)
                    listCommandeClient.Where(cc =>
                        (filtre.Username == null || cc.Client.UserName.Equals(filtre.Username, StringComparison.InvariantCultureIgnoreCase))
                        && (filtre.MinimumDate == null || cc.DateCommande >= filtre.MinimumDate)
                        && (filtre.MaximumDate == null || cc.DateCommande <= filtre.MaximumDate));
                
                return listCommandeClient.ToList();
            }
        }

        public static List<CommandeClient> GetAllFor(string userID, Filtre filtre = null) {
            using (ApplicationDbContext db = new ApplicationDbContext()) {
                var listCommandeClient = db.CommandeClient.Include("Client").Include("DeliveryAddress").Where(cc => cc.ClientId.Equals(userID) && cc.isTrack).OrderByDescending(cc => cc.DateCommande);
                if (filtre != null)
                    listCommandeClient.Where(cc =>
                        (filtre.Username == null || cc.Client.UserName.Equals(filtre.Username, StringComparison.InvariantCultureIgnoreCase))
                        && (filtre.MinimumDate == null || cc.DateCommande >= filtre.MinimumDate)
                        && (filtre.MaximumDate == null || cc.DateCommande <= filtre.MaximumDate));

                return listCommandeClient.ToList();
            }
        }

        public static void StopTracking(CommandeClient commandeClient) {
            using (ApplicationDbContext db = new ApplicationDbContext()) {
                commandeClient = GetById(commandeClient.Id, db);
                if (commandeClient != null) {
                    commandeClient.isTrack = false;
                    db.SaveChanges();
                }
            }
        }
    }
}