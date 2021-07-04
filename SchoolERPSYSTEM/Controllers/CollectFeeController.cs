using SchoolERPSYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using SchoolERPSYSTEM.Config;
using System.IO;

namespace SchoolERPSYSTEM.Controllers
{
    public class CollectFeeController : Controller
    {
     
           
        // GET: CollectFee
        public ActionResult Index()
        {
            List<Student> student = new List<Student>();
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select ID, AdmissionNo,Firstname,Lastname from StudentTble", con))
                {
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtProducts = new DataTable();

                    dtProducts.Load(sdr);

                    foreach (DataRow row in dtProducts.Rows)
                    {
                        student.Add(
                            new Student
                            {
                                id = Convert.ToInt32(row["ID"].ToString()),
                                AdmissionNo = row["AdmissionNo"].ToString(),
                                Firstname = row["Firstname"].ToString(),
                                Lastname = row["Lastname"].ToString()

                            }



                            );
                    }
                    con.Close();
                }
            }

            return View(student);

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

        public ActionResult Collectfee(int id)
        {
            ViewBag.Level = PopulateClassLevel();
            ViewBag.Session = PopulateSession();

            CollectFee fee = new CollectFee();

            DataTable dtfee = new DataTable();
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                con.Open();
                string query = "Select * from StudentTble Where ID = @ID";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, con);
                sqlDa.SelectCommand.Parameters.AddWithValue("@ID", id);
                sqlDa.Fill(dtfee);



            }

            if (dtfee.Rows.Count == 1)
            {

               // fee.id = Convert.ToInt32(dtfee.Rows[0][0].ToString());
                fee.AdmissionNo = dtfee.Rows[0][1].ToString();
                fee.FirstName = dtfee.Rows[0][2].ToString();
                fee.Lastname = dtfee.Rows[0][4].ToString();
                return View(fee);


            }
            else




                return View("index");



        }

        public ActionResult SubmitFee(CollectFee fee)
        {

            string filename = Path.GetFileNameWithoutExtension(fee.PdfFile.FileName);
            string extension = Path.GetExtension(fee.PdfFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            fee.teller = "~/Fee/" + filename;
            filename = Path.Combine(Server.MapPath("~/Fee/"), filename);
            fee.PdfFile.SaveAs(filename);

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("InsertFee", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Firstname", fee.FirstName);
                    cmd.Parameters.AddWithValue("@Lastname", fee.Lastname);
                    cmd.Parameters.AddWithValue("@AdmissionNo", fee.AdmissionNo);
                    cmd.Parameters.AddWithValue("@Class", fee.Class);
                    cmd.Parameters.AddWithValue("@Session", fee.SessionName);
                    cmd.Parameters.AddWithValue("@Amount", fee.amount);
                    cmd.Parameters.AddWithValue("@Teller", fee.teller);
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();
                    cmd.ExecuteNonQuery();

                }
                con.Close();





            }

            TempData["SuccessMessage"] = "Save Successfully";

            return RedirectToAction("Index");
        }


        public ActionResult Search(string session,string level)
        {
            List<CollectFee> fee = ViewFeesCollected("GetResultPay", session, level);

            return View("ViewFeeCollected", fee);
        }



        public List<CollectFee> ViewFeesCollected(string StoredProcedure,string session,string level)
        {
            List<CollectFee> fee = new List<CollectFee>();

            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedure, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    

                        cmd.Parameters.AddWithValue("@Session", session );
                        cmd.Parameters.AddWithValue("@Level", level);

                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtProducts = new DataTable();

                    dtProducts.Load(sdr);

                    foreach (DataRow row in dtProducts.Rows)
                    {
                        fee.Add(
                            new CollectFee
                            {
                                AdmissionNo = row["AdmissionNo"].ToString(),
                                FirstName = row["Firstname"].ToString(),
                                Lastname = row["Lastname"].ToString(),
                                Class = row["Levelname"].ToString(),
                                Session = row["Session"].ToString(),
                                amount = Convert.ToInt32(row["Amount"].ToString())

                            }



                            );
                    }
                }
            }

            return fee;
        }


        public ActionResult ViewFeeCollected()
        {
           
            List<CollectFee> fee = new List<CollectFee>();
            using (SqlConnection con = new SqlConnection(StoreConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("select p.Firstname,p.Lastname,p.AdmissionNo,t.Session,l.Levelname,p.Amount from PaymentSlip p inner join TblSession t on  p.Session = t.SessionID inner join ClassLevel l on l.LevelID = p.Class", con))
                {
                    if (con.State != System.Data.ConnectionState.Open)

                        con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    DataTable dtProducts = new DataTable();

                    dtProducts.Load(sdr);

                    foreach (DataRow row in dtProducts.Rows)
                    {
                        fee.Add(
                            new CollectFee
                            {
                               
                                AdmissionNo = row["AdmissionNo"].ToString(),
                                FirstName = row["Firstname"].ToString(),
                                Lastname = row["Lastname"].ToString(),
                                Class = row["Levelname"].ToString(),
                                Session = row["Session"].ToString(),
                                amount = Convert.ToInt32(row["Amount"].ToString())


                                

                            }



                            );
                    }
                    con.Close();
                }
            }

            return View(fee);


            
        }






    }
}