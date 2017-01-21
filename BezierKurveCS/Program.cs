//Autor: Ramona Srbecky
using System;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace Bezier
{
    public class DrawingMyBezier
    {
        PointF[] p = new PointF[3];

        public DrawingMyBezier(PointF p1, PointF p2, PointF p3)
        {
            this.p[0] = p1;
            this.p[1] = p2;
            this.p[2] = p3;
        }

        public void DrawRedPart(object sender, PaintEventArgs e)
        {
            Thread.Sleep(150);

            Pen redPen = new Pen(Color.HotPink, 3);

            e.Graphics.DrawLine(redPen, p[0], p[1]);
            e.Graphics.DrawLine(redPen, p[1], p[2]);
        }
    }
    public class BezierKurve : Form
    {
        PointF P1 = new PointF(10, 300);
        PointF P2 = new PointF(180, 50);
        PointF P3 = new PointF(320, 300);
        int n = 0;
        int nMax = 6;
        public BezierKurve()
        {
            Size = new Size(600, 500);
            this.Paint += new PaintEventHandler(drawMyLine);
            drawBezierCurv(n, P1, P2, P3);
            
        }
        private void drawBezierCurv(int n, PointF p1, PointF p2, PointF p3)
        {
            if (n == nMax)
            {
                DrawingMyBezier draw = new DrawingMyBezier(p1, p2, p3);

                this.Paint += new PaintEventHandler(draw.DrawRedPart);
            }

            else
            {

                PointF P12 = new PointF(0, 0);
                PointF P23 = new PointF(0, 0);
                PointF P123 = new PointF(0, 0);

                P12.X = p1.X + p2.X;
                P12.X =  (float)(P12.X * 0.5);
                P12.Y =  (float)(0.5 * (p1.Y  + p2.Y));
                P23.X =  (float)(0.5 * (p2.X  + p3.X));
                P23.Y =  (float)(0.5 * (p2.Y  + p3.Y));
                P123.X = (float)(0.5 * (P12.X + P23.X));
                P123.Y = (float)(0.5 * (P12.Y + P23.Y));

                n += 1;

                drawBezierCurv(n, p1, P12, P123);
                drawBezierCurv(n, P123, P23, p3);
            }
        }
        
        private void drawMyLine(object sender, PaintEventArgs e)
        {
            Pen blackPen = new Pen(Color.Black, 3);
            e.Graphics.DrawLine(blackPen, P1, P2);
            e.Graphics.DrawLine(blackPen, P2, P3);
        }

        static void Main()
        {
            Application.Run(new BezierKurve());
        }
    }
  
}
