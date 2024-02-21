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

            string �����_������ = usernameTextBox.Text;
            string ������_������ = passwordTextBox.Text;

            string query = "SELECT * FROM ������������� WHERE �����_������ = @�����_������ AND ������_������ = @������_������";

            using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@�����_������", �����_������);
                cmd.Parameters.AddWithValue("@������_������", ������_������);

                try
                {
                    connection.Open();
                    NpgsqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        string fullname = reader["�����_������"].ToString();


                        this.Hide();
                        menu menu = new menu();
                        menu.Show();
                    }
                    else
                    {
                        MessageBox.Show("�� ��� ����� ��� ������");
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("������ ��� ���������� �������: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

    }
}
