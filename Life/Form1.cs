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

        public class Sector
            {
                public int? nodeId;

                public Sector()
                { }
            }

        public class BufferedField
        {
            // private readonly 
        }

        public class TorusFoldedField
        {
            private Sector [,] _field;

            public int Width { get; private set; }
            public int Height { get; private set; }

            public TorusFoldedField(int width, int height)
            {
                Width = width;
                Height = height;
                _field=new Sector[Width,Height];
                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        _field[x, y] = new Sector();
                    }
                }
            }

            public Sector this[x,y] 
            {
                get 
                {
                    var _x=getCircularCoords(x,Width);
                    var _y=getCircularCoords(y,Height);
                    return _field[_x,_y];
                }
            }

            public int getCircularCoords(int coord, int length)
            {
                if (length < 1) throw new ArgumentException("Размер пространства не может быть меньше 1", "length");
                if (coord < 0) return length + coord % length;
                if (coord >= length) return coord % length;
                return coord;
            } 


        }

        public class Universe
        {
            public int lastNodeId;
            public int lastScaleFactor { get; private set; }

            public void born(int x, int y)
            {
                nodes.Add(++lastNodeId, new Node());
                field[x, y].nodeId = lastNodeId;
            }

            public void kill(int x, int y)
            {

                var nodeId = field[x, y].nodeId;
                if (nodeId.HasValue)
                {
                    nodes.Remove(nodeId.Value);
                    field[x, y].nodeId = null;
                }
            }

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
                protected Field(sector[,] sectors)
                {
                    this.sectors = sectors.Clone() as sector[,];
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

            //public sector[,] field, oldField;
            public Field field; //oldField;

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
                field = new Field(width,height);
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        cc = rnd.Next(101);
                        if (cc < fillFactor)
                        {
                            born(x,y);
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
                int width = field.width;
                int height = field.height;
                Bitmap result = new Bitmap(width, height);
                Color color;
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (field[x, y].nodeId.HasValue)
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

                lastScaleFactor = scaleFactor;

                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.InterpolationMode = InterpolationMode.NearestNeighbor;

                gr.FillRectangle(new SolidBrush(Color.White),0,0,outWidth,outHeight);
                gr.DrawImage(result, 0, 0, width * scaleFactor, height * scaleFactor);
            }

            public void nextGeneration()
            {
                int neighborCount;
                int width = field.width;
                int height = field.height;

                field.swap();
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        field.swap();
                        neighborCount = countNeighbors(x, y);
                        var nodeId = field[x, y].nodeId;
                        field.swap();
                        if (nodeId.HasValue)
                        {
                            field[x, y].nodeId = nodeId;
                            if (neighborCount != 2 && neighborCount != 3)
                            {
                                //die
                                kill(x,y);
                            }
                            else
                            {
                                
                            }

                        }
                        else if (neighborCount == 3)
                        {
                            //born
                            born(x,y);
                        }

                    }
                }
            }

            public int countNeighbors(int sx, int sy) 
            {
                if (sx >= field.width || sy >= field.height) return -1;
                int result = 0;
                int x,y;
                for (x = sx - 1; x <= sx + 1; x++)
                {
                    for (y = sy-1; y <= sy+1; y++)
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
            //int scaleFactor = Math.Min(width / Glob.universe.field.GetLength(0), height / Glob.universe.field.GetLength(1));
            //if (scaleFactor < 1) scaleFactor = 1;
            //MessageBox.Show(scaleFactor.ToString());

            //int scaledWidth = width / scaleFactor;
            //int scaledHeight = height / scaleFactor;


            //Glob.universe.genField(scaledWidth, scaledHeight, fillFactor);

            Glob.universe.nextGeneration();

            Graphics gr = pictureBox1.CreateGraphics();
            Glob.universe.drawField(gr, width, height);


            /*
            if (Glob.universe == null) return;

            int width = pictureBox1.Width;
            int height = pictureBox1.Height;
            int fillFactor = 30;

             
            int scaleFactor = decimal.ToInt32(nudScale.Value);
            //int scaleFactor = Math.Min(width / Glob.universe.field.GetLength(0), height / Glob.universe.field.GetLength(1));

            int scaledWidth = width/scaleFactor;
            int scaledHeight = height/scaleFactor;

            
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
