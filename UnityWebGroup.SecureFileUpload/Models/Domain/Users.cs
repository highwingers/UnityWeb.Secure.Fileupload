using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnityWebGroup.SecureFileUpload.Models
{

    // Maps to DB
    public class Users
    {
        public int id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public bool remember { get; set; }
    }
}