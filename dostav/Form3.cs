using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dostav
{
    public partial class Form3 : Form
    {
        DataTable dt3 = new DataTable(); 

        public Form3()
        {
            InitializeComponent();
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=dostavka;User ID=postgres;Password=123");
            conn.Open();

            NpgsqlCommand comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "SELECT * FROM курьеры";
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells; 
            NpgsqlDataReader dr = comm.ExecuteReader();
            if (dr.HasRows)
            {
                dt3.Load(dr);
                dataGridView3.DataSource = dt3;
            }
            dr.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            menu menu = new menu();
            menu.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedCells.Count > 0)
            {
                // Проверка на корректность ввода
                if (IsValidInput(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text))
                {
                    string idValue = textBox1.Text;
                    if (int.TryParse(idValue, out int idIntValue))
                    {
                        using (NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=dostavka;User ID=postgres;Password=123"))
                        {
                            conn.Open();
                            string sql = "INSERT INTO курьеры (код_курьера, фио, номер_телефона, тип_транспорта) VALUES (@код_курьера, @фио, @номер_телефона,@тип_транспорта) RETURNING *;"; // Возвращаем добавленную строку
                            using (NpgsqlCommand command = new NpgsqlCommand(sql, conn))
                            {
                                command.Parameters.AddWithValue("@код_курьера", NpgsqlDbType.Integer, idIntValue);
                                command.Parameters.AddWithValue("@фио", NpgsqlDbType.Text, textBox2.Text);
                                command.Parameters.AddWithValue("@номер_телефона", NpgsqlDbType.Text, textBox3.Text);
                                command.Parameters.AddWithValue("@тип_транспорта", NpgsqlDbType.Text, textBox4.Text);

                                using (NpgsqlDataReader dr = command.ExecuteReader())
                                {
                                    if (dr.Read())
                                    {
                                        DataRow row = dt3.NewRow();
                                        row["код_курьера"] = dr["код_курьера"];
                                        row["номер_телефона"] = dr["номер_телефона"];
                                        row["тип_транспорта"] = dr["тип_транспорта"];
                                        row["фио"] = dr["фио"];
                                        dt3.Rows.Add(row);
                                    }
                                }
                            }
                        }

                        dataGridView3.DataSource = null;
                        dataGridView3.DataSource = dt3;
                    }
                }
                else
                {
                    MessageBox.Show("Некорректное значение");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите ячейку для добавления записи");
            }
        }

        private bool IsValidInput(string код_курьера, string номер_телефона, string тип_транспорта, string фио)
        {
            // Проверка на пустые значения
            if (string.IsNullOrWhiteSpace(код_курьера) || string.IsNullOrWhiteSpace(номер_телефона) || string.IsNullOrWhiteSpace(тип_транспорта) || string.IsNullOrWhiteSpace(фио))
            {
                MessageBox.Show("Пожалуйста, заполните все поля");
                return false;
            }
            return true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Port=5432;Database=dostavka;User ID=postgres;Password=123";

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT * FROM курьеры", conn))
                {
                    DataTable dt3 = new DataTable();
                    adapter.Fill(dt3);

                    int selectedRowIndex = dataGridView3.SelectedCells[0].RowIndex;

                    if (selectedRowIndex >= 0 && selectedRowIndex < dt3.Rows.Count)
                    {
                        DataRow selectedRow = dt3.Rows[selectedRowIndex];
                        selectedRow["фио"] = textBox2.Text;
                        selectedRow["тип_транспорта"] = textBox3.Text;
                        selectedRow["номер_телефона"] = textBox4.Text;

                        using (NpgsqlCommandBuilder commandBuilder = new NpgsqlCommandBuilder(adapter))
                        {
                            adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                            adapter.Update(dt3);

                            dt3.Clear();
                            adapter.Fill(dt3);  
                            dataGridView3.DataSource = dt3; 
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Selected row (index {selectedRowIndex}) is out of range.");
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedCells.Count > 0)
            {
                int selectedRowIndex = dataGridView3.SelectedCells[0].RowIndex;

                DataRow selectedRow = ((DataRowView)dataGridView3.SelectedCells[0].OwningRow.DataBoundItem).Row;

                int idToDelete = Convert.ToInt32(selectedRow["код_курьера"]); 

                selectedRow.Delete();

                using (NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=dostavka;User ID=postgres;Password=123"))
                {
                    conn.Open();
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT * FROM курьеры", conn))
                    {
                        // Определяем DeleteCommand
                        NpgsqlCommand deleteCommand = new NpgsqlCommand("DELETE FROM курьеры WHERE код_курьера = @код_курьера", conn);
                        deleteCommand.Parameters.Add(new NpgsqlParameter("@код_курьера", SqlDbType.Int));
                        deleteCommand.Parameters["@код_курьера"].SourceColumn = "код_курьера";

                        // Добавляем команду к адаптеру
                        adapter.DeleteCommand = deleteCommand;

                        using (NpgsqlCommandBuilder builder = new NpgsqlCommandBuilder(adapter))
                        {

                            adapter.Update(dt3);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите строку для удаления");
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}
