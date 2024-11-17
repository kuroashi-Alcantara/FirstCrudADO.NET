using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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

//ventana para agregar
namespace FirstCrudProduct
{
    /// <summary>
    /// Lógica de interacción para addWindow.xaml
    /// </summary>
    public partial class addWindow : Window
    {

        

        public addWindow()
        {
            InitializeComponent();

            string myConnection = ConfigurationManager.ConnectionStrings["FirstCrudProduct.Properties.Settings.ProductsConnectionString"].ConnectionString;

            myConnectionSql = new SqlConnection(myConnection);

            
        }
        SqlConnection myConnectionSql;

        private void addWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                    string consult = "INSERT INTO Products (Name,Price,Amount) VALUES (@name,@price,@amount)";

                SqlCommand myCommand = new SqlCommand(consult, myConnectionSql);

                myConnectionSql.Open();


                if (!int.TryParse(textAddName.Text, out int name))
                {
                    myCommand.Parameters.AddWithValue("@name", textAddName.Text);
                }
                else
                {
                    throw new FormatException("Nombre con formato incorrecto");
                }

                if (!decimal.TryParse(textAddPrice.Text, out decimal price))
                {
                    
                    throw new FormatException("Precio con formato incorrecto");
                }
                else
                {
                    myCommand.Parameters.AddWithValue("@price", Convert.ToDecimal(textAddPrice.Text));
                }

                if (int.TryParse(textAddAmount.Text, out int amount))
                {
                    myCommand.Parameters.AddWithValue("@amount", Convert.ToInt32(textAddAmount.Text));
                }
                else
                {
                    throw new FormatException("Cantidad con formato incorrecto");
                }

                myCommand.ExecuteNonQuery();

                myConnectionSql.Close();

                this.Close();
                
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Formato incorrecto o los campos estan vacios, Error: " + ex.Message);
            }  
            /*catch(Exception ex)
            {
                MessageBox.Show("los campos estan vacios, Error: " + ex.Message);

            }*/
            finally
            {
                myConnectionSql.Close();
            }



        }
    }
}
