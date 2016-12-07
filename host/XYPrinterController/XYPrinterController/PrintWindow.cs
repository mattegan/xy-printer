using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        public List<PointF> printMaterial = new List<PointF>();
        int printIncrement = 10;
        int currentPoint = 0;

        public PrintWindow()
        {
            InitializeComponent();
        }

        private void movedToPositionCallback(List<string> results)
        {
            Debug.WriteLine(results);
            currentPoint += printIncrement;


            if (currentPoint < printMaterial.Count)
            {
                sendPoint();
            }
        }

        private void startPrint(object sender, EventArgs e)
        {
            int max_x = 0;
            int max_y = 0;
            for(int i = 0; i < printMaterial.Count; i++)
            {
                if(printMaterial[i].X > max_x)
                {
                    max_x = (int)printMaterial[i].X;
                }
                if (printMaterial[i].Y > max_x)
                {
                    max_y = (int)printMaterial[i].Y;
                }
            }
            Debug.WriteLine("X: " + max_x + " -- Y: " + max_y);
            printIncrement = printMaterial.Count / 5000;
            Debug.WriteLine(printIncrement);
            currentPoint = 0;
            sendPoint();
        }

        private void sendPoint()
        {
            int x = (int)Math.Floor(printMaterial[currentPoint].X);
            int y = (int)Math.Floor(printMaterial[currentPoint].Y);
            Debug.WriteLine("X: " + x + " -- Y: " + y);
            printer.SendCommand("m", new List<dynamic> { x, y }, new XYPrinter.ResponseDelegate(movedToPositionCallback));
        }
    }
}
