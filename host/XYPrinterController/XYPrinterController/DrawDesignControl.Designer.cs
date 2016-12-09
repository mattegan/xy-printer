namespace XYPrinterController
{
    partial class DrawDesignControl
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
            this.drawDesignViewer = new XYPrinterController.DrawDesignViewer();
            this.SuspendLayout();
            // 
            // drawDesignViewer
            // 
            this.drawDesignViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawDesignViewer.Location = new System.Drawing.Point(0, 0);
            this.drawDesignViewer.Name = "drawDesignViewer";
            this.drawDesignViewer.Size = new System.Drawing.Size(944, 558);
            this.drawDesignViewer.TabIndex = 0;
            this.drawDesignViewer.Text = "drawDesignViewer1";
            this.drawDesignViewer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawMouseDown);
            this.drawDesignViewer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawMouseMove);
            // 
            // DrawDesignControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.drawDesignViewer);
            this.Name = "DrawDesignControl";
            this.Size = new System.Drawing.Size(944, 558);
            this.ResumeLayout(false);

        }

        #endregion

        private DrawDesignViewer drawDesignViewer;
    }
}
