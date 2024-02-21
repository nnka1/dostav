using Npgsql;

namespace dostav
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {

            string connectionString = "Server=localhost;Port=5432;Database=dostavka;User ID=postgres;Password=123";
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);

            string логин_админа = usernameTextBox.Text;
            string пароль_админа = passwordTextBox.Text;

            string query = "SELECT * FROM администратор WHERE логин_админа = @логин_админа AND пароль_админа = @пароль_админа";

            using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@логин_админа", логин_админа);
                cmd.Parameters.AddWithValue("@пароль_админа", пароль_админа);

                try
                {
                    connection.Open();
                    NpgsqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        string fullname = reader["логин_админа"].ToString();


                        this.Hide();
                        menu menu = new menu();
                        menu.Show();
                    }
                    else
                    {
                        MessageBox.Show("Не тот логин или пароль");
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при выполнении запроса: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

    }
}
