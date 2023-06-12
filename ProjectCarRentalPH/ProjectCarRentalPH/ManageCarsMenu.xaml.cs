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

namespace ProjectCarRentalPH
{
    /// <summary>
    /// Interaction logic for ManageCarsMenu.xaml
    /// </summary>
    public partial class ManageCarsMenu : Window
    {
        public List<string> Brands {  get; set; }

        // Umożliwia przesuwanie konsoli poprzez nacisnięcie lewego przycisku myszki
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        public ManageCarsMenu()
        {
            InitializeComponent();
            populate();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\piotr\Desktop\GITHUB\ProjektSemestralnyPO\ProjectCarRentalPH\ProjectCarRentalPH\Database.mdf;Integrated Security=True");

        // Przenosi do poprzedniego okna (Employee Menu)
        private void EmployeeMenu(object sender, RoutedEventArgs e)
        {
            EmployeeMenu objEmployeeMenu = new EmployeeMenu();
            this.Visibility = Visibility.Hidden;
            objEmployeeMenu.Show();
        }



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


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }  

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        // Dodaje dane do bazy danych
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
    }
}
