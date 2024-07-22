using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace VectorEditor
{
    internal class FillProperties : FigureProperties, IFillProperties
    {
        public Color FillColor { get; set; }
        public HatchStyle HStyle { get; set; }

        public FillProperties(Color fillColor, HatchStyle hStyle = HatchStyle.Max)
        {
            this.FillColor = fillColor;
            this.HStyle = hStyle;
        }
        public override void Apply(GraphSystem graphSystem)
        {
            graphSystem.ChangeFill(FillColor, HStyle);

            //graphSystem.FillColor = this.fillColor;
        }

        public override FigureProperties Clone()
        {
            return new FillProperties(FillColor, HStyle);
        }
    }
}
