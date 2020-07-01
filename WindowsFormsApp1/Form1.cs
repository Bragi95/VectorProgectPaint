using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        
        Color CurrentColor = Color.Black;       
        int cnt = 0;
        Point[][] p;      
        StaticBitmap staticBitmap;
        Point CurrentPoint;
        bool mDown;
        string mode;
        bool pointFocus;
        int catchLineindex;
        int catchPointindex;
        Point start;
        public Form1()
        {         
           InitializeComponent();
            mode = "Рисуем линию";
            pointFocus = false;
            catchLineindex = -1;
            catchPointindex = -1;           
            mDown = false;          
            p = new Point[100][];
            for (int i = 0; i < 100; i++)
            {
                p[i] = new Point[2];            
            }

            staticBitmap = new StaticBitmap(10000,10000);
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }      

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            mDown = true;
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не выбрали действи");

            }
            else if (comboBox1.SelectedIndex == 0)
            {
                if (mode == "Рисуем линию")
                {
                    p[cnt][0].X = e.X;
                    p[cnt][0].Y = e.Y;                 
                }
                if (mode == "Изменяем линию")
                {
                    p[catchLineindex][catchPointindex].X = e.X;
                    p[catchLineindex][catchPointindex].Y = e.Y;
                }
            }
            else if(comboBox1.SelectedIndex == 1)
            {
               CurrentPoint = e.Location;              
                            
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
           
            if (mDown)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    if (mode == "Рисуем линию")
                    {
                        p[cnt][1].X = e.X;
                        p[cnt][1].Y = e.Y;
                    }
                    if (mode == "Изменяем линию")
                    {
                        p[catchLineindex][catchPointindex].X = e.X;
                        p[catchLineindex][catchPointindex].Y = e.Y;
                    }
                }
                else if (comboBox1.SelectedIndex == 1)
                {                                         
                        start = CurrentPoint;
                        CurrentPoint = e.Location;
                       
                        PaintPixel( start, CurrentPoint, CurrentColor);                       
                    


                }
              
            }
            else
            {
                mode = "Рисуем линию";
                pointFocus = false;
                catchLineindex = -1;
                catchPointindex = -1;
                for (int i = 0; i < cnt; i++)
                {
                    if(Math.Abs(p[i][0].X - e.X)<5 && Math.Abs(p[i][0].Y - e.Y) < 5)
                    {
                        pointFocus = true;
                        catchLineindex = i;
                        catchPointindex = 0;
                        mode = "Изменяем линию";
                    }
                    if (Math.Abs(p[i][1].X - e.X) < 5 && Math.Abs(p[i][1].Y - e.Y) < 5)
                    {
                        pointFocus = true;
                        catchLineindex = i;
                        catchPointindex = 1;
                        mode = "Изменяем линию";
                    }
                }
            }
           
           
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            mDown = false;
            if (comboBox1.SelectedIndex == 0)
            {
                if (mode == "Рисуем линию")
                {
                    p[cnt][1].X = e.X;
                    p[cnt][1].Y = e.Y;
                    cnt++;
                }
                if (mode == "Изменяем линию")
                {
                    p[catchPointindex][catchPointindex].X = e.X;
                    p[catchPointindex][catchPointindex].Y = e.Y;
                }

                mode = "Рисуем линию";
            }                     
           
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
           Graphics graphics = e.Graphics;
            for (int i = 0; i < cnt; i++)
            {
                graphics.DrawLine(new Pen(CurrentColor), p[i][0].X, p[i][0].Y, p[i][1].X, p[i][1].Y);
            }



            if (pointFocus)
            {
                graphics.DrawRectangle(new Pen(Color.Red), p[catchLineindex][catchPointindex].X - 5, p[catchLineindex][catchPointindex].Y - 5, 10, 10);

            }                    

            if(mDown)
            {                
                if (comboBox1.SelectedIndex == 0)
                {
                    graphics.DrawLine(new Pen(CurrentColor), p[cnt][0].X, p[cnt][0].Y, p[cnt][1].X, p[cnt][1].Y);                    
                } 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult D = colorDialog1.ShowDialog();
            if(D== DialogResult.OK)
            {
                CurrentColor = colorDialog1.Color;
            }
        }

        private void PaintPixel(Point start, Point CurrentPoint, Color CurrentColor)
        {
            staticBitmap.bitmap.SetPixel(start.X, start.Y, CurrentColor);
            staticBitmap.bitmap.SetPixel(CurrentPoint.X, CurrentPoint.Y, CurrentColor);
            pictureBox1.Image = staticBitmap.bitmap;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Graphics clear = pictureBox1.CreateGraphics();
            clear.Clear(SystemColors.Window);
            for (int i = 0; i < cnt; i++)
            {
                for (int j = 0; j <= 1; j++)
                {
                    p[i][j] = p[0][0];
                    
                }
            }
            staticBitmap.bitmap.Dispose();
            cnt = 0;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }       
    }
}
    

