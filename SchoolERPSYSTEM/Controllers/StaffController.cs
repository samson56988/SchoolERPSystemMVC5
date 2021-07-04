using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using SchoolERPSYSTEM.Models;
using SchoolERPSYSTEM.Config;
using System.IO;
using System.Web.UI;

namespace SchoolERPSYSTEM.Controllers


{

    public class StaffController : Controller
    {
        SchoolErpSystemEntities db = new SchoolErpSystemEntities();

        // GET: Staff
        public ActionResult Index()
        {
            List<MStaff> staff = new List<MStaff>();
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
                            new MStaff
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

        public ActionResult Create()
        {
            FillStaffType();

            FillDesignation();
            return View();
        }

        [HttpPost]
        public ActionResult Create(MStaff staff)
        {


            string filename = Path.GetFileNameWithoutExtension(staff.ImageFile.FileName);
            string extension = Path.GetExtension(staff.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            staff.Image = "~/images/" + filename;
            filename = Path.Combine(Server.MapPath("~/images/"), filename);
            staff.ImageFile.SaveAs(filename);






            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("insertStaff", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Designation", staff.Designation);
                    cmd.Parameters.AddWithValue("@StaffType", staff.StaffType);
                    cmd.Parameters.AddWithValue("@Firstname", staff.Firstname);
                    cmd.Parameters.AddWithValue("@Lastname", staff.Lastname);
                    cmd.Parameters.AddWithValue("@Gender", staff.Gender);
                    cmd.Parameters.AddWithValue("@Phone1", staff.Phone1);
                    cmd.Parameters.AddWithValue("@Phone2", staff.Phone2);
                    cmd.Parameters.AddWithValue("@Email", staff.Email);
                    cmd.Parameters.AddWithValue("@DateOfAppointmentdate", staff.DateOfAppointment);
                    cmd.Parameters.AddWithValue("@Nationality", staff.Nationality);
                    cmd.Parameters.AddWithValue("@Address", staff.Address);
                    cmd.Parameters.AddWithValue("@HighestQualifiaction", staff.HighestQualification);
                    cmd.Parameters.AddWithValue("@YearOfExperience", staff.YearOfExperience);
                    cmd.Parameters.AddWithValue("@PreviousOrg", staff.PreviouseOrganization);
                    cmd.Parameters.AddWithValue("@Image", staff.Image);
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();
                    cmd.ExecuteNonQuery();

                }
                con.Close();





            }

            TempData["SuccessMessage"] = "Save Successfully";

            return RedirectToAction("Index");
        }

        public void FillStaffType()
        {

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Select * from StaffType", con))
                {
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    List<SelectListItem> li = new List<SelectListItem>();
                    li.Add(new SelectListItem { Text = "Select", Value = "0" });
                    while (rdr.Read())
                    {
                        li.Add(new SelectListItem { Text = rdr[1].ToString(), Value = rdr[0].ToString() });
                    }
                    ViewData["Id1"] = li;
                }
                con.Close();

            }




        }

        public void FillDesignation()
        {

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Select * from Designation", con))
                {
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    List<SelectListItem> li = new List<SelectListItem>();
                    li.Add(new SelectListItem { Text = "Select", Value = "0" });
                    while (rdr.Read())
                    {
                        li.Add(new SelectListItem { Text = rdr[1].ToString(), Value = rdr[0].ToString() });
                    }
                    ViewData["Id"] = li;
                }
            }


        }

        public void FillStaffID()
        {
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Select * from Staff", con))
                {
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    List<SelectListItem> li = new List<SelectListItem>();
                    li.Add(new SelectListItem { Text = "Select", Value = "0" });
                    while (rdr.Read())
                    {
                        li.Add(new SelectListItem { Text = rdr[0].ToString(), Value = rdr[0].ToString() });
                    }
                    ViewData["Id"] = li;
                }
            }
        }

        public ActionResult Edit(int id)
        {
            MStaff staff = new MStaff();

            DataTable dtStaff = new DataTable();
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                con.Open();
                string query = "Select * from Staff Where StaffID = @StaffID";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, con);
                sqlDa.SelectCommand.Parameters.AddWithValue("@StaffID", id);
                sqlDa.Fill(dtStaff);



            }

            if (dtStaff.Rows.Count == 1)
            {
                FillStaffType();
                FillDesignation();

                staff.StaffID = Convert.ToInt32(dtStaff.Rows[0][0].ToString());
                staff.Designation = Convert.ToInt32(dtStaff.Rows[0][1].ToString());
                staff.StaffType = Convert.ToInt32(dtStaff.Rows[0][2].ToString());
                staff.Firstname = dtStaff.Rows[0][3].ToString();
                staff.Lastname = dtStaff.Rows[0][4].ToString();
                staff.Gender = dtStaff.Rows[0][5].ToString();
                staff.Phone1 = Convert.ToDecimal(dtStaff.Rows[0][6].ToString());
                staff.Phone2 = Convert.ToDecimal(dtStaff.Rows[0][7].ToString());
                staff.Email = dtStaff.Rows[0][8].ToString();
                staff.DateOfAppointment = Convert.ToDateTime(dtStaff.Rows[0][9].ToString());
                staff.Nationality = dtStaff.Rows[0][10].ToString();
                staff.Address = dtStaff.Rows[0][11].ToString();
                staff.HighestQualification = dtStaff.Rows[0][12].ToString();
                staff.YearOfExperience = Convert.ToInt32(dtStaff.Rows[0][13].ToString());
                staff.PreviouseOrganization = dtStaff.Rows[0][14].ToString();
                staff.Image = dtStaff.Rows[0][15].ToString();
                return View(staff);


            }
            else




                return View("index");
        }

        [HttpPost]
        public ActionResult Edit(int id, MStaff staff)
        {
            string filename = Path.GetFileNameWithoutExtension(staff.ImageFile.FileName);
            string extension = Path.GetExtension(staff.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            staff.Image = "~/images/" + filename;
            filename = Path.Combine(Server.MapPath("~/images/"), filename);
            staff.ImageFile.SaveAs(filename);






            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateStaff", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Designation", staff.Designation);
                    cmd.Parameters.AddWithValue("@StaffType", staff.StaffType);
                    cmd.Parameters.AddWithValue("@Firstname", staff.Firstname);
                    cmd.Parameters.AddWithValue("@Lastname", staff.Lastname);
                    cmd.Parameters.AddWithValue("@Gender", staff.Gender);
                    cmd.Parameters.AddWithValue("@Phone1", staff.Phone1);
                    cmd.Parameters.AddWithValue("@Phone2", staff.Phone2);
                    cmd.Parameters.AddWithValue("@Email", staff.Email);
                    cmd.Parameters.AddWithValue("@DateOfAppointmentdate", staff.DateOfAppointment);
                    cmd.Parameters.AddWithValue("@Nationality", staff.Nationality);
                    cmd.Parameters.AddWithValue("@Address", staff.Address);
                    cmd.Parameters.AddWithValue("@HighestQualifiaction", staff.HighestQualification);
                    cmd.Parameters.AddWithValue("@YearOfExperience", staff.YearOfExperience);
                    cmd.Parameters.AddWithValue("@PreviousOrg", staff.PreviouseOrganization);
                    cmd.Parameters.AddWithValue("@Image", staff.Image);
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();
                    cmd.ExecuteNonQuery();

                }
                con.Close();





            }



            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            MStaff staff = new MStaff();

            DataTable dtStaff = new DataTable();
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                con.Open();
                string query = "Select * from Staff Where StaffID = @StaffID";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, con);
                sqlDa.SelectCommand.Parameters.AddWithValue("@StaffID", id);
                sqlDa.Fill(dtStaff);



            }

            if (dtStaff.Rows.Count == 1)
            {
                staff.StaffID = Convert.ToInt32(dtStaff.Rows[0][0].ToString());
                staff.Designation = Convert.ToInt32(dtStaff.Rows[0][1].ToString());
                staff.StaffType = Convert.ToInt32(dtStaff.Rows[0][2].ToString());
                staff.Firstname = dtStaff.Rows[0][3].ToString();
                staff.Lastname = dtStaff.Rows[0][4].ToString();
                staff.Gender = dtStaff.Rows[0][5].ToString();
                staff.Phone1 = Convert.ToDecimal(dtStaff.Rows[0][6].ToString());
                staff.Phone2 = Convert.ToDecimal(dtStaff.Rows[0][7].ToString());
                staff.Email = dtStaff.Rows[0][8].ToString();
                staff.DateOfAppointment = Convert.ToDateTime(dtStaff.Rows[0][9].ToString());
                staff.Nationality = dtStaff.Rows[0][10].ToString();
                staff.Address = dtStaff.Rows[0][11].ToString();
                staff.HighestQualification = dtStaff.Rows[0][12].ToString();
                staff.YearOfExperience = Convert.ToInt32(dtStaff.Rows[0][13].ToString());
                staff.PreviouseOrganization = dtStaff.Rows[0][14].ToString();
                staff.Image = dtStaff.Rows[0][15].ToString();
                return View(staff);


            }
            else




                return View("index");
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteStaff", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);

                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();
                    cmd.ExecuteNonQuery();

                }
                con.Close();

            }
            return View("index");
        }

        public ActionResult ActivatedStaffList()
        {
            List<MStaff> staff = new List<MStaff>();
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(" Select StaffID, Firstname + '  ' + Lastname as Name,AccountActive from Staff", con))
                {
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtProducts = new DataTable();

                    dtProducts.Load(sdr);

                    foreach (DataRow row in dtProducts.Rows)
                    {
                        staff.Add(
                            new MStaff
                            {
                                StaffID = Convert.ToInt32(row["StaffID"]),
                                Fullname = row["Name"].ToString(),
                                AccountStatus = row["AccountActive"].ToString()


                            }



                            );
                    }
                    con.Close();
                }
            }

            return View(staff);
        }

        public ActionResult Activateaccount(int id)
        {

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Update Staff set AccountActive = 'Active' where StaffID = '" + id + "' ", con))
                {

                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    cmd.ExecuteNonQuery();
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Success','Record Saved Successfully!...','success')", true);
                    return RedirectToAction("ActivatedStaffList");



                }
            }
        }

        public ActionResult DeActivateaccount(int id)
        {

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Update Staff set AccountActive = 'In-Active' where StaffID = '" + id + "' ", con))
                {

                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    cmd.ExecuteNonQuery();
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Success','Record Saved Successfully!...','success')", true);
                    return RedirectToAction("ActivatedStaffList");



                }
            }
        }

        public ActionResult RoleList()
        {
            List<Role> role = new List<Role>();
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Select s.StaffID,r.Role,s.Firstname + '  ' + s.Lastname as Name from Role r inner join Staff s on r.StaffID = s.StaffID", con))
                {
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtProducts = new DataTable();

                    dtProducts.Load(sdr);

                    foreach (DataRow row in dtProducts.Rows)
                    {
                        role.Add(
                            new Role
                            {
                                StaffID = Convert.ToInt32(row["StaffID"]),
                                Fullname = row["Name"].ToString(),
                                Rolename = row["Role"].ToString()




                            }



                            );
                    }
                    con.Close();
                }
            }


            return View(role);
        }

        public ActionResult Role()
        {
            FillStaffID();

            return View();
        }

        public ActionResult CreateTeacherRole(Role role)
        {
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("InsertTeacherRole", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StaffID", role.StaffID);
                    cmd.Parameters.AddWithValue("@Username", role.Username);
                    cmd.Parameters.AddWithValue("@Password", role.Password);
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();
                    cmd.ExecuteNonQuery();

                }
                con.Close();
            }
            return RedirectToAction("Role");




        }

        public ActionResult CreateAdminRole(Role role)
        {
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("insertAdminRole", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StaffID", role.StaffID);
                    cmd.Parameters.AddWithValue("@Username", role.Username);
                    cmd.Parameters.AddWithValue("@Password", role.Password);
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();
                    cmd.ExecuteNonQuery();

                }
                con.Close();
            }
            return RedirectToAction("Role");




        }

        public ActionResult CreateBursarRole(Role role)
        {
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("insertbursarRole", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StaffID", role.StaffID);
                    cmd.Parameters.AddWithValue("@Username", role.Username);
                    cmd.Parameters.AddWithValue("@Password", role.Password);
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();
                    cmd.ExecuteNonQuery();

                }
                con.Close();
            }

            return RedirectToAction("Role");




        }

        public ActionResult AssignedClass()
        {

            List<AssignClass> assign = new List<AssignClass>();
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Select s.Firstname + '  ' + s.Lastname as Name, l.Levelname, a.Prefix from AssignClass a inner join Staff s on a.StaffID = s.StaffID inner join ClassLevel l on l.LevelID = a.ClassLevelID", con))
                {
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtProducts = new DataTable();

                    dtProducts.Load(sdr);

                    foreach (DataRow row in dtProducts.Rows)
                    {
                        assign.Add(
                            new AssignClass
                            {
                               
                                Staffname = row["Name"].ToString(),
                                ClassLevel = row["Levelname"].ToString(),
                                Prefix = row["Prefix"].ToString()
                                


                            }



                            );
                    }
                    con.Close();
                }
            }

            return View(assign);
        }

        public ActionResult AssignClass()
        {

            ViewBag.Staff = PopulateStaff();

            ViewBag.Level = PopulateClassLevel();

            return View();
        }

        public ActionResult AssignTeaherToClass(AssignClass assign)
        {
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("AssignClasses", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StaffID", assign.StaffID);
                    cmd.Parameters.AddWithValue("@ClasslevelID", assign.ClassLevelID);
                    cmd.Parameters.AddWithValue("@Prefix", assign.Prefix);
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();
                    cmd.ExecuteNonQuery();

                }
                con.Close();
            }

            return RedirectToAction("AssignedClass");



        }

        public ActionResult AssignedSubject()
        {

            List<AssignSubject> assign = new List<AssignSubject>();
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Select s.Firstname + '  ' + s.Lastname as Name,su.Subject, l.Levelname, a.Prefix from AssignSubject a inner join Staff s on a.StaffID = s.StaffID inner join ClassLevel l on l.LevelID = a.ClassLevelID inner join Subject su on su.SubjectID = a.SubjectID", con))
                {
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtProducts = new DataTable();

                    dtProducts.Load(sdr);

                    foreach (DataRow row in dtProducts.Rows)
                    {
                        assign.Add(
                            new AssignSubject
                            {

                                Staffname = row["Name"].ToString(),
                                sujectname = row["Subject"].ToString(),
                                level = row["Levelname"].ToString(),
                                prefix = row["Prefix"].ToString()
                                



                            }



                            );
                    }
                    con.Close();

                    return View(assign);
                }
            }
        }

        public ActionResult AssignSubjectToTeacher(AssignSubject subject)
        {
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("AssignSubjects", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StaffID", subject.StaffID);
                    cmd.Parameters.AddWithValue("@SubjectID", subject.subjectID);
                    cmd.Parameters.AddWithValue("@ClassLevelID", subject.ClassleveID);
                    cmd.Parameters.AddWithValue("@Prefix", subject.prefix);
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();
                    cmd.ExecuteNonQuery();

                }
                con.Close();

                return RedirectToAction("AssignedSubject");
            }
        }

        public ActionResult AssignSubject()
        {

            ViewBag.Staff = PopulateStaff();

            ViewBag.Level = PopulateClassLevel();

            ViewBag.Subject = PopulateSuject();

            return View();
        }

        private static List<MStaff> PopulateStaff()
        {
            List<MStaff> staff = new List<MStaff>();
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Select s.StaffID ,  s.Firstname + '  ' + s.Lastname as Name   from Staff s inner join Role on s.StaffID = Role.StaffID where Role = 'Teacher' ", con))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            staff.Add(
                            new MStaff
                            {

                                 Name= sdr["Name"].ToString(),
                                StaffID = Convert.ToInt32(sdr["StaffID"])
                                

                            }



                            );
                        }
                        con.Close();
                    }
                }

                return staff;
            }


        }

        private static List<ClassLevel> PopulateClassLevel()
        {
            List<ClassLevel> level = new List<ClassLevel>();
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Select * from ClassLevel ", con))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            level.Add(
                            new ClassLevel
                            {

                                Levelname = sdr["Levelname"].ToString(),
                                LevelID= Convert.ToInt32(sdr["LevelID"])


                            }



                            );
                        }
                        con.Close();
                    }
                }

                return level;
            }


        }

        private static List<Subject> PopulateSuject()
        {
            List<Subject> subject = new List<Subject>();
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Select * from Subject ", con))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            subject.Add(
                            new Subject
                            {

                                Subject1 = sdr["Subject"].ToString(),
                                SubjectID = Convert.ToInt32(sdr["SubjectID"])


                            }



                            );
                        }
                        con.Close();
                    }
                }

                return subject;
            }


        }

        public ActionResult StaffFinance()
        {
            ViewBag.Staff = PopulateStaff();

            return View();
        }

        public ActionResult EnterStaffFinance(StaffFinance finance)
        {

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("insertStaffFinance", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StaffID", finance.StaffID);
                    cmd.Parameters.AddWithValue("@BasicSalary", finance.BasicSalary);
                    cmd.Parameters.AddWithValue("@HRA", finance.HRA);
                    cmd.Parameters.AddWithValue("@HouseA", finance.HouseA);
                    cmd.Parameters.AddWithValue("@TransA", finance.TransA);
                    cmd.Parameters.AddWithValue("@Tax", finance.Tax);
                    cmd.Parameters.AddWithValue("@Vat", finance.Vat);
                    cmd.Parameters.AddWithValue("@LatenessFee", finance.latenessFee);
                    cmd.Parameters.AddWithValue("@Nhf", finance.NHF);
                    cmd.Parameters.AddWithValue("@TotalPay", finance.Total);
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();
                    cmd.ExecuteNonQuery();

                }
                con.Close();
                TempData["SuccessMessage"] = "Save Successfully";

                return RedirectToAction("StaffFinance");
            }

            
        }

        public ActionResult GetTotoalSalary(StaffFinance finance)
        {

            //finance.Total = (finance.BasicSalary) - ()

            return View("StaffFinance");
        }







    }

}