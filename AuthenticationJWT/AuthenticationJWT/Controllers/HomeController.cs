using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuthenticationJWT.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string token = Request.Cookies["userToken"]?.Value;
            return View();
        }
    }
}