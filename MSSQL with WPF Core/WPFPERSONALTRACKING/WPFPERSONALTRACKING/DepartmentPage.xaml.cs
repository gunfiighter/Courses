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
    /// Логика взаимодействия для Department.xaml
    /// </summary>
    public partial class DepartmentPage : Window
    {
        public Department department;
        public DepartmentPage()
        {
            InitializeComponent();
        }       

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(txtDepartmentName.Text.Trim() == "")
            {
                MessageBox.Show("Please fill the Department Name area");
               
            }
            else
            {
                using(PERSONALTRACKINGContext db = new PERSONALTRACKINGContext())
                {
                    if(department != null && department.Id != 0)
                    {
                        Department dpt = new Department();
                        dpt.DepartmentName = txtDepartmentName.Text;
                        dpt.Id = department.Id;
                        db.Departments.Update(dpt);
                        db.SaveChanges();
                        MessageBox.Show("Update was successful");
                    }
                    else
                    {
                        Department dpt = new Department();
                        dpt.DepartmentName = txtDepartmentName.Text;
                        db.Departments.Add(dpt);
                        db.SaveChanges();
                        txtDepartmentName.Clear();
                        MessageBox.Show("Department was Added");
                    }

                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(department != null && department.Id != 0)
            {
                txtDepartmentName.Text = department.DepartmentName;
            }
        }
    }
}
