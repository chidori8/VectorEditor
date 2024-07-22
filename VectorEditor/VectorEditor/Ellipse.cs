using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorEditor
{
    internal class Ellipse : Figure
    {
        public Ellipse(Frame frame, PropList propList) : base(frame, propList)
        {
            this.type = FigureType.Ellipse;
        }

        protected override void DrawGeometry(GraphSystem gs)
        {
            gs.DrawEllipse(Frame.Points[0], Frame.Points[1], Frame.Points[2], Frame.Points[3]);
        }

        public override bool InBody(int x, int y)
        {
            int[] X = Frame.SortX();
            int[] Y = Frame.SortY();
            double a = Math.Abs(X[0] - X[1]) / 2;
            double b = Math.Abs(Y[0] - Y[1]) / 2;
            int x0 = (X[0] + X[1]) / 2;
            int y0 = (Y[0] + Y[1]) / 2;
            double ellipseEq = (Math.Pow((x - x0), 2)) / (Math.Pow(a, 2)) + (Math.Pow((y - y0), 2)) / (Math.Pow(b, 2));
            if (ellipseEq <= 1)
            {
                return true;
            }
            else
                return false;
        }

        public override Selection CreateSelection()
        {
            return new EllipseSelection(this);
        }
    }
}
