using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using SchoolERPSYSTEM.Models;

namespace SchoolERPSYSTEM.Controllers
{
    public class ProductController : Controller
    {
        string ConnectionString = "Data Source=DESKTOP-J3DHBNP\\;Initial Catalog=SchoolErpSystem;Integrated Security=True";
        // GET: Product
        [HttpGet]
        public ActionResult Index()
        {

            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(ConnectionString))
            {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("Select * from Product", sqlcon);

                sqlDa.Fill(dtblProduct);
            }
                return View(dtblProduct);
        }

       
        // GET: Product/Create
        public ActionResult Create()
        {
            return View(new Product());


        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product collection)
        {
            using (SqlConnection sqlcon = new SqlConnection(ConnectionString))
            {
                sqlcon.Open();
                string Query = "Insert into Product(ProductName,Price,Quantity) Values(@ProductName,@Price,@Quantity)";
                SqlCommand cmd = new SqlCommand(Query,sqlcon);
                cmd.Parameters.AddWithValue("@ProductName", collection.Productname);
                cmd.Parameters.AddWithValue("@Price", collection.price);
                cmd.Parameters.AddWithValue("@Quantity", collection.quantity);
                cmd.ExecuteNonQuery();
            }

                return RedirectToAction("Index");
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            Product product = new Product();

            DataTable dtproduct = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(ConnectionString))
            {
                sqlcon.Open();
                string query = "Select * from Product Where ProductID = @ProductID";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query,sqlcon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@ProductID", id);
                sqlDa.Fill(dtproduct);



            }
            if (dtproduct.Rows.Count == 1)
            {
                product.ID = Convert.ToInt32(dtproduct.Rows[0][0].ToString());
                product.Productname = dtproduct.Rows[0][1].ToString();
                product.price = Convert.ToDouble(dtproduct.Rows[0][1].ToString());
                product.quantity = Convert.ToInt32(dtproduct.Rows[0][2].ToString());
                return View(product);


            }
            else
            
               return RedirectToAction("Index");

            

        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
