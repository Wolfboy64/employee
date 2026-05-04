using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace employee.Model
{
    internal class EmployeeList : List<Employee>
    {
        public string[] ColumnNames;
    }
}
