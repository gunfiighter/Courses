using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFPERSONALTRACKING.DB;

namespace WPFPERSONALTRACKING
{
    /// <summary>
    /// Логика взаимодействия для TaskPage.xaml
    /// </summary>
    public partial class TaskPage : Window
    {
        public TaskPage()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        PERSONALTRACKINGContext db = new PERSONALTRACKINGContext();
        List<Employee> employees = new List<Employee>();
        List<Position> positions = new List<Position>();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            employees = db.Employees.OrderBy(x => x.Name).ToList();
            gridEmployee.ItemsSource = employees;
            cmbDepartment.ItemsSource = db.Departments.ToList();
            cmbDepartment.DisplayMemberPath = "DepartmentName";
            cmbDepartment.SelectedValuePath = "Id";
            cmbDepartment.SelectedIndex = -1;

            positions = db.Positions.ToList();
            cmbPosition.ItemsSource = db.Positions.ToList();
            cmbPosition.DisplayMemberPath = "PositionName";
            cmbPosition.SelectedValuePath = "Id";
            cmbPosition.SelectedIndex = -1;
        }

        int employeeId = 0;

        private void gridEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employee employee = (Employee)gridEmployee.SelectedItem;
            txtUserNumber.Text = employee.UserNumber.ToString();
            txtName.Text = employee.Name;
            txtSurname.Text = employee.Surname;
            employeeId = employee.Id;            
        }

        private void cmbDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int departmentId = Convert.ToInt32(cmbDepartment.SelectedValue);
            if (cmbDepartment.SelectedIndex != -1)
            {
                cmbPosition.ItemsSource = positions.Where(x => x.DepartmentId == departmentId).ToList();
                cmbPosition.DisplayMemberPath = "PositionName";
                cmbPosition.SelectedValuePath = "Id";
                cmbPosition.SelectedIndex = -1;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(employeeId == 0)
            {
                MessageBox.Show("Please select an employee from table.");
            }
            else if(txtTitle.Text.Trim() == "" || txtContent.Text.Trim() == "")
            {
                MessageBox.Show("Please fill the necessary fields");
            }
            else
            {
                DB.Task task = new DB.Task()
                {
                    EmployeeId = employeeId,
                    TaskStartDate = DateTime.Now,
                    TaskTitle = txtTitle.Text,
                    TaskContent = txtContent.Text,
                    TaskState = Definitions.TaskStates.OnEmployee
                };
                db.Tasks.Add(task);
                db.SaveChanges();
                MessageBox.Show("Task was added");
                txtContent.Clear();
                txtName.Clear();
                txtSurname.Clear();
                txtTitle.Clear();
                txtUserNumber.Clear();
            }
        }
    }
}
