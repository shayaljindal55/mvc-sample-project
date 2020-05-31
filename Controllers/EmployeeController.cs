using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Web.Mvc;
using Sample_Project.Models;

namespace Sample_Project.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            List<Employee> employeeList = new List<Employee>();
            string CS = ConfigurationManager.ConnectionStrings["Demo"].ConnectionString;
           
            using (SQLiteConnection con = new SQLiteConnection(CS))
            {
                
                var stm = "SELECT * FROM Employee";
                var command = new SQLiteCommand(stm, con);
                con.Open();
                try
                {
                    var rdr = command.ExecuteReader();
                   
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            var employee = new Employee();

                            employee.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
                            employee.Name = rdr["Name"].ToString();
                            employee.Gender = rdr["Gender"].ToString();
                            employee.Age = Convert.ToInt32(rdr["Age"]);
                            employee.Position = rdr["Position"].ToString();
                            employee.Office = rdr["Office"].ToString();
                            employee.Salary = Convert.ToInt32(rdr["Salary"]);
                            employeeList.Add(employee);
                        }
                    }
                     rdr.Close();
                    return View(employeeList);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private static string LoadConnectionString(string id = "Demo")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}