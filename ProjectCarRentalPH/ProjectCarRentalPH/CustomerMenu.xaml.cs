using Microsoft.Data.SqlClient;
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
        #region Pola
        private List<Customer> registeredCustomers;
        private int loggedInCustomerId;
        #endregion

        #region Konstruktor
        public CustomerMenu(List<Customer> registeredCustomers, int loggedInCustomerId)
        {
            InitializeComponent();
            this.registeredCustomers = registeredCustomers;
            this.loggedInCustomerId = loggedInCustomerId;
            RefreshRegisteredCustomers();
        }
        #endregion

        #region Obsługa zdarzeń
        /// <summary>
        /// Logowanie klienta
        /// </summary>
        private void RentalMenu(object sender, RoutedEventArgs e)
        {
            if (Pass.Password != "" && LastName.Text != "")
            {
                string customerName = LastName.Text.Trim();
                string password = Pass.Password.Trim();

                bool loginSuccessful = false;

                if (registeredCustomers != null)
                {
                    using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\piotr\Desktop\GITHUB\ProjektSemestralnyPO\ProjectCarRentalPH\ProjectCarRentalPH\Database.mdf;Integrated Security=True"))
                    {
                        con.Open();
                        string query = "SELECT * FROM Customers WHERE LastName = @LastName AND Password = @Password";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@LastName", customerName);
                        cmd.Parameters.AddWithValue("@Password", password);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            loggedInCustomerId = Convert.ToInt32(reader["ID_Customer"]);
                            loginSuccessful = true;
                        }
                        con.Close();
                    }
                }
                if (loginSuccessful)
                {
                    MessageBox.Show("Login successful!");
                    RentalMenu objRentalMenu = new RentalMenu(registeredCustomers, loggedInCustomerId);
                    this.Visibility = Visibility.Hidden;
                    objRentalMenu.Show();
                }
                else
                {
                    MessageBox.Show("Invalid user ID or password!");
                }
            }
        }


        /// <summary>
        /// Przenosi do poprzedniego okna (Main Windowsa)
        /// </summary>
        private void MainWindow(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            objMainWindow.Show();
        }

        /// <summary>
        /// Przenosi do okna RegisterMenu
        /// </summary>
        private void RegisterMenu(object sender, RoutedEventArgs e)
        {
            RegisterMenu objRegisterMenu = new RegisterMenu();
            objRegisterMenu.Closed += RegisterMenu_Closed;
            this.Visibility = Visibility.Hidden;
            objRegisterMenu.Show();
        }

        private void RegisterMenu_Closed(object sender, EventArgs e)
        {
            RefreshRegisteredCustomers(); 
            this.Visibility = Visibility.Visible; 
        }

        /// <summary>
        /// Odświeża listę klientów
        /// </summary>
        private void RefreshRegisteredCustomers()
        {
            try
            {
                registeredCustomers = new List<Customer>();
                registeredCustomers.Clear(); 

                using (SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\piotr\Desktop\GITHUB\ProjektSemestralnyPO\ProjectCarRentalPH\ProjectCarRentalPH\Database.mdf;Integrated Security=True"))
                {
                    Con.Open();
                    string query = "SELECT * FROM Customers";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Customer customer = new Customer
                        {
                            
                            LastName = reader["LastName"].ToString(),
                            Password = reader["Password"].ToString(),
                            Phone = reader["Phone"].ToString()
                        };
                        registeredCustomers.Add(customer);
                    }
                    Con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

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
        #endregion
    }
}
