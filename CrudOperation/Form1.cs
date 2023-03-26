using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrudOperation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-CCCDLDJC;Initial Catalog=Crudoperations_DB;Integrated Security=True");
        public int Student_id;
        private void Form1_Load(object sender, EventArgs e)
        {
            GetStudentDetails();
        }

        private void GetStudentDetails()
        {
            SqlCommand cmd = new SqlCommand("select * from tbl_student",con);
            DataTable dt = new DataTable();


            con.Open();

            SqlDataReader sdr=cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            StudentDataGrid.DataSource= dt;



        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
            SqlCommand cmd = new SqlCommand("insert into tbl_student values (@fname,@lname,@department,@age,@email,@phone)",con);
                cmd.CommandType= CommandType.Text;
                cmd.Parameters.AddWithValue("@fname", fname.Text);
                cmd.Parameters.AddWithValue("@lname", lname.Text);
                cmd.Parameters.AddWithValue("@department", department.Text);
                cmd.Parameters.AddWithValue("@age", age.Text);
                cmd.Parameters.AddWithValue("@email", email.Text);
                cmd.Parameters.AddWithValue("@phone", phone.Text);


                con.Open();
                
                cmd.ExecuteNonQuery();
                con.Close();


                MessageBox.Show("New Student is added successfully","saved",MessageBoxButtons.OK,MessageBoxIcon.Information);
                GetStudentDetails();
                ResetForm();

            }
        }

        private bool IsValid()
        {
           if(fname.Text == String.Empty)
            {
                MessageBox.Show("Student name is Required","failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;   
            }
            return true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Student_id > 0)
            {
                SqlCommand cmd = new SqlCommand("delete from tbl_student where student_id=Student_id", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", this.Student_id);


                con.Open();

                cmd.ExecuteNonQuery();
                con.Close();


                MessageBox.Show(" Student data Deleted successfully", "deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetStudentDetails();
                ResetForm();
            }
            else
            {
                MessageBox.Show("Please select a student", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Student_id > 0)
            {
                SqlCommand cmd = new SqlCommand("update tbl_student set  fname = @fname,lname = @lname,department = @department,age = @age,email= @email,phone = @phone where student_id=Student_id", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@fname", fname.Text);
                cmd.Parameters.AddWithValue("@lname", lname.Text);
                cmd.Parameters.AddWithValue("@department", department.Text);
                cmd.Parameters.AddWithValue("@age", age.Text);
                cmd.Parameters.AddWithValue("@email", email.Text);
                cmd.Parameters.AddWithValue("@phone", phone.Text);
                cmd.Parameters.AddWithValue("@ID", this.Student_id);


                con.Open();

                cmd.ExecuteNonQuery();
                con.Close();


                MessageBox.Show(" Student data updated successfully", "updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetStudentDetails();
                ResetForm();
            }
            else
            {
                MessageBox.Show("Please select a student", "updated", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            ResetForm();

        }

        private void ResetForm()
        {
            Student_id = 0;
            fname.Clear();
            lname.Clear();
            department.Clear();
            age.Clear();
            email.Clear();
            phone.Clear();

            fname.Focus();
        }

        private void StudentDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Student_id = Convert.ToInt32(StudentDataGrid.SelectedRows[0].Cells[0].Value);
            fname.Text = StudentDataGrid.SelectedRows[0].Cells[1].Value.ToString();
            lname.Text = StudentDataGrid.SelectedRows[0].Cells[2].Value.ToString();
            department.Text = StudentDataGrid.SelectedRows[0].Cells[3].Value.ToString();
            age.Text = StudentDataGrid.SelectedRows[0].Cells[4].Value.ToString();
            email.Text = StudentDataGrid.SelectedRows[0].Cells[5].Value.ToString();
            phone.Text = StudentDataGrid.SelectedRows[0].Cells[6].Value.ToString();

        }
    }
}
