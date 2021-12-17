using CourseProject.Dto.AttendanceDto;
using CourseProject.Models;
using CourseProject.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    public class AttendanceController : ControllerBase
    {
        private IRepositoryWrapper _repositoryWrapper;
        private readonly UserManager<User> _userManager;

        public AttendanceController(IRepositoryWrapper repositoryWrapper, UserManager<User> userManager)
        {
            _repositoryWrapper = repositoryWrapper;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repositoryWrapper.Attendance.FindAllAsync();
            var result1 = new List<UpdateAttendanceDto>();
            if (result != null)
            {
                foreach (var item in result)
                {
                    var subject = await _repositoryWrapper.Subject.FindFirstByConditionAsync(x => x.Id.Equals(item.SubjectId));
                    var auditorium = await _repositoryWrapper.Auditorium.FindFirstByConditionAsync(x => x.Id.Equals(item.AuditoriumId));
                    var teacher = await _userManager.FindByIdAsync(item.UserTeacherId.ToString());
                    var student = await _userManager.FindByIdAsync(item.UserStudentId.ToString());
                    var newAttendance = new UpdateAttendanceDto
                    {
                        Id = item.Id,
                        DateTime = item.DateTime,
                        SubjectName = subject.Name,
                        AuditoriumName = auditorium.Name,
                        TeacherUsername = teacher.UserName,
                        StudentUsername = student.UserName
                    };
                    result1.Add(newAttendance);
                }
                return new JsonResult(result1);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] CreateAttendanceDto model)
        {
            if (ModelState.IsValid)
            {
                var subject = await _repositoryWrapper.Subject.FindFirstByConditionAsync(x => x.Id.Equals(new Guid(model.SubjectId)));
                var auditorium = await _repositoryWrapper.Auditorium.FindFirstByConditionAsync(x => x.Name.Equals(model.AuditoriumName));
                var teacher = await _userManager.FindByEmailAsync(model.TeacherUsername);
                var student = await _userManager.GetUserAsync(this.User);
                var attendance = new Attendance
                {
                    Id = Guid.NewGuid(),
                    DateTime = DateTime.Parse(model.DateTime),
                    Subject = subject,
                    Auditorium = auditorium,
                    UserTeacherId = teacher.Id,
                    UserStudentId = student.Id
                };
                await _repositoryWrapper.Attendance.CreateAsync(attendance);
                return Ok(new Response { Status = "Success", Message = "Attendance created successfully" });
            }
            return NotFound();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateAttendanceDto model)
        {
            if (ModelState.IsValid)
            {
                var subject = await _repositoryWrapper.Subject.FindFirstByConditionAsync(x => x.Id.Equals(model.SubjectName));
                var auditorium = await _repositoryWrapper.Auditorium.FindFirstByConditionAsync(x => x.Id.Equals(model.AuditoriumName));
                var teacher = await _userManager.FindByNameAsync(model.TeacherUsername);
                var student = await _userManager.FindByNameAsync(model.StudentUsername);
                var attendance = new Attendance
                {
                    Id = model.Id,
                    DateTime = model.DateTime,
                    Subject = subject,
                    Auditorium = auditorium,
                    UserTeacher = teacher,
                    UserStudent = student
                };
                await _repositoryWrapper.Attendance.UpdateAsync(attendance);
                return Ok(new Response { Status = "Success", Message = "Attendance update successfully" });
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteAttendanceDto model)
        {
            if (ModelState.IsValid)
            {
                var attendance = await _repositoryWrapper.Attendance.FindFirstByConditionAsync(x => x.Id.Equals(model.Id));
                await _repositoryWrapper.Attendance.DeleteAsync(attendance);
                return Ok(new Response { Status = "Success", Message = "Attendance deleted successfully" });
            }
            return NotFound();
        }
    }
}
