using FindIt.Models;
using FindIt.Models.Entities;
using FindIt.Models.Manager;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FindIt.Models.Manager
{
    public class MainCategoriesManager
    {
        public static void Add(MainCategories mainCategories)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.MainCategories.Add(mainCategories);
                db.SaveChanges();
            }
        }

        public static MainCategories GetById(int id, ApplicationDbContext db = null)
        {
            MainCategories mainCategories = null;
            if (db != null)
            {
                mainCategories = db.MainCategories.Include("SubCategorie").Where(v => v.Id == id).FirstOrDefault();
            }
            else
            {
                using (db = new ApplicationDbContext())
                {
                    mainCategories = db.MainCategories.Include("SubCategorie").Where(v => v.Id == id).FirstOrDefault();
                }
            }
            return mainCategories;
        }

        public static void Delete(int mainCategoriesId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                MainCategories mainCategories = GetById(mainCategoriesId, db);

                
                if (mainCategories != null)
                {
                    /*
                    List<SubCategorie> suppCategorie = db.SubCategorie.Where(p => p.MainCategoriesId == mainCategoriesId).ToList();
                    if(suppCategorie != null){
                        foreach (SubCategorie s in suppCategorie)
                        {
                            SubCategorieManager.Delete(s.Id);
                        }
                    }

                    */
                    List<SubCategorie> subCate = db.SubCategorie.Where(s => s.MainCategoriesId == mainCategories.Id).ToList();
                    List<Product> suppProduit = null;
                    if(subCate != null){
                        foreach (SubCategorie s in subCate)
                        {
                            suppProduit = db.Products.Where(p => p.SubCategorieID == s.Id).ToList();
                        }
                        if(suppProduit != null)
                            db.Products.RemoveRange(suppProduit);

                        db.SubCategorie.RemoveRange(subCate);
                        db.MainCategories.Remove(mainCategories);
                    }

                }
                db.SaveChanges();
            }
        }

        public static void Modify(MainCategories newMainCategories)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                MainCategories mainCategories = GetById(newMainCategories.Id, db);

                mainCategories.Name = newMainCategories.Name;

                db.SaveChanges();
            }
        }

        public static List<MainCategories> GetAll()
        {
            List<MainCategories> listMainCategories = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                listMainCategories = db.MainCategories.Include("SubCategorie").OrderBy(c => c.Id).ToList();
            }
            return listMainCategories;
        }

        public static object GetByName(string name, ApplicationDbContext db = null)
        {
            MainCategories mainCategories = null;
            if (db != null)
            {
                
                mainCategories = db.MainCategories.Include("SubCategorie").Where(v => v.Name == name).FirstOrDefault();
            }
            else
            {
                using (db = new ApplicationDbContext())
                {
                    mainCategories = db.MainCategories.Include("SubCategorie").Where(v => v.Name == name).FirstOrDefault();
                }
            }
            return mainCategories;
        }
    }
}