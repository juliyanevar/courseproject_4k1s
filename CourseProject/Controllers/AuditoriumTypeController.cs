using CourseProject.Dto.AuditoriumTypeDto;
using CourseProject.Models;
using CourseProject.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuditoriumTypeController : ControllerBase
    {
        private IRepositoryWrapper _repositoryWrapper;

        public AuditoriumTypeController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repositoryWrapper.AuditoriumType.FindAllAsync();
            if (result != null)
            {
                return new JsonResult(result);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] CreateAuditoriumTypeDto model)
        {
            if (ModelState.IsValid)
            {
                var auditoriumType = new AuditoriumType
                {
                    Id = Guid.NewGuid(),
                    Name = model.Name
                };
                await _repositoryWrapper.AuditoriumType.CreateAsync(auditoriumType);
                return Ok(new Response { Status = "Success", Message = "Auditorium type created successfully" });
            }
            return NotFound();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateAuditoriumTypeDto model)
        {
            if (ModelState.IsValid)
            {
                var auditoriumType = new AuditoriumType
                {
                    Id = model.Id,
                    Name = model.Name
                };
                await _repositoryWrapper.AuditoriumType.UpdateAsync(auditoriumType);
                return Ok(new Response { Status = "Success", Message = "Auditorium type update successfully" });
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteAuditoriumTypeDto model)
        {
            if (ModelState.IsValid)
            {
                var auditoriumType = await _repositoryWrapper.AuditoriumType.FindFirstByConditionAsync(x => x.Id.Equals(model.Id));
                await _repositoryWrapper.AuditoriumType.DeleteAsync(auditoriumType);
                return Ok(new Response { Status = "Success", Message = "Auditorium type deleted successfully" });
            }
            return NotFound();
        }
    }
}
