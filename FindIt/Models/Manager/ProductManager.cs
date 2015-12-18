using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindIt.Models.Entities;
using FindIt.Models;
using System.Web.Mvc;

namespace FindIt.Models.Entities
{
    public class ProductManager
    {

        public static void Edit(Product product)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Product productInDb = ProductManager.GetById(product.Id, db);

                productInDb.Name = product.Name;
                productInDb.Price = product.Price;
                productInDb.Description = product.Description;

                //TODO Liste des KeyWord

                List<int> newKeyWords = null;
                List<int> oldKeyWord = new List<int>();
                //TODO Get All Supplier by Product and all By KeyWord
                List<Supplier> mySuplier = db.Products.Single(p => p.Id == product.Id).Supplier.ToList();
                List<Keyword> myKeyWord = db.Products.Single(p => p.Id == product.Id).Keyword.ToList();

                //TODO GetAll KeyWord
                foreach (Keyword i in myKeyWord)
                {
                    oldKeyWord.Add(i.Id);
                }

                if (product.SelectedItemKeyWordIds != null)
                {
                    newKeyWords = product.SelectedItemKeyWordIds;
                }
                else
                {
                    newKeyWords = new List<int>();
                }
                Keyword keyToAdd = null;
                Keyword keyToRemove = null;

                foreach (int id in newKeyWords)
                {
                    //Add the new KeyWord
                    if (!oldKeyWord.Contains(id))
                    {
                        keyToAdd = db.Keyword.Where(k => k.Id == id).FirstOrDefault();
                        productInDb.Keyword.Add(keyToAdd);
                    }
                }

                //Remove the unselected KeyWord
                foreach (int id in oldKeyWord)
                {
                    if (!newKeyWords.Contains(id))
                    {
                        keyToRemove = db.Keyword.Where(k => k.Id == id).FirstOrDefault();
                        productInDb.Keyword.Remove(keyToRemove);
                    }
                }


                List<int> newSuppliers = null;
                List<int> oldSuppliers = new List<int>();
                //TODO GetAll KeyWord
                foreach (Supplier i in mySuplier)
                {
                    oldSuppliers.Add(i.Id);
                }

                if (product.SelectedItemKeyWordIds != null)
                {
                    newSuppliers = product.SelectedItemSupplierIds;
                }
                else
                {
                    newSuppliers = new List<int>();
                }
                Supplier supplierToAdd = null;
                Supplier supplerToRemove = null;

                foreach (int id in newSuppliers)
                {
                    //TODO Add the new KeyWord
                    if (!oldSuppliers.Contains(id))
                    {
                        supplierToAdd = db.Supplier.Where(k => k.Id == id).FirstOrDefault();
                        productInDb.Supplier.Add(supplierToAdd);
                    }
                }

                foreach (int id in oldSuppliers)
                {
                    if (!newSuppliers.Contains(id))
                    {
                        supplerToRemove = db.Supplier.Where(k => k.Id == id).FirstOrDefault();
                        productInDb.Supplier.Remove(supplerToRemove);
                    }
                }
                if (product.Files != null)
                {
                    productInDb.Files = product.Files;
                }

                db.SaveChanges();

            }
        }

        public static int Add(Product product)
        {
            int nb = -1;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Keyword keyword = null;
                if (product.SelectedItemKeyWordIds != null)
                {
                    foreach (int id in product.SelectedItemKeyWordIds)
                    {
                        keyword = db.Keyword.Where(k => k.Id == id).FirstOrDefault();
                        if (keyword != null)
                        {
                            if (product.Keyword == null)
                            {
                                product.Keyword = new List<Keyword>();
                            }
                            product.Keyword.Add(keyword);
                        }
                    }
                }
                

                Supplier supplier = null;
                if (product.SelectedItemSupplierIds != null)
                {
                    foreach (int id in product.SelectedItemSupplierIds)
                    {
                        supplier = db.Supplier.Where(s => s.Id == id).FirstOrDefault();
                        if (keyword != null)
                        {
                            if (product.Supplier == null)
                            {
                                product.Supplier = new List<Supplier>();
                            }
                            product.Supplier.Add(supplier);
                        }
                    }
                }
                

                db.Products.Add(product);
                nb = db.SaveChanges();
            }
            return nb;
        }

        public static Product GetById(int id, ApplicationDbContext db = null)
        {
            Product product = null;
            if (db != null)
            {
                product = db.Products.Include("Keyword").Include("Supplier").Include("Files").Where(v => v.Id == id).FirstOrDefault();
            }
            else
            {
                using (db = new ApplicationDbContext())
                {
                    product = db.Products.Include("Keyword").Include("Supplier").Include("Files").Where(v => v.Id == id).FirstOrDefault();
                }
            }
            return product;
        }

        public static void Delete(int productId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Product product = GetById(productId, db);

                if (product != null)
                {
                    db.Products.Remove(product);
                }
                db.SaveChanges();
            }
        }

        public static List<Product> GetByIdSubCategorie(int id, ApplicationDbContext db = null)
        {
            List<Product> product = null;
            if (db != null)
            {
                product = db.Products.Include("Keyword").Include("Supplier").Include("Files").Where(v => v.SubCategorieID == id).ToList();
            }
            else
            {
                using (db = new ApplicationDbContext())
                {
                    product = db.Products.Include("Keyword").Include("Supplier").Include("Files").Where(v => v.SubCategorieID == id).ToList();
                }
            }
            return product;
        }
        public static void Modify(Product newProduct)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                Product product = GetById(newProduct.Id, db);

                product.Name = newProduct.Name;

                db.SaveChanges();
            }
        }

        public static List<Product> GetAll()
        {
            List<Product> listProduct = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                listProduct = db.Products.Include("Keyword").Include("Supplier").Include("SubCategorie").Include("Files").OrderBy(c => c.Id).ToList();
            }
            return listProduct;
        }

        public static List<Product> GetByCategorieId(int id)
        {
            List<Product> lst = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                //trouver la categorie en question
                MainCategories mc = db.MainCategories.Include("SubCategorie").Where(mcat => mcat.Id == id).FirstOrDefault();
                //getAll subcategorieByCategories
                List<SubCategorie> lstSubCat = db.MainCategories.Include("SubCategorie").Single(m => m.Id == id).SubCategorie.ToList();
                lst = new List<Product>();
                foreach (SubCategorie sub in lstSubCat)
                {
                    //list product by sub categorie
                    List<Product> listProductSubCategorie = db.Products.Include("SubCategorie").Include("Files").Where(p => p.SubCategorieID == sub.Id).ToList();
                    lst.AddRange(listProductSubCategorie);
                }
            }

            return lst;
        }

        public static List<Product> GetByNameOrKeyWord(string name)
        {
            List<Product> lst = new List<Product>();
            if (name != "")
            {
                List<Product> lstByName = null;
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    //search by name
                    lstByName = db.Products.Include("Keyword").Include("Supplier").Include("SubCategorie").Include("Files").Where(p => p.Name.Contains(name)).ToList();
                    lst.AddRange(lstByName);

                    // verifier si la mot cherche es tkeyword
                    Keyword key = db.Keyword.Where(k => k.Name.Contains(name)).FirstOrDefault();
                    //search by KeyWord
                    if (key != null)
                    {
                        List<Product> myProductByKeyWord = db.Products.Include("Keyword").Include("Files").Where(p => p.Keyword.Any(k => k.Id == key.Id)).ToList();
                        lst.AddRange(myProductByKeyWord);
                    }

                    //List<Keyword> myKeyWord = db.Products.Single(p => p.Id == product.Id).Keyword.ToList();
                }

            }
            return lst;
            
        }
        public static List<Product> GetAllBySupplierId(int id)
        {
            List<Product> listProduct = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                listProduct = db.Supplier.Single(p => p.Id == id).Products.ToList(); ;
            }
            return listProduct;
		}
		
        public static bool IsQtyEnough(int productId, int qty) {
            using (ApplicationDbContext db = new ApplicationDbContext()) {
                return (db.Products.SingleOrDefault(p => p.Id == productId && p.Qty >= qty) != null);
            }
        }

        public static bool SellProduct(int productId, int qty) {
            using (ApplicationDbContext db = new ApplicationDbContext()) {
                Product product = db.Products.SingleOrDefault(p => p.Id == productId && p.Qty > qty);
                //Si jamais le client tente d'acheter un produit lorsque le produit c'est fait retirer
                if (product != null) {
                    product.Qty -= qty;
                    //Si une ligne a été modifié alors, le produit est bel et bien marquer comme vendu
                    return (db.SaveChanges() > 0);
                }

                return false;
            }
        }

        public static IEnumerable<SelectListItem> GetSelectListItem(int? id)
        {
            int selectedValue = id.HasValue ? id.Value : -1;
            IEnumerable<Product> listProduct = GetAll();
            IEnumerable<SelectListItem> selectListItemProduct = listProduct.OrderBy(s => s.Name).Select(s =>
                new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString(),
                    Selected = s.Id == selectedValue
                });
            return selectListItemProduct;
        }

        public static Product GetByName(Product product)
        {
            Product pro = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                pro = db.Products.Where(p => p.Name.Equals(product.Name)).FirstOrDefault();
            }

            return pro;
        }
    }
}