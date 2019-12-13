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
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IList<StudentViewModel> result = await _studentService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("GetStudentByLevel/{levelEnum}")]
        public async Task<IActionResult> GetStudentByLevel(LevelEnum levelEnum)
        {
            List<StudentViewModel> result = await _studentService.GetStudentByLevelAsync(levelEnum);
            return Ok(result);
        }

        [HttpGet("reportEnrollment")]
        public IActionResult reportEnrollment([FromQuery]ReportParam reportParam)
        {
            List<StudentFlowYearViewModel> result = _studentService.GetStudentReportEnrollment(reportParam.FromYear, reportParam.ToYear);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid? id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return BadRequest();
            }

            StudentViewModel result = await _studentService.GetByIdAsync(id.Value);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            PagedList<StudentViewModel> paged = await _studentService.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged.Items);
        }

        [HttpGet("GetStudentByLevelPaging/{levelEnum}")]
        public async Task<IActionResult> GetStudentByLevelPaging([FromQuery]PagingParams pagingParams, LevelEnum levelEnum)
        {
            PagedList<StudentViewModel> paged = await _studentService.GetStudentByLevelPagingAsync(pagingParams, levelEnum);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged.Items);
        }

        [HttpGet("CheckExistsRecord/{checkWord}")]
        public async Task<IActionResult> CheckExistsName(string checkWord)
        {
            var result = await _studentService.CheckExistsRecordAsync(checkWord);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentViewModel studentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                studentViewModel.Code = "SV" + DateTime.Now.ToString("MMddyyhhmmssff");
                StudentViewModel result = await _studentService.CreateAsync(studentViewModel);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(StudentViewModel studentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Guid? id = studentViewModel.Id;
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return BadRequest();
            }
            try
            {
                await _studentService.UpdateAsync(studentViewModel);
            }
            catch (Exception e)
            {
                if (!await _studentService.CheckExistsAsync(id.Value))
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

        [HttpPut("ChangeGrade")]
        public async Task<IActionResult> changeGradeAsync(ChangeGradeViewModel changeGradeViewModel)
        {
            if (changeGradeViewModel.studentsId.Count() == 0)
            {
                return BadRequest();
            }
            if (string.IsNullOrEmpty(changeGradeViewModel.gradeId.ToString()))
            {
                return BadRequest();
            }
            try
            {
                await _studentService.ChangeGradeAsync(changeGradeViewModel);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
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

            bool isExists = await _studentService.CheckExistsAsync(id.Value);

            if (!isExists)
            {
                return NotFound();
            }
            try
            {
                await _studentService.DeleteAsync(id.Value);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
            return Ok(true);
        }
    }
}