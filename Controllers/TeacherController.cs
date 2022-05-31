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
using Class_Management_System.ViewModel;
using Microsoft.AspNetCore.Http;

namespace Class_Management_System.Controllers
{

    public class TeacherController : Controller
    {



        
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("roletype") == "Admin")
            {
                IDbConnection connection = new SqlConnection(Dapper.Connection);
                connection.Open();
                var data = connection.Query<DisplayName>("Select Teacher.TeacherID, Teacher.TeacherName, Teacher.TeacherAddress, Teacher.TeacherGender, Subjects.SubjectName, Teacher.TeacherPassword From Teacher JOIN Subjects ON Subjects.SubjectID = Teacher.SubjectID;").ToList();

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
                var subject = new Subject();
                IDbConnection connection = new SqlConnection(Dapper.Connection);
                var subdata = connection.Query<Subject>("select * from Subjects").ToList();
                ViewBag.TotalSubs = new SelectList(subdata, "SubjectId", "SubjectName");
            }
            else
            {
                return RedirectToAction(controllerName: "adminlogin", actionName: "Index");
            }
           

            //int i = 0;
            //while(subdata.Read())
            //{
            //    var subjectname = subdata["SubjectName"].ToString();
            //    var subjectid = subdata["SubjectID"].ToString();
            //    subject.Add(subjectid, subjectname);
            //    i++;
            //}

            //ViewBag.TotalSubs = new SelectList(subject, "SubjectId", "SubjectName");
            //ViewBag.Totalid = subject2;
            return View();
        }
    
        //
        [HttpPost]
        public ActionResult Create(Teacher teach)
        {

            

            try
            {
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    db.Open();
                    var rolet = "Teacher";

                    string sqlQuery = "Insert Into Teacher (TeacherName, TeacherAddress, TeacherGender, SubjectID, TeacherPassword) Values( @TeacherName, @TeacherAddress, @TeacherGender, @SubjectID, @TeacherPassword);select IDENT_CURRENT('Teacher') as count";

                    var rowsAffected = db.ExecuteReader(sqlQuery, new { TeacherName = teach.TeacherName, TeacherAddress = teach.TeacherAddress, TeacherGender = teach.TeacherGender, SubjectID = teach.SubjectId, TeacherPassword = teach.TeacherPassword});


                    rowsAffected.Read();
                    var idas = rowsAffected["count"].ToString();
                    rowsAffected.Close();
                    string sqlQuery2 = "Insert Into Roles (username, userpassword, roletype, TeacherID) Values( @TeacherName, @TeacherPassword, @roletype, @teacherid)";
                    var rowsAffected2 = db.Execute(sqlQuery2, new { TeacherName = teach.TeacherName, TeacherPassword = teach.TeacherPassword, roletype = rolet, teacherid = idas });

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
                Teacher teach = new Teacher();
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    teach = db.Query<Teacher>("Select * From Teacher WHERE TeacherID =" + id, new { id }).SingleOrDefault();
                }
                return View(teach);
            }
            else
            {
                return RedirectToAction(controllerName: "adminlogin", actionName: "Index");
            }
            
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Teacher teach)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    string sqlQuery = "UPDATE Teacher set TeacherName='" + teach.TeacherName +
                        "',TeacherAddress='" + teach.TeacherAddress +
                        "',TeacherGender='" + teach.TeacherGender +
                        "',SubjectID='" + teach.SubjectId +
                        "',TeacherPassword='" + teach.TeacherPassword +
                        "' WHERE TeacherID=" + id;

                    string sqlQuery2 = "UPDATE Roles set username='" + teach.TeacherName +
                        "',userpassword='" + teach.TeacherPassword +
                        "' WHERE TeacherID=" + id;

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
                Teacher sad = new Teacher();
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    sad = db.Query<Teacher>("Select * From Teacher WHERE TeacherID =" + id, new { id }).SingleOrDefault();
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
        public ActionResult Details(int id, Teacher teach)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    string sqlQuery = "Select Teacher set TeacherName='" + teach.TeacherName +
                        "',TeacherAddress='" + teach.TeacherAddress +
                        "',TeacherGender='" + teach.TeacherGender +
                        "',SubjectID='" + teach.SubjectId +
                        "',TeacherPassword='" + teach.TeacherPassword +
                        "' WHERE TeacherID=" + id;

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
                Teacher clsss = new Teacher();
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    clsss = db.Query<Teacher>("Select * From Teacher WHERE TeacherID =" + id, new { id }).SingleOrDefault();
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
        public ActionResult Delete(int id, Teacher Cldss)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    string sqlQuery = "Delete From Teacher WHERE TeacherID = " + id;
                    string sqlQuery2 = "Delete From Roles WHERE TeacherID = " + id;
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
