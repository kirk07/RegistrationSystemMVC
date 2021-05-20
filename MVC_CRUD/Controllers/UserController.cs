using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MVC_CRUD.DataContext;
using MVC_CRUD.Models;




namespace MVC_CRUD.Controllers
{
    public class UserController : Controller
    {
        private readonly CrudDBContext _cruddbcontext;

        public UserController(CrudDBContext crudDBContext)
        {
            _cruddbcontext = crudDBContext;
        }
        

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User u)
        {

            if (ModelState.IsValid)
            {
                //no errors in the form
                //insert the record to DB via EF Core
                _cruddbcontext.Users.Add(u);
                _cruddbcontext.SaveChanges();
                return RedirectToAction("Allusers");
            }


            return View("Index");
        }



        public IActionResult Allusers()
        {
            return View(_cruddbcontext.Users.ToList());
        }


        [HttpGet]
        public IActionResult Update(int id)
        {

            //perform null check
            if (id==0) 
            {
                return NotFound();
            }
            var user = _cruddbcontext.Users.
            FirstOrDefault(s => s.UserId == id);
            return View(user);
     
        }

        [HttpPost]
        public IActionResult Update(User u) 
        {

            //perform null check
            if (u == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //update DB
                _cruddbcontext.Update(u);
                _cruddbcontext.SaveChanges();
                return RedirectToAction("Allusers");
            }

            return View("Update");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {

            var person = _cruddbcontext.Users
          .FirstOrDefault(s => s.UserId == id);

            //perform null check
            if (person != null)
            {
                //remove and update DB
                _cruddbcontext.Users.Remove(person);
                _cruddbcontext.SaveChanges();
                return RedirectToAction("Allusers");
            }

            return View();

        }
    }
}
