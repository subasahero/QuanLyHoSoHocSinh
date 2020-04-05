using StudenMangerServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utility.Dtos;

namespace StudenMangerServices.Interfaces
{
    public interface IRoleService
    {
        Task<IList<RoleViewModel>> GetAllAsync();
        Task<PagedList<RoleViewModel>> GetAllPagingAsync(PagingParams pagingParams);
        Task<RoleViewModel> GetByIdAsync(Guid id);
        Task<RoleViewModel> CreateAsync(RoleViewModel roleVM);
        Task UpdateAsync(RoleViewModel roleVM);
        Task DeleteAsync(Guid id);
        Task<bool> CheckExistsAsync(Guid id);
        Task<Guid?> GetIdByNameAsync(string roleName);
    }
}
