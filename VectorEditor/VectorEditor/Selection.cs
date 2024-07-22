using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace VectorEditor
{
    abstract class Selection
    {
        protected GraphItem _item;
        protected readonly int dotSize = 5;
        protected readonly int delta = 20;
        protected Point pickedP;
        protected Rect[] dots;
        protected bool isPicked;
        protected bool isActive;
        protected Point startMoveP;
        private Color nardoGray = Color.FromArgb(255,104, 106, 108);
        public Selection(GraphItem item)
        {
            isPicked = true;
            isActive = true;
            _item = item;
            startMoveP = Point.Empty;
        }

        public bool TryGrab(int x, int y)
        {
            if (_item.InBody(x, y))
            {
                isPicked = true;
                return true;
            }
            isPicked = false;
            return false;
        }

        public bool MarkerHit(int x, int y)
        {
            for (int i = 0; i < dots.Length; i++)
            {
                if (dots[i] == null)
                    continue;
                if (dots[i].InBody(x, y))
                {
                    isPicked = true;
                    return true;
                }
            }
            isPicked = false;
            return false;
        }

        virtual public bool TryDragTo(int x, int y)
        {
            DotsUpdate();
            if (pickedP == Point.Empty)
            {
                if (!TryDrag(x, y))
                    return false;
            }
            bool X = false, Y = false;
            for (int i = 0; i < _item.Frame.Points.Length; i += 2)
            {
                if (pickedP.X == _item.Frame.Points[i])
                {
                    _item.Frame.Points[i] = x;
                    pickedP.X = x;
                    X = true;
                }
                if (pickedP.Y == _item.Frame.Points[i+1])
                {
                    _item.Frame.Points[i+1] = y;
                    pickedP.Y = y;
                    Y = true;
                }
                if (X == Y == true)
                    return true;
            }
            return false;
        }

        virtual public bool TryDrag(int x, int y)
        {
            DotsUpdate();
            for (int i = 0; i < dots.Length; i++)
            {
                if (dots[i] == null)
                    continue;
                if (dots[i].InBody(x, y))
                {
                    int X = (dots[i].Frame.Points[0] + dots[i].Frame.Points[2]) / 2;
                    int Y = (dots[i].Frame.Points[1] + dots[i].Frame.Points[3]) / 2;
                    for (int j = 0; j < _item.Frame.Points.Length; j += 2)
                    {
                        if (X == _item.Frame.Points[j])
                        {
                            pickedP.X = _item.Frame.Points[j];
                        }
                        if (Y == _item.Frame.Points[j + 1])
                        {
                            pickedP.Y = _item.Frame.Points[j + 1];
                        }
                    }
                    return true;
                }
            }
            pickedP = Point.Empty;
            return false;
        }

        virtual public bool TryMove(int x, int y)
        {
            if (!isPicked)
                return false;
            if (startMoveP == Point.Empty)
            {
                startMoveP.X = x;
                startMoveP.Y = y;
            }
            int deltaX, deltaY;
            if (x < startMoveP.X)
            {
                deltaX = Math.Abs(x - startMoveP.X) * -1;
            }
            else
                deltaX = Math.Abs(x - startMoveP.X);

            if (y < startMoveP.Y)
            {
                deltaY = Math.Abs(y - startMoveP.Y) * -1;
            }
            else
                deltaY = Math.Abs(y - startMoveP.Y);

            for (int i = 0; i < _item.Frame.Points.Length; i += 2)
            {
                _item.Frame.Points[i] += deltaX;
                _item.Frame.Points[i + 1] += deltaY;
            }
            startMoveP.X = x;
            startMoveP.Y = y;
            return true;
        }

        virtual public void ReleaseGrab()
        {
            pickedP = Point.Empty;
            startMoveP = Point.Empty;
        }

        public void Draw(GraphSystem gs)
        {
            DotsUpdate();
            Rect rect = new Rect(new Frame(_item.Frame.Points[0] + (int)dotSize / 2, _item.Frame.Points[1] + (int)dotSize / 2, _item.Frame.Points[2] - (int)dotSize / 2, _item.Frame.Points[3] - (int)dotSize / 2), new PropList(new ContourProperties(nardoGray, 1.5f, System.Drawing.Drawing2D.DashStyle.Dash), new FillProperties(Color.Empty)));
            rect.Draw(gs);
            for (int i = 0; i < dots.Length; i++)
            {
                if (dots[i] == null)
                    continue;
                dots[i].Draw(gs);
            }
        }

        virtual protected void DotsUpdate()
        {
            Frame frame = _item.Frame;
            /*
            [1]---------[2]
             |           |
             |           |
             |           |
            [0]---------[3]
            */
            dots[0] = new Rect(new Frame(frame.Points[0] - dotSize, frame.Points[1] - dotSize, frame.Points[0] + dotSize, frame.Points[1] + dotSize), new PropList(new FillProperties(Color.DarkGray), new ContourProperties(Color.WhiteSmoke, 0.5f)));
            dots[1] = new Rect(new Frame(frame.Points[0] - dotSize, frame.Points[3] - dotSize, frame.Points[0] + dotSize, frame.Points[3] + dotSize), new PropList(new FillProperties(Color.DarkGray), new ContourProperties(Color.WhiteSmoke, 0.5f)));
            dots[2] = new Rect(new Frame(frame.Points[2] - dotSize, frame.Points[3] - dotSize, frame.Points[2] + dotSize, frame.Points[3] + dotSize), new PropList(new FillProperties(Color.DarkGray), new ContourProperties(Color.WhiteSmoke, 0.5f)));
            dots[3] = new Rect(new Frame(frame.Points[2] - dotSize, frame.Points[1] - dotSize, frame.Points[2] + dotSize, frame.Points[1] + dotSize), new PropList(new FillProperties(Color.DarkGray), new ContourProperties(Color.WhiteSmoke, 0.5f)));
        }

        public bool IsPicked()
        {
            return isPicked;
        }

        public bool IsActive()
        {
            return isActive;
        }

        public void DeactivateSelection()
        {
            isActive = false;
        }

        public void ActivateSelection()
        {
            isActive = true;
        }

        public GraphItem GetItem()
        {
            return _item;
        }
    }

    internal class LineSelection : Selection
    {
        public LineSelection(GraphItem item) : base(item)
        {
            dots = new Rect[2];
            DotsUpdate();
        }

        public override bool TryDragTo(int x, int y)
        {
            DotsUpdate();
            if (pickedP == Point.Empty)
            {
                if (!TryDrag(x, y))
                    return false;
            }
            for (int i = 0; i < _item.Frame.Points.Length; i+=2)
            {
                if (pickedP.X == _item.Frame.Points[i] && pickedP.Y == _item.Frame.Points[i+1])
                {
                    _item.Frame.Points[i] = x;
                    _item.Frame.Points[i+1] = y;
                    pickedP.X = x;
                    pickedP.Y = y;
                    return true;
                }
            }
            return false;
        }

        protected override void DotsUpdate()
        {
            Frame frame = _item.Frame;
            dots[0] = new Rect(new Frame(frame.Points[0] - dotSize, frame.Points[1] - dotSize, frame.Points[0] + dotSize, frame.Points[1] + dotSize), new PropList(new FillProperties(Color.DarkGray), new ContourProperties(Color.WhiteSmoke, 0.5f)));
            dots[1] = new Rect(new Frame(frame.Points[2] - dotSize, frame.Points[3] - dotSize, frame.Points[2] + dotSize, frame.Points[3] + dotSize), new PropList(new FillProperties(Color.DarkGray), new ContourProperties(Color.WhiteSmoke, 0.5f)));
        }
    }

    internal class RectSelection : Selection
    {
        public RectSelection(GraphItem item) : base(item)
        {
            dots = new Rect[4];
            DotsUpdate();
        }
    }

    internal class EllipseSelection : Selection
    {
        public EllipseSelection(GraphItem item) : base(item)
        {
            dots = new Rect[4];
            DotsUpdate();
        }
    }

    internal class GroupSelection : Selection
    {
        public GroupSelection(Group group) : base(group)
        {
            dots = new Rect[4];
            DotsUpdate();
        }

        public override bool TryDrag(int x, int y)
        {
            DotsUpdate();
            for (int i = 0; i < dots.Length; i++)
            {
                if (dots[i] == null)
                    continue;
                if (dots[i].InBody(x, y))
                {
                    int X = (dots[i].Frame.Points[0] + dots[i].Frame.Points[2]) / 2;
                    int Y = (dots[i].Frame.Points[1] + dots[i].Frame.Points[3]) / 2;
                    for (int j = 0; j < _item.Frame.Points.Length; j += 2)
                    {
                        if (X == _item.Frame.Points[j])
                        {
                            pickedP.X = _item.Frame.Points[j];
                        }
                        if (Y == _item.Frame.Points[j + 1])
                        {
                            pickedP.Y = _item.Frame.Points[j + 1];
                        }
                    }
                    (_item as Group).SetMultipliers();
                    return true;
                }
            }
            pickedP = Point.Empty;
            return false;
        }

        public override bool TryDragTo(int x, int y)
        {
            DotsUpdate();

            if (pickedP == Point.Empty)
            {
                if (!TryDrag(x, y))
                    return false;
            }

            if ((x < _item.Frame.Points[0] + delta || y < _item.Frame.Points[1] + delta) &&
                (pickedP.X != _item.Frame.Points[0] && pickedP.Y != _item.Frame.Points[1]))
                return false;

            if ((x > _item.Frame.Points[2] - delta || y > _item.Frame.Points[3] - delta) &&
                (pickedP.X != _item.Frame.Points[2] && pickedP.Y != _item.Frame.Points[3]))
                return false;

            if ((x < _item.Frame.Points[0] + delta || y < _item.Frame.Points[3] + delta) &&
                (pickedP.X != _item.Frame.Points[0] && pickedP.Y != _item.Frame.Points[3]))
                return false;

            if ((x > _item.Frame.Points[2] - delta || y < _item.Frame.Points[1] - delta) &&
                (pickedP.X != _item.Frame.Points[2] && pickedP.Y != _item.Frame.Points[1]))
                return false;

            bool X = false, Y = false;

            for (int i = 0; i < _item.Frame.Points.Length; i += 2)
            {
                if (pickedP.X == _item.Frame.Points[i])
                {
                    _item.Frame.Points[i] = x;
                    pickedP.X = x;
                    X = true;
                }
                if (pickedP.Y == _item.Frame.Points[i + 1])
                {
                    _item.Frame.Points[i + 1] = y;
                    pickedP.Y = y;
                    Y = true;
                }
            }
            if ((X == Y) == true)
            {
                (_item as Group).ApplyMultipliers();
                return true;
            }
            return false;
        }

        public override bool TryMove(int x, int y)
        {
            if (!isPicked)
                return false;
            if (startMoveP == Point.Empty)
            {
                startMoveP.X = x;
                startMoveP.Y = y;
            }
            int deltaX, deltaY;
            if (x < startMoveP.X)
            {
                deltaX = Math.Abs(x - startMoveP.X) * -1;
            }
            else
                deltaX = Math.Abs(x - startMoveP.X);

            if (y < startMoveP.Y)
            {
                deltaY = Math.Abs(y - startMoveP.Y) * -1;
            }
            else
                deltaY = Math.Abs(y - startMoveP.Y);

            startMoveP.X = x;
            startMoveP.Y = y;

            (_item as Group).ChangeFrame(deltaX, deltaY);
            (_item as Group).SetMultipliers();
            (_item as Group).ApplyMultipliers();
            return true;
        }
    }
}
