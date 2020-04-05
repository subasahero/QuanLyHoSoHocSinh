using StudenMangerServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudenMangerServices.Interfaces
{
    public interface IUserRoleService
    {
        Task<UserRoleViewModel> GetByUserIdAsync(Guid userId);
        Task<UserRoleViewModel> CreateAsync(UserRoleViewModel userRoleVM);
        Task DeleteAsync(Guid userId, Guid roleId);
    }
}
