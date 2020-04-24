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
    public class DiemLopChinController : ControllerBase
    {
        private readonly IDiemLopChinService _diemLopChinService;

        public DiemLopChinController(IDiemLopChinService diemLopChinService)
        {
            _diemLopChinService = diemLopChinService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid? id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return BadRequest();
            }

            DiemLopChinViewModel result = await _diemLopChinService.GetById(id.Value);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DiemLopChinViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var result = await _diemLopChinService.Insert(model);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(DiemLopChinViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Guid? id = model.Id;
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return BadRequest();
            }
            try
            {
                await _diemLopChinService.Update(model);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            return Ok(true);
        }
    }
}