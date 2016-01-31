//===================================================================================
// Microsoft Developer & Platform Evangelism
//=================================================================================== 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// Copyright (c) Microsoft Corporation.  All Rights Reserved.
// This code is released under the terms of the MS-LPL license, 
// http://microsoftnlayerapp.codeplex.com/license
//===================================================================================

using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.Client.Controls
{
    /// <summary>
    /// Custom TextBlock control with shadow and lightning capabilities
    /// </summary>
    public class TextBlockPro : Control
    {
        #region Private Fields

        //Stores the light and shadow position
        Point lightPoint = new Point(0, 0);
        Point shadowPoint = new Point(0, 0);
        //The text composition layers
        FormattedText lightText;
        FormattedText shadowText;
        FormattedText formattedText;

        #endregion

        #region Constructor

        /// <summary>
        /// Default public parameterless constructor
        /// </summary>
        public TextBlockPro()
        {            
            InitializeText();
            this.SnapsToDevicePixels = true;
        }

        #endregion

        #region Dependency Properties

        [Category("Common Properties")]
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TextBlockPro), new FrameworkPropertyMetadata("TextBlockPro", FrameworkPropertyMetadataOptions.AffectsRender, PropertyChanged));

        [Category("Text")]
        public TextAlignment TextAlignment
        {
            get { return (TextAlignment)GetValue(TextAlignmentProperty); }
            set { SetValue(TextAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextAlignmentProperty =
            DependencyProperty.Register("TextAlignment", typeof(TextAlignment), typeof(TextBlockPro), new FrameworkPropertyMetadata(TextAlignment.Left, FrameworkPropertyMetadataOptions.AffectsRender, PropertyChanged));

        [Category("Brushes")]
        public Brush LightBrush
        {
            get { return (Brush)GetValue(LightBrushProperty); }
            set { SetValue(LightBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LightBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LightBrushProperty =
            DependencyProperty.Register("LightBrush", typeof(Brush), typeof(TextBlockPro), new FrameworkPropertyMetadata(new SolidColorBrush(Colors.White), FrameworkPropertyMetadataOptions.AffectsRender, PropertyChanged));

        [Category("Brushes")]
        public Brush ShadowBrush
        {
            get { return (Brush)GetValue(ShadowBrushProperty); }
            set { SetValue(ShadowBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShadowBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShadowBrushProperty =
            DependencyProperty.Register("ShadowBrush", typeof(Brush), typeof(TextBlockPro), new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Black), FrameworkPropertyMetadataOptions.AffectsRender, PropertyChanged));

        [Category("Custom Properties")]
        public double LightDistance
        {
            get { return (double)GetValue(LightDistanceProperty); }
            set { SetValue(LightDistanceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LightDistance.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LightDistanceProperty =
            DependencyProperty.Register("LightDistance", typeof(double), typeof(TextBlockPro), new FrameworkPropertyMetadata(0.8, FrameworkPropertyMetadataOptions.AffectsRender, PropertyChanged));

        [Category("Custom Properties")]
        public double ShadowDistance
        {
            get { return (double)GetValue(ShadowDistanceProperty); }
            set { SetValue(ShadowDistanceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShadowDistance.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShadowDistanceProperty =
            DependencyProperty.Register("ShadowDistance", typeof(double), typeof(TextBlockPro), new FrameworkPropertyMetadata(0.8, FrameworkPropertyMetadataOptions.AffectsRender, PropertyChanged));

        [Category("Custom Properties")]
        public int LightAngle
        {
            get { return (int)GetValue(LightAngleProperty); }
            set { SetValue(LightAngleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LightAngle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LightAngleProperty =
            DependencyProperty.Register("LightAngle", typeof(int), typeof(TextBlockPro), new FrameworkPropertyMetadata(135, FrameworkPropertyMetadataOptions.AffectsRender, PropertyChanged));

        [Category("Custom Properties")]
        public int ShadowAngle
        {
            get { return (int)GetValue(ShadowAngleProperty); }
            set { SetValue(ShadowAngleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShadowAngle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShadowAngleProperty =
            DependencyProperty.Register("ShadowAngle", typeof(int), typeof(TextBlockPro), new FrameworkPropertyMetadata(315, FrameworkPropertyMetadataOptions.AffectsRender, PropertyChanged));


        #endregion

        #region Internal Methods

        /// <summary>
        /// Validate text to avoid null reference exceptions
        /// </summary>
        internal void ValidateNullText()
        {
            if (this.Text == null)
            {
                this.Text = String.Empty;
            }
        }

        /// <summary>
        /// Set all values to composition text layers
        /// </summary>
        internal void InitializeText()
        {
            ValidateNullText();
            lightText = InitializeText(this.LightBrush);
            shadowText = InitializeText(this.ShadowBrush);
            formattedText = InitializeText(this.Foreground);
        }

        /// <summary>
        /// Set Properties to individual FormattedText layer
        /// </summary>
        /// <param name="color">The Foreground color</param>
        /// <returns>FormattedText initialized with new values</returns>
        internal FormattedText InitializeText(Brush color)
        {
            return new FormattedText(
                this.Text,
                CultureInfo.CurrentUICulture,
                this.FlowDirection,
                new Typeface(this.FontFamily, this.FontStyle, this.FontWeight, this.FontStretch),
                this.FontSize,
                color);
        }

        /// <summary>
        /// Set properties to FormattedText in render time
        /// </summary>
        /// <param name="fText">The instance of FormattedText</param>
        internal void SetTextProperties(FormattedText fText)
        {
            if (this.ActualHeight > 0)
            {
                fText.MaxTextHeight = this.ActualHeight;
            }
            if (this.ActualWidth > 0)
            {
                fText.MaxTextWidth = this.ActualWidth;
            }
            fText.TextAlignment = this.TextAlignment;
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// Set the actual size to control
        /// </summary>
        /// <param name="sizeAvailable">The new desired size</param>
        /// <returns>The new size of control's instance</returns>
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            InitializeText();
            Size sizeDesired = new Size(formattedText.Width,
                                        formattedText.Height);
            sizeDesired.Width = Math.Min(sizeDesired.Width, sizeAvailable.Width) + 0.000001;
            sizeDesired.Height = Math.Min(sizeDesired.Height, sizeAvailable.Height) + 0.000001;
            return sizeDesired;
        }

        /// <summary>
        /// Renders the text composition layers
        /// </summary>
        /// <param name="drawingContext">The actual drawing context</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            ValidateNullText();
            InitializeText();
            //Set properties and draw text
            
            SetTextProperties(lightText);
            drawingContext.DrawText(lightText, lightPoint);
            SetTextProperties(shadowText);
            drawingContext.DrawText(shadowText, shadowPoint);
            SetTextProperties(formattedText);
            drawingContext.DrawText(formattedText, new Point(0, 0));
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Updates the values of the properties
        /// </summary>
        /// <param name="sender">The actual instance</param>
        /// <param name="e">Value changed parameters</param>
        private static void PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TextBlockPro control = (TextBlockPro)sender;
            control.InitializeText();
            control.lightPoint.Y = -Math.Sin(2 * Math.PI * control.LightAngle / 360) * control.LightDistance;
            control.lightPoint.X = Math.Cos(2 * Math.PI * control.LightAngle / 360) * control.LightDistance;
            control.shadowPoint.Y = -Math.Sin(2 * Math.PI * control.ShadowAngle / 360) * control.ShadowDistance;
            control.shadowPoint.X = Math.Cos(2 * Math.PI * control.ShadowAngle / 360) * control.ShadowDistance;
        }

        #endregion
    }
}

