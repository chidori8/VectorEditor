using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorEditor
{
    enum FigureType
    {
        Line,
        Rect,
        Ellipse
    } 

    abstract class Figure : GraphItem
    {
        private protected FigureType type;

        public PropList propList;

        public Figure(Frame frame, PropList propList) : base(frame)
        {
            this.Frame = frame;
            this.propList = propList;
        }

        public override void Draw(GraphSystem gs)
        {
            propList.Apply(gs);
            DrawGeometry(gs);
        }

        protected abstract void DrawGeometry(GraphSystem gs);

    }
}
