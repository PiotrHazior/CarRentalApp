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
using static ProjectCarRentalPH.RegisterEmployee;

namespace ProjectCarRentalPH
{
    /// <summary>
    /// Interaction logic for ManageCarsMenu.xaml
    /// </summary>
    public partial class ManageCarsMenu : Window
    {
        #region Pola
        public List<string> Brands {  get; set; }
        private List<Employee> registeredEmployees;
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\piotr\Desktop\GITHUB\ProjektSemestralnyPO\ProjectCarRentalPH\ProjectCarRentalPH\Database.mdf;Integrated Security=True");
        #endregion

        #region Konstruktor
        public ManageCarsMenu(List<Employee> registeredEmployees)
        {
            InitializeComponent();
            populate();
            this.registeredEmployees = registeredEmployees;
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
        /// Przenosi do poprzedniego okna (Employee Menu)
        /// </summary>
        private void EmployeeMenu(object sender, RoutedEventArgs e)
        {
            EmployeeMenu objEmployeeMenu = new EmployeeMenu(registeredEmployees);
            this.Visibility = Visibility.Hidden;
            objEmployeeMenu.Show();
        }

        /// <summary>
        /// Pobiera dane z tabeli SportCars
        /// </summary>
        private void populate()
        {
            Con.Open();
            string query = "select * from SportCars";
            SqlDataAdapter adapter = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            datagrid.ItemsSource = ds.Tables[0].DefaultView;

            Brands = ds.Tables[0].AsEnumerable().Select(row => row.Field<string>("Brand")).ToList();

            Con.Close();
        }


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) { } 

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) { }

        /// <summary>
        /// Dodaje dane do bazy danych
        /// </summary>
        private void Button_Add(object sender, RoutedEventArgs e)
        {
            if (Brand.Text == "" || Model.Text == "" || Price.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into SportCars values(@Brand, @Model, @Price)";
                    SqlCommand cmdInsert = new SqlCommand(query, Con);
                    cmdInsert.Parameters.AddWithValue("@Brand", Brand.Text);
                    cmdInsert.Parameters.AddWithValue("@Model", Model.Text);
                    cmdInsert.Parameters.AddWithValue("@Price", Price.Text);
                    cmdInsert.ExecuteNonQuery();
                    MessageBox.Show("Car Successfully Added!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Con.Close();
                    populate();
                }
            }

        }

        /// <summary>
        /// Usuwa dane z bazy danych
        /// </summary>
        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            if (ID_SportCar.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from SportCars where ID_SportCar=" + ID_SportCar.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Model deleted successfully!");
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
        /// Przycisk INFO - pomaga użytkownikowi
        /// </summary>
        private void INFOM(object sender, RoutedEventArgs e)
        {
            string message = "Aby dodać auto do systemu należy podać Brand, Model, Price oraz kliknąć w przycisk ADD.\n"
                             + "Natomiast aby usunąć auto z systemu należy podać tylko ID tego samochodu i kliknąć w przycisk DELETE.";

            MessageBox.Show(message, "Informacja");
        }
        #endregion
    }
}
