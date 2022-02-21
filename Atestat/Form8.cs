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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        OleDbConnection conn = Form1.conn;
        string query, query2;
        string fromCurrency;
        string toCurrency;
        float amount;
        float sumaDisp;

        private void load()
        {
            query = "SELECT Nume FROM Clienti";
            OleDbCommand cmd = new OleDbCommand(query, conn);
            conn.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader.GetString(0));
                comboBox3.Items.Add(reader.GetString(0));
            }
            conn.Close();
        }

        private void load2()
        {
            query = "SELECT IBAN FROM Conturi WHERE [Nume titular]='" + comboBox1.SelectedItem.ToString() + "' AND ([Tip cont]='Curent' OR [Tip cont]='Economii')";
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
            query = "SELECT IBAN FROM Conturi WHERE [Nume titular]='" + comboBox3.SelectedItem.ToString() + "' AND ([Tip cont]='Curent' OR [Tip cont]='Economii')";
            OleDbCommand cmd = new OleDbCommand(query, conn);
            conn.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                comboBox4.Items.Add(reader.GetString(0));
            }
            conn.Close();
        }

        private void from()
        {
            query = "SELECT SUMA FROM Conturi WHERE [IBAN]='" + comboBox2.SelectedItem.ToString() + "'";
            query2 = "SELECT Moneda FROM Conturi WHERE [IBAN]='" + comboBox2.SelectedItem.ToString() + "'";
            OleDbCommand cmd = new OleDbCommand(query, conn);
            OleDbCommand cmd2 = new OleDbCommand(query2, conn);
            conn.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                sumaDisp = reader.GetFloat(0);
            }
            reader = cmd2.ExecuteReader();
            while (reader.Read())
            {
                fromCurrency = reader.GetString(0); ;
            }
            conn.Close();
        }

        private void to()
        {
            query = "SELECT Moneda FROM Conturi WHERE [IBAN]='" + comboBox4.SelectedItem.ToString() + "'";
            OleDbCommand cmd = new OleDbCommand(query, conn);
            conn.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                toCurrency = reader.GetString(0); ;
            }
            conn.Close();

        }

        private void Form8_Load(object sender, EventArgs e)
        {
            load();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            load2();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();
            load3();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            from();
            label1.Visible = true;
            label1.Text = "Suma disponibila: " + sumaDisp.ToString();
            label6.Visible = true;
            label6.Text = "Moneda: " + fromCurrency;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            to();
            label7.Visible = true;
            label7.Text = "Moneda: " + toCurrency;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                amount = float.Parse(textBox1.Text);

                if (sumaDisp >= amount)
                {
                    try
                    {
                        float exchangeRate = Exchange.GetExchangeRate(fromCurrency, toCurrency, amount);

                        string result = exchangeRate.ToString().Replace(',', '.');

                        string amountStr = amount.ToString().Replace(',', '.');

                        query = "UPDATE Conturi SET [Suma] = ([Suma]-" + amountStr + ") WHERE [IBAN]='" + comboBox2.SelectedItem.ToString() + "'";
                        query2 = "UPDATE Conturi SET [Suma] = ([Suma]+" + result + ") WHERE [IBAN]='" + comboBox4.SelectedItem.ToString() + "'";
                        OleDbCommand cmd3 = new OleDbCommand(query, conn);
                        OleDbCommand cmd4 = new OleDbCommand(query2, conn);
                        conn.Open();
                        cmd3.ExecuteNonQuery();
                        cmd4.ExecuteNonQuery();
                        Form1.f1.Message("Transfer realizat cu succes." + " Au fost transferati " + result + " " + toCurrency, Color.PaleGreen);
                        conn.Close();
                    }
                    catch (OleDbException)
                    {
                        Form1.f1.Message("Date introduse sunt invalide", Color.Red);
                        conn.Close();
                    }
                }
                else
                    Form1.f1.Message("Nu se poate transfera o suma mai mare decat cea depusa!", Color.Red);
            }
            catch(System.FormatException)
            {
                Form1.f1.Message("Date introduse sunt invalide", Color.Red);
            }
        }
    }
}
