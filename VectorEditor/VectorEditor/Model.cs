using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace VectorEditor
{
    interface IModel
    {
        IGraphProperties GraphProperties { get; }
        IGrController GrController { get; }
        IFactory Factory { get; }

        event SelectedItemChanged OnSelectedItemChanged;
    }
    internal class Model : IModel
    {
        private GrPropChannel grPropChannel;
        public IGraphProperties GraphProperties { get { return grPropChannel; } private set { grPropChannel = value as GrPropChannel; } }

        private Scene scene;
        public IGrController GrController { get { return scene; } private set { scene = value as Scene; } }

        private Factory factory;
        public IFactory Factory { get { return factory; } private set { factory = value as Factory; } }

        private Store Store { get; }

        public event SelectedItemChanged OnSelectedItemChanged;

        //public EventHandler EventHandler { get; }

        public Model(Graphics g, PropList properties)
        {
            Store = new Store();
            GraphProperties = new GrPropChannel(properties);
            GrController = new Scene(g, Store);
            Factory = new Factory(Store, properties, GraphProperties);
            Factory.OnSelectedItemChanged += (string s) => { OnSelectedItemChanged.Invoke(s); };
        }
    }
}
