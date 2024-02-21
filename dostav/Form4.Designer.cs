namespace dostav
{
    partial class Form4
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            pictureBox1 = new PictureBox();
            label1 = new Label();
            textBox1 = new TextBox();
            listView1 = new ListView();
            button1 = new Button();
            button7 = new Button();
            listView2 = new ListView();
            textBox3 = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            textBoxФИО = new TextBox();
            textBoxАдрес = new TextBox();
            dataGridView2 = new DataGridView();
            txtКодБлюда = new TextBox();
            txtКодКурьера = new TextBox();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, -13);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1048, 770);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.DarkSeaGreen;
            label1.Font = new Font("Segoe UI", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.ForeColor = Color.White;
            label1.Location = new Point(447, 9);
            label1.Name = "label1";
            label1.Size = new Size(122, 50);
            label1.TabIndex = 14;
            label1.Text = "Заказ";
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            textBox1.Location = new Point(22, 99);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(275, 34);
            textBox1.TabIndex = 15;
            textBox1.TextChanged += textBox1_TextChanged_2;
            // 
            // listView1
            // 
            listView1.Location = new Point(22, 139);
            listView1.Name = "listView1";
            listView1.Size = new Size(275, 404);
            listView1.TabIndex = 17;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.SmallIcon;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged_1;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 14.2F, FontStyle.Bold);
            button1.Location = new Point(65, 589);
            button1.Name = "button1";
            button1.Size = new Size(154, 55);
            button1.TabIndex = 18;
            button1.Text = "Ок";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // button7
            // 
            button7.BackColor = Color.DarkSlateGray;
            button7.Font = new Font("Segoe UI", 22.2F, FontStyle.Bold);
            button7.ForeColor = Color.White;
            button7.Location = new Point(12, 665);
            button7.Name = "button7";
            button7.Size = new Size(228, 76);
            button7.TabIndex = 26;
            button7.Text = "Назад";
            button7.UseVisualStyleBackColor = false;
            button7.Click += button7_Click_1;
            // 
            // listView2
            // 
            listView2.Location = new Point(335, 139);
            listView2.Name = "listView2";
            listView2.Size = new Size(293, 404);
            listView2.TabIndex = 27;
            listView2.UseCompatibleStateImageBehavior = false;
            listView2.View = View.SmallIcon;
            listView2.SelectedIndexChanged += listView2_SelectedIndexChanged_1;
            // 
            // textBox3
            // 
            textBox3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            textBox3.Location = new Point(335, 99);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(293, 34);
            textBox3.TabIndex = 28;
            textBox3.TextChanged += textBox3_TextChanged_1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.DarkSeaGreen;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label2.ForeColor = SystemColors.ButtonFace;
            label2.Location = new Point(22, 68);
            label2.Name = "label2";
            label2.Size = new Size(79, 28);
            label2.TabIndex = 31;
            label2.Text = "Блюдо";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.DarkSeaGreen;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label3.ForeColor = SystemColors.ButtonHighlight;
            label3.Location = new Point(335, 68);
            label3.Name = "label3";
            label3.Size = new Size(81, 28);
            label3.TabIndex = 32;
            label3.Text = "Курьер";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.DarkSeaGreen;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label4.ForeColor = SystemColors.ButtonHighlight;
            label4.Location = new Point(669, 184);
            label4.Name = "label4";
            label4.Size = new Size(72, 28);
            label4.TabIndex = 33;
            label4.Text = "Адрес";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.DarkSeaGreen;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label5.ForeColor = SystemColors.ButtonHighlight;
            label5.Location = new Point(669, 68);
            label5.Name = "label5";
            label5.Size = new Size(54, 28);
            label5.TabIndex = 34;
            label5.Text = "Фио";
            // 
            // textBoxФИО
            // 
            textBoxФИО.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            textBoxФИО.Location = new Point(669, 99);
            textBoxФИО.Name = "textBoxФИО";
            textBoxФИО.Size = new Size(328, 43);
            textBoxФИО.TabIndex = 35;
            textBoxФИО.TextChanged += textBoxФИО_TextChanged;
            // 
            // textBoxАдрес
            // 
            textBoxАдрес.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            textBoxАдрес.Location = new Point(669, 215);
            textBoxАдрес.Name = "textBoxАдрес";
            textBoxАдрес.Size = new Size(328, 43);
            textBoxАдрес.TabIndex = 37;
            // 
            // dataGridView2
            // 
            dataGridView2.BackgroundColor = Color.DarkSeaGreen;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.GridColor = Color.DarkCyan;
            dataGridView2.Location = new Point(669, 294);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 51;
            dataGridView2.Size = new Size(328, 289);
            dataGridView2.TabIndex = 38;
            dataGridView2.CellContentClick += dataGridView2_CellContentClick_1;
            // 
            // txtКодБлюда
            // 
            txtКодБлюда.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            txtКодБлюда.Location = new Point(22, 549);
            txtКодБлюда.Name = "txtКодБлюда";
            txtКодБлюда.Size = new Size(275, 34);
            txtКодБлюда.TabIndex = 39;
            txtКодБлюда.TextChanged += txtКодБлюда_TextChanged_1;
            // 
            // txtКодКурьера
            // 
            txtКодКурьера.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            txtКодКурьера.Location = new Point(335, 549);
            txtКодКурьера.Name = "txtКодКурьера";
            txtКодКурьера.Size = new Size(293, 34);
            txtКодКурьера.TabIndex = 40;
            txtКодКурьера.TextChanged += txtКодКурьера_TextChanged;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 14.2F, FontStyle.Bold);
            button2.Location = new Point(556, 616);
            button2.Name = "button2";
            button2.Size = new Size(185, 48);
            button2.TabIndex = 41;
            button2.Text = "Статистика";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click_2;
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1029, 753);
            Controls.Add(button2);
            Controls.Add(txtКодКурьера);
            Controls.Add(txtКодБлюда);
            Controls.Add(dataGridView2);
            Controls.Add(textBoxАдрес);
            Controls.Add(textBoxФИО);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBox3);
            Controls.Add(listView2);
            Controls.Add(button7);
            Controls.Add(button1);
            Controls.Add(listView1);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Name = "Form4";
            Text = "Form4";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private TextBox textBox1;
        private ListView listView1;
        private Button button1;
        private Button button7;
        private ListView listView2;
        private TextBox textBox3;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox textBoxФИО;
        private TextBox textBoxАдрес;
        private DataGridView dataGridView2;
        private TextBox txtКодБлюда;
        private TextBox txtКодКурьера;
        private Button button2;
    }
}