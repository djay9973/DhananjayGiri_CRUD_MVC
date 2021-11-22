using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminCMS.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminCMS.Controllers
{
    [HandleError(View = "Error")]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [HandleError(ExceptionType = typeof(InvalidOperationException))]
        public ActionResult Login(Account login)
        {
            try
            {
                using (var context = new UsersEntities1())
                {
                    bool isValid = context.Accounts.Any(x => x.Email == login.Email && x.Password == login.Password);
                    if (isValid)
                    {
                        return RedirectToAction("Read", "Home");
                    }
                    else
                    {
                        ViewBag.Message = "Invalid login credentials !";
                        return View();
                    }
                }
            }
            catch (Exception er)
            {
                return View("Error");
            }
            finally
            {
            }
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Account registerDetails)
        {
            if (ModelState.IsValid)
            {
                using (var databaseContext = new UsersEntities1())
                {
                    Account reglog = new Account();
                    reglog.FirstName = registerDetails.FirstName;
                    reglog.LastName = registerDetails.LastName;
                    reglog.Email = registerDetails.Email;
                    reglog.Password = registerDetails.Password;
                    reglog.ContactNo = registerDetails.ContactNo;
                    databaseContext.Accounts.Add(reglog);
                    databaseContext.SaveChanges();
                }

                ViewBag.Message = "User Details Saved";
                return View("Register");
            }
            else
            {
                return View("Register", registerDetails);
            }
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}