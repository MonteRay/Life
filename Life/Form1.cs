﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace Life
{


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _random.Next();
        }

        public enum Gender { Male, Female };

        static Random _random = new Random();

        static T RandomEnumValue<T>()
        {
            Array _values = Enum.GetValues(typeof(T));
            return (T)_values.GetValue(_random.Next(_values.Length));
        }


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


        public class Node
        {
            //public int id { get; set; }
            public int age;
            public double resource; 
            public Gender gender;
            public int x;
            public int y;

            public Node(int x, int y)
            {
                gender = RandomEnumValue<Gender>();
                this.x = x;
                this.y = y;
            }
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

            public Sector this[int x, int y] 
            {
                get 
                {
                    var _x=GetCircularCoords(x,Width);
                    var _y=GetCircularCoords(y,Height);
                    return _field[_x,_y];
                }
            }

            public int GetCircularCoords(int coord, int length)
            {
                if (length < 1) throw new ArgumentException("Размер пространства не может быть меньше 1", "length");
                if (coord < 0) return length + coord % length;
                if (coord >= length) return coord % length;
                return coord;
            } 


        }

        public class BufferedField
        {
            private readonly TorusFoldedField _first, _second;
            private bool _swapped = false;

            public int Width { get; private set; }
            public int Height { get; private set; }

            public BufferedField(int width, int height)
            {
                Width = width;
                Height = height;
                _first = new TorusFoldedField(Width,Height);
                _second = new TorusFoldedField(Width,Height);
            }

            public TorusFoldedField Front
            {
                get { return _swapped ? _second : _first; }
            }

            public TorusFoldedField Back
            {
                get { return _swapped ? _first : _second; }
            }

            public void Swap()
            {
                _swapped = !_swapped;
            }

            public Sector this[int x, int y]
            {
                get
                {
                    return Front[x, y];
                }
            }
        }

        public class Universe
        {
            public int _lastNodeId;
            public int _lastScaleFactor { get; private set; }

            public bool movingNodes;

            public bool processing { get; private set; }

            public bool nextGenerationReady;

            public Bitmap FieldScaledBitmap;

            public void preBornNode(int x, int y)
            {
                int nodeId = _lastNodeId + 1 + bornList.Count;
                bornList.Add(nodeId, new Node(x,y));
                field.Back[x, y].nodeId = nodeId;
            }

            public void bornNode(int x, int y)
            {
                nodes.Add(++_lastNodeId, new Node(x,y));
                field.Front[x, y].nodeId = _lastNodeId;
            }

            public void killNode(int x, int y)
            {

                var nodeId = field[x, y].nodeId;
                if (nodeId.HasValue)
                {
                    nodes.Remove(nodeId.Value);
                    field[x, y].nodeId = null;
                }
            }

            public void killNode(int nodeId)
            {
                killNode(nodes[nodeId].x,nodes[nodeId].y);
            }



            /*
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
                 * * /

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
                * /
            }
             */

            //public sector[,] field, oldField;
            public BufferedField field; //oldField;

            public Dictionary<int,Node> nodes;


            private Dictionary<int, Node> bornList;

            private List<int> killList;


            public Universe()
            {
                _lastNodeId = -1;
                nodes = new Dictionary<int, Node>();
                bornList = new Dictionary<int, Node>();
                killList=new List<int>();
                //FieldScaledBitmap = new Bitmap();

             }

            public void genField(int width, int height, int fillFactor)
            {
                processing = true;
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
                            bornNode(x,y);
                            //result[x, y] = true;
                        }
                        else
                        {
                            //result[x, y] = false;
                        }


                    }
                }
                processing = false;
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

            public async void drawField()
            {
                int width = field.Width;
                int height = field.Height;
                int outWidth = FieldScaledBitmap.Width;
                int outHeight = FieldScaledBitmap.Height;

                Bitmap result = new Bitmap(width, height);
                Color color;
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (field[x, y].nodeId.HasValue)
                        {
                            color = nodes[field[x, y].nodeId.Value].gender == Gender.Male ? Color.Blue : Color.Red;
                            result.SetPixel(x, y, color);
                        }
                        else
                        {
                            //color = Color.White;
                        }

                        //result.SetPixel(x, y, color);

                    }
                }
                int scaleFactor = Math.Min(outWidth / width, outHeight / height);
                if (scaleFactor < 1) scaleFactor = 1;

                _lastScaleFactor = scaleFactor;

                Graphics gr = Graphics.FromImage(FieldScaledBitmap);

                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.InterpolationMode = InterpolationMode.NearestNeighbor;

                gr.FillRectangle(new SolidBrush(Color.White), 0, 0, outWidth, outHeight);
                gr.DrawImage(result, 0, 0, width * scaleFactor, height * scaleFactor);
            }
            /*
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
                        if (field[x, y].nodeId.HasValue)
                        {
                            color = nodes[field[x,y].nodeId.Value].gender==Gender.Male ? Color.Blue : Color.Red;
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

                _lastScaleFactor = scaleFactor;

                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.InterpolationMode = InterpolationMode.NearestNeighbor;

                gr.FillRectangle(new SolidBrush(Color.White),0,0,outWidth,outHeight);
                gr.DrawImage(result, 0, 0, width * scaleFactor, height * scaleFactor);
            }
            */

            public async void prepareNextGeneration()
            {
                nextGenerationReady = false;
                processing = true;
                int neighborCount;
                int width = field.Width;
                int height = field.Height;

                //field.Swap();
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        neighborCount = countNeighbors(x, y, field.Front);
                        var nodeId = field.Front[x, y].nodeId;

                        if (nodeId.HasValue)
                        {
                            field.Back[x, y].nodeId = nodeId;
                            if (neighborCount == 2 || neighborCount == 3)
                            {

                            }
                            else
                            {
                                //die
                                killList.Add(nodeId.Value);
                            }

                        }
                        else if (neighborCount == 3)
                        {
                            //born
                            preBornNode(x, y);
                        }
                        else
                        {
                            field.Back[x, y].nodeId = null;
                        }

                    }
                }

                if (!movingNodes) { processing = false; nextGenerationReady = true; return; }
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        var nodeId = field.Back[x, y].nodeId;
                        if (nodeId.HasValue)
                        {
                            int dx = _random.Next(3) - 1;
                            int dy = _random.Next(3) - 1;
                            if (dx == 0 && dy == 0) continue;
                            if (!field.Back[x + dx, y + dy].nodeId.HasValue)
                            {
                                field.Back[x + dx, y + dy].nodeId = nodeId;
                                field.Back[x, y].nodeId = null;
                            }
                        }
                    }

                }
                processing = false;
                nextGenerationReady = true;
            }

            public async void nextGeneration()
            {

                if (!nextGenerationReady) { return;}
                field.Swap();
                //nodes=nodes.Concat(bornList).ToDictionary(e=>e.Key,e=>e.Value);
                foreach (var node in bornList)
                {
                    nodes.Add(node.Key,node.Value);
                }
                _lastNodeId += bornList.Count;
                bornList.Clear();
                foreach (int nodeId in killList)
                {
                    killNode(nodeId);
                }
                killList.Clear();

            }

             /* processing = true;
                int neighborCount;
                int width = field.Width;
                int height = field.Height;

                field.Swap();
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        neighborCount = countNeighbors(x, y, field.Back);
                        var nodeId = field.Back[x, y].nodeId;

                        if (nodeId.HasValue)
                        {
                            field[x, y].nodeId = nodeId;
                            if (neighborCount == 2 || neighborCount == 3)
                            {
                                
                            }
                            else
                            {
                                //die
                                killNode(x, y);
                            }

                        }
                        else if (neighborCount == 3)
                        {
                            //born
                            bornNode(x,y);
                        }
                        else
                        {
                            field[x, y].nodeId = null;
                        }

                    }
                }
              
                if (!movingNodes) {processing = false; return;}
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        var nodeId = field[x, y].nodeId;
                        if (nodeId.HasValue)
                        {
                            int dx = _random.Next(3) - 1;
                            int dy = _random.Next(3) - 1;
                            if (dx == 0 && dy == 0) continue;
                            if (!field[x + dx, y + dy].nodeId.HasValue)
                            {
                                field[x + dx, y + dy].nodeId = nodeId;
                                field[x, y].nodeId = null;
                            }
                        }
                    }

                }
                processing = false;
               
            } */

            public int countNeighbors(int sx, int sy, TorusFoldedField field) 
            {
                if (sx >= field.Width || sy >= field.Height) return -1;
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

            int scaleFactor = decimal.ToInt32(nudScaleFactor.Value);

            int scaledWidth = width / scaleFactor;
            int scaledHeight = height / scaleFactor;

            Glob.universe = new Universe();
            Glob.universe.genField(scaledWidth, scaledHeight, fillFactor);
            Glob.universe.movingNodes=cbMovingNodes.Checked;

            Glob.universe.FieldScaledBitmap = new Bitmap(scaledWidth*scaleFactor,scaledHeight*scaleFactor);
            Glob.universe.drawField();
            pictureBox1.Invalidate();
            //pictureBox1.Image.

            //Graphics gr = pictureBox1.CreateGraphics();
            //Glob.universe.drawField(gr,width,height);
            
            
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {

            if (Glob.universe == null) return;

            Glob.universe.prepareNextGeneration();
            Glob.universe.nextGeneration();
            Glob.universe.drawField();
            pictureBox1.Invalidate();



            //int width = pictureBox1.Width;
            //int height = pictureBox1.Height;

            //Graphics gr = pictureBox1.CreateGraphics();

            /*
            new Thread(() =>
            {
                Glob.universe.nextGeneration();
            }
                ).Start();
         
            */



        }

        private void startStopBtn_Click(object sender, EventArgs e)
        {
            
            tmr.Enabled = !tmr.Enabled;
            startStopBtn.BackColor = tmr.Enabled ? Color.LightGreen : Color.LightCoral;
       
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (Glob.universe == null) return;
            
            MouseEventArgs eventArgs = e as MouseEventArgs;
            //MessageBox.Show(eventArgs.X.ToString() + " " + eventArgs.Y.ToString());
            int scaleFactor = Glob.universe._lastScaleFactor;
            int scaledX = eventArgs.X/scaleFactor;
            int scaledY = eventArgs.Y/scaleFactor;

            if (Glob.universe.field.Front[scaledX,scaledY].nodeId.HasValue)
            {
                Glob.universe.killNode(scaledX,scaledY);
            }
            else
            {
                Glob.universe.bornNode(scaledX,scaledY);
            }

            //int width = pictureBox1.Width;
            //int height = pictureBox1.Height;
            //Graphics gr = pictureBox1.CreateGraphics();
            Glob.universe.drawField();
            pictureBox1.Invalidate();
        }

        private void nudInterval_ValueChanged(object sender, EventArgs e)
        {
            tmr.Interval = decimal.ToInt32(nudInterval.Value);
        }

        private void cbMovingNodes_CheckedChanged(object sender, EventArgs e)
        {
            if (Glob.universe == null) return;
            Glob.universe.movingNodes = cbMovingNodes.Checked;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (Glob.universe!=null && Glob.universe.FieldScaledBitmap != null) 
                e.Graphics.DrawImageUnscaled(Glob.universe.FieldScaledBitmap,0,0);

        }
    }

    

    

    

}
