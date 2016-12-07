namespace XYPrinterController
{
    partial class SpirographDesignControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.penOffsetTrackbar = new System.Windows.Forms.TrackBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.innerRadiusTrackbar = new System.Windows.Forms.TrackBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.outerRadiusTrackbar = new System.Windows.Forms.TrackBar();
            this.viewer = new XYPrinterController.SpirographViewer();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.penOffsetTrackbar)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.innerRadiusTrackbar)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outerRadiusTrackbar)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(3, 429);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1010, 92);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Spirograph Parameters";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1004, 73);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox4.Controls.Add(this.penOffsetTrackbar);
            this.groupBox4.Location = new System.Drawing.Point(671, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(330, 67);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Pen Offset";
            // 
            // penOffsetTrackbar
            // 
            this.penOffsetTrackbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.penOffsetTrackbar.Location = new System.Drawing.Point(3, 16);
            this.penOffsetTrackbar.Maximum = 50;
            this.penOffsetTrackbar.Minimum = 1;
            this.penOffsetTrackbar.Name = "penOffsetTrackbar";
            this.penOffsetTrackbar.Size = new System.Drawing.Size(324, 45);
            this.penOffsetTrackbar.TabIndex = 1;
            this.penOffsetTrackbar.TickFrequency = 20;
            this.penOffsetTrackbar.Value = 1;
            this.penOffsetTrackbar.ValueChanged += new System.EventHandler(this.UpdateSpirographDisplay);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox3.Controls.Add(this.innerRadiusTrackbar);
            this.groupBox3.Location = new System.Drawing.Point(337, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(328, 67);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Inner Radius";
            // 
            // innerRadiusTrackbar
            // 
            this.innerRadiusTrackbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.innerRadiusTrackbar.Location = new System.Drawing.Point(3, 16);
            this.innerRadiusTrackbar.Maximum = 100;
            this.innerRadiusTrackbar.Minimum = -100;
            this.innerRadiusTrackbar.Name = "innerRadiusTrackbar";
            this.innerRadiusTrackbar.Size = new System.Drawing.Size(322, 45);
            this.innerRadiusTrackbar.TabIndex = 1;
            this.innerRadiusTrackbar.TickFrequency = 20;
            this.innerRadiusTrackbar.Value = 1;
            this.innerRadiusTrackbar.ValueChanged += new System.EventHandler(this.UpdateSpirographDisplay);
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox2.Controls.Add(this.outerRadiusTrackbar);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(328, 67);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Outer Radius";
            // 
            // outerRadiusTrackbar
            // 
            this.outerRadiusTrackbar.Cursor = System.Windows.Forms.Cursors.Default;
            this.outerRadiusTrackbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.outerRadiusTrackbar.Location = new System.Drawing.Point(3, 16);
            this.outerRadiusTrackbar.Maximum = 100;
            this.outerRadiusTrackbar.Minimum = 1;
            this.outerRadiusTrackbar.Name = "outerRadiusTrackbar";
            this.outerRadiusTrackbar.Size = new System.Drawing.Size(322, 45);
            this.outerRadiusTrackbar.TabIndex = 0;
            this.outerRadiusTrackbar.TickFrequency = 20;
            this.outerRadiusTrackbar.Value = 50;
            this.outerRadiusTrackbar.ValueChanged += new System.EventHandler(this.UpdateSpirographDisplay);
            // 
            // viewer
            // 
            this.viewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.viewer.Location = new System.Drawing.Point(0, 0);
            this.viewer.Name = "viewer";
            this.viewer.Size = new System.Drawing.Size(1016, 423);
            this.viewer.TabIndex = 0;
            this.viewer.Text = "spirographViewer1";
            // 
            // SpirographDesignControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.viewer);
            this.Name = "SpirographDesignControl";
            this.Size = new System.Drawing.Size(1016, 524);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.penOffsetTrackbar)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.innerRadiusTrackbar)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outerRadiusTrackbar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SpirographViewer viewer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TrackBar penOffsetTrackbar;
        private System.Windows.Forms.TrackBar innerRadiusTrackbar;
        private System.Windows.Forms.TrackBar outerRadiusTrackbar;
    }
}
