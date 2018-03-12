using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using UnityWebGroup.SecureFileUpload.Models;
using UnityWebGroup.SecureFileUpload.Server;

namespace UnityWebGroup.SecureFileUpload.Service
{
    public class UsersService : IUsersService
    {
        IUsersRepo _repo;
        public UsersService(IUsersRepo repo)
        {
            _repo = repo;
        }

        public bool isValid(Users model)
        {
            try
            {
                bool validate = _repo.ValidateUser(model);
                if (validate)
                {
                    FormsAuthentication.SetAuthCookie(model.email, model.remember);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }

        }
        public void Logoff()
        {
            FormsAuthentication.SignOut();
        }

        public Users GetUser(string email)
        {
            return _repo.GetUser(email);
        }

        public ServiceResponse Register(Users model)
        {                        
            try
            {

                ServiceResponse response = ValidateUser(model, true);
                if (response.result)
                {
                    response.result = _repo.Register(model);
                }
                
                return response;

            }
            catch (Exception e)
            {

                return new ServiceResponse()
                {
                    message = e.Message,
                    result = false
                };
            }

        }

        private ServiceResponse ValidateUser(Users model, bool isNewUser=false)
        {
            ServiceResponse response = new ServiceResponse();
            response.result = false;

            if (isNewUser && string.IsNullOrEmpty(model.email))
            {
                response.message = "Email Address is required";
            }
            else if (string.IsNullOrEmpty(model.name))
            {
                response.message = "Your Name is required";
            }
            else if (string.IsNullOrEmpty(model.password))
            {
                response.message = "Password is required";
            }
            else if (isNewUser && _repo.UserExists(model))
            {
                response.message = "User already exists";
            }
            else if (model.password.Length <= 6)
            {
                response.message = "Password must be more then 6 characters";
            }
            else // not errors Found.
            {
                response.message = "Success";
                response.result = true;
            }            

            return response;
        }


        public ServiceResponse UpdateUser(Users model)
        {
            try
            {

                ServiceResponse response = ValidateUser(model);
                if (response.result)
                {
                    response.result = _repo.UpdateUser(model);
                }

                return response;

            }
            catch (Exception e)
            {

                return new ServiceResponse()
                {
                    message = e.Message,
                    result = false
                };
            }
        }
    }
}