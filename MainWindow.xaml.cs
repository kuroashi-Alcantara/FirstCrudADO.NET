using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
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
using System.Configuration;
using System.Data;

namespace FirstCrudProduct
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            string myConnection = ConfigurationManager.ConnectionStrings["FirstCrudProduct.Properties.Settings.ProductsConnectionString"].ConnectionString;

            myConnectionSql = new SqlConnection(myConnection);

            ShowProduct();
            
        }

        SqlConnection myConnectionSql;


        private void ShowProduct()
        {
            try
            {
                string consult = "SELECT * FROM Products";

                SqlDataAdapter adapterSql = new SqlDataAdapter(consult,myConnectionSql);

                using(adapterSql)
                {
                    DataTable productTable = new DataTable();

                    adapterSql.Fill(productTable);

                    productList.DisplayMemberPath = "Name";
                    productList.SelectedValuePath = "ID";
                    productList.ItemsSource = productTable.DefaultView;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void ShowMoreDetails()
        {
            try
            {
                string consult = "SELECT Price, Amount FROM Products WHERE ID=@Selected";

                SqlCommand myComand = new SqlCommand(consult, myConnectionSql);

                SqlDataAdapter adapterSql = new SqlDataAdapter(myComand);

                using( adapterSql)
                {
                    myComand.Parameters.AddWithValue("@Selected", productList.SelectedValue);

                    DataTable productTable = new DataTable();

                    adapterSql.Fill(productTable);

                    showProductList.DisplayMemberPath = "Price";
                    showProductList2.DisplayMemberPath = "Amount";
                    

                    showProductList.ItemsSource = productTable.DefaultView;
                    showProductList2.ItemsSource = productTable.DefaultView;

                }


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }




        private void productList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowMoreDetails();
        }

        //agregar 
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
           addWindow addWindow = new addWindow();



            addWindow.ShowDialog();

            ShowProduct();
            showProductList.ItemsSource = null;
            showProductList2.ItemsSource = null;
        }

        //actualizar
        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            updateWindow updateWindow = new updateWindow((int)productList.SelectedValue);

            try
            {
                string consult = "SELECT Name,Price,Amount FROM Products WHERE ID=@p";

                SqlCommand myComand = new SqlCommand(consult,myConnectionSql);

                SqlDataAdapter adapterSql = new SqlDataAdapter(myComand);

                using(adapterSql)
                {
                    myComand.Parameters.AddWithValue("@p",productList.SelectedValue);

                    DataTable productTable = new DataTable();

                    adapterSql.Fill(productTable);

                    updateWindow.textUpdName.Text = productTable.Rows[0]["Name"].ToString();
                    updateWindow.textUpdPrice.Text = productTable.Rows[0]["Price"].ToString();
                    updateWindow.textUpdAmount.Text = productTable.Rows[0]["Amount"].ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            updateWindow.ShowDialog();

            ShowProduct();
           showProductList.ItemsSource = null; 
           showProductList2.ItemsSource = null;
        }

        //eliminar
        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            string consult = "DELETE FROM products WHERE ID=@p";

            SqlCommand myComand = new SqlCommand(consult,myConnectionSql);


            myConnectionSql.Open();

            myComand.Parameters.AddWithValue("@p", productList.SelectedValue);

            myComand.ExecuteNonQuery();

            myConnectionSql.Close();

            ShowProduct();

            showProductList.ItemsSource = null; 
            showProductList2.ItemsSource = null;
        }
    }
}
