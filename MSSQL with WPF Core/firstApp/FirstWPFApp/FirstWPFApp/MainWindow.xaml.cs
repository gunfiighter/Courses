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

namespace FirstWPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void WinMain_Loaded(object sender, RoutedEventArgs e)
        {
            btnFirst.Content = "Changed";
            MessageBox.Show("FirstMessage");
        }

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            btnFirst.Content = "Second change";
            MessageBox.Show("Button Clicked");
            Button second = new Button();
            second.Content = "Second Button";
            second.Width = 100;
            second.Height = 60;
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(@"1.jpg", UriKind.RelativeOrAbsolute));
            second.Background = brush;
            second.Click += new RoutedEventHandler(second_clicked);
            grid.Children.Add(second);
        }

        private void second_clicked(object sender, RoutedEventArgs e)
        {
            WinMain.Title = "Title changed";

        }
    }
}
