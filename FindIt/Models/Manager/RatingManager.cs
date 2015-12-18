using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindIt.Models.Entities;

namespace FindIt.Models.Manager
{
    public class RatingManager
    {
        public static void Add(Rating rating)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Rating.Add(rating);
                db.SaveChanges();
            }
        }

        public static Rating GetById(int id, ApplicationDbContext db = null)
        {
            Rating rating = null;
            if (db != null)
            {
                rating = db.Rating.Include("ApplicationUser").Where(v => v.Id == id).FirstOrDefault();
            }
            else
            {
                using (db = new ApplicationDbContext())
                {
                    rating = db.Rating.Include("ApplicationUser").Where(v => v.Id == id).FirstOrDefault();
                }
            }
            return rating;
        }

        public static void Delete(int ratingId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Rating rating = GetById(ratingId, db);

                if (rating != null)
                {
                    db.Rating.Remove(rating);
                }
                db.SaveChanges();
            }
        }

        public static void Modify(Rating newRating)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                Rating rating = GetById(newRating.Id, db);

                rating.Score = newRating.Score;
                
                db.SaveChanges();
            }
        }

        public static List<Rating> GetAll(int idproduit)
        {
            List<Rating> listRating = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                listRating = db.Rating.Include("ApplicationUser").Where(c=>c.ProductId == idproduit).OrderBy(c => c.Id).ToList();
            }
            return listRating;
        }
        //recupere la note d'un user sur un produit donner
        public static Rating GetUserRating(string idUser,int idproduit)
        {
            Rating rating = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                rating = db.Rating.Include("ApplicationUser").Where(c => c.ProductId == idproduit && c.ApplicationUserId == idUser).FirstOrDefault();
            }
            return rating;
        }
        
    }
}