using CourseProject.Dto;
using CourseProject.Dto.Profession;
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
    public class ProfessionController : ControllerBase
    {
        private IRepositoryWrapper _repositoryWrapper;

        public ProfessionController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repositoryWrapper.Profession.FindAllAsync();
            var result1 = new List<UpdateProfessionDto>();
            if (result != null)
            {
                foreach (var item in result)
                {
                    var faculty = await _repositoryWrapper.Faculty.FindFirstByConditionAsync(x => x.Id.Equals(item.FacultyId));
                    var newProfession = new UpdateProfessionDto
                    {
                        Id = item.Id,
                        Name = item.Name,
                        FacultyName = faculty.Name
                    };
                    result1.Add(newProfession);
                }
                return new JsonResult(result1);
            }
            return NotFound();
        }


        [HttpGet("facultyName")]
        [Route("GetProfessionNamesByFaculty")]
        public async Task<IActionResult> GetProfessionNamesByFaculty(string facultyName)
        {
            var faculty = await _repositoryWrapper.Faculty.FindFirstByConditionAsync(x => x.Name.Equals(facultyName));
            var result = await _repositoryWrapper.Profession.FindByConditionAsync(x=>x.FacultyId.Equals(faculty.Id));
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
            var result = await _repositoryWrapper.Profession.FindAllAsync();
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
        public async Task<IActionResult> Create([FromBody] CreateProfessionDto model)
        {
            if (ModelState.IsValid)
            {
                var faculty = await _repositoryWrapper.Faculty.FindFirstByConditionAsync(x => x.Name.Equals(model.FacultyName));
                var profession = new Profession
                {
                    Id = Guid.NewGuid(),
                    Name = model.Name,
                    Faculty = faculty
                };
                await _repositoryWrapper.Profession.CreateAsync(profession);
                return Ok(new Response { Status = "Success", Message = "Profession created successfully" });
            }
            return NotFound();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateProfessionDto model)
        {
            if (ModelState.IsValid)
            {
                var faculty = await _repositoryWrapper.Faculty.FindFirstByConditionAsync(x => x.Name.Equals(model.FacultyName));
                var profession = new Profession
                {
                    Id = model.Id,
                    Name = model.Name,
                    Faculty = faculty
                };
                await _repositoryWrapper.Profession.UpdateAsync(profession);
                return Ok(new Response { Status = "Success", Message = "Profession update successfully" });
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteProfessionDto model)
        {
            if (ModelState.IsValid)
            {
                var profession = await _repositoryWrapper.Profession.FindFirstByConditionAsync(x => x.Id.Equals(model.Id));
                await _repositoryWrapper.Profession.DeleteAsync(profession);
                return Ok(new Response { Status = "Success", Message = "Profession deleted successfully" });
            }
            return NotFound();
        }
    }
}
