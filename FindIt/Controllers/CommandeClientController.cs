using FindIt.Models.Entities;
using FindIt.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindIt.Controllers
{
    [Authorize]
    public class CommandeClientController : Controller
    {
        // GET: CommandClient
        public ActionResult Index()
        {
            ViewBag.CommandeClient = CommandeClientManager.GetAllFor(IdentityManager.GetByUser(User).Id);
            return View();
        }

        [Authorize(Roles="Admin")]
        public ActionResult All(Filtre filtre) {
            ViewBag.CommandeClient = CommandeClientManager.GetAllFor(IdentityManager.GetByUser(User).Id, filtre);
            return View(filtre);
        }


        public ActionResult Delete(int id) {
            CommandeClient commandeClient = CommandeClientManager.GetById(id);
            
            //Si la commandeClient existe et que l'utilisateur est propriétaire de la commande a supprimé
            if (commandeClient != null && commandeClient.ClientId == IdentityManager.GetByUser(User).Id) {
                CommandeClientManager.StopTracking(commandeClient);
            }
            return Index();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteFromAdmin(int id) {
            CommandeClientManager.Delete(id);
            return All(null);
        }
    }
}