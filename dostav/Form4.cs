using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using NpgsqlTypes;

namespace dostav
{
    public partial class Form4 : Form
    {
        NpgsqlConnection conn; 

        public Form4()
        {
            InitializeComponent();
            listView1.View = View.Details;
            listView1.Columns.Add("код_блюда", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("описание", -2, HorizontalAlignment.Left);

            listView2.View = View.Details;
            listView2.Columns.Add("код_курьера", -2, HorizontalAlignment.Left);
            listView2.Columns.Add("фио", -2, HorizontalAlignment.Left);
            listView2.Columns.Add("тип_транспорта", -2, HorizontalAlignment.Left);
            conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=dostavka;User ID=postgres;Password=123;Include Error Detail=true;");
            conn.Open();

            InitializeDataGridView();
          


            LoadDataToListView();
            LoadDataToListView1();

      
            textBox1.TextChanged += TextBox1_TextChanged;
            textBox3.TextChanged += textBox3_TextChanged_1;
        }

       
        private void LoadDataToListView()
        {
            listView1.Items.Clear(); 

            string query = "SELECT * FROM меню"; 

            using (NpgsqlCommand comm = new NpgsqlCommand(query, conn))
            {
                using (NpgsqlDataReader reader = comm.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ListViewItem item = new ListViewItem(reader["код_блюда"].ToString());
                      
                        item.SubItems.Add(reader["описание"].ToString());
                        listView1.Items.Add(item);
                    }
                }
            }
        }

       
        private void SearchInListView(string searchQuery)
        {
         
            listView1.BeginUpdate();

            foreach (ListViewItem item in listView1.Items)
            {
               
                item.BackColor = SystemColors.Window;
                item.ForeColor = SystemColors.WindowText;

                
                if (!string.IsNullOrEmpty(searchQuery))
                {
                    bool itemMatches = item.SubItems
                        .Cast<ListViewItem.ListViewSubItem>()
                        .Any(subItem => subItem.Text.ToLower().Contains(searchQuery.ToLower()));

                   
                    if (itemMatches)
                    {
                        item.BackColor = SystemColors.Highlight;
                        item.ForeColor = SystemColors.HighlightText;
                        item.Selected = true;
                    }
                    
                    item.Font = itemMatches ? new Font(item.Font, FontStyle.Regular) : new Font(item.Font, FontStyle.Strikeout); 
                    item.ForeColor = itemMatches ? item.ForeColor : SystemColors.GrayText; 
                    item.Selected = false;
                }
            }

            listView1.EndUpdate();
        }



        private void LoadDataToListView1()
        {
            listView2.Items.Clear();

            string query = "SELECT * FROM курьеры";

            using (NpgsqlCommand comm = new NpgsqlCommand(query, conn))
            {
                using (NpgsqlDataReader reader = comm.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ListViewItem item = new ListViewItem(reader["код_курьера"].ToString());
                        item.SubItems.Add(reader["фио"].ToString());
                        item.SubItems.Add(reader["тип_транспорта"].ToString());
                        listView2.Items.Add(item);
                    }
                }
            }
        }
        private void SearchInListView1(string searchQuery)
        {
          
            listView2.BeginUpdate();

            foreach (ListViewItem item in listView2.Items)
            {
                
                item.BackColor = SystemColors.Window;
                item.ForeColor = SystemColors.WindowText;

               
                if (!string.IsNullOrEmpty(searchQuery))
                {
                    bool itemMatches = item.SubItems
                        .Cast<ListViewItem.ListViewSubItem>()
                        .Any(subItem => subItem.Text.ToLower().Contains(searchQuery.ToLower()));

                   
                    if (itemMatches)
                    {
                        item.BackColor = SystemColors.Highlight;
                        item.ForeColor = SystemColors.HighlightText;
                        item.Selected = true;
                    }
  
                    item.Font = itemMatches ? new Font(item.Font, FontStyle.Regular) : new Font(item.Font, FontStyle.Strikeout); 
                    item.ForeColor = itemMatches ? item.ForeColor : SystemColors.GrayText; 
                    item.Selected = false;
                }
            }


            listView2.EndUpdate();
        }

       

        private void InitializeDataGridView()
        {

            dataGridView2.Columns.Add("код_заказа", "Код заказа");
            dataGridView2.Columns.Add("код_блюда", "Код блюда");
            dataGridView2.Columns.Add("код_курьера", "Код курьера");
            dataGridView2.Columns.Add("фио", "ФИО");
            dataGridView2.Columns.Add("адрес", "Адрес");

                
            LoadOrdersToDataGridView();
        }
        private string AddOrderToDB(string кодБлюда, string кодКурьера, string фио, string адрес)
        {
            string кодЗаказа = Guid.NewGuid().ToString();
            string insertQuery = "INSERT INTO заказ (код_заказa, код_блюда, код_курьера, фио, адрес) VALUES (@код_заказa, @код_блюда, @код_курьера, @фио, @адрес)";

            using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, conn))
            {
                command.Parameters.AddWithValue("@код_заказa", кодЗаказа);
                command.Parameters.AddWithValue("@код_блюда", кодБлюда);
                command.Parameters.AddWithValue("@код_курьера", кодКурьера);
                command.Parameters.AddWithValue("@фио", фио);
                command.Parameters.AddWithValue("@адрес", адрес);

                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("заказ добавлен!");
                        return кодЗаказа;   
                    }
                    else
                    {
                        MessageBox.Show("ошибка при создании.");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ой: " + ex.Message);
                    return null;
                }
            }
        }
        private void LoadOrdersToDataGridView()
        {
            dataGridView2.Rows.Clear();    

            string query = "SELECT * FROM заказ";   

            using (NpgsqlCommand comm = new NpgsqlCommand(query, conn))
            {
                using (NpgsqlDataReader reader = comm.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataGridView2.Rows.Add(reader["код_заказa"], reader["код_блюда"], reader["код_курьера"], reader["фио"], reader["адрес"]);
                    }
                }
            }
        }


        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }


        private void txtКодБлюда_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string кодБлюда = txtКодБлюда.Text;  
            string кодКурьера = txtКодКурьера.Text;  
            string фио = textBoxФИО.Text;  
            string адрес = textBoxАдрес.Text;  

            string кодЗаказа = AddOrderToDB(кодБлюда, кодКурьера, фио, адрес);
            if (кодЗаказа != null)
            {
                LoadOrdersToDataGridView();  
            }
        }

        private void dataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

         
                string код_блюда = row.Cells["код_блюда"].Value.ToString();
                string код_курьера = row.Cells["код_курьера"].Value.ToString();

                string фио = row.Cells["фио"].Value.ToString();
                string адрес = row.Cells["адрес"].Value.ToString();

         
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = textBox1.Text.Trim();
            SearchInListView(searchQuery); // Вызываем метод поиска с обновленной строкой.
        }

        private void txtКодКурьера_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            menu menu = new menu();
            menu.Show();
        }

        private void textBox1_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                txtКодБлюда.Text = listView1.SelectedItems[0].SubItems[1].Text; // Передаем выбранные данные из ListView в TextBox
            }
        }

        private void listView2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                ListView.SelectedListViewItemCollection selectedItems = listView2.SelectedItems;
                ListViewItem selectedItem = selectedItems[0];
                if (selectedItem.SubItems.Count > 1)
                {
                    txtКодКурьера.Text = selectedItem.SubItems[1].Text;
                }
                else
                {

                    txtКодКурьера.Text = "";
                }
            }
            else
            {
                txtКодКурьера.Text = "";
            }
        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {
            string searchQuery = textBox3.Text.Trim();
            SearchInListView1(searchQuery); 
        }

        private void textBoxФИО_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtКодБлюда_TextChanged_1(object sender, EventArgs e)
        {

        }



        private void button2_Click_2(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];

                if (selectedRow.Cells["код_блюда"].Value != null)
                {
                    string код_блюда = selectedRow.Cells["код_блюда"].Value.ToString();
                    // Дальнейшие действия с кодом блюда
                }

                if (selectedRow.Cells["код_курьера"].Value != null)
                {
                    string код_курьера = selectedRow.Cells["код_курьера"].Value.ToString();
                    // Дальнейшие действия с кодом курьера
                }

                if (selectedRow.Cells["фио"].Value != null)
                {
                    string фио = selectedRow.Cells["фио"].Value.ToString();
                    // Дальнейшие действия с ФИО
                }

                if (selectedRow.Cells["адрес"].Value != null)
                {
                    string адрес = selectedRow.Cells["адрес"].Value.ToString();
                    // Дальнейшие действия с адресом
                }

                Dictionary<string, int> статистика_заказов = new Dictionary<string, int>();

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Cells["код_курьера"].Value != null)
                    {
                        string текущий_код_курьера = row.Cells["код_курьера"].Value.ToString();

                        if (статистика_заказов.ContainsKey(текущий_код_курьера))
                        {
                            статистика_заказов[текущий_код_курьера]++;
                        }
                        else
                        {
                            статистика_заказов[текущий_код_курьера] = 1;
                        }
                    }
                }

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Cells["код_курьера"].Value != null)
                    {
                        string текущий_код_курьера = row.Cells["код_курьера"].Value.ToString();
                        int количество_заказов = статистика_заказов[текущий_код_курьера];
                        MessageBox.Show("Текущий код курьера: " + текущий_код_курьера + ", Количество заказов: " + количество_заказов);

                    }
                }

            }
        }
    }
}