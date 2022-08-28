using RightProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using RightProject.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace RightProject.Controllers
{
    
    public class Account : Controller {
        private readonly finalContext db;
        public static string Message { get; set; }
        public static string SuccessMsg { get; set; }
        //  public LoginModel Input { get => input; set => input = value; }

        public Account(finalContext context)
        {
            db = context;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Shopmanagerid,Salary,Name,Username,Password,PasswordConfirm,Email,lockout,lockTime,ErrorLogCount")] ShopManager Appuser)
        {
            Message = string.Empty;
            SuccessMsg = string.Empty;
            if (ModelState.IsValid)
            {

                string input = Appuser.Password;
                if (!string.IsNullOrEmpty(input))
                {
                    Appuser.Password = AppHash.HashPassword(input);
                    Appuser.PasswordConfirm = AppHash.HashPassword(input);
                    // DataTable dt = new DataTable();
                    //Users users = new Users();
                    string userName = Appuser.Username;
                    string email = Appuser.Email;
                    int userId = Appuser.Shopmanagerid;
                    // dt = users.CheckUserNameExist(userName);
                    if (!IsusersExists(userName))
                    {
                        if (!IsEmailsExists(email))
                        {
                            int userCount = db.ShopManagers.Count();
                            db.Add(Appuser);
                            await db.SaveChangesAsync();
                            int roleId = db.AppRoles.Where(e => e.RoleName == "ShopManager").Select(e => e.id).SingleOrDefault(); ;


                            UserRole userRole = new UserRole();
                            userRole.Roleid = roleId;
                            userRole.Userid = Appuser.Shopmanagerid;
                            userRole.Type = "ShopManager";
                            await db.AddAsync(userRole);
                            await db.SaveChangesAsync();



                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            Message = "The Email : " + email + " is used previosly";
                            return View();
                        }
                    }
                    else
                    {
                        Message = " The User name : " + userName + " is used previosly";
                        return View();
                    }

                }

            }
            return RedirectToAction(nameof(Register));
        }
        //********************************************************cookies and login/logout operations*************************************************************************************************************************************
        //
        public class LoginModel
        {
            [Required]
            public string Username { get; set; }
            [Required]
            public string Password { get; set; }
            public bool rememberMe { get; set; }
        }
        //[BindProperty]
        private LoginModel input = new LoginModel();

        public IActionResult login()
        {
            return View();
        }
        public IActionResult login2()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> login(string username, string password, bool rem)
        {
            Message = string.Empty;
            if (username == null || password == null)
            {
                return View();
            }
           
            if (IsLoged(username, password))
            {
                int idshopm = await db.ShopManagers.Where(a => a.Username == username).Select(a => a.Shopmanagerid).SingleOrDefaultAsync();

                if (idshopm != 0)
                {
                    var shopManager = db.ShopManagers.Find(idshopm);
                    if (shopManager != null)
                    {



                        int id = await db.ShopManagers.Where(a => a.Username == username).Select(a => a.Shopmanagerid).SingleOrDefaultAsync();
                        int roleIdByUserId = await db.UserRoles.Where(a => a.Userid == id).Select(a => a.Roleid).SingleOrDefaultAsync();
                        string roleNameById = await db.AppRoles.Where(a => a.id == roleIdByUserId).Select(a => a.RoleName).SingleOrDefaultAsync();
                        AddCookies(username, roleNameById, id);
                        if (roleNameById == "ShopManager") { return RedirectToAction("shop", "myshop"); }
                        else
                        {
                            Message = "you can't log in ";
                            //return View();
                        }


                    }
                }
            }

            return View();
        }
                       
                     

          
        
        [HttpPost]
        public async Task<IActionResult> login2(string username, string password, bool rem)
        {
            if (username == null || password == null)
            {
                return View();
            }
            if (IsLoged2(username, password))
            {
                int idshopm = await db.AdminManagers.Where(a => a.Username == username).Select(a => a.Managerid).SingleOrDefaultAsync();
               // int idm = await db.AdminManagers.Where(a => a.Username == username).Select(a => a.Managerid).SingleOrDefaultAsync();
                if (idshopm != 0)
                {
                    var shopManager = db.AdminManagers.Find(idshopm);
                    if (shopManager != null)
                    {
                        if (shopManager.lockout == false)
                        {
                            shopManager.ErrorLogCount = 0;
                            db.AdminManagers.Attach(shopManager);
                            db.Entry(shopManager).Property(x => x.ErrorLogCount).IsModified = true;
                            await db.SaveChangesAsync();

                            int id = await db.AdminManagers.Where(a => a.Username == username).Select(a => a.Managerid).SingleOrDefaultAsync();
                           // int roleIdByUserId = await db.UserRoles.Where(a => a.Userid == idshopm).Select(a => a.Roleid).SingleOrDefaultAsync();
                           // string roleNameById = await db.AppRoles.Where(a => a.id == roleIdByUserId).Select(a => a.RoleName).SingleOrDefaultAsync();
                            AddCookies(username, "Admin", id);
                         
                                return RedirectToAction("Index", "Shops");
                            

                        }
                        else
                        {
                            if (Islukoutfinished2(shopManager.lockTime, idshopm, username))
                            {
                                int id = await db.AdminManagers.Where(a => a.Username == username).Select(a => a.Managerid).SingleOrDefaultAsync();
                               // int roleIdByUserId = await db.UserRoles.Where(a => a.Userid == id).Select(a => a.Roleid).SingleOrDefaultAsync();
                               // string roleNameById = await db.AppRoles.Where(a => a.id == roleIdByUserId).Select(a => a.RoleName).SingleOrDefaultAsync();
                                AddCookies(username, "Admin", id);
                                return RedirectToAction("forget2", "Account");
                            }
                            else
                            {
                                ViewBag.msg = "please try after time of blocking";
                                return View();
                            }
                        }
                    }
                }
            }
            else
            {
                if (LogError2(username))
                {
                    ViewBag.msg = username + " : your account do not work..you are blocked now .. ";
                }
            }

            return View();
        }
        private bool IsLoged(string username, string password)
        {
            //  DataTable dt = new DataTable();
            //  Users cs = new Users();
           string hash = AppHash.HashPassword(password);
            //dt = cs.checkLogin(username, hash);
            if (db.ShopManagers.Any(a => a.Username == username))
            {
                if (db.ShopManagers.Any(a => a.Password == hash))
                {
                    return true;
                }
            }

            return false;
        }
        private bool IsLoged2(string username, string password)
        {
            //  DataTable dt = new DataTable();
            //  Users cs = new Users();
            string hash = AppHash.HashPassword(password);
            //dt = cs.checkLogin(username, hash);
            if (db.AdminManagers.Any(a => a.Username == username))
            {
                if (db.AdminManagers.Any(a => a.Password == hash))
                {
                    return true;
                }
            }

            return false;
        }
        private bool Islukoutfinished(DateTime? lockDate, int id, string username)
        {
            if (id != 0)
            {
                var shopManager = db.ShopManagers.Find(id);
                if (shopManager != null)
                {
                    if (lockDate != null)
                    {
                        if (DateTime.Now >= lockDate)
                        {
                            shopManager.ErrorLogCount = 0;
                            shopManager.lockout = false;
                            shopManager.lockTime = null;
                            db.ShopManagers.Attach(shopManager);
                            db.Entry(shopManager).Property(x => x.lockout).IsModified = true;
                            db.Entry(shopManager).Property(x => x.lockTime).IsModified = true;
                            db.Entry(shopManager).Property(x => x.ErrorLogCount).IsModified = true;
                            db.SaveChanges();
                            return true;


                        }
                    }
                }
            }
            return false;
        }
        private bool Islukoutfinished2(DateTime? lockDate, int id, string username)
        {
            if (id != 0)
            {
                var Manager = db.AdminManagers.Find(id);
                if (Manager != null)
                {
                    if (lockDate != null)
                    {
                        if (DateTime.Now >= lockDate)
                        {
                            Manager.ErrorLogCount = 0;
                            Manager.lockout = false;
                            Manager.lockTime = null;
                            db.AdminManagers.Attach(Manager);
                            db.Entry(Manager).Property(x => x.lockout).IsModified = true;
                            db.Entry(Manager).Property(x => x.lockTime).IsModified = true;
                            db.Entry(Manager).Property(x => x.ErrorLogCount).IsModified = true;
                            db.SaveChanges();
                            return true;


                        }
                    }
                }
            }
            return false;
        }
        private async void AddCookies(string username, string roleName, int id)
        {
            //  int id = db.ShopManagers.Where(a => a.Username == username).Select(a => a.Shopmanagerid).SingleOrDefault();
            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.Name,username),
                new Claim(ClaimTypes.NameIdentifier,id.ToString()),
                new Claim(ClaimTypes.Role,roleName)
            };
            var claimIdentity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
            var authproperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(25)

            };
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimIdentity),
                authproperties
                );
        }

        public async Task<IActionResult> logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");

        }
        public IActionResult AccessDenied()
        {

            return View();

        }
        public bool LogError(string username)
        {
            int id = db.ShopManagers.Where(a => a.Username == username).Select(a => a.Shopmanagerid).SingleOrDefault();
            if (id != 0)
            {
                var appUser = db.ShopManagers.Find(id);
                if (appUser != null)
                {
                    appUser.ErrorLogCount += 1;
                    int count = appUser.ErrorLogCount;
                    if (appUser.ErrorLogCount < 3)
                    {
                        db.ShopManagers.Attach(appUser);
                        db.SaveChangesAsync();
                        ViewBag.msg = "you have " + count + " attempts from 3";
                        return false;
                    }
                    else
                    {
                        db.ShopManagers.Attach(appUser);
                        appUser.ErrorLogCount += 1;
                        appUser.lockTime = DateTime.Now.AddMinutes(2);
                        appUser.lockout = true;
                        db.Entry(appUser).Property(x => x.lockout).IsModified = true;
                        db.Entry(appUser).Property(x => x.ErrorLogCount).IsModified = true;
                        db.Entry(appUser).Property(x => x.lockTime).IsModified = true;
                        db.SaveChangesAsync();
                        return true;



                    }
                }
            }
            return false;
        }
        public bool LogError2(string username)
        {
            int id = db.AdminManagers.Where(a => a.Username == username).Select(a => a.Managerid).SingleOrDefault();
            if (id != 0)
            {
                var appUser = db.AdminManagers.Find(id);
                if (appUser != null)
                {
                    appUser.ErrorLogCount += 1;
                    int count = appUser.ErrorLogCount;
                    if (appUser.ErrorLogCount < 3)
                    {
                        db.AdminManagers.Attach(appUser);
                        db.SaveChangesAsync();
                        ViewBag.msg = "you have " + count + " attempts from 3";
                        return false;
                    }
                    else
                    {
                        db.AdminManagers.Attach(appUser);
                        appUser.ErrorLogCount += 1;
                        appUser.lockTime = DateTime.Now.AddMinutes(2);
                        appUser.lockout = true;
                        db.Entry(appUser).Property(x => x.lockout).IsModified = true;
                        db.Entry(appUser).Property(x => x.ErrorLogCount).IsModified = true;
                        db.Entry(appUser).Property(x => x.lockTime).IsModified = true;
                        db.SaveChangesAsync();
                        return true;



                    }
                }
            }
            return false;
        }
        //*******************************************change password operations*******************************************************************************************************************************
        private string GetUserPass(int userId)
        {
            try
            {
                return db.AdminManagers.Where(e => e.Managerid == userId).Select(e => e.Password).FirstOrDefault();
            }
            catch { }
            return string.Empty;
        }
        [Authorize(AuthenticationSchemes = "Cookies")]
        [HttpPost]
       public IActionResult VerifyPassword(string pass)
        {
            if(pass == null)
            {
                return View();
            }
            string password = AppHash.HashPassword(pass);
            string id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //int.Parse(id);
            if(int.Parse(id) != 0)
            {
                if(password == GetUserPass(int.Parse(id)))
                {
                    Message = string.Empty;
                    return RedirectToAction(nameof(change),new {UId=password });
                }
                else
                {
                    Message = "password is not true";
                }
            }
            
            return RedirectToAction(nameof(changepassword));
        }
        [Authorize(AuthenticationSchemes = "Cookies")]
        public IActionResult change()
        {
            string uid = HttpContext.Request.Query["UId"].ToString();
            if (string.IsNullOrEmpty(uid))
            {
                return RedirectToAction("Index", "Home");
            }
            string id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.Parse(id) == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            if (GetUserPass(int.Parse(id)) != uid) {
                return RedirectToAction("Index", "Home");
            }

                return View();
        }
        [Authorize(AuthenticationSchemes = "Cookies")]
        [HttpPost]
        public IActionResult change(string pass,string passconfirm)
        {
            if (pass == null || passconfirm == null)
            {
                return RedirectToAction(nameof(changepassword));
            }
            string password = AppHash.HashPassword(pass);
            string id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.Parse(id) != 0)
            {
                if(pass == passconfirm)
                {
                    var user = db.AdminManagers.FirstOrDefault(a => a.Managerid == int.Parse(id));
                    if(user != null)
                    {
                        user.Password = password;
                        user.PasswordConfirm = password;
                        db.Attach(user);
                        db.Entry(user).Property(x => x.Password).IsModified = true;
                        db.Entry(user).Property(x => x.PasswordConfirm).IsModified = true;
                        db.SaveChanges();
                        return RedirectToAction(nameof(changepassword));
                    }
                    else
                    {
                        return View("Index", "Home");
                    }
                }
                else
                {
                    ViewBag.msg = "password words are not same";
                }
            }
                return View();
        }
       public IActionResult changepassword()
        {
            return View();
        }
       //*************************************forget operations for shop manager***************************************************************
       [Authorize(Roles = "ShopManager", AuthenticationSchemes = "Cookies")]

        public IActionResult forget()
        {
            return View();
        }
       [Authorize(Roles = "ShopManager", AuthenticationSchemes = "Cookies")]
        [HttpPost]
        public IActionResult forget(string email,string pass,string passconfirm)
        {
            if(email == null)
            {
                return View();
            }
            if (IsEmailsExists(email))
            {
                var user1 = db.ShopManagers.Where(e=>e.Email==email).SingleOrDefault();
                if (pass == null || passconfirm == null)
                {
                    return RedirectToAction("Index","Home");
                }
                string password = AppHash.HashPassword(pass);
              //  string id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
              int id= db.ShopManagers.Where(e => e.Email == email).Select(a=>a.Shopmanagerid).SingleOrDefault();
                if (id != 0)
                {
                    if (pass == passconfirm)
                    {
                        var user = db.ShopManagers.Where(a => a.Shopmanagerid == id).SingleOrDefault();
                        if (user != null)
                        {
                            user.Password = password;
                            user.PasswordConfirm = password;
                            db.Attach(user);
                            db.Entry(user).Property(x => x.Password).IsModified = true;
                            db.Entry(user).Property(x => x.PasswordConfirm).IsModified = true;
                            db.SaveChanges();
                            return RedirectToAction("privacy", "Home");
                        }
                        else
                        {
                            return View("Index", "Home");
                        }
                    }
                    else
                    {
                        ViewBag.msg = "password words are not same";
                    }
                }
               

            }
            else
            {
                ViewBag.msg = "email not found";
            }
            return View();
        }
        //**********************************forget operation for admin***********************************************************************
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Cookies")]

        public IActionResult forget2()
        {
            return View();
        }
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Cookies")]
        [HttpPost]
        public IActionResult forget2(string email, string pass, string passconfirm)
        {
            if (email == null)
            {
                return View();
            }
            if (IsEmailsExists2(email))
            {
                var user1 = db.AdminManagers.Where(e => e.Email == email).SingleOrDefault();
                if (pass == null || passconfirm == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                string password = AppHash.HashPassword(pass);
                //  string id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                int id = db.AdminManagers.Where(e => e.Email == email).Select(a => a.Managerid).SingleOrDefault();
                if (id != 0)
                {
                    if (pass == passconfirm)
                    {
                        var user = db.AdminManagers.Where(a => a.Managerid == id).SingleOrDefault();
                        if (user != null)
                        {
                            user.Password = password;
                            user.PasswordConfirm = password;
                            db.Attach(user);
                            db.Entry(user).Property(x => x.Password).IsModified = true;
                            db.Entry(user).Property(x => x.PasswordConfirm).IsModified = true;
                            db.SaveChanges();
                            return RedirectToAction("privacy", "Home");
                        }
                        else
                        {
                            return View("Index", "Home");
                        }
                    }
                    else
                    {
                        ViewBag.msg = "password words are not same";
                    }
                }


            }
            else
            {
                ViewBag.msg = "email not found";
            }
            return View();
        }
        //*************************************check for shop manager*************************************************************************

        public bool IsEmailsExists(string email)
        {
            return db.ShopManagers.Any(a => a.Email == email);
        }
        public bool IsusersExists(string email)
        {
            return db.ShopManagers.Any(a => a.Username == email);
        }
        public bool IpassExists(string pass, string username)
        {
            return db.ShopManagers.Any(a => a.Password == pass);
        }
        //**********************************check for admin*************************************************************************************
        public bool IsusersExists2(string email)
        {
            return db.AdminManagers.Any(a => a.Username == email);
        }
        public bool IpassExists2(string pass, string username)
        {
            return db.AdminManagers.Where(a => a.Username == username).Any(a => a.Password == pass);
        }

        public bool IsEmailsExists2(string email)
        {
            return db.AdminManagers.Any(a => a.Email == email);
        }
        public bool IsEmailmExists(string email)
        {
            return db.AdminManagers.Any(a => a.Email == email);
        }
        public bool IsusermExists(string email)
        {
            return db.AdminManagers.Any(a => a.Username == email);
        }
        //*******************************check customer operations********************************************
        public bool IsEmailExists(string email)
        {
            return db.Customers.Any(a => a.Email == email);
        }
        public bool IsuserExists(string email)
        {
            return db.Customers.Any(a => a.Username == email);
        }
        //**********************************************************add person operation****************************************************************************************************************************************
        public IActionResult RegisterCus()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterCus([Bind("Customerid,Name,Password,PasswordConfirm,Email,Gender,Username,Bdate,Address,lockout,Points,lockTime,ErrorLogCount")] Customer Appuser)
        {
            Message = string.Empty;
            SuccessMsg = string.Empty;
            if (ModelState.IsValid)
            {

                string input = Appuser.Password;
                if (!string.IsNullOrEmpty(input))
                {
                    Appuser.Password = AppHash.HashPassword(input);
                    Appuser.PasswordConfirm = AppHash.HashPassword(input);
                    // DataTable dt = new DataTable();
                    //Users users = new Users();
                    string userName = Appuser.Username;
                    string email = Appuser.Email;
                    int userId = Appuser.Customerid;
                    // dt = users.CheckUserNameExist(userName);
                    if (!IsuserExists(userName))
                    {
                        if (!IsEmailExists(email))
                        {
                            int userCount = db.ShopManagers.Count();
                            db.Add(Appuser);
                            await db.SaveChangesAsync();
                            int roleId = db.AppRoles.Where(e => e.RoleName == "Customer").Select(e => e.id).SingleOrDefault(); ;


                            UserRole userRole = new UserRole();
                            userRole.Roleid = roleId;
                            userRole.Userid = Appuser.Customerid;
                            userRole.Type = "Customer";
                            await db.AddAsync(userRole);
                            await db.SaveChangesAsync();



                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            Message = "The Email : " + email + " is used previosly";
                            return View();
                        }
                    }
                    else
                    {
                        Message = " The User name : " + userName + " is used previosly";
                        return View();
                    }

                }

            }
            return RedirectToAction(nameof(RegisterCus));
        }
     
        public IActionResult RegisterAdd()
        {
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAdd([Bind("Managerid,Name,Username,Password,PasswordConfirm,Email,lockout,lockTime,ErrorLogCount")] AdminManager adminManager)
        {
            Message = string.Empty;
            SuccessMsg = string.Empty;
            if (ModelState.IsValid)
            {

                string input = adminManager.Password;
                if (!string.IsNullOrEmpty(input))
                {
                    adminManager.Password = AppHash.HashPassword(input);
                    adminManager.PasswordConfirm = AppHash.HashPassword(input);
                    // DataTable dt = new DataTable();
                    //Users users = new Users();
                    string userName = adminManager.Username;
                    string email = adminManager.Email;
                    int userId = adminManager.Managerid;
                    // dt = users.CheckUserNameExist(userName);
                    if (!IsusermExists(userName))
                    {
                        if (!IsEmailmExists(email))
                        {
                            int userCount = db.AdminManagers.Count();
                            db.Add(adminManager);
                            await db.SaveChangesAsync();
                            int roleId = db.AppRoles.Where(e => e.RoleName == "Admin").Select(e => e.id).SingleOrDefault(); ;


                            UserRole userRole = new UserRole();
                            userRole.Roleid = roleId;
                            userRole.Userid = adminManager.Managerid;
                            userRole.Type = "Admin";
                            await db.AddAsync(userRole);
                            await db.SaveChangesAsync();



                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            Message = "The Email : " + email + " is used previosly";
                            return View();
                        }
                    }
                    else
                    {
                        Message = " The User name : " + userName + " is used previosly";
                        return View();
                    }

                }

            }
            return RedirectToAction(nameof(RegisterCus));
        }
    }
}
