using CertificationProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertificationProject.Controllers
{
    public class UserController : Controller
    {
        private readonly TestContext _db;
        
        
        public UserController(TestContext db)
        {
           _db = db;
        }

        public IActionResult Login()
        {
            IEnumerable<User> objList = _db.Users;
            return View(objList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(int id, string password)
        {
            var obj = _db.Users.Find(id);

            if (obj == null)
            {
                return NotFound();
            }
            if (obj.Password == password)
            {
                return View(obj);
            }
            else return RedirectToAction("Registration");
            
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
                _db.Users.Add(user);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else return View(user);


        }
    }
}
