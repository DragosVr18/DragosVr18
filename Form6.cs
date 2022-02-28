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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        OleDbConnection conn = Form1.conn;
        string query,query2;

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
        }

        private void load2()
        {
            comboBox2.Items.Add("RON");
            comboBox2.Items.Add("EUR");
            comboBox2.Items.Add("USD");
            comboBox2.Items.Add("GBP");
            comboBox2.Items.Add("JPY");
        }

        private void load3()
        {
            comboBox3.Items.Add("Curent");
            comboBox3.Items.Add("Economii");
            comboBox3.Items.Add("Investitii");
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            load();
            load2();
            load3();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string IBAN = "";

            Random rnd = new Random();

            string generate(int s)
            {
                string rez = "";
                for (int i = 1; i <= s; i++)
                    rez = rez + Convert.ToString(rnd.Next(0, 10));
                return rez;
            }

            IBAN = "RO" + generate(2) + "BANK" + generate(16);
            textBox1.Text = IBAN;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                query = "INSERT INTO Conturi ([Nume titular],[IBAN],[Moneda],[Suma],[Tip cont])values('" + comboBox1.SelectedItem.ToString() + "','" + textBox1.Text + "','" + comboBox2.SelectedItem.ToString() + "','" + textBox2.Text + "','" + comboBox3.SelectedItem.ToString() + "')";
                query2 = "UPDATE Clienti SET [Numar conturi] = ([Numar conturi]+1) WHERE [Nume]='" + comboBox1.SelectedItem.ToString() + "'";
                OleDbCommand cmd = new OleDbCommand(query, conn);
                OleDbCommand cmd2 = new OleDbCommand(query2, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                Form1.f1.Message("Cont bancar inregistrat cu succes.",Color.PaleGreen);
                conn.Close();
            }
            catch (OleDbException)
            {
                Form1.f1.Message("Datele introduse sunt invalide!", Color.Red);
                conn.Close();
            }
        }
    }
}
