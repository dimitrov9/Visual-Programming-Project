using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Roulette
{
    public class Ball
    {
        public static readonly int RADIUS = 25;
        public Point Center { get; set; }
        public Color Color { get; set; }

        public Ball(Point center, Color color)
        {
            Center = center;
            Color = color;
        }

        public void Draw(Graphics g)
        {
            //g.Clear(Color.Transparent);
            Brush b = new SolidBrush(Color);
            g.FillEllipse(b, Center.X - RADIUS, Center.Y - RADIUS, RADIUS * 2, RADIUS * 2);
            b.Dispose();
        }

    }
}
