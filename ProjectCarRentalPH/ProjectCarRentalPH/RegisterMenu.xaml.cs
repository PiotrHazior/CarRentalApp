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
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;


namespace ProjectCarRentalPH
{
    /// <summary>
    /// Interaction logic for RegisterMenu.xaml
    /// </summary>
    public partial class RegisterMenu : Window
    {
        #region Pola
        private List<Customer> registeredCustomers;
        private int loggedInCustomerId;
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\piotr\Desktop\GITHUB\ProjektSemestralnyPO\ProjectCarRentalPH\ProjectCarRentalPH\Database.mdf;Integrated Security=True");
        #endregion

        #region Konstruktor
        public RegisterMenu()
        {
            InitializeComponent();
            registeredCustomers = new List<Customer>();
        }
        #endregion

        #region Obsługa zdarzeń
        /// <summary>
        /// Przenosi do poprzedniego okna CustomerMenu
        /// </summary>
        private void CustomerMenu(object sender, RoutedEventArgs e)
        {
            CustomerMenu objCustomerMenu = new CustomerMenu(registeredCustomers, loggedInCustomerId);
            this.Visibility = Visibility.Hidden;
            objCustomerMenu.Show();           
        }

        /// <summary>
        /// Pobiera dane z tabeli Customers
        /// </summary>
        private void populate()
        {
            Con.Open();
            string query = "select * from Customers";
            SqlDataAdapter adapter = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            CustDGV.ItemsSource = ds.Tables[0].DefaultView;
            Con.Close();
        }

        /// <summary>
        /// Rejestracja klienta
        /// </summary>
        private void Button_Confirm(object sender, RoutedEventArgs e)
        {
            if (LastName.Text == "" || Phone.Text == "" || Pass2.Password == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into Customers values(@LastName, @Phone, @Password)";
                    SqlCommand cmdInsert = new SqlCommand(query, Con);
                    cmdInsert.Parameters.AddWithValue("@LastName", LastName.Text);
                    cmdInsert.Parameters.AddWithValue("@Phone", Phone.Text);
                    cmdInsert.Parameters.AddWithValue("@Password", Pass2.Password);
                    cmdInsert.ExecuteNonQuery();
                    Customer customer = new Customer
                    {
                        LastName = LastName.Text,
                        Password = Pass2.Password,
                        Phone = Phone.Text
                    };
                    registeredCustomers.Add(customer);
                    MessageBox.Show("Registered Successfully!");
                    Con.Close();
                    populate();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// Wczytuje dane do listy
        /// </summary>
        private void Customers_Load(object sender, EventArgs e)
        {
            populate();
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) { }

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
    public class Customer
    {
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public int ID { get; internal set; }
    }
}
