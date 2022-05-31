using Class_Management_System.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Class_Management_System.Controllers
{
    public class TeachermanageController : Controller
    {
       

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("roletype") == "Teacher")
            {
                string sql = "Select Student.StudentID, Student.StudentName, Student.StudentAddress, Student.StudentDOB, Student.StudentGender, Student.PreviousSchoolName, Student.StudentPassword, Class.ClassID, Class.ClassName From Student JOIN Class ON Class.ClassID = Student.ClassID;";
                string sql2 = "select * from Class";
           

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", HttpContext.Session.GetInt32("TeacherID"));

                var data = Methods.RunQuery<DisplayName>(sql, parameters);
                var data2 = Methods.RunQuery<Class>(sql2, parameters);
                ViewBag.stuclass = new SelectList(data2, "ClassID", "ClassName");
                return View(data);
            }
            else
            {
                return RedirectToAction(controllerName: "adminlogin", actionName: "Index");
            }
            
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("roletype") == "Teacher")
            {
                string sql2 = "select * from Class";


                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", HttpContext.Session.GetInt32("TeacherID"));

                var data2 = Methods.RunQuery<Class>(sql2, parameters);
                ViewBag.stuclass = new SelectList(data2, "ClassID", "ClassName");
                //string sql = "select * from Class";
                //DynamicParameters parameters = new DynamicParameters();
                //parameters.Add("@id", HttpContext.Session.GetString("TeacherID"));

                //var data = Methods.RunQuery<Class>(sql, parameters);
                //return View(data);


                //var subject = new Class();
                //IDbConnection connection = new SqlConnection(Dapper.Connection);
                //var subjects = connection.Query<Class>("select * from Class").ToList();
                //ViewBag.stuclass = new SelectList(subjects, "ClassID", "ClassName");


                return View();
            }
            else
            {
                return RedirectToAction(controllerName: "adminlogin", actionName: "Index");
            }


        }

        //
        [HttpPost]
        public ActionResult Create(Student stu)
        {
            try
            {
               

                string sql = "Insert Into Student(StudentName, StudentAddress, StudentDOB, StudentGender, PreviousSchoolName, ClassID, StudentPassword) Values(@StudentName, @StudentAddress, @StudentDOB, @StudentGender, @PreviousSchoolName, @ClassID, @StudentPassword); select IDENT_CURRENT('Student') as count";
               
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@StudentName", stu.StudentName);
                parameters.Add("@StudentAddress", stu.StudentAddress);
                parameters.Add("@StudentDOB", stu.StudentDob);
                parameters.Add("@StudentGender", stu.StudentGender);
                parameters.Add("@PreviousSchoolName", stu.PreviousSchoolName);
                parameters.Add("@ClassID", stu.ClassID);
                parameters.Add("@StudentPassword", stu.StudentPassword);

                var data = Methods.RunQuery<Student>(sql, parameters);
                var iddata = Methods.GetID(sql, parameters);


                var rolet = "Student";
                string sql3 = "Insert Into Roles(username, userpassword, roletype, StudentID) Values(@StudentName, @StudentPassword, @roletype, @studentid)";
                parameters.Add("@StudentName", stu.StudentName);
                parameters.Add("@StudentAddress", stu.StudentAddress);
                parameters.Add("@roletype", rolet);
                parameters.Add("@studentid", iddata);

                var dat2 = Methods.RunQuery<Roles>(sql3, parameters);

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
            if (HttpContext.Session.GetString("roletype") == "Teacher")
            {

                string sql = "select * from Student WHERE StudentID = @id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", id);

                var data = Methods.RunQuery<Student>(sql, parameters).SingleOrDefault();
                return View(data);

                //Student stu = new Student();
                //using (IDbConnection db = new SqlConnection(Dapper.Connection))
                //{
                //    stu = db.Query<Student>("Select * From Student WHERE StudentID =" + id, new { id }).SingleOrDefault();
                //}
                //return View(stu);




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
               
                    string sqlQuery = "UPDATE Student set StudentName= @StudentName, StudentAddress= @StudentAddress, StudentDob=@StudentDob,StudentGender=@StudentGender, PreviousSchoolName=@PreviousSchoolName, ClassID=@ClassID, StudentPassword= @StudentPassword WHERE StudentID= @StudentID";
                    string sqlQuery2 = "UPDATE Roles set username= @StudentName, userpassword= @StudentPassword  WHERE StudentID=@StudentID";
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@StudentName", stu.StudentName);
                    parameters.Add("@StudentAddress", stu.StudentAddress);
                    parameters.Add("@StudentDOB", stu.StudentDob);
                    parameters.Add("@StudentGender", stu.StudentGender);
                    parameters.Add("@PreviousSchoolName", stu.PreviousSchoolName);
                    parameters.Add("@ClassID", stu.ClassID);
                    parameters.Add("@StudentPassword", stu.StudentPassword);
                    parameters.Add("@StudentID", id);

                    var data = Methods.RunQuery<Student>(sqlQuery, parameters);
                    var data2 = Methods.RunQuery<Roles>(sqlQuery2, parameters);
                

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("roletype") == "Teacher")
            {

                string sql = "select * from Student WHERE StudentID = @id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", id);

                var data = Methods.RunQuery<Student>(sql, parameters).SingleOrDefault();
                return View(data);
                //Student stu = new Student();
                //using (IDbConnection db = new SqlConnection(Dapper.Connection))
                //{
                //    stu = db.Query<Student>("Select * From Student WHERE StudentID =" + id, new { id }).SingleOrDefault();
                //}
                //return View(stu);
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

                string sqlQuery = "Select from Student where StudentName= @StudentName, StudentAddress= @StudentAddress, StudentDob=@StudentDob,StudentGender=@StudentGender, PreviousSchoolName=@PreviousSchoolName, ClassID=@ClassID, StudentPassword= @StudentPassword WHERE StudentID= @StudentID";
             
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@StudentName", stu.StudentName);
                parameters.Add("@StudentAddress", stu.StudentAddress);
                parameters.Add("@StudentDOB", stu.StudentDob);
                parameters.Add("@StudentGender", stu.StudentGender);
                parameters.Add("@PreviousSchoolName", stu.PreviousSchoolName);
                parameters.Add("@ClassID", stu.ClassID);
                parameters.Add("@StudentPassword", stu.StudentPassword);
                parameters.Add("@StudentID", id);

                var data = Methods.RunQuery<Student>(sqlQuery, parameters);
              

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("roletype") == "Teacher")
            {
                string sql = "select * from Student WHERE StudentID = @id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", id);

                var data = Methods.RunQuery<Student>(sql, parameters).SingleOrDefault();
                return View(data);
                //Student st = new Student();
                //using (IDbConnection db = new SqlConnection(Dapper.Connection))
                //{
                //    st = db.Query<Student>("Select * From Student WHERE StudentID =" + id, new { id }).SingleOrDefault();
                //}
                //return View(st);
            }
            else
            {
                return RedirectToAction(controllerName: "adminlogin", actionName: "Index");
            }

        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Student stu)
        {
            try
            {
                string sqlQuery = "Delete From Student WHERE StudentID= @StudentID";
                string sqlQuery2 = "Delete From Roles WHERE StudentID= @StudentID";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@StudentName", stu.StudentName);
                parameters.Add("@StudentAddress", stu.StudentAddress);
                parameters.Add("@StudentDOB", stu.StudentDob);
                parameters.Add("@StudentGender", stu.StudentGender);
                parameters.Add("@PreviousSchoolName", stu.PreviousSchoolName);
                parameters.Add("@ClassID", stu.ClassID);
                parameters.Add("@StudentPassword", stu.StudentPassword);
                parameters.Add("@StudentID", id);

                var data = Methods.RunQuery<Student>(sqlQuery, parameters);
                var data2 = Methods.RunQuery<Roles>(sqlQuery2, parameters);

               

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public ActionResult AccountEdit(int id)
        {
            if (HttpContext.Session.GetString("roletype") == "Teacher")
            {

                string sql = "select * from Teacher WHERE TeacherID = @id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", HttpContext.Session.GetString("TeacherID"));

                var data = Methods.RunQuery<Teacher>(sql, parameters).SingleOrDefault();
                return View(data);

                //Student stu = new Student();
                //using (IDbConnection db = new SqlConnection(Dapper.Connection))
                //{
                //    stu = db.Query<Student>("Select * From Student WHERE StudentID =" + id, new { id }).SingleOrDefault();
                //}
                //return View(stu);




            }
            else
            {
                return RedirectToAction(controllerName: "adminlogin", actionName: "Index");
            }

        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult AccountEdit(int id, Teacher teach)
        {
            try
            {

                string sqlQuery = "UPDATE Teacher set TeacherName= @TeacherName, TeacherAddress= @TeacherAddress, TeacherGender=@TeacherGender, SubjectID=@SubjectID, TeacherPassword= @TeacherPassword WHERE TeacherID= @TeacherID";
                string sqlQuery2 = "UPDATE Roles set username= @TeacherName, userpassword= @TeacherPassword  WHERE TeacherID=@TeacherID";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@StudentName", teach.TeacherName);
                parameters.Add("@StudentAddress", teach.TeacherAddress);
                parameters.Add("@StudentGender", teach.TeacherGender);
                parameters.Add("@SubjectID", teach.SubjectId);
                parameters.Add("@TeacherPassword", teach.TeacherPassword);
                parameters.Add("@TeacherID", id);

                var data = Methods.RunQuery<Student>(sqlQuery, parameters);
                var data2 = Methods.RunQuery<Roles>(sqlQuery2, parameters);


                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public ActionResult AccountDetails(int id)
        {
            if (HttpContext.Session.GetString("roletype") == "Teacher")
            {

                string sql = "select * from Teacher WHERE TeacherID = @id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", HttpContext.Session.GetString("TeacherID"));

                var data = Methods.RunQuery<Teacher>(sql, parameters).SingleOrDefault();
                return View(data);
                //Student stu = new Student();
                //using (IDbConnection db = new SqlConnection(Dapper.Connection))
                //{
                //    stu = db.Query<Student>("Select * From Student WHERE StudentID =" + id, new { id }).SingleOrDefault();
                //}
                //return View(stu);
            }
            else
            {
                return RedirectToAction(controllerName: "adminlogin", actionName: "Index");
            }

        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult AccountDetails(int id, Teacher teach)
        {
            try
            {

                string sqlQuery = "Select Teacher From TeacherName= @TeacherName, TeacherAddress= @TeacherAddress, TeacherGender=@TeacherGender, SubjectID=@SubjectID, TeacherPassword= @TeacherPassword WHERE TeacherID= @TeacherID";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@StudentName", teach.TeacherName);
                parameters.Add("@StudentAddress", teach.TeacherAddress);
                parameters.Add("@StudentGender", teach.TeacherGender);
                parameters.Add("@SubjectID", teach.SubjectId);
                parameters.Add("@TeacherPassword", teach.TeacherPassword);
                parameters.Add("@TeacherID", id);

                var data = Methods.RunQuery<Student>(sqlQuery, parameters);


                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
