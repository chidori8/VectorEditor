using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace VectorEditor
{
    internal class GraphSystem
    {
        public Graphics g;
        private HatchStyle hStyle;
        private Pen Pen { get; }
        public Color FillColor { get; set; }

        public GraphSystem(Graphics g)
        {
            this.g = g;
            Pen = new Pen(Color.Black);
        }
        public void DrawLine(int x1, int y1, int x2, int y2)
        {
            g.DrawLine(Pen, x1, y1, x2, y2);
        }
        public void ChangeContour(Color color, float thickness, DashStyle style = DashStyle.Solid)
        {
            Pen.Color = color;
            Pen.Width = thickness;
            Pen.DashStyle = style;
        }

        public void ChangeFill(Color color, HatchStyle hStyle = HatchStyle.Max)
        {
            FillColor = color;
            this.hStyle = hStyle;
            
        }

        public void DrawRectangle(int x1, int y1, int x2, int y2)
        {
            int width = Math.Abs(x2 - x1);
            int height = Math.Abs(y2 - y1);
            Rectangle rectangle = new Rectangle(Math.Min(x1, x2), Math.Min(y1, y2), width, height);
            if (FillColor != Color.Empty)
            {
                SolidBrush brush = new SolidBrush(FillColor);
                //HatchBrush brush = new HatchBrush(hStyle, FillColor);
                g.FillRectangle(brush, rectangle);
            }
            g.DrawRectangle(Pen, rectangle);
        }

        public void DrawEllipse(int x1, int y1, int x2, int y2)
        {
            int width = Math.Abs(x2 - x1);
            int height = Math.Abs(y2 - y1);
            Rectangle rectangle = new Rectangle(Math.Min(x1, x2), Math.Min(y1, y2), width, height);

            SolidBrush brush = new SolidBrush(FillColor);
            //HatchBrush brush = new HatchBrush(hStyle, FillColor);
            g.FillEllipse(brush, rectangle);
            g.DrawEllipse(Pen, rectangle);
        }

        public void Clear(Color color)
        {
            g.Clear(color);
        }
    }
}
