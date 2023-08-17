using AuthenticationJWT.Models;
using AuthenticationJWT.Utils;
using log4net;
using ServiceLayer.DTO;
using ServiceLayer.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace AuthenticationJWT.Controllers
{
    [OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
    public class HomeController : Controller
    {
        #region Declaration and Initialization
        private readonly ILog _logger;
        private readonly IService _service;
        private readonly IMap _mapper;
        public HomeController(IService service, IMap mapper)
        {
            _service = service;
            _mapper = mapper;
            _logger = LogManager.GetLogger(typeof(LoginController));
        }
        #endregion

        public async Task<ActionResult> Index()
        {
            try
            {
                string token = Request.Cookies["userToken"]?.Value;
                if (!string.IsNullOrEmpty(token))
                {
                    ClaimsPrincipal claimsPrincipal = await Task.Run(() => JwtHelper.GetClaimsPrincipalFromToken(token));
                    if (claimsPrincipal != null)
                    {
                        User user = new User
                        {
                            Id = Guid.Parse(claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                            Name = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value,
                            Email = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value
                        };
                        return View(user);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}\n{ex.StackTrace}");                
            }
            return RedirectToAction("SignIn", "Login");
        }
        public async Task<ActionResult> Logout()
        {
            HttpCookie cookie = Request.Cookies["userToken"];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddMinutes(-1);
                Response.Cookies.Add(cookie);
                await Task.Run(() => Request.Cookies.Remove("userToken"));
            }            
            return RedirectToAction("SignIn", "Login");
        }
        
        [HttpGet]
        public async Task<ActionResult> EditUser(Guid id)
        {
            try
            {
                if (id != Guid.Empty)
                {
                    string token = Request.Cookies["userToken"]?.Value;
                    if (!string.IsNullOrEmpty(token))
                    {
                        UserDetailsDTO user = await Task.Run(() => _service.GetUserDetail(token, id));
                        if (user != null)
                        {
                            UserDetails userDetail = _mapper.GetUserDetails(user);
                            return View(userDetail);
                        }
                    }
                }                                
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}\n{ex.StackTrace}");
            }
            return RedirectToAction("Logout", "Home");
        }
    }
}