using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : Window
    {
        PERSONALTRACKINGContext db = new PERSONALTRACKINGContext();
        List<Position> positions = new List<Position>();
        OpenFileDialog dialog = new OpenFileDialog();
        public EmployeeDetailModel Model;
        public EmployeePage()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
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

            if(Model != null && Model.Id != 0)
            {
                cmbDepartment.SelectedValue = Model.DepartmentId;
                cmbPosition.SelectedValue = Model.PositionId;
                txtUserNo.Text = Model.UserNo.ToString();
                txtPassword.Text = Model.Password.ToString();
                txtName.Text = Model.Name.ToString();
                txtSurname.Text = Model.Surname.ToString();
                txtSalary.Text = Model.Salary.ToString();
                txtAdress.AppendText(Model.Adress.ToString());
                picker1.SelectedDate = Model.BirthDay;
                chIsAdmin.IsChecked = Model.IsAdmin;
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(@"Images//" + Model.ImagePath, UriKind.RelativeOrAbsolute);
                image.EndInit();
                EmployeeImage.Source = image;
            }
        }

        private void cmbDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int departmentId = Convert.ToInt32(cmbDepartment.SelectedValue);
            if(cmbDepartment.SelectedIndex != -1)
            {
                cmbPosition.ItemsSource = positions.Where(x => x.DepartmentId == departmentId).ToList();
                cmbPosition.DisplayMemberPath = "PositionName";
                cmbPosition.SelectedValuePath = "Id";
                cmbPosition.SelectedIndex = -1;
            }
        }

        private void btnChoose_Click(object sender, RoutedEventArgs e)
        {
            if(dialog.ShowDialog() == true)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(dialog.FileName);
                image.EndInit();
                EmployeeImage.Source = image;
                txtImage.Text = dialog.FileName;
            }
        }

        private void txtUserNo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(txtUserNo.Text.Trim() == "" || txtPassword.Text.Trim() == "" || txtName.Text.Trim() == "" ||
                txtSurname.Text.Trim() == "" || txtSalary.Text.Trim() == "" || cmbDepartment.SelectedIndex == -1 ||
                cmbPosition.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill the necessary areas");
            }
            else
            {
                if(Model != null && Model.Id != 0)
                {
                    Employee employee = db.Employees.Find(Model.Id);
                    List<Employee> employeeList = db.Employees.Where(x => x.UserNumber == Convert.ToInt32(txtUserNo.Text) &&
                        x.Id != employee.Id).ToList();
                    if (employeeList.Count > 0)
                    {
                        MessageBox.Show("This User Number is already used by another Employee");
                    }
                    else
                    {
                        if (txtImage.Text.Trim() != "")
                        {
                            if (File.Exists(@"Images//" + employee.ImagePath))
                            {
                                File.Delete(@"Images//" + employee.ImagePath);
                                string filename = "";
                                string uniqueName = Guid.NewGuid().ToString();
                                filename += uniqueName + System.IO.Path.GetFileName(txtImage.Text);
                                File.Copy(txtImage.Text, @"Images//" + filename);
                                employee.ImagePath = filename;
                            }
                        }

                        employee.UserNumber = Convert.ToInt32(txtUserNo.Text);
                        employee.Password = txtPassword.Text;
                        employee.Name = txtName.Text;
                        employee.Surname = txtSurname.Text;
                        employee.Salary = Convert.ToInt32(txtSalary.Text);
                        employee.DepartmentId = Convert.ToInt32(cmbDepartment.SelectedValue);
                        employee.PositionId = Convert.ToInt32(cmbPosition.SelectedValue);
                        TextRange text = new TextRange(txtAdress.Document.ContentStart, txtAdress.Document.ContentEnd);
                        employee.Adress = text.Text;
                        employee.BirthDay = picker1.SelectedDate;
                        employee.IsAdmin = (bool)chIsAdmin.IsChecked;

                        db.SaveChanges();
                        MessageBox.Show("Employee Updated Successfully");
                    }
                }
                else
                {
                    var uniqueList = db.Employees.Where(x => x.UserNumber == Convert.ToInt32(txtUserNo.Text)).ToList();                    
                    
                    

                    if (uniqueList.Count > 0)
                    {
                        MessageBox.Show("This UserNumber is already have been taken");
                    }
                    else
                    {
                        Employee employee = new Employee();
                        employee.UserNumber = Convert.ToInt32(txtUserNo.Text);
                        employee.Password = txtPassword.Text;
                        employee.Name = txtName.Text;
                        employee.Surname = txtSurname.Text;
                        employee.Salary = Convert.ToInt32(txtSalary.Text);
                        employee.DepartmentId = Convert.ToInt32(cmbDepartment.SelectedValue);
                        employee.PositionId = Convert.ToInt32(cmbPosition.SelectedValue);
                        TextRange text = new TextRange(txtAdress.Document.ContentStart, txtAdress.Document.ContentEnd);
                        employee.Adress = text.Text;
                        employee.BirthDay = picker1.SelectedDate;
                        employee.IsAdmin = (bool)chIsAdmin.IsChecked;
                        string fileName = "";
                        string unique = Guid.NewGuid().ToString();
                        fileName += unique + dialog.SafeFileName;
                        employee.ImagePath = fileName;
                        db.Employees.Add(employee);
                        db.SaveChanges();
                        if(File.Exists(txtImage.Text))
                        {
                            File.Copy(txtImage.Text, @"Images//" + fileName);
                        }
                        
                        MessageBox.Show("Employee was Added");
                        txtUserNo.Clear();
                        txtPassword.Clear();
                        txtName.Clear();
                        txtImage.Clear();
                        txtSurname.Clear();
                        txtSalary.Clear();
                        picker1.SelectedDate = DateTime.Now;
                        cmbDepartment.SelectedIndex = -1;
                        cmbPosition.ItemsSource = positions;
                        cmbPosition.SelectedIndex = -1;
                        txtAdress.Document.Blocks.Clear();
                        chIsAdmin.IsChecked = false;
                        EmployeeImage.Source = new BitmapImage();
                        txtImage.Clear();
                    }
                }               
            }
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            bool isUnique = false;
            var uniqueList = db.Employees.Where(x => x.UserNumber == Convert.ToInt32(txtUserNo.Text)).ToList();
            if(uniqueList.Count > 0)
            {
                MessageBox.Show("This UserNumber is already have been taken");
            }
            else
            {
                MessageBox.Show("This UserNumber is availiable");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
