using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace FamilyFeud
{
    public partial class Form2 : Form
    {
        public SoundPlayer buzz;
        public int timeLeft=0;
        public Form2()
        {
            InitializeComponent();
            buzz = new SoundPlayer(FamilyFeud.Properties.Resources.buzz);
            timer1.Enabled = false;

        }

        private void Form2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 'x')
            {
                buzz.Play();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int total = 0;
            total += Int16.Parse(textBox11.Text);
            total += Int16.Parse(textBox12.Text);
            total += Int16.Parse(textBox13.Text);
            total += Int16.Parse(textBox14.Text);
            total += Int16.Parse(textBox15.Text);
            total += Int16.Parse(textBox16.Text);
            total += Int16.Parse(textBox17.Text);
            total += Int16.Parse(textBox18.Text);
            total += Int16.Parse(textBox19.Text);
            total += Int16.Parse(textBox20.Text);
            if (textBox10.Text != "")
            {
                if (total >= 200)
                {
                    label1.Text = "Total: $25,000";
                }
                else
                {
                    label1.Text = "Points: " + total.ToString() + "   Total: $" + (total * 5).ToString();
                }
            }
            else
            {
                label1.Text = "Points: " + total.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(button2.Text == "Hide Answers")
            {
                textBox1.PasswordChar = '$';
                textBox2.PasswordChar = '$';
                textBox3.PasswordChar = '$';
                textBox4.PasswordChar = '$';
                textBox5.PasswordChar = '$';
                button2.Text = "Show Answers";
            }
            else
            {
                textBox1.PasswordChar = '\0';
                textBox2.PasswordChar = '\0';
                textBox3.PasswordChar = '\0';
                textBox4.PasswordChar = '\0';
                textBox5.PasswordChar = '\0';
                button2.Text = "Hide Answers";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled) {
                DialogResult round = MessageBox.Show("Round 1?","First Round?", MessageBoxButtons.YesNo);
                if(round==DialogResult.Yes)
                {
                    timeLeft = 200;
                }
                else
                {
                    timeLeft = 250;
                }
                timer1.Enabled = true;
            }
            else {
                timer1.Enabled = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timeLeft <= 0)
            {
                timer1.Enabled = false;
                MessageBox.Show("Time's Up!");
            }
            else
            {
                timeLeft--;
                label2.Text = "Time: " + timeLeft / 10 + "." + timeLeft % 10;
            }
        }
    }
}
