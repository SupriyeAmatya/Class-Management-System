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
    public class MarkController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("roletype") == "Admin"|| HttpContext.Session.GetString("roletype") == "Teacher"|| HttpContext.Session.GetString("roletype") == "Student")
            {
                IDbConnection connection = new SqlConnection(Dapper.Connection);
                connection.Open();
                var data = connection.Query<DisplayName>("Select Marks.MarksID, Subjects.SubjectID, Subjects.SubjectName, Student.StudentID, Student.StudentName, Marks.TermName, MarksGotten from Marks JOIN Subjects ON Subjects.SubjectID = Marks.SubjectID JOIN Student ON Student.StudentID = Marks.StudentID;").ToList();
                var subdata = connection.Query<Subject>("select * from Class").ToList();
                var studata = connection.Query<Student>("select * from Class").ToList();
                return View(data);
            }
            else
            {
                return RedirectToAction(controllerName: "adminlogin", actionName: "Index");
            }
            
        }

        public IActionResult Create()
        {

            if (HttpContext.Session.GetString("roletype") == "Admin" || HttpContext.Session.GetString("roletype") == "Teacher" || HttpContext.Session.GetString("roletype") == "Student")
            {
                //var subject = new Subject();
                IDbConnection connection = new SqlConnection(Dapper.Connection);
                var subdata = connection.Query<Subject>("select * from Subjects").ToList();
                ViewBag.TotalSubs = new SelectList(subdata, "SubjectId", "SubjectName");
                var studata = connection.Query<Student>("Select * From Student").ToList();
                ViewBag.TotalStu = new SelectList(studata, "StudentId", "StudentName");
                return View();
            }
            else
            {
                return RedirectToAction(controllerName: "adminlogin", actionName: "Index");
            }

           
        }

        //
        [HttpPost]
        public ActionResult Create(Mark mark)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    db.Open();
                    string sqlQuery = "Insert Into Marks (SubjectID, StudentID, TermName, MarksGotten) Values( @SubjectID, @StudentID, @TermName, @MarksGotten)";

                    var rowsAffected = db.Execute(sqlQuery, new { SubjectID = mark.SubjectId, StudentID = mark.StudentId, TermName = mark.TermName, MarksGotten = mark.MarksGotten  });
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
            if (HttpContext.Session.GetString("roletype") == "Admin" || HttpContext.Session.GetString("roletype") == "Teacher" || HttpContext.Session.GetString("roletype") == "Student")
            {
                Mark mark = new Mark();
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    mark = db.Query<Mark>("Select * From Marks WHERE MarksID =" + id, new { id }).SingleOrDefault();
                }
                return View(mark);
            }
            else
            {
                return RedirectToAction(controllerName: "adminlogin", actionName: "Index");
            }
            
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Mark mark )
        {

            try
            {
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    string sqlQuery = "UPDATE Marks set SubjectID='" + mark.SubjectId +
                        "',StudentId='" + mark.StudentId +
                        "',TermName='" + mark.TermName +
                        "',MarksGotten='" + mark.MarksGotten +
                        "' WHERE MarksID=" + id;

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
            if (HttpContext.Session.GetString("roletype") == "Admin" || HttpContext.Session.GetString("roletype") == "Teacher" || HttpContext.Session.GetString("roletype") == "Student")
            {
                Mark mark = new Mark();
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    mark = db.Query<Mark>("Select * From Marks WHERE MarksID =" + id, new { id }).SingleOrDefault();
                }
                return View(mark);
            }
            else
            {
                return RedirectToAction(controllerName: "adminlogin", actionName: "Index");
            }
            
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Details(int id, Mark mark)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    string sqlQuery = "Select Marks set SubjectID='" + mark.SubjectId +
                        "',StudentId='" + mark.StudentId +
                        "',TermName='" + mark.TermName +
                        "',MarksGotten='" + mark.MarksGotten +
                        "' WHERE MarksID=" + id;

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
            if (HttpContext.Session.GetString("roletype") == "Admin" || HttpContext.Session.GetString("roletype") == "Teacher" || HttpContext.Session.GetString("roletype") == "Student")
            {
                Mark mark = new Mark();
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    mark = db.Query<Mark>("Select * From Marks WHERE MarksID =" + id, new { id }).SingleOrDefault();
                }
                return View(mark);
            }
            else
            {
                return RedirectToAction(controllerName: "adminlogin", actionName: "Index");
            }
            
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Mark mark)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    string sqlQuery = "Delete From Marks WHERE MarksID = " + id;

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
