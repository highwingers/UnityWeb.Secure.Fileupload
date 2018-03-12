using UnityWebGroup.SecureFileUpload.Models;

namespace UnityWebGroup.SecureFileUpload.Service
{
    public interface IUsersService
    {
        bool isValid(Users model);
        void Logoff();
        ServiceResponse Register(Users model);
        Users GetUser(string email);

        ServiceResponse UpdateUser(Users model);

    }
}