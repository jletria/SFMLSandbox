using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace SFMLSandbox {
    class Program {
        static RenderWindow window;
        static void Main(string[] args) {
            //window = new RenderWindow(new VideoMode(500, 500), "test");
            //window = new Window(new VideoMode(500, 500), "My window");
            window.Closed += Window_Closed;
            window.Clear();
            CircleShape shape = new CircleShape(50);
            shape.FillColor = Color.Red;
            shape.OutlineThickness = 10;
            shape.OutlineColor = Color.Blue;
            //while (window.IsOpen) {
                window.Draw(shape);
                window.Display();
            //}
            Console.ReadKey();
        }

        private static void Window_Closed(object sender, EventArgs e) {
            window.Close();
        }
    }
}