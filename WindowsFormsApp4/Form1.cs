using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
            timer1.Start();     //timer1의 이벤트를 시작시킴.
            select();
            update();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //label1의 Text에 현재시각을 표출
            label1.Text = DateTime.Now.ToString("yyyy년MM월dd일HH시mm분");
        }

     

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 newfrom2 = new Form2();
            newfrom2.ShowDialog();
            select();
            
        }
   

        private void button2_Click(object sender, EventArgs e)
        {
            // select();

            Form3 newfrom3 = new Form3();
            newfrom3.ShowDialog();
            select();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void select()
        {
            try
            {
                string connStr = string.Format("server=127.0.0.1; user=hansung; password=1234; database=srlist");
                //보여지는 쿼리 
                string Query = "SELECT no, date, detail, state, writer FROM srlist.srlist;";
                MySqlConnection MyConn2 = new MySqlConnection(connStr);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = MyCommand2;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                dataGridView1.DataSource = dTable; // here i have assign dTable object to the dataGridView1 object to display data.
                                                   // MyConn2.Close();

                //
                string Query2 = "SELECT * FROM srlist.srlist";
                MySqlCommand MyCommand3 = new MySqlCommand(Query2, MyConn2);
                MySqlDataAdapter MyAdapter2 = new MySqlDataAdapter();
                MyAdapter2.SelectCommand = MyCommand3;
                DataTable dTable2 = new DataTable();
                MyAdapter2.Fill(dTable2);
                //긴급과 처리중 카운트
                int sum1 = 0;
                int sum2 = 0;
                for (int i = 0; i < dTable2.Rows.Count; i++)
                {
                    string id = dTable2.Rows[i]["state1"].ToString();
                    
                    if(id == "g" )
                    {
                        sum1 += 1;
                    }
                    else if(id == "r")
                    {
                        sum2 += 1;
                       // dataGridView1.Rows[2].Cells[2].Style.BackColor = Color.OrangeRed;
                    }

                }
                string str1 = sum1.ToString();
                string str2 = sum2.ToString();
                label13.Text = str1;
                label14.Text = str2;

                //최근 일주일 접수
                label12.Text = dataGridView1.RowCount.ToString();
               
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        //상태 (stae) cell 클릭시 db에 state UPDATE.
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string connStr8 = string.Format("server=127.0.0.1; user=hansung; password=1234; database=srlist");
            MySqlConnection conn8= new MySqlConnection(connStr8);
            try
            {
                if (e.ColumnIndex == 3)  // 4번째 칼럼이 선택되면....
                {
                    conn8.Open();
                    string st = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();  //선택한 셀의 state
                    string no = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    int outValue = int.Parse(no);

                    
                    if (st == "처리중")
                    {
                        MySqlCommand command8 = new MySqlCommand("update srlist.srlist set state=\"완료\" where no ='" + outValue + "';", conn8);
                        if (command8.ExecuteNonQuery() != 1)
                            MessageBox.Show("Failed to insert data.");
                    }
                    else if (st == "완료")
                    {
                        MySqlCommand command9 = new MySqlCommand("update srlist.srlist set state=\"처리중\" where no ='" + outValue + "';", conn8);
                        if (command9.ExecuteNonQuery() != 1)
                            MessageBox.Show("Failed to insert data.");
                    }
                    select();
                }
                else { }
            }

            catch
            {

                MessageBox.Show("MySql 연결 실패");

            }
            conn8.Close();
            
        }

        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }
        public void update()
        {

        }
        // 상태 (state) 색상 설정. 
        private void dataGridView1_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                string u = Convert.ToString(e.Value);
                if (u =="처리중")
                {
                    e.CellStyle.BackColor = Color.PaleGreen;
                }
                else if(u=="완료")
                {
                    e.CellStyle.BackColor = Color.Gainsboro;
                }
            }

        }
    }
}
