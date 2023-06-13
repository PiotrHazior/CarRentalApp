using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class RegisterEmployee : Window
    {
        #region Pola
        private List<Employee> registeredEmployees;
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\piotr\Desktop\GITHUB\ProjektSemestralnyPO\ProjectCarRentalPH\ProjectCarRentalPH\Database.mdf;Integrated Security=True");
        #endregion

        #region Konstruktor
        public RegisterEmployee()
        {
            InitializeComponent();
            registeredEmployees = new List<Employee>();
        }
        #endregion

        #region Obsługa zdarzeń
        /// <summary>
        /// Przenosi do okna EmployeeMenu
        /// </summary>
        private void EmployeeMenu(object sender, RoutedEventArgs e)
        {
            EmployeeMenu objEmployeeMenu = new EmployeeMenu(registeredEmployees);
            this.Visibility = Visibility.Hidden;
            objEmployeeMenu.Show();
        }

        /// <summary>
        /// Pobiera dane z tabeli Employees
        /// </summary>
        private void populate2()
        {
            Con.Open();
            string query = "select * from Employees";
            SqlDataAdapter adapter = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            EmployeeDGV.ItemsSource = ds.Tables[0].DefaultView;
            Con.Close();
        }

        /// <summary>
        /// Rejestracja pracownika
        /// </summary>
        private void Button_Confirm2(object sender, RoutedEventArgs e)
        {
            if (LastName2.Text == "" || Phone2.Text == "" || Pass3.Password == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into Employees values(@LastName, @PhoneNumb, @Password)";
                    SqlCommand cmdInsert = new SqlCommand(query, Con);
                    cmdInsert.Parameters.AddWithValue("@LastName", LastName2.Text);
                    cmdInsert.Parameters.AddWithValue("@PhoneNumb", Phone2.Text);
                    cmdInsert.Parameters.AddWithValue("@Password", Pass3.Password);
                    cmdInsert.ExecuteNonQuery();
                    Employee employee = new Employee
                    {
                        LastName = LastName2.Text,
                        Password = Pass3.Password,
                        PhoneNumb = Phone2.Text
                    };
                    registeredEmployees.Add(employee);
                    MessageBox.Show("Registered Successfully!");
                    Con.Close();
                    populate2();
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
        private void Employees_Load(object sender, EventArgs e)
        {
            populate2();
            try
            {
                Con.Open();
                string query = "SELECT * FROM Employees";
                SqlCommand cmd = new SqlCommand(query, Con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Employee employee = new Employee
                    {
                        LastName = reader["LastName"].ToString(),
                        Password = reader["Password"].ToString(),
                        PhoneNumb = reader["Phone"].ToString()
                    };
                    registeredEmployees.Add(employee);
                }
                Con.Close();
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

        private void EmployeeDGV_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
        #endregion

        #region Klasy pomocnicze
        public class Employee
        {
            public string LastName { get; set; }
            public string Password { get; set; }
            public string PhoneNumb { get; set; }
        }
        #endregion
     
    }
}
