using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace Pers_ach
{
    public partial class Main : Form
    {
        private string file_name;
        private string conn_str;
        private string cmd_str;

        private string new_cmd_str;
        private string ppl_cmd_str;



        private OpenFileDialog openFileDialog;

        private MySqlConnection sql_conn;
        private MySqlCommand sql_cmd;
        private DataSet sql_ds;
        private MySqlDataAdapter sql_da;


        public Main() //инициализация
        {
            InitializeComponent();
        }

        private void textBox3_TextChanged(object sender, EventArgs e) //чары для пароля
        {
            textBox3.PasswordChar = '*';
        }

        public void button1_Click(object sender, EventArgs e) //открытие проводника для выбора файла
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Comma Separated Value(*.csv) | *.csv"; //только csv


            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                file_name = openFileDialog.FileName;
                MessageBox.Show(file_name);
            }
        }

        public void button2_Click(object sender, EventArgs e) //загрузка файла в БД
        {
           
        }//еще не сделано!!!!!!!!!!!!!

        private void button3_Click(object sender, EventArgs e) //подключение по нажатию кнопки "подключиться"
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) 
                || string.IsNullOrWhiteSpace(textBox3.Text) || comboBox1.SelectedIndex == -1 
                || comboBox2.SelectedIndex == -1) // проверка заполненности полей
            {
                MessageBox.Show("Проверьте, заполнены ли все поля!");
            }
            else
            {
                /*textBox1.Text = "db4free.net";
                textBox2.Text = "boy_next_root";
                textBox3.Text = "6f444895";
                comboBox1.SelectedText = "boy_next_db";
                comboBox2.SelectedText = "all_students";*/

                conn_str = "server=" + textBox1.Text + ";user=" + textBox2.Text + " ;password=" + textBox3.Text + ";database=" + comboBox1.SelectedItem.ToString() + ";old guids = true;";
                cmd_str = "SELECT * FROM " + comboBox2.SelectedItem;
                //conn_str = "server=" + textBox1.Text + ";user=" + textBox2.Text + " ;password=" + textBox3.Text + ";database=boy_next_db;old guids = true;";
                //cmd_str = "SELECT * FROM all_students";  

                sql_conn = new MySqlConnection(conn_str);
                sql_conn.Open();

                sql_cmd = new MySqlCommand(cmd_str, sql_conn);
                sql_da = new MySqlDataAdapter(cmd_str, sql_conn);
                sql_ds = new DataSet();
                sql_da.Fill(sql_ds);

                dataGridView1.DataSource = sql_ds.Tables[0].DefaultView;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
           }
        }

        private void button4_Click(object sender, EventArgs e) //выполнение сортировки и поиска
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) 
                || string.IsNullOrWhiteSpace(textBox3.Text) || comboBox1.SelectedIndex == -1 
                || comboBox2.SelectedIndex == -1) // проверка заполненности полей
            {
                MessageBox.Show("Проверьте, заполнены ли все поля!");
            }
            else
            {
                MySqlCommand new_sql_cmd = new MySqlCommand(new_cmd_str, sql_conn);
                sql_da = new MySqlDataAdapter(new_cmd_str, sql_conn);
                sql_ds = new DataSet();
                sql_da.Fill(sql_ds);

                dataGridView1.DataSource = sql_ds.Tables[0].DefaultView;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }//еще не сделано!!!!!!!!!!!!!

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                ppl_cmd_str = "SELECT * FROM `" + row.Cells["ФИО"].Value.ToString() + "`";
                textBox4.Text = ppl_cmd_str;
                sql_cmd = new MySqlCommand(ppl_cmd_str, sql_conn);
                sql_da = new MySqlDataAdapter(ppl_cmd_str, sql_conn);
                sql_ds = new DataSet();
                sql_da.Fill(sql_ds);

                dataGridView2.DataSource = sql_ds.Tables[0].DefaultView;
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }
    }
}
