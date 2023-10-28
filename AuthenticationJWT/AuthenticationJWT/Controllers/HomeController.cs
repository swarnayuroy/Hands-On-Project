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
                        User user = new User()
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(UserView viewDetails)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("ViewProfile", viewDetails);
                }
                string token = Request.Cookies["userToken"]?.Value;
                if (!string.IsNullOrEmpty(token))
                {                  
                    UserDetailsDTO user = _mapper.GetUserDetailsDTO(viewDetails.User);
                    bool isUserDetailSaved = await Task.Run(() => _service.EditUserDetails(token, user));
                    if (isUserDetailSaved)
                    {
                        return RedirectToAction("ViewProfile", new { id = viewDetails.User.Id, isEditEnabled = false});
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}\n{ex.StackTrace}");
                ViewBag.Error = $"We encountered some problem while saving your details. Please try again later.";
            }
            return RedirectToAction("Logout", "Home");
        }
        
        [HttpGet]
        public async Task<ActionResult> ViewProfile(Guid id, bool isEditEnabled = false)
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
                            UserView view = new UserView
                            {
                                User = _mapper.GetUserDetails(user),
                                IsEditEnabled = isEditEnabled
                            };
                            return View(view);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}\n{ex.StackTrace}");
                ViewBag.Error = $"We encountered some problem while fetching your details. Please try again later.";
            }
            return RedirectToAction("Logout", "Home");
        }

        [HttpGet]
        public async Task<ActionResult> ChangePassword(Guid id)
        {
            try
            {
                if (id != Guid.Empty)
                {
                    string token = Request.Cookies["userToken"]?.Value;
                    if (!string.IsNullOrEmpty(token))
                    {
                        UserDetailsDTO userDetails = await Task.Run(() => _service.GetUserDetail(token, id, true));
                        User user = _mapper.GetOnlyUserCred(userDetails);
                        AlterPassword alterPassword = new AlterPassword()
                        {
                            Id = user.Id,
                            Name = user.Name,
                            Email = user.Email,
                            Password = user.Password
                        };
                        return View(alterPassword);
                    }
                }      
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}\n{ex.StackTrace}");
                ViewBag.Message = $"We encountered some problem while fetching your details. Please try again later.";
            }
            return RedirectToAction("Logout", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(AlterPassword userDetails)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("ChangePassword", userDetails);
                }
                string token = Request.Cookies["userToken"]?.Value;
                if (!string.IsNullOrEmpty(token))
                {
                    UserDetails user = new UserDetails()
                    {
                        Id = userDetails.Id,
                        Name = userDetails.Name,
                        Password = userDetails.New
                    };
                    bool isUserPasswordSaved = await Task.Run(() => _service.EditUserDetails(token, _mapper.GetUserDetailsDTO(user), true));
                    if (isUserPasswordSaved)
                    {
                        ViewBag.Message = "Your password has been changed";
                    }
                    else
                    {
                        ViewBag.Message = "Failed to change your password!\nPlease try again later.";
                    }
                    return View(userDetails);
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}\n{ex.StackTrace}");
                ViewBag.Message = $"We encountered some problem while saving your new password. Please try again later.";
            }
            return RedirectToAction("Logout", "Home");
        }

        public async Task<ActionResult> DeleteAccount(Guid id)
        {
            try
            {
                string token = Request.Cookies["userToken"]?.Value;
                if (!string.IsNullOrEmpty(token))
                {
                    bool isUserDeleted = await Task.Run(() => _service.DeleteAccount(token, id));
                    if (isUserDeleted)
                    {
                        return RedirectToAction("Logout", "Home");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}\n{ex.StackTrace}");
                ViewBag.Error = $"We encountered some problem while deleting your account! Please try again later.";
            }            
            return RedirectToAction("Index", "Home");
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

    }
}