using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Backend.Extension;
using Data.Enum;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IHostingEnvironment _hostingEnvironment;

        public StudentController(IStudentService studentService, IHostingEnvironment hostingEnvironment)
        {
            _studentService = studentService;
            _hostingEnvironment = hostingEnvironment;
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

        [HttpGet("GetByIdAllInfo/{id}")]
        public async Task<IActionResult> GetByIdAllInfo(Guid? id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return BadRequest();
            }

            StudentViewModel result = await _studentService.GetByIdAllInfoAsync(id.Value);
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
        public async Task<IActionResult> Create([FromForm]StudentViewModel studentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                studentViewModel.Code = "HS" + DateTime.Now.ToString("MMddyyhhmmssff");
                string getExtension = Path.GetExtension(studentViewModel.File.FileName);
                string imgUploadFolder = Path.Combine(_hostingEnvironment.ContentRootPath, "Assets/uploaded/img");

                if (!Directory.Exists(imgUploadFolder))
                {
                    Directory.CreateDirectory(imgUploadFolder);
                }
                if (getExtension == ".jpg" || getExtension == ".png")
                {
                    string uploadFolder = Path.Combine(_hostingEnvironment.ContentRootPath, "Assets/uploaded/img");
                    string imgFileName = Guid.NewGuid() + getExtension;
                    string imglFilePath = Path.Combine(uploadFolder, imgFileName);
                    FileInfo fileLocation = new FileInfo(imglFilePath);
                    using (var fileStream = new FileStream(imglFilePath, FileMode.Create))
                    {
                        await studentViewModel.File.CopyToAsync(fileStream);
                    }
                    studentViewModel.imageLink = imgFileName;
                }
                else
                {
                    return BadRequest();
                }
                StudentViewModel result = await _studentService.CreateAsync(studentViewModel);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm]StudentViewModel studentViewModel)
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

        [HttpPut("DinhChiHoc")]
        public async Task<IActionResult> DinhChiHoc(DinhChiHocViewModel model)
        {
            if (string.IsNullOrEmpty(model.GradeId.Value.ToString()))
            {
                return BadRequest();
            }
            try
            {
                await _studentService.DinhChiHocAsync(model);
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