using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataBase.Entities;
using SPCF.Models;
using SPCF.ViewModels;
using Microsoft.AspNet.Identity;
using static SPCF.Controllers.ManageController;
using System.Web.Security;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SPCF.Controllers
{
    public class UserProfilesController : Controller
    {
        private SPContext db = new SPContext();

        // GET: UserProfiles
        public ActionResult Index()
        {

            return View(db.UserProfiles.ToList());
        }

        // GET: UserProfiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfiles userProfiles = db.UserProfiles.Find(id);
            if (userProfiles == null)
            {
                return HttpNotFound();
            }
            return View(userProfiles);
        }


        // GET: UserProfiles/Create
        [Authorize]
        public ActionResult Create()
        {
            var model = new ViewModels.User.Edit();

            return View(model);
        }

        // POST: UserProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModels.User.Edit model)
        {
            if (ModelState.IsValid)
            {
                    //var test = model.CreateUserProfile();
                    //db.UserProfiles.Add(test);
                    var User = new UserProfiles();
                    var ID = System.Web.HttpContext.Current.User.Identity.GetUserId();
                    var AspUser = db.Users.Find(ID);

                    User.Created = DateTime.UtcNow.Ticks;
                    //User.UserID = ID; //Guid.NewGuid().ToString();
                    User.FirstName = model.FirstName;
                    User.LastName = model.LastName;
                    User.Gender = model.Gender;
                    User.BirthDate = model.DateOfBirth;
                    User.Street = model.Street;
                    User.City = model.City;
                    User.Zip = model.Zip;
                    User.Image = model.Upload == null ? null : ImageArray.ToByteArray(model.Upload);


                    AspUser.UserProfiles = User;
                    db.UserProfiles.Add(User);

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        var temp = e;
                    }
                   
                    return RedirectToAction("Index");
            }

            return View(model);
        }   


        // GET: UserProfiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfiles userProfiles = db.UserProfiles.Find(id);
            if (userProfiles == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.CurrentController = "UserProfiles";
            return View(userProfiles);
        }

        // POST: UserProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Created,Modified,FirstName,LastName,Gender,Email,Password,RepeatPasword,BirthDate,Street,City,Zip,MobilePhone")] UserProfiles userProfiles, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Roles = new SelectList(db.Roles.Where(a => !a.Name.Contains("Admin")).ToList(), "Name", "Name");

                var ticks = DateTime.Now.Ticks;
                userProfiles.Modified = ticks;
                var userProfileFromDb = db.UserProfiles.Find(userProfiles.UserID);
                userProfileFromDb.Modified = ticks;
                //userProfileFromDb.Image = ToByteArray(image);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userProfiles);
        }

        // GET: UserProfile from Userprofile and AspNetUser tables/Edit/
        public ActionResult EditProfile()
        {
            var ID = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var aspNetUser = db.Users.Find(ID);

            aspNetUser.UserProfiles = db.UserProfiles.Find(aspNetUser.Id);
            if (aspNetUser.UserProfiles == null)
            {

                aspNetUser.UserProfiles = new UserProfiles();
                aspNetUser.UserProfiles.UserID = ID;
                aspNetUser.UserProfiles.FirstName = "..";
                aspNetUser.UserProfiles.LastName = "..";
                aspNetUser.UserProfiles.Created = DateTime.UtcNow.Ticks;

                db.SaveChanges();
            }

            var model = new ViewModels.UserAspNet.Edit(aspNetUser);
            aspNetUser.UserProfiles.Image = model.UserProfile.Image;

            return View(model);
        }

        // POST: UserProfile from Userprofile and AspNetUser tables/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(ViewModels.UserAspNet.Edit model)
        {
            if (ModelState.IsValid)
            {
                var User = db.Users.Find(model.ID);
                User.UserName = model.UserName ?? User.UserName;
                User.PhoneNumber = model.PhoneNo;

                var profile = db.UserProfiles.Find(model.ID);

                profile.FirstName = model.UserProfile.FirstName;
                profile.LastName = model.UserProfile.LastName;
                profile.Gender = model.UserProfile.Gender;
                profile.BirthDate = model.UserProfile.DateOfBirth;
                profile.Street = model.UserProfile.Street;
                profile.City = model.UserProfile.City;
                profile.Zip = model.UserProfile.Zip;
                profile.Modified = DateTime.UtcNow.Ticks;
                profile.Image = model.UserProfile.Image ?? ImageArray.ToByteArray(model.UserProfile.Upload);
  
                    db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Admin Control of UserProfile from Userprofile and AspNetUser tables/Edit/
        public ActionResult EditProfilesAdmin(string ID)
        {
            var aspNetUser = db.Users.Find(ID);

            aspNetUser.UserProfiles = db.UserProfiles.Find(aspNetUser.Id);
            
            if (aspNetUser.UserProfiles == null)
            {

                aspNetUser.UserProfiles = new UserProfiles();
                aspNetUser.UserProfiles.UserID = ID;
                aspNetUser.UserProfiles.FirstName = "..";
                aspNetUser.UserProfiles.LastName = "..";
                aspNetUser.UserProfiles.Created = DateTime.UtcNow.Ticks;

                db.SaveChanges();
            }

            var roles = db.Roles.ToList();
            var model = new ViewModels.UserAspNet.EditAdmin(roles, aspNetUser);
            aspNetUser.UserProfiles.Image = model.UserProfile.Image;

            return View(model);
        }

        // POST: UserProfile from Userprofile and AspNetUser tables/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfilesAdmin(ViewModels.UserAspNet.EditAdmin model)
        {
            if (ModelState.IsValid)
            {
                var User = db.Users.Find(model.ID);
                User.UserName = model.UserName ?? User.UserName;
                User.PhoneNumber = model.PhoneNo;

                if (User.Roles.FirstOrDefault(a => a.RoleId == model.SelectedRoleId) == null)
                {
                    UserManager<ApplicationUser> _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new SPContext()));

                    foreach (var roleId in User.Roles.Select(a => a.RoleId))
                    {
                        var roleToRemove = db.Roles.Find(roleId);

                        if (roleToRemove != null)
                        {
                            _userManager.RemoveFromRole(User.Id, roleToRemove.Name);
                        }
                    }

                    var roleToAdd = db.Roles.Find(model.SelectedRoleId);

                    if (roleToAdd != null)
                    {
                        _userManager.AddToRole(User.Id, roleToAdd.Name);
                    }
              
                    
                }


                var profile = db.UserProfiles.Find(model.ID);

                profile.FirstName = model.UserProfile.FirstName;
                profile.LastName = model.UserProfile.LastName;
                profile.Gender = model.UserProfile.Gender;
                profile.BirthDate = model.UserProfile.DateOfBirth;
                profile.Street = model.UserProfile.Street;
                profile.City = model.UserProfile.City;
                profile.Zip = model.UserProfile.Zip;
                profile.Modified = DateTime.UtcNow.Ticks;
                profile.Image = model.UserProfile.Image ?? ImageArray.ToByteArray(model.UserProfile.Upload);


                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(model);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _UploadImage(int? userId, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                var userProfileFromDb = db.UserProfiles.Find(userId);
                userProfileFromDb.Image = ImageArray.ToByteArray(image);

                db.SaveChanges();
                return RedirectToAction("EditProfile", new { Id = userId });
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadImage(ViewModels.UserAspNet.Edit model)
        {
            if (ModelState.IsValid)
            {

                var ID = System.Web.HttpContext.Current.User.Identity.GetUserId();

                var userProfileFromDb = db.UserProfiles.Find(ID);
                var appUser = db.Users.Find(ID);


                userProfileFromDb.Image = ImageArray.ToByteArray(model.UserProfile.Upload);

                db.SaveChanges();

                var newModel = new ViewModels.UserAspNet.Edit(appUser);
                newModel.Email = model.Email;
                newModel.PhoneNo = model.PhoneNo;
                newModel.UserName = model.UserName;

                return View("EditProfile", newModel);
            }
            return View();
        }

        // GET: UserProfiles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfiles userProfiles = db.UserProfiles.Find(id);
            if (userProfiles == null)
            {
                return HttpNotFound();
            }

            return View(userProfiles);
        }

        // POST: UserProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserProfiles Profile = db.UserProfiles.Find(id);
            ApplicationUser AspNetProfile = db.Users.Find(id);

            db.UserProfiles.Remove(Profile);
            db.Users.Remove(AspNetProfile);

            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult DeleteImage(int userId)
        {
            var userProfile = db.UserProfiles.Find(userId);
            userProfile.Image = null;
            db.SaveChanges();
            return RedirectToAction("Edit", new { id = userProfile.UserID });
        }

        
        [HttpGet]
        public ActionResult DeleteImageofEditProfile()
        {
            var ID = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var userProfile = db.UserProfiles.Find(ID);
            userProfile.Image = null;
            db.SaveChanges();
            return RedirectToAction("EditProfile");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
            }
    }
}
