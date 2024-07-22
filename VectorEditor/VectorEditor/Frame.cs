using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VectorEditor
{
    internal class Frame
    {
        public int[] Points { get; set; }
        public Frame(int x1, int y1, int x2, int y2)
        {
            Points = new int[4] {x1, y1, x2, y2};
        }

        public virtual void MergeFrame(Frame f)
        {
            if (Points[0] < 0 || Points[1] < 0 || Points[2] < 0 || Points[3] < 0)
            {
                Points[0] = f.Points[0];
                Points[1] = f.Points[1];
                Points[2] = f.Points[2];
                Points[3] = f.Points[3];
            }

            Points[0] = Math.Min(Math.Min(Points[0], Points[2]), Math.Min(f.Points[0], f.Points[2]));
            Points[1] = Math.Min(Math.Min(Points[1], Points[3]), Math.Min(f.Points[1], f.Points[3]));
            Points[2] = Math.Max(Math.Max(Points[0], Points[2]), Math.Max(f.Points[0], f.Points[2]));
            Points[3] = Math.Max(Math.Max(Points[1], Points[3]), Math.Max(f.Points[1], f.Points[3]));
        }

        public virtual void MergeFrame(Frame f1, Frame f2)
        {
            if (Points[0] < 0 || Points[1] < 0 || Points[2] < 0 || Points[3] < 0)
            {
                Points[0] = f1.Points[0];
                Points[1] = f1.Points[1];
                Points[2] = f1.Points[2];
                Points[3] = f1.Points[3];
            }

            int maxX, maxY, minX, minY, x, y;
            minX = Points[0];
            minY = Points[1];
            maxX = Points[2];
            maxY = Points[3];

            for (int i = 0; i <= Points.Length / 2; i += 2)
            {
                x = Math.Max(f2.Points[i], f1.Points[i]);
                if (maxX < x)
                    maxX = x;

                y = Math.Max(f2.Points[i + 1], f1.Points[i + 1]);
                if (maxY < y)
                    maxY = y;

                x = Math.Min(f2.Points[i], f1.Points[i]);
                if (minX > x)
                    minX = x;

                y = Math.Min(f2.Points[i + 1], f1.Points[i + 1]);
                if (minY > y)
                    minY = y;
            }
            Points[0] = minX;
            Points[1] = minY;
            Points[2] = maxX;
            Points[3] = maxY;
        }

        public Frame Clone()
        {
            return new Frame(Points[0], Points[1], Points[2], Points[3]);
        }

        public int[] SortX()
        {
            if (Points[0] == Points[2])
                return new int[] { Points[0], Points[2] };
            return new int[] { Math.Min(Points[0], Points[2]), Math.Max(Points[0], Points[2]) };
        }

        public int[] SortY()
        {
            if (Points[0] == Points[2])
                return new int[] { Points[1], Points[3] };
            return new int[] { Math.Min(Points[1], Points[3]), Math.Max(Points[1], Points[3]) };
        }
    }
}
