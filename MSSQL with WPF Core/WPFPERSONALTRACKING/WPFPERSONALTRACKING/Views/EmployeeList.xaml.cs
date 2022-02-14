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
    /// Логика взаимодействия для EmployeeList.xaml
    /// </summary>
    public partial class EmployeeList : UserControl
    {
        PERSONALTRACKINGContext db = new PERSONALTRACKINGContext();
        List<Position> positions = new List<Position>();
        List<EmployeeDetailModel> list = new List<EmployeeDetailModel>();
        public EmployeeList()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            EmployeePage page = new EmployeePage();
            page.ShowDialog();
            FillDatagrid();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillDatagrid();
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            List<EmployeeDetailModel> searchList = list;
            if(txtUserNo.Text.Trim() != "")
            {
                searchList = searchList.Where(x => x.UserNo == Convert.ToInt32(txtUserNo.Text)).ToList();
            }

            if(txtName.Text.Trim() != "")
            {
                searchList = searchList.Where(x => x.Name.Contains(txtName.Text)).ToList();
            }

            if(txtSurname.Text.Trim() != "")
            {
                searchList = searchList.Where(x => x.Surname.Contains(txtSurname.Text)).ToList();
            }

            if(cmbDepartment.SelectedIndex != -1)
            {
                searchList = searchList.Where(x => x.DepartmentId == Convert.ToInt32(cmbDepartment.SelectedValue)).ToList();
            }

            gridEmployee.ItemsSource = searchList;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtName.Clear();
            txtSurname.Clear();
            txtUserNo.Clear();
            cmbDepartment.SelectedIndex = -1;
            cmbDepartment.ItemsSource = positions;
            cmbPosition.SelectedIndex = -1;
            gridEmployee.ItemsSource = list;
        }

        void FillDatagrid()
        {
            cmbDepartment.ItemsSource = db.Departments.ToList();
            cmbDepartment.DisplayMemberPath = "DepartmentName";
            cmbDepartment.SelectedValuePath = "Id";
            cmbDepartment.SelectedIndex = -1;

            positions = db.Positions.ToList();
            cmbPosition.ItemsSource = db.Positions.ToList();
            cmbPosition.DisplayMemberPath = "PositionName";
            cmbPosition.SelectedValuePath = "Id";
            cmbPosition.SelectedIndex = -1;

            list = db.Employees.Include(x => x.Position).Include(x => x.Department).Select(x => new EmployeeDetailModel()
            {
                Id = x.Id,
                Name = x.Name,
                Adress = x.Adress,
                BirthDay = (DateTime)x.BirthDay,
                DepartmentId = x.DepartmentId,
                DepartmentName = x.Department.DepartmentName,
                IsAdmin = (bool)x.IsAdmin,
                Password = x.Password,
                PositionId = x.PositionId,
                PositionName = x.Position.PositionName,
                Salary = x.Salary,
                Surname = x.Surname,
                UserNo = x.UserNumber,
                ImagePath = x.ImagePath,
            }).ToList();
            gridEmployee.ItemsSource = list;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            EmployeeDetailModel model = (EmployeeDetailModel)gridEmployee.SelectedItem;
            EmployeePage page = new EmployeePage();
            page.Model = model;
            page.ShowDialog();
            FillDatagrid();
        }
    }
}
