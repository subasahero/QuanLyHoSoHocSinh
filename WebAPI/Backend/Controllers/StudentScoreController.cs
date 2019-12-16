using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudenMangerServices.Interfaces;
using StudenMangerServices.ViewModel;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentScoreController : ControllerBase
    {
        private readonly IStudentScoreService _studentScoreService;

        public StudentScoreController(IStudentScoreService studentScoreService)
        {
            _studentScoreService = studentScoreService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid? id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return BadRequest();
            }

            StudentScoreViewModel result = await _studentScoreService.GetByIdAsync(id.Value);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentScoreViewModel studentScoreViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                StudentScoreViewModel result = await _studentScoreService.CreateAsync(studentScoreViewModel);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(StudentScoreViewModel studentScoreViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Guid? id = studentScoreViewModel.Id;
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return BadRequest();
            }
            try
            {
                await _studentScoreService.UpdateAsync(studentScoreViewModel);
            }
            catch (Exception e)
            {
                if (!await _studentScoreService.CheckExistsAsync(id.Value))
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

            bool isExists = await _studentScoreService.CheckExistsAsync(id.Value);

            if (!isExists)
            {
                return NotFound();
            }
            try
            {
                await _studentScoreService.DeleteAsync(id.Value);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
            return Ok(true);
        }
    }
}