using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XYPrinterController
{
    public partial class DrawDesignControl : UserControl, DrawControl
    {

        public DrawDesignControl()
        {
            InitializeComponent();
        }

        private void drawMouseDown(object sender, MouseEventArgs e)
        {
            Point mousePoint = drawDesignViewer.PointToClient(Cursor.Position);
            drawDesignViewer.drawPoints = new List<PointF>();
            drawDesignViewer.drawPoints.Add(mousePoint);
        }

        private void drawMouseMove(object sender, MouseEventArgs e)
        {
            if(MouseButtons == MouseButtons.Left)
            {
                Point mousePoint = drawDesignViewer.PointToClient(Cursor.Position);
                drawDesignViewer.drawPoints.Add(mousePoint);
                drawDesignViewer.Invalidate();
            }
        }

        public List<PointF> getPointsToPrint()
        {
            return drawDesignViewer.drawPoints;
        }
    }
}
