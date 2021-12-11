using CourseProject.Dto.UserDto;
using CourseProject.Models;
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

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
    }
}
