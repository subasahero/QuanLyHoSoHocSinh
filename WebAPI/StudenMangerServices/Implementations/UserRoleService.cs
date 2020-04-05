using AutoMapper;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using StudenMangerServices.Interfaces;
using StudenMangerServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudenMangerServices.Implementations
{
    public class UserRoleService : IUserRoleService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public UserRoleService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<UserRoleViewModel> CreateAsync(UserRoleViewModel userRoleVM)
        {
            UserRole userRole = _mapper.Map<UserRole>(userRoleVM);
            _dataContext.UserRoles.Add(userRole);
            await _dataContext.SaveChangesAsync();
            UserRoleViewModel result = _mapper.Map<UserRoleViewModel>(userRole);
            return result;
        }

        public async Task DeleteAsync(Guid userId, Guid roleId)
        {
            UserRole userRole = await _dataContext.UserRoles
                                                .FirstOrDefaultAsync(x => x.UserId == userId && x.RoleId == roleId);

            _dataContext.UserRoles.Remove(userRole);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<UserRoleViewModel> GetByUserIdAsync(Guid userId)
        {
            UserRole userRole = await _dataContext.UserRoles
                                                .FirstOrDefaultAsync(x => x.UserId == userId);

            UserRoleViewModel result = _mapper.Map<UserRoleViewModel>(userRole);
            return result;
        }
    }
}
