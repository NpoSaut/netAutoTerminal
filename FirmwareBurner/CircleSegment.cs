using System;
using System.Windows;
using System.Windows.Media;

namespace FirmwareBurner
{
    public class CircleSegment : FrameworkElement
    {
        public static readonly DependencyProperty BackgroundProperty = DependencyProperty.Register(
            "Background", typeof (Brush), typeof (CircleSegment), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty StartPortionProperty = DependencyProperty.Register(
            "StartPortion", typeof (Double), typeof (CircleSegment),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty SegmentPortionProperty = DependencyProperty.Register(
            "SegmentPortion", typeof (Double), typeof (CircleSegment),
            new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty InnerRadiusPortionProperty = DependencyProperty.Register(
            "InnerRadiusPortion", typeof (Double), typeof (CircleSegment),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

        static CircleSegment() { DefaultStyleKeyProperty.OverrideMetadata(typeof (CircleSegment), new FrameworkPropertyMetadata(typeof (CircleSegment))); }

        /// <summary>Внутренний радиус (в долях от ширины)</summary>
        public Double InnerRadiusPortion
        {
            get { return (Double)GetValue(InnerRadiusPortionProperty); }
            set { SetValue(InnerRadiusPortionProperty, value); }
        }

        /// <summary>Заливка</summary>
        public Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        /// <summary>Начальная позиция (в долях от полной окружности)</summary>
        public Double StartPortion
        {
            get { return (Double)GetValue(StartPortionProperty); }
            set { SetValue(StartPortionProperty, value); }
        }

        /// <summary>Ширина сегмента (в долях от полной окружности)</summary>
        public Double SegmentPortion
        {
            get { return (Double)GetValue(SegmentPortionProperty); }
            set { SetValue(SegmentPortionProperty, value); }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            double penThickness = Math.Min(ActualHeight, ActualWidth) * (1 - InnerRadiusPortion) / 2;
            var rect = new Rect(new Point(penThickness / 2, penThickness / 2), new Size(ActualWidth - penThickness, ActualHeight - penThickness));
            GeometryDrawing arc = CreateArcDrawing(rect, 360.0 * StartPortion - 90, 359.99 * SegmentPortion);

            drawingContext.DrawGeometry(Background, new Pen(Background, penThickness), arc.Geometry);
        }

        /// <summary>Create an Arc geometry drawing of an ellipse or circle</summary>
        /// <param name="rect">Box to hold the whole ellipse described by the arc</param>
        /// <param name="startDegrees">Start angle of the arc degrees within the ellipse. 0 degrees is a line to the right.</param>
        /// <param name="sweepDegrees">Sweep angle, -ve = Counterclockwise, +ve = Clockwise</param>
        /// <returns>GeometryDrawing object</returns>
        private static GeometryDrawing CreateArcDrawing(Rect rect, double startDegrees, double sweepDegrees)
        {
            // degrees to radians conversion
            double startRadians = startDegrees * Math.PI / 180.0;
            double sweepRadians = sweepDegrees * Math.PI / 180.0;

            // x and y radius
            double dx = rect.Width / 2;
            double dy = rect.Height / 2;

            // determine the start point 
            double xs = rect.X + dx + (Math.Cos(startRadians) * dx);
            double ys = rect.Y + dy + (Math.Sin(startRadians) * dy);

            // determine the end point 
            double xe = rect.X + dx + (Math.Cos(startRadians + sweepRadians) * dx);
            double ye = rect.Y + dy + (Math.Sin(startRadians + sweepRadians) * dy);

            // draw the arc into a stream geometry
            var streamGeom = new StreamGeometry();
            using (StreamGeometryContext ctx = streamGeom.Open())
            {
                bool isLargeArc = Math.Abs(sweepDegrees) > 180;
                SweepDirection sweepDirection = sweepDegrees < 0 ? SweepDirection.Counterclockwise : SweepDirection.Clockwise;

                ctx.BeginFigure(new Point(xs, ys), false, false);
                ctx.ArcTo(new Point(xe, ye), new Size(dx, dy), 0, isLargeArc, sweepDirection, true, false);
            }

            // create the drawing
            var drawing = new GeometryDrawing();
            drawing.Geometry = streamGeom;
            return drawing;
        }
    }
}
