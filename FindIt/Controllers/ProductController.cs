using FindIt.Models;
using FindIt.Models.Entities;
using FindIt.Models.Manager;
using FindIt.Models.Manager;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace FindIt.Controllers
{
    [AllowAnonymous]
    public class ProductController : Controller
    {

        public ActionResult Delete3(int commentid, int produitid)
        {
            CommentManager.Delete(commentid);
            return RedirectToAction("Details", "product", new { id = produitid });
        }
        public ActionResult Products( int? page)
        {
            List<Product> allProduct = ProductManager.GetAll();
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(allProduct.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Index()
        {
            return View();
        }
        // GET: Produit
        //TODO A revoir remplacer la liste de categori par vue partial
        public ActionResult GetListCategoriePartialView()
        {
            List<MainCategories> listCategories = MainCategoriesManager.GetAll();
            return PartialView(listCategories);
        }


        public ActionResult GetProductByCategorieParitalView(int id, int? page)
        {
            List<Product> listProduct = ProductManager.GetByCategorieId(id);

            TempData["subCategories"] = SubCategorieManager.GetByIdCategorie(id);
            
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(listProduct.ToPagedList(pageNumber, pageSize));

        }
        public ActionResult GetProductBySubCategorieParitalView(int id)
        {
            List<Product> listProduct = ProductManager.GetByIdSubCategorie(id);
            if (listProduct != null)
            {
          //      SubCategorie sub = SubCategorieManager.GetById(listProduct.FirstOrDefault().SubCategorieID);
                SubCategorie sub = SubCategorieManager.GetById(id); 
                if(sub != null)
                    TempData["subCategories"] = SubCategorieManager.GetByIdCategorie(sub.MainCategoriesId);
            }
            return View("GetProductByCategorieParitalView", listProduct.ToPagedList( 1, 8));
        }
        // getAll Product by Categorie
        public ActionResult GetProductByCategorie(int id)
        {
            List<Product> listProduct = ProductManager.GetByCategorieId(id);
            TempData["subCategories"]
                = SubCategorieManager.GetByIdCategorie(id);
            return View("GetAllProduct", listProduct);
        }

        //getAll product by name or KeyWord

        public ActionResult GetProductByNameOrKeyWord(string name)
        {
            List<Product> listProduct = ProductManager.GetByNameOrKeyWord(name);
            return View("GetProductByNameOrKeyWord", listProduct);
        }


        public ActionResult GetAllProduct()
        {
            List<Product> allProduct = ProductManager.GetAll();
            return View(allProduct);
        }

        [Authorize(Roles="Admin,Employee,Buyer")]
        public ActionResult AddKeyWord(string key)
        {
            Keyword keyword = new Keyword { Name = key };
            int nb = KeywordManager.Add(keyword);
            if (nb > 0)
            {

                TempData["msg"] = "L'ajout est bien ajoute";
            }
            else
            {
                TempData["msg"] = "mot cle deja existe";
            }
            
            ViewBag.KeyWordList = KeywordManager.GetSelectList(null);
            return View();
        }

        [Authorize(Roles = "Admin,Employee,Buyer")]
        public ActionResult Edit(int id)
        {
            ViewBag.KeyWordList = KeywordManager.GetSelectList(null);
            ViewBag.SupplierList = SupplierManager.GetSelectList(null);
            ViewBag.SubCategorieList = SubCategorieManager.GetSelectList(null);
            Product product = ProductManager.GetById(id);
            if (product != null)
            {
                return View(product);
            }
            else
            {
                return RedirectToAction("GetAllProduct");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, HttpPostedFileBase upload)
        {
            Product pro = ProductManager.GetById(product.Id); 
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    File ff = pro.Files.Where(p => p.ProductId == pro.Id).FirstOrDefault();
                    if (pro.Files.Any(f => f.FileType == FileType.Avatar))
                    {

                        FileManager.Delete(ff);
                    }
                    var avatar = new File
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Avatar,
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        avatar.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    product.Files = new List<File> { avatar };
                }
                ProductManager.Edit(product);
                ModelState.Clear();
                return RedirectToAction("Products");
            }
            else
            {
                ViewBag.KeyWordList = KeywordManager.GetSelectList(null);
                ViewBag.SupplierList = SupplierManager.GetSelectList(null);
                ViewBag.SubCategorieList = SubCategorieManager.GetSelectList(null);
                return View(product);
            }
        }

        [Authorize(Roles = "Admin,Employee,Buyer")]
        public ActionResult Delete(int id)
        {
            ProductManager.Delete(id);
            return RedirectToAction("GetAllProduct");
        }


        public ActionResult Details(int id)
        {
            Product product = ProductManager.GetById(id);
            if (product != null)
            {
                return View(product);
            }
            else
            {
                return View("GetAllProduct");
            }
        }
        [Authorize(Roles = "Admin,Employee,Buyer")]
        public ActionResult AddProduct()
        {
            ViewBag.KeyWordList = KeywordManager.GetSelectList(null);
            ViewBag.SupplierList = SupplierManager.GetSelectList(null);
            ViewBag.SubCategorieList = SubCategorieManager.GetSelectList(null);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(Product product, HttpPostedFileBase upload)
        {
            ViewBag.KeyWordList = KeywordManager.GetSelectList(null);
            ViewBag.SupplierList = SupplierManager.GetSelectList(null);
            ViewBag.SubCategorieList = SubCategorieManager.GetSelectList(null);
            Product p = ProductManager.GetByName(product);
            if (p==null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        if (upload != null && upload.ContentLength > 0)
                        {
                            var avatar = new File
                            {
                                FileName = System.IO.Path.GetFileName(upload.FileName),
                                FileType = FileType.Avatar,
                                ContentType = upload.ContentType
                            };
                            using (var reader = new System.IO.BinaryReader(upload.InputStream))
                            {
                                avatar.Content = reader.ReadBytes(upload.ContentLength);
                            }
                            product.Files = new List<File> { avatar };
                        }
                        int nb = 0;
                        nb = ProductManager.Add(product);
                        return RedirectToAction("Products");
                    }

                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }

                return View(product);
            }
            else
            {
                return View(product);
            }
            
        }
    }
}