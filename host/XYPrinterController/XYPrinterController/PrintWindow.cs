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
        int printIncrement = 1;
        int currentPrintPoint = 0;

        enum printState {
            WAITING_FOR_SERIAL_RESPONSE,
            NEEDS_MOVE_TOP_LEFT,
            NEEDS_MOVE_BOTTOM_RIGHT,
            READY_TO_PRINT,
            WAITING_FOR_UNCLICK,
            WAITING_FOR_CLICK,
            PRINTING,
            FINISHED_PRINTING};
        printState currentState = printState.NEEDS_MOVE_TOP_LEFT;

        bool waitingForSerialResponse = false;

        public PrintWindow()
        {
            InitializeComponent();
        }

        private void movePrinter(object sender, EventArgs e)
        {
            int xChange = 0;
            int yChange = 0;
            if(sender == upButton)
            {
                yChange = -traverseIncrement.Value;
            } else if(sender == downButton)
            {
                yChange = traverseIncrement.Value;
            } else if(sender == leftButton)
            {
                xChange = -traverseIncrement.Value;    
            } else if(sender == rightButton)
            {
                xChange = traverseIncrement.Value;
            }
            waitingForSerialResponse = true;
            printer.SendCommand("rm", new List<dynamic> { xChange, yChange }, new XYPrinter.ResponseDelegate(printerTraversedCallback));
        }

        private void printerTraversedCallback(List<string> response)
        {
            waitingForSerialResponse = false;
        }

        private void takeNextAction(object sender, EventArgs e)
        {
            switch(currentState)
            {
                case printState.NEEDS_MOVE_TOP_LEFT:
                    handleSetTopLeftPoint();
                    break;
                case printState.NEEDS_MOVE_BOTTOM_RIGHT:
                    handleSetBottomRightPoint();
                    break;
                case printState.READY_TO_PRINT:
                    startPrinting();
                    break;
            }
        }

        private void handleSetTopLeftPoint()
        {
            waitingForSerialResponse = true;
            printer.SendCommand("p", new List<dynamic> { 0, 0 }, new XYPrinter.ResponseDelegate(topLeftPointSetCallback));
        }

        private void topLeftPointSetCallback(List<string> responses)
        {
            this.Invoke((MethodInvoker) delegate
            {
                waitingForSerialResponse = false;
                currentState = printState.NEEDS_MOVE_BOTTOM_RIGHT;
                statusLabel.Text = "Move Printer To Bottom Right";
                printViewer.paperTopLeft = new PointF(0, 0);
                printViewer.currentDrawState = PrintViewControl.drawState.TOP_LEFT;
                printViewer.Invalidate();
            });

        }

        private void handleSetBottomRightPoint()
        {
            waitingForSerialResponse = true;
            printer.SendQuery("p", new XYPrinter.ResponseDelegate(bottomRightPointQueryCallback));
        }

        private void bottomRightPointQueryCallback(List <string> responses)
        {
            this.Invoke((MethodInvoker) delegate
            {
                waitingForSerialResponse = false;
                int x = Convert.ToInt16(responses[0]);
                int y = Convert.ToInt16(responses[1]);
                preparePointsForPrinting(new SizeF(x, y));
            });
        }

        private void preparePointsForPrinting(SizeF paperSize)
        {

            // tell the printViewer what the paper size was
            printViewer.paperBottomRight = new PointF(paperSize.Width, paperSize.Height);

            // search through the points for the min x, min y, max x, max y;

            float minX, maxX, minY, maxY;
            minX = maxX = printMaterial[0].X;
            minY = maxY = printMaterial[0].Y;
            for(int i = 0; i < printMaterial.Count; i++)
            {
                PointF p = printMaterial[i];
                if(p.X > maxX) { maxX = p.X; }
                if(p.X < minX) { minX = p.X; }
                if(p.Y > maxY) { maxY = p.Y; }
                if(p.Y < minY) { minY = p.Y; }
            }

            float drawingWidth = maxX - minX;
            float drawingHeight = maxY - minY;

            float widthRatio = paperSize.Width / (float)drawingWidth;
            float heightRatio = paperSize.Height / (float)drawingHeight;
            float scaleFactor = Math.Min(widthRatio, heightRatio);

            float paperCenterX = paperSize.Width / 2;
            float paperCenterY = paperSize.Height / 2;
            float drawingCenterX = (drawingWidth / 2) + minX;
            float drawingCenterY = (drawingHeight / 2) + minY;

            for (int i = 0; i < printMaterial.Count; i++)
            {
                PointF p = printMaterial[i];
                printMaterial[i] = new PointF(((p.X - drawingCenterX) * scaleFactor) + paperCenterX, ((p.Y - drawingCenterY) * scaleFactor) + paperCenterY);
            }

            printViewer.printPoints = printMaterial;
            printViewer.currentDrawState = PrintViewControl.drawState.ALL_POINTS;
            printViewer.Invalidate();

            currentState = printState.READY_TO_PRINT;
            statusLabel.Text = "Ready To Print";

        }

        private void startPrinting()
        {
            currentState = printState.PRINTING;
            statusLabel.Text = "Printing!";
            currentPrintPoint = 0;
            sendPoint();
        }

        private void movedToPositionCallback(List<string> results)
        {
            this.Invoke((MethodInvoker)delegate
            {
                currentPrintPoint += printIncrement;
                if (currentPrintPoint < printMaterial.Count)
                {
                    waitingForSerialResponse = false;
                    sendPoint();
                }
            });
        }


        private void sendPoint()
        {
            waitingForSerialResponse = true;
            printViewer.printProgressIndex = currentPrintPoint;
            int x = (int)Math.Floor(printMaterial[currentPrintPoint].X);
            int y = (int)Math.Floor(printMaterial[currentPrintPoint].Y);
            Debug.WriteLine("X: " + x + " -- Y: " + y);
            printer.SendCommand("m", new List<dynamic> { x, y }, new XYPrinter.ResponseDelegate(movedToPositionCallback));
            printViewer.Invalidate();
        }
    }
}
