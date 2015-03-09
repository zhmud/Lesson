using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework_WFA
{
    public partial class Form1 : Form
    {
        private int M;
        private int N;
        private Button[,] Btn;
        private ToolTip[,] tip;
        private Button anime;
        private int count = 0;
        public Form1()
        {
            InitializeComponent();
            M = 10;
            N = 10;
            Btn = new Button[M, N];
            tip = new ToolTip[M, N];
            Random rand = new Random();

            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Btn[i, j] = new Button();                  
                    Btn[i, j].Name = (j * 10 + i + 1).ToString();
                    Btn[i, j].Size = new Size(50, 50);
                    Btn[i, j].Location = new Point(5 + i * 50, 5 + j * 50);
                    Btn[i, j].TabIndex = 0;
                    Btn[i, j].Text = (j * 10 + i + 1).ToString();
                    Btn[i, j].BackColor = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
                    Btn[i, j].MouseDown += Button_MouseDown;

                    tip[i, j] = new ToolTip();
                    tip[i, j].IsBalloon = true;
                    tip[i, j].ToolTipIcon = ToolTipIcon.Info;
                    tip[i, j].UseFading = true;
                    tip[i, j].ToolTipTitle = String.Format("M : {0}, N : {1}", i + 1, j + 1 );
                    tip[i, j].SetToolTip(Btn[i, j], "Button position!");

                    this.Controls.Add(Btn[i, j]);
                }
            }
            
            this.Show();
        }

        void BumTimer_Tick(object sender, EventArgs e)
        {
            var BumTimer = sender as Timer;
            if (count > 8)
            {
                anime.Dispose();
                BumTimer.Enabled = false;
                count = 0;
            }
            anime.Image = Image.FromFile(String.Format(@"..\..\..\Bum\{0}.png", count));
            count++;    
        }

        void Button_MouseDown(object sender, MouseEventArgs e)
        {
            var b = sender as Button;
            switch(e.Button)
            {
                case MouseButtons.Left:
                    var BumTimer = new Timer();
                    BumTimer.Interval = 35;
                    BumTimer.Tick += BumTimer_Tick;
                    anime = b;
                    BumTimer.Start();
                    break;
                case MouseButtons.Middle:
                    this.Text = "Number : " + b.Text;
                    break;
                case MouseButtons.Right:
                    b.Enabled = false;
                    break;
            }
        }
    }
}
