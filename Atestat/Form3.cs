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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        OleDbConnection conn = Form1.conn;
        string query, query2, query3;

         private void load()
        {
            query = "SELECT Nume FROM Clienti";
            OleDbCommand cmd = new OleDbCommand(query,conn);
            conn.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader.GetString(0));
            }
            conn.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            load();
        } 

        private void button1_Click(object sender, EventArgs e)
        {
            query = "DELETE FROM Clienti WHERE [Nume]='" + comboBox1.SelectedItem.ToString() + "'";
            query2 = "DELETE FROM Conturi WHERE [Nume titular]='" + comboBox1.SelectedItem.ToString() + "'";
            query3 = "DELETE FROM Carduri WHERE [Nume titular]='" + comboBox1.SelectedItem.ToString() + "'";
            OleDbCommand cmd = new OleDbCommand(query, conn);
            OleDbCommand cmd2 = new OleDbCommand(query2, conn);
            OleDbCommand cmd3 = new OleDbCommand(query3, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            try
            {
                cmd2.ExecuteNonQuery();
            }
            catch { };
            try
            {
                cmd3.ExecuteNonQuery();
            }
            catch { };
            Form1.f1.Message("Clientul a fost sters cu succes.", Color.PaleGreen);
            conn.Close();
        }
    }
}
