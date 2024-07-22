using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VectorEditor
{
    delegate void stateUpdate(States states);
    interface IEventHandler
    {
        bool CtrlPressed { get; }
        bool AltPressed { get; }
        void LeftMouseDown(int x, int y);
        void LeftMouseUp(int x, int y);
        void MouseMove(int x, int y);
        void KeyDown(object sender, KeyEventArgs e);
        void KeyUp(object sender, KeyEventArgs e);
        void GroupUp(object sender, EventArgs e);
        void UnGroup(object sender, EventArgs e);

        void CreateStateUp(object sender, EventArgs e);
        State State { get; set; }

        event stateUpdate StateUpdated;
    }
    internal class EventHandler : IEventHandler
    {
        private State state;
        public State State { get { return state; } set { state = value; StateUpdated?.Invoke(State.currentState); } }

        public DragState DragState { get; set; }

        public CreateState CreateState { get; set; }

        public SingleSelectState SingleSelectState { get; set; }

        public EmptyState EmptyState { get; set; }

        public MultiSelectState MultiSelectState { get; set; }

        public GroupTransformState GroupTransformState { get; set; }

        public State LastState { get; set; }

        public bool CtrlPressed { get; private set; }

        public bool AltPressed { get; private set; }

        public event stateUpdate StateUpdated;

        public EventHandler(IModel model)
        {
            DragState = new DragState(this, model);
            CreateState = new CreateState(this, model);
            SingleSelectState = new SingleSelectState(this, model);
            EmptyState = new EmptyState(this, model);
            MultiSelectState = new MultiSelectState(this, model);
            GroupTransformState = new GroupTransformState(this, model);
            LastState = new EmptyState(this, model);
            State = CreateState;
            CtrlPressed = false;
            AltPressed = false;
        }

        public void KeyDown(object sender, KeyEventArgs e)
        {
            CtrlPressed = e.Control;
            AltPressed = e.Alt;
            if (e.KeyCode == Keys.Escape)
            {
                State.EscPress();
                return;
            }
            if (e.KeyCode == Keys.Delete)
            {
                State.DelPress();
                return;
            }
            if (e.Alt)
            {
                State.AltPress();
                return;
            }
        }

        public void KeyUp(object sender, KeyEventArgs e)
        {
            CtrlPressed = e.Control;
            AltPressed = e.Alt;
            if (e.KeyCode == Keys.Menu)
                State.AltUp();
        }

        public void LeftMouseDown(int x, int y)
        {
            State.MouseDown(x, y);
        }

        public void LeftMouseUp(int x, int y)
        {
            State.MouseUp(x, y);
        }

        public void MouseMove(int x, int y)
        {
            State.MouseMove(x, y);
        }

        public void GroupUp(object sender, EventArgs e)
        {
            State.GroupUp();
        }

        public void UnGroup(object sender, EventArgs e)
        {
            State.UnGroup();
        }

        public void CreateStateUp(object sender, EventArgs e)
        {
            State = CreateState;
        }

        public void ChangeState(State state)
        {
            LastState = State;
            State = state;
        }
    }
}
