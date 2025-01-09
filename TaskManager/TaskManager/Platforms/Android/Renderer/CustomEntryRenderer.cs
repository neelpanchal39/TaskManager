using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;
using TaskManager.CustomControls;
using TaskManager.Platforms.Android.Renderer;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace TaskManager.Platforms.Android.Renderer
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            var view = (CustomEntry)Element;

            if (view != null && Control != null)
            {
                var textColor = view.TextColor != null ? view.TextColor.ToAndroid() : Colors.Black.ToAndroid();
                var placeholderColor = view.PlaceholderColor != null ? view.PlaceholderColor.ToAndroid() : Colors.Gray.ToAndroid();
                float FontSize = view.FontSize == null ? 14 : (float)view.FontSize;
                var BackgroundColor = view.BackgroundColor == null ? Colors.Transparent : view.BackgroundColor;

                Control.Enabled = view.IsEnabled;
                //Control.SetBackgroundColor(global::Android.Graphics.Color.Argb(0, 0, 0, 0));
                Control.Text = view.Text;
                Control.SetTextColor(textColor);
                Control.Hint = view.Placeholder;
                Control.SetHintTextColor(placeholderColor);
                Control.TextSize = FontSize;

                var _gradientBackground = new GradientDrawable();
                _gradientBackground.SetShape(ShapeType.Rectangle);
                _gradientBackground.SetColor(BackgroundColor.ToAndroid());

                // Thickness of the stroke line  
                _gradientBackground.SetStroke(view.BorderWidth, view.BorderColor.ToAndroid());

                // Radius for the curves  
                _gradientBackground.SetCornerRadius(DpToPixels(this.Context, Convert.ToSingle(view.CornerRadius)));
                _gradientBackground.SetGradientRadius(DpToPixels(this.Context, Convert.ToSingle(view.CornerRadius)));

                // set the background of the   
                Control.SetBackground(_gradientBackground);

                //Typeface tf = Typeface.CreateFromAsset(Context.Assets, view.FontFamily);
                //Control.SetTypeface(tf, TypefaceStyle.Normal);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Element != null)
            {
                var view = (CustomEntry)Element;

                var BackgroundColor = view.BackgroundColor == null ? Colors.Transparent : view.BackgroundColor;

                // creating gradient drawable for the curved background  
                var _gradientBackground = new GradientDrawable();
                _gradientBackground.SetShape(ShapeType.Rectangle);
                _gradientBackground.SetColor(BackgroundColor.ToAndroid());

                // Thickness of the stroke line  
                _gradientBackground.SetStroke(view.BorderWidth, view.BorderColor.ToAndroid());

                // Radius for the curves  
                _gradientBackground.SetCornerRadius(DpToPixels(this.Context, Convert.ToSingle(view.CornerRadius)));
                _gradientBackground.SetGradientRadius(DpToPixels(this.Context, Convert.ToSingle(view.CornerRadius)));

                // set the background of the   
                Control.SetBackground(_gradientBackground);

                // Set padding for the internal text from border  
                Control.SetPadding((int)DpToPixels(this.Context, Convert.ToSingle(12)), Control.PaddingTop, (int)DpToPixels(this.Context, Convert.ToSingle(12)), Control.PaddingBottom);
            }
        }

        public static float DpToPixels(Context context, float valueInDp)
        {
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }
    }
}

