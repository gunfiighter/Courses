using Microsoft.EntityFrameworkCore;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFPERSONALTRACKING.DB;
using WPFPERSONALTRACKING.ViewModels;

namespace WPFPERSONALTRACKING.Views
{
    /// <summary>
    /// Логика взаимодействия для TaskList.xaml
    /// </summary>
    public partial class TaskList : UserControl
    {
        public TaskList()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            TaskPage page = new TaskPage();
            page.ShowDialog();
            FillDataGrid();
        }

        PERSONALTRACKINGContext db = new PERSONALTRACKINGContext();
        List<TaskDetailModel> taskList = new List<TaskDetailModel>();
        List<TaskDetailModel> searchList = new List<TaskDetailModel>();
        List<Employee> employees = new List<Employee>();
        List<Position> positions = new List<Position>();

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }

        private void FillDataGrid()
        {
            taskList = db.Tasks.Include(x => x.TaskStateNavigation).Include(x => x.Employee)
                .ThenInclude(x => x.Department).ThenInclude(x => x.Positions).Select(x => new TaskDetailModel()
                {
                    Id = x.Id,
                    EmployeeId = x.EmployeeId,
                    Name = x.Employee.Name,
                    StateName = x.TaskStateNavigation.StateName,
                    Surname = x.Employee.Surname,
                    TaskContent = x.TaskContent,
                    TaskDeliveryDate = x.TaskDeliveryDate,
                    TaskStartDate = (DateTime)x.TaskStartDate,                    
                    TaskTitle = x.TaskTitle,
                    UserNumber = x.Employee.UserNumber,
                    PositionId = x.Employee.PositionId,
                    DepartmentId = x.Employee.DepartmentId,
                }).ToList();
            gridTask.ItemsSource = taskList;
            searchList = taskList;

            cmbDepartment.ItemsSource = db.Departments.ToList();
            cmbDepartment.DisplayMemberPath = "DepartmentName";
            cmbDepartment.SelectedValuePath = "Id";
            cmbDepartment.SelectedIndex = -1;

            positions = db.Positions.ToList();
            cmbPosition.ItemsSource = db.Positions.ToList();
            cmbPosition.DisplayMemberPath = "PositionName";
            cmbPosition.SelectedValuePath = "Id";
            cmbPosition.SelectedIndex = -1;

            List<Taskstate> taskStates = db.Taskstates.ToList();
            cmbState.ItemsSource = taskStates;
            cmbState.DisplayMemberPath = "NameState";
            cmbState.SelectedValuePath = "Id";
            cmbState.SelectedIndex = -1;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            List<TaskDetailModel> search = searchList;
            if(txtUserNumber.Text.Trim() != "")
            {
                search = search.Where(x => x.UserNumber == Convert.ToInt32(txtUserNumber.Text)).ToList();
            }
            if(txtName.Text.Trim() != string.Empty)
            {
                search = search.Where(x => x.Name == txtName.Text.Trim()).ToList();
            }
            if (txtSurname.Text.Trim() != string.Empty)
            {
                search = search.Where(x => x.Surname == txtSurname.Text.Trim()).ToList();
            }
            if (cmbDepartment.SelectedIndex != -1)
            {
                search = search.Where(x => x.DepartmentId == Convert.ToInt32(cmbDepartment.SelectedValue)).ToList();
            }
            if (cmbPosition.SelectedIndex != -1)
            {
                search = search.Where(x => x.PositionId == Convert.ToInt32(cmbPosition.SelectedValue)).ToList();
            }
            if (cmbState.SelectedIndex != -1)
            {
                search = search.Where(x => x.TaskState == Convert.ToInt32(cmbState.SelectedValue)).ToList();
            }
            if (rbStart.IsChecked == true)
            {
                search = search.Where(x => x.TaskStartDate > dpStart.SelectedDate && x.TaskStartDate < dpDelivery.SelectedDate).ToList();
            }
            if (rbDelivery.IsChecked == true)
            {
                search = search.Where(x => x.TaskDeliveryDate > dpStart.SelectedDate && x.TaskDeliveryDate < dpDelivery.SelectedDate).ToList();
            }
            gridTask.ItemsSource = search;
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

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtName.Clear();
            txtSurname.Clear();
            txtUserNumber.Clear();
            dpDelivery.SelectedDate = DateTime.Today;
            dpStart.SelectedDate = DateTime.Today;
            cmbDepartment.SelectedIndex = -1;
            cmbState.SelectedIndex = -1;
            cmbPosition.ItemsSource = positions;
            cmbPosition.SelectedIndex = -1;
            rbDelivery.IsChecked = false;
            rbStart.IsChecked = false;
            gridTask.ItemsSource = taskList;
        }

        TaskDetailModel model = new TaskDetailModel();

        private void gridTask_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            model = (TaskDetailModel)gridTask.SelectedItem;
        }
        
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            TaskPage page = new TaskPage();
            page.Model = model;
            page.ShowDialog();
            FillDataGrid();
        }
    }
}
