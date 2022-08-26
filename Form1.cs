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
    public partial class Form1 : Form
    {
        public string[][] answers;
        public int[][] points;
        public int round;
        public int roundScore;
        public int teamA;
        public int teamB;
        public int strikes;
        public SoundPlayer correct;
        public SoundPlayer fanfare;
        public SoundPlayer strike;
        public Form2 bonus;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bonus = new Form2();
            bonus.Hide();
            correct = new SoundPlayer(FamilyFeud.Properties.Resources.correct);
            strike  = new SoundPlayer(FamilyFeud.Properties.Resources.strike );
            fanfare  = new SoundPlayer(FamilyFeud.Properties.Resources.fanfare);
            var questionBank = System.IO.File.ReadAllLines(@"questions.txt");
            answers = new string[4][];
            answers[0] = new string[8];
            answers[1] = new string[8];
            answers[2] = new string[8];
            answers[3] = new string[8];
            points = new int[4][];
            points[0] = new int[8];
            points[1] = new int[8];
            points[2] = new int[8];
            points[3] = new int[8];
            var a = -1;
            var b = 0;
            for (int x=0;x<questionBank.Count();x++)
            {
                if (questionBank[x].StartsWith("@"))
                {
                    a++;
                    b = 0;
                }
                else
                {
                    answers[a][b] = questionBank[x].Split(',')[0];
                    points[a][b] = Int16.Parse(questionBank[x].Split(',')[1]);   
                    b++;
                }
            }
            round = -1;
            roundScore = 0;
            teamA = 0;
            teamB = 0;
            strikes = 0;
            label1.Text = "000";
            label2.Text = "000";
            label3.Text = "000";
            label4.Text = "";

            /*answers[0][0] = "Walking";
            answers[0][1] = "Running";
            answers[0][2] = "Swimming";
            answers[0][3] = "Bike Riding";
            answers[0][4] = "Aerobics";
            answers[0][5] = "Basketball";
            answers[0][6] = "Tennis";
            answers[0][7] = "Cardio";
            answers[1][0] = "Bloody";
            answers[1][1] = "Tally-Ho";
            answers[1][2] = "Cheerio";
            answers[1][3] = "Ta-Ta";
            answers[1][4] = "Tea Time";
            answers[1][5] = "Jolly Good";
            answers[1][6] = "";
            answers[1][7] = "";
            answers[2][0] = "Write";
            answers[2][1] = "Comb Hair";
            answers[2][2] = "Swing a Bat";
            answers[2][3] = "Throw a Ball";
            answers[2][4] = "";
            answers[2][5] = "";
            answers[2][6] = "";
            answers[2][7] = "";
            answers[3][0] = "Tired";
            answers[3][1] = "Rain/Weather";
            answers[3][2] = "Comfort";
            answers[3][3] = "Cold";
            answers[3][4] = "";
            answers[3][5] = "";
            answers[3][6] = "";
            answers[3][7] = "";
            points[0][0] = 24;
            points[0][1] = 20;
            points[0][2] = 16;
            points[0][3] = 9;
            points[0][4] = 7;
            points[0][5] = 6;
            points[0][6] = 5;
            points[0][7] = 3;
            points[1][0] = 20;
            points[1][1] = 14;
            points[1][2] = 13;
            points[1][3] = 11;
            points[1][4] = 8;
            points[1][5] = 7;
            points[1][6] = 0;
            points[1][7] = 0;
            points[2][0] = 51;
            points[2][1] = 20;
            points[2][2] = 8;
            points[2][3] = 6;
            points[2][4] = 0;
            points[2][5] = 0;
            points[2][6] = 0;
            points[2][7] = 0;
            points[3][0] = 24;
            points[3][1] = 19;
            points[3][2] = 16;
            points[3][3] = 13;
            points[3][4] = 0;
            points[3][5] = 0;
            points[3][6] = 0;
            points[3][7] = 0;*/
            nextRound();
            fanfare.Play();
        }

        public void showAnswer(int num)
        {
            num--;
            string answer = answers[round][num] + " | "+points[round][num].ToString();
            switch(num)
            {
                case 0:
                    button1.Text = answer;
                    button1.Enabled = false;
                    break;
                case 1:
                    button2.Text = answer;
                    button2.Enabled = false;
                    break;
                case 2:
                    button3.Text = answer;
                    button3.Enabled = false;
                    break;
                case 3:
                    button4.Text = answer;
                    button4.Enabled = false;
                    break;
                case 4:
                    button5.Text = answer;
                    button5.Enabled = false;
                    break;
                case 5:
                    button6.Text = answer;
                    button6.Enabled = false;
                    break;
                case 6:
                    button7.Text = answer;
                    button7.Enabled = false;
                    break;
                case 7:
                    button8.Text = answer;
                    button8.Enabled = false;
                    break;
            }
            if (strikes < 3)
            {
                if (round == 3)
                {
                    roundScore += points[round][num] * (round);
                }
                else
                {
                    roundScore += points[round][num] * (round + 1);
                }
            }
            label1.Text = roundScore.ToString();
            correct.Play();
        }

        public void updateStrikes(bool remove)
        {
            if (remove)
            {
                if (strikes > 0)
                {
                    strikes--;
                }
                else
                {
                    strikes = 0;
                }
            }
            else
            {
                strikes++;
                strike.Play();
            }
            switch(strikes)
            {
                case 0:
                    label4.Text = "";
                    break;
                case 1:
                    label4.Text = "X";
                    break;
                case 2:
                    label4.Text = "XX";
                    break;
                case 3:
                    label4.Text = "XXX";
                    break;
            }
            
        }
        public void nextRound()
        {
            if (round != -1)
            { 
                int teamWon = (int)MessageBox.Show("Did Team A win?", "Who Won?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (teamWon == 6)
                {
                    teamA += roundScore;
                }
                else
                {
                    teamB += roundScore;
                }
            }
            if (teamA >= 300 || teamB >= 300 || round>=3)
            {
                showBonus();
            }
            else
            {
                fanfare.Play();
                round++;
                roundScore = 0;
                strikes = 0;
                label4.Text = "";
                label1.Text = "000";
                label2.Text = teamA.ToString();
                label3.Text = teamB.ToString();
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                button6.Visible = true;
                button7.Visible = true;
                button8.Visible = true;
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;
                button7.Enabled = true;
                button8.Enabled = true;
                button1.Text = "1";
                button2.Text = "2";
                button3.Text = "3";
                button4.Text = "4";
                button5.Text = "5";
                button6.Text = "6";
                button7.Text = "7";
                button8.Text = "8";
                for (int x = 0; x < 8; x++)
                {
                    if (points[round][x] == 0)
                    {
                        switch (x)
                        {
                            case 0:
                                button1.Visible = false;
                                break;
                            case 1:
                                button2.Visible = false;
                                break;
                            case 2:
                                button3.Visible = false;
                                break;
                            case 3:
                                button4.Visible = false;
                                break;
                            case 4:
                                button5.Visible = false;
                                break;
                            case 5:
                                button6.Visible = false;
                                break;
                            case 6:
                                button7.Visible = false;
                                break;
                            case 7:
                                button8.Visible = false;
                                break;
                        }
                    }
                }
            }
        }

        private void showBonus()
        {
            bonus.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            showAnswer(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            showAnswer(2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            showAnswer(3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            showAnswer(4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            showAnswer(5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            showAnswer(6);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            showAnswer(7);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            showAnswer(8);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            nextRound();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch(e.KeyChar)
            {
                case 'x':
                    updateStrikes(false);
                    break;
                case 'z':
                    updateStrikes(true);
                    break;
                case '1':
                    if (button1.Enabled)
                    { showAnswer(1); }
                    break;
                case '2':
                    if (button2.Enabled)
                    { showAnswer(2); }
                    break;
                case '3':
                    if (button3.Enabled)
                    { showAnswer(3); }
                    break;
                case '4':
                    if (button4.Enabled)
                    { showAnswer(4); }
                    break;
                case '5':
                    if (button5.Enabled)
                    { showAnswer(5); }
                    break;
                case '6':
                    if (button6.Enabled)
                    { showAnswer(6); }
                    break;
                case '7':
                    if (button7.Enabled)
                    { showAnswer(7); }
                    break;
                case '8':
                    if (button8.Enabled)
                    { showAnswer(8); }
                    break;
                case 'n':
                    nextRound();
                    break;
                case 's':
                    fanfare.Stop();
                    break;
            }
        }
    }
}
