using System.Web.Mvc;
using UnityWebGroup.SecureFileUpload.Models;
using UnityWebGroup.SecureFileUpload.Server;
using UnityWebGroup.SecureFileUpload.Service;

namespace UnityWebGroup.SecureFileUpload.Controllers
{
    public class UserController : BaseController
    {
 
        private IUsersService _userService;
        public UserController(IUsersService userService)
        {
            _userService = userService;
            log.Info("IUsersService Injected From UserController");
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users model)
        {
            bool isValid = _userService.isValid(model);
            if (isValid)
            {
                return RedirectToAction("Index","Home");
            }
            ViewBag.message = "Email/Password are incorrect";
            return View();
        }
        public ActionResult Logoff()
        {
            _userService.Logoff();
            return  RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(Users model)
        {
            ServiceResponse r = _userService.Register(model);
            if (r.result)
            {
                return RedirectToAction("Login","User");
            }
            ViewBag.message = r.message;
            return View();
        }
        public ActionResult Edit()
        {
            return View(_userService.GetUser(HttpContext.User.Identity.Name));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Users model)
        {
            ServiceResponse r = _userService.UpdateUser(model);
            if (r.result)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.message = r.message;
            return View();
        }
    }
}