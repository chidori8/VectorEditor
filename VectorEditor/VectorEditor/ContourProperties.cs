using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace VectorEditor
{
    internal class ContourProperties : FigureProperties, IContourProperties
    {
        public Color Color { get; set; }
        public float Thickness { get; set; }
        public DashStyle Style { get; set; }

        public ContourProperties(Color color, float thickness, DashStyle style = DashStyle.Solid)
        {
            this.Color = color;
            this.Style = style;
            this.Thickness = thickness;
        }
        public override void Apply(GraphSystem graphSystem)
        {
            graphSystem.ChangeContour(Color, Thickness, Style);
        }

        public override FigureProperties Clone()
        {
            return new ContourProperties(Color, Thickness, Style);
        }
    }
}

