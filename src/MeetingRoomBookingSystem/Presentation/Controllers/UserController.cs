using AutoMapper;
using DataAccess.Identity;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.Models;
using Service.ServicesContract;
using System.Data;

namespace Presentation.Controllers
{
    public class UserController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IDepartmentManagementService _departmentManagementService;
        private readonly ILogger<UserController> _logger;

        public UserController(RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper,
            IDepartmentManagementService departmentManagementService,
            ILogger<UserController> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _departmentManagementService = departmentManagementService;
            _logger = logger;
        }

        //This method return all users with pagination and with column sorting...
        [Route("User/AllUserList")]
        public async Task<IActionResult> AllUserList()
        {
            var model = new RegistrationModel();
            model.SetDepartmentsValues(await _departmentManagementService.GetDepartmentsAsync());
            return View(model);
        }

        //This method return all users with pagination and with column sorting...
        [HttpPost]
        [Route("User/GetAllUsers")]
        public async Task<IActionResult> GetAllUsers(int draw, int start, int length, string search, List<Order> order)
        {
            var query = _userManager.Users.AsQueryable();

            // Filter by search term if it is provided
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(u => u.UserName.Contains(search) || u.Email.Contains(search));
            }

            // Total count of records after filtering
            var filteredCount = query.Count();

            // Get paginated user data
            var usersList = query
                .Skip(start)
                .Take(length)
                .ToList();

            // Create a list to store user details along with roles
            var usersWithRoles = new List<(ApplicationUser User, string RoleNames)>();

            // Fetch roles for each user
            foreach (var user in usersList)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var roleNames = string.Join(", ", roles);
                usersWithRoles.Add((user, roleNames));
            }

            // Handle sorting
            if (order != null && order.Count > 0)
            {
                var columnIndex = order[0].Column; // Get the column index for sorting
                var sortDirection = order[0].Direction; // Get the sort direction (asc or desc)

                // Sort based on the selected column
                switch (columnIndex)
                {
                    case 0: // Username
                        usersWithRoles = sortDirection == "asc"
                            ? usersWithRoles.OrderBy(u => u.User.UserName).ToList()
                            : usersWithRoles.OrderByDescending(u => u.User.UserName).ToList();
                        break;

                    case 1: // Pin
                        usersWithRoles = sortDirection == "asc"
                            ? usersWithRoles.OrderBy(u => u.User.Pin).ToList()
                            : usersWithRoles.OrderByDescending(u => u.User.Pin).ToList();
                        break;

                    case 2: // Email
                        usersWithRoles = sortDirection == "asc"
                            ? usersWithRoles.OrderBy(u => u.User.Email).ToList()
                            : usersWithRoles.OrderByDescending(u => u.User.Email).ToList();
                        break;

                    case 3: // phone number
                        usersWithRoles = sortDirection == "asc"
                            ? usersWithRoles.OrderBy(u => u.User.PhoneNumber).ToList()
                            : usersWithRoles.OrderByDescending(u => u.User.PhoneNumber).ToList();
                        break;

                    case 4: // department
                        usersWithRoles = sortDirection == "asc"
                            ? usersWithRoles.OrderBy(u => u.User.DepartmentId).ToList()
                            : usersWithRoles.OrderByDescending(u => u.User.DepartmentId).ToList();
                        break;

                    case 5: // designation
                        usersWithRoles = sortDirection == "asc"
                            ? usersWithRoles.OrderBy(u => u.User.Designation).ToList()
                            : usersWithRoles.OrderByDescending(u => u.User.Designation).ToList();
                        break;

                    case 6: // RoleNames
                        usersWithRoles = sortDirection == "asc"
                            ? usersWithRoles.OrderBy(u => u.RoleNames).ToList()
                            : usersWithRoles.OrderByDescending(u => u.RoleNames).ToList();
                        break;

                    default:
                        break;
                }
            }

            var departments = await _departmentManagementService.GetDepartmentsAsync();
            var departmentDictionary = departments.ToDictionary(d => d.Id, d => d.Name);

            // Prepare the final data to return
            var data = usersWithRoles.Select(u => new
            {
                u.User.UserName,
                u.User.Pin,
                u.User.Email,
                u.User.PhoneNumber,
                u.User.Id,
                Department = departmentDictionary.ContainsKey(u.User.DepartmentId)
                 ? departmentDictionary[u.User.DepartmentId]
                 : "Unknown",
                u.User.Designation,
                RoleNames = u.RoleNames,
                u.User.Status
            }).ToList();

            return Json(new
            {
                draw = draw,
                recordsTotal = _userManager.Users.Count(),  // Total records without filtering
                recordsFiltered = filteredCount,             // Total records after filtering
                data = data
            });
        }

        //This is delete user method...
        [HttpPost]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                if (user != null)
                {
                    // Remove user roles
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles.Any())
                    {
                        await _userManager.RemoveFromRolesAsync(user, roles);
                    }

                    var claims = await _userManager.GetClaimsAsync(user);
                    if (claims.Any())
                    {
                        await _userManager.RemoveClaimsAsync(user, claims);
                    }

                    var result = await _userManager.DeleteAsync(user);

                    if (result.Succeeded)
                    {
                        // Check if the deleted user is the currently logged-in user
                        var currentUserId = _userManager.GetUserId(User);
                        if (currentUserId == user.Id.ToString())
                        {
                            await _signInManager.SignOutAsync();
                        }

                        return Json(new
                        {
                            success = true,
                            message = "The User has been deleted successfully."
                        });
                    }
                }

                return Json(new
                {
                    success = true,
                    message = "The User has deleted successfuly"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The User deleted failed");
                return Json(new
                {
                    success = true,
                    message = "The User deleted failed"
                });
            }
        }


        //This mehtod get a user by id for update...
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return Json(new { success = false });
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles.Select(r => r.Name).ToList();

            return Json(new
            {
                success = true,
                data = new
                {
                    id = user.Id,
                    Name = user.UserName,
                    email = user.Email,
                    phoneNumber = user.PhoneNumber,
                    pin = user.Pin,
                    designation = user.Designation,
                    userRoles = userRoles, 
                    availableRoles = allRoles 
                }
            });
        }


        //This is user update mehtod also user roles update code...
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUser(UserUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                // Get the user
                var user = await _userManager.FindByIdAsync(model.Id.ToString());
                if (user == null)
                {
                    return Json(new { success = false, message = "User not found." });
                }

                // Update user properties
                user = _mapper.Map(model, user);

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return Json(new { success = false, message = "Failed to update user." });
                }

                // Get the current roles of the user
                var currentRoles = await _userManager.GetRolesAsync(user);


                return Json(new { success = true, message = "User updated successfully." });
            }
            return Json(new { success = false, message = "Invalid data." });
        }
    }
}
