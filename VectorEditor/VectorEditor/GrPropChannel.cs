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
    //
    interface IContourProperties
    {
        Color Color { get; set; }
        float Thickness { get; set; }
        DashStyle Style { get; set; }

    }

    interface IFillProperties
    {
        Color FillColor { get; set; }
        HatchStyle HStyle { get; set; }
    }

    interface IGraphProperties
    {
        IContourProperties ContourProperties { get; }

        IFillProperties FillProperties { get; }

    }
    internal class GrPropChannel : IGraphProperties
    {
        public IContourProperties ContourProperties { get; private set; }
        public IFillProperties FillProperties { get; private set; }

        public GrPropChannel(PropList properties)
        {
            Update(properties);
        }

        private void Update(PropList pl)
        {
            foreach (FigureProperties p in pl)
            {
                if (p is ContourProperties properties)
                {
                    ContourProperties = properties;
                }
                if (p is FillProperties properties1)
                {
                    FillProperties = properties1;
                }
            }
            //if (ContourProperties != null && FillProperties != null)
            //    return;
        }
    }
}
