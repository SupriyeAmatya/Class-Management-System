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
    public class StudentController : Controller
    {
        public IActionResult Index()
        {

            if (HttpContext.Session.GetString("roletype") == "Admin")
            {
                IDbConnection connection = new SqlConnection(Dapper.Connection);
                connection.Open();
                var data = connection.Query<DisplayName>("Select Student.StudentID, Student.StudentName, Student.StudentAddress, Student.StudentDOB, Student.StudentGender, Student.PreviousSchoolName, Student.StudentPassword, Class.ClassID, Class.ClassName From Student JOIN Class ON Class.ClassID = Student.ClassID;").ToList();
                var classdata = connection.Query<Class>("select * from Class").ToList();
                return View(data);
            }
            else {
                return RedirectToAction(controllerName: "adminlogin", actionName: "Index");
            }

         

        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("roletype") == "Admin")
            {
                var subject = new Class();
                IDbConnection connection = new SqlConnection(Dapper.Connection);
                var subjects = connection.Query<Class>("select * from Class").ToList();
                ViewBag.stuclass = new SelectList(subjects, "ClassID", "ClassName");


                return View();
            }
            else {
                return RedirectToAction(controllerName: "adminlogin", actionName: "Index");
            }

            
        }

        //
        [HttpPost]
        public ActionResult Create(Student stu)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    db.Open();
                    var rolet = "Student";
                    string sqlQuery = "Insert Into Student (StudentName,StudentAddress, StudentDOB, StudentGender, PreviousSchoolName, ClassID, StudentPassword) Values( @StudentName, @StudentAddress, @StudentDOB, @StudentGender, @PreviousSchoolName, @ClassID, @StudentPassword);select IDENT_CURRENT('Student') as count";

                    var rowsAffected = db.ExecuteReader(sqlQuery, new { StudentName = stu.StudentName, StudentAddress = stu.StudentAddress, StudentDOB= stu.StudentDob, StudentGender = stu.StudentGender, PreviousSchoolName = stu.PreviousSchoolName, ClassID = stu.ClassID, StudentPassword= stu.StudentPassword });
                    rowsAffected.Read();
                    var idas = rowsAffected["count"].ToString();
                    rowsAffected.Close();
                    string sqlQuery2 = "Insert Into Roles (username, userpassword, roletype, StudentID) Values( @StudentName, @StudentPassword, @roletype, @studentid)";
                    var rowsAffected2 = db.Execute(sqlQuery2, new { StudentName = stu.StudentName, StudentPassword = stu.StudentPassword, roletype = rolet, studentid = idas });
                   


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
                Student stu = new Student();
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    stu = db.Query<Student>("Select * From Student WHERE StudentID =" + id, new { id }).SingleOrDefault();
                }
                return View(stu);
            }
            else
            {
                return RedirectToAction(controllerName: "adminlogin", actionName: "Index");
            }
            
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Student stu)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    string sqlQuery = "UPDATE Student set StudentName='" + stu.StudentName +
                        "',StudentAddress='" + stu.StudentAddress +
                        "',StudentDob='" + stu.StudentDob +
                        "',StudentGender='" + stu.StudentGender +
                        "',PreviousSchoolName='" + stu.PreviousSchoolName +
                        "',ClassId='" + stu.ClassID+
                        "',StudentPassword='" + stu.StudentPassword +
                        "' WHERE StudentID=" + id;


                    string sqlQuery2 = "UPDATE Roles set username='" + stu.StudentName +
                        "',userpassword='" + stu.StudentPassword +
                        "' WHERE StudentID=" + id;

                    int rowsAffected = db.Execute(sqlQuery);
                    int rowsAffected2 = db.Execute(sqlQuery2);
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
                Student stu = new Student();
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    stu = db.Query<Student>("Select * From Student WHERE StudentID =" + id, new { id }).SingleOrDefault();
                }
                return View(stu);
            }
            else
            {
                return RedirectToAction(controllerName: "adminlogin", actionName: "Index");
            }
           
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Details(int id, Student stu)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    string sqlQuery = "Select Students from StudentName='" + stu.StudentName +
                        "',StudentAddress='" + stu.StudentAddress +
                        "',StudentDob='" + stu.StudentDob +
                        "',StudentGender='" + stu.StudentGender +
                        "',PreviousSchoolName='" + stu.PreviousSchoolName +
                        "',ClassId='" + stu.ClassID +
                        "',StudentPassword='" + stu.StudentPassword +
                        "' WHERE StudentID=" + id;

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
                Student st = new Student();
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    st = db.Query<Student>("Select * From Student WHERE StudentID =" + id, new { id }).SingleOrDefault();
                }
                return View(st);
            }
            else
            {
                return RedirectToAction(controllerName: "adminlogin", actionName: "Index");
            }
          
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Student st)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    string sqlQuery = "Delete From Student WHERE StudentID = " + id;

                    string sqlQuery2 = "Delete From Roles WHERE StudentID = " + id;
                    int rowsAffected = db.Execute(sqlQuery);
                    int rowsAffected2 = db.Execute(sqlQuery2);

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
