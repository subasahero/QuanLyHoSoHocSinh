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

namespace StudenMangerServices.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public RoleService(
            RoleManager<Role> roleManager,
            DataContext dataContext,
            IMapper mapper)
        {
            _roleManager = roleManager;
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<bool> CheckExistsAsync(Guid id)
        {
            RoleViewModel result = await GetByIdAsync(id);
            return result == null ? false : true;
        }

        public async Task<RoleViewModel> CreateAsync(RoleViewModel roleVM)
        {
            Role role = _mapper.Map<Role>(roleVM);
            await _roleManager.CreateAsync(role);
            RoleViewModel result = _mapper.Map<RoleViewModel>(role);
            return result;
        }

        public async Task DeleteAsync(Guid id)
        {
            Role role = await _roleManager.FindByIdAsync(id.ToString());
            await _roleManager.DeleteAsync(role);
        }

        public async Task<IList<RoleViewModel>> GetAllAsync()
        {
            IList<RoleViewModel> rolesVM = await _roleManager.Roles
                .Where(x => x.Status == true)
                .OrderBy(x => x.Name)
                .ProjectTo<RoleViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return rolesVM;
        }

        public Task<PagedList<RoleViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            //IQueryable<RoleViewModel> query = _roleManager.Roles.OrderBy(x => x.Name).AsQueryable();

            //if (!string.IsNullOrEmpty(pagingParams.Keyword))
            //{
            //    string keyword = pagingParams.Keyword.ToUpper().ToTrim();

            //    query = query.Where(x =>
            //                        x.Name.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
            //                        x.Name.ToUpper().Contains(keyword));
            //}

            //if (!string.IsNullOrEmpty(pagingParams.SearchValue))
            //{
            //    if (pagingParams.SearchKey == "name")
            //        query = query.Where(x => x.Name == pagingParams.SearchValue.Trim());
            //}

            //if (!string.IsNullOrEmpty(pagingParams.SortValue) && !pagingParams.SortValue.Equals("null") && !pagingParams.SortValue.Equals("undefined"))
            //{
            //    switch (pagingParams.SortKey)
            //    {
            //        case "name":
            //            if (pagingParams.SortValue == "ascend")
            //            {
            //                query = query.OrderBy(x => x.Name);
            //            }
            //            else
            //            {
            //                query = query.OrderByDescending(x => x.Name);
            //            }
            //            break;
            //        default:
            //            break;
            //    }
            //}

            //return await PagedList<RoleViewModel>
            //    .CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);

            throw new NotImplementedException();
        }

        public async Task<RoleViewModel> GetByIdAsync(Guid id)
        {
            Role role = await _dataContext.Roles
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(x => x.Id == id);

            RoleViewModel result = _mapper.Map<RoleViewModel>(role);
            return result;
        }

        public async Task<Guid?> GetIdByNameAsync(string roleName)
        {
            Role role = await _dataContext.Roles
                .FirstOrDefaultAsync(x => x.NormalizedName == roleName.ToUpper());
            return role?.Id;
        }

        public async Task UpdateAsync(RoleViewModel roleVM)
        {
            Role role = await _roleManager.FindByIdAsync(roleVM.Id.ToString());
            role.Name = roleVM.Name;
            await _roleManager.UpdateAsync(role);
        }
    }
}
