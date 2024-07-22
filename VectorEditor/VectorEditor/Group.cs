using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VectorEditor
{
    internal class Group : GraphItem
    {
        public List<GraphItem> items;
        public Group(List<GraphItem> items, Frame frame) : base(frame)
        {
            if (items != null)
                this.items = items;
            MergeFrame();
        }

        public void MergeFrame()
        {
            foreach (GraphItem item in this.items)
            {
                this.Frame.MergeFrame(item.Frame);
            }
        }

        public void FrameUpdate()
        {
            this.Frame = items[0].Frame.Clone();
            for (int i = 0; i < this.items.Count; i++)
            {
                if (i + 1 >= this.items.Count)
                {
                    return;
                }
                this.Frame.MergeFrame(this.items[i].Frame, this.items[i+1].Frame);
            }
        }

        public override void Draw(GraphSystem gs)
        {
            if (items == null)
                return;
            foreach (GraphItem item in items)
            {
                item.Draw(gs);
            }
        }

        public void UnGroup(GraphSystem gs)
        {
            Draw(gs);
            items.Clear();
        }

        public override bool InBody(int x, int y)
        {
            int[] X = Frame.SortX();
            int[] Y = Frame.SortY();
            if (X[1] > x && Y[1] > y && X[0] < x && Y[0] < y)
            {
                return true;
            }
            return false;
        }

        public void SetMultipliers()
        {
            int width = Math.Abs(Frame.Points[0] - Frame.Points[2]);
            int height = Math.Abs(Frame.Points[1] - Frame.Points[3]);
            foreach (GraphItem item in this.items)
            {
                for (int i = 0; i < item.Frame.Points.Length; i += 2)
                {
                    item.Multipliers[i] = (double)((item.Frame.Points[i] - Frame.Points[i]) / (double)width);
                    item.Multipliers[i+1] = (double)((item.Frame.Points[i+1] - Frame.Points[i+1]) / (double)height);
                }
                if (item is Group)
                    (item as Group).SetMultipliers();
            }
        }

        public void ApplyMultipliers()
        {
            int width = Math.Abs(Frame.Points[0] - Frame.Points[2]);
            int height = Math.Abs(Frame.Points[1] - Frame.Points[3]);
            foreach (GraphItem item in this.items)
            {
                item.Frame.Points[0] = Math.Min(Frame.Points[0], Frame.Points[2]) + (int)(item.Multipliers[0] * width);
                item.Frame.Points[1] = Math.Min(Frame.Points[1], Frame.Points[3]) + (int)(item.Multipliers[1] * height);
                item.Frame.Points[2] = Math.Max(Frame.Points[0], Frame.Points[2]) + (int)(item.Multipliers[2] * width);
                item.Frame.Points[3] = Math.Max(Frame.Points[1], Frame.Points[3]) + (int)(item.Multipliers[3] * height);
                if (item is Group)
                    (item as Group).ApplyMultipliers();
            }
        }

        public override Selection CreateSelection()
        {
            return new GroupSelection(this);
        }

        public void ChangeFrame(int deltaX, int deltaY)
        {
            for (int i = 0; i < this.Frame.Points.Length; i += 2)
            {
                this.Frame.Points[i] += deltaX;
                this.Frame.Points[i + 1] += deltaY;
            }
            foreach (GraphItem item in this.items)
            {
                if (item is Group)
                {
                    (item as Group).ChangeFrame(deltaX, deltaY);
                }
                else
                {
                    item.Frame.Points[0] += deltaX;
                    item.Frame.Points[1] += deltaY;
                    item.Frame.Points[2] += deltaX;
                    item.Frame.Points[3] += deltaY;
                }
            }
        }
        //public void ChangeFrame(int deltaX, int deltaY)
        //{
        //    for (int i = 0; i < this.Frame.Points.Length; i += 2)
        //    {
        //        this.Frame.Points[i] += deltaX;
        //        this.Frame.Points[i + 1] += deltaY;
        //        foreach (GraphItem item in this.items)
        //        {
        //            item.Frame.Points[i] += deltaX;
        //            item.Frame.Points[i + 1] += deltaY;
        //            //item.Frame.Points[0] += deltaX;
        //            //item.Frame.Points[0] += deltaX;
        //            //item.Frame.Points[0] += deltaX;
        //            //item.Frame.Points[0] += deltaX;
        //            if (item is Group)
        //                (item as Group).ChangeFrame(deltaX, deltaY);
        //        }
        //    }

        //}
    }
}
