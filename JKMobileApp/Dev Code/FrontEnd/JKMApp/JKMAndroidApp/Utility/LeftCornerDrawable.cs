using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics.Drawables;

namespace JKMAndroidApp.Utility
{
    public static class LeftCornerDrawable
    {
        public static void CustomView(View v, int borderColor)
        {
            GradientDrawable shape = new GradientDrawable();
            shape.SetShape(ShapeType.Rectangle);
            shape.SetCornerRadii(new float[] { 8, 0, 8, 0, 0, 0, 0, 0 });
            v.SetBackgroundDrawable(shape);
        }

        public static void RoundCornerView(View v)
        {
            GradientDrawable shape = new GradientDrawable();
            shape.SetShape(ShapeType.Rectangle);
            shape.SetCornerRadii(new float[] { 12, 12, 12, 12, 12, 12, 12, 12 });
            v.SetBackgroundDrawable(shape);
        }
    }
}