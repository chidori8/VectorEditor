using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace VectorEditor
{
    interface IGrController
    {
        void Repaint();
        void SetPort(Graphics graphics);
        void Clear(Color color);

        GraphSystem GetSystem();
    }
    internal class Scene : IGrController
    {
        private GraphSystem _system;
        private Store _store;

        public void Repaint()
        {
            _system.Clear(Color.White);
            if (_system == null || _store == null)
                return;
            foreach (GraphItem figure in _store)
            {
                figure.Draw(_system);
            }
        }

        public Scene(Graphics gr, Store store)
        {
            _system = new GraphSystem(gr);
            _store = store;
        }

        public void SetPort(Graphics graphics)
        {
            _system.g = graphics;
        }

        public void Clear(Color color)
        {
            _system.Clear(color);
        }

        public GraphSystem GetSystem()
        {
            return _system;
        }
    }
}
