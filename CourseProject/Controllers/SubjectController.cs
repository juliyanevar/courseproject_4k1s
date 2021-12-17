using CourseProject.Dto.SubjectDto;
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
    public class SubjectController : ControllerBase
    {
        private IRepositoryWrapper _repositoryWrapper;

        public SubjectController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repositoryWrapper.Subject.FindAllAsync();
            var result1 = new List<UpdateSubjectDto>();
            if (result != null)
            {
                foreach (var item in result)
                {
                    var pulpit = await _repositoryWrapper.Pulpit.FindFirstByConditionAsync(x => x.Id.Equals(item.PulpitId));
                    var newSubject = new UpdateSubjectDto
                    {
                        Id = item.Id,
                        Name = item.Name,
                        PulpitName = pulpit.Name
                    };
                    result1.Add(newSubject);
                }
                return new JsonResult(result1);
            }
            return NotFound();
        }

        [HttpGet("pulpitName")]
        [Route("GetSubjectNamesByPulpit")]
        public async Task<IActionResult> GetSubjectNamesByPulpit(string pulpitName)
        {
            var pulpit = await _repositoryWrapper.Pulpit.FindFirstByConditionAsync(x => x.Name.Equals(pulpitName));
            var result = await _repositoryWrapper.Subject.FindByConditionAsync(x => x.PulpitId.Equals(pulpit.Id));
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
        public async Task<IActionResult> Create([FromBody] CreateSubjectDto model)
        {
            if (ModelState.IsValid)
            {
                var pulpit = await _repositoryWrapper.Pulpit.FindFirstByConditionAsync(x => x.Name.Equals(model.PulpitName));
                var subject = new Subject
                {
                    Id = Guid.NewGuid(),
                    Name = model.Name,
                    Pulpit = pulpit
                };
                await _repositoryWrapper.Subject.CreateAsync(subject);
                return Ok(new Response { Status = "Success", Message = "Subject created successfully" });
            }
            return NotFound();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateSubjectDto model)
        {
            if (ModelState.IsValid)
            {
                var pulpit = await _repositoryWrapper.Pulpit.FindFirstByConditionAsync(x => x.Name.Equals(model.PulpitName));
                var subject = new Subject
                {
                    Id = model.Id,
                    Name = model.Name,
                    Pulpit = pulpit
                };
                await _repositoryWrapper.Subject.UpdateAsync(subject);
                return Ok(new Response { Status = "Success", Message = "Subject update successfully" });
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteSubjectDto model)
        {
            if (ModelState.IsValid)
            {
                var subject = await _repositoryWrapper.Subject.FindFirstByConditionAsync(x => x.Id.Equals(model.Id));
                await _repositoryWrapper.Subject.DeleteAsync(subject);
                return Ok(new Response { Status = "Success", Message = "Subject deleted successfully" });
            }
            return NotFound();
        }
    }
}
