using FindIt.Models;
using FindIt.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindIt.Models.Manager
{
    public class SubCategorieManager
    {
        public static void Add(SubCategorie subCategorie)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.SubCategorie.Add(subCategorie);
                db.SaveChanges();
            }
        }

        public static SubCategorie GetById(int? id, ApplicationDbContext db = null)
        {
            SubCategorie subCategorie = null;
            if (db != null)
            {
                subCategorie = db.SubCategorie.Where(v => v.Id == id).FirstOrDefault();
            }
            else
            {
                using (db = new ApplicationDbContext())
                {
                    subCategorie = db.SubCategorie.Where(v => v.Id == id).FirstOrDefault();
                }
            }
            return subCategorie;
        }
        public static SubCategorie GetByName(string name, ApplicationDbContext db = null)
        {
            SubCategorie mainCategories = null;
            if (db != null)
            {
                mainCategories = db.SubCategorie.Where(v => v.Name == name).FirstOrDefault();
            }
            else
            {
                using (db = new ApplicationDbContext())
                {
                    mainCategories = db.SubCategorie.Where(v => v.Name == name).FirstOrDefault();
                }
            }
            return mainCategories;
        }
        public static void Delete(int subCategorieId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                SubCategorie subCategorie = GetById(subCategorieId, db);

                if (subCategorie != null)
                {
                    List<Product> suppProduit = db.Products.Where(p => p.SubCategorieID == subCategorieId).ToList();
                    db.Products.RemoveRange(suppProduit);

                    db.SubCategorie.Remove(subCategorie);
                    db.SaveChanges();
                }
            }
        }

        public static void Modify(SubCategorie newSubCategorie)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                SubCategorie subCategorie = GetById(newSubCategorie.Id, db);

                subCategorie.Name = newSubCategorie.Name;
       //         subCategorie.Products = newSubCategorie.Products;
                subCategorie.MainCategoriesId = newSubCategorie.MainCategoriesId;
       //         subCategorie.MainCategories = newSubCategorie.MainCategories;

                db.SaveChanges();
            }
        }

        public static List<SubCategorie> GetAll()
        {
            List<SubCategorie> listSubCategorie = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                listSubCategorie = db.SubCategorie.OrderBy(c => c.Id).ToList();
            }
            return listSubCategorie;
        }

        public static SubCategorie GetByIdMainCategorie(int id, string name, ApplicationDbContext db = null)
        {
            SubCategorie subCategorie = null;
            if (db != null)
            {
                subCategorie = db.SubCategorie.Where(v => v.MainCategoriesId == id && v.Name == name).FirstOrDefault();
            }
            else
            {
                using (db = new ApplicationDbContext())
                {
                    subCategorie = db.SubCategorie.Where(v => v.MainCategoriesId == id && v.Name == name).FirstOrDefault();
                }
            }
            return subCategorie;
        }
        public static List<SubCategorie> GetByIdCategorie(int id)
        {
            List<SubCategorie> subCategorie = null;
            
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    subCategorie = db.SubCategorie.Where(v => v.MainCategoriesId == id).ToList();
                }
            
            return subCategorie;
        }

        public static IEnumerable<SelectListItem> GetSelectList(int? id)
        {
            int selectedValue = id.HasValue ? id.Value : -1;
            IEnumerable<SubCategorie> listSubCategorie = GetAll();
            IEnumerable<SelectListItem> selectListItemSubCategorie = listSubCategorie.OrderBy(s => s.Name).Select(s =>
                new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString(),
                    Selected = s.Id == selectedValue
                });
            return selectListItemSubCategorie;
        }
    }
}