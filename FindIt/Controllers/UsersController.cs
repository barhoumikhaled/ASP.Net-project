using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindIt.Models;
using FindIt.Models.Manager;

namespace FindIt.Controllers
{
    
    public class UsersController : Controller
    {
        // GET: Users
        [Authorize(Roles="Admin")]
        public ActionResult Index()
        {
            List<ApplicationUser> lstUsers = IdentityManager.GetAll();
            return View(lstUsers);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Details(string id)
        {
            //id != null
            if (id != null)
            {
                ApplicationUser user = IdentityManager.GetById(id);
                user.Adress = AddressManager.GetByIdJoinCountry(user.AdressId);

                if (user != null)
                {
                    return View(user);
                }
                else
                {
                    return View("Index");
                }
            }
            else
            {
                return new HttpStatusCodeResult(403);
            }
        }

        public ActionResult Edit(string id)
        {
            if (id != null)
            {
                ApplicationUser user = IdentityManager.GetById(id);
                user.Adress = AddressManager.GetByIdJoinCountry(user.AdressId);
                if (user != null)
                {
                    return View(user);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return new HttpStatusCodeResult(403);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                //some other validations if needed
                IdentityManager.Edit(user);
                ModelState.Clear();
                return RedirectToAction("Details", new { id = user.Id });
            }
            else
            {
                return View(user);
            }
        }



        public ActionResult Delete(string id)
        {
            IdentityManager.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ApplicationUser user)
        {
            int affectedRowNum = 0;
            if (this.ModelState.IsValid)
            {


                affectedRowNum = IdentityManager.AddUser(user);
                if (affectedRowNum != 0)
                {
                    this.ModelState.Clear();
                    return RedirectToAction("Index");
                }
                else
                {
                    return Content("add failed");
                }
            }
            else
            {
                return View(user);
            }
        }

    }
}