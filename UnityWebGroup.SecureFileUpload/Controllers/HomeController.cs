using System.Web.Mvc;
using UnityWebGroup.SecureFileUpload.Service;

namespace UnityWebGroup.SecureFileUpload.Controllers
{
    public class HomeController : BaseController
    {
        private IUsersService _userService;
        public HomeController(IUsersService userService)
        {
            _userService = userService;
            log.Info("IUsersService Injected From HomeController");
        }
       
        public ActionResult Index()
        {
            return View(_userService.GetUser(HttpContext.User.Identity.Name));
        }
       

    }
}