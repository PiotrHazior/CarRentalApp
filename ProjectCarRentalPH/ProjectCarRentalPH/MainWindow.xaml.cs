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
using static ProjectCarRentalPH.RegisterEmployee;

namespace ProjectCarRentalPH
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Pola
        private List<Customer> registeredCustomers;
        private List<Employee> registeredEmployees;
        private int loggedInCustomerId;
        #endregion

        #region Konstruktor
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region Obsługa zdarzeń
        /// <summary>
        /// Umożliwia przesuwanie konsoli poprzez nacisnięcie lewego przycisku myszki
        /// </summary>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        /// <summary>
        /// Przenosi do menu dla pracowników
        /// </summary>
        private void EmployeeMenu(object sender, RoutedEventArgs e)
        {
            EmployeeMenu objEmployeeMenu = new EmployeeMenu(registeredEmployees);
            this.Visibility = Visibility.Hidden;
            objEmployeeMenu.Show();
        }

        /// <summary>
        /// Przenosi do menu dla klientów
        /// </summary>
        private void CustomerMenu(object sender, RoutedEventArgs e)
        {
            CustomerMenu objCustomerMenu = new CustomerMenu(registeredCustomers, loggedInCustomerId);
            this.Visibility = Visibility.Hidden;
            objCustomerMenu.Show();
        }
        #endregion
    }
}
