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
    public class TeacherClassController : Controller
    {
        // GET: TeacherClass
        public ActionResult Index()
        {
            string Username = (string)Session["Username"];
            List<TeacherDashboard> student = new List<TeacherDashboard>();
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Select * from StudentTble s inner join AssignClass a on a.ClassLevelID =  s.ClassLevel inner join Staff st on a.StaffID = st.StaffID inner join Role r on st.StaffID = r.StaffID where r.Username = '" + Username + "' ", con))
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
                                id = Convert.ToInt32(row["id"].ToString()),
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

        public ActionResult Present(TeacherDashboard dash, int id)
        {
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("insertAttendance", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@date", System.DateTime.Now);
                    cmd.Parameters.AddWithValue("@ID", dash.id);
                    cmd.Parameters.AddWithValue("@AttendanceValue", "Present");
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();
                    cmd.ExecuteNonQuery();





                }
                con.Close();
            }


            TempData["SuccessMessage"] = "Save Successfully";

            return RedirectToAction("Index");


        }

        public ActionResult Absent(TeacherDashboard dash, int id)
        {
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("insertAttendance", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@date", System.DateTime.Now);
                    cmd.Parameters.AddWithValue("@ID", dash.id);
                    cmd.Parameters.AddWithValue("@AttendanceValue", "Absent");
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();
                    cmd.ExecuteNonQuery();





                }
                con.Close();
            }


            TempData["SuccessMessage"] = "Save Successfully";

            return RedirectToAction("Index");


        }

        public ActionResult AddSubjectMark()
        {
            FillName();

            ViewBag.Session = PopulateSession();
            ViewBag.Level = PopulateClassLevel();

            ViewBag.Subject = PopulateSuject();


            return View();
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
                                LevelID = Convert.ToInt32(sdr["LevelID"])


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
                using (SqlCommand cmd = new SqlCommand("Select  * from Subject s inner join AssignClass a on s.ClassType = a.ClassLevelID ", con))
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

        private static List<Student> PopulateSession()
        {
            List<Student> student = new List<Student>();
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Select *  from TblSession", con))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            student.Add(
                            new Student
                            {

                                Session = sdr["Session"].ToString(),
                                SessionID = Convert.ToInt32(sdr["SessionID"])


                            }



                            );
                        }
                        con.Close();
                    }
                }

                return student;
            }


        }

        public void FillName()
        {
            string Username = (string)Session["Username"];
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Select s.ID ,  s.Firstname + '  ' + s.Lastname as Name   from StudentTble s inner join AssignClass a on  a.ClassLevelID = s.ClassLevel inner join Role r on r.StaffID = a.StaffID where r.Username = '" + Username + "' ", con))
                {
                    cmd.Parameters.AddWithValue("Username", Session["Username"].ToString());
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    List<SelectListItem> li = new List<SelectListItem>();
                    li.Add(new SelectListItem { Text = "Select" , Value="0"});
                    while (rdr.Read())
                    {
                        li.Add(new SelectListItem { Text = rdr[1].ToString(), Value =rdr[0].ToString() });
                    }
                    ViewData["Id"] = li;
                }
            }


        }

        public ActionResult InsertSubjectMark(SubjectMarks marks)
        {

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("insertSubMark", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sesssion", marks.Session);
                    cmd.Parameters.AddWithValue("@term", marks.Term);
                    cmd.Parameters.AddWithValue("@Subject", marks.Subject);
                    cmd.Parameters.AddWithValue("@SelectClass", marks.Class);
                    cmd.Parameters.AddWithValue("@StudentID", marks.StudentName);
                    cmd.Parameters.AddWithValue("@1stCA", marks.firstCa);
                    cmd.Parameters.AddWithValue("@2ndCA", marks.SecondCa);
                    cmd.Parameters.AddWithValue("@Exams", marks.Exam);
                    cmd.Parameters.AddWithValue("@Total", marks.Total);
                    cmd.Parameters.AddWithValue("@Grade", marks.Grade);
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();
                    cmd.ExecuteNonQuery();

                }
                con.Close();

                return RedirectToAction("AddSubjectMark");

            }
        }


        public ActionResult ViewSubjectMarks()
        {
            string Username = (string)Session["Username"];
            List<ListSubject> student = new List<ListSubject>();
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Select st.Firstname,st.Lastname,su.Subject,c.Levelname,tb.Session,s.Term,s.FirstCA,s.SecondCa,s.Exams,s.Total, s.Grade from SubjectMarks s inner join StudentTble st on s.Student = st.ID inner join Subject su on s.Subject = su.SubjectID inner join ClassLevel c on s.SelectClass = c.LevelID inner join AssignClass ac on ac.ClassLevelID = c.LevelID inner join Role r on r.StaffID = ac.StaffID inner Join TblSession tb on tb.SessionID = s.Session where r.Username = '" + Username + "' ORDER BY st.Firstname ", con))
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
                            new ListSubject
                            {
                                Firstname = row["Firstname"].ToString(),
                                Lastname = row["Lastname"].ToString(),
                                Subject = row["Subject"].ToString(),
                                Levelname = row["levelname"].ToString(),
                                FirstAss = Convert.ToInt32(row["FirstCA"].ToString()),
                                SecondAss = Convert.ToInt32( row["SecondCa"].ToString()),
                                Exams =Convert.ToInt32(row["Exams"].ToString()),
                                Total = Convert.ToInt32( row["Total"].ToString()),
                                Grade = row["Grade"].ToString(),
                                session = row["Session"].ToString(),
                                Term = row["Term"].ToString()

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