using FindIt.Models.Entities;
using FindIt.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FintIt.Controllers
{
    public class SubCategorieController : Controller
    {
        // GET: SubCategorie
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin,Buyer,Employee")]
        public ActionResult AddSubCategorie(SubCategorie subCategorie)
        {
            
            if (subCategorie.Name != null)
            {
      //          if (ModelState.IsValid)
      //          {
                    if (subCategorie.Id != 1 && subCategorie.Id != 0 && SubCategorieManager.GetByIdMainCategorie(subCategorie.MainCategoriesId, subCategorie.Name) == null)
                    {
                        SubCategorieManager.Modify(subCategorie);
                        ModelState.AddModelError("", "vous avez bien modifier la categorie " + subCategorie.Name);
                        subCategorie = null;
                        return View("Index", subCategorie);
                    }
                    if (SubCategorieManager.GetByName(subCategorie.Name) != null && SubCategorieManager.GetByIdMainCategorie(subCategorie.MainCategoriesId, subCategorie.Name) != null)
                    {
                        ModelState.AddModelError("", "cette sous categorie existe deja dans la base de donnees");
                        
                        return View("Index",subCategorie);
                    }
                    SubCategorieManager.Add(subCategorie);
                    return View("Index");
        //        }
            }else if (subCategorie.Id != 0){
                subCategorie = SubCategorieManager.GetById(subCategorie.Id);
                ModelState.AddModelError("", "La categorie est pret a etre modifier");
                return View("Index", subCategorie);
            }
            else
            {
                ModelState.AddModelError("", "Veuillez saisir une sous categorie de produit");
                //   return View(subCategorie);
            }

            return View();
        }
        [Authorize(Roles = "Admin,Buyer,Employee")]
        public ActionResult Delete(int idSubCategorie)
        {
            SubCategorieManager.Delete(idSubCategorie);

            return View("Index");
        }
    }
}