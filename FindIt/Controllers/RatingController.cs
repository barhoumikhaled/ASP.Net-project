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
    [AllowAnonymous]
    public class RatingController : Controller
    {
        // GET: Rating
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult Add(Rating rating)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    if (RatingManager.GetUserRating(User.Identity.GetUserId(), rating.ProductId) == null)
                    {
                        rating.ApplicationUserId = User.Identity.GetUserId();
                        RatingManager.Add(rating);
                        TempData["ErreurRating"] = "";
                        Product produit = ProductManager.GetById(rating.ProductId);
                        return PartialView("_AjoutDeNote", produit);
                    }
                    else
                    {
                        Rating note = RatingManager.GetUserRating(User.Identity.GetUserId(), rating.ProductId);
                        note.Score = rating.Score;
                        note.ApplicationUserId = User.Identity.GetUserId();
                        RatingManager.Modify(note);
                        Product produit = ProductManager.GetById(rating.ProductId);
                        return PartialView("_AjoutDeNote", produit);
                    }
                }
                else
                {
                    TempData["ErreurRating"] = "erreur lors de l'annotation";
                }
            }
            else
            {
                TempData["ErreurRating"] = "Vous devez vous connectez pour noter ce produit";
            }
            return RedirectToAction("Details", "product", new { id = rating.ProductId });
        }
    }
}