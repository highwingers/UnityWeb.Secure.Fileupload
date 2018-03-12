using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnityWebGroup.SecureFileUpload.Models;

namespace UnityWebGroup.SecureFileUpload.Server
{
    public class UsersRepo : BaseRepo, IUsersRepo
    {

        public bool ValidateUser(Users model)
        {

            using (var u = new UnitOfWork())
            {
                return u.Query<bool>("Select 1 From Users where email=@email and password=@password", new { model.email, model.password }).FirstOrDefault();
            }

        }

        public bool UserExists(Users model)
        {

            using (var u = new UnitOfWork())
            {
                return u.Query<bool>("Select 1 From Users where email=@email", new { model.email }).FirstOrDefault();
            }

        }

        public Users GetUser(string email)
        {

            using (var u = new UnitOfWork())
            {
                return u.Query<Users>("Select * From Users where email=@email", new { email }).FirstOrDefault();
            }

        }

        public bool Register(Users model)
        {
            using (var u = new UnitOfWork(false))
            {
                u.Execute("Insert into Users(email,password,name) values(@email,@password,@name)", new { model.email, model.password, model.name });
                return true;
            }          

        }

        public bool UpdateUser(Users model)
        {
            using (var u = new UnitOfWork(false))
            {
                u.Execute("Update Users set password=@password,name=@name where id=@id", new { model.email, model.password, model.name,model.id });
                return true;
            }

        }


    }
}