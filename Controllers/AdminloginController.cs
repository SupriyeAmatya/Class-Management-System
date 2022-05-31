using Class_Management_System.Models;
using Dapper;
using Microsoft.AspNetCore.Authorization;
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

    //[Route("account")]
    public class AdminloginController : Controller
    {
        //[HttpGet]
        //[AllowAnonymous]
        //[Route("")]
        //[Route("index")]
        //[Route("~/")]
        public IActionResult Index()
        {
            return View();
        }
        //[Route("login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[AllowAnonymous]
       
        public IActionResult Login(string username, string password, string role)
        {
            try
            {
                var admin = new Admin();
                IDbConnection connection = new SqlConnection(Dapper.Connection);
                connection.Open();
                var sql = "select * from Roles WHERE username = @username and userpassword = @userpassword";
                var data = connection.ExecuteReader(sql, new { username = username, userpassword = password });
                data.Read();

                var adminnamedata = data["username"].ToString();
                var passworddata = data["userpassword"].ToString();
                var roledata = data["roletype"].ToString();
                var AdminIDdata = data["AdminID"].ToString();
                var StudentIDdata = data["StudentID"].ToString();
                var TeacherIDdata = data["TeacherID"].ToString();
                if (username != null && password != null && username == adminnamedata && password == passworddata)
                {
                    HttpContext.Session.SetString("username", username);
                    var data123= HttpContext.Session.GetString("username");
                    HttpContext.Session.SetString("roletype", roledata);
                    HttpContext.Session.SetString("AdminID", AdminIDdata);
                    HttpContext.Session.SetString("StudentID", StudentIDdata);
                    HttpContext.Session.SetString("TeacherID", TeacherIDdata);
                    //var subdata = connection.Query<Roles>("select roletype from Roles where username= " + username);

                    //ViewBag.rolee = new SelectList(subdata, "roletype");



                    return View("Success");
                }
                else
                {
                    ViewBag.error = "Invalid Account";
                    return View("Index");
                }
            }
            catch(Exception ex)
            {
                ViewBag.error = "Invalid Account";
                return View("Index");

            }
        }

        //[Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("roletype");
            HttpContext.Session.Remove("AdminID");
            HttpContext.Session.Remove("StudentID");
            HttpContext.Session.Remove("TeacherID");
            return RedirectToAction("Index");
        }
    }
}
