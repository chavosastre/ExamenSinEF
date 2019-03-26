using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Datos.Models;


namespace ExamenCRUD.Controllers
{
    public class SchoolController : Controller
    {
        string connectionString = @"Data Source = SASTRE-LENOVO; Initial Catalog = SistemaPrueba; Integrated Security = True";
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dtEscuelas = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("Select * from Escuela", sqlCon);
                sqlDa.Fill(dtEscuelas);
            }
            return View(dtEscuelas);
        }

        // GET: School/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: School/Create
        public ActionResult Create()
        {
            return View( new SchoolModel());
        }

        // POST: School/Create
        [HttpPost]
        public ActionResult Create(SchoolModel schoolModel)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "Insert into Escuela Values(@Clave,@Nombre,@Direccion)";
                SqlCommand sqlCom = new SqlCommand(query, sqlCon);
                sqlCom.Parameters.AddWithValue("@Clave", schoolModel.Clave);
                sqlCom.Parameters.AddWithValue("@Nombre", schoolModel.Nombre);
                sqlCom.Parameters.AddWithValue("@Direccion", schoolModel.Direccion);
                sqlCom.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: School/Edit/5
        public ActionResult Edit(string id)
        {
            id = id.Replace(" ", "");
            SchoolModel schoolModel = new SchoolModel();
            DataTable dtSchool = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "Select * from Escuela where clave = @Clave";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@Clave",id);
                sqlDa.Fill(dtSchool);
            }
            if (dtSchool.Rows.Count == 1)
            {
                schoolModel.Clave = dtSchool.Rows[0][0].ToString();
                schoolModel.Nombre = dtSchool.Rows[0][1].ToString();
                schoolModel.Direccion = dtSchool.Rows[0][2].ToString();

                return View(schoolModel);
            }
            return RedirectToAction("Index");
        }

        // POST: School/Edit/5
        [HttpPost]
        public ActionResult Edit(SchoolModel schoolModel)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "Update Escuela set Nombre = @Nombre, Direccion = @Direccion where Clave = @Clave";
                SqlCommand sqlCom = new SqlCommand(query, sqlCon);
                sqlCom.Parameters.AddWithValue("@Clave", schoolModel.Clave);
                sqlCom.Parameters.AddWithValue("@Nombre", schoolModel.Nombre);
                sqlCom.Parameters.AddWithValue("@Direccion", schoolModel.Direccion);
                sqlCom.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: School/Delete/5
        public ActionResult Delete(string id)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "Delete from Escuela where Clave = @Clave";
                SqlCommand sqlCom = new SqlCommand(query, sqlCon);
                sqlCom.Parameters.AddWithValue("@Clave", id);
                sqlCom.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }
    }
}
