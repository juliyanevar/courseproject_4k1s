using CourseProject.Dto.PulpitDto;
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
    public class PulpitController : ControllerBase
    {
        private IRepositoryWrapper _repositoryWrapper;

        public PulpitController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repositoryWrapper.Pulpit.FindAllAsync();
            var result1 = new List<UpdatePulpitDto>();
            if (result != null)
            {
                foreach(var item in result)
                {
                    var faculty= await _repositoryWrapper.Faculty.FindFirstByConditionAsync(x => x.Id.Equals(item.FacultyId));
                    var newPulpit = new UpdatePulpitDto
                    {
                        Id = item.Id,
                        Name = item.Name,
                        FacultyName = faculty.Name
                    };
                    result1.Add(newPulpit);
                }
                return new JsonResult(result1);
            }
            return NotFound();
        }

        [HttpGet("facultyName")]
        [Route("GetPulpitNamesByFaculty")]
        public async Task<IActionResult> GetPulpitNamesByFaculty(string facultyName)
        {
            var faculty = await _repositoryWrapper.Faculty.FindFirstByConditionAsync(x => x.Name.Equals(facultyName));
            var result = await _repositoryWrapper.Pulpit.FindByConditionAsync(x => x.FacultyId.Equals(faculty.Id));
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

        [HttpGet]
        [Route("GetFacultyNames")]
        public async Task<IActionResult> GetFacultyNames()
        {
            var result = await _repositoryWrapper.Pulpit.FindAllAsync();
            var result1 = new List<string>();
            if (result != null)
            {
                foreach (var item in result)
                {
                    var faculty = await _repositoryWrapper.Faculty.FindFirstByConditionAsync(x => x.Id.Equals(item.FacultyId));
                    result1.Add(faculty.Name);
                }
                return new JsonResult(result1.Distinct());
            }
            return NotFound();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] CreatePulpitDto model)
        {
            if (ModelState.IsValid)
            {
                var faculty = await _repositoryWrapper.Faculty.FindFirstByConditionAsync(x => x.Name.Equals(model.FacultyName));
                var pulpit = new Pulpit
                {
                    Id = Guid.NewGuid(),
                    Name = model.Name,
                    Faculty = faculty
                };
                await _repositoryWrapper.Pulpit.CreateAsync(pulpit);
                return Ok(new Response { Status = "Success", Message = "Pulpit created successfully" });
            }
            return NotFound();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] UpdatePulpitDto model)
        {
            if (ModelState.IsValid)
            {
                var faculty = await _repositoryWrapper.Faculty.FindFirstByConditionAsync(x => x.Name.Equals(model.FacultyName));
                var pulpit = new Pulpit
                {
                    Id = model.Id,
                    Name = model.Name,
                    Faculty = faculty
                };
                await _repositoryWrapper.Pulpit.UpdateAsync(pulpit);
                return Ok(new Response { Status = "Success", Message = "Pulpit update successfully" });
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeletePulpitDto model)
        {
            if (ModelState.IsValid)
            {
                var pulpit = await _repositoryWrapper.Pulpit.FindFirstByConditionAsync(x => x.Id.Equals(model.Id));
                await _repositoryWrapper.Pulpit.DeleteAsync(pulpit);
                return Ok(new Response { Status = "Success", Message = "Pulpit deleted successfully" });
            }
            return NotFound();
        }
    }
}
