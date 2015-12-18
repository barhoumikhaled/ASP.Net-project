using FindIt.Models.Entities;
using FindIt.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindIt.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult Add(Comment comment)
        {
            if (Request.Form["CommentId"] != null)
                comment.Id = Int32.Parse(Request.Form["CommentId"]);
            else
                comment.Id = 0;

            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    if (RatingManager.GetUserRating(comment.ApplicationUserId, comment.ProductId) != null)
                    {
                        if (comment.Id != 0)
                        {
                            
                            CommentManager.Modify(comment);
                        }
                        else
                        {
                            CommentManager.Add(comment);
                        }
                        //   ModelState.AddModelError("comments", "Ecrivez un commentaire avant d'envoyer");
                        TempData["MessageErreur"] = "";
                    }
                    else
                    {
                        TempData["ErreurRating"] = "vous devez donnez une note avant de commenter";
                    }
                }
                else
                {
                    TempData["MessageErreur"] = "Ecrivez un commentaire avant d'envoyer";
                }
            }
            else
            {
                TempData["MessageErreur"] = "Vous devez vous connectez pour commenter ce produit";
            }
            return RedirectToAction("Details", "product", new { id = comment.ProductId });
        }
        public ActionResult Delete(int commentid, int produitid)
        {
            CommentManager.Delete(commentid);
            return RedirectToAction("Details", "product", new { id = produitid });
        }
    }
}