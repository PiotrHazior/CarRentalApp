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

namespace ProjectCarRentalPH
{
    /// <summary>
    /// Interaction logic for EmployeeMenu.xaml
    /// </summary>
    public partial class EmployeeMenu : Window
    {
        public EmployeeMenu()
        {
            InitializeComponent();
        }

        private void ManageCarsMenu(object sender, RoutedEventArgs e)
        {
            if (Pass2.Password != "" && ID_Employee.Text != "")
            {
                if (Pass2.Password == "test" && ID_Employee.Text == "user1")
                {
                    MessageBox.Show("Login successful!");
                    ManageCarsMenu objManageCarsMenu = new ManageCarsMenu();
                    this.Visibility = Visibility.Hidden;
                    objManageCarsMenu.Show();
                }
                else
                {
                    MessageBox.Show("Invalid user ID or password!");
                }
            }
        }

        // Przenosi do poprzedniego okna (Main Windowsa)
        private void MainWindow(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            objMainWindow.Show();
        }
    }
}
