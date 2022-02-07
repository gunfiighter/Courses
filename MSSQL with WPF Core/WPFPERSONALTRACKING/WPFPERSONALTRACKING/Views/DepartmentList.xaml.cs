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
            using(PERSONALTRACKINGContext db = new PERSONALTRACKINGContext())
            {
                List<Department> departments = db.Departments.ToList();
                gridDepartment.ItemsSource = departments;
            }            
        }
    }
}
