using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VectorEditor
{
    public enum States
    {
        DRAG,
        CREATE,
        EMPTY,
        SINGLESELECT,
        MULTISELECT,
        GROUPTRANSFORM
    }

    abstract internal class State
    {

        public States currentState;

        protected IModel Model { get; }

        protected EventHandler Handler { get; set; }

        public State(EventHandler Handler, IModel Model)
        {
            if (Handler == null || Model == null)
                return;
            this.Handler = Handler;
            this.Model = Model;
        }

        abstract public void MouseMove(int x, int y);

        abstract public void MouseUp(int x, int y);

        abstract public bool MouseDown(int x, int y);

        abstract public void EscPress();

        abstract public void DelPress();

        abstract public void AltPress();

        abstract public void AltUp();

        abstract public void GroupUp();
        abstract public void UnGroup();
    }

    internal class CreateState : State
    {
        public CreateState(EventHandler Handler, IModel Model) : base(Handler, Model)
        {
            this.currentState = States.CREATE;
        }

        public override void DelPress()
        {

        }

        public override void EscPress()
        {

        }

        public override void AltPress()
        {

        }

        public override bool MouseDown(int x, int y)
        {
            Model.Factory.CreateAndGrabItem(x, y);
            //Handler.State = Handler.DragState;
            Handler.ChangeState(Handler.DragState);

            Model.GrController.Repaint();
            return true;
        }

        public override void MouseMove(int x, int y)
        {
            
        }

        public override void MouseUp(int x, int y)
        {

        }
        public override void GroupUp()
        {

        }

        public override void UnGroup()
        {

        }

        public override void AltUp()
        {

        }
    }

    internal class DragState : State
    {
        public DragState(EventHandler Handler, IModel Model) : base(Handler, Model)
        {
            this.currentState = States.DRAG;
        }

        public override bool MouseDown(int x, int y)
        {
            return true;
        }

        public override void MouseMove(int x, int y)
        {
            Model.Factory.Selections.TryDragTo(x, y);
            Model.GrController.Repaint();
            Model.Factory.Selections.Draw(Model.GrController.GetSystem());
            if (Handler.LastState == Handler.GroupTransformState)
            {
                Model.Factory.Selections.GroupFrameUpdate();
            }
        }

        public override void MouseUp(int x, int y)
        {
            Model.Factory.Selections.TryDragTo(x, y);
            Model.GrController.Repaint();
            Model.Factory.Selections.Draw(Model.GrController.GetSystem());
            Model.Factory.Selections.Release();

            
            if (Handler.LastState is CreateState)
            {
                Handler.ChangeState(Handler.EmptyState);
            }
            else
            {

                //    Model.Factory.Selections.NullCurrentSelection();
                Handler.ChangeState(Handler.LastState);

            }
        }

        public override void DelPress()
        {

        }

        public override void EscPress()
        {

        }

        public override void GroupUp()
        {

        }

        public override void UnGroup()
        {

        }

        public override void AltPress()
        {

        }

        public override void AltUp()
        {

        }
    }

    internal class SingleSelectState : State
    {
        public SingleSelectState(EventHandler Handler, IModel Model) : base(Handler, Model)
        {
            this.currentState = States.SINGLESELECT;
            
        }

        public override bool MouseDown(int x, int y)
        {
            /*if (Model.Factory.Selections.MarkerHit(x, y))
            {
                Handler.State = Handler.DragState;
                return true;
            }
            else*/ if (Model.Factory.Selections.TryGrab(x, y, Handler.CtrlPressed))
            {
                if (Handler.CtrlPressed)
                {
                    //Handler.State = Handler.MultiSelectState;
                    Handler.ChangeState(Handler.MultiSelectState);

                }
                else
                    //Handler.State = Handler.DragState;
                    Handler.ChangeState(Handler.DragState);
            }
            else
            {
                Model.Factory.Selections.Release();
                //Handler.State = Handler.EmptyState;
                Handler.ChangeState(Handler.EmptyState);

            }
            Model.GrController.Repaint();
            Model.Factory.Selections.Draw(Model.GrController.GetSystem());
            return true;
        }

        public override void MouseMove(int x, int y)
        {

        }

        public override void MouseUp(int x, int y)
        {

        }

        public override void DelPress()
        {
            Model.Factory.Selections.Delete();
            Model.GrController.Repaint();
            //Handler.State = Handler.EmptyState;
            Handler.ChangeState(Handler.EmptyState);

        }

        public override void EscPress()
        {
            Model.GrController.Repaint();
            //Handler.State = Handler.EmptyState;
            Handler.ChangeState(Handler.EmptyState);

        }

        public override void GroupUp()
        {

        }

        public override void UnGroup()
        {
            Model.Factory.Selections.UnGrouping();
            Model.GrController.Repaint();
            Model.Factory.Selections.Draw(Model.GrController.GetSystem());
            //Handler.State = Handler.MultiSelectState;
            Handler.ChangeState(Handler.MultiSelectState);

        }

        public override void AltPress()
        {
            if (Model.Factory.Selections.IsGroupPicked())
            {
                //Handler.State = Handler.GroupTransformState;
                Handler.ChangeState(Handler.GroupTransformState);

            }
            //Model.Factory.Selections.GroupTransformation(Model.GrController.GetSystem());
        }

        public override void AltUp()
        {

        }
    }
    
    internal class MultiSelectState : State
    {
        public MultiSelectState(EventHandler Handler, IModel Model) : base(Handler, Model)
        {
            this.currentState = States.MULTISELECT;
        }

        public override bool MouseDown(int x, int y)
        {
            if (Model.Factory.Selections.TryGrab(x, y, Handler.CtrlPressed))
            {
                if (!Handler.CtrlPressed)
                {
                    //Handler.State = Handler.SingleSelectState;
                    Handler.ChangeState(Handler.SingleSelectState);

                }
            }
            else
            {
                //Handler.State = Handler.EmptyState;
                Handler.ChangeState(Handler.EmptyState);

            }
            Model.GrController.Repaint();
            Model.Factory.Selections.Draw(Model.GrController.GetSystem());
            return true;
        }

        public override void MouseMove(int x, int y)
        {

        }

        public override void MouseUp(int x, int y)
        {

        }

        public override void DelPress()
        {
            Model.Factory.Selections.Delete();
            Model.GrController.Repaint();
            Handler.ChangeState(Handler.EmptyState);
        }

        public override void EscPress()
        {
            Model.GrController.Repaint();
            Handler.ChangeState(Handler.EmptyState);
        }

        public override void GroupUp()
        {
            Model.Factory.Selections.Grouping();
            Model.GrController.Repaint();
            Model.Factory.Selections.Draw(Model.GrController.GetSystem());
            //Handler.State = Handler.SingleSelectState;
            Handler.ChangeState(Handler.SingleSelectState);

        }

        public override void UnGroup()
        {

        }

        public override void AltPress()
        {

        }

        public override void AltUp()
        {

        }
    }

    internal class EmptyState : State
    {
        public EmptyState(EventHandler Handler, IModel Model) : base(Handler, Model)
        {
            this.currentState = States.EMPTY;
        }

        public override bool MouseDown(int x, int y)
        {
            if (Model.Factory.Selections.TryGrab(x, y, false))
            {
                Model.GrController.Repaint();
                Model.Factory.Selections.Draw(Model.GrController.GetSystem());
                //Handler.State = Handler.SingleSelectState;
                Handler.ChangeState(Handler.SingleSelectState);
                return true;
            }
            else
                Model.Factory.Selections.Release();
            return true;
        }

        public override void MouseMove(int x, int y)
        {

        }

        public override void MouseUp(int x, int y)
        {

        }

        public override void DelPress()
        {

        }

        public override void EscPress()
        {

        }

        public override void GroupUp()
        {

        }

        public override void UnGroup()
        {

        }

        public override void AltPress()
        {
        }

        public override void AltUp()
        {
        }
    }

    internal class GroupTransformState : State
    {
        private bool transformCreated;
        public GroupTransformState(EventHandler Handler, IModel Model) : base(Handler, Model)
        {
            this.currentState = States.GROUPTRANSFORM;
            transformCreated = false;
        }

        public override void AltPress()
        {
             
        }

        public override void DelPress()
        {
             
        }

        public override void EscPress()
        {
             
        }

        public override void GroupUp()
        {
             
        }

        public override bool MouseDown(int x, int y)
        {
            if (Model.Factory.Selections.TryGrabGroup(x, y))
            {
                
                if (!transformCreated)
                {
                    Model.Factory.Selections.GroupTransformation();
                    transformCreated = true;
                }
                if (Model.Factory.Selections.TryGrab(x, y, false))
                {
                    Model.Factory.Selections.NullCurrentSelection();
                    Handler.ChangeState(Handler.DragState);
                }
            }
            else
            {
                AltUp();
            }
            return true;
        }

        public override void MouseMove(int x, int y)
        {
             
        }

        public override void MouseUp(int x, int y)
        {
             
        }

        public override void UnGroup()
        {
             
        }

        public override void AltUp()
        {
            if (!transformCreated)
                Model.Factory.Selections.GroupTransformation();
            else
                transformCreated = false;

            Model.Factory.Selections.GroupTransformationClose();
            Handler.ChangeState(Handler.EmptyState);
        }
    }
}
