using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form2 : Form
    {
        string btnclick="g";
        //string Conn = "Sever=jdbc:mysql://127.0.0.1:3306/jspdb?serverTimezone=Asia/Seoul;Database=sr_list;Uid=hansung;Pwd=1234;";
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void button2_Click_1(object sender, EventArgs e)
        {
            string connStr = string.Format("server=127.0.0.1; user=hansung; password=1234; database=srlist");
            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();
                MessageBox.Show("MySql 연결 성공!");
                MySqlCommand command = new MySqlCommand("INSERT INTO srlist.srlist(date,detail,state1,writer) values('"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','"+textBox4.Text+"','"+ btnclick + "','"+textBox5.Text+"')", conn);
                if (command.ExecuteNonQuery() != 1)
                    MessageBox.Show("Failed to insert data.");
            }

            catch
            {

                MessageBox.Show("MySql 연결 실패");

            }
            conn.Close();
            this.Close();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            btnclick = "g";
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            btnclick = "r";
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}