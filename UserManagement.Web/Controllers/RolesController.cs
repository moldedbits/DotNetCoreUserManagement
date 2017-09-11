﻿using UserAppService.Context;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using UserAppService.Service;
using UserAppService.ViewModel;
using UserAppService.ViewModels;

namespace UserManagement.Web.Controllers
{
    public class RolesController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        private readonly IAuthService _service;
        public RolesController(IAuthService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            var rolesList = new List<RoleViewModel>();
            foreach(var role in _db.Roles)
            {
                var roleModel = new RoleViewModel(role);
                rolesList.Add(roleModel);
            }
            return View(rolesList);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Create(string message = "")
        {
            ViewBag.Message = message;
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include =
            "RoleName,Description")]RoleViewModel model)
        {
            string message = "That role name has already been used";
            if (ModelState.IsValid)
            {
                //bool result = _service.AddApplicationRole(model);
                //if (!result)
                //{
                //    return View(message);
                //}
                //else
                //{
                //    return RedirectToAction("Index", "Roles");
                //}
            }
            return View();
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            // It's actually the Role.Name tucked into the id param:
            var role = _db.Roles.First(r => r.Name == id);
            var roleModel = new EditRoleViewModel(role);
            return View(roleModel);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include =
            "RoleName,OriginalRoleName,Description")] EditRoleViewModel model)
        {
            if(ModelState.IsValid)
            {
                var role = _db.Roles.First(r => r.Name == model.OriginalRoleName);
                role.Name = model.RoleName;
                _db.Entry(role).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = _db.Roles.First(r => r.Name == id);
            var model = new RoleViewModel(role);
            if(role == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            //var role = _db.Roles.First(r => r.Name == id);
            //ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<User,UserRole >(_db));
            //_db.DeleteRole(_db, userManager, role.Id);
            return RedirectToAction("Index");
        }
    }
}