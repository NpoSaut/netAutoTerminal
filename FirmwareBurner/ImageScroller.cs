using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FirmwareBurner
{
    public class ImageScroller : Control
    {
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
            "Source", typeof (ImageSource), typeof (ImageScroller),
            new FrameworkPropertyMetadata(default(ImageSource), FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty VerticalOffsetProperty = DependencyProperty.Register(
            "VerticalOffset", typeof (Double), typeof (ImageScroller),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty VerticalScrollRangeProperty = DependencyProperty.Register(
            "VerticalScrollRange", typeof (Double), typeof (ImageScroller),
            new FrameworkPropertyMetadata(0.5, FrameworkPropertyMetadataOptions.AffectsRender));

        static ImageScroller() { DefaultStyleKeyProperty.OverrideMetadata(typeof (ImageScroller), new FrameworkPropertyMetadata(typeof (ImageScroller))); }

        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public Double VerticalOffset
        {
            get { return (Double)GetValue(VerticalOffsetProperty); }
            set { SetValue(VerticalOffsetProperty, value); }
        }

        public Double VerticalScrollRange
        {
            get { return (Double)GetValue(VerticalScrollRangeProperty); }
            set { SetValue(VerticalScrollRangeProperty, value); }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            double verticalPixelScrollRange = ActualHeight * Math.Abs(VerticalScrollRange);
            double canvasHeight = ActualHeight + verticalPixelScrollRange;
            double canvasWidth = ActualWidth;

            double scale = Math.Max(canvasWidth / Source.Width, canvasHeight / Source.Height);

            double imageWidth = Source.Width * scale;
            double imageHeight = Source.Height * scale;

            double y = -VerticalOffset * verticalPixelScrollRange;

            drawingContext.DrawImage(Source, new Rect(0, y, imageWidth, imageHeight));
        }
    }
}
