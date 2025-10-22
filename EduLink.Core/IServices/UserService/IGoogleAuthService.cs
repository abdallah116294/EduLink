using EduLink.Core.Entities;
using EduLink.Utilities.DTO;
using EduLink.Utilities.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.IServices.UserService
{
    public interface IGoogleAuthService
    {
        Task<ResponseDTO<User>> GoogleSignIn(GoogleSignInVM model);
    }
}
