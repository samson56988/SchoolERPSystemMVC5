using SchoolERPSYSTEM.Config;
using SchoolERPSYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolERPSYSTEM.Controllers
{
    public class TeacherDashboardController : Controller
    {
        // GET: TeacherDashboard
        public ActionResult Index()
        {

            string Username = (string)Session["Username"];
            List<TeacherDashboard> student = new List<TeacherDashboard>();
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Select * from StudentTble s inner join AssignClass a on a.ClassLevelID =  s.ClassLevel inner join Staff st on a.StaffID = st.StaffID inner join Role r on st.StaffID = r.StaffID where r.Username = '"+Username+"' ", con))
                {
                    cmd.Parameters.AddWithValue("Username", Session["Username"].ToString());
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtProducts = new DataTable();

                    dtProducts.Load(sdr);

                    foreach (DataRow row in dtProducts.Rows)
                    {
                        student.Add(
                            new TeacherDashboard
                            {
                                AdmissionNo = row["AdmissionNo"].ToString(),
                                FirstName = row["Firstname"].ToString(),
                                LastName = row["Lastname"].ToString()                                


                            }



                            );
                    }
                    con.Close();
                }
            }

            return View(student);





        }
    }
}