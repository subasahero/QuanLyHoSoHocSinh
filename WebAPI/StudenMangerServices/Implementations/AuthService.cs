using AutoMapper;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudenMangerServices.Interfaces;
using StudenMangerServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utility.Enums;

namespace StudenMangerServices.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public AuthService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            DataContext dataContext,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<UserViewModel> GetByUserNameAsync(string userName)
        {
            User user = await _userManager.Users.FirstOrDefaultAsync(x => x.NormalizedUserName == userName.ToUpper());
            UserViewModel result = _mapper.Map<UserViewModel>(user);
            return result;
        }

        public async Task<IList<string>> GetRolesAsync(UserViewModel userVM)
        {
            User user = _mapper.Map<User>(userVM);
            IList<string> roles = await _userManager.GetRolesAsync(user);
            IList<string> rolesVM = _mapper.Map<IList<string>>(roles);
            return rolesVM;
        }

        public async Task<LoginResult> LoginAsync(LoginViewModel loginVM)
        {
            User user = await _userManager.FindByNameAsync(loginVM.UserName);
            if(user == null)
            {
                return LoginResult.Unauthorized;
            }

            if(user.Status == false)
            {
                return LoginResult.IsLockedOut;
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, loginVM.Password, false);

            if (result.Succeeded)
            {
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, user.Id.ToString()));
                return LoginResult.Succeeded;
            }

            return LoginResult.Incorrect;
        }
    }
}
