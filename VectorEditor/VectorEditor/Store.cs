using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VectorEditor
{
    internal class Store : List<GraphItem>
    {
        public Store()
        {

        }

        public override string ToString()
        {
            string s = "";
            foreach (GraphItem item in this)
            {
                s+=item.GetType().Name + "; ";
            }
            return s;
        }

        public void DeleteItems(List<GraphItem> items)
        {
            
            for (int i = 0; i < items.Count; i++)
            {
                
                this.Remove(items[i]);
                items.Remove(items[i]);
                i--;
            }
        }

        public void AddItems(List<GraphItem> items)
        {
            foreach (GraphItem item in items)
            {
                this.Add(item);
            }
        }
    }

    internal class SelectionStore : List<Selection>
    {
        public Selection currSel;
        public Selection selGroup;
        public delegate void Grouping(List<GraphItem> items);
        public event Grouping AddItems;
        public event Grouping DeleteItems;
        public event SelectedItemChanged OnSelectedItemChanged;

        public SelectionStore()
        {
        }

        public bool TryGrab(int x, int y, bool ctrl)
        {
            bool a = false;
            foreach (Selection selection in this)
            {
                if (selection.IsPicked() && ctrl)
                {
                    a = true;
                    continue;
                }
                if (selection.IsActive() && selection.TryGrab(x, y))
                {
                    currSel = selection;
                    OnSelectedItemChanged.Invoke(currSel.GetType().Name);
                    a = true;
                }
            }
            return a;
        }

        public bool TryGrabGroup(int x, int y)
        {
            foreach (Selection selection in this)
            {
                if (selection is GroupSelection && selection.TryGrab(x, y))
                {
                    OnSelectedItemChanged.Invoke(selection.GetType().Name);
                    selGroup = selection;
                    return true;
                }
            }
            return false;
        }

        public void ReleaseGrab()
        {
            foreach (Selection selection in this)
            {
                selection.ReleaseGrab();
            }
        }

        public void Draw(GraphSystem system)
        {
            foreach (Selection selection in this)
            {
                if (selection.IsPicked())
                {
                    selection.Draw(system);
                }
            }
        }

        public void AddSelection(Selection sItem)
        {
            if (sItem == null)
            {
                return;
            }
                
            this.Add(sItem);
            currSel = sItem;
        }

        public bool TryDragTo(int x, int y)
        {
            if (currSel != null && currSel.IsActive())
            {
                return (currSel.TryDragTo(x, y));
            }
            foreach (Selection selection in this)
            {
                if (selection.IsActive() && selection.TryDragTo(x, y))
                {
                    currSel = selection;  OnSelectedItemChanged.Invoke(currSel.GetType().Name);
                    return true;
                }
            }
            return false;
        }

        public bool TryMove(int x, int y)
        {
            if ((currSel != null) && (currSel.IsActive()))
            {
                return (currSel.TryMove(x, y));
            }
            foreach (Selection selection in this)
            {
                if (selection.IsActive() && selection.TryMove(x, y))
                {
                    currSel = selection;
                    OnSelectedItemChanged.Invoke(currSel.GetType().Name);
                    return true;
                }
            }
            return false;
        }

        public bool MarkerHit(int x, int y)
        {
            foreach (Selection selection in this)
            {
                if (selection.IsActive() && selection.MarkerHit(x, y))
                {
                    currSel = selection;  OnSelectedItemChanged.Invoke(currSel.GetType().Name);
                    return true;
                }  
            }
            return false;
        }

        private List<GraphItem> GetSelectedItems()
        {
            List<GraphItem> items = new List<GraphItem>();
            foreach (Selection selection in this)
            {
                if (selection.IsActive() && selection.IsPicked())
                {
                    items.Add(selection.GetItem());
                }
            }
            return items;
        }

        private void DeleteSelectedItems()
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].IsPicked() && this[i].IsActive())
                {
                    this.Remove(this[i]);
                    i--;
                }  
            }
        }

        private List<GraphItem> UnGroupList()
        {
            if (!(currSel is GroupSelection))
                return null;
            Group group = currSel.GetItem() as Group;
            if (group.items == null)
                return null;
            return group.items;
        }

        public void GroupUp()
        {
            Group group = new Group(GetSelectedItems(), new Frame(-1, -1, -1, -1));
            AddItems.Invoke(new List<GraphItem>() {group});
            DeleteItems.Invoke(GetSelectedItems());
            DeleteSelectedItems();
            AddSelection(group.CreateSelection());
        }

        public bool Ungroup()
        {
            List<GraphItem> list = UnGroupList();
            if (list == null)
                return false;
            DeleteItems.Invoke(new List<GraphItem>() { currSel.GetItem() });
            this.Remove(currSel);
            AddItems.Invoke(list);
            foreach (GraphItem item in list)
            {
                AddSelection(item.CreateSelection());
            }
            return true;
        }

        public void GroupTransformation()
        {
            List<GraphItem> list;
            selGroup = currSel;
            if (!(selGroup is GroupSelection))
            {
                list = UnGroupList();
                AddItems.Invoke(list);
            }
            else
            {
                list = (selGroup.GetItem() as Group).items;
            }

            foreach (Selection selection in this)
            {
                selection.DeactivateSelection();
            }

            foreach (GraphItem item in list)
            {
                AddSelection(item.CreateSelection());
            }
            selGroup.DeactivateSelection();
            currSel = null;
        }

        public void GroupTransformationClose()
        {
            currSel = selGroup;
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].IsActive())
                {
                    this.Remove(this[i]);
                    i--;
                }
                else
                {
                    this[i].ActivateSelection();
                }
            }
        }

        public void NullCurrentSelection()
        {
            currSel = null;
        }
    }

    internal class StateStore : List<State>
    {
        public StateStore()
        {
        }
    }
}
