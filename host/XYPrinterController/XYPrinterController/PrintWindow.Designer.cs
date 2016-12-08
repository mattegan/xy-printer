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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintWindow));
            this.actionButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.rightButton = new System.Windows.Forms.Button();
            this.upButton = new System.Windows.Forms.Button();
            this.downButton = new System.Windows.Forms.Button();
            this.leftButton = new System.Windows.Forms.Button();
            this.traverseIncrement = new System.Windows.Forms.TrackBar();
            this.printViewer = new XYPrinterController.PrintViewControl();
            ((System.ComponentModel.ISupportInitialize)(this.traverseIncrement)).BeginInit();
            this.SuspendLayout();
            // 
            // actionButton
            // 
            this.actionButton.Location = new System.Drawing.Point(894, 532);
            this.actionButton.Name = "actionButton";
            this.actionButton.Size = new System.Drawing.Size(75, 23);
            this.actionButton.TabIndex = 0;
            this.actionButton.Text = "Next >";
            this.actionButton.UseVisualStyleBackColor = true;
            this.actionButton.Click += new System.EventHandler(this.takeNextAction);
            // 
            // statusLabel
            // 
            this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.statusLabel.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.Location = new System.Drawing.Point(758, 490);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(210, 27);
            this.statusLabel.TabIndex = 2;
            this.statusLabel.Text = "Move Printer To Top Right";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rightButton
            // 
            this.rightButton.Location = new System.Drawing.Point(894, 70);
            this.rightButton.Name = "rightButton";
            this.rightButton.Size = new System.Drawing.Size(50, 50);
            this.rightButton.TabIndex = 13;
            this.rightButton.Text = "right";
            this.rightButton.UseVisualStyleBackColor = true;
            this.rightButton.Click += new System.EventHandler(this.movePrinter);
            // 
            // upButton
            // 
            this.upButton.Location = new System.Drawing.Point(844, 21);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(50, 50);
            this.upButton.TabIndex = 12;
            this.upButton.Text = "up";
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.movePrinter);
            // 
            // downButton
            // 
            this.downButton.Location = new System.Drawing.Point(844, 119);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(50, 50);
            this.downButton.TabIndex = 11;
            this.downButton.Text = "down";
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Click += new System.EventHandler(this.movePrinter);
            // 
            // leftButton
            // 
            this.leftButton.Location = new System.Drawing.Point(795, 70);
            this.leftButton.Name = "leftButton";
            this.leftButton.Size = new System.Drawing.Size(50, 50);
            this.leftButton.TabIndex = 10;
            this.leftButton.Text = "left";
            this.leftButton.UseVisualStyleBackColor = true;
            this.leftButton.Click += new System.EventHandler(this.movePrinter);
            // 
            // traverseIncrement
            // 
            this.traverseIncrement.Location = new System.Drawing.Point(762, 185);
            this.traverseIncrement.Maximum = 3000;
            this.traverseIncrement.Minimum = 10;
            this.traverseIncrement.Name = "traverseIncrement";
            this.traverseIncrement.Size = new System.Drawing.Size(207, 45);
            this.traverseIncrement.TabIndex = 14;
            this.traverseIncrement.TickFrequency = 200;
            this.traverseIncrement.Value = 1000;
            // 
            // printViewer
            // 
            this.printViewer.Location = new System.Drawing.Point(12, 12);
            this.printViewer.Name = "printViewer";
            this.printViewer.Size = new System.Drawing.Size(740, 540);
            this.printViewer.TabIndex = 1;
            this.printViewer.Text = "printViewControl1";
            // 
            // PrintWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 567);
            this.Controls.Add(this.traverseIncrement);
            this.Controls.Add(this.rightButton);
            this.Controls.Add(this.upButton);
            this.Controls.Add(this.downButton);
            this.Controls.Add(this.leftButton);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.printViewer);
            this.Controls.Add(this.actionButton);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PrintWindow";
            this.Text = "Print";
            ((System.ComponentModel.ISupportInitialize)(this.traverseIncrement)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button actionButton;
        private PrintViewControl printViewer;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button rightButton;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.Button leftButton;
        private System.Windows.Forms.TrackBar traverseIncrement;
    }
}