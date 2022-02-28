using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Atestat
{
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        OleDbConnection conn = Form1.conn;
        public string query, query2, query3, sum, currency;
        public DateTime expire;

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

        private void load2()
        {
            query = "SELECT IBAN FROM Conturi WHERE [Nume titular]='" + comboBox1.SelectedItem.ToString() + "' AND [Tip cont]='Curent'";
            OleDbCommand cmd = new OleDbCommand(query, conn);
            conn.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                comboBox2.Items.Add(reader.GetString(0));
            }
            conn.Close();
        }

        private void load3()
        {
            comboBox3.Items.Add("Credit");
            comboBox3.Items.Add("Debit");
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            load();
            load3();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            query = "SELECT Suma FROM Conturi WHERE [IBAN]='" + comboBox2.SelectedItem.ToString() + "'";
            query2 = "SELECT Moneda FROM Conturi WHERE [IBAN]='" + comboBox2.SelectedItem.ToString() + "'";
            OleDbCommand cmd = new OleDbCommand(query, conn);
            OleDbCommand cmd2 = new OleDbCommand(query2, conn);
            conn.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                sum = reader.GetFloat(0).ToString();
            }
            reader = cmd2.ExecuteReader();
            while (reader.Read())
            {
                currency = reader.GetString(0);
            }
            conn.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //comboBox2.Items.Clear();
            load2();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();

            string generate(int s)
            {
                string rez = "";
                for (int i = 1; i <= s; i++)
                    rez = rez + Convert.ToString(rnd.Next(0, 10));
                return rez;
            }

            string nrCard = generate(4) + "-" + generate(4) + "-" + generate(4) + "-" + generate(4);

            string cvv = generate(3);

            expire = DateTime.Today;
            expire.AddYears(4);

            //try
            //{
                query = "Insert into Carduri ([Nume titular],[Moneda],[Suma disponibila],[IBAN],[Numar card],[Data expirare],[CVV],[Tip card],[PIN]) values ('" + comboBox1.SelectedItem.ToString() + "','" + currency + "','" + textBox1.Text + "','" + comboBox2.SelectedItem.ToString() + "','" + nrCard + "','" + expire + "','" + cvv + "','" + comboBox3.SelectedItem.ToString() + "','" + textBox2.Text + "')";
                query2 = "Update Clienti Set [Numar carduri] = ([Numar carduri]+1) WHERE [Nume]='" + comboBox1.SelectedItem.ToString() + "'";
                query3 = "Update Conturi Set [Suma] = ([Suma]-" + textBox1.Text + ") where [IBAN] = '" + comboBox2.SelectedItem.ToString() + "'";
                OleDbCommand cmd = new OleDbCommand(query, conn);
                OleDbCommand cm2 = new OleDbCommand(query2, conn);
                OleDbCommand cm3 = new OleDbCommand(query3, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                cm2.ExecuteNonQuery();
                cm3.ExecuteNonQuery();
                conn.Close();
                Form1.f1.Message("Card adaugat cu succes", Color.PaleGreen);
            //}
            //catch (OleDbException)
            //{
               // Form1.f1.Message(ex, Color.Red);
            //}
        }
    }
}
