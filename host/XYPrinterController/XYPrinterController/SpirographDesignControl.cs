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
    public partial class SpirographDesignControl : UserControl
    {
        public SpirographDesignControl()
        {
            InitializeComponent();
        }

        private void UpdateSpirographDisplay(object sender, EventArgs ee)
        {
            viewer.R = outerRadiusTrackbar.Value;
            viewer.r = innerRadiusTrackbar.Value;
            viewer.p = penOffsetTrackbar.Value;
            viewer.Invalidate();
        }

        public List<PointF> getPointsToPrint()
        {
            return viewer.points;
        }
    }
}
