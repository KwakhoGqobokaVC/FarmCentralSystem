using Microsoft.AspNetCore.Mvc;
using FARMCENTRALPROTOTYPE.Models;
using System.Data.SqlClient;
using FARMCENTRALPROTOTYPE.Data;

namespace FARMCENTRALPROTOTYPE.Controllers
{
    public class LoginController : Controller
    {
        DBFarmCentral dbFC;
       

        private readonly string connectionString;
        public LoginController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("localDBConnect");
            dbFC = new DBFarmCentral(configuration);
        }

        // GET: Login
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Check if the login credentials exist in the FARMER table
                    string farmerQuery = "SELECT COUNT(*) FROM FARMERS WHERE FARMER_EMAIL = @Username AND FARMER_PASSWORD = @Password";
                    using (SqlCommand farmerCommand = new SqlCommand(farmerQuery, connection))
                    {
                        farmerCommand.Parameters.AddWithValue("@Username", loginModel.Username);
                        farmerCommand.Parameters.AddWithValue("@Password", loginModel.Password);
                        int farmerCount = (int)farmerCommand.ExecuteScalar();
                        if (farmerCount > 0)
                        {// Farmer login successful, set farmer ID in session
                            string farmerID = GetFarmerIDFromEmail(loginModel.Username);
                            HttpContext.Session.SetString("FARMER_ID", farmerID);

                            // Redirect to farmer's dashboard
                            return RedirectToAction("Index", "Product");
                        }
                    }

                    // Check if the login credentials exist in the EMPLOYEE table
                    string employeeQuery = "SELECT COUNT(*) FROM EMPLOYEES WHERE EMPLOYEE_EMAIL = @Username AND EMPLOYEE_PASSWORD = @Password";
                    using (SqlCommand employeeCommand = new SqlCommand(employeeQuery, connection))
                    {
                        employeeCommand.Parameters.AddWithValue("@Username", loginModel.Username);
                        employeeCommand.Parameters.AddWithValue("@Password", loginModel.Password);
                        int employeeCount = (int)employeeCommand.ExecuteScalar();

                        if (employeeCount > 0)
                        {
                            // Employee login successful, redirect to employee's dashboard
                            return RedirectToAction("Index", "Farmer");
                        }
                    }

                    // Invalid login, show error message
                    ViewBag.ErrorMessage = "Invalid email or password";
                    return View();
                }
            }

            return View();
        }
        private string GetFarmerIDFromEmail(string username)
        {
            string farmerID = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT FARMER_ID FROM FARMERS WHERE FARMER_EMAIL = @Username";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    farmerID = (string)command.ExecuteScalar();
                }
            }

            return farmerID;
        }
    }
        }

    

