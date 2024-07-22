using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorEditor
{
    internal class Rect : Figure
    {
        public Rect(Frame frame, PropList propList) : base(frame, propList)
        {
            this.type = FigureType.Rect;
        }

        protected override void DrawGeometry(GraphSystem gs)
        {
            gs.DrawRectangle(Frame.Points[0], Frame.Points[1], Frame.Points[2], Frame.Points[3]);
        }

        public override bool InBody(int x, int y)
        {
            int[] X = Frame.SortX();
            int[] Y = Frame.SortY();
            int delta = 3;
            //if (X[1] + delta >= x && Y[1] + delta >= y && X[0] - delta <= x && Y[0] - delta <= y)

            if (X[1] + delta >= x && Y[1] + delta >= y && X[0] - delta <= x && Y[0] - delta <= y)
            {
                return true;
            }
            return false;
        }

        public override Selection CreateSelection()
        {
            return new RectSelection(this);
        }
    }
}
