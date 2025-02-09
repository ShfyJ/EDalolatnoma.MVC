﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EDalolatnoma.MVC.Constants;
using EDalolatnoma.MVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDalolatnoma.MVC.Controllers
{
[Authorize(Roles = "Администратор")]
public class UserRolesController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
         
    public UserRolesController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    public async Task<IActionResult> Index(string userId)
    {
        var viewModel = new List<UserRolesViewModel>();
        var user = await _userManager.FindByIdAsync(userId);
        foreach (var role in _roleManager.Roles.ToList())
        {
            var userRolesViewModel = new UserRolesViewModel
            {
                RoleName = role.Name
            };
            if (await _userManager.IsInRoleAsync(user, role.Name))
            {
                userRolesViewModel.Selected = true;
            }
            else
            {
                userRolesViewModel.Selected = false;
            }
            viewModel.Add(userRolesViewModel);
        }
        var model = new ManageUserRolesViewModel()
        {
            UserId = userId,
            UserRoles = viewModel
        };
            ViewBag.user = user.FioName+"("+user.UserName+")";
        return View(model);
    }

    public async Task<IActionResult> Update(string id, ManageUserRolesViewModel model)
    {
        var user = await _userManager.FindByIdAsync(id);
        var roles = await _userManager.GetRolesAsync(user);
        var result = await _userManager.RemoveFromRolesAsync(user, roles);
        result = await _userManager.AddToRolesAsync(user, model.UserRoles.Where(x => x.Selected).Select(y => y.RoleName));
        var currentUser = await _userManager.GetUserAsync(User);
        await _signInManager.RefreshSignInAsync(currentUser);
        await Seeds.DefaultUsers.SeedSuperAdminAsync(_userManager, _roleManager);
        return RedirectToAction("Index","Users");
    }
}
}