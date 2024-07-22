using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorEditor
{
    internal class PropList : List<FigureProperties>
    {
        public PropList()
        {
            
        }

        public PropList(params FigureProperties[] properties)
        {
            for (int i = 0; i < properties.Length; i++)
            {
                this.AddProperty(properties[i]);
            }
        }

        public void Apply(GraphSystem gs)
        {
            for (int i = 0; i < this.Count; i++)
            {
                this[i].Apply(gs);
            }
        }

        internal PropList Clone()
        {
            PropList clone = new PropList();
            //PropList pl = new PropList();
            foreach (FigureProperties p in this)
            {
                clone.Add(p.Clone());
            }
            return clone;
        }

        public void AddProperty(FigureProperties property)
        {
            if (property == null)
                return;
            foreach(var p in this)
            {
                if (p.GetType() == property.GetType())
                {
                    this.Remove(p);
                    this.Add(property);
                    return;
                }
            }
            this.Add(property);
        }
    }
}
