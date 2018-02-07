using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using BookMyTableDataLayer.Data;

namespace BookMyTable.Controllers
{
    
    public class LoginController : Controller
    {
        bookmytableEntities1 db = new bookmytableEntities1();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
       
        public ActionResult Index(FormCollection collection)
        {
            string email = collection.Get("email");
            string Password = collection.Get("Password");
            if (email == "admin@gmail.com" && Password == "12345")
            {
                return Redirect("~/Manager/index");
            }
            else
            {
                ViewBag.Message = "Please enter valid Email ID and Password";

            }
            return RedirectToAction("index");
        }
    }
}
    
