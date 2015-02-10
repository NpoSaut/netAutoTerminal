using System;
using System.Windows;
using System.Windows.Controls;

namespace FirmwareBurner
{
    public class CircleProgress : ProgressBar
    {
        public static readonly DependencyProperty InnerRadiusPortionProperty = DependencyProperty.Register(
            "InnerRadiusPortion", typeof (Double), typeof (CircleProgress),
            new FrameworkPropertyMetadata(default(Double), FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty ProgressPortionProperty = DependencyProperty.Register(
            "ProgressPortion", typeof (Double), typeof (CircleProgress),
            new FrameworkPropertyMetadata(default(Double), FrameworkPropertyMetadataOptions.AffectsRender));

        static CircleProgress() { DefaultStyleKeyProperty.OverrideMetadata(typeof (CircleProgress), new FrameworkPropertyMetadata(typeof (CircleProgress))); }

        public Double InnerRadiusPortion
        {
            get { return (Double)GetValue(InnerRadiusPortionProperty); }
            set { SetValue(InnerRadiusPortionProperty, value); }
        }

        public Double ProgressPortion
        {
            get { return (Double)GetValue(ProgressPortionProperty); }
            private set { SetValue(ProgressPortionProperty, value); }
        }

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            UpdateProgressPortion();
        }

        protected override void OnMaximumChanged(double oldMaximum, double newMaximum)
        {
            base.OnMaximumChanged(oldMaximum, newMaximum);
            UpdateProgressPortion();
        }

        private void UpdateProgressPortion() { ProgressPortion = Value / Maximum; }
    }
}
