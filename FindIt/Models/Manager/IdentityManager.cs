using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using FindIt.Models.Entities;
using FindIt.Models.Manager;

namespace FindIt.Models.Manager
{
    public class IdentityManager 
    {
       //Ajouter user
        public static int AddUser(ApplicationUser user)
        {
            int ret = 0;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Users.Add(user);
                ret = db.SaveChanges();
            }
            return ret;
        }


    //Lister tout les users de la base de donnee
        public static List<ApplicationUser> GetAll()
        {
            List<ApplicationUser> listUser = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                listUser = db.Users.OrderBy(s => s.Id).ToList();
            }
            return listUser;
        }


        //Supprimer les users
        public static void Delete(string id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ApplicationUser user = GetById(id, db, new String[] { "ClientsCommandes", "SupplierCommandes" });
                if (user != null)
                {
                    db.Users.Remove(user);
                }
                db.SaveChanges();
            }
        }


        //choisir les users par id
        public static ApplicationUser GetById(string id, String[] includes = null)
        {
            using (ApplicationDbContext db = new ApplicationDbContext()) {
                return GetById(id, db, includes);
            }
        }

        public static ApplicationUser GetById(String id, ApplicationDbContext db, String[] includes = null) {
            var user = db.Users.AsQueryable();
            if(includes != null && includes.Length > 0)
                foreach (String include in includes) {
                    user = user.Include(include);
                }
            return user.Where(u => u.Id == id).SingleOrDefault();
        }

        public static ApplicationUser GetByUser(IPrincipal user) {
            using (ApplicationDbContext db = new ApplicationDbContext()) {
                return GetById(Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(user.Identity), db);
            }
        }

        public static void Edit(ApplicationUser UserModified)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                ApplicationUser UserInDB = GetById(UserModified.Id, db);
                //② Modify
                UserInDB.Lname = UserModified.Lname;
                UserInDB.Fname = UserModified.Fname;
                UserInDB.Email = UserModified.Email;
                UserInDB.Adress.PostalCode = UserModified.Adress.PostalCode;
                UserInDB.Adress.Street = UserModified.Adress.Street;
                UserInDB.Adress.No = UserModified.Adress.No;
                UserInDB.Adress.City = UserModified.Adress.City;
                UserInDB.Adress.Province = UserModified.Adress.Province;
                UserInDB.Adress.CountryId = UserModified.Adress.CountryId;


                //③ SaveChanges                    
                db.SaveChanges();
            }
        }

    }


    
}