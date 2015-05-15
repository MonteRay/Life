using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Life
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //public class tField:
        //   public bool genField()
        // {
        //
        // }
        public static class Glob
        {
            public static bool[,] field;
            public static cUniverse universe;

        }

        public class cUniverse
        {
            public class cNode
            {
                //public int id { get; set; }
                public int age { get; set; }
                public int gender;
            }

            public class sector
            {
                public int nodeId;
            }

            public sector[,] field;

            public Dictionary<int,cNode> nodes;

            public cUniverse()
            {

            }

            public void genField(int width, int height, int fillFactor)
            {
                int cc;
                Random rnd = new Random();
                this.nodes.Clear();
                this.field = new sector[width, height];
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        cc = rnd.Next(101);
                        if (cc < fillFactor)
                        {
                            this.nodes.Add() !!!!!!!!!!!!!
                            result[i, j] = true;
                        }
                        else
                        {
                            result[i, j] = false;
                        }


                    }
                }
                this.field = result;
            }
            
            public Bitmap drawField(int scaleFactor)
            {
                int width = this.field.GetLength(0);
                int height = this.field.GetLength(1);
                Bitmap result = new Bitmap(width * scaleFactor, height * scaleFactor);
                System.Drawing.Color color;
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (this.field[x, y])
                        {
                            color = System.Drawing.Color.Black;
                        }
                        else
                        {
                            color = System.Drawing.Color.White;
                        }

                        for (int dx = 0; dx < scaleFactor; dx++)
                            for (int dy = 0; dy < scaleFactor; dy++)
                            {
                                result.SetPixel(x * scaleFactor + dx, y * scaleFactor + dy, color);
                            }


                    }
                }
                return result;
            }



        }


        private void BtnGen_Click(object sender, EventArgs e)
        {

            Glob.universe = new cUniverse();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Glob.universe == null) return;

            int width = pictureBox1.Width;
            int height = pictureBox1.Height;
            int fillFactor = 30;
           
            int scaleFactor = decimal.ToInt32(nudScale.Value);

            int scaledWidth = width/scaleFactor;
            int scaledHeight = height/scaleFactor;

            
            Glob.universe.genField(scaledWidth, scaledHeight, fillFactor);

            pictureBox1.Image = Glob.universe.drawField(scaleFactor);

        }

        private void startStopBtn_Click(object sender, EventArgs e)
        {
            tmr.Enabled = !tmr.Enabled;
        }
    }

    

    

    

}
