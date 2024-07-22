using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VectorEditor
{
    interface IFactory
    {
        FigureType FigureType { get; set; }

        IGraphProperties GraphProperties { get; }

        ISelections Selections { get; }

        void CreateAndGrabItem(int x, int y);

        //Group CreateGroup(List<GraphItem> items);

        event SelectedItemChanged OnSelectedItemChanged;

    }
    internal class Factory : IFactory
    {
        public IGraphProperties GraphProperties { get; set; }
        
        public FigureType FigureType { get; set; }

        public ISelections Selections { get; private set; }
        private Store Store { get; set; }
        private PropList Properties { get; set; }

        public event SelectedItemChanged OnSelectedItemChanged;

        public Factory(Store store, PropList properties, IGraphProperties graphProperties)
        {
            this.Properties = properties;
            this.Store = store;
            this.GraphProperties = graphProperties;
            FigureType = FigureType.Line;
            Selections = new SelectionController(store);
            Selections.OnSelectedItemChanged += (string s) => { OnSelectedItemChanged.Invoke(s); };

        }

        public GraphItem AddFigure(int x, int y)
        {
            if (Properties == null || Store == null)
                return null;//
            GraphItem item = null;
            Frame frame = new Frame(x, y, x, y);
            switch (FigureType)
            {
                case FigureType.Line:
                    item = new Line(frame, Properties.Clone());
                    break;
                case FigureType.Rect:
                    item = new Rect(frame, Properties.Clone());
                    break;
                case FigureType.Ellipse:
                    item = new Ellipse(frame, Properties.Clone());
                    break;
            }
            return item;
        }

        public void CreateAndGrabItem(int x, int y)
        {
            GraphItem item = AddFigure(x, y);
            Store.Add(item);
            Selections.SelectAndGrab(item, x, y);
        }

        public void CreateLine()
        {
            Frame frame = new Frame(300, 300, 720, 500);
            GraphItem item = new Line(frame, Properties.Clone());
            Store.Add(item);
            Selections.SelectAndGrab(item, 300, 300);
        }

        //public Group CreateGroup(List<GraphItem> items)
        //{
        //    Group group = new Group(items, new Frame(0, 0, 0, 0));
        //    Store.Add(group);
        //    Store.DeleteItems(items);
        //    return group;
        //}
    }
}
