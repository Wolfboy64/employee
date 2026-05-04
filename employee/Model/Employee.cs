using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace employee.Model
{
    internal class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal GrossWage { get; set; }
        public decimal NetWage { get; set; }
        public string JobTitle { get; set; }
        public string Department { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        public Employee(string firstname, string lastname, decimal grossWage, decimal netWage, string JobTitle, string jobDepartment, DateTime beginDate, DateTime endDate)
        {
            this.FirstName = firstname;
            this.LastName = lastname;
            this.GrossWage = grossWage;
            this.NetWage = netWage;
            this.JobTitle = JobTitle;
            this.Department = jobDepartment;
            this.BeginDate = beginDate;
            this.EndDate = endDate;
        }

        public Employee(string firstname, string lastname, string grossWage, string netWage, string JobTitle, string jobDepartment, string beginDate, string endDate)
        {
            this.FirstName = firstname;
            this.LastName = lastname;
            decimal gWage;
            if (decimal.TryParse(grossWage, out gWage))
            {
                this.GrossWage = gWage;
            }
            decimal nWage;
            if (decimal.TryParse(netWage, out nWage))
            {
                this.NetWage = nWage;
            }
            this.JobTitle = JobTitle;
            this.Department = jobDepartment;
            DateTime bDate;
            if (DateTime.TryParse(beginDate, out bDate))
            {
                this.BeginDate = bDate;
            }
            DateTime eDate;
            if (DateTime.TryParse(endDate, out eDate))
            {
                this.EndDate = eDate;
            }
        }
        public override string ToString()
        {
            return $"{FirstName} | {LastName} | {GrossWage} | {NetWage} | {JobTitle} | {Department} | {BeginDate.ToString("yyyy-MM-dd")} |{EndDate.ToString("yyyy-MM-dd")}";
        }
    }
}
