using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace VectorEditor
{
    abstract class GraphItem
    {
        public Frame Frame { get; set; }
        public double[] Multipliers { get; set; }

        public GraphItem(Frame frame)
        {
            this.Frame = frame;
            this.Multipliers = new double[4];
        }

        abstract public void Draw(GraphSystem gs);

        abstract public bool InBody(int x, int y);

        abstract public Selection CreateSelection();
    }
}
