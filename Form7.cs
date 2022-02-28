using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Atestat
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        OleDbConnection conn = Form1.conn;
        string query = "SELECT * FROM Conturi";

        private void Form7_Load(object sender, EventArgs e)
        {
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, conn);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "Conturi");
            dataGridView1.DataSource = dataSet.Tables[0].DefaultView;
        }
    }
}
