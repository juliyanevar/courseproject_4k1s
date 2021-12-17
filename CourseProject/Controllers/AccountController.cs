using CourseProject.Dto.UserDto;
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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private IRepositoryWrapper _repositoryWrapper;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IRepositoryWrapper repositoryWrapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpGet]
        [Route("GetUserInfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            var user = await _userManager.GetUserAsync(this.User);
            if (user != null)
            {
                var result = new UpdateUserInfoDto { userName = user.UserName, email = user.Email, firstName=user.FirstName, lastName = user.LastName };
                return new JsonResult(result);
            }
            return null;
        }

        [HttpPut]
        [Route("UpdateUserInfo")]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UpdateUserInfoDto model)
        {
            var currentUser = this.User;
            var user = await _userManager.GetUserAsync(currentUser);
            user.UserName = model.userName;
            user.Email = model.email;
            user.FirstName = model.firstName;
            user.LastName = model.lastName;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Can not update user" });
            }
            return Ok(new Response { Status = "Success", Message = "User update successfully" });
        }


        [HttpPut]
        [Route("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentDto model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            var profession = await _repositoryWrapper.Profession.FindFirstByConditionAsync(x => x.Name.Equals(model.professionName));
            var group1 = await _repositoryWrapper.Group.FindByConditionAsync(x => x.ProfessionId.Equals(profession.Id));
            var groupResult = new Group();
            foreach (var item in group1)
            {
                if (item.Course == Int32.Parse(model.course) && item.Number == Int32.Parse(model.groupNumber))
                {
                    groupResult = item;
                    break;
                }
            }
            user.Group = groupResult;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Can not update user" });
            }
            return Ok(new Response { Status = "Success", Message = "User update successfully" });
        }


        [HttpPut]
        [Route("UpdateTeacher")]
        public async Task<IActionResult> UpdateTeacher([FromBody] UpdateTeacherDto model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            var pulpit = await _repositoryWrapper.Pulpit.FindFirstByConditionAsync(x => x.Name.Equals(model.pulpitName));
            user.Pulpit = pulpit;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Can not update user" });
            }
            return Ok(new Response { Status = "Success", Message = "User update successfully" });
        }


        [HttpGet]
        [Route("GetCurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _userManager.GetUserAsync(this.User);
            if (user != null) 
            {
                var roleList = await _userManager.GetRolesAsync(user);
                var role = roleList.FirstOrDefault();
                var result = new UserRoleDto { UserName = user.UserName, RoleName = role };
                return new JsonResult(result);
            }
            return null;
        }

        [HttpGet]
        [Route("GetStudents")]
        public async Task<IActionResult> GetStudents()
        {
            var users = _userManager.Users.ToList();
            List<GetStudentsDto> students = new List<GetStudentsDto>();
            foreach(var item in users)
            {
                var roleList = await _userManager.GetRolesAsync(item);
                var roleName = roleList.FirstOrDefault();
                if (roleName == "student")
                {
                    var group = await _repositoryWrapper.Group.FindFirstByConditionAsync(x=>x.Id.Equals(item.GroupId));
                    var profession = await _repositoryWrapper.Profession.FindFirstByConditionAsync(x => x.Id.Equals(group.ProfessionId));
                    var student = new GetStudentsDto
                    {
                        Id=item.Id,
                        UserName = item.UserName,
                        Email = item.Email,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        GroupNumber = group.Number.ToString(),
                        Course = group.Course.ToString(),
                        ProfessionName = profession.Name
                    };
                    students.Add(student);
                }
            }
            return new JsonResult(students);
        }

        [HttpGet]
        [Route("GetTeachers")]
        public async Task<IActionResult> GetTeachers()
        {
            var users = _userManager.Users.ToList();
            List<GetTeacherDto> teachers = new List<GetTeacherDto>();
            foreach (var item in users)
            {
                var roleList = await _userManager.GetRolesAsync(item);
                var roleName = roleList.FirstOrDefault();
                if (roleName == "teacher")
                {
                    var pulpit = await _repositoryWrapper.Pulpit.FindFirstByConditionAsync(x => x.Id.Equals(item.PulpitId));
                    var teacher = new GetTeacherDto
                    {
                        Id = item.Id,
                        UserName = item.UserName,
                        Email = item.Email,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        PulpitName = pulpit.Name
                    };
                    teachers.Add(teacher);
                }
            }
            return new JsonResult(teachers);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto model)
        {
            var userExist = await _userManager.FindByEmailAsync(model.Email);
            if (userExist != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User with this email already exists" });
            var user = new User()
            {
                UserName = model.Email,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Can not create user" });
            }
            await _signInManager.SignInAsync(user, false);
            return Ok(new Response { Status = "Success", Message = "User created successfully" });
        }

        [HttpPost]
        [Route("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterUserDto model)
        {
            var userExist = await _userManager.FindByEmailAsync(model.Email);
            if (userExist != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User with username already exists" });
            var user = new User()
            {
                UserName = model.Email,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Can not create user" });
            }
            await _signInManager.SignInAsync(user, false);
            await _userManager.AddToRoleAsync(user, "admin");
            return Ok(new Response { Status = "Success", Message = "Admin created successfully" });
        }

        [HttpPut]
        [Route("AddRoleToUser")]
        public async Task<IActionResult> AddRoleToUser([FromBody] AddRoleUserDto model)
        {
            var currentUser = this.User;
            var user = await _userManager.GetUserAsync(currentUser);
            await _userManager.AddToRoleAsync(user, model.RoleName);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            var result=await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Can not update user" });
            }
            return Ok(new Response { Status = "Success", Message = "User update successfully" });
        }

        [HttpPut]
        [Route("AddGroupToStudent")]
        public async Task<IActionResult> AddGroupToStudent([FromBody] AddGroupToStudentDto model)
        {
            var currentUser = this.User;
            var user = await _userManager.GetUserAsync(currentUser);
            var profession = await _repositoryWrapper.Profession.FindFirstByConditionAsync(x => x.Name.Equals(model.ProfessionName));
            var group1 = await _repositoryWrapper.Group.FindByConditionAsync(x => x.ProfessionId.Equals(profession.Id));
            var groupResult = new Group();
            foreach(var item in group1)
            {
                if(item.Course == Int32.Parse(model.Course) && item.Number == Int32.Parse(model.Number))
                {
                    groupResult = item;
                    break;
                }
            }
            user.Group = groupResult;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Can not update user" });
            }
            return Ok(new Response { Status = "Success", Message = "User update successfully" });
        }

        [HttpPut]
        [Route("AddPulpitToTeacher")]
        public async Task<IActionResult> AddPulpitToTeacher([FromBody] AddPulpitToTeacherDto model)
        {
            var currentUser = this.User;
            var user = await _userManager.GetUserAsync(currentUser);
            var pulpit = await _repositoryWrapper.Pulpit.FindFirstByConditionAsync(x => x.Name.Equals(model.PulpitName));
            user.Pulpit = pulpit;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Can not update user" });
            }
            return Ok(new Response { Status = "Success", Message = "User update successfully" });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, true, false);
                if (result.Succeeded)
                {
                    return Ok(new Response { Status = "Success", Message = "Login success" }); ;
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Incorrect username or password" }); ;
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Incorrect data" });
        }

        [HttpPost]
        [Route("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return Ok(new Response { Status = "Success", Message = "Logout success" }); ;
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteUserDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                await _userManager.DeleteAsync(user);
                return Ok(new Response { Status = "Success", Message = "User deleted successfully" });
            }
            return NotFound();
        }
    }
}
