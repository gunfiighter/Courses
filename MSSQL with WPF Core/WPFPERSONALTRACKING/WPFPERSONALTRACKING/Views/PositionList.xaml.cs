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
    /// Логика взаимодействия для PositionList.xaml
    /// </summary>
    public partial class PositionList : UserControl
    {
        PERSONALTRACKINGContext db = new PERSONALTRACKINGContext();
        public PositionList()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshPositions();
        }

        private void RefreshPositions()
        {
            var list = db.Positions.Include(x => x.Department).Select(x => new
            {
                positionId = x.Id,
                PositionName = x.PositionName,
                departmentId = x.DepartmentId,
                DepartmentName = x.Department.DepartmentName
            }).OrderBy(x => x.PositionName).ToList();
            List<PositionModel> positionsList = new List<PositionModel>();
            foreach (var item in list)
            {
                PositionModel model = new PositionModel();
                model.Id = item.positionId;
                model.PositionName = item.PositionName;
                model.DepartmentName = item.DepartmentName;
                model.DepartmentId = item.departmentId;
                positionsList.Add(model);
            }
            gridPosition.ItemsSource = positionsList;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            PositionPage positionPage = new PositionPage();
            positionPage.ShowDialog();
            RefreshPositions();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            PositionModel positionModel = gridPosition.SelectedItem as PositionModel;
            if(positionModel != null && positionModel.Id != 0)
            {
                PositionPage page = new PositionPage();
                page.model = positionModel;
                page.ShowDialog();
                RefreshPositions();
            }
            
        }
    }
}
