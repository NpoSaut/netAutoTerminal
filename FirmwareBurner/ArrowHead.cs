using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace FirmwareBurner
{
    public enum ArrowHeadDirection
    {
        Down,
        Right
    }

    public class ArrowHead : FrameworkElement
    {
        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(
            "StrokeThickness", typeof (Double), typeof (ArrowHead),
            new FrameworkPropertyMetadata(default(Double), FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(
            "Stroke", typeof (Brush), typeof (ArrowHead), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty FillProperty = DependencyProperty.Register(
            "Fill", typeof (Brush), typeof (ArrowHead), new PropertyMetadata(default(Brush)));

        public static readonly DependencyProperty DirectionProperty = DependencyProperty.Register(
            "Direction", typeof(ArrowHeadDirection), typeof(ArrowHead), new FrameworkPropertyMetadata(ArrowHeadDirection.Right, FrameworkPropertyMetadataOptions.AffectsRender));

        static ArrowHead() { DefaultStyleKeyProperty.OverrideMetadata(typeof (ArrowHead), new FrameworkPropertyMetadata(typeof (ArrowHead))); }

        public ArrowHeadDirection Direction
        {
            get { return (ArrowHeadDirection)GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }

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
            ICollection<Point> points = null;

            switch (Direction)
            {
                case ArrowHeadDirection.Right:
                    points = new[]
                             {
                                 new Point(0, 0.5 * StrokeThickness),
                                 new Point(ActualWidth, ActualHeight / 2),
                                 new Point(0, ActualHeight - 0.5 * StrokeThickness),
                             };
                    break;
            }

            var streamGeometry = new StreamGeometry();
            if (points != null)
            {
                using (StreamGeometryContext geometryContext = streamGeometry.Open())
                {
                    geometryContext.BeginFigure(points.First(), true, false);
                    geometryContext.PolyLineTo(points.Skip(1).ToList(), true, true);
                }
            }
            return streamGeometry;
        }
    }
}
