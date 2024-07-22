using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorEditor
{
    interface IController
    {
        IModel Model { get; }

        IEventHandler Handler { get; }
    }
    internal class Controller : IController
    {
        public IModel Model { get; }

        public IEventHandler Handler { get; }

        public Controller(IModel model) { Model = model; Handler = new EventHandler(model); }
    }
}
