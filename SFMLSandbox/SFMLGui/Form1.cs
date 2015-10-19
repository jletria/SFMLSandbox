using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SFMLGui {
    public partial class Form1 : Form {

        RenderWindow window;
        CircleShape shape;
        System.Timers.Timer timer;

        bool initialized = false;

        public Form1() {
            InitializeComponent();
            timer = new System.Timers.Timer(32);
            timer.Elapsed += Timer_Elapsed;

            Thread drawingThread = new Thread(new ThreadStart(DrawingThread));

        }

        private void button1_Click(object sender, EventArgs e) {
            
            timer.Start();
            //Draw();
        }

        private void Draw() {
            this.window.Draw(shape);
            this.window.Display();
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {

        }

        private void DrawingThread() {
            if (!initialized) {
                initialized = true;
                window = new RenderWindow(panel1.Handle);
                window.Clear();
                shape = new CircleShape(50);
                shape.FillColor = SFML.Graphics.Color.Red;
                shape.OutlineThickness = 10;
                shape.OutlineColor = SFML.Graphics.Color.Blue;
            }
            while (true) {

                Draw();
            }
        }
    }
}
