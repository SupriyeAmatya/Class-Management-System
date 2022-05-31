using Dapper;
using Class_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    public class AdminController : Controller
    {
        //[Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("roletype") == "Admin")
            {
                IDbConnection connection = new SqlConnection(Dapper.Connection);
                connection.Open();
                var data = connection.Query<Admin>("select * from Admins").ToList();
                return View(data);
            }
            else
            {
                return RedirectToAction(controllerName: "adminlogin", actionName: "Index");
            }
            
        }

        //[Authorize(Roles="Admin")]
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
        //[Authorize]
        public ActionResult Create(Admin admin)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    db.Open();
                    var rolet = "Admin";
                    string sqlQuery = "Insert Into Admins (AdminName,AdminPassword) Values( @AdminName, @AdminPassword);select IDENT_CURRENT('Admins') as count";
                   
                    var rowsAffected = db.ExecuteReader(sqlQuery, new { AdminName = admin.AdminName, AdminPassword = admin.AdminPassword });
                    rowsAffected.Read();
                    var idas = rowsAffected["count"].ToString();
                    rowsAffected.Close();
                    string sqlQuery2 = "Insert Into Roles (username, userpassword, roletype, adminID) Values( @AdminName, @AdminPassword, @roletype, @adminid)";
                    var rowsAffected2 = db.Execute(sqlQuery2, new { AdminName = admin.AdminName, AdminPassword = admin.AdminPassword, roletype = rolet, adminid = idas });
                    
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        // GET: Customer/Edit/5

        //[Authorize]
        public ActionResult Edit(int id, string name)
        {
            if (HttpContext.Session.GetString("roletype") == "Admin")
            {
                Admin admin = new Admin();
                Roles admin2 = new Roles();
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    admin = db.Query<Admin>("Select * From Admins WHERE AdminID =" + id, new { id }).SingleOrDefault();
                    //admin2 = db.Query<Role>("Select * From Role WHERE username =" + name, new { name }).SingleOrDefault();
                }
                return View(admin);
            }
            else
            {
                return RedirectToAction(controllerName: "adminlogin", actionName: "Index");
            }
            
        }

        // POST: Customer/Edit/5
        [HttpPost]
        //[Authorize]
        public ActionResult Edit(int id, Admin admin)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    string sqlQuery = "UPDATE Admins set AdminName='" + admin.AdminName +
                        "',AdminPassword='" + admin.AdminPassword +
                        "' WHERE AdminID=" + id;

                    string sqlQuery2 = "UPDATE Roles set username='" + admin.AdminName +
                        "',userpassword='" + admin.AdminPassword +
                        "' WHERE adminID=" + id;

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

        //[Authorize]
        public ActionResult Details(int id, string name)
        {
            if (HttpContext.Session.GetString("roletype") == "Admin")
            {
                Admin admin = new Admin();
                Roles admin2 = new Roles();
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    admin = db.Query<Admin>("Select * From Admins WHERE AdminID =" + id, new { id }).SingleOrDefault();
                    //admin2 = db.Query<Role>("Select * From Role WHERE username =" + name, new { name }).SingleOrDefault();
                }
                return View(admin);
            }
            else
            {
                return RedirectToAction(controllerName: "adminlogin", actionName: "Index");
            }
            
        }

        // POST: Customer/Edit/5
        [HttpPost]
        //[Authorize]
        public ActionResult Details(int id, Admin admin, string name)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    string sqlQuery = "Select Admins from AdminName='" + admin.AdminName +
                        "',AdminPassword='" + admin.AdminPassword +
                        "' WHERE AdminID=" + id;

                    //string sqlQuery2 = "Select Role from username='" + admin.AdminName +
                    //   "',userpassword='" + admin.AdminPassword +
                    //   "' WHERE username=" + name;

                    int rowsAffected = db.Execute(sqlQuery);
                    //int rowsAffected2 = db.Execute(sqlQuery2);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }


        //[Authorize]
        public ActionResult Delete(int id, string name)
        {
            if (HttpContext.Session.GetString("roletype") == "Admin")
            {
                Admin admin = new Admin();
                Roles admin2 = new Roles();
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    admin = db.Query<Admin>("Select * From Admins WHERE AdminID =" + id, new { id }).SingleOrDefault();
                    //admin2 = db.Query<Role>("Select * From Role WHERE username =" + name, new { name }).SingleOrDefault();
                }
                return View(admin);
            }
            else
            {
                return RedirectToAction(controllerName: "adminlogin", actionName: "Index");
            }
           
        }

        // POST: Customer/Delete/5
        [HttpPost]
        //[Authorize]
        public ActionResult Delete(int id, Admin admin)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Dapper.Connection))
                {
                    string sqlQuery = "Delete From Admins WHERE AdminID = " + id;
                    string sqlQuery2 = "Delete From Roles WHERE adminID = " + id;
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
        //public string password(int digit)
        //    {
        //    var result = "";
        //    var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        //    var charactersLength = characters.digit;
        //    for (var i = 0; i < length; i++)
        //    {
        //        result += characters.charAt(Math.Floor(Math.random() *
        //   charactersLength));
        //    }
        //    return result;

        //}
    }
}
