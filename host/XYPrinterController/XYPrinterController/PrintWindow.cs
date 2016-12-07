using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XYPrinterController
{
    public partial class PrintWindow : Form
    {

        public XYPrinter printer;
        bool waitingForPosition = false;

        public PrintWindow()
        {
            InitializeComponent();
            positionRefreshTimer.Start();
        }

        private void updatePositionCallback(List<string> coordinates)
        {
            this.Invoke(new Action(() =>
            {
                if (coordinates.Count == 2)
                {
                    xPositionLabel.Text = "X: " + coordinates[0];
                    yPositionLabel.Text = "Y: " + coordinates[1];
                }
                waitingForPosition = false;
            }));
        }

        private void positionRefreshTimer_Tick(object sender, EventArgs e)
        {
            if(!waitingForPosition)
            {
                waitingForPosition = true;
                printer.SendQuery("p", new XYPrinter.ResponseDelegate(updatePositionCallback));
            }
        }
    }
}
