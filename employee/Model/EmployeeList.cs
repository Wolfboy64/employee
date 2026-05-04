using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace employee.Model
{
    public class EmployeeList : List<Employee>
    {
        public string[] ColumnNames;
    }
}
