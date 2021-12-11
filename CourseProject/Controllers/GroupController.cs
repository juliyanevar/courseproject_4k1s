using CourseProject.Dto.GroupDto;
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
    public class GroupController : ControllerBase
    {
        private IRepositoryWrapper _repositoryWrapper;

        public GroupController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repositoryWrapper.Group.FindAllAsync();
            var result1 = new List<UpdateGroupDto>();
            if (result != null)
            {
                foreach (var item in result)
                {
                    var profession = await _repositoryWrapper.Profession.FindFirstByConditionAsync(x => x.Id.Equals(item.ProfessionId));
                    var newGroup = new UpdateGroupDto
                    {
                        Id = item.Id,
                        Number = item.Number.ToString(),
                        Course = item.Course.ToString(),
                        ProfessionName = profession.Name
                    };
                    result1.Add(newGroup);
                }
                return new JsonResult(result1);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("GetCourses")]
        public async Task<IActionResult> GetCourses()
        {
            var result = await _repositoryWrapper.Group.FindAllAsync();
            var result1 = new List<string>();
            if (result != null)
            {
                foreach (var item in result)
                {
                    result1.Add(item.Course.ToString());
                }
                return new JsonResult(result1.Distinct());
            }
            return NotFound();
        }

        [HttpGet]
        [Route("GetNumberOfGroup")]
        public async Task<IActionResult> GetNumberOfGroup()
        {
            var result = await _repositoryWrapper.Group.FindAllAsync();
            var result1 = new List<string>();
            if (result != null)
            {
                foreach (var item in result)
                {
                    result1.Add(item.Number.ToString());
                }
                return new JsonResult(result1.Distinct());
            }
            return NotFound();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] CreateGroupDto model)
        {
            if (ModelState.IsValid)
            {
                var profession = await _repositoryWrapper.Profession.FindFirstByConditionAsync(x => x.Name.Equals(model.ProfessionName));
                var group = new Group
                {
                    Id = Guid.NewGuid(),
                    Number = Int32.Parse(model.Number),
                    Course = Int32.Parse(model.Course),
                    Profession = profession
                };
                await _repositoryWrapper.Group.CreateAsync(group);
                return Ok(new Response { Status = "Success", Message = "Group created successfully" });
            }
            return NotFound();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateGroupDto model)
        {
            if (ModelState.IsValid)
            {
                var profession = await _repositoryWrapper.Profession.FindFirstByConditionAsync(x => x.Name.Equals(model.ProfessionName));
                var group = new Group
                {
                    Id = model.Id,
                    Number = Int32.Parse(model.Number),
                    Course = Int32.Parse(model.Course),
                    Profession = profession
                };
                await _repositoryWrapper.Group.UpdateAsync(group);
                return Ok(new Response { Status = "Success", Message = "Group update successfully" });
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteGroupDto model)
        {
            if (ModelState.IsValid)
            {
                var group = await _repositoryWrapper.Group.FindFirstByConditionAsync(x => x.Id.Equals(model.Id));
                await _repositoryWrapper.Group.DeleteAsync(group);
                return Ok(new Response { Status = "Success", Message = "Group deleted successfully" });
            }
            return NotFound();
        }
    }
}
