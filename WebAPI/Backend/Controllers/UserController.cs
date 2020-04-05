using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Extension;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using StudenMangerServices.Interfaces;
using StudenMangerServices.ViewModel;
using Utility.Dtos;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly DataContext _dataContext;
        public UserController(DataContext dataContext, IUserService userService)
        {
            _userService = userService;
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IList<UserViewModel> data = await _userService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            PagedList<UserViewModel> paged = await _userService.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged.Items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid? id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return BadRequest();
            }

            UserViewModel data = await _userService.GetByIdAsync(id.Value);
            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreationViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (IDbContextTransaction transaction = _dataContext.Database.BeginTransaction())
            {
                try
                {
                    UserViewModel result = await _userService.CreateAsync(userViewModel);

                    transaction.Commit();
                    return Ok(true);
                }
                catch (Exception e)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
                }
            }
        }


        [HttpPut("resetPassword")]
        public async Task<IActionResult> ResetPassword(string id)
        {
            UserViewModel user = await _userService.GetByIdAsync(new Guid(id));

            if (user == null)
            {
                return NotFound();
            }

            bool result = await _userService.ChangePasswordAsync(new Guid(id), "123456");
            return Ok(result);
        }

        [HttpPut("changePasswordForUser")]
        public async Task<IActionResult> ChangePasswordForUser(UserChangePasswordViewModel model)
        {
            UserViewModel user = await _userService.GetByIdAsync(new Guid(model.userId));

            if (user == null)
            {
                return NotFound();
            }

            bool result = await _userService.ChangePasswordForUser(model);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserUpdationViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Guid? id = userViewModel.Id;
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return BadRequest();
            }

            using (IDbContextTransaction transaction = _dataContext.Database.BeginTransaction())
            {
                try
                {
                    await _userService.UpdateAsync(userViewModel);

                    transaction.Commit();
                    return Ok(true);
                }
                catch (Exception e)
                {
                    if (!await _userService.CheckExistsAsync(id.Value))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
                    }
                }
            }
        }

        [HttpPut("updateForUser")]
        public async Task<IActionResult> UpdateForUser(UserUpdationViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Guid? id = userViewModel.Id;
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return BadRequest();
            }

            try
            {
                await _userService.UpdateForUserAsync(userViewModel);
                return Ok(true);
            }
            catch (Exception e)
            {
                if (!await _userService.CheckExistsAsync(id.Value))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return BadRequest();
            }

            bool isExists = await _userService.CheckExistsAsync(id.Value);

            if (!isExists)
            {
                return NotFound();
            }
            try
            {
                await _userService.DeleteAsync(id.Value);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
            return Ok(true);
        }
    }
}