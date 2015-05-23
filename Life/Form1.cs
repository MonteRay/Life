using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
            public int lastNodeId;
            
            public class cNode
            {
                //public int id { get; set; }
                public int age { get; set; }
                public int gender;
            }

            public struct sector
            {
                public int nodeId;
            }

            public sector[,] field, oldField;



            public Dictionary<int,cNode> nodes;

            public cUniverse()
            {
                lastNodeId = 0;
                nodes = new Dictionary<int, cNode>();
             }

            public void genField(int width, int height, int fillFactor)
            {
                int cc;
                Random rnd = new Random();
                nodes.Clear();
                field = new sector[width, height];
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        cc = rnd.Next(101);
                        if (cc < fillFactor)
                        {
                            nodes.Add(++lastNodeId,new cNode());
                            field[x,y].nodeId=lastNodeId;
                            //result[x, y] = true;
                        }
                        else
                        {
                            //result[x, y] = false;
                        }


                    }
                }
                ;
            }
            
            public Bitmap olddrawField(int scaleFactor)
            {
                int width = field.GetLength(0);
                int height = field.GetLength(1);
                Bitmap result = new Bitmap(width * scaleFactor, height * scaleFactor);
                System.Drawing.Color color;
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (field[x, y].nodeId!=0)
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

            public void drawField(Graphics gr, int outWidth, int outHeight)
            {
                int width = field.GetLength(0);
                int height = field.GetLength(1);
                Bitmap result = new Bitmap(width, height);
                Color color;
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (field[x, y].nodeId != 0)
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
                oldField = (sector[,]) field.Clone();
                int neighborCount;
                int width = field.GetLength(0);
                int height = field.GetLength(1);
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        neighborCount = countNeighbors(x, y);
                        if (oldField[x, y].nodeId > 0)
                        {
                            if (neighborCount != 2 && neighborCount != 3)
                            {
                                //die
                                nodes.Remove(field[x, y].nodeId);
                                field[x, y].nodeId = 0;
                            }
                        }
                        else if (neighborCount == 3)
                        {
                            //born
                            nodes.Add(++lastNodeId, new cNode());
                            field[x, y].nodeId = lastNodeId;
                        }
                    }
                }
            }

            public int countNeighbors(int sx, int sy) 
            {
                if (sx >= field.GetLength(0) || sy >= field.GetLength(1)) return -1;
                int result = 0;
                int cx,cy,x,y;
                for (cx = sx - 1; cx <= sx + 1; cx++)
                {
                    for (cy = sy-1; cy <= sy+1; cy++)
                    {
                        if (sx == cx && sy == cy) continue;
                        if (cx < 0) { x = field.GetLength(0) + cx; } else if (cx > field.GetLength(0) - 1) { x = cx - field.GetLength(0); } else { x = cx; }
                        if (cy < 0) { y = field.GetLength(1) + cy; } else if (cy > field.GetLength(1) - 1) { y = cy - field.GetLength(1); } else { y = cy; }
                        if (oldField[x, y].nodeId > 0) result++; 
                        /*
                        if ($cx -lt 0) {$x=$field.width+$cx} elseif ($cx -gt $field.width-1) {$x=$cx-$field.width} else {$x=$cx}
                        if ($cy -lt 0) {$y=$field.height+$cy} elseif ($cy -gt $field.height-1) {$y=$cy-$field.height} else {$y=$cy}
                        if ($field[$x,$y]) {$NeighborCount++}
                        */
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
            int fillFactor = 30;

            int scaleFactor = decimal.ToInt32(nudScale.Value);

            int scaledWidth = width / scaleFactor;
            int scaledHeight = height / scaleFactor;

            Glob.universe = new cUniverse();
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
            int fillFactor = 30;

             
            int scaleFactor = decimal.ToInt32(nudScale.Value);
            //int scaleFactor = Math.Min(width / Glob.universe.field.GetLength(0), height / Glob.universe.field.GetLength(1));

            int scaledWidth = width/scaleFactor;
            int scaledHeight = height/scaleFactor;

            
            Glob.universe.genField(scaledWidth, scaledHeight, fillFactor);

            pictureBox1.Image = Glob.universe.olddrawField(scaleFactor);

        }

        private void startStopBtn_Click(object sender, EventArgs e)
        {
            //tmr.Enabled = !tmr.Enabled;

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
        }
    }

    

    

    

}
