using FindIt.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindIt.Controllers
{
    [AllowAnonymous]
    public class FileController : Controller
    {
        //
        // GET: /File/
        public ActionResult Index(int id)
        {
            //var fileToRetrieve = db.Files.Find(id);
            var fileToRetrieve = FileManager.GetById(id);
            return File(fileToRetrieve.Content, fileToRetrieve.ContentType);
        }
    }
}