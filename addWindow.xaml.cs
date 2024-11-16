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
            string consult = "INSERT INTO Products (Name,Price,Amount) VALUES (@name,@price,@amount)";

            SqlCommand myCommand = new SqlCommand(consult,myConnectionSql);

            myConnectionSql.Open();

            myCommand.Parameters.AddWithValue("@name", textAddName.Text);
            myCommand.Parameters.AddWithValue("@price", Convert.ToDecimal(textAddPrice.Text));
            myCommand.Parameters.AddWithValue("@amount", Convert.ToInt32(textAddAmount.Text));

            myCommand.ExecuteNonQuery();

            myConnectionSql.Close();

            this.Close();

        }
    }
}
