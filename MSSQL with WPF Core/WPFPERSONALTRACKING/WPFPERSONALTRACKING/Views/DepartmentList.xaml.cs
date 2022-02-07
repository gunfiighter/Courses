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

namespace WPFPERSONALTRACKING.Views
{
    /// <summary>
    /// Логика взаимодействия для DepartmentList.xaml
    /// </summary>
    public partial class DepartmentList : UserControl
    {
        public DepartmentList()
        {
            InitializeComponent();
            RefreshDepartments();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DepartmentPage page = new DepartmentPage();
            //this.Visibility = Visibility.Collapsed;
            page.ShowDialog();
            RefreshDepartments();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Department dpt = (Department)gridDepartment.SelectedItem;
            DepartmentPage page = new DepartmentPage();
            page.department = dpt;
            page.ShowDialog();
            RefreshDepartments();
        }

        private void RefreshDepartments()
        {
            using (PERSONALTRACKINGContext db = new PERSONALTRACKINGContext())
            {
                List<Department> departments = db.Departments.OrderBy(x => x.DepartmentName).ToList();
                gridDepartment.ItemsSource = departments;
            }
        }
    }
}
