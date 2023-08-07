using AuthenticationJWT.Models;
using AuthenticationJWT.Utils;
using log4net;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AuthenticationJWT.Controllers
{
    public class HomeController : Controller
    {
        #region Declaration and Initialization
        private readonly ILog _logger;
        public HomeController()
        {
            _logger = LogManager.GetLogger(typeof(LoginController));
        }
        #endregion

        public async Task<ActionResult> Index()
        {            
            try
            {
                string token = Request.Cookies["userToken"]?.Value;
                ClaimsPrincipal claimsPrincipal = await Task.Run(()=> JwtHelper.GetClaimsPrincipalFromToken(token));
                if (claimsPrincipal != null)
                {
                    UserDetails user = new UserDetails
                    {
                        Id = Guid.Parse(claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                        Name = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value,
                        Email = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value
                    };
                    return View(user);
                }                
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}\n{ex.StackTrace}");                
            }
            return RedirectToAction("SignIn", "Login");
        }
    }
}