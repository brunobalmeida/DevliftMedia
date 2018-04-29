using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EventApp.Models;

namespace EventApp.Controllers
{
    [Authorize(Roles = "Admin, Developer")]
    public class RoleManagementController : Controller
    {

        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public RoleManagementController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles.OrderBy(a => a.Name);
            return View(roles);
        }

        public async Task<IActionResult> AddRole(string roleName)
        {
            if (String.IsNullOrEmpty(roleName))
            {
                TempData["message"] = "Role name cannot be empty or null";
            }
            else if (await _roleManager.RoleExistsAsync(roleName))
            {
                TempData["message"] = "This Role already exists";
            }
            else
            {
                try
                {
                    IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(roleName));
                    if (result.Succeeded)
                    {
                        TempData["message"] = $"Role '{roleName}' created.";
                    }
                    else
                    {
                        throw new Exception(result.Errors.FirstOrDefault().Description);
                    }
                }
                catch (Exception e)
                {
                    TempData["message"] = $"Exception creating role '{roleName}': {e.GetBaseException().Message}";
                }
            }

            return RedirectToAction("index", "rolemanagement");

        }

        public async Task<IActionResult> DeleteRole(string roleName, Boolean deleteConfirmed = false)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                TempData["message"] = $"'{roleName}' does not exits";
                return RedirectToAction("index");
            }

            var users = await _userManager.GetUsersInRoleAsync(roleName);

            if (users.Any() && deleteConfirmed == false)
            {
                TempData["message"] = "There are users in this role, please remove them first.";
                return Redirect("index");
            }

            try
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    TempData["message"] = "Role Deleted";
                }
                else
                {
                    throw new Exception(result.Errors.FirstOrDefault().Description);
                }
            }
            catch (Exception e)
            {
                TempData["message"] = "Exception on delete : " + e.GetBaseException().Message;
            }
            return Redirect("index");

        }

        public async Task<IActionResult> ManageUsers(string roleName)
        {

            if (roleName != null)
            {
                HttpContext.Session.SetString("roleName", roleName);
            }
            else if (HttpContext.Session.GetString(nameof(roleName)) != null)
            {
                roleName = HttpContext.Session.GetString(nameof(roleName));
            }
            else
            {
                TempData["message"] = "Select a role to manage its users";
                return RedirectToAction("index");
            }

            var usersInRole = (await _userManager.GetUsersInRoleAsync(roleName)).OrderBy(a => a.UserName);
            var users = _userManager.Users;
            var usersNotInRole = _userManager.Users.Where(a => !usersInRole.Contains(a)).OrderBy(a => a.UserName);
            ViewData["addUser"] = new SelectList(usersNotInRole, "UserName", "UserName");
            return View(usersInRole);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToRole(string addUser)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByNameAsync(addUser);
                IdentityResult result = await _userManager.AddToRoleAsync(user, HttpContext.Session.GetString("roleName"));

                if (result.Succeeded)
                {
                    TempData["message"] = $"User {addUser} added to role";
                }
                else
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
            catch (Exception e)
            {
                TempData["message"] = $"Exception adding user to role: {e.GetBaseException().Message}";
            }

            return RedirectToAction("manageusers");

        }

        public IActionResult RemoveFromRole(string userName)
        {
            if (userName != null)
            {
                HttpContext.Session.SetString("userName", userName);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromRole()
        {
            try
            {
                string userName = HttpContext.Session.GetString("userName");
                ApplicationUser user = await _userManager.FindByNameAsync(userName);
                IdentityResult result = await _userManager.RemoveFromRoleAsync(user, HttpContext.Session.GetString("roleName"));
                if (result.Succeeded)
                {
                    TempData["message"] = $"User {userName} removed from role {HttpContext.Session.GetString("roleName")}";
                }
                else
                {
                    throw new Exception(result.Errors.First().Description);
                }

            }
            catch (Exception e)
            {
                TempData["message"] = $"Exception removing user from role: {e.GetBaseException().Message}";
            }

            return RedirectToAction("manageusers");
        }




    }
}