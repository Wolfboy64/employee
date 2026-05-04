using employee.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace employee.Logic
{
    internal class CSVHelper
    {
        internal static List<Employee> GetDataFromCSV(string path)
        {
            var ret = new EmployeeList();
            if (File.Exists(path)) 
            {
                using (StreamReader sr = new StreamReader(path)) 
                {
                    string header = sr.ReadLine();
                    ret.ColumnNames = header.Split(',');
                    while (!sr.EndOfStream)
                    {
                        string[] sorok = sr.ReadLine().Split(',');
                        var empl = new Employee(
                            firstname: sorok[0], 
                            lastname: sorok[1],
                            grossWage: sorok[2], 
                            netWage: sorok[3],
                            JobTitle: sorok[4], 
                            jobDepartment: sorok[5],
                            beginDate: sorok[6],
                            endDate: sorok[7].TrimEnd(';'));
                        ret.Add(empl);
                    }

                }
            }
            return ret;
        }
    }
}
