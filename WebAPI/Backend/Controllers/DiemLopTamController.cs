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
    public class DiemLopTamController : ControllerBase
    {
        private readonly IDiemLopTamService _diemLopTamService;

        public DiemLopTamController(IDiemLopTamService diemLopTamService)
        {
            _diemLopTamService = diemLopTamService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid? id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return BadRequest();
            }

            DiemLopTamViewModel result = await _diemLopTamService.GetById(id.Value);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DiemLopTamViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var result = await _diemLopTamService.Insert(model);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(DiemLopTamViewModel model)
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
                await _diemLopTamService.Update(model);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            return Ok(true);
        }
    }
}