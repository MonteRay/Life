using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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
            public static Universe universe;

        }

        public class Universe
        {
            public int lastNodeId;
            
            public class Node
            {
                //public int id { get; set; }
                public int age { get; set; }
                public int gender;
            }


            public class Field
            {
                private Sector[,,] sectors;
                private int currentLayer;

                public void swap()
                {
                    currentLayer = currentLayer > 0 ? 0 : 1;
                }

                public Field(int width, int height)
                {
                    sectors=new Sector[2,width,height];
                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            sectors[0, x, y] = new Sector();
                            sectors[1, x, y] = new Sector();
                        }
                    }
                    currentLayer = 0;
                }
                /*
                protected Field(Sector[,] sectors)
                {
                    this.sectors = sectors.Clone() as Sector[,];
                }
                 * */

                public Sector this[int cx, int cy]
                {
                    get
                    {
                        int x, y;
                        if (cx < 0) { x = width + cx % width; } else if (cx > width - 1) { x = cx % width; } else { x = cx; }
                        if (cy < 0) { y = height + cy % height; } else if (cy > height - 1) { y = cy % height; } else { y = cy; }
                        return sectors[currentLayer, x, y];
                    }
                    //set { sectors[x, y] = value; }
                }

                public int width { get { return sectors.GetLength(1); } }

                public int height {get { return sectors.GetLength(2); } }

                /*
                public Field Clone()
                {
                    return new Field(this.sectors);
                }
                */
            }

            //public Sector[,] field, oldField;
            public BufferedField field; //oldField;

            public Dictionary<int,Node> nodes;

            public Universe()
            {
                lastNodeId = -1;
                nodes = new Dictionary<int, Node>();

             }

            public void genField(int width, int height, int fillFactor)
            {
                int cc;
                Random rnd = new Random();
                nodes.Clear();
                field = new BufferedField(width,height);
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        cc = rnd.Next(101);
                        if (cc < fillFactor)
                        {
                            nodes.Add(++lastNodeId,new Node());
                            field.Front[x, y].nodeId=lastNodeId;
                            //result[x, y] = true;
                        }
                        else
                        {
                            //result[x, y] = false;
                        }


                    }
                }
            }
            
            /*
            public Bitmap olddrawField(int scaleFactor)
            {
                int width = field.width;
                int height = field.height;
                Bitmap result = new Bitmap(width * scaleFactor, height * scaleFactor);
                Color color;
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (field[x, y].nodeId.HasValue)
                        {
                            color = Color.Black;
                        }
                        else
                        {
                            color = Color.White;
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
            */

            public void drawField(Graphics gr, int outWidth, int outHeight)
            {
                int width = field.Width;
                int height = field.Height;
                Bitmap result = new Bitmap(width, height);
                Color color;
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (field.Front[x, y].nodeId.HasValue)
                        {
                            color = Color.Black;
                            result.SetPixel(x, y, color);
                        }
                        else
                        {
                            //color = Color.White;
                        }

                        //result.SetPixel(x, y, color);

                    }
                }
                int scaleFactor = Math.Min(outWidth/width,outHeight/height);
                if (scaleFactor < 1) scaleFactor = 1;

                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.InterpolationMode = InterpolationMode.NearestNeighbor;

                gr.FillRectangle(new SolidBrush(Color.White),0,0,outWidth,outHeight);
                gr.DrawImage(result, 0, 0, width * scaleFactor, height * scaleFactor);
            }

            public void nextGeneration()
            {
                field.Swap();

                int width = field.Width;
                int height = field.Height;

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        var neighborCount = countNeighbors(x, y, field.Back);
                        var oldNodeId = field.Back[x, y].nodeId;
                        if (oldNodeId.HasValue)
                        {
                            if (neighborCount == 2 || neighborCount == 3)
                            {
                                field.Front[x, y].nodeId = oldNodeId;
                            }
                            else
                            {
                                //die
                                nodes.Remove(oldNodeId.Value);
                                field.Front[x, y].nodeId = null;
                            }
                        }
                        else if (neighborCount == 3)
                        {
                            //born
                            nodes.Add(++lastNodeId, new Node());
                            field.Front[x, y].nodeId=lastNodeId;
                        }
                        else
                        {
                            field.Front[x, y].nodeId = null;
                        }

                    }
                }
            }

            public int countNeighbors(int sx, int sy, TorusFoldedField field) 
            {
                //if (sx >= field.Width || sy >= field.Height) return -1;
                var result = 0;
                for (var x = sx - 1; x <= sx + 1; x++)
                {
                    for (var y = sy-1; y <= sy+1; y++)
                    {
                        if (sx == x && sy == y) continue;
                        
                        if (field[x, y].nodeId.HasValue) result++; 
                    }
                }
                return result;
            }

        }


        private void BtnGen_Click(object sender, EventArgs e)
        {

            //if (Glob.universe == null) return;

            int width = pictureBox1.Width;
            int height = pictureBox1.Height;
            int fillFactor = decimal.ToInt32(nudFillFactor.Value);

            int scaleFactor = decimal.ToInt32(nudScale.Value);

            int scaledWidth = width / scaleFactor;
            int scaledHeight = height / scaleFactor;

            Glob.universe = new Universe();
            Glob.universe.genField(scaledWidth, scaledHeight, fillFactor);
            
            //pictureBox1.Image.

            Graphics gr = pictureBox1.CreateGraphics();
            Glob.universe.drawField(gr,width,height);
            
            //Graphics gr = Graphics.FromImage(Glob.universe.newDrawField(0));
            //Bitmap bmp = Glob.universe.drawField(0);
            //pictureBox1.Image = bmp;
            //gr.SmoothingMode=SmoothingMode.None;
            //gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
            //gr.InterpolationMode=InterpolationMode.NearestNeighbor;
            //gr.DrawImage(bmp,0,0,scaledWidth*scaleFactor,scaledHeight*scaleFactor);
            //pictureBox1.Image = Glob.universe.drawField(scaleFactor);


        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (Glob.universe == null) return;

            int width = pictureBox1.Width;
            int height = pictureBox1.Height;
            //int fillFactor = 30;

            //int scaleFactor = decimal.ToInt32(nudScale.Value);
            //int scaleFactor = Math.Min(width / Glob.universe.field.GetLength(0), Height / Glob.universe.field.GetLength(1));
            //if (scaleFactor < 1) scaleFactor = 1;
            //MessageBox.Show(scaleFactor.ToString());

            //int scaledWidth = width / scaleFactor;
            //int scaledHeight = Height / scaleFactor;


            //Glob.universe.genField(scaledWidth, scaledHeight, fillFactor);

            Glob.universe.nextGeneration();

            Graphics gr = pictureBox1.CreateGraphics();
            Glob.universe.drawField(gr, width, height);


            /*
            if (Glob.universe == null) return;

            int width = pictureBox1.Width;
            int Height = pictureBox1.Height;
            int fillFactor = 30;

             
            int scaleFactor = decimal.ToInt32(nudScale.Value);
            //int scaleFactor = Math.Min(width / Glob.universe.field.GetLength(0), Height / Glob.universe.field.GetLength(1));

            int scaledWidth = width/scaleFactor;
            int scaledHeight = Height/scaleFactor;

            
            Glob.universe.genField(scaledWidth, scaledHeight, fillFactor);

            pictureBox1.Image = Glob.universe.olddrawField(scaleFactor);
            */

        }

        private void startStopBtn_Click(object sender, EventArgs e)
        {
            tmr.Enabled = !tmr.Enabled;
        }
    }
}
