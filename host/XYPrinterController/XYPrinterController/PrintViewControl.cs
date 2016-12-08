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
    public partial class PrintViewControl : Control
    {
    
        [Flags] public enum drawState {
            NONE,
            TOP_LEFT,
            ALL_POINTS,
            CURRENT_PRINTER_POSITION
            }

        public drawState currentDrawState = drawState.NONE;

        public PointF paperTopLeft;
        public PointF paperBottomRight;
        public PointF currentPosition;
        public int padding;
        public float scaleFactor;
        
        public List<PointF> printPoints;
        public int printProgressIndex = 0;

        public PrintViewControl()
        {
            InitializeComponent();

            // set the padding to something reasonable
            padding = 20;

            // this supposedly prevents the redrawing of the view from flickering
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs args)
        {
            base.OnPaint(args);

            // draw everything with pen p into graphics g
            Graphics g = args.Graphics;
            Pen pg = new Pen(Color.DarkGray);
            Pen pb = new Pen(Color.Black);
            Pen pr = new Pen(Color.OrangeRed);

            g.Clear(Color.White);

            if (currentDrawState == drawState.TOP_LEFT)
            {
                // just draw the top left point somewhere arbitrary
                g.FillEllipse(Brushes.Black, new RectangleF(padding, padding, 4, 4));
            }

            if (currentDrawState == drawState.ALL_POINTS)
            {
                // calculate where to put these points
                int maxPossibleWidth = this.Width - (2 * padding);
                int maxPossibleHeight = this.Height - (2 * padding);

                // calculate the width and height of the printer's drawable area
                int drawableAreaWidth = (int)(paperBottomRight.X - paperTopLeft.X);
                int drawableAreaHeight = (int)(paperBottomRight.Y - paperTopLeft.Y);
                Debug.WriteLine("drawable height: " + drawableAreaHeight);
                Debug.WriteLine("drawable width: " + drawableAreaWidth);

                float widthRatio = maxPossibleWidth / (float)drawableAreaWidth;
                float heightRatio = maxPossibleHeight / (float)drawableAreaHeight;
                scaleFactor = Math.Min(widthRatio, heightRatio);

                g.DrawRectangle(pg, Rectangle.Round(new RectangleF(tickToPixelPad(paperTopLeft), new SizeF(tickToPixel(paperBottomRight)))));


                int remaining = printPoints.Count - printProgressIndex;
                if(printProgressIndex > 2)
                {
                    g.DrawLines(pr, tickToPixelPad(this.printPoints.GetRange(0, printProgressIndex)).ToArray());
                }
                if (remaining > 2)
                {
                    g.DrawLines(pg, tickToPixelPad(this.printPoints.GetRange(printProgressIndex, remaining)).ToArray());
                }

            }
        }

        private PointF tickToPixelPad(PointF p)
        {
            return new PointF((p.X * scaleFactor) + padding, (p.Y * scaleFactor) + padding);
        }

        private PointF tickToPixel(PointF p)
        {
            return new PointF((p.X * scaleFactor), (p.Y * scaleFactor));
        }

        private List<PointF>tickToPixelPad(List<PointF> points)
        {
            List<PointF> ret = new List<PointF>();
            for(int i = 0; i < points.Count; i++)
            {
                ret.Add(tickToPixelPad(points[i]));
            }
            return ret;
        }
    }
}
