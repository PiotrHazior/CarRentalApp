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
    /// <summary>
    /// Interaction logic for RegisterEmployee.xaml
    /// </summary>
    public partial class RegisterEmployee : Window
    {
        private List<Employee> registeredEmployees;
        public RegisterEmployee()
        {
            InitializeComponent();
            registeredEmployees = new List<Employee>();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\piotr\Desktop\GITHUB\ProjektSemestralnyPO\ProjectCarRentalPH\ProjectCarRentalPH\Database.mdf;Integrated Security=True");

        private void EmployeeMenu(object sender, RoutedEventArgs e)
        {
            EmployeeMenu objEmployeeMenu = new EmployeeMenu(registeredEmployees);
            this.Visibility = Visibility.Hidden;
            objEmployeeMenu.Show();
        }

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

        private void Employees_Load(object sender, EventArgs e)
        {
            populate2();

            // Wczytaj dane z bazy do listy registeredCustomers
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

        // Umożliwia przesuwanie konsoli poprzez nacisnięcie lewego przycisku myszki
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        public class Employee
        {
            public string LastName { get; set; }
            public string Password { get; set; }
            public string PhoneNumb { get; set; }
        }

        private void EmployeeDGV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
