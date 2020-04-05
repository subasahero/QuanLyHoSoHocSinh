using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudenMangerServices.Interfaces;
using StudenMangerServices.ViewModel;
using Utility.Dtos;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IList<RoleViewModel> data = await _roleService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            PagedList<RoleViewModel> paged = await _roleService.GetAllPagingAsync(pagingParams);
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

            RoleViewModel data = await _roleService.GetByIdAsync(id.Value);
            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        [AllowAnonymous]
        [HttpGet("GetIdByName/{roleName}")]
        public async Task<IActionResult> GetIdByName(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return BadRequest();
            }

            Guid? result = await _roleService.GetIdByNameAsync(roleName);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel roleViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                RoleViewModel result = await _roleService.CreateAsync(roleViewModel);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(RoleViewModel roleViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Guid? id = roleViewModel.Id;
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return BadRequest();
            }

            try
            {
                await _roleService.UpdateAsync(roleViewModel);
            }
            catch (Exception e)
            {
                if (!await _roleService.CheckExistsAsync(id.Value))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
                }
            }

            return Ok(true);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return BadRequest();
            }

            bool isExists = await _roleService.CheckExistsAsync(id.Value);
            if (!isExists)
            {
                return NotFound();
            }

            try
            {
                await _roleService.DeleteAsync(id.Value);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            return Ok(true);
        }
    }
}