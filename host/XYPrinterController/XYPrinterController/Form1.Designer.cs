namespace XYPrinterController
{
    partial class PrintWindow
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
            this.positionRefreshTimer = new System.Windows.Forms.Timer(this.components);
            this.xPositionLabel = new System.Windows.Forms.Label();
            this.yPositionLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // positionRefreshTimer
            // 
            this.positionRefreshTimer.Tick += new System.EventHandler(this.positionRefreshTimer_Tick);
            // 
            // xPositionLabel
            // 
            this.xPositionLabel.AutoSize = true;
            this.xPositionLabel.Location = new System.Drawing.Point(383, 151);
            this.xPositionLabel.Name = "xPositionLabel";
            this.xPositionLabel.Size = new System.Drawing.Size(17, 13);
            this.xPositionLabel.TabIndex = 0;
            this.xPositionLabel.Text = "X:";
            // 
            // yPositionLabel
            // 
            this.yPositionLabel.AutoSize = true;
            this.yPositionLabel.Location = new System.Drawing.Point(383, 178);
            this.yPositionLabel.Name = "yPositionLabel";
            this.yPositionLabel.Size = new System.Drawing.Size(17, 13);
            this.yPositionLabel.TabIndex = 1;
            this.yPositionLabel.Text = "Y:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 413);
            this.Controls.Add(this.yPositionLabel);
            this.Controls.Add(this.xPositionLabel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer positionRefreshTimer;
        private System.Windows.Forms.Label xPositionLabel;
        private System.Windows.Forms.Label yPositionLabel;
    }
}