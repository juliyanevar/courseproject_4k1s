using CourseProject.Dto.FacultyDto;
using CourseProject.Models;
using CourseProject.Repository.Interfaces;
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
    public class FacultyController : ControllerBase
    {
        private IRepositoryWrapper _repositoryWrapper;

        public FacultyController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repositoryWrapper.Faculty.FindAllAsync();
            if(result!=null)
            {
                return new JsonResult(result);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] CreateFacultyDto model)
        {
            if (ModelState.IsValid)
            {
                var faculty = new Faculty
                {
                    Id = Guid.NewGuid(),
                    Name = model.Name
                };
                await _repositoryWrapper.Faculty.CreateAsync(faculty);
                return Ok(new Response { Status = "Success", Message = "Faculty created successfully" });
            }
            return NotFound();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateFacultyDto model)
        {
            if (ModelState.IsValid)
            {
                var faculty = new Faculty
                {
                    Id = model.Id,
                    Name = model.Name
                };
                await _repositoryWrapper.Faculty.UpdateAsync(faculty);
                return Ok(new Response { Status = "Success", Message = "Faculty update successfully" });
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteFacultyDto model)
        {
            if (ModelState.IsValid)
            {
                var faculty = await _repositoryWrapper.Faculty.FindFirstByConditionAsync(x => x.Id.Equals(model.Id));
                await _repositoryWrapper.Faculty.DeleteAsync(faculty);
                return Ok(new Response { Status = "Success", Message = "Faculty deleted successfully" });
            }
            return NotFound();
        }
    }
}
