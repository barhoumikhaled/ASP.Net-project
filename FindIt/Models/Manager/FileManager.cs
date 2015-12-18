using FindIt.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindIt.Models.Manager
{
    public class FileManager
    {
        public static File GetById(int id, ApplicationDbContext db = null)
        {
            File file = null;
            if (db != null)
            {
                file = db.Files.Where(v => v.FileId == id).FirstOrDefault();
            }
            else
            {
                using (db = new ApplicationDbContext())
                {
                    file = db.Files.Where(v => v.FileId == id).FirstOrDefault();
                }
            }
            return file;
        }


        public static void Delete(File file)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                File f = GetById(file.FileId, db);

                if (file != null)
                {
                    db.Files.Remove(f);
                }
                db.SaveChanges();
            }
        }

    }
}