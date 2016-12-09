using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XYPrinterController
{
    public partial class MainWindow : Form
    {

        XYPrinter selectedPrinter;
        List<XYPrinter> availablePrinters = new List<XYPrinter>();
        UserControl currentDrawControl;

        public MainWindow()
        {
            InitializeComponent();
            selectedPrinter = new XYPrinter("COM3");
        }

        private void spirographToolStripMenuItem_Click(object sender, EventArgs e)
        {
            removeCurrentDrawControl();
            currentDrawControl = new SpirographDesignControl();
            this.Controls.Add(currentDrawControl);
            this.Controls.SetChildIndex(currentDrawControl, 0);
            currentDrawControl.Dock = DockStyle.Fill;
        }


        private void newLineDrawingButtonClick(object sender, EventArgs e)
        {
            removeCurrentDrawControl();
            currentDrawControl = new DrawDesignControl();
            this.Controls.Add(currentDrawControl);
            this.Controls.SetChildIndex(currentDrawControl, 0);
            currentDrawControl.Dock = DockStyle.Fill;
        }

        private void removeCurrentDrawControl()
        {
            if(currentDrawControl != null)
            {
                this.Controls.Remove(currentDrawControl);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }

        // eventually this will populate the XY printers list in the menu bar
        private void CheckForNewXYPrinters(object sender, EventArgs e)
        {
            //string[] availablePorts = SerialPort.GetPortNames();
            //for(int i = 0; i < availablePorts.Count(); i++)
            //{
            //    string name = availablePorts[i];

            //    if(selectedPrinter.port.PortName != name)
            //    {

            //    }
            //    XYPrinter testPrinter = new XYPrinter(availablePorts[i]);
            //    Stopwatch timeoutWatch = new Stopwatch();
            //    XYPrinter.ResponseDelegate del = new XYPrinter.ResponseDelegate((args) =>
            //    {

            //    })
            //}
        }

        private void printerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printerMonitorToolStripMenuItem.Enabled = false;
            printToolStripMenuItem.Enabled = false;
            if (selectedPrinter != null)
            {
                if(selectedPrinter.port.IsOpen)
                {
                    printerMonitorToolStripMenuItem.Enabled = true;
                    printToolStripMenuItem.Enabled = true;
                }
            }
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(printToolStripMenuItem.Enabled == true)
            {
                // check to make sure printer is connected
                if(selectedPrinter.port.IsOpen)
                {
                    // now open the print window, passing a reference to the printer
                    PrintWindow printWindow = new PrintWindow();
                    printWindow.printer = selectedPrinter;
                    printWindow.printMaterial = ((DrawControl)currentDrawControl).getPointsToPrint();
                    printWindow.ShowDialog();
                }
            }
        }

    }
}
