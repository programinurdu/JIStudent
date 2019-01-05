using JIStudent.Models.Account;
using JIStudent.Models.General;
using JIStudent.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace JIStudent.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet, Authorize(Roles = "Administrator, Manager")]
        public ActionResult UserList()
        {
            List<UserModel> users = AccountViewModels.GetAllUsers();
          
            return View(users);
        }

        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                bool isAuthenticated = WebSecurity.Login(loginModel.UserName, loginModel.Password, loginModel.RememberMe);

                if (isAuthenticated)
                {
                    string returnUrl = Request.QueryString["ReturnUrl"];

                    if (returnUrl == null)
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        return Redirect(Url.Content(returnUrl));
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password are invalid.");
                }
            }

            return View();
        }

        public ActionResult SignOut()
        {
            WebSecurity.Logout();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator, Manager")]
        public ActionResult Register()
        {
            GetRolesForCurrentUser();

            return View();
        }

        private void GetRolesForCurrentUser()
        {
            if (Roles.IsUserInRole(WebSecurity.CurrentUserName, "Administrator"))
                ViewBag.RoleId = (int)Role.Administrator;
            else
                ViewBag.RoleId = (int)Role.NoRole;
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Manager")]
        public ActionResult Register(RegisterModel registerModel)
        {
            GetRolesForCurrentUser();

            if (ModelState.IsValid)
            {
                bool isUserExists = WebSecurity.UserExists(registerModel.UserName);

                if (isUserExists)
                {
                    ModelState.AddModelError("UserName", "User Name already exists");
                }
                else
                {
                    WebSecurity.CreateUserAndAccount(registerModel.UserName, registerModel.Password, new { FullName = registerModel.FullName, Email = registerModel.Email });
                    Roles.AddUserToRole(registerModel.UserName, registerModel.Role);

                    return RedirectToAction("Index", "Dashboard");
                }
            }

            return View();
        }

        [HttpGet, Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordModel changePasswordModel)
        {
            if (ModelState.IsValid)
            {
                bool isPasswordChanged = WebSecurity.ChangePassword(WebSecurity.CurrentUserName, changePasswordModel.OldPassword, changePasswordModel.NewPassword);

                if (isPasswordChanged)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ModelState.AddModelError("OldPassword", "Old Password is not correct.");
                }
            }
            return View();
        }

        [HttpGet, Authorize]
        public ActionResult UserProfile()
        {
            UserProfileModel userProfileModel = AccountViewModels.GetUserProfileData(WebSecurity.CurrentUserId);
            return View(userProfileModel);
        }

        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public ActionResult UserProfile(UserProfileModel userProfileModel)
        {
            if (ModelState.IsValid)
            {
                AccountViewModels.UpdateUserProfile(userProfileModel);
                ViewBag.Message = "Profile is saved successfully.";
            }

            return View();
        }
    }
}