using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Views;
using Fijicare.Droid;
using Fijicare.Utility.CustomEntry;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace Fijicare.Droid
{
    public class CustomEntryRenderer : EntryRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {






                CustomEntry view = (CustomEntry)Element;

                if (view.IsCurvedCornersEnabled)
                {
                    // creating gradient drawable for the curved background
                    GradientDrawable _gradientBackground = new GradientDrawable();
                    _gradientBackground.SetShape(ShapeType.Rectangle);
                    _gradientBackground.SetColor(view.BackgroundColor.ToAndroid());

                    // Thickness of the stroke line
                    // _gradientBackground.SetStroke(view.BorderWidth, view.BorderColor.ToAndroid());
                    _gradientBackground.SetStroke(0, view.BorderColor.ToAndroid());
                    // Radius for the curves
                    _gradientBackground.SetCornerRadius(
                        DpToPixels(this.Context,
                            Convert.ToSingle(view.CornerRadius)));

                    // set the background of the label
                    Control.SetBackground(_gradientBackground);
                }


                if (Control.Id == 44)
                // Set padding for the internal text from border
                {
                    Control.SetPadding(
                      (int)DpToPixels(this.Context, Convert.ToSingle(12)),
                      Control.PaddingTop,
                      (int)DpToPixels(this.Context, Convert.ToSingle(12)),
                      Control.PaddingBottom);
                }
                else
                {
                    //Control.SetPadding(
                    //                     (int)DpToPixels(this.Context, Convert.ToSingle(10)),
                    //                    (int)DpToPixels(this.Context, Convert.ToSingle(4)),
                    //                     (int)DpToPixels(this.Context, Convert.ToSingle(0)),
                    //                    (int)DpToPixels(this.Context, Convert.ToSingle(0)));

                    if (view.HorizontalTextAlignment == Xamarin.Forms.TextAlignment.Start)
                        Control.Gravity = GravityFlags.Start;
                    if (view.HorizontalTextAlignment == Xamarin.Forms.TextAlignment.Center)
                        Control.Gravity = GravityFlags.Center;
                    if (view.HorizontalTextAlignment == Xamarin.Forms.TextAlignment.End)
                        Control.Gravity = GravityFlags.End;
                     

                }
                // Control.Gravity = GravityFlags.CenterHorizontal;
            }
        }
        public static float DpToPixels(Context context, float valueInDp)
        {
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }
    }
}
