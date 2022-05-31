using Dapper;
using Class_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Class_Management_System.Controllers
{
    public class ClassController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("roletype") == "Admin")
            {
                IDbConnection connection = new SqlConnection(Dapper.Connection);
                connection.Open();
                var data = connection.Query<Class>("select * from Class").ToList();
                return View(data);
            }
            else
            {
                return RedirectToAction(controllerName: "adminlogin", actionName: "Index");
            }
            
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("roletype") == "Admin")
            {
                return View();
            }
            else
            {
                return RedirectToAction(controllerName: "adminlogin", actionName: "Index");
            }
            
        }

        //
        [HttpPost]
        public ActionResult Create(Class classd)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    db.Open();
                    string sqlQuery = "Insert Into Class (ClassName) Values( @ClassName)";

                    var rowsAffected = db.Execute(sqlQuery, new { ClassName = classd.ClassName});
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("roletype") == "Admin")
            {
                Class classd = new Class();
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    classd = db.Query<Class>("Select * From Class WHERE ClassID =" + id, new { id }).SingleOrDefault();
                }
                return View(classd);
            }
            else
            {
                return RedirectToAction(controllerName: "adminlogin", actionName: "Index");
            }
            
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Class classn)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    string sqlQuery = "UPDATE Class set ClassName='" + classn.ClassName +
                        "' WHERE ClassID=" + id;

                    int rowsAffected = db.Execute(sqlQuery);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("roletype") == "Admin")
            {
                Class sad = new Class();
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    sad = db.Query<Class>("Select * From Class WHERE ClassID =" + id, new { id }).SingleOrDefault();
                }
                return View(sad);
            }
            else
            {
                return RedirectToAction(controllerName: "adminlogin", actionName: "Index");
            }
            
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Details(int id, Class closs)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    string sqlQuery = "Select Class from ClassName='" + closs.ClassName +
                        "' WHERE ClassID=" + id;

                    int rowsAffected = db.Execute(sqlQuery);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("roletype") == "Admin")
            {
                Class clsss = new Class();
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    clsss = db.Query<Class>("Select * From Class WHERE ClassID =" + id, new { id }).SingleOrDefault();
                }
                return View(clsss);
            }
            else
            {
                return RedirectToAction(controllerName: "adminlogin", actionName: "Index");
            }
            
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Class Cldss)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    string sqlQuery = "Delete From Class WHERE ClassID = " + id;

                    int rowsAffected = db.Execute(sqlQuery);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
