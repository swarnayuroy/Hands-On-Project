using AuthenticationJWT.Models;
using AuthenticationJWT.Models.Mapping;
using AutoMapper;
using log4net;
using ServiceLayer.DTO;
using ServiceLayer.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuthenticationJWT.Controllers
{
    public class LoginController : Controller
    {
        #region Declaration and Initialization
        private ILog _logger;
        private IService _service;
        private MapEntity _map;
        public LoginController(IService service)
        {
            _service = service;
            _logger = LogManager.GetLogger(typeof(LoginController));
        }
        #endregion

        #region Login
        [HttpGet]
        public ActionResult SignIn()
        {
            try
            {                
                Form usrForm = new Form();
                return View(usrForm);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message.ToString());
                return View("Some error occurred!");
            }            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(Form credential)
        {            
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("SignIn", credential);
                }
                
                string usrName = $"";
                _map = new MapEntity();
                User usrCred = _map.GetUserCredential(credential.Login);
                UserDTO user = _map.GetUserDTO(usrCred);

                bool isValid = _service.ConfirmValidCredential(user, out usrName);
                if (isValid)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message.ToString());
                ModelState.Clear();
                ViewBag.Error = $"We encountered some problem. Please try again later.";
                return View("SignIn");
            }
            ViewBag.InvalidCred = $"Invalid credential!";
            return View("SignIn", credential);
        }
        #endregion

        #region Registration
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Form newUser)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("SignIn", newUser);
                }                
                _map = new MapEntity();
                UserDTO user = _map.GetUserDTO(newUser.Register);

                bool isRegistered = _service.RegisterNewUser(user);
                if (isRegistered)
                {
                    ModelState.Clear();
                    return View("SignIn");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message.ToString());
                ModelState.Clear();
                ViewBag.Error = $"We encountered some problem. Please try again later.";
                return View("SignIn");
            }
            ModelState.Clear();
            ViewBag.Error = $"Registration failed! Please try again later.";            
            return View("SignIn");
        }
        #endregion
    }
}