using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using NpgsqlTypes;

namespace dostav
{
    public partial class Form2 : Form
    {
        DataTable dt1 = new DataTable(); 
        DataTable dt2 = new DataTable();

        public Form2()
        {
            InitializeComponent();
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=dostavka;User ID=postgres;Password=123");
            conn.Open();

            NpgsqlCommand comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "SELECT * FROM меню";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells; 

            NpgsqlDataReader dr = comm.ExecuteReader();
            if (dr.HasRows)
            {
                dt1.Load(dr);
                dataGridView1.DataSource = dt1; 
            }
            dr.Close(); 

            comm.CommandText = "SELECT * FROM ингредиенты";
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dr = comm.ExecuteReader();
            if (dr.HasRows)
            {
                dt2.Load(dr);
                dataGridView2.DataSource = dt2; 
            }
            dr.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedCells.Count > 0)
            {
                // Проверка на корректность ввода
                if (IsValidInput(textBox1.Text, textBox2.Text, textBox3.Text))
                {
                    string idValue = textBox1.Text;
                    if (int.TryParse(idValue, out int idIntValue))
                    {
                        using (NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=dostavka;User ID=postgres;Password=123"))
                        {
                            conn.Open();
                            string sql = "INSERT INTO меню (код_блюда, цена, описание) VALUES (@id, @price, @description) RETURNING *;";
                            using (NpgsqlCommand command = new NpgsqlCommand(sql, conn))
                            {
                                command.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, idIntValue);
                                command.Parameters.AddWithValue("@price", Convert.ToDecimal(textBox2.Text));
                                command.Parameters.AddWithValue("@description", NpgsqlDbType.Text, textBox3.Text);

                                using (NpgsqlDataReader dr = command.ExecuteReader())

                                {

                                    if (dr.Read())
                                    {
                                        DataRow row = dt1.NewRow();
                                        row["код_блюда"] = dr["код_блюда"];
                                        row["цена"] = dr["цена"];
                                        row["описание"] = dr["описание"];
                                        dt1.Rows.Add(row);
                                    }
                                }
                            }
                        }

                      
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = dt1;
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

        private bool IsValidInput(string id, string price, string description)
        {
     
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(price) || string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Пожалуйста, заполните все поля");
                return false;
            }
            return true;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "Server=localhost;Port=5432;Database=dostavka;User ID=postgres;Password=123";

                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT * FROM меню", conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;

                        if (selectedRowIndex >= 0 && selectedRowIndex < dt.Rows.Count)
                        {
                            DataRow selectedRow = dt.Rows[selectedRowIndex];
                            selectedRow["код_блюда"] = textBox1.Text;
                            selectedRow["цена"] = textBox2.Text;
                            selectedRow["описание"] = textBox3.Text;

                            using (NpgsqlCommandBuilder commandBuilder = new NpgsqlCommandBuilder(adapter))
                            {
                                adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                                adapter.Update(dt);

                                textBox3.Clear();
                                textBox2.Clear();
                            }
                        }
                    }
                }
            }
            catch (Npgsql.PostgresException ex)
            {
                MessageBox.Show("Ошибка при обновлении данных: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;

                    DataRow selectedRow = ((DataRowView)dataGridView1.SelectedCells[0].OwningRow.DataBoundItem).Row;

                    selectedRow.Delete();

                    using (NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=dostavka;User ID=postgres;Password=123"))
                    {
                        conn.Open();
                        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT * FROM меню", conn))
                        {
                            using (NpgsqlCommandBuilder builder = new NpgsqlCommandBuilder(adapter))
                            {
                                adapter.DeleteCommand = builder.GetDeleteCommand();

                                adapter.Update(dt1);  
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите строку для удаления");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при удалении строки: " + ex.Message);
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private bool IsValidInput1(string id, string ingred)
        {
            // Проверка на пустые значения
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(ingred))
            {
                MessageBox.Show("Пожалуйста, заполните все поля");
                return false;
            }
            return true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (IsValidInput1(textBox4.Text, textBox6.Text))
            {
                string idValue = textBox6.Text;
                if (int.TryParse(idValue, out int idIntValue))
                {
                    try
                    {
                        using (NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=dostavka;User ID=postgres;Password=123"))
                        {
                            conn.Open();
                            string sql = "INSERT INTO ингредиенты (код_блюда, название_ингредиента) VALUES (@id, @ingred) RETURNING *;"; // Возвращаем добавленную строку
                            using (NpgsqlCommand command = new NpgsqlCommand(sql, conn))
                            {
                                command.Parameters.AddWithValue("@id", idIntValue);
                                command.Parameters.AddWithValue("@ingred", textBox4.Text);

                                using (NpgsqlDataReader dr = command.ExecuteReader())
                                {
                                    if (dr.Read())
                                    {
                                        DataRow row = dt2.NewRow();
                                        row["код_блюда"] = dr["код_блюда"];
                                        row["название_ингредиента"] = dr["название_ингредиента"];
                                        dt2.Rows.Add(row);
                                    }
                                }
                            }
                        }

                        dataGridView2.DataSource = null;
                        dataGridView2.DataSource = dt2;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Произошла ошибка при добавлении записи: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("ID блюда должно быть числом.");
                }
            }
            else
            {
                MessageBox.Show("Некорректное значение.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Port=5432;Database=dostavka;User ID=postgres;Password=123";

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT код_блюда, название_ингредиента FROM ингредиенты", conn))
                {
                    DataTable dt2 = new DataTable();
                    adapter.Fill(dt2);

                    int selectedRowIndex = dataGridView2.SelectedCells[0].RowIndex;

                    if (selectedRowIndex >= 0 && selectedRowIndex < dt2.Rows.Count)
                    {
                        DataRow selectedRow = dt2.Rows[selectedRowIndex];
                        // Проверка на пустое значение textBox4
                        if (string.IsNullOrEmpty(textBox4.Text))
                        {
                            MessageBox.Show("Название ингредиента не может быть пустым.");
                            return;
                        }
                        selectedRow["название_ингредиента"] = textBox4.Text;

                        // Явно создаем команду обновления
                        using (NpgsqlCommand updateCommand = new NpgsqlCommand("UPDATE ингредиенты SET название_ингредиента = @название_ингредиента WHERE код_блюда = @код_блюда", conn))
                        {
                            updateCommand.Parameters.AddWithValue("@название_ингредиента", textBox4.Text);
                            updateCommand.Parameters.AddWithValue("@код_блюда", (int)selectedRow["код_блюда"]);

                            adapter.UpdateCommand = updateCommand;
                            adapter.Update(dt2);

                            dt2.Clear();
                            adapter.Fill(dt2);  // обновляем данные в DataTable для отображения изменений
                            dataGridView2.DataSource = dt2;  // обновляем источник данных для отображения изменений в DataGridView
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
            if (dataGridView2.SelectedCells.Count > 0)
            {
                int selectedRowIndex = dataGridView2.SelectedCells[0].RowIndex;

                DataRow selectedRow = ((DataRowView)dataGridView2.SelectedCells[0].OwningRow.DataBoundItem).Row;

                int idToDelete = Convert.ToInt32(selectedRow["код_блюда"]); // или другое имя столбца первичного ключа

                selectedRow.Delete();

                using (NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=dostavka;User ID=postgres;Password=123"))
                {
                    conn.Open();
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT * FROM ингредиенты", conn))
                    {
                        // Определяем DeleteCommand
                        NpgsqlCommand deleteCommand = new NpgsqlCommand("DELETE FROM ингредиенты WHERE код_блюда = @код_блюда", conn);
                        deleteCommand.Parameters.Add(new NpgsqlParameter("@код_блюда", SqlDbType.Int));
                        deleteCommand.Parameters["@код_блюда"].SourceColumn = "код_блюда"; // имя столбца с первичным ключом

                        // Добавляем команду к адаптеру
                        adapter.DeleteCommand = deleteCommand;

                        using (NpgsqlCommandBuilder builder = new NpgsqlCommandBuilder(adapter))
                        {

                            adapter.Update(dt2);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите строку для удаления");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            menu menu = new menu();
            menu.Show();
        }


        private void LoadAndFilterData(DataGridView dataGridView, string searchValue, string columnName, string tableName)
        {
            string connString = "Server=localhost;Port=5432;Database=dostavka;User ID=postgres;Password=123";
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string sql = $"SELECT * FROM {tableName}";

                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, conn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        DataView dataView = new DataView(dataTable);
                        dataView.RowFilter = $"{columnName} LIKE '%{searchValue}%'";
                        dataGridView.DataSource = dataView;
                    }
                    else
                    {
                        dataGridView.DataSource = dataTable;
                    }
                }
            }
        }

        private void ApplyFilter(DataTable dataTable, DataGridView dataGridView, string searchValue, string columnName)
        {
            DataView dataView = new DataView(dataTable);
            dataView.RowFilter = $"{columnName} LIKE '%{searchValue}%'";

            dataGridView.DataSource = dataView.ToTable(); 
        }

        private void buttonSearchMenu_Click_1(object sender, EventArgs e)
        {
            string searchValue = textBoxSearchMenu.Text.ToLower();
            LoadAndFilterData(dataGridView1, searchValue, "описание", "меню");
        }

        private void buttonSearchIngredients_Click_1(object sender, EventArgs e)
        {
            string searchValue = textBoxSearchIngredients.Text.ToLower();
            LoadAndFilterData(dataGridView2, searchValue, "название_ингредиента", "ингредиенты");
        }
    }
}
