using StudenMangerServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utility.Enums;

namespace StudenMangerServices.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResult> LoginAsync(LoginViewModel loginVM);
        Task<UserViewModel> GetByUserNameAsync(string userName);
        Task<IList<string>> GetRolesAsync(UserViewModel userVM);
    }
}
