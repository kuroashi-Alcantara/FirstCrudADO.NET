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

namespace FirstCrudProduct
{
    /// <summary>
    /// Lógica de interacción para updateWindow.xaml
    /// </summary>
    public partial class updateWindow : Window
    {
        private int z;

        public updateWindow(int z)
        {
            InitializeComponent();

            string myConnection = ConfigurationManager.ConnectionStrings["FirstCrudProduct.Properties.Settings.ProductsConnectionString"].ConnectionString;

            myConnectionSql = new SqlConnection(myConnection);

            this.z = z;
        }

        SqlConnection myConnectionSql;

        private void updWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string consult = "UPDATE Products SET Name=@name, Price=@price, Amount=@amount WHERE ID=" + z;

                SqlCommand myComand = new SqlCommand(consult, myConnectionSql);

                myConnectionSql.Open();

                if (textUpdName.Text != null && textUpdName.Text != "")
                {
                    myComand.Parameters.AddWithValue("@name", textUpdName.Text);
                }
                else
                {
                    throw new Exception("Nombre no puede estar vacio");
                }

                myComand.Parameters.AddWithValue("@price", textUpdPrice.Text);
                myComand.Parameters.AddWithValue("@amount", textUpdAmount.Text);

                myComand.ExecuteNonQuery();

                

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                myConnectionSql.Close();
            }
        }
    }
}
