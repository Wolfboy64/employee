using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using employee.Logic;
using employee.Model;


namespace employeeGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<Employee> employeesOriginal = new List<Employee>();
        List<Employee> employees = new List<Employee>();
        private void Form1_Load(object sender, EventArgs e)
        {
            employeesOriginal = Database.LoadEmployees();



            BindingSource bs = new BindingSource();
            employees = Database.LoadEmployees();
            BindingList<Employee> bList = new BindingList<Employee>(employees);
            bs.DataSource = bList;
            dataGridView1.DataSource = bs;

            dataGridView2.DataSource = bs;

        }
        Dictionary<int, Employee> dict = new Dictionary<int, Employee>();
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // TODO: Összes adatot frissítem a Database.FelSyncEmps() metódusban, de csak akkor, ha a dict-ben 5-nél több elem van. Ez egyfajta batch update lesz.
            var emp_ = dataGridView1.Rows[e.RowIndex].DataBoundItem as Employee;
            int x = emp_.Id;
            if (dict.TryGetValue(x, out Employee emp))
            {
                dict[x] = emp_;
            }
            else
            {
                dict.Add(x, emp_);
            }
            if (dict.Count >= 5)
            {
                dict.Clear();
            }
            MessageBox.Show($"{emp_}"); //Tostring lefut!
        }
    }
}
