using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CertificationProject.Data;
using CertificationProject.Models;

namespace CertificationProject.Controllers
{
    public class UserController : Controller
    {

        private readonly ApplicationDbContext _db;


        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(User user)
        {
            if (ModelState.IsValid)
            {
                user.Role = "user";
                _db.user.Add(user);
                _db.SaveChanges();
                return RedirectToAction("Login");
            }
            else return View(user);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(User user)
        {
            var obj = _db.user.Find(user.Id);
            if (obj == null)
            {
                return View();
            }
            else
            {
                if (obj.Password == user.Password)
                {
                    return RedirectToAction("Index");
                }
                else return View();
            }
            
        }
    }
}
