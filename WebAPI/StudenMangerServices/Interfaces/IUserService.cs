using StudenMangerServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utility.Dtos;

namespace StudenMangerServices.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> CreateAsync(UserCreationViewModel userVM);
        Task UpdateAsync(UserUpdationViewModel userVM);
        Task UpdateForUserAsync(UserUpdationViewModel userVM);
        Task DeleteAsync(Guid id);
        Task<UserViewModel> GetByIdAsync(Guid id);
        Task<bool> CheckExistsAsync(Guid id);
        Task<IList<UserViewModel>> GetAllAsync();
        Task<PagedList<UserViewModel>> GetAllPagingAsync(PagingParams pagingParams);

        Task<bool> ChangePasswordAsync(Guid id, string password);
        Task<bool> ChangePasswordForUser(UserChangePasswordViewModel model);
    }
}
