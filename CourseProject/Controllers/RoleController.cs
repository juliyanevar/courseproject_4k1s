using CourseProject.Dto.RoleDto;
using CourseProject.Models;
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
    public class RoleController : ControllerBase
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<User> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        
        
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody]CreateRoleDto model)
        {
            if (!string.IsNullOrEmpty(model.Name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(model.Name));
                if (result.Succeeded)
                {
                    return Ok(new Response { Status="Success", Message="Create role successfully"});
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Can not create role" });
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Role name is empty" });
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromBody]DeleteRoleDto model)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(model.Id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
            }
            return Ok(new Response { Status="Success", Message="Delete role successfully"});
        }


        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Edit([FromBody]ChangeRoleDto model)
        {
            // получаем пользователя
            User user = await _userManager.FindByIdAsync(model.UserId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                // получаем все роли
                var allRoles = _roleManager.Roles.ToList();
                // получаем список ролей, которые были добавлены
                var addedRoles = model.Roles.Except(userRoles);
                // получаем роли, которые были удалены
                var removedRoles = userRoles.Except(model.Roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return Ok(new Response { Status = "Success", Message = "Edit role successfully" });
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User not found" });
        }
    }
}
