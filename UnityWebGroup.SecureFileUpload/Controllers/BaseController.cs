using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnityWebGroup.SecureFileUpload.Service;

namespace UnityWebGroup.SecureFileUpload.Controllers
{

    [Authorize]
    public class BaseController : Controller
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public BaseController()
        {
            
        }

    }
}