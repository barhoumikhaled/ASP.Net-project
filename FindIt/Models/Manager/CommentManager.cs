using FindIt.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindIt.Models.Manager
{
    public class CommentManager
    {
        public static void Add(Comment comment)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Comment.Add(comment);
                db.SaveChanges();
            }
        }

        public static Comment GetById(int id, ApplicationDbContext db = null)
        {
            Comment comment = null;
            if (db != null)
            {
                comment = db.Comment.Where(v => v.Id == id).FirstOrDefault();
            }
            else
            {
                using (db = new ApplicationDbContext())
                {
                    comment = db.Comment.Where(v => v.Id == id).FirstOrDefault();
                    
                }
            }
            return comment;
        }

        public static void Delete(int commentId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Comment comment = GetById(commentId, db);

                if (commentId != 0)
                {
                    db.Comment.Remove(comment);
                    db.SaveChanges();
                }
                
            }
        }

        public static void Modify(Comment comment)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Comment comments = GetById(comment.Id, db);

                comments.comments = comment.comments;

                db.SaveChanges();
            }
        }

        public static List<Comment> GetAll()
        {
            List<Comment> comment = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                comment = db.Comment.Include("ApplicationUser").OrderBy(c => c.Id).ToList();
            }
            return comment;
        }
        public static List<Comment> GetAllByIdProduit(int idProduit)
        {
            List<Comment> comment = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                comment = db.Comment.Include("ApplicationUser").Where(c=>c.ProductId == idProduit).OrderBy(c => c.Id).ToList();
            }
            return comment;
        }
        //verifier si le user a deja commente ce produit 
        public static Comment CommentVerification(string idUser, int idProduit, int? idCommentataire){
            Comment comment = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (idCommentataire != null)
                {
                    comment = db.Comment.Include("ApplicationUser").Where(c => c.ProductId == idProduit && c.ApplicationUserId == idUser && c.Id == idCommentataire).FirstOrDefault();
                }
                else
                {
                    comment = db.Comment.Include("ApplicationUser").Where(c => c.ProductId == idProduit && c.ApplicationUserId == idUser).FirstOrDefault();
                }
            }
            return comment;
        }
    }
}