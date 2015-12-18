using FindIt.Models.Entities;
using FindIt.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindIt.Controllers
{
    public class MainCategorieController : Controller
    {
        // GET: Categorie
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AddMainCategorie(MainCategories mainCategorie)
        {
          
            if (mainCategorie.Name != null)
            {
       //         if (ModelState.IsValid)
       //         {
                    //modifier une categorie
                    if (mainCategorie.Id != 0 && MainCategoriesManager.GetByName(mainCategorie.Name) == null)
                    {
                        MainCategoriesManager.Modify(mainCategorie);
                        ModelState.AddModelError("", "vous avez bien modifier la categorie "+mainCategorie.Name);
                        mainCategorie = null;
                        return View("Index", mainCategorie);
                    }
                    // verifier l'existence d'une categorie
                    if (MainCategoriesManager.GetByName(mainCategorie.Name) != null)
                    {
                        ModelState.AddModelError("", "cette categorie existe deja dans la base de donnees");
                        return View("Index", mainCategorie);
                    }
                    //ajout de categorie
                    MainCategoriesManager.Add(mainCategorie);
                    ModelState.AddModelError("", "la categorie a bien ete ajouter");
                    return View("Index");
        //        }
            }else if(mainCategorie.Id != 0){
                mainCategorie = MainCategoriesManager.GetById(mainCategorie.Id);
                ModelState.AddModelError("", "La categorie est pret a etre modifier");
                return View("Index", mainCategorie);
            }else{
                ModelState.AddModelError("", "Veuillez saisir une categorie de produit");
           //     return View(mainCategorie);
            }
            
            return View("Index", mainCategorie);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int idCategorie)
        {

            MainCategoriesManager.Delete(idCategorie);
            
            return View("Index");
        }
    }
    
}