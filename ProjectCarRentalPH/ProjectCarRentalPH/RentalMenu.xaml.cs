using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
using Microsoft.Data.SqlClient;

namespace ProjectCarRentalPH
{
    /// <summary>
    /// Interaction logic for RentalMenu.xaml
    /// </summary>
    public partial class RentalMenu : Window
    {
        private List<Customer> registeredCustomers;
        private DataRowView selectedRow;

        public RentalMenu(List<Customer> registeredCustomers)
        {
            InitializeComponent();
            this.registeredCustomers = registeredCustomers;
            LoadDataGrid();
            //carsRent();
            //UpdateBrandComboBox();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\piotr\Desktop\GITHUB\ProjektSemestralnyPO\ProjectCarRentalPH\ProjectCarRentalPH\Database.mdf;Integrated Security=True");


        // Przenosi do poprzedniego okna (Customer Menu)
        private void CustomerMenu(object sender, RoutedEventArgs e)
        {
            CustomerMenu objCustomerMenu = new CustomerMenu(registeredCustomers);
            this.Visibility = Visibility.Hidden;
            objCustomerMenu.Show();
        }
        private void LoadDataGrid()
        {
            try
            {
                Con.Open();
                string query = "SELECT * FROM SportCars";
                SqlDataAdapter adapter = new SqlDataAdapter(query, Con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                RentalDGV.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }

        private void Button_Add3(object sender, RoutedEventArgs e)
        {
            if (selectedRow == null || RentalDate.SelectedDate == null || DateOfReturn.SelectedDate == null)
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    string query = "INSERT INTO RentalCar (RentalDate, DateOfReturn, ID_Customer, ID_SportCar) VALUES (@RentalDate, @DateOfReturn, @ID_Customer, @ID_SportCar)";
                    using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\piotr\Desktop\GITHUB\ProjektSemestralnyPO\ProjectCarRentalPH\ProjectCarRentalPH\Database.mdf;Integrated Security=True"))
                    {
                        con.Open();

                        int sportCarId = Convert.ToInt32(selectedRow["ID_SportCar"]);
                        int customerId = GetCustomerId(con);

                        using (SqlCommand cmdInsert = new SqlCommand(query, con))
                        {
                            cmdInsert.Parameters.AddWithValue("@RentalDate", RentalDate.SelectedDate.Value);
                            cmdInsert.Parameters.AddWithValue("@DateOfReturn", DateOfReturn.SelectedDate.Value);
                            cmdInsert.Parameters.AddWithValue("@ID_Customer", customerId);
                            cmdInsert.Parameters.AddWithValue("@ID_SportCar", sportCarId);

                            cmdInsert.ExecuteNonQuery();
                            MessageBox.Show("Car rented. Have a nice ride :) ");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }



        private int GetCustomerId(SqlConnection con)
        {
            int customerId = 0;

            try
            {
                string query = "INSERT INTO Customers DEFAULT VALUES; SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    customerId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return customerId;
        }



        private void Window_Closed(object sender, EventArgs e)
        {
            Con.Close();
        }



        private void Car_Load(object sender, RoutedEventArgs e)
        {
            LoadDataGrid();
            //carsRent();
        }   

        // Umożliwia przesuwanie konsoli poprzez nacisnięcie lewego przycisku myszki
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Button_Delete3(object sender, RoutedEventArgs e)
        {

        }



        //private DataRowView selectedRow; // Dodaj pole do przechowywania wybranego wiersza

        private void RentalDGV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RentalDGV.SelectedItem != null)
            {
                selectedRow = RentalDGV.SelectedItem as DataRowView;
            }
        }


    }
}
