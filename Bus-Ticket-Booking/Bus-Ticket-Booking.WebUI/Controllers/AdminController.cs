using Bus_Ticket_Booking.Business.Abstract;
using Bus_Ticket_Booking.Entity;
using Bus_Ticket_Booking.WebUI.Identity;
using Bus_Ticket_Booking.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bus_Ticket_Booking.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly IRouteService _routeService;
        private readonly ICityService _cityService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public AdminController(ITicketService ticketService, IRouteService routeService, ICityService cityService, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _ticketService = ticketService;
            _routeService = routeService;
            _cityService = cityService;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult UserList()
        {
            return View(_userManager.Users);
        }
        public IActionResult UserCreate()
        {
            var roles = _roleManager.Roles.Select(i => i.Name);
            ViewBag.Roles = roles;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserCreate(UserDetailsModel model, string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.UserName,
                    Email = model.Email,
                    EmailConfirmed = model.EmailConfirmed
                };
                var result = await _userManager.CreateAsync(user, "Qwe123.");
                if (result.Succeeded)
                {
                    selectedRoles = selectedRoles ?? new string[] { };
                    await _userManager.AddToRolesAsync(user, selectedRoles);
                    return Redirect("~/admin/user/list");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                var roles = _roleManager.Roles.Select(i => i.Name);
                ViewBag.Roles = roles;
                return View(model);
            }
            var roles2 = _roleManager.Roles.Select(i => i.Name);
            ViewBag.Roles = roles2;
            return View(model);
        }
        public async Task<IActionResult> UserEdit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var selectedRoles = await _userManager.GetRolesAsync(user);
                var roles = _roleManager.Roles.Select(i => i.Name);
                ViewBag.Roles = roles;
                return View(new UserDetailsModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    SelectedRoles = selectedRoles
                });
            }
            return Redirect("~/admin/user/list");
        }
        [HttpPost]
        public async Task<IActionResult> UserEdit(UserDetailsModel model, string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    user.EmailConfirmed = model.EmailConfirmed;
                }
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    selectedRoles = selectedRoles ?? new string[] { };
                    await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles).ToArray<string>());
                    await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles).ToArray<string>());
                    return Redirect("~/admin/user/list");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                var roles2 = _roleManager.Roles.Select(i => i.Name);
                ViewBag.Roles = roles2;
                return View(model);
            }
            ModelState.AddModelError("", "Lütfen ilgili alanları kontrol ediniz!");
            var roles = _roleManager.Roles.Select(i => i.Name);
            ViewBag.Roles = roles;
            return View(model);
        }

        public async Task<IActionResult> DeleteUser(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            await _userManager.DeleteAsync(user);
            return Redirect("~/admin/user/list");
        }
        public IActionResult RoleList()
        {
            return View(_roleManager.Roles);
        }

        public IActionResult RoleCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RoleCreate(RoleModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(
                    new IdentityRole(model.Name));
                if (result.Succeeded)
                {
                    return RedirectToAction("RoleList");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        public async Task<ActionResult> RoleEdit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var members = new List<User>();
            var nonMembers = new List<User>();

            foreach (var user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            }
            var model = new RoleDetails()
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> RoleEdit(RoleEditDetails model)
        {
            if (ModelState.IsValid)
            {
                foreach (var userId in model.IdsToAdd ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        var result = await _userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }

                foreach (var userId in model.IdsToDelete ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        var result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }
            }
            return Redirect("/admin/role/" + model.RoleId);
        }

        public async Task<IActionResult> DeleteRole(string RoleId)
        {
            var role = await _roleManager.FindByIdAsync(RoleId);
            await _roleManager.DeleteAsync(role);
            return Redirect("~/admin/role/list");
        }
        public IActionResult AdminList()
        {
            return View(new RouteTicket()
            {
                Tickets = _ticketService.GetAll()
            });
        }

        public IActionResult CancelTicket(int ticketId)
        {
            var ticket = _ticketService.GetById(ticketId);
            if (ticket != null)
            {
                _ticketService.Delete(ticket);
            }
            return RedirectToAction("AdminList");
        }

        public IActionResult ListRoute()
        {
            return View(new RouteTicket()
            {
                Routes = _routeService.GetAll()
            });
        }
        public IActionResult CreateRoute()
        {
            ViewBag.Routes = _routeService.GetAll();
            var cities = _cityService.GetAll();
            ViewBag.Cities = new SelectList(cities, "CityName", "CityName");
            return View();
        }

        [HttpPost]
        public IActionResult CreateRoute(Route route)
        {
            _routeService.Create(route);
            return RedirectToAction("AdminList");
        }

        public IActionResult EditRoute(int id)
        {
            var cities = _cityService.GetAll();
            ViewBag.Cities = new SelectList(cities, "CityName", "CityName");
            var entity = _routeService.GetById(id);
            return View(entity);
        }

        [HttpPost]
        public IActionResult EditRoute(Route route)
        {
            _routeService.Update(route);
            return RedirectToAction("ListRoute");

        }

        public IActionResult DeleteRoute(int routeId)
        {
            var entity = _routeService.GetById(routeId);
            _routeService.Delete(entity);
            return RedirectToAction("ListRoute");
        }
    }
    
}
