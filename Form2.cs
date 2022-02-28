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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        int cont;

        private void button1_Click(object sender, EventArgs e)
        {
            string IBAN = "";

            if (checkBox1.Checked == true)
            {
                cont = 1;

                Random rnd = new Random();

                string generate(int s)
                {
                    string rez = "";
                    for (int i = 1; i <= s; i++)
                        rez = rez + Convert.ToString(rnd.Next(0, 10));
                    return rez;
                }

                IBAN = "RO" + generate(2) + "BANK" + generate(16);
            }
            else cont = 0;

            OleDbConnection conn = Form1.conn;
            OleDbCommand cmd = conn.CreateCommand();
            OleDbCommand cmd2 = conn.CreateCommand();
            conn.Open();
            cmd.CommandText = "Insert into Clienti([Nume],[Venit lunar],[Numar conturi],[Numar carduri],[Numar credite],[Credite restante],[Adresa e-mail])Values('"+textBox1.Text+"','"+textBox2.Text+"','"+Convert.ToString(cont)+"','0','0','0','"+textBox3.Text+"')";
            cmd.Connection = conn;
            try
            {
                cmd.ExecuteNonQuery();
                if (cont == 1)
                {
                    cmd2.CommandText = "INSERT INTO Conturi ([Nume titular],[IBAN],[Moneda],[Suma],[Tip cont])values('" + textBox1.Text + "','" + IBAN + "','" + "RON" + "','" + Convert.ToString(0) + "','" + "Curent" + "')";
                    cmd2.ExecuteNonQuery();
                }
                Form1.f1.Message("Client adaugat cu succes.",Color.PaleGreen);
            }
            catch (OleDbException)
            {
                Form1.f1.Message("Date introduse sunt invalide!",Color.Red); 
            }
            conn.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
