using System;
using System.Windows;
using System.Windows.Media;

namespace FirmwareBurner
{
    public class ArrowHead : FrameworkElement
    {
        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(
            "StrokeThickness", typeof (Double), typeof (ArrowHead),
            new FrameworkPropertyMetadata(default(Double), FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(
            "Stroke", typeof (Brush), typeof (ArrowHead), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty FillProperty = DependencyProperty.Register(
            "Fill", typeof (Brush), typeof (ArrowHead), new PropertyMetadata(default(Brush)));

        static ArrowHead() { DefaultStyleKeyProperty.OverrideMetadata(typeof (ArrowHead), new FrameworkPropertyMetadata(typeof (ArrowHead))); }

        public Double StrokeThickness
        {
            get { return (Double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var pen = new Pen(Stroke, StrokeThickness);
            StreamGeometry geometry = GetGeometry();
            drawingContext.DrawGeometry(Fill, pen, geometry);
        }

        private StreamGeometry GetGeometry()
        {
            var point1 = new Point(0, 0.5 * StrokeThickness);
            var point2 = new Point(ActualWidth, ActualHeight / 2);
            var point3 = new Point(0, ActualHeight - 0.5 * StrokeThickness);
            var streamGeometry = new StreamGeometry();
            using (StreamGeometryContext geometryContext = streamGeometry.Open())
            {
                geometryContext.BeginFigure(point1, true, false);
                var points = new PointCollection
                             {
                                 point2,
                                 point3
                             };
                geometryContext.PolyLineTo(points, true, true);
            }
            return streamGeometry;
        }
    }
}
