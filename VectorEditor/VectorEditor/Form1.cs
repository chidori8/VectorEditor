using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VectorEditor
{
    public partial class Form1 : Form
    {
        private IController controller;
        private ToolStripLabel stateLabel;
        public Form1()
        {
            InitializeComponent();
            IModel model = new Model(panel1.CreateGraphics(), new PropList(new ContourProperties(Color.Black, 2), new FillProperties(Color.Empty)));
            model.OnSelectedItemChanged += UpdateStateLabel;
            controller = new Controller(model);
            comboBox1.DataSource = Enum.GetValues(typeof(FigureType));
            DashStyles.DataSource = Enum.GetValues(typeof(DashStyle));
            HatchStyleBox.DataSource = Enum.GetValues(typeof(HatchStyle));
            stateLabel = new ToolStripLabel();
            statusStrip1.Items.Add(stateLabel);
            statusStrip1.Items.Add(new ToolStripLabel());
            controller.Handler.StateUpdated += UpdateStateLabel;
            panel1.KeyDown += controller.Handler.KeyDown;
            panel1.KeyUp += controller.Handler.KeyUp;
            groupUpButton.Click += controller.Handler.GroupUp;
            unGroupButton.Click += controller.Handler.UnGroup;
            comboBox1.SelectedIndexChanged += controller.Handler.CreateStateUp;
            trackBar1.Scroll += controller.Handler.CreateStateUp;
            FillColorButton.Click += controller.Handler.CreateStateUp;
            buttonColorDialog.Click += controller.Handler.CreateStateUp;
            DashStyles.SelectedIndexChanged += controller.Handler.CreateStateUp;
        }

        public virtual void UpdateStateLabel(States states)
        {
            stateLabel.Text = states.ToString();
        }

        public virtual void UpdateStateLabel(string s)
        {
            statusStrip1.Items[1].Text = s;
        }

        private void Panel1_Paint(object sender, PaintEventArgs e = null)
        {
            controller.Model.GrController.Repaint();
        }

        private void PanelMouseDown(object sender, MouseEventArgs e)
        {
            controller.Handler.LeftMouseDown(e.X, e.Y);
        }

        private void Panel1_MouseUp(object sender, MouseEventArgs e)
        {
            controller.Handler.LeftMouseUp(e.X, e.Y);
            this.ActiveControl = panel1;
        }

        private void ButtonColorDialog_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            controller.Model.Factory.GraphProperties.ContourProperties.Color = colorDialog1.Color;
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            controller.Model.GrController.Clear(Color.White);
        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            controller.Model.Factory.GraphProperties.ContourProperties.Thickness = trackBar1.Value;
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            controller.Model.GrController.SetPort(panel1.CreateGraphics());
            controller.Model.GrController.Repaint();
        }

        private void FillColorButton_Click(object sender, EventArgs e)
        {
            if (colorDialog2.ShowDialog() == DialogResult.Cancel)
                return;
            controller.Model.Factory.GraphProperties.FillProperties.FillColor = colorDialog2.Color;
        }

        private void Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            controller.Handler.MouseMove(e.X, e.Y);
        }

        private void ComboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            controller.Model.Factory.FigureType = (FigureType)comboBox1.SelectedValue;

        }

        private void DashStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
            controller.Model.Factory.GraphProperties.ContourProperties.Style = (DashStyle)DashStyles.SelectedValue;
        }

        private void HatchStyleBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            controller.Model.Factory.GraphProperties.FillProperties.HStyle = (HatchStyle)HatchStyleBox.SelectedValue;
        }
    }
}