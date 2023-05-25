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
    /// Interaction logic for CustomerMenu.xaml
    /// </summary>
    public partial class CustomerMenu : Window
    {
        public CustomerMenu()
        {
            InitializeComponent();
        }

        // Warunek logowania klienta
        private void RentalMenu(object sender, RoutedEventArgs e)
        {
            if (Pass.Password != "" && ID_Customer.Text != "")
            {
                if (Pass.Password == "test" && ID_Customer.Text == "user1")
                {
                    MessageBox.Show("Login successful!");
                    RentalMenu objRentalMenu = new RentalMenu();
                    this.Visibility = Visibility.Hidden;
                    objRentalMenu.Show();
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
