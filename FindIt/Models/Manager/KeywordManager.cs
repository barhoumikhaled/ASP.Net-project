using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindIt.Models.Entities;
using System.Web.Mvc;

namespace FindIt.Models.Manager
{
    public class KeywordManager
    {
        public static int Add(Keyword keyword)
        {
            int retour;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                // search if the keyword existe
                Keyword key = db.Keyword.Where(k => k.Name.Contains(keyword.Name)).FirstOrDefault();
                if (key == null)
                {
                    db.Keyword.Add(keyword);
                }
                retour = db.SaveChanges();
            }

            return retour;
        }

        public static Keyword GetById(int id, ApplicationDbContext db = null)
        {
            Keyword keyword = null;
            if (db != null)
            {
                keyword = db.Keyword.Where(v => v.Id == id).FirstOrDefault();
            }
            else
            {
                using (db = new ApplicationDbContext())
                {
                    keyword = db.Keyword.Where(v => v.Id == id).FirstOrDefault();
                }
            }
            return keyword;
        }

        public static void Delete(int keywordId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Keyword keyword = GetById(keywordId, db);

                if (keyword != null)
                {
                    db.Keyword.Remove(keyword);
                }
                db.SaveChanges();
            }
        }

        public static void Modify(Keyword newKeyword)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                Keyword keyword = GetById(newKeyword.Id, db);

                keyword.Name = newKeyword.Name;

                db.SaveChanges();
            }
        }

        public static List<Keyword> GetAll()
        {
            List<Keyword> listKeyword = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                listKeyword = db.Keyword.OrderBy(c => c.Id).ToList();
            }
            return listKeyword;
        }

        public static MultiSelectList GetSelectList(int? id)
        {
            int selectedValue = id.HasValue ? id.Value : -1;
            IEnumerable<Keyword> listKeyword = GetAll().OrderBy(key=>key.Name);
            
            return new MultiSelectList(listKeyword, "Id","Name");
        }

        
    }
}