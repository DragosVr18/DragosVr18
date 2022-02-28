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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        OleDbConnection conn = Form1.conn;
        string query;

        private void load()
        {
            query = "SELECT Nume FROM Clienti";
            OleDbCommand cmd = new OleDbCommand(query, conn);
            conn.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader.GetString(0));
            }
            conn.Close();
            comboBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void load_2()
        {
            comboBox2.Items.Add("Venit lunar");
            comboBox2.Items.Add("Credite restante");
            comboBox2.Items.Add("Adresa e-mail");
        }

        private void Form4_Shown(object sender, EventArgs e)
        {
            load();
            load_2();
    }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                query = "UPDATE Clienti SET [" + comboBox2.SelectedItem.ToString() + "]='" + textBox1.Text + "' WHERE [Nume]='" + comboBox1.SelectedItem.ToString() + "'";
                OleDbCommand cmd = new OleDbCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                Form1.f1.Message("Datele clientului au fost actualizate cu succes.",Color.PaleGreen);
                conn.Close();
            }
            catch (OleDbException)
            {
                Form1.f1.Message("Datele introduse sunt invalide!",Color.Red);
                conn.Close();
            }
        }
    }
}
