using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VectorEditor
{
    delegate void SelectedItemChanged(string itemName);
    interface ISelections
    {
        int Count { get; }
        void Release();
        bool TryDragTo(int x, int y);
        bool TryGrab(int x, int y, bool ctrl);
        bool Grouping();
        bool UnGrouping();
        bool MarkerHit(int x, int y);
        void SelectAndGrab(GraphItem item, int x, int y);
        bool TryMove(int x, int y);
        void Draw(GraphSystem system);
        void GroupTransformation();
        bool TryGrabGroup(int x, int y);
        bool IsGroupPicked();
        void GroupFrameUpdate();
        void NullCurrentSelection();
        void GroupTransformationClose();
        void Delete();
        event SelectedItemChanged OnSelectedItemChanged;
    }

    internal class SelectionController : ISelections
    {
        public int Count { get; private set; }

        public SelectionStore SelStore { get; private set;}

        private Store store;

        public event SelectedItemChanged OnSelectedItemChanged;

        public SelectionController(Store store)
        {
            SelStore = new SelectionStore();
            this.store = store;
            SelStore.AddItems += this.store.AddItems;
            SelStore.DeleteItems += this.store.DeleteItems;
            SelStore.OnSelectedItemChanged += (string s) => { OnSelectedItemChanged.Invoke(s); };
        }

        public void SelectAndGrab(GraphItem item, int x, int y)
        {
            SelStore.AddSelection(item.CreateSelection());
            SelStore.TryGrab(x, y, false);
            Count++;
        }

        public bool TryDragTo(int x, int y)
        {
            if (!SelStore.TryDragTo(x, y))
                if (!SelStore.TryMove(x, y))
                {
                    return false;
                }
            return true;
        }

        public void Release()
        {
            SelStore.ReleaseGrab();
        }

        public bool TryGrab(int x, int y, bool ctrl)
        {
            if (!SelStore.TryGrab(x, y, ctrl))
                if (!SelStore.MarkerHit(x, y))
                    return false;
            return true;
        }

        public bool TryGrabGroup(int x, int y)
        {
            return SelStore.TryGrabGroup(x, y);
        }

        public bool IsGroupPicked()
        {
            return SelStore.currSel is GroupSelection;
        }

        public bool Grouping()
        {
            SelStore.GroupUp();
            return true;
        }

        public void GroupFrameUpdate()
        {
            (SelStore.selGroup.GetItem() as Group).FrameUpdate();
        }

        public void NullCurrentSelection()
        {
            SelStore.NullCurrentSelection();
        }

        public void GroupTransformationClose()
        {
            SelStore.GroupTransformationClose();
        }

        public bool UnGrouping()
        {
            return SelStore.Ungroup();
        }

        public void Draw(GraphSystem system)
        {
            SelStore.Draw(system);
        }

        public void Delete()
        {
            for (int i = 0; i < SelStore.Count; i++)
            {
                if (SelStore[i].IsPicked())
                {
                    store.Remove(SelStore[i].GetItem());
                    SelStore.Remove(SelStore[i]);
                    i--;
                }
            }
        }

        public bool MarkerHit(int x, int y)
        {
            return SelStore.MarkerHit(x, y);
        }

        public bool TryMove(int x, int y)
        {
            return SelStore.TryMove(x, y);
        }

        public void GroupTransformation()
        {
            SelStore.GroupTransformation();
        }
    }
}
