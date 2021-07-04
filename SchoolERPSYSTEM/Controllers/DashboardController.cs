using SchoolERPSYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Sql;
using System.Data.SqlClient;
using SchoolERPSYSTEM.Config;
using System.Data;

namespace SchoolERPSYSTEM.Controllers
{
    public class DashboardController : Controller
    {
        private SchoolErpSystemEntities db = new SchoolErpSystemEntities();
        // GET: Dashboard
        public ActionResult Index()
        {
            List<Dashboard> staff = new List<Dashboard>();
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Select s.StaffID,s.Firstname,s.Lastname,d.Designation as 'DesignationName',s.HighestQualification,s.Gender,s.Nationality from Staff s inner join Designation d on s.Designation = d.DesignationID inner join StaffType st on s.StaffType = st.StaffTypeID  ", con))
                {
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtProducts = new DataTable();

                    dtProducts.Load(sdr);

                    foreach (DataRow row in dtProducts.Rows)
                    {
                        staff.Add(
                            new Dashboard
                            {
                                StaffID = Convert.ToInt32(row["StaffID"]),
                                Firstname = row["Firstname"].ToString(),
                                Lastname = row["Lastname"].ToString(),
                                // Designation = Convert.ToInt32(row["Designation"].ToString()),
                                DesignationName = row["DesignationName"].ToString(),
                                //StaffType = Convert.ToInt32(row["StaffType"].ToString()),
                                HighestQualification = row["HighestQualification"].ToString(),

                                Gender = row["Gender"].ToString(),
                                Nationality = row["Nationality"].ToString()


                            }



                            );
                    }
                    con.Close();
                }
            }

            return View(staff);
        }

       
    }
        
            
    }
