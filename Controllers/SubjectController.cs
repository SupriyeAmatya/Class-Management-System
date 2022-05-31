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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace Class_Management_System.Controllers
{
    public class SubjectController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("roletype") == "Admin")
            {
                IDbConnection connection = new SqlConnection(Dapper.Connection);
                connection.Open();
                var data = connection.Query<DisplayName>("Select Subjects.SubjectID, Subjects.SubjectName, Subjects.TotalMarks, Class.ClassID, Class.ClassName FROM Subjects INNER JOIN Class ON Class.ClassID = Subjects.ClassID;").ToList();
                var classdata = connection.Query<Class>("select * from Class").ToList();
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
                var subject = new Class();
                IDbConnection connection = new SqlConnection(Dapper.Connection);
                var subdata = connection.Query<Class>("select * from Class").ToList();
                ViewBag.TotalClass = new SelectList(subdata, "ClassID", "ClassName");
                return View();
            }
            else
            {
                return RedirectToAction(controllerName: "adminlogin", actionName: "Index");
            }
            
        }

        //
        [HttpPost]
        public ActionResult Create(Subject sub, Class name)
        {
          
            try
            {
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    db.Open();
                    string sqlQuery = "Insert Into Subjects (SubjectName, TotalMarks, ClassID) Values( @SubjectName, @TotalMarks, @ClassID)";

                    var rowsAffected = db.Execute(sqlQuery, new { SubjectName = sub.SubjectName, TotalMarks = sub.TotalMarks, ClassID = sub.ClassID });
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
                Subject classd = new Subject();
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    classd = db.Query<Subject>("Select * From Subjects WHERE SubjectID =" + id, new { id }).SingleOrDefault();
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
        public ActionResult Edit(int id, Subject sub)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    string sqlQuery = "UPDATE Subjects set SubjectName='" + sub.SubjectName +
                        "',TotalMarks='" + sub.TotalMarks +
                        "',ClassId='" + sub.ClassID +
                        "' WHERE SubjectID=" + id;

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
                Subject admin = new Subject();
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    admin = db.Query<Subject>("Select * From Subjects WHERE SubjectID =" + id, new { id }).SingleOrDefault();
                }
                return View(admin);
            }
            else
            {
                return RedirectToAction(controllerName: "adminlogin", actionName: "Index");
            }
            
        }

        // POST: Customer/details/5
        [HttpPost]
        public ActionResult Details(int id, Subject sub)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    string sqlQuery = "Select Subjects from SubjectName='" + sub.SubjectName +
                        "',TotalMarks='" + sub.TotalMarks +
                        "',ClassId='" + sub.ClassID +
                        "' WHERE SubjectID=" + id;

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
                Subject ssub = new();
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    ssub = db.Query<Subject>("Select * From Subjects WHERE SubjectID =" + id, new { id }).SingleOrDefault();
                }
                return View(ssub);
            }
            else
            {
                return RedirectToAction(controllerName: "adminlogin", actionName: "Index");
            }
            
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Subject sub)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    string sqlQuery = "Delete From Subjects WHERE SubjectID = " + id;

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
