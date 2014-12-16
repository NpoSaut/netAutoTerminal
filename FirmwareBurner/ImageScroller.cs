using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FirmwareBurner
{
    public class ImageScroller : Control
    {
        public static readonly DependencyProperty UniterProperty = DependencyProperty.Register(
            "Uniter", typeof (ImageScrollerUniter), typeof (ImageScroller),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, UniterChangedCallback));

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
            "Source", typeof (ImageSource), typeof (ImageScroller),
            new FrameworkPropertyMetadata(default(ImageSource), FrameworkPropertyMetadataOptions.AffectsRender));

        static ImageScroller() { DefaultStyleKeyProperty.OverrideMetadata(typeof (ImageScroller), new FrameworkPropertyMetadata(typeof (ImageScroller))); }

        public ImageScrollerUniter Uniter
        {
            get { return (ImageScrollerUniter)GetValue(UniterProperty); }
            set { SetValue(UniterProperty, value); }
        }

        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        private static void UniterChangedCallback(DependencyObject O, DependencyPropertyChangedEventArgs PropertyChangedEventArgs)
        {
            var sender = (ImageScroller)O;
            var oldUniter = PropertyChangedEventArgs.OldValue as ImageScrollerUniter;
            if (oldUniter != null)
                sender.ReleaseFrom(oldUniter);
            var newUniter = PropertyChangedEventArgs.NewValue as ImageScrollerUniter;
            if (newUniter != null)
                sender.RegisterTo(newUniter);
        }

        private void ReleaseFrom(ImageScrollerUniter OldUniter)
        {
            OldUniter.Members.Remove(this);
            OldUniter.OffsetChanged -= OnUniterOffsetChanged;
        }

        private void RegisterTo(ImageScrollerUniter NewUniter)
        {
            NewUniter.Members.Add(this);
            NewUniter.OffsetChanged += OnUniterOffsetChanged;
        }

        private void OnUniterOffsetChanged(object Sender, EventArgs Args) { InvalidateVisual(); }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (Uniter == null) return;

            List<Rect> rects = Uniter.Members
                                     .Select(m => new Rect(m.PointToScreen(new Point(0, 0)), m.RenderSize))
                                     .ToList();

            Point p1 = PointFromScreen(new Point(rects.Min(r => r.X),
                                                 rects.Min(r => r.Y)));
            Point p2 = PointFromScreen(new Point(rects.Max(r => r.X + r.Width),
                                                 rects.Max(r => r.Y + r.Height)));

            var renderRect = new Rect(p1, p2);

            base.OnRender(drawingContext);

            double verticalPixelScrollRange = renderRect.Height * Math.Abs(Uniter.VerticalScrollRange);
            double canvasHeight = renderRect.Height + verticalPixelScrollRange;
            double canvasWidth = renderRect.Width;

            double scale = Math.Max(canvasWidth / Source.Width, canvasHeight / Source.Height);

            double imageWidth = Source.Width * scale;
            double imageHeight = Source.Height * scale;

            double y = Uniter.VerticalOffset * verticalPixelScrollRange;

            drawingContext.DrawImage(Source, new Rect(renderRect.X, renderRect.Y - y, imageWidth, imageHeight));
        }
    }

    public class ImageScrollerUniter : DependencyObject
    {
        public static readonly DependencyProperty VerticalOffsetProperty = DependencyProperty.Register(
            "VerticalOffset", typeof (Double), typeof (ImageScrollerUniter),
            new PropertyMetadata(0.0, OffsetChangedCallback));

        public static readonly DependencyProperty VerticalScrollRangeProperty = DependencyProperty.Register(
            "VerticalScrollRange", typeof (Double), typeof (ImageScrollerUniter),
            new PropertyMetadata(0.5));

        public ImageScrollerUniter() { Members = new Collection<ImageScroller>(); }
        public ICollection<ImageScroller> Members { get; private set; }

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

        private static void OffsetChangedCallback(DependencyObject O, DependencyPropertyChangedEventArgs PropertyChangedEventArgs)
        {
            var sender = (ImageScrollerUniter)O;
            sender.OnOffsetChanged();
        }

        public event EventHandler OffsetChanged;

        protected virtual void OnOffsetChanged()
        {
            EventHandler handler = OffsetChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
