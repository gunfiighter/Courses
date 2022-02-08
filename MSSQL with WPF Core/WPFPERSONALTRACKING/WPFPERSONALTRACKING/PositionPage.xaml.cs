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
using WPFPERSONALTRACKING.ViewModels;

namespace WPFPERSONALTRACKING
{
    /// <summary>
    /// Логика взаимодействия для PositionPage.xaml
    /// </summary>
    public partial class PositionPage : Window
    {
        PERSONALTRACKINGContext db = new PERSONALTRACKINGContext();
        public PositionModel model;
        public PositionPage()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var list = db.Departments.ToList().OrderBy(x => x.DepartmentName);
            cmbDepartment.ItemsSource = list;
            cmbDepartment.DisplayMemberPath = "DepartmentName";
            cmbDepartment.SelectedValuePath = "Id";
            cmbDepartment.SelectedIndex = -1;
            if(model != null && model.Id != 0)
            {
                cmbDepartment.SelectedValue = model.DepartmentId;
                txtPositionName.Text = model.PositionName;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(cmbDepartment.SelectedIndex == -1 || txtPositionName.Text.Trim() == "")
            {
                MessageBox.Show("Please fill all areas");
            }
            else
            {
                if (model != null && model.Id != 0)
                {
                    Position ps = new Position();
                    ps.DepartmentId = (int)cmbDepartment.SelectedValue;
                    ps.PositionName = txtPositionName.Text;
                    ps.Id = model.Id;
                    db.Positions.Update(ps);
                    db.SaveChanges();
                    MessageBox.Show("Position was updated");
                }
                else
                {
                    Position position = new Position();
                    position.PositionName = txtPositionName.Text;
                    position.DepartmentId = Convert.ToInt32(cmbDepartment.SelectedValue);
                    db.Positions.Add(position);
                    db.SaveChanges();
                    cmbDepartment.SelectedIndex = -1;
                    txtPositionName.Clear();
                    MessageBox.Show("Position was Added");
                }
                
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
