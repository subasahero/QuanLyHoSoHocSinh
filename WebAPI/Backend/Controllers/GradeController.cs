using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Extension;
using Data.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudenMangerServices.Interfaces;
using StudenMangerServices.ViewModel;
using Utility.Dtos;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IGradeService _gradeService;

        public GradeController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IList<GradeViewModel> result = await _gradeService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("CheckExistsRecord/{name}/{levelId}")]
        public async Task<IActionResult> CheckExistsName(string name, LevelEnum levelEnum)
        {
            var result = await _gradeService.CheckExistsRecordAsync(name, levelEnum);
            return Ok(result);
        }

        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            PagedList<GradeViewModel> paged = await _gradeService.GetAllPagingAsync(pagingParams);
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

            GradeViewModel result = await _gradeService.GetByIdAsync(id.Value);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("GetGradeByLevel/{level}")]
        public async Task<IActionResult> GetGradeByLevel(int level)
        {
            List<GradeViewModel> result = await _gradeService.GetGradeByLevelAsync(level);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GradeViewModel gradeViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                GradeViewModel result = await _gradeService.CreateAsync(gradeViewModel);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(GradeViewModel gradeViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Guid? id = gradeViewModel.Id;
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return BadRequest();
            }
            try
            {
                await _gradeService.UpdateAsync(gradeViewModel);
            }
            catch (Exception e)
            {
                if (!await _gradeService.CheckExistsAsync(id.Value))
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

            bool isExists = await _gradeService.CheckExistsAsync(id.Value);

            if (!isExists)
            {
                return NotFound();
            }
            try
            {
                await _gradeService.DeleteAsync(id.Value);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
            return Ok(true);
        }
    }
}