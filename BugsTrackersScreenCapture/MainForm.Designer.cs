namespace BugsTrackersScreenCapture
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonCapture = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonSend = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.radioButtonArea = new System.Windows.Forms.RadioButton();
            this.radioButtonForeground = new System.Windows.Forms.RadioButton();
            this.radioButtonDesktop = new System.Windows.Forms.RadioButton();
            this.numericUpDownDelay = new System.Windows.Forms.NumericUpDown();
            this.labelDelay = new System.Windows.Forms.Label();
            this.labelBugId = new System.Windows.Forms.Label();
            this.radioButtonUpdateExisting = new System.Windows.Forms.RadioButton();
            this.radioButtonCreateNew = new System.Windows.Forms.RadioButton();
            this.textBoxShortDescription = new System.Windows.Forms.TextBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBoxBugId = new BugsTrackersScreenCapture.NumericTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonSaveAs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonCopy = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripComboBoxPenType = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButtonUndo = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAbout = new System.Windows.Forms.ToolStripButton();
            this.toolStripConfigure = new System.Windows.Forms.ToolStripButton();
            this.toolStripExit = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelay)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Gray;
            this.pictureBox1.Location = new System.Drawing.Point(-1, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(138, 125);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // buttonCapture
            // 
            this.buttonCapture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCapture.ForeColor = System.Drawing.Color.Blue;
            this.buttonCapture.Location = new System.Drawing.Point(843, 31);
            this.buttonCapture.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCapture.Name = "buttonCapture";
            this.buttonCapture.Size = new System.Drawing.Size(173, 48);
            this.buttonCapture.TabIndex = 0;
            this.buttonCapture.Text = "Capture";
            this.buttonCapture.UseVisualStyleBackColor = true;
            this.buttonCapture.Click += new System.EventHandler(this.buttonCapture_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "BugTracker Screen Capture";
            this.notifyIcon1.Visible = true;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoScroll = true;
            this.panel2.AutoScrollMinSize = new System.Drawing.Size(48, 48);
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(12, 28);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.MinimumSize = new System.Drawing.Size(799, 590);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(799, 590);
            this.panel2.TabIndex = 4;
            // 
            // buttonSend
            // 
            this.buttonSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSend.ForeColor = System.Drawing.Color.Blue;
            this.buttonSend.Location = new System.Drawing.Point(843, 543);
            this.buttonSend.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(173, 48);
            this.buttonSend.TabIndex = 1;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // radioButtonArea
            // 
            this.radioButtonArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonArea.AutoSize = true;
            this.radioButtonArea.Checked = true;
            this.radioButtonArea.Location = new System.Drawing.Point(14, 6);
            this.radioButtonArea.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonArea.Name = "radioButtonArea";
            this.radioButtonArea.Size = new System.Drawing.Size(101, 21);
            this.radioButtonArea.TabIndex = 0;
            this.radioButtonArea.TabStop = true;
            this.radioButtonArea.Text = "Select area";
            this.radioButtonArea.UseVisualStyleBackColor = true;
            this.radioButtonArea.CheckedChanged += new System.EventHandler(this.radioButtonArea_CheckedChanged);
            // 
            // radioButtonForeground
            // 
            this.radioButtonForeground.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonForeground.AutoSize = true;
            this.radioButtonForeground.Location = new System.Drawing.Point(14, 31);
            this.radioButtonForeground.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonForeground.Name = "radioButtonForeground";
            this.radioButtonForeground.Size = new System.Drawing.Size(152, 21);
            this.radioButtonForeground.TabIndex = 1;
            this.radioButtonForeground.TabStop = true;
            this.radioButtonForeground.Text = "Foreground window";
            this.radioButtonForeground.UseVisualStyleBackColor = true;
            this.radioButtonForeground.CheckedChanged += new System.EventHandler(this.radioButtonForeground_CheckedChanged);
            // 
            // radioButtonDesktop
            // 
            this.radioButtonDesktop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonDesktop.AutoSize = true;
            this.radioButtonDesktop.Location = new System.Drawing.Point(14, 55);
            this.radioButtonDesktop.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonDesktop.Name = "radioButtonDesktop";
            this.radioButtonDesktop.Size = new System.Drawing.Size(123, 21);
            this.radioButtonDesktop.TabIndex = 2;
            this.radioButtonDesktop.TabStop = true;
            this.radioButtonDesktop.Text = "Current screen";
            this.radioButtonDesktop.UseVisualStyleBackColor = true;
            this.radioButtonDesktop.CheckedChanged += new System.EventHandler(this.radioButtonDesktop_CheckedChanged);
            // 
            // numericUpDownDelay
            // 
            this.numericUpDownDelay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownDelay.Location = new System.Drawing.Point(52, 85);
            this.numericUpDownDelay.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownDelay.Name = "numericUpDownDelay";
            this.numericUpDownDelay.Size = new System.Drawing.Size(53, 22);
            this.numericUpDownDelay.TabIndex = 3;
            // 
            // labelDelay
            // 
            this.labelDelay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDelay.AutoSize = true;
            this.labelDelay.Location = new System.Drawing.Point(2, 87);
            this.labelDelay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDelay.Name = "labelDelay";
            this.labelDelay.Size = new System.Drawing.Size(171, 17);
            this.labelDelay.TabIndex = 16;
            this.labelDelay.Text = "Delay                  Seconds";
            // 
            // labelBugId
            // 
            this.labelBugId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelBugId.AutoSize = true;
            this.labelBugId.Location = new System.Drawing.Point(14, 57);
            this.labelBugId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelBugId.Name = "labelBugId";
            this.labelBugId.Size = new System.Drawing.Size(62, 17);
            this.labelBugId.TabIndex = 19;
            this.labelBugId.Text = "Bug ID#:";
            // 
            // radioButtonUpdateExisting
            // 
            this.radioButtonUpdateExisting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonUpdateExisting.AutoSize = true;
            this.radioButtonUpdateExisting.Location = new System.Drawing.Point(14, 28);
            this.radioButtonUpdateExisting.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonUpdateExisting.Name = "radioButtonUpdateExisting";
            this.radioButtonUpdateExisting.Size = new System.Drawing.Size(154, 21);
            this.radioButtonUpdateExisting.TabIndex = 1;
            this.radioButtonUpdateExisting.Text = "Update existing bug";
            this.radioButtonUpdateExisting.UseVisualStyleBackColor = true;
            this.radioButtonUpdateExisting.CheckedChanged += new System.EventHandler(this.radioButtonUpdateExisting_CheckedChanged);
            // 
            // radioButtonCreateNew
            // 
            this.radioButtonCreateNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonCreateNew.AutoSize = true;
            this.radioButtonCreateNew.Checked = true;
            this.radioButtonCreateNew.Location = new System.Drawing.Point(14, 4);
            this.radioButtonCreateNew.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonCreateNew.Name = "radioButtonCreateNew";
            this.radioButtonCreateNew.Size = new System.Drawing.Size(128, 21);
            this.radioButtonCreateNew.TabIndex = 0;
            this.radioButtonCreateNew.TabStop = true;
            this.radioButtonCreateNew.Text = "Create new bug";
            this.radioButtonCreateNew.UseVisualStyleBackColor = true;
            this.radioButtonCreateNew.CheckedChanged += new System.EventHandler(this.radioButtonCreateNew_CheckedChanged);
            // 
            // textBoxShortDescription
            // 
            this.textBoxShortDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxShortDescription.Location = new System.Drawing.Point(835, 247);
            this.textBoxShortDescription.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxShortDescription.MaxLength = 200;
            this.textBoxShortDescription.Multiline = true;
            this.textBoxShortDescription.Name = "textBoxShortDescription";
            this.textBoxShortDescription.Size = new System.Drawing.Size(181, 152);
            this.textBoxShortDescription.TabIndex = 2;
            // 
            // labelDescription
            // 
            this.labelDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(824, 228);
            this.labelDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(83, 17);
            this.labelDescription.TabIndex = 22;
            this.labelDescription.Text = "Description:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.numericUpDownDelay);
            this.panel1.Controls.Add(this.labelDelay);
            this.panel1.Controls.Add(this.radioButtonForeground);
            this.panel1.Controls.Add(this.radioButtonDesktop);
            this.panel1.Controls.Add(this.radioButtonArea);
            this.panel1.Location = new System.Drawing.Point(835, 84);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(181, 124);
            this.panel1.TabIndex = 23;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.radioButtonCreateNew);
            this.panel3.Controls.Add(this.textBoxBugId);
            this.panel3.Controls.Add(this.labelBugId);
            this.panel3.Controls.Add(this.radioButtonUpdateExisting);
            this.panel3.Location = new System.Drawing.Point(835, 427);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(181, 92);
            this.panel3.TabIndex = 24;
            // 
            // textBoxBugId
            // 
            this.textBoxBugId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBugId.Location = new System.Drawing.Point(75, 53);
            this.textBoxBugId.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxBugId.Name = "textBoxBugId";
            this.textBoxBugId.Size = new System.Drawing.Size(84, 22);
            this.textBoxBugId.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonSaveAs,
            this.toolStripSeparator3,
            this.toolStripButtonCopy,
            this.toolStripSeparator1,
            this.toolStripComboBoxPenType,
            this.toolStripButtonUndo,
            this.toolStripButtonAbout,
            this.toolStripConfigure,
            this.toolStripExit});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 53, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1056, 26);
            this.toolStrip1.TabIndex = 25;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonSaveAs
            // 
            this.toolStripButtonSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonSaveAs.ForeColor = System.Drawing.Color.Blue;
            this.toolStripButtonSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSaveAs.Image")));
            this.toolStripButtonSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSaveAs.Name = "toolStripButtonSaveAs";
            this.toolStripButtonSaveAs.Size = new System.Drawing.Size(79, 23);
            this.toolStripButtonSaveAs.Text = "Save as...";
            this.toolStripButtonSaveAs.Click += new System.EventHandler(this.toolStripButtonSaveAs_Click_1);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 26);
            // 
            // toolStripButtonCopy
            // 
            this.toolStripButtonCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonCopy.ForeColor = System.Drawing.Color.Blue;
            this.toolStripButtonCopy.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCopy.Image")));
            this.toolStripButtonCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCopy.Name = "toolStripButtonCopy";
            this.toolStripButtonCopy.Size = new System.Drawing.Size(126, 23);
            this.toolStripButtonCopy.Text = "Copy to Clipboard";
            this.toolStripButtonCopy.Click += new System.EventHandler(this.toolStripButtonCopy_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 26);
            // 
            // toolStripComboBoxPenType
            // 
            this.toolStripComboBoxPenType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxPenType.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.toolStripComboBoxPenType.Items.AddRange(new object[] {
            "red arrow",
            "red marker",
            "yellow highlighter"});
            this.toolStripComboBoxPenType.Name = "toolStripComboBoxPenType";
            this.toolStripComboBoxPenType.Size = new System.Drawing.Size(160, 26);
            // 
            // toolStripButtonUndo
            // 
            this.toolStripButtonUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonUndo.ForeColor = System.Drawing.Color.Blue;
            this.toolStripButtonUndo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonUndo.Image")));
            this.toolStripButtonUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUndo.Name = "toolStripButtonUndo";
            this.toolStripButtonUndo.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.toolStripButtonUndo.Size = new System.Drawing.Size(86, 23);
            this.toolStripButtonUndo.Text = "Undo Line";
            this.toolStripButtonUndo.Click += new System.EventHandler(this.toolStripButtonUndo_Click);
            // 
            // toolStripButtonAbout
            // 
            this.toolStripButtonAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonAbout.ForeColor = System.Drawing.Color.Blue;
            this.toolStripButtonAbout.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAbout.Image")));
            this.toolStripButtonAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAbout.Name = "toolStripButtonAbout";
            this.toolStripButtonAbout.Padding = new System.Windows.Forms.Padding(70, 0, 0, 0);
            this.toolStripButtonAbout.Size = new System.Drawing.Size(135, 23);
            this.toolStripButtonAbout.Text = "About...";
            this.toolStripButtonAbout.Click += new System.EventHandler(this.toolStripButtonAbout_Click);
            // 
            // toolStripConfigure
            // 
            this.toolStripConfigure.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripConfigure.ForeColor = System.Drawing.Color.Blue;
            this.toolStripConfigure.Image = ((System.Drawing.Image)(resources.GetObject("toolStripConfigure.Image")));
            this.toolStripConfigure.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripConfigure.Name = "toolStripConfigure";
            this.toolStripConfigure.Size = new System.Drawing.Size(73, 23);
            this.toolStripConfigure.Text = "Configure";
            this.toolStripConfigure.Click += new System.EventHandler(this.toolStripConfigure_Click);
            // 
            // toolStripExit
            // 
            this.toolStripExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripExit.ForeColor = System.Drawing.Color.Blue;
            this.toolStripExit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripExit.Image")));
            this.toolStripExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripExit.Name = "toolStripExit";
            this.toolStripExit.Size = new System.Drawing.Size(35, 23);
            this.toolStripExit.Text = "Exit";
            this.toolStripExit.Click += new System.EventHandler(this.toolStripExit_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.buttonCapture;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 644);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.textBoxShortDescription);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.buttonCapture);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1064, 670);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "BugTracker Screen Capture";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelay)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonCapture;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.RadioButton radioButtonArea;
        private System.Windows.Forms.RadioButton radioButtonForeground;
        private System.Windows.Forms.RadioButton radioButtonDesktop;
        private System.Windows.Forms.NumericUpDown numericUpDownDelay;
        private System.Windows.Forms.Label labelDelay;
        private NumericTextBox textBoxBugId;
        private System.Windows.Forms.Label labelBugId;
        private System.Windows.Forms.RadioButton radioButtonUpdateExisting;
        private System.Windows.Forms.RadioButton radioButtonCreateNew;
        private System.Windows.Forms.TextBox textBoxShortDescription;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveAs;
        private System.Windows.Forms.ToolStripButton toolStripButtonCopy;
        private System.Windows.Forms.ToolStripButton toolStripButtonUndo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxPenType;
        private System.Windows.Forms.ToolStripButton toolStripButtonAbout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripConfigure;
        private System.Windows.Forms.ToolStripButton toolStripExit;
    }
}

