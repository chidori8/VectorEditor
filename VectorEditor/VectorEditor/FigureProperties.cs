using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace VectorEditor
{
    abstract class FigureProperties
    {
        public abstract void Apply(GraphSystem graphSystem);

        public abstract FigureProperties Clone();
    }
}
