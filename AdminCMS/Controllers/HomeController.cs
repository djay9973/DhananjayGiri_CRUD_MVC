using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using AdminCMS.Models;
using System.Data.Entity.Validation;
using System.Data.Entity;

namespace AdminCMS.Controllers
{
    [HandleError(View = "Error")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult create(Account registerDetails)
        {
            try
             {
                 using (var context = new UsersEntities1())
                 {
                     context.Accounts.Add(registerDetails);
                     context.SaveChanges();
                 }
                 string message = "Created the record successfully";
                 ViewBag.Message = message;
                 return View();
             }
             catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
             {
                 Exception raise = dbEx;
                 foreach (var validationErrors in dbEx.EntityValidationErrors)
                 {
                     foreach (var validationError in validationErrors.ValidationErrors)
                     {
                         string message = string.Format("{0}:{1}",
                             validationErrors.Entry.Entity.ToString(),
                             validationError.ErrorMessage);
                         raise = new InvalidOperationException(message, raise);
                     }
                 }
                 throw raise;
             }
          
        }
        [HttpGet]
        public ActionResult Read()
        {
            using (var context = new UsersEntities1())
            {
                var data = context.Accounts.ToList();
                return View(data);
            }
        }
        public ActionResult Update(int id)
        {
            using (var context = new UsersEntities1())
            {
                var data = context.Accounts.Where(x => x.Id == id).SingleOrDefault();
                return View(data);
            }
        }
        [HttpPost, ValidateInput(false), ActionName("Update")]
        [ValidateAntiForgeryToken]
        [HandleError]
        public ActionResult Update(int id, Account model)
        {
            try
            {
                /* using (var context = new UsersEntities1())
                 {
                     var data = context.Accounts.FirstOrDefault(x => x.Id == id);
                     if (data != null)
                     {
                         data.FirstName = model.FirstName;
                         data.LastName = model.LastName;
                         data.Email = model.Email;
                         data.Password = model.Password;
                         data.ContactNo = model.ContactNo;
                         context.SaveChanges();
                         return RedirectToAction("Read");
                     }
                     else
                         return View();
                 }*/
                if (ModelState.IsValid)
                {
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Read");
                }
                return View(model);
            }
            
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
        public ActionResult Delete(int? id)
        {
            try
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Account user_details = db.Accounts.Find(id);
                if (user_details == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    ViewBag.Message = String.Format("Please wait..");
                    return View(user_details);
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Account user_details = db.Accounts.Find(id);
                db.Accounts.Remove(user_details);
                db.SaveChanges();
            }
            catch (Exception er)
            {
                return View("Error");
            }
            finally
            {

            }
            return RedirectToAction("Read");
        }
        private UsersEntities1 db = new UsersEntities1();
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Account user_details = db.Accounts.Find(id);
                if (user_details == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    ViewBag.Message = String.Format("Please wait..");
                    return View(user_details);
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
    }
}