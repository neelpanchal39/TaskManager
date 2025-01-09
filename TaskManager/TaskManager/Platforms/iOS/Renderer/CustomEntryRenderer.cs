using System;
using CoreGraphics;
using Foundation;
using System.Drawing;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using Microsoft.Maui.Controls.Platform;
using TaskManager.CustomControls;
using TaskManager.Platforms.iOS.Renderer;
using UIKit;
using Microsoft.Maui.Platform;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace TaskManager.Platforms.iOS.Renderer
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            var view = (CustomEntry)Element;

            if (view != null && Control != null)
            {
                Control.Enabled = view.IsEnabled;

                Control.Layer.CornerRadius = Convert.ToSingle(view.CornerRadius);
                Control.Layer.BorderColor = view.BorderColor.ToCGColor();
                Control.Layer.BorderWidth = view.BorderWidth;
                Control.ClipsToBounds = true;

                Control.Text = view.Text;
                Control.TextColor = view.TextColor?.ToUIColor();
                Control.Placeholder = view.Placeholder;

                float FontSize = view.FontSize == null ? 14 : (float)view.FontSize;

                if (view.FontFamily != null)
                {
                    UIFont font = UIFont.FromName(view.FontFamily, FontSize);
                    if (Element.Placeholder != null)
                        UpdateAttributedPlaceholder(new NSAttributedString(Element.Placeholder, font, view.PlaceholderColor?.ToUIColor()));
                }

                Control.Frame = new CGRect(0, 0, view.WidthRequest, 50);

                Control.BackgroundColor = Colors.Transparent.ToUIColor();
                SetDoneButton(view);
            }
        }

        void SetDoneButton(Entry view)
        {
            var toolbar = new UIToolbar(new RectangleF(0.0f, 0.0f, (float)Control.Frame.Size.Width, 44.0f));

            toolbar.Items = new[]
            {
                new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
                new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate { Control.ResignFirstResponder(); ((IEntryController)Element).SendCompleted(); })
            };

            this.Control.InputAccessoryView = toolbar;
        }
    }
}

