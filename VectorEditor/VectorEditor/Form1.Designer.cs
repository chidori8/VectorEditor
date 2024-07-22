namespace VectorEditor
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.unGroupButton = new System.Windows.Forms.Button();
            this.groupUpButton = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.HatchStyleBox = new System.Windows.Forms.ComboBox();
            this.DashStyles = new System.Windows.Forms.ComboBox();
            this.buttonColorDialog = new System.Windows.Forms.Button();
            this.FillColorButton = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.buttonClear = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.colorDialog2 = new System.Windows.Forms.ColorDialog();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.statusStrip1);
            this.panel1.Location = new System.Drawing.Point(0, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(845, 519);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel1_Paint);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelMouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panel1_MouseUp);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 497);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(845, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.unGroupButton);
            this.groupBox1.Controls.Add(this.groupUpButton);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.HatchStyleBox);
            this.groupBox1.Controls.Add(this.DashStyles);
            this.groupBox1.Controls.Add(this.buttonColorDialog);
            this.groupBox1.Controls.Add(this.FillColorButton);
            this.groupBox1.Controls.Add(this.trackBar1);
            this.groupBox1.Controls.Add(this.buttonClear);
            this.groupBox1.Location = new System.Drawing.Point(851, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(168, 519);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // unGroupButton
            // 
            this.unGroupButton.Location = new System.Drawing.Point(89, 251);
            this.unGroupButton.Name = "unGroupButton";
            this.unGroupButton.Size = new System.Drawing.Size(75, 23);
            this.unGroupButton.TabIndex = 12;
            this.unGroupButton.Text = "UnGroup";
            this.unGroupButton.UseVisualStyleBackColor = true;
            // 
            // groupUpButton
            // 
            this.groupUpButton.Location = new System.Drawing.Point(8, 251);
            this.groupUpButton.Name = "groupUpButton";
            this.groupUpButton.Size = new System.Drawing.Size(75, 23);
            this.groupUpButton.TabIndex = 11;
            this.groupUpButton.Text = "GroupUp";
            this.groupUpButton.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(6, 478);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(74, 21);
            this.comboBox1.TabIndex = 10;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged_1);
            // 
            // HatchStyleBox
            // 
            this.HatchStyleBox.FormattingEnabled = true;
            this.HatchStyleBox.Location = new System.Drawing.Point(75, 102);
            this.HatchStyleBox.Name = "HatchStyleBox";
            this.HatchStyleBox.Size = new System.Drawing.Size(87, 21);
            this.HatchStyleBox.TabIndex = 9;
            this.HatchStyleBox.SelectedIndexChanged += new System.EventHandler(this.HatchStyleBox_SelectedIndexChanged);
            // 
            // DashStyles
            // 
            this.DashStyles.FormattingEnabled = true;
            this.DashStyles.Location = new System.Drawing.Point(73, 24);
            this.DashStyles.Name = "DashStyles";
            this.DashStyles.Size = new System.Drawing.Size(87, 21);
            this.DashStyles.TabIndex = 8;
            this.DashStyles.SelectedIndexChanged += new System.EventHandler(this.DashStyles_SelectedIndexChanged);
            // 
            // buttonColorDialog
            // 
            this.buttonColorDialog.BackColor = System.Drawing.SystemColors.Menu;
            this.buttonColorDialog.Location = new System.Drawing.Point(8, 24);
            this.buttonColorDialog.Name = "buttonColorDialog";
            this.buttonColorDialog.Size = new System.Drawing.Size(59, 21);
            this.buttonColorDialog.TabIndex = 6;
            this.buttonColorDialog.Text = "LineColor";
            this.buttonColorDialog.UseVisualStyleBackColor = false;
            this.buttonColorDialog.Click += new System.EventHandler(this.ButtonColorDialog_Click);
            // 
            // FillColorButton
            // 
            this.FillColorButton.Location = new System.Drawing.Point(8, 102);
            this.FillColorButton.Name = "FillColorButton";
            this.FillColorButton.Size = new System.Drawing.Size(59, 21);
            this.FillColorButton.TabIndex = 5;
            this.FillColorButton.Text = "FillColor";
            this.FillColorButton.UseVisualStyleBackColor = true;
            this.FillColorButton.Click += new System.EventHandler(this.FillColorButton_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.Location = new System.Drawing.Point(7, 51);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(150, 45);
            this.trackBar1.TabIndex = 4;
            this.trackBar1.Scroll += new System.EventHandler(this.TrackBar1_Scroll);
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonClear.Location = new System.Drawing.Point(87, 478);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 21);
            this.buttonClear.TabIndex = 3;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.ButtonClear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 522);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Vector Editor";
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button FillColorButton;
        private System.Windows.Forms.ColorDialog colorDialog2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox HatchStyleBox;
        private System.Windows.Forms.ComboBox DashStyles;
        private System.Windows.Forms.Button buttonColorDialog;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button unGroupButton;
        private System.Windows.Forms.Button groupUpButton;
    }
}

