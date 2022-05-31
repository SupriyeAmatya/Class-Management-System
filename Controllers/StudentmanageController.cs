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
    public class StudentmanageController : Controller
    {
        //private readonly ClassManagementSystemContext _db;

        //public StudentmanageController(ClassManagementSystemContext db)
        //{

        //    _db = db;
        //}
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("roletype") == "Student")
            {
                string sql = "select * from Student";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", HttpContext.Session.GetInt32("StudentID"));

                var data = Methods.RunQuery<Student>(sql, parameters);
                return View(data);
            }
            else
            {
                return RedirectToAction(controllerName: "adminlogin", actionName: "Index");
            }
        }
        //public ActionResult asd(int id)
        //{
        //    // Find the customer by name
        //    var subjectsn = _db.Subjects.First(c => c.SubjectId == id);

        //    // Get the customers products
        //    var subname = subjectsn.SubjectName;

        //    // Send products to the View to be rendered
        //    return subname;
        //}
        public ActionResult Markdetails()
        {
            if (HttpContext.Session.GetString("roletype") == "Student")
            {

                string sql = "select * from Marks WHERE StudentId = @id";
                
                string sql2 = "Select Marks.SubjectID as SubjectId, Marks.MarksGotten, Marks.TermName, Subjects.SubjectName as SubjectName, Student.StudentID, Student.StudentName from Marks INNER JOIN Subjects ON Marks.SubjectID = Subjects.SubjectID Inner JOIN Student ON Marks.StudentID = Student.StudentID Where Student.StudentID = @id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", HttpContext.Session.GetString("StudentID"));

                //var subjects = Methods.RunQuery<DisplayName>(sql2, parameters);
              
              
                //ViewBag.subname = new SelectList(subjects, "sidm", "sname");
                var data = Methods.RunQuery<DisplayName>(sql2, parameters).ToList();

                return View(data);

              


            }
            else
            {
                return RedirectToAction(controllerName: "adminlogin", actionName: "Index");
            }
        }



        public ActionResult AccountEdit(int id)
        {
            if (HttpContext.Session.GetString("roletype") == "Student")
            {


                string sql = "select * from Student WHERE StudentID = @id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", HttpContext.Session.GetString("StudentID"));

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
        public ActionResult AccountEdit(int id, Student stu)
        {
            try
            {

                string sqlQuery = "UPDATE Student set StudentName= @StudentName, StudentAddress= @StudentAddress, StudentDob=@StudentDob,StudentGender=@StudentGender, PreviousSchoolName=@PreviousSchoolName, ClassID=@ClassID, StudentPassword= @StudentPassword WHERE StudentID= @StudentID";
                string sqlQuery2 = "UPDATE Roles set username= @StudentName, userpassword= @StudentPassword  WHERE TeacherID=@TeacherID";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@StudentName", stu.StudentName);
                parameters.Add("@StudentAddress", stu.StudentAddress);
                parameters.Add("@StudentDOB", stu.StudentDob);
                parameters.Add("@StudentGender", stu.StudentGender);
                parameters.Add("@PreviousSchoolName", stu.PreviousSchoolName);
                parameters.Add("@ClassID", stu.ClassID);
                parameters.Add("@StudentPassword", stu.StudentPassword);

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
            if (HttpContext.Session.GetString("roletype") == "Student")
            {

                string sql = "select * from Student WHERE StudentID = @id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", HttpContext.Session.GetString("StudentID"));

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
        public ActionResult AccountDetails(int id, Student stu)
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
                parameters.Add("@StudentID", HttpContext.Session.GetString("StudentID"));

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
