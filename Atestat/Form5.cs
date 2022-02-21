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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        OleDbConnection conn = Form1.conn;
        string query = "SELECT * FROM Clienti";

        private void Form5_Load(object sender, EventArgs e)
        {
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, conn);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet,"Clienti");
            dataGridView1.DataSource = dataSet.Tables[0].DefaultView;
        }
    }
}
