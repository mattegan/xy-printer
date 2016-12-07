using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace XYPrinterController
{
    class SpirographViewer : Control 
    {

        // adjust these parameters to adjust the look of the spirograph
        public int R = 1; // large outside radius circle
        public int r = 54; // smaller inner circle
        public int p = 22; // distance of hole from center of inner circle
        public double dt = 0.2; // the number of radians the inside circle turns each draw step
        public List<PointF> points;

        public SpirographViewer()
        {
            // this supposedly prevents the redrawing of the view from flickering
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | 
                          ControlStyles.UserPaint | 
                          ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs args)
        {
            base.OnPaint(args);
            
            // draw everything with pen p into graphics g
            Graphics g = args.Graphics;
            Pen p = new Pen(Color.DarkGray);

            // translate the coordinate system to a regular cartesian
            // (0, 0) in middle, increasing x to the right, increasing y to the top
            Point center = new Point(this.Width / 2, this.Height / 2);
            g.TranslateTransform(center.X, center.Y);
            g.ScaleTransform(1, -1);

            // clear the background
            g.Clear(Color.White);

            // turn on antialiasing for some extra smoothness
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // you can some axes so we know we're right
            // g.DrawLine(p, new Point(0, 0), new Point(100, 0));
            // g.DrawLine(p, new Point(0, 0), new Point(0, 100));

            // actually draw the spirograph now

            // this representes how many t we'll iterate for, there might be a better way to
            // do this (or make it user adjustable)
            double finalTime = 2 * Math.PI * 200;
            double t = 0;

            // generate a list of points for every t += dt until finalTime, for each
            // point, keep track of the maximum radius we've seen, so that we can resize the
            // spirograph to fit inside the view            
            points = new List<PointF>();
            PointF current;
            double maxRadius = 0, currentRadius;
            while (t < finalTime)
            {
                current = CalculatePoint((t += dt) - dt);
                currentRadius = this.Radius(current);
                maxRadius = maxRadius < currentRadius ? currentRadius : maxRadius;
                points.Add(current);             
            }

            // get the maximum radius circle that will fit inside the current size of this control
            int maxAllowableRadius = (this.Width > this.Height ? this.Height : this.Width) / 2;

            // make sure that the spirograph actually had some radius (some combinations of r, R, and p will
            //  create a 0 radius spirograph)
            if (maxRadius > 0)
            {
                float scaleFactor = (float)(maxAllowableRadius / maxRadius);

                for(int i = 0; i < points.Count; i++)
                {
                    points[i] = new PointF(points[i].X * scaleFactor, points[i].Y * scaleFactor);
                }
                
                // also ensure we actually generated some points, this can happen if finalTime is too
                // small, not nessassary now since finalTime is static, but may be useful in the future   
                if (points.Count > 1)
                {
                    g.DrawLines(p, points.ToArray());
                }
            }

            p.Dispose();        
            

        }

        // scaling function that can be used by outside code
        public List<PointF> scalePoints(List<PointF> points, float scaleFactor)
        {
            List<PointF> results = new List<PointF>();
            for (int i = 0; i < points.Count; i++)
            {
                results[i] = new PointF(points[i].X * scaleFactor, points[i].Y * scaleFactor);
            }
            return results;
        }

        private double Radius(PointF p)
        {
            return Math.Sqrt(Math.Pow(p.X, 2) + Math.Pow(p.Y, 2));
        }

        private PointF CalculatePoint(double t)
        {
            // see : https://en.wikipedia.org/wiki/Spirograph
            double l = this.p / (double)this.r;
            double k = this.r / (double)this.R;
            double p1 = (1 - k);
            double p2 = (l * k);
            double p3 = ((1 - k) / k) * t;
            double x = ((p1 * Math.Cos(t)) + (p2 * Math.Cos(p3)));
            double y = ((p1 * Math.Sin(t)) - (p2 * Math.Sin(p3)));
            return new PointF((float)x, (float)y);
        }
    }
}
