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
using static ProjectCarRentalPH.RegisterEmployee;

namespace ProjectCarRentalPH
{
    /// <summary>
    /// Interaction logic for EmployeeMenu.xaml
    /// </summary>
    public partial class EmployeeMenu : Window
    {
        private List<Employee> registeredEmployees;
        public EmployeeMenu(List<Employee> registeredEmployees)
        {
            InitializeComponent();
            this.registeredEmployees = registeredEmployees;
            RefreshRegisteredEmployees();
        }

        // Warunek logowania klienta
        private void OpenManageCarsMenu(object sender, RoutedEventArgs e)
        {
            if (Pass2.Password != "" && LastNameEmpl.Text != "")
            {
                string employeeName = LastNameEmpl.Text.Trim();
                string password = Pass2.Password.Trim();

                bool loginSuccessful = false;

                if (registeredEmployees != null)
                {
                    foreach (Employee employee in registeredEmployees)
                    {
                        if (employee.LastName == employeeName && employee.Password == password)
                        {
                            loginSuccessful = true;
                            break;
                        }
                    }
                }
                if (loginSuccessful)
                {
                    MessageBox.Show("Login successful!");
                    ManageCarsMenu objManageCarsMenu = new ManageCarsMenu(registeredEmployees);
                    this.Visibility = Visibility.Hidden;
                    objManageCarsMenu.Show();
                }
                else
                {
                    MessageBox.Show("Invalid user ID or password!");
                }
            }
        }
  
        //private void ManageCarsMenu(object sender, RoutedEventArgs e)
        //{
        //    if (Pass2.Password != "" && LastNameEmpl.Text != "")
        //    {
        //        if (Pass2.Password == "test" && LastNameEmpl.Text == "user1")
        //        {
        //            MessageBox.Show("Login successful!");
        //            ManageCarsMenu objManageCarsMenu = new ManageCarsMenu();
        //            this.Visibility = Visibility.Hidden;
        //            objManageCarsMenu.Show();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Invalid user ID or password!");
        //        }
        //    }
        //}


        // Przenosi do poprzedniego okna (Main Windowsa)
        private void MainWindow(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            objMainWindow.Show();
        }

        private void RegisterEmployee(object sender, RoutedEventArgs e)
        {
            RegisterEmployee objRegisterEmployee = new RegisterEmployee();
            objRegisterEmployee.Closed += RegisterEmployee_Closed;
            this.Visibility = Visibility.Hidden;
            objRegisterEmployee.Show();
        }

        private void RegisterEmployee_Closed(object sender, EventArgs e)
        {
            RefreshRegisteredEmployees(); // Odświeżenie listy klientów po zamknięciu okna RegisterMenu
            this.Visibility = Visibility.Visible; // Ponowne wyświetlenie okna CustomerMenu
        }

        private void RefreshRegisteredEmployees()
        {
            try
            {
                registeredEmployees = new List<Employee>();
                registeredEmployees.Clear(); // Wyczyść listę

                using (SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\piotr\Desktop\GITHUB\ProjektSemestralnyPO\ProjectCarRentalPH\ProjectCarRentalPH\Database.mdf;Integrated Security=True"))
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
                           // PhoneNumb = reader["PhoneNumb"].ToString()
                        };
                        registeredEmployees.Add(employee);
                    }
                    Con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ManageCarsMenu(object sender, RoutedEventArgs e)
        {
            OpenManageCarsMenu(sender, e);
        }


        // Umożliwia przesuwanie konsoli poprzez nacisnięcie lewego przycisku myszki
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

    }
}
