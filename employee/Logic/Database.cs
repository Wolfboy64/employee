using employee.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace employee.Logic
{
    internal class Database
    {
        public static bool SaveEmployees(List<Employee> employees)
        {
           
            bool ret = false;
            string constr = "server=localhost;user=root;database=hrapp;port=3306";

            try
            {
                using (var connection = new MySqlConnection(constr))
                {
                    connection.Open();
                    string sqldelete1 = "DELETE FROM `employment`";
                    MySqlCommand cmdDelete1 = new MySqlCommand(sqldelete1, connection);
                    cmdDelete1.ExecuteNonQuery();

                    string sqldelete2 = "DELETE FROM `employee`";
                    MySqlCommand cmdDelete2 = new MySqlCommand(sqldelete2, connection);
                    cmdDelete2.ExecuteNonQuery();

                    string sqldelete4 = "DELETE FROM `job_title`";
                    MySqlCommand cmdDelete4 = new MySqlCommand(sqldelete4, connection);
                    cmdDelete4.ExecuteNonQuery();

                    string sqldelete3 = "DELETE FROM `department`";
                    MySqlCommand cmdDelete3 = new MySqlCommand(sqldelete3, connection);
                    cmdDelete3.ExecuteNonQuery();

                    var departments = employees.Select(e => e.Department).Distinct().ToList();
                    foreach (var department in departments)
                    {
                        string cmdtext = "INSERT INTO `department`(`name`) VALUES (@name)";
                        MySqlCommand cmd = new MySqlCommand(cmdtext, connection);
                        cmd.Parameters.AddWithValue("@name", department);
                        cmd.ExecuteNonQuery();
                    }
                    var jobtitles = employees.Select(e => e.JobTitle).Distinct().ToList();
                    foreach (var jobtitle in jobtitles)
                    {
                        string department = employees.Where(e => e.JobTitle == jobtitle).Select(e => e.Department).FirstOrDefault();
                        string cmdDepartment = "SELECT `department_id` FROM `department` WHERE `name` = @name";
                        MySqlCommand departmentCmd = new MySqlCommand(cmdDepartment, connection);
                        departmentCmd.Parameters.AddWithValue("@name", department);
                        var departmentId = departmentCmd.ExecuteScalar();


                        string cmdtext = "INSERT INTO `job_title`(`title`, `department_id`) VALUES (@name, @departmentid)";
                        MySqlCommand cmd = new MySqlCommand(cmdtext, connection);
                        cmd.Parameters.AddWithValue("@name", jobtitle);
                        cmd.Parameters.AddWithValue("@departmentid", departmentId);
                        cmd.ExecuteNonQuery();
                    }

                    foreach (var employee in employees)
                    {
                        string cmdtext = "INSERT INTO `employee`(`first_name`, `last_name`) VALUES (@firstname, @lastname )";
                        MySqlCommand cmd = new MySqlCommand(cmdtext, connection);
                        cmd.Parameters.AddWithValue("@firstname", employee.FirstName);
                        cmd.Parameters.AddWithValue("@lastname", employee.LastName);
                        var employeeId = cmd.ExecuteScalar();

                        string departmentIdCmdText = "SELECT `department_id` FROM `department` WHERE `name` = @name";
                        MySqlCommand departmentIdCmd = new MySqlCommand(departmentIdCmdText, connection);
                        departmentIdCmd.Parameters.AddWithValue("@name", employee.Department);
                        var departmentId = departmentIdCmd.ExecuteScalar();

                        string jobTitleIdCmdText = "SELECT `job_title_id` FROM `job_title` WHERE `title` = @name";
                        MySqlCommand jobtitle = new MySqlCommand(jobTitleIdCmdText, connection);
                        jobtitle.Parameters.AddWithValue("@name", employee.JobTitle);
                        var jobTitleId = jobtitle.ExecuteScalar();

                        string cmdtext2 = "INSERT INTO `employment`(`employee_id`, `department_id`, `job_title_id`, `grosswage`, `netwage`, `begindate`, `enddate`) VALUES (@employeeid, @departmentid, @jobtitleid, @grosswage, @netwage, @begindate, @enddate)";
                        MySqlCommand cmd2 = new MySqlCommand(cmdtext2, connection);
                        cmd2.Parameters.AddWithValue("@employeeid", employeeId);
                    }
                }
                ret = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ret = false;
            }
            return ret;
        }

    }
}
