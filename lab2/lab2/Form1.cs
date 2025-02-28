
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;
namespace lab2
{
    public partial class Form1 : Form
    {

        SqlConnection con;
        SqlCommand cmd;
        public Form1()
        {
            InitializeComponent();
          
            con = new SqlConnection();
            con.ConnectionString = "Data Source= DESKTOP-P8ERP6C\\SQLEXPRESS ;Initial Catalog=ITI ;Integrated Security=True ;TrustServerCertificate=True";
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // button display 
            // open connection
            cmd = new SqlCommand("SELECT s.id , s.First_Name , s.Last_Name , s.age,s.Dept_id ,d.Dept_Name  FROM student as s ,Department as d where d.Dept_id=s.Dept_id", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            List<student> sts = new List<student>();
            while (dr.Read())
            {
                student s = new student();
                s.firstName = dr["First_Name"].ToString();
                s.lastName = dr["Last_Name"].ToString();
                s.Dept_Name = dr["Dept_Name"].ToString();
                s.s_id = (int)dr["id"];
                s.dept_id = (int)dr["Dept_id"];
                sts.Add(s);

            }
            dataGridView1.DataSource = sts;
            //close connection 
            con.Close();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("insert into student(id,First_Name ,Last_Name ,Dept_id) values(@id ,@fname,@lname,@deptid)", con);
            cmd.Parameters.AddWithValue("@id", textBox1.Text);
            cmd.Parameters.AddWithValue("@fname", textBox2.Text);
            cmd.Parameters.AddWithValue("@lname", textBox3.Text);
            cmd.Parameters.AddWithValue("@deptid", comboBox1.SelectedValue);
            con.Open();
            int roweffect = cmd.ExecuteNonQuery();

            if (roweffect > 0)
            {
                textBox1.Text = textBox2.Text = textBox3.Text = "";
                comboBox1.SelectedIndex = -1;
                MessageBox.Show("successful inserted!");
                display();

            }
            else
            {
                MessageBox.Show("fail! not inserted");
            }
            con.Close();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {



        }
        private void button3_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id_delete = (int)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[0].Value;

                cmd = new SqlCommand("delete from student where id=@id", con);
                cmd.Parameters.AddWithValue("@id", id_delete);
                con.Open();
                int row = cmd.ExecuteNonQuery();
                // remove row from grid view
                MessageBox.Show("deleted successfully" + id_delete);
                textBox1.Text = textBox2.Text = textBox3.Text = " ";
                comboBox1.SelectedIndex = -1;
                display();
            }
            else
            {
                MessageBox.Show("fail! not deleted");
                con.Close();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            button4.Visible = true;
            cmd = new SqlCommand("SELECT Dept_id, Dept_Name FROM Department", con);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            List<dept> depts = new List<dept>();

            while (dr.Read())
            {
                if (dr["Dept_id"] != DBNull.Value)
                {
                    dept d = new dept();
                    d.id = Convert.ToInt32(dr["Dept_id"]);
                    d.name = dr["Dept_Name"].ToString();
                    depts.Add(d);
                }
            }

            dr.Close();
            con.Close();
            
            comboBox1.ValueMember = "id";
            comboBox1.DisplayMember = "name";
            comboBox1.DataSource = depts;
            comboBox1.SelectedIndex = -1;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.BackColor = Color.White;
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            comboBox1.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[4].Value;
            con.Open();
            //button4.Visible = true;
            //button2.Visible = true;
            con.Close();
            textBox1.Enabled = false;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("update student set First_Name=@fname ,Last_Name=@lname ,Dept_id=@deptid where id=@id", con);
            cmd.Parameters.AddWithValue("@id", textBox1.Text);
            cmd.Parameters.AddWithValue("@fname", textBox2.Text);
            cmd.Parameters.AddWithValue("@lname", textBox3.Text);
            cmd.Parameters.AddWithValue("@deptid", comboBox1.SelectedValue);
            con.Open();
            int roweffected = cmd.ExecuteNonQuery();
            if (roweffected > 0)
            {
                MessageBox.Show("updated successfully");
                textBox1.Text = textBox2.Text = textBox3.Text = "";
                comboBox1.SelectedIndex = -1;
                display();
                button2.Visible = true;
                button4.Visible = true;
                textBox1.Enabled = true;
            }
            else
            {
                MessageBox.Show("fail! not updated");
            }
            con.Close();

        }

        private void display()
        {
            cmd = new SqlCommand("SELECT s.id , s.First_Name , s.Last_Name , s.age,s.Dept_id ,d.Dept_Name  FROM student as s ,Department as d where d.Dept_id=s.Dept_id", con);
            // con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            List<student> sts = new List<student>();
            while (dr.Read())
            {
                student s = new student();
                s.firstName = dr["First_Name"].ToString();
                s.lastName = dr["Last_Name"].ToString();
                s.Dept_Name = dr["Dept_Name"].ToString();
                s.s_id = (int)dr["id"];
                s.dept_id = (int)dr["Dept_id"];
                sts.Add(s);

            }
            dataGridView1.DataSource = sts;
            //close connection 
            con.Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("Selected Value: " + dataGridView1.SelectedCells[0].Value);
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedRowIndex];

                string selectedValue = Convert.ToString(selectedRow.Cells[0].Value);

                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                comboBox1.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[4].Value;

                button4.Visible = true; 
            }
        }

        private void button4_VisibleChanged(object sender, EventArgs e)
        {

        }
    }


}
