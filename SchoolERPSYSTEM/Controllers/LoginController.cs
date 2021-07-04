using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using SchoolERPSYSTEM.Models;
using System.Web.Security;

namespace SchoolERPSYSTEM.Controllers
{
    public class LoginController : Controller
    {

        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;


        [HttpGet]
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult test()
        {
            return View();
        }

        void connectionString()
        {
            con.ConnectionString = "Data Source=DESKTOP-J3DHBNP\\;Initial Catalog=SchoolErpSystem;Integrated Security=True";
        }

        [HttpPost]
        public ActionResult Verify(Account acc)
        {
            string usernam = "", role_name = "";
            bool found = false;
            SqlDataReader dr;
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "Select * from Role where Username = '"+acc.Username+"' and Password = '"+acc.Password+"' ";

            dr = com.ExecuteReader();
            if(dr.Read())
            {
                found = true;
                usernam = dr["Username"].ToString();
                role_name = dr["Role"].ToString();
                FormsAuthentication.SetAuthCookie(acc.Username, true);
                Session["Username"] = acc.Username.ToString();
                
            }
            else
            {

                found = false;

            }
            dr.Close();
            con.Close();
            if (found == true)
            {

                if (role_name == "Teacher")
                {
                    FormsAuthentication.SetAuthCookie(acc.Username, true);
                    Session["Username"] = acc.Username.ToString();
                    return RedirectToAction("Index","TeacherDashboard");

                }

                else if (role_name == "Admin")
                {
                    FormsAuthentication.SetAuthCookie(acc.Username, true);
                    Session["Username"] = acc.Username.ToString();
                    return RedirectToAction("Index", "Dashboard");

                }
               


            }
            else
            {
                ViewData["message"] = "Username & password are wrong!";
            }
            con.Close();
            return View();



        }


    }
}