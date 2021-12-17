using CourseProject.Dto.AuditoriumDto;
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
    public class AuditoriumController : ControllerBase
    {
        private IRepositoryWrapper _repositoryWrapper;

        public AuditoriumController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repositoryWrapper.Auditorium.FindAllAsync();
            var result1 = new List<UpdateAuditoriumDto>();
            if (result != null)
            {
                foreach (var item in result)
                {
                    var auditoriumType = await _repositoryWrapper.AuditoriumType.FindFirstByConditionAsync(x => x.Id.Equals(item.AuditoriumTypeId));
                    var newAuditorium = new UpdateAuditoriumDto
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Capacity = item.Capacity.ToString(),
                        AuditoriumTypeName = auditoriumType.Name
                    };
                    result1.Add(newAuditorium);
                }
                return new JsonResult(result1);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("GetAuditoriumNames")]
        public async Task<IActionResult> GetAuditoriumNames()
        {
            var result = await _repositoryWrapper.Auditorium.FindAllAsync();
            var result1 = new List<string>();
            if (result != null)
            {
                foreach (var item in result)
                {
                    result1.Add(item.Name);
                }
                return new JsonResult(result1.Distinct());
            }
            return NotFound();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] CreateAuditoiumDto model)
        {
            if (ModelState.IsValid)
            {
                var auditoriumType = await _repositoryWrapper.AuditoriumType.FindFirstByConditionAsync(x => x.Name.Equals(model.AuditoriumTypeName));
                var auditorium = new Auditorium
                {
                    Id = Guid.NewGuid(),
                    Capacity = Int32.Parse(model.Capacity),
                    Name = model.Name,
                    AuditoriumType = auditoriumType
                };
                await _repositoryWrapper.Auditorium.CreateAsync(auditorium);
                return Ok(new Response { Status = "Success", Message = "Auditorium created successfully" });
            }
            return NotFound();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateAuditoriumDto model)
        {
            if (ModelState.IsValid)
            {
                var auditoriumType = await _repositoryWrapper.AuditoriumType.FindFirstByConditionAsync(x => x.Name.Equals(model.AuditoriumTypeName));
                var auditorium = new Auditorium
                {
                    Id = model.Id,
                    Capacity = Int32.Parse(model.Capacity),
                    Name = model.Name,
                    AuditoriumType = auditoriumType
                };
                await _repositoryWrapper.Auditorium.UpdateAsync(auditorium);
                return Ok(new Response { Status = "Success", Message = "Auditorium update successfully" });
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteAuditoriumDto model)
        {
            if (ModelState.IsValid)
            {
                var auditorium = await _repositoryWrapper.Auditorium.FindFirstByConditionAsync(x => x.Id.Equals(model.Id));
                await _repositoryWrapper.Auditorium.DeleteAsync(auditorium);
                return Ok(new Response { Status = "Success", Message = "Auditorium deleted successfully" });
            }
            return NotFound();
        }
    }
}
