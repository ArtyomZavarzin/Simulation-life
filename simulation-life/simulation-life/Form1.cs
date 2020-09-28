using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace simulation_life
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private bool[,] map;
        private int rows;
        private int cols;
        private int cell_size;
        Random random = new Random();
        public Form1()
        {
            InitializeComponent();
        }


        private void Start()
        {
            if (timer1.Enabled) return;

            cell_size = 1;
            rows = pictureBox1.Height / cell_size;
            cols = pictureBox1.Width / cell_size;
            map = new bool[cols, rows];


            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    map[x, y] = random.Next(100) == 0;
                }
            }

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            timer1.Start();
        }

        private void MakeStep(int x, int y, ref bool[,] next_map)
        {
            int way = random.Next(4);

            switch(way)
            {
                case 0:
                    if (x - 1 >= 0 && !next_map[x - 1, y] && !map[x - 1, y])
                    {
                        next_map[x - 1, y] = true;
                        next_map[x, y] = false;
                        map[x, y] = false;
                    } 
                    else
                    {
                        next_map[x, y] = true;
                    }
                    break;
                case 1:
                    if (y - 1 >= 0 && !next_map[x, y - 1] && !map[x, y - 1])
                    {
                        next_map[x, y - 1] = true;
                        next_map[x, y] = false;
                        map[x, y] = false;
                    }
                    else
                    {
                        next_map[x, y] = true;
                    }
                    break;
                case 2:
                    if (x + 1 < cols && !next_map[x + 1, y] && !map[x + 1, y])
                    {
                        next_map[x + 1, y] = true;
                        next_map[x, y] = false;
                        map[x, y] = false;
                    }
                    else
                    {
                        next_map[x, y] = true;
                    }
                    break;
                case 3:
                    if (y + 1 < rows && !next_map[x, y + 1] && !map[x, y + 1])
                    {
                        next_map[x, y + 1] = true;
                        next_map[x, y] = false;
                        map[x, y] = false;
                    }
                    else
                    {
                        next_map[x, y] = true;
                    }
                    break;
            }
        }

        private void NextStep()
        {
            graphics.Clear(Color.White);

            bool[,] next_map = new bool[cols, rows];

            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if (map[x, y])
                    {
                        graphics.FillRectangle(Brushes.Black, x * cell_size, y * cell_size, cell_size, cell_size);

                        MakeStep(x, y, ref next_map);
                    }
                }
            }
            pictureBox1.Refresh();

            map = next_map;
        }

        private void Stop()
        {
            if (!timer1.Enabled) return;
            timer1.Stop();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            NextStep();
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            Stop();
        }
    }
}
