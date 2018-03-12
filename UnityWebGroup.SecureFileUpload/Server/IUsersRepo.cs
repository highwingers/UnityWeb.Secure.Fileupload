using UnityWebGroup.SecureFileUpload.Models;

namespace UnityWebGroup.SecureFileUpload.Server
{
    public interface IUsersRepo
    {
        bool ValidateUser(Users model);
        bool Register(Users model);
        Users GetUser(string email);
        bool UserExists(Users model);
        bool UpdateUser(Users model);

    }
}