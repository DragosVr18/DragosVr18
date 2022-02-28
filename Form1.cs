using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Timers;

namespace Atestat
{
    public partial class Form1 : Form
    {
        public static Form1 f1;

        public Form1()
        {
            InitializeComponent();
            f1 = this;
            AddDrag(f1);
        }

        private void AddDrag(Control Control) { Control.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DragForm_MouseDown); }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void DragForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        public static OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Banca.accdb");

        void check()
        {
            if(panel5.Visible==true)
            {
                panel5.Visible = false;
            }
        }

        static void Delay(int ms, EventHandler action)
        {
            var tmp = new System.Windows.Forms.Timer { Interval = ms };
            tmp.Tick += new EventHandler((o, e) => tmp.Enabled = false);
            tmp.Tick += action;
            tmp.Enabled = true;
        }

        public void Message(string text, Color color)
        {
            label10.ForeColor = color;
            label10.Visible = true;
            label10.Text = text;
            Delay(5000, (o, a) =>
            {
                label10.Visible = false;
                label10.Text = "";
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.TopLevel = false;
            f2.AutoScroll = true;
            check();
            panel3.Controls.Clear();
            panel3.Controls.Add(f2);
            f2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.TopLevel = false;
            f3.AutoScroll = true;
            check();
            panel3.Controls.Clear();
            panel3.Controls.Add(f3);
            f3.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.TopLevel = false;
            f4.AutoScroll = true;
            check();
            panel3.Controls.Clear();
            panel3.Controls.Add(f4);
            f4.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            f5.TopLevel = false;
            f5.AutoScroll = true;
            check();
            panel3.Controls.Clear();
            panel3.Controls.Add(f5);
            f5.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form6 f6 = new Form6();
            f6.TopLevel = false;
            f6.AutoScroll = true;
            check();
            panel3.Controls.Clear();
            panel3.Controls.Add(f6);
            f6.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form7 f7 = new Form7();
            f7.TopLevel = false;
            f7.AutoScroll = true;
            check();
            panel3.Controls.Clear();
            panel3.Controls.Add(f7);
            f7.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form8 f8 = new Form8();
            f8.TopLevel = false;
            f8.AutoScroll = true;
            check();
            panel3.Controls.Clear();
            panel3.Controls.Add(f8);
            f8.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            float exchangeRate;

            exchangeRate = Exchange.GetExchangeRate("eur", "ron", 1);
            label2.Text = exchangeRate.ToString();

            exchangeRate = Exchange.GetExchangeRate("gbp", "ron", 1);
            label3.Text = exchangeRate.ToString();

            exchangeRate = Exchange.GetExchangeRate("usd", "ron", 1);
            label4.Text = exchangeRate.ToString();

            exchangeRate = Exchange.GetExchangeRate("jpy", "ron", 1);
            label5.Text = exchangeRate.ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form9 f9 = new Form9();
            f9.TopLevel = false;
            f9.AutoScroll = true;
            check();
            panel3.Controls.Clear();
            panel3.Controls.Add(f9);
            f9.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            panel3.Controls.Add(panel5);
            panel5.Visible = true;
        }
    }
}
