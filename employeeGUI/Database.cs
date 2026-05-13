using employee.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace employee.Logic
{
    public class Database
    {
        public static string constr = "server=localhost;user=root;database=hrapp;port=3306";
        public static bool SaveEmployees(List<Employee> employees)
        {

            bool ret = false;
            

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
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "SELECT LAST_INSERT_ID();";
                        var employeeId = Convert.ToInt32(cmd.ExecuteScalar());


                        string jobTitleIdCmdText = "SELECT `job_title_id` FROM `job_title` WHERE `title` = @name";
                        MySqlCommand jobtitle = new MySqlCommand(jobTitleIdCmdText, connection);
                        jobtitle.Parameters.AddWithValue("@name", employee.JobTitle);
                        var jobTitleId = jobtitle.ExecuteScalar();


                        string cmdtext2 = "INSERT INTO `employment`(`employee_id`, `job_title_id`, `gross_wage`, `net_wage`, `begin_date`, `end_date`) VALUES (@employeeid, @jobtitleid, @grosswage, @netwage, @begindate, @enddate)";
                        MySqlCommand cmd2 = new MySqlCommand(cmdtext2, connection);
                        cmd2.Parameters.AddWithValue("@employeeid", employeeId);
                        cmd2.Parameters.AddWithValue("@jobtitleid", jobTitleId);
                        cmd2.Parameters.AddWithValue("@grosswage", employee.GrossWage);
                        cmd2.Parameters.AddWithValue("@netwage", employee.NetWage);
                        cmd2.Parameters.AddWithValue("@begindate", employee.BeginDate);
                        cmd2.Parameters.AddWithValue("@enddate", employee.EndDate);
                        cmd2.ExecuteNonQuery();
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
        public static List<Employee> LoadEmployees()
        {
            List<Employee> ret = new List<Employee>();


            try
            {
                using (MySqlConnection conn = new MySqlConnection(constr))
                {
                    conn.Open();
                    string command = "SELECT * FROM employment_full;";
                    MySqlCommand cmd = new MySqlCommand(command, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        
                        Employee employee = new Employee
                        (
                            id: reader.GetInt32("employee_id"),
                            firstname: reader.GetString("first_name"),
                            lastname: reader.GetString("last_name"),
                            grossWage: reader.GetDecimal("gross_wage"),
                            netWage: reader.GetDecimal("net_wage"),
                            JobTitle: reader.GetString("title"),
                            jobDepartment: reader.GetString("name"),
                            beginDate: reader.GetDateTime("begin_date"),
                            endDate: reader.IsDBNull(reader.GetOrdinal("end_date")) ? (DateTime?)null : reader.GetDateTime("end_date")
                        );
                            
                        ret.Add(employee);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return ret;
        }
        public static bool FelSyncEmps()
        { 
            bool ret = false;
            
            using (var connection = new MySqlConnection(constr))
            {
                try
                {
                    connection.Open();
                    string cmdtext = "UDATE";
                    MySqlCommand cmd = new MySqlCommand(cmdtext, connection);
                    cmd.ExecuteNonQuery();
                    ret = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    ret = false;
                }
            }

            return ret; 
        }
    }
}
