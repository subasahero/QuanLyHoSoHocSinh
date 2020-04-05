using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudenMangerServices.Interfaces;
using StudenMangerServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Dtos;
using Utility.Heplers;

namespace StudenMangerServices.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IRoleService _roleService;
        private readonly IUserRoleService _userRoleService;
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IRoleService roleService,
            IUserRoleService userRoleService,
            DataContext dataContext,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userRoleService = userRoleService;
            _roleService = roleService;
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<bool> CheckExistsAsync(Guid id)
        {
            User user = await _dataContext.Users
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(x => x.Id == id);

            return user == null ? false : true;
        }

        public async Task<bool> ChangePasswordAsync(Guid id, string password)
        {
            User user = await _userManager.FindByIdAsync(id.ToString());
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, password);
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<UserViewModel> CreateAsync(UserCreationViewModel userVM)
        {
            User user = _mapper.Map<User>(userVM);
            await _userManager.CreateAsync(user, userVM.Password);

            UserRoleViewModel userRoleViewModel = new UserRoleViewModel
            {
                UserId = user.Id,
                RoleId = userVM.RoleId
            };
            await _userRoleService.CreateAsync(userRoleViewModel);

            UserViewModel result = _mapper.Map<UserViewModel>(user);
            return result;
        }

        public async Task DeleteAsync(Guid id)
        {
            User user = await _dataContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            await _userManager.DeleteAsync(user);
        }

        public async Task<IList<UserViewModel>> GetAllAsync()
        {
            IList<UserViewModel> usersVM = await _userManager.Users
                .Where(x => x.Status == true)
                .OrderBy(x => x.UserName)
                .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return usersVM;
        }

        public async Task<PagedList<UserViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            IQueryable<UserViewModel> query = from user in _dataContext.Users
                                              join userRole in _dataContext.UserRoles on user.Id equals userRole.UserId
                                              join role in _dataContext.Roles on userRole.RoleId equals role.Id
                                              orderby user.UserName
                                              select new UserViewModel
                                              {
                                                  Id = user.Id,
                                                  UserName = user.UserName,
                                                  FullName = user.FullName,
                                                  Status = user.Status,
                                                  RoleId = role.Id,
                                                  RoleName= role.Description,
                                              };

            if (!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                string keyword = pagingParams.Keyword.ToUpper().ToTrim();

                query = query.Where(x =>
                                    x.UserName.ToUpper().Contains(keyword) ||
                                    x.FullName.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                    x.FullName.ToUpper().Contains(keyword) ||
                                    x.RoleName.ToUpper().Contains(keyword) ||
                                    x.RoleName.ToUpper().ToUnSign().Contains(keyword.ToUnSign()));
            }

            return await PagedList<UserViewModel>
                .CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public async Task<UserViewModel> GetByIdAsync(Guid id)
        {
            IQueryable<UserViewModel> query = from u in _dataContext.Users
                                              join ur in _dataContext.UserRoles on u.Id equals ur.UserId
                                              join r in _dataContext.Roles on ur.RoleId equals r.Id
                                              where u.Id == id
                                              select new UserViewModel
                                              {
                                                  Id = u.Id,
                                                  UserName = u.UserName,
                                                  FullName = u.FullName,
                                                  Gender = u.Gender,
                                                  Address = u.Address,
                                                  Avatar = u.Avatar,
                                                  DateOfBirth = u.DateOfBirth,
                                                  CreatedDate = u.CreatedDate,
                                                  ModifiedDate = u.ModifiedDate,
                                                  Status = u.Status,
                                                  RoleId = r.Id,
                                                  RoleName = r.Name
                                              };

            UserViewModel result = await query.FirstOrDefaultAsync();
            return result;
        }

        public async Task UpdateAsync(UserUpdationViewModel userVM)
        {
            User user = await _userManager.FindByIdAsync(userVM.Id.ToString());
            user.UserName = userVM.UserName;
            user.Email = userVM.Email;
            user.FullName = userVM.FullName;
            user.Gender = userVM.Gender;
            user.DateOfBirth = userVM.DateOfBirth;
            await _userManager.UpdateAsync(user);

            UserRoleViewModel userRoleVM = await _userRoleService.GetByUserIdAsync(user.Id);

            await _userRoleService.DeleteAsync(user.Id, userRoleVM.RoleId);
            await _userRoleService.CreateAsync(new UserRoleViewModel
            {
                UserId = user.Id,
                RoleId = userVM.RoleId
            });
        }

        public async Task UpdateForUserAsync(UserUpdationViewModel userVM)
        {
            User user = await _userManager.FindByIdAsync(userVM.Id.ToString());
            user.UserName = userVM.UserName;
            user.Email = userVM.Email;
            user.FullName = userVM.FullName;
            user.Gender = userVM.Gender;
            user.DateOfBirth = userVM.DateOfBirth;
            await _userManager.UpdateAsync(user);
        }

        public async Task<bool> ChangePasswordForUser(UserChangePasswordViewModel model)
        {
            User user = await _userManager.FindByIdAsync(model.userId.ToString());
            IdentityResult result = await _userManager.ChangePasswordAsync(user, model.currentPassword, model.newPassword);
            if(result.Succeeded)
            {
                return true;
            } 
            else
            {
                return false;
            }
        }
    }
}
