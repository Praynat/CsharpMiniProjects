using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CsharpMiniProjects.Utilities
{
    public class OutlinedText : Control
    {
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(OutlinedText),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty FontSizeProperty =
            DependencyProperty.Register("FontSize", typeof(double), typeof(OutlinedText),
                new FrameworkPropertyMetadata(36.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty FontFamilyProperty =
            DependencyProperty.Register("FontFamily", typeof(FontFamily), typeof(OutlinedText),
                new FrameworkPropertyMetadata(new FontFamily("Shrikhand"), FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(OutlinedText),
                new FrameworkPropertyMetadata(2.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register("Fill", typeof(Brush), typeof(OutlinedText),
                new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register("Stroke", typeof(Brush), typeof(OutlinedText),
                new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty TextAlignmentProperty =
    DependencyProperty.Register(
        nameof(TextAlignment),
        typeof(TextAlignment),
        typeof(OutlinedText),
        new FrameworkPropertyMetadata(TextAlignment.Left, FrameworkPropertyMetadataOptions.AffectsRender));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public FontFamily FontFamily
        {
            get => (FontFamily)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        public double StrokeThickness
        {
            get => (double)GetValue(StrokeThicknessProperty);
            set => SetValue(StrokeThicknessProperty, value);
        }

        public Brush Fill
        {
            get => (Brush)GetValue(FillProperty);
            set => SetValue(FillProperty, value);
        }

        public Brush Stroke
        {
            get => (Brush)GetValue(StrokeProperty);
            set => SetValue(StrokeProperty, value);
        }

        public TextAlignment TextAlignment
        {
            get => (TextAlignment)GetValue(TextAlignmentProperty);
            set => SetValue(TextAlignmentProperty, value);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            // Create formatted text based on the current properties
            var formattedText = new FormattedText(
                Text ?? string.Empty,
                System.Globalization.CultureInfo.InvariantCulture,
                FlowDirection.LeftToRight,
                new Typeface(FontFamily, FontStyles.Normal, FontWeights.Bold, FontStretches.Normal),
                FontSize,
                Fill,
                VisualTreeHelper.GetDpi(this).PixelsPerDip)
            {
                MaxTextWidth = availableSize.Width,
                TextAlignment = this.TextAlignment
            };

            // Return the desired size based on the formatted text
            return new Size(formattedText.Width, formattedText.Height);
        }
        protected override Size ArrangeOverride(Size finalSize)
        {
            // Accept the final size given by the layout system
            return finalSize;
        }


        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            var formattedText = new FormattedText(
                Text ?? string.Empty,
                System.Globalization.CultureInfo.InvariantCulture,
                FlowDirection.LeftToRight,
                new Typeface(FontFamily, FontStyles.Normal, FontWeights.Bold, FontStretches.Normal),
                FontSize,
                Fill,
                VisualTreeHelper.GetDpi(this).PixelsPerDip)
            {
                MaxTextWidth = RenderSize.Width,
                TextAlignment = this.TextAlignment
            };

            // Calculate the vertical center position
            double y = (RenderSize.Height - formattedText.Height) / 2;

            // Build geometry starting at (0, y)
            var geometry = formattedText.BuildGeometry(new Point(0, y));

            var pen = new Pen(Stroke, StrokeThickness);

            drawingContext.DrawGeometry(Fill, pen, geometry);
        }


    }
}
