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
    public partial class DrawDesignViewer : Control
    {

        public List<PointF> drawPoints = new List<PointF>();

        public DrawDesignViewer()
        {
            InitializeComponent();

            // this supposedly prevents the redrawing of the view from flickering
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs args)
        {
            // draw everything with pen p into graphics g
            Graphics g = args.Graphics;
            Pen pb = new Pen(Color.Black);

            // clear the background
            g.Clear(Color.White);

            // turn on antialiasing for some extra smoothness
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            if (drawPoints.Count > 2)
            {
                g.DrawLines(pb, drawPoints.ToArray());
            }
        }
    }
}
