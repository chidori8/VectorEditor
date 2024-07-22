using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace VectorEditor
{
    internal class Line : Figure
    {
        public Line(Frame frame, PropList propList) : base(frame, propList)
        {
            this.type = FigureType.Line;
        }

        protected override void DrawGeometry(GraphSystem gs)
        {
            gs.DrawLine(Frame.Points[0], Frame.Points[1], Frame.Points[2], Frame.Points[3]);
        }

        public override bool InBody(int x, int y)
        {
            double SELECTION_FUZZINESS = 4;
            Point rightPoint, leftPoint;
            if (Frame.Points[0] >= Frame.Points[2])
            {
                rightPoint = new Point(Frame.Points[0], Frame.Points[1]);
                leftPoint = new Point(Frame.Points[2], Frame.Points[3]);
            }
            else
            {
                leftPoint = new Point(Frame.Points[0], Frame.Points[1]);
                rightPoint = new Point(Frame.Points[2], Frame.Points[3]);
            }

            double deltaX = rightPoint.X - leftPoint.X;
            double deltaY = rightPoint.Y - leftPoint.Y;

            if (deltaX == 0 || deltaY == 0) 
                return true;
            if (x > rightPoint.X || x < leftPoint.X)
                return false;

            double slope = deltaY / deltaX;
            double offset = leftPoint.Y - leftPoint.X * slope;
            double calculatedY = x * slope + offset;

            return (y - SELECTION_FUZZINESS <= calculatedY && calculatedY <= y + SELECTION_FUZZINESS);
        }

        public override Selection CreateSelection()
        {
            return new LineSelection(this);
        }
    }
}
