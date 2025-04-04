using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using WindowsFormsApp1;

namespace DheaLabExam
{
    public partial class StudentPageForm : Form
    {
        private string connectionString = "server=localhost;uid=root;pwd=;database=StudentInfoDB";

        public StudentPageForm()
        {
            InitializeComponent();
            LoadStudentRecords();
        }

        private void LoadStudentRecords()
        {
            dataGridView1.Rows.Clear();
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT studentId, CONCAT(firstName, ' ', lastName) AS fullName FROM StudentRecordTB";
                var command = new MySqlCommand(query, connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader["studentId"].ToString(), reader["fullName"].ToString(), "View");
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex >= 0)
            {
                var studentId = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                var detailForm = new StudentPageIndividualForm(studentId);
                detailForm.ShowDialog();
            }
        }
    }
}
