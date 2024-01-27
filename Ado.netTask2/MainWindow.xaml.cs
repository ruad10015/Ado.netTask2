using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows;
namespace Ado.netTask2;
public partial class MainWindow : Window
{
    SqlConnection ? connectionString = null;
    SqlCommand ? command = null;
    SqlDataAdapter ? adapter = null;
    DataTable ? dataTable = null;

    public MainWindow()
    {
        InitializeComponent();
        connectionString = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Library;Integrated Security=True;");
        dataTable = new DataTable();    
        adapter.Fill(dataTable);
        dataGridView.ItemsSource = dataTable.DefaultView;
    }



    private void txtBox_GotFocus(object sender, RoutedEventArgs e)
    {   
        if(txtBox.Text=="Search By Name") txtBox.Text = string.Empty;
    }

    private void searchBtn_Click(object sender, RoutedEventArgs e)
    {
        SqlParameter parameter = new SqlParameter() {
            ParameterName = "@item",
            SqlDbType = SqlDbType.NVarChar,
            Value = txtBox.Text
        };
        command = new SqlCommand("SELECT * FROM Authors AS A WHERE A.FirstName=@item", connectionString);
        command.Parameters.Add(parameter);
        adapter = new SqlDataAdapter(command);
        dataTable = new DataTable();
        adapter.Fill(dataTable);
        dataGridView.ItemsSource = dataTable.DefaultView;
    }

    private void updateBtn_Click(object sender, RoutedEventArgs e)
    {
        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
        if (dataTable is not null)
        {
            adapter?.Update(dataTable);
        } 
    }
}