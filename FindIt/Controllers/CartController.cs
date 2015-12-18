using FindIt.Models;
using FindIt.Models.Entities;
using FindIt.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace FindIt.Controllers
{
    [AllowAnonymous]
    public class CartController : Controller
    {
        public ActionResult Index()
        {
            if (Session["cart"] == null) {
                Session["cart"] = new List<CartProduct>();
            }

            return View(Session["Cart"]);
        }

        public ActionResult Add() {
            int[] productsId = Request.Form.GetValues("productsId").StringArrayToIntArray();
            int[] productsQty = Request.Form.GetValues("productsQty").StringArrayToIntArray();

            if (Session["cart"] == null) {
                Session["cart"] = new List<CartProduct>();
            }

            if(productsId != null && productsQty != null && productsId.Length == productsQty.Length) {            
                for(int i=0 ;i < productsId.Length; i++) {
                    if (productsQty[i] > 0) {
                        Product product = ProductManager.GetById(productsId[i]);
                        if (product != null && product.Qty >= productsQty[i]) {
                            CartProduct cartProduct = (Session["cart"] as ICollection<CartProduct>).SingleOrDefault(cp => cp.Product.Id == productsId[i]);
                            
                            if (cartProduct != null)
                                cartProduct.Qty += productsQty[i];
                            else
                                (Session["cart"] as ICollection<CartProduct>).Add(new CartProduct { Product = product, Qty = productsQty[i] });
                        }
                    }
                }
            }

            return PartialView("_CartView");
        }

        //On fait modifie la quantité des produits qui se trouve dans le panier (seulement)
        public void Update() {
            int[] productsId = Request.Form.GetValues("productsId").StringArrayToIntArray();
            int[] productsQty = Request.Form.GetValues("productsQty").StringArrayToIntArray();

            if (Session["cart"] == null) {
                Session["cart"] = new List<CartProduct>();
            }

            if (productsId != null && productsQty != null && productsId.Length == productsQty.Length) {
                for (int i = 0; i < productsId.Length; i++) {
                    if (productsQty[i] > 0) {
                        CartProduct cartProduct = (Session["cart"] as ICollection<CartProduct>).SingleOrDefault(cp => cp.Product.Id == productsId[i]);
                        if (cartProduct != null && ProductManager.IsQtyEnough(cartProduct.Product.Id, cartProduct.Qty))
                            cartProduct.Qty = productsQty[i];
                    }
                }
            }
        }

        //Version Non-Ajax
        public void Remove() {
            if (Session["cart"] != null) {
                int[] productsId = Request.Form.GetValues("productsId").StringArrayToIntArray();

                if (productsId != null) {
                    for (int i = 0; i < productsId.Length; i++) {
                        CartProduct cartProduct = (Session["cart"] as ICollection<CartProduct>).SingleOrDefault(cp => cp.Product.Id == productsId[i]);
                        if (cartProduct != null)
                            (Session["cart"] as ICollection<CartProduct>).Remove(cartProduct);
                    }
                }
            }
        }

        public ActionResult Checkout() {
            Address address = null;
            if (User.Identity.IsAuthenticated) {
                ApplicationUser user = IdentityManager.GetByUser(User);

                if (user.DeliveryAddressId.HasValue) {
                    address = AddressManager.GetByIdJoinCountry(user.DeliveryAddressId.Value);
                }
                else {
                    address = AddressManager.GetByIdJoinCountry(user.AdressId);
                }
            }

            if (address != null) {
                ViewBag.CountryList = CountryManager.GetSelectList(address.Province.CountryId);
                ViewBag.ProvinceList = ProvinceManager.GetSelectList(address.ProvinceId);
            }
            else {
                ViewBag.CountryList = CountryManager.GetSelectList(null);
            }

            return View(address);
        }

        [HttpPost]
        public ActionResult Checkout(Address address) {
            //On considère que le client à payé

            //Normalement une transaction aurait été utilisé
            if(Session["cart"] != null) {
                string userId = null;
                if(User.Identity.IsAuthenticated)
                    userId = IdentityManager.GetByUser(User).Id;

                Address existingAdress = AddressManager.GetDBAddressByData(address);
                if (existingAdress != null)
                    address = existingAdress;
                else
                    AddressManager.Add(address);

                CommandeClient cc = new CommandeClient { ClientId = userId, DateCommande = DateTime.Now, DeliveryAddressId = address.Id };
                CommandeClientManager.Add(cc);

                int i=0;
                ICollection<CartProduct> cartProducts = (Session["cart"] as ICollection<CartProduct>);
                while(i < cartProducts.Count) {
                    CartProduct cp = cartProducts.ElementAt(i);
                    
                    //S'il y a suffisant du produit en inventaire pour satisfaire la quantité demandé par le client
                    if (ProductManager.IsQtyEnough(cp.Product.Id, cp.Qty)) {
                        ProductManager.SellProduct(cp.Product.Id, cp.Qty);
                        EntryCommandeClientManager.Add(new EntryCommandeClient { CommandeClientId = cc.Id, ProductId = cp.Product.Id, Quantity = cp.Qty});
                        cartProducts.Remove(cp);
                    }
                    //Sinon on passe au prochain produit dans le panier
                    else {
                        i++;
                    }
                }
            }
            //TODO
            return RedirectToAction("", "");
        }
    }

    public static class ArrayExtension {
        public static int[] StringArrayToIntArray(this String[] strArray) {
            if (strArray == null)
                return null;
            List<int> numbers = new List<int>();
            int t_number;
            foreach (String str in strArray) {
                if (int.TryParse(str, out t_number))
                    numbers.Add(t_number);
            }

            return numbers.ToArray();
        }
    }
}