using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SFMLGui {
    public partial class Form1 : Form {

        
        RectangleShape shape;


        RenderWindow window;
        Thread drawingThread;
        VertexArray va;

        bool initialized = false;

        Dictionary<string, Color> colors;

        public Form1() {
            InitializeComponent();
            colors = new Dictionary<string, Color>();
            drawingThread = new Thread(new ThreadStart(DrawingThread));
            PopulateColors();
        }

        private void PopulateColors() {
            colors.Clear();
            List<string> skipColors = new List<string>();
            bool invalidColors = true;
            System.Drawing.Color tmpColor;
            foreach (System.Drawing.KnownColor c in Enum.GetValues(typeof(System.Drawing.KnownColor)).Cast<System.Drawing.KnownColor>()) {
                if (c == System.Drawing.KnownColor.ButtonFace) break;
                //if (c == System.Drawing.KnownColor.Black) continue;
                if (!invalidColors && !skipColors.Contains(c.ToString())) {
                    tmpColor = System.Drawing.Color.FromKnownColor(c);
                    colors.Add(c.ToString(), new Color(tmpColor.R, tmpColor.G, tmpColor.B, tmpColor.A));
                }
                if (c == System.Drawing.KnownColor.Transparent) invalidColors = false;
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            //Draw();
            if (!initialized) {
                initialized = true;
                //window = new RenderWindow(new VideoMode(500, 500), "");
                window = new RenderWindow(panel1.Handle);
                window.Clear();

                uint GridWidth = 10;
                uint GridHeight = 10;
                uint SquareWidth = (500 / GridWidth);
                uint SquareHeight = (500 / GridHeight);

                va = new VertexArray(PrimitiveType.Quads, GridWidth * GridHeight * 4);
                /*
                for(int i = 0; i < va.VertexCount; i++) {
                    va[i].
                }
                */
                List<Color> colorList = colors.Values.ToList();
                uint xcursor = 0, ycursor = 0;
                for (int y = 0; y < GridHeight; y++) {
                    for (int x = 0; x < GridWidth; x++) {
                        va.Append(new Vertex(new SFML.System.Vector2f(xcursor, ycursor), colorList[x + y]));
                        va.Append(new Vertex(new SFML.System.Vector2f(xcursor + SquareWidth, ycursor), colorList[x + y]));
                        va.Append(new Vertex(new SFML.System.Vector2f(xcursor + SquareWidth, ycursor + SquareHeight), colorList[x + y]));
                        va.Append(new Vertex(new SFML.System.Vector2f(xcursor, ycursor + SquareHeight), colorList[x + y]));
                        xcursor += SquareWidth;
                    }
                    xcursor = 0;
                    ycursor += SquareHeight;
                }
                shape = new RectangleShape(new SFML.System.Vector2f(10, 10));
                shape.FillColor = SFML.Graphics.Color.Red;
                shape.OutlineThickness = 1;
                shape.OutlineColor = SFML.Graphics.Color.Blue;
                shape.Position = new SFML.System.Vector2f(0,0);
            }
            drawingThread.Start();
        }

        private void Draw() {
            this.window.Draw(va);
            this.window.Display();
        }


        private void DrawingThread() {
            List<Color> colorList = colors.Values.ToList();
            for (int i = 0; true; i++) {
                try {
                    /*if (i == colorList.Count) i = 0;
                    shape.FillColor = colors["MidnightBlue"];
                    shape.Position = new SFML.System.Vector2f(shape.Position.X + 5, shape.Position.Y + 5);*/
                    Invoke(new Action(() => { Draw(); }));
                    System.Threading.Thread.Sleep(13);
                }
                catch (Exception e) { break; }
            }
        }


        delegate void SetTextCallback(string text);
        private void SetText(string text) {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBox1.InvokeRequired) {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else {
                this.textBox1.Text = text;
            }
        }
    }
}
